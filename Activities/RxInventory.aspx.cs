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
using Adio.UALog;
public partial class RxInventory : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    PatientInfoDAL Pat_Info = new PatientInfoDAL();
    private UserActivityLog objUALog = new UserActivityLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");
            
            AutocompletePatients.ContextKey = (string)Session["User"];

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        
    }

    // GetPatientNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetPatinetNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names = Pat_Info.get_PatientNames(prefixText, (string)HttpContext.Current.Session["Role"], contextKey);
        if (Pat_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Pat_Names.Rows)
            {
                Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString().Trim() + "," + dr[0].ToString().Trim() + " - " + dr[5].ToString().Trim(), dr[2].ToString().Trim() + "," + dr[3].ToString().Trim() + "," + dr[4].ToString().Trim()));
            }
        }
        else
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Patient Found...", "0"));
        }
        return Pat_List.ToArray();
    }
    //GetMedicationNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetMedicationNames(string prefixText, int count)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names = Pat_Info.get_MedicationNames(prefixText);
        foreach (DataRow dr in Pat_Names.Rows)
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
        }
        return Pat_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetFacilityNames(string prefixText, int count)
    {
        EmployeeInfoDAL emp_Info = new EmployeeInfoDAL();
        List<string> emp_List = new List<string>();
        emp_Info = new EmployeeInfoDAL();
        emp_List.Clear();
        DataTable _Names = emp_Info.get_LocationNames(prefixText);
        foreach (DataRow dr in _Names.Rows)
        {
            emp_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[2].ToString()));
        }
        return emp_List.ToArray();
    }

    //protected void Filldata()
    //{

    //    SqlConnection sqlCon = new SqlConnection(conStr);

    //    //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

    //    SqlCommand sqlCmd = new SqlCommand("sp_getRxRequestQueue", sqlCon);
    //    sqlCmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter par_RequestID = sqlCmd.Parameters.Add("@Rx_Req_ID", SqlDbType.Int);
    //    par_RequestID.Value = Request.QueryString["RxRequestID"].ToString();
      

    //    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
    //    DataSet dsRxRequest = new DataSet();
    //    try
    //    {
    //        sqlDa.Fill(dsRxRequest, "RxRequest");
    //        //gridRxQueueList.DataSource = dsRxQueueList;
    //        //gridRxQueueList.DataBind();
    //        txtDocName.Text = dsRxRequest.Tables[0].Rows[0]["doctorName"].ToString();
    //        hidDocID.Value = dsRxRequest.Tables[0].Rows[0]["Doc_ID"].ToString();
    //        lblpatientName.Text = "Patient Name: " + dsRxRequest.Tables[0].Rows[0]["PatientName"].ToString();
    //        txtDrug.Text = dsRxRequest.Tables[0].Rows[0]["Rx_DrugName"].ToString();
    //        txtQty.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Qty"].ToString();
    //        txtSIG.Text = dsRxRequest.Tables[0].Rows[0]["Rx_SIG"].ToString();
    //        txtComments.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Request_Comments"].ToString();
    //        rbtnDecision.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Status"].ToString();
    //        rbtnRequestType.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Type"].ToString();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        
        if (Session["User"] == null)
            Response.Redirect("../Login.aspx");
        int i,j;
        i=int.Parse(lblStockQty1.Text);
        j=int.Parse(txtQty.Text);

        if (rbtnTType.SelectedValue == "R" && j > i)
        {
            string str = "alert('Quantity removed can not be greater than available stock!');";
            //else
            // str = "alert('Select Drug');";
            ScriptManager.RegisterStartupScript(btngetStock, typeof(Page), "alert", str, true);
        }
        else
        {

            //string sqlQuery = "";
            string userID = (string)Session["User"];
            //if (rbtnTType.SelectedValue == "R")
            //{
            //    sqlQuery = "Insert INTO Drug_Inventory   ([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],[NDC],[ReasonforRemove],Facility_ID,Lot_Num,Expiry_Date,LastModified,LastModifiedBy,Clinic_ID)    VALUES ('"
            //       + rbtnIType.SelectedValue + "','" + rbtnTType.SelectedValue + "',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + txtDrug.Text + "'),'-" + txtQty.Text + "','" + txtComments.Text + "','Tablet','" + txtNDC.Text + "','" + ddlRReason.SelectedValue.ToString() + "','" + hidFacID.Value.ToString() + "','" + txtLotNum.Text + "','" + Convert.ToDateTime(txtExpiryDate.Text) + "',getdate(),'" + Session["User"] + "', (select Clinic_ID from Facility_Info where Facility_ID=" + hidFacID.Value.ToString() + "))";
            //}
            //else
            //{
            //    sqlQuery = "Insert INTO Drug_Inventory   ([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],[NDC],Facility_ID,Lot_Num,Expiry_Date,LastModified,LastModifiedBy,Clinic_ID)    VALUES ('"
            //       + rbtnIType.SelectedValue + "','" + rbtnTType.SelectedValue + "',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + txtDrug.Text + "'),'" + txtQty.Text + "','" + txtComments.Text + "','Tablet','" + txtNDC.Text + "','" + hidFacID.Value.ToString() + "','" + txtLotNum.Text + "','" + Convert.ToDateTime(txtExpiryDate.Text) + "',getdate(),'" + Session["User"] + "', (select Clinic_ID from Facility_Info where Facility_ID=" + hidFacID.Value.ToString() + "))";
            //}
            try
            {
                SqlCommand sqlCmd = new SqlCommand("sp_Set_Rx_Drug_Inventory", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                
                SqlParameter sp_Inv_Group_Code = sqlCmd.Parameters.Add("@Inv_Group_Code", SqlDbType.Char, 1);
                sp_Inv_Group_Code.Value = char.Parse(rbtnIType.SelectedValue);

                SqlParameter sp_Inv_Trans_Code = sqlCmd.Parameters.Add("@Inv_Trans_Code", SqlDbType.Char, 1);
                sp_Inv_Trans_Code.Value = char.Parse(rbtnTType.SelectedValue);

                SqlParameter sp_Drug_Name = sqlCmd.Parameters.Add("@Drug_Name", SqlDbType.VarChar, 50);
                sp_Drug_Name.Value = txtDrug.Text;

                SqlParameter sp_Qty = sqlCmd.Parameters.Add("@Qty", SqlDbType.Int);
                if(rbtnTType.SelectedValue == "R")
                    sp_Qty.Value = int.Parse("-" + txtQty.Text);
                else
                    sp_Qty.Value = int.Parse(txtQty.Text);

                SqlParameter sp_Inv_Desc = sqlCmd.Parameters.Add("@Inv_Desc", SqlDbType.VarChar, 255);
                sp_Inv_Desc.Value = txtComments.Text;

                SqlParameter sp_NDC = sqlCmd.Parameters.Add("@NDC", SqlDbType.VarChar, 50);
                sp_NDC.Value = txtNDC.Text;  
 
                SqlParameter sp_FacID = sqlCmd.Parameters.Add("@FacilityID", SqlDbType.Int);
                sp_FacID.Value = int.Parse(hidFacID.Value.ToString());

                SqlParameter sp_PatID = sqlCmd.Parameters.Add("@PatientID", SqlDbType.Int);
                if (hidPatID.Value.ToString() == "")
                    sp_PatID = null;
                else
                    sp_PatID.Value = int.Parse(hidPatID.Value.ToString());

                SqlParameter sp_Reason_Remove = sqlCmd.Parameters.Add("@Reason_Remove", SqlDbType.VarChar, 50);
                sp_Reason_Remove.Value = ddlRReason.SelectedValue.ToString();  
                                
                SqlParameter sp_Lot_Num = sqlCmd.Parameters.Add("@Lot_Num", SqlDbType.VarChar, 50);
                if (txtLotNum.Text != "")
                    sp_Lot_Num.Value = txtLotNum.Text;
                else
                    sp_Lot_Num.Value = System.DBNull.Value;

                SqlParameter sp_Expiry_Date = sqlCmd.Parameters.Add("@Expiry_Date", SqlDbType.DateTime);
                if ( txtExpiryDate.Text  != "")
                    sp_Expiry_Date.Value = Convert.ToDateTime(txtExpiryDate.Text);
                else
                    sp_Expiry_Date.Value = System.DBNull.Value;

                SqlParameter sp_LastModifiedBy = sqlCmd.Parameters.Add("@LastModifiedBy", SqlDbType.VarChar, 50);
                sp_LastModifiedBy.Value = userID;

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();

                objUALog.LogUserActivity(conStr, userID, rbtnTType.SelectedValue + " " + rbtnIType.SelectedValue + " " + txtDrug.Text.Trim() + " Qty:" + txtQty.Text, "Drug_Inventory", 0);
                sqlCon.Close();
                lblMsg.Text = "Saved Successfully ...";
                lblMsg.Visible = true;
                Response.Redirect("SampleLog.aspx");

            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error In Adding ...";
                lblMsg.Visible = true;

                objNLog.Error("Error : " + ex.Message);

            }
        }
       objNLog.Info("Event Completed...");
    }
  
    


    
    protected void btnUserCancel_Click(object sender, ImageClickEventArgs e)
    {
       // Response.Redirect("RxRequestQueue.aspx");
    }


    protected void hidDrugID_ValueChanged(object sender, EventArgs e)
    {
        try
        {
            getStockCount();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void getStockCount()
    {
        objNLog.Info("Function Started...");
        try
        {
            string drugID = hidDrugID.Value.ToString();
            string facID = hidFacID.Value.ToString();
            if (facID == "" || drugID == "")
            {
                string str = "";
                if (facID == "")
                {
                    str = "alert('Select Facility');";
                    //else
                    // str = "alert('Select Drug');";
                    ScriptManager.RegisterStartupScript(btngetStock, typeof(Page), "alert", str, true);
                }
            }
            else
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd;

                if (rbtnIType.SelectedValue == "")
                {
                    rbtnIType.SelectedValue = "S";
                }


                string sqlQuery = "Select sum(Qty) from Drug_Inventory where Drug_ID='" + drugID + "' and Clinic_ID=(Select Clinic_ID from Facility_Info WHERE Facility_ID='" + hidFacID.Value.ToString() + "') and Inv_Group_Code='" + rbtnIType.SelectedValue.ToString() + "'";




                sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                sqlCon.Open();

                string count = sqlCmd.ExecuteScalar().ToString();
                sqlCon.Close();
                if (count == "")
                {
                    lblStockQty1.Text = "0";
                    rbtnTType.Items[1].Enabled = false;
                    rbtnTType.Items[0].Selected = true;

                }

                else
                {
                    lblStockQty1.Text = count;
                    rbtnTType.Items[1].Enabled = true;
                    rbtnTType.Items[0].Selected = true;
                }

            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getStockCount();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void rbtnIType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getStockCount();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void rbtnTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnTType.SelectedValue == "R")
            {
                txtPatName.Visible = true;
                ddlRReason.Visible = true;
            }
            else
            {
                txtPatName.Visible = false;
                ddlRReason.Visible = false;
            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
}