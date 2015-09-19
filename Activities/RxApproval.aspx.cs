using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NLog;
public partial class RxApproval : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    PatientInfoDAL Pat_Info = new PatientInfoDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");
            
            if (!Page.IsPostBack)
            {
                setcontextkey();
                //if ((string)Session["Role"] == "D")
                //{
                //    lblDoctor.Visible = true;
                //    txtDocName.Visible = false;

                //}
                SqlConnection sqlCon = new SqlConnection(conStr);

                string sqlQuery = "SELECT [Rx_Type],[Rx_Type_Description] FROM [Rx_Types]";



                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
                DataSet dsDeliveryModes = new DataSet();
                sqlDa.Fill(dsDeliveryModes);

                rbtnRequestType.DataSource = dsDeliveryModes;
                rbtnRequestType.DataTextField = "Rx_Type_Description";
                rbtnRequestType.DataValueField = "Rx_Type";
                rbtnRequestType.DataBind();
                //rbtnRequestType.SelectedIndex = 0;
                Filldata();
                RenderJSArrayWithCliendIds(txtQuantity, rbtnDecision);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void setcontextkey()
    {
        objNLog.Info("Function Started..setcontextkey.");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd;

            if ((string)Session["Role"] == "C" || (string)Session["Role"] == "D")
            {
                sqlCmd = new SqlCommand("sp_getClinics", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
                sp_UserID.Value = (string)Session["User"];

                SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
                sp_UserRole.Value = (string)Session["Role"];



                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet dsClinicList = new DataSet();


                sqlDa.Fill(dsClinicList, "ClinicList");

                if (dsClinicList.Tables[0].Rows.Count > 0)
                {
                    AutoCompleteExtender2.ContextKey = dsClinicList.Tables[0].Rows[0]["Clinic_ID"].ToString();

                }
            }
            else if (Request.QueryString["RxRequestID"].ToString() != null)
            {

                sqlCmd = new SqlCommand("select facility_Info.Clinic_ID from facility_Info,Patient_Info,Rx_Drug_Requests where facility_Info.Facility_ID=Patient_Info.Facility_ID and  Patient_Info.pat_ID=Rx_Drug_Requests.pat_ID  and Rx_Drug_Requests.Rx_Req_ID=" + int.Parse((string)Request.QueryString["RxRequestID"]), sqlCon);

                sqlCon.Open();
                string clinicID = sqlCmd.ExecuteScalar().ToString();
                sqlCon.Close();
                AutoCompleteExtender2.ContextKey = clinicID;

            }
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        } 
    
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Doc_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Doc_List.Clear();
        DataTable Doc_Names = Pat_Info.get_DoctorNames(prefixText, contextKey);
        if (Doc_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Doc_Names.Rows)
            {
                Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString().Trim() + "," + dr[0].ToString().Trim(), dr[2].ToString()));
            }
        }
        else
        {
            Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Doctor Found...", "0"));
        }
        return Doc_List.ToArray();
    }

    protected void Filldata()
    {
        objNLog.Info("Function Started...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getRxRequestQueue", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_RequestID = sqlCmd.Parameters.Add("@Rx_Req_ID", SqlDbType.Int);
            par_RequestID.Value = Request.QueryString["RxRequestID"].ToString();

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxRequest = new DataSet();
            sqlDa.Fill(dsRxRequest, "RxRequest");
            txtDocName.Text = dsRxRequest.Tables[0].Rows[0]["doctorName"].ToString();
            hidDocID.Value = dsRxRequest.Tables[0].Rows[0]["Doc_ID"].ToString();
            lblpatientName.Text = dsRxRequest.Tables[0].Rows[0]["PatientName"].ToString();
            lblPatContact.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Pat_Contact"].ToString();
            txtDrug.Text = dsRxRequest.Tables[0].Rows[0]["Rx_DrugName"].ToString();
            txtQuantity.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Qty"].ToString();
            txtSIG.Text = dsRxRequest.Tables[0].Rows[0]["Rx_SIG"].ToString();
            ddlRefills.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Refills"].ToString();
            txtPharmacy.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Phrm"].ToString();
            txtComments.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Request_Comments"].ToString();
            rbtnDecision.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Status"].ToString();
            rbtnRequestType.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Type"].ToString();

            if (txtPharmacy.Text.Trim() == "ADiO Pharmacy")
                rbtnPhrm.SelectedValue = "0";
            else
            {
                rbtnPhrm.SelectedValue = "1";
                txtPharmacy.Visible = true;
            }

            if (dsRxRequest.Tables[0].Rows[0]["Rx_Status"].ToString() != "N")
            {
                if ((string)Session["Role"] != "A" && (string)Session["Role"] != "M")
                {
                    btnSave.Visible = false;
                }
            }
            if (rbtnDecision.SelectedValue == "A")
            {
                btnSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            if (rbtnDecision.SelectedValue == "R")
            {
                UpdateRxMedRequest();
            }
            else
            {
                int DrugID = Pat_Info.Get_MedID(txtDrug.Text.Trim());

                if (DrugID > 0)  // If entered drug name doesn't exist
                {
                    if (txtQuantity.Text == "" || txtQuantity.Text == "0")
                    {
                        lblMsg.Text = "Drug Quantity can not be empty or zero.";
                        lblMsg.Visible = true;
                    }
                    else
                    {
                        UpdateRxMedRequest();
                        
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid Drug Name - " + txtDrug.Text;
                    lblMsg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
  
    protected void btnUserCancel_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("RxRequestQueue.aspx");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "window.close()", true);
        //Response.Close();
    }

    //GetMedicationNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetMedicationNames(string prefixText, int count)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Med_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Med_List.Clear();
        DataTable Med_Names = Pat_Info.get_MedicationNames(prefixText);
        if (Med_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Med_Names.Rows)
            {
                Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
            }
        }
        else
        {
            Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Medication Found..", "0"));
        }
        return Med_List.ToArray();
    }
   
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetFacilityNames(string prefixText, int count, string contextKey)
    {
        List<string> Fac_List = new List<string>();
        PatientInfoDAL pat_Info = new PatientInfoDAL();
        if (contextKey == "0")
        {
            Fac_List.Clear();
            DataTable dtFac = pat_Info.get_FacilityNames(prefixText, count, contextKey);
            foreach (DataRow dr in dtFac.Rows)
            {
                Fac_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Facility_Name"].ToString(), dr[0].ToString()));
            }
        }

        return Fac_List.ToArray();
    }
    public void RenderJSArrayWithCliendIds(params Control[] wc)
    {
        try
        {
            if (wc.Length > 0)
            {
                StringBuilder arrClientIDValue = new StringBuilder();
                StringBuilder arrServerIDValue = new StringBuilder();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Now loop through the controls and build the client and server id's
                for (int i = 0; i < wc.Length; i++)
                {
                    arrClientIDValue.Append("\"" + wc[i].ClientID + "\",");
                    arrServerIDValue.Append("\"" + wc[i].ID + "\",");
                }
                // Register the array of client and server id to the client
                cs.RegisterArrayDeclaration("MyClientID", arrClientIDValue.ToString().Remove(arrClientIDValue.ToString().Length - 1, 1));
                cs.RegisterArrayDeclaration("MyServerID", arrServerIDValue.ToString().Remove(arrServerIDValue.ToString().Length - 1, 1));
                // Now register the method GetClientId, used to get the client id of the control
                cs.RegisterStartupScript(this.Page.GetType(), "key", "\nfunction GetClientId(serverId)\n{\nfor(i=0; i<MyServerID.length; i++)\n{\nif ( MyServerID[i] == serverId )\n{\nreturn MyClientID[i];\nbreak;\n}\n}\n}", true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void rbtnPhrm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnPhrm.SelectedValue == "1")
        {
            txtPharmacy.Text = "Other Pharmacy";
            txtPharmacy.Visible = true;
        }
        else
        {
            txtPharmacy.Visible = false;
        }

    }

    private void UpdateRxMedRequest()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_Update_PatientMedRequest", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        try
        {
            SqlParameter sp_MedicationName = sqlCmd.Parameters.Add("@Medicine", SqlDbType.VarChar, 50);
            sp_MedicationName.Value = txtDrug.Text.Trim();

            SqlParameter sp_Qty = sqlCmd.Parameters.Add("@Quantity", SqlDbType.Int);
            sp_Qty.Value = Int32.Parse(txtQuantity.Text);

            SqlParameter sp_ReqID = sqlCmd.Parameters.Add("@Rx_Req_ID", SqlDbType.Int);
            sp_ReqID.Value = Int32.Parse(Request.QueryString["RxRequestID"].ToString());

            SqlParameter sp_SIG = sqlCmd.Parameters.Add("@SIG", SqlDbType.VarChar, 50);
            sp_SIG.Value = txtSIG.Text;

            SqlParameter sp_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            sp_RxType.Value = rbtnRequestType.SelectedValue;

            SqlParameter sp_RxStatus = sqlCmd.Parameters.Add("@RX_Status", SqlDbType.Char, 1);
            sp_RxStatus.Value = rbtnDecision.SelectedValue;

            SqlParameter sp_Comments = sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 2000);
            sp_Comments.Value = txtComments.Text;

            SqlParameter sp_Refills = sqlCmd.Parameters.Add("@RxRefills", SqlDbType.Int);
            sp_Refills.Value = Int32.Parse(ddlRefills.SelectedValue);

            SqlParameter sp_Phrm = sqlCmd.Parameters.Add("@Rx_Phrm", SqlDbType.VarChar, 50);

            if (rbtnPhrm.SelectedValue == "0")
            {
                sp_Phrm.Value = "ADiO Pharmacy";
            }
            else
            {
                sp_Phrm.Value = txtPharmacy.Text;
            }

            SqlParameter sp_User = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_User.Value = (string)Session["User"];

            SqlParameter sp_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
            sp_DocID.Value = Int32.Parse(hidDocID.Value);

            byte[] rxDoc = null;
            if (FileUpRxDoc.PostedFile != null && FileUpRxDoc.PostedFile.FileName != "")
            {
                rxDoc = new byte[FileUpRxDoc.PostedFile.ContentLength];
                HttpPostedFile Image = FileUpRxDoc.PostedFile;
                Image.InputStream.Read(rxDoc, 0, (int)FileUpRxDoc.PostedFile.ContentLength);
            }

            SqlParameter Rx_Doc = sqlCmd.Parameters.Add("@Rx_Doc", SqlDbType.Image);
            if (rxDoc != null)
                Rx_Doc.Value = rxDoc;
            else
                Rx_Doc.Value = Convert.DBNull;
            //Added for Rx Document Attachment 

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            //Response.Redirect("RxRequestQueue.aspx");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "window.close()", true);
        }

        catch (Exception ex)
        {
            lblMsg.Text = "Error occured while adding due to - " + ex.Message;
            lblMsg.Visible = true;
            objNLog.Error("Error : " + ex.Message);
        }
    }
}