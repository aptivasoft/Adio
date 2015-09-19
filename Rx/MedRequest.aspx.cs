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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NLog;

public partial class MedRequest : System.Web.UI.Page
{
    PatinetMedHistoryInfo Pat_MedInfo = new PatinetMedHistoryInfo();
    PatientPersonalDetails PID = new PatientPersonalDetails();
    PatientInfoDAL Pat_Info = new PatientInfoDAL();
    MedicationRequestDAL objMedInfo = new MedicationRequestDAL();
    Property objProp = new Property();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null) { Response.Redirect("../Login.aspx"); }

            AutocompletePatients.ContextKey = (string)Session["User"];
            if (!Page.IsPostBack)
            {
                setcontextkey();
                DataSet dsDeliveryModes = objMedInfo.GetDeliveryModes();
                rbtnDelivery.DataSource = dsDeliveryModes.Tables[0];
                rbtnDelivery.DataTextField = "Delivery_Mode_Description";
                rbtnDelivery.DataValueField = "Delivery_Mode";
                rbtnDelivery.DataBind();

                rbtnDelivery.SelectedIndex = 0;
                rbtnRequestType.DataSource = dsDeliveryModes.Tables[1];
                rbtnRequestType.DataTextField = "Rx_Type_Description";
                rbtnRequestType.DataValueField = "Rx_Type";
                rbtnRequestType.DataBind();

                rbtnRequestType.SelectedValue = "R";
                if (Request.QueryString["RxItemID"] != null)
                    Filldata(int.Parse(Request.QueryString["RxItemID"].ToString()));
           }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void Filldata(int rxItemID)
    {
        objNLog.Info("Function Started with rxItemId as parameter.");
        DataSet dsRxItem = objMedInfo.GetRxItem(rxItemID);
        try
        {
            if (dsRxItem.Tables[0].Rows.Count > 0)
            {
                txtPatname.Text = dsRxItem.Tables[0].Rows[0]["PatientName"].ToString();
                txtDocName.Text = dsRxItem.Tables[0].Rows[0]["doctorName"].ToString().Trim();
                txtMedicationName.Text = dsRxItem.Tables[0].Rows[0]["Rx_DrugName"].ToString().Trim();
                //objNLog.Info("txtMedication:"+txtMedicationName.Text);

                txtPatContact.Text = dsRxItem.Tables[0].Rows[0]["Pat_Phone"].ToString();
                txtQty.Text = dsRxItem.Tables[0].Rows[0]["Rx_Qty"].ToString();
                txtSIG.Text = dsRxItem.Tables[0].Rows[0]["Rx_SIG"].ToString();
                rbtnRequestType.SelectedValue = dsRxItem.Tables[0].Rows[0]["Rx_Type"].ToString();
                hidDocID.Value = dsRxItem.Tables[0].Rows[0]["Doc_ID"].ToString();
                hidPatID.Value = dsRxItem.Tables[0].Rows[0]["Pat_ID"].ToString();
                txtPatname.Enabled = false;
               if (dsRxItem.Tables[0].Rows[0]["Rx_Type"].ToString() == "S")
                {
                    rbtnRequestType.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed.");
    }

    protected void setcontextkey()
    {
        objNLog.Info("Function Started..");
        try
        {
            if ((string)Session["Role"] == "C" || (string)Session["Role"] == "D")
            {
                objProp.UserID = (string)Session["User"];
                objProp.UserRole = (string)Session["Role"];
                DataSet dsClinicList = objMedInfo.GetClinics(objProp);
                if (dsClinicList.Tables[0].Rows.Count > 0)
                {
                    AutoCompleteExtender2.ContextKey = dsClinicList.Tables[0].Rows[0]["Clinic_ID"].ToString();

                }
            }
            else if (Request.QueryString["RxItemID"] != null)
            {
                int rxItemId = int.Parse((string)Request.QueryString["RxItemID"]);
                string clinicID = objMedInfo.GetClinicID(rxItemId);
                AutoCompleteExtender2.ContextKey = clinicID;
            }
        }
        catch (Exception ex)
        {
            objNLog.Info("Error : " + ex.Message);
        }
        objNLog.Info("Function completed..");
    }

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
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Patient Found...","0"));
        }
        return Pat_List.ToArray();
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
            Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Doctor Found...","0"));
        }
        return Doc_List.ToArray();
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
        DataTable Med_Names = Pat_Info.get_MedicationNames(prefixText.Trim());
        if (Med_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Med_Names.Rows)
            {
                Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
            }
        }
        else
        {
            Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Medication Found..","0"));
        }
        return Med_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSIGNames(string prefixText, int count, string contextKey)
    {
        List<string> sig_List = new List<string>();
        PatientInfoDAL pat_Info = new PatientInfoDAL();
        if (contextKey == "0")
        {
            sig_List.Clear();
            DataTable dtsig = pat_Info.get_SIGCodes(prefixText, count, contextKey);
            if (dtsig.Rows.Count > 0)
            {
                foreach (DataRow dr in dtsig.Rows)
                {
                    sig_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["SIG_Code"].ToString().Trim() + "||" + dr["SIG_Name"].ToString().Trim(), dr[0].ToString()));
                }
            }
            else
            { 
                sig_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No SIG Found...","0")); 
            }
        }
        return sig_List.ToArray();
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Med Request Submit Click Event Started...");
        UpdatePanel1.Update();
        try
        {
            string reqType = "Facility";
            if (rbtnPatCalled.Checked) reqType = "Patient";
            objProp.PatID = hidPatID.Value.ToString();

            objNLog.Info("txtMedication:(save)" + txtMedicationName.Text);

            int DrugID = Pat_Info.Get_MedID(txtMedicationName.Text.Trim());

            if (DrugID > 0)  // If entered drug name doesn't exist
            {
                objProp.DrugName = txtMedicationName.Text.Trim();
                if (txtQty.Text == "")
                    txtQty.Text = "0";
                objProp.DrugQty = txtQty.Text;
                string[] sig;
                string sigDesc;
                if (txtSIG.Text != "" && txtSIG.Text.Contains("||"))
                {
                    sig = txtSIG.Text.Split(new string[] { "||" }, StringSplitOptions.None);
                    sigDesc = sig[1].TrimStart().ToString();
                }
                else
                    sigDesc = txtSIG.Text;

                objProp.DrugSIG = sigDesc;
                objProp.DocID = hidDocID.Value.ToString();
                objProp.RxType = char.Parse(rbtnRequestType.SelectedValue.ToString());
                objProp.DeliveryMode = char.Parse(rbtnDelivery.SelectedValue.ToString());
                objProp.RequestType = reqType;
                objProp.Comments = txtComments.Text;
                objProp.UserID = (string)Session["User"];
                objProp.Phone = txtPatContact.Text;
                if (rbtnPhrm.SelectedValue == "0")
                {
                    objProp.Pharmacy = "ADiO Pharmacy";
                }
                else
                {
                    objProp.Pharmacy = txtPharmacy.Text;
                }
                int flag = objMedInfo.Set_MedRequest(objProp);
                if (flag == 1)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Black;
                    lblMsg.Text = "Med Request Made Sucessfully...";
                    lblMsg.Visible = true;
                    if (Request.QueryString["RxItemID"] == null)
                    {
                        ClearControls();
                    }
                }
                if (Request.QueryString["RxItemID"] != null)
                {
                    Response.Redirect("~/Patient/AllPatientProfile.aspx?patID=" + hidPatID.Value.ToString());
                }

            }
            else
            {
                lblMsg.ForeColor =  System.Drawing.Color.Red ;
                lblMsg.Text = "Error!  Invalid Medication Name ...";
                lblMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error - Request failed due to the following error:" + ex.Message;
            lblMsg.Visible = true;
            objNLog.Error("Error: " + ex.Message);
        }
    
        objNLog.Info("Submit Click Event Completed...");
    }
    
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            ClearControls();
            if (Request.QueryString["RxItemID"] != null)
                Response.Redirect("~/Patient/AllPatientProfile.aspx?patID=" + hidPatID.Value.ToString());
            else
                Response.Redirect("~/Patient/AllPatientProfile.aspx");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    private void ClearControls()
    {
        objNLog.Info("Function Started...");
        try
        {
            txtPatname.Text = "";
            txtDocName.Text = "";
            txtMedicationName.Text = "";
            txtPatContact.Text = "";
            txtQty.Text = "";
            txtSIG.Text = "";
            txtComments.Text = "";
            rbtnPhrm.SelectedValue = "0";
            if (txtPharmacy.Visible == true)
            {
                txtPharmacy.Text = "";
                txtPharmacy.Visible = false;
            }
            hidDocID.Value = "";
            hidPatID.Value = "";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void rbtnPhrm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnPhrm.SelectedValue == "1")
        {
            txtPharmacy.Visible = true;
        }
        else
        {
            txtPharmacy.Visible = false;
        }

    }
}
