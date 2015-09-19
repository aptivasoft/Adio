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
using Adio.UALog;

public partial class NurseHome : System.Web.UI.Page
{
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
    string conStr = ConfigurationManager.AppSettings["conStr"];

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Doc_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Doc_List.Clear();
        DataTable Doc_Names = objPat_Info.get_AllDoctors(prefixText);
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            if (!Page.IsPostBack)
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("sp_getClinics", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
                sp_UserID.Value = (string)Session["User"];

                SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
                sp_UserRole.Value = (string)Session["Role"];

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet dsClinicList = new DataSet();
                sqlDa.Fill(dsClinicList, "ClinicList");
                ddlFilterClinic.DataTextField = "Clinic_Name";
                ddlFilterClinic.DataValueField = "Clinic_ID";
                ddlFilterClinic.DataSource = dsClinicList;
                ddlFilterClinic.DataBind();
                if (dsClinicList.Tables[0].Rows.Count < 2)
                {
                    ddlFilterClinic.Items.RemoveAt(0);
                }
                else
                {
                    ddlFilterFacility.Items.Insert(0, new ListItem("All Locations", "0"));
                    ddlFilterFacility.SelectedIndex = 0;
                }
                ddlFilterClinic.SelectedIndex = 0;
                BindLocation(ddlFilterClinic.SelectedValue);
                setcontextkey();
                gridRxMedRequest.PageIndex = 0;
                gridRxMedIssue.PageIndex = 0;
                Filldata();
                string dd = hidDocID.ClientID;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
   
    protected void Filldata()
    {
        objNLog.Info("Function Started...");

        int pagecount = 0;
        int pageSize = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_getRxMedRequest_Nurse", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar,20);
        sp_UserID.Value =(string) Session["User"];

        if (rbtnFilterByDate.Checked == true)
        {
            SqlParameter par_RxReqStartDate = sqlCmd.Parameters.Add("@RxReqStartDate", SqlDbType.DateTime);
            par_RxReqStartDate.Value = Convert.ToDateTime(txtStartDate.Text);

            SqlParameter par_RxReqEndDate = sqlCmd.Parameters.Add("@RxReqEndDate", SqlDbType.DateTime);
            par_RxReqEndDate.Value = Convert.ToDateTime(txtEndDate.Text);

        }
        if (rbtnFilterByFac.Checked == true)
        {
            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
            par_ClinicID.Value = int.Parse(ddlFilterClinic.SelectedValue);

            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@FacilityID", SqlDbType.Int);
            par_FacilityID.Value = int.Parse(ddlFilterFacility.SelectedValue);
        }
        if (rbtnFilterByDoc.Checked == true)
        {
            SqlParameter par_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
            par_DocID.Value = int.Parse(hidDocID.Value);
        }

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsRxQueue = new DataSet();
        try
        {
            sqlDa.Fill(dsRxQueue);
           
            gridRxMedRequest.DataSource = dsRxQueue.Tables[0];
            gridRxMedRequest.DataBind();
           
            gridRxMedIssue.DataSource = dsRxQueue.Tables[1];
            gridRxMedIssue.DataBind();

            Session["CallLog_Nurse"] = dsRxQueue.Tables[1];
            if (gridRxMedIssue.Rows.Count > 0)
            {
                imgBtnSendCallLog.Enabled = true;
                imgBtnPrintCallLog.Enabled = true;
            }
            else
            {
                imgBtnSendCallLog.Enabled = false;
                imgBtnPrintCallLog.Enabled = false;
            }
           
            //Paging Med Requests
            pageSize = gridRxMedRequest.PageSize;
            pagecount = dsRxQueue.Tables[0].Rows.Count/pageSize ;

            if (pagecount * pageSize < dsRxQueue.Tables[0].Rows.Count)
            {
                pagecount = pagecount + 1;
            }
            if (gridRxMedRequest.PageIndex > 0)
            {
                btnPMR.Enabled  = true;
                btnPMR.CommandArgument = (gridRxMedRequest.PageIndex - 1).ToString();
            }
            else
            {
                btnPMR.Enabled = false;
            }
            if (gridRxMedRequest.PageIndex  == pagecount - 1 || pagecount==0)
            {
                btnNMR.Enabled = false;
            }
            else
            {
                btnNMR.CommandArgument = (gridRxMedRequest.PageIndex + 1).ToString();
                btnNMR.Enabled = true;
            }

            if (dsRxQueue.Tables[0].Rows.Count <= pageSize)
            {
                btnNMR.Visible = false;
                btnPMR.Visible = false;
                btnALLMR.Visible = false;
            }
            else
            {
                btnNMR.Visible = true ;
                btnPMR.Visible = true;
                btnALLMR.Visible = true;
            }

            //Paging Med Issues
            pageSize = gridRxMedIssue.PageSize;
            pagecount = dsRxQueue.Tables[1].Rows.Count / pageSize;

            if (pagecount * pageSize < dsRxQueue.Tables[1].Rows.Count)
            {
                pagecount = pagecount + 1;
            }

            if (gridRxMedIssue.PageIndex > 0)
            {
                btnPMI.Enabled = true;
                btnPMI.CommandArgument = (gridRxMedIssue.PageIndex - 1).ToString();
            }
            else
            {
                btnPMI.Enabled = false;
            }
            if (gridRxMedIssue.PageIndex == pagecount - 1 || pagecount == 0)
            {
                btnNMI.Enabled = false;
            }
            else
            {
                btnNMI.CommandArgument = (gridRxMedIssue.PageIndex + 1).ToString();
                btnNMI.Enabled = true;
            }

            if (dsRxQueue.Tables[1].Rows.Count <= pageSize)
            {
                btnNMI.Visible = false;
                btnPMI.Visible = false;
                btnAllMI.Visible = false;
            }
            else
            {
                btnNMI.Visible = true;
                btnPMI.Visible = true;
                btnAllMI.Visible = true;
            }

            FillTodaysActivities();
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void btnRxApprovalSave_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
            SqlConnection sqlCon = new SqlConnection(conStr);
            try
            {
                SqlCommand sqlCmd = new SqlCommand("sp_Update_MedRequest", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sp_Qty = sqlCmd.Parameters.Add("@Quantity", SqlDbType.Int);
                SqlParameter sp_Refills = sqlCmd.Parameters.Add("@Refills", SqlDbType.Int);
                SqlParameter sp_ReqID = sqlCmd.Parameters.Add("@Rx_Req_ID", SqlDbType.Int);
                sp_ReqID.Value = hfID.Value;
                SqlParameter sp_SIG = sqlCmd.Parameters.Add("@SIG", SqlDbType.VarChar, 50);
                SqlParameter sp_RxStatus = sqlCmd.Parameters.Add("@RX_Status", SqlDbType.Char, 1);
                sp_RxStatus.Value = hfStatus.Value;
                SqlParameter sp_Comments = sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 255);
                sp_Comments.Value = txtNote.Text;
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
                sp_DocID.Value = Int32.Parse(hidDocID.Value.ToString());
                //if (hfStatus.Value == "A")
                //{
                    sp_Qty.Value = Int32.Parse(txtQuantity.Text);
                    sp_Refills.Value = Int32.Parse(ddlRefills.SelectedValue);
                    sp_SIG.Value = txtSIG.Text;
                //}
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();

                Filldata();
            }
            catch (Exception ex)
            {
                objNLog.Error("Error : " + ex.Message);
            }
            finally 
            { 
                sqlCon.Close(); 
            }
            objNLog.Info("Event Completed...");
    }

    protected void btnMedIssueSave_Click(object sender, EventArgs e)
    {
        
    }


    #region  Grid Events
    

    protected void gridRxMedRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Label lblMed;
            Label lblDocName;
            Label lblDocID;
            if (e.CommandName == "Approve")
            {

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);

                lblMed = (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblMed");
                string[] s = new string[3];
                s = lblMed.Text.Split(';');
                lblMedicationName.Text = s[0].Trim();

                txtSIG.Text = s[3].Trim();

                txtQuantity.Text = s[1].Trim();
                ddlRefills.SelectedIndex = Convert.ToInt32(s[2].Trim());
                txtPharmacy.Text = s[4];

                if (txtPharmacy.Text.Trim() == "ADiO Pharmacy")
                {
                    rbtnPhrm.SelectedValue = "0";
                    //txtPharmacy.Visible = false;
                }
                else
                {
                    rbtnPhrm.SelectedValue = "1";
                    txtPharmacy.Visible = true;
                }

                txtNote.Text = "";
                hfID.Value = (string)e.CommandArgument;
                hfStatus.Value = "A";

                txtQuantity.Enabled = true;
                txtSIG.Enabled = true;

                lblDocName = (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblDoc_Name");
                txtDocName.Text = lblDocName.Text;

                lblDocID = (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblDoc_ID");
                hidDocID.Value = lblDocID.Text;
                popRxApproval.Show();

            }
            if (e.CommandName == "Denied")
            {

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);

                lblMed = (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblMed");
                string[] s = lblMed.Text.Split(';');
                lblMedicationName.Text = s[0];

                txtSIG.Text = s[2];

                txtQuantity.Text = s[1];
                txtNote.Text = "";
                hfID.Value = (string)e.CommandArgument;

                lblDocName = (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblDoc_Name");
                txtDocName.Text = lblDocName.Text;

               
                lblDocID= (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblDoc_ID");
                hidDocID.Value = lblDocID.Text;
                
                hfStatus.Value = "R";

                txtQuantity.Enabled = false;
                txtSIG.Enabled = false;
                popRxApproval.Show();
            }

            if (e.CommandName == "Paging")
            {

                gridRxMedRequest.PageIndex = int.Parse(e.CommandArgument.ToString());// e.NewPageIndex;
                Filldata();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error :" + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridRxMedIssue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Label lbl;
            if (e.CommandName == "Action")
            {

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);

                lbl = (Label)gridRxMedIssue.Rows[intRowIndex].FindControl("lblDesc");

                lblMedIssue1.Text = lbl.Text;
                hfCallID.Value = (string)e.CommandArgument;

            }
            popMedIssue.Show();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error :" + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridRxMedRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
    }
    protected void gridRxMedIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    #endregion
    protected void btnPMR_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            LinkButton lbtn = (LinkButton)sender;
            gridRxMedRequest.PageIndex = int.Parse(lbtn.CommandArgument.ToString());
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void btnPMI_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            LinkButton lbtn = (LinkButton)sender;
            gridRxMedIssue.PageIndex = int.Parse(lbtn.CommandArgument.ToString());
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] SearchDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Doc_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Doc_List.Clear();
        DataTable Doc_Names = Pat_Info.get_DoctorNames(prefixText, contextKey);
        foreach (DataRow dr in Doc_Names.Rows)
        {
            Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString(), dr[2].ToString()));
        }
        return Doc_List.ToArray();
    }

    protected void setcontextkey()
    {
        objNLog.Info("Function Started...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd;

            if ((string)Session["Role"] == "N" || (string)Session["Role"] == "D")
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
                    autoSrchDoc.ContextKey = dsClinicList.Tables[0].Rows[0]["Clinic_ID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    private void FillTodaysActivities()
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlConnection sqlCon = new SqlConnection(conStr);
        sqlCmd.CommandText="sp_getTodayActivities";
        sqlCmd.Connection=sqlCon;
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp_UserID1 = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID1.Value = (string)Session["User"];
        SqlParameter sp_LocName = sqlCmd.Parameters.Add("@LocName", SqlDbType.VarChar, 50);
        sp_LocName.Direction = ParameterDirection.Output;
        SqlDataAdapter  sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsActivities = new DataSet();
        sqlDa.Fill(dsActivities);
        lblHeading2.Text = "Today’s Activity for " + sp_LocName.Value;

        if (dsActivities.Tables[0].Rows.Count > 0)
        {
            lblNOP1.Text = dsActivities.Tables[0].Rows[0][0].ToString();
            lblNOR1.Text = dsActivities.Tables[0].Rows[0][1].ToString();
            lblNOMR1.Text = dsActivities.Tables[0].Rows[0][2].ToString();
            lblNOMI1.Text = dsActivities.Tables[0].Rows[0][3].ToString();
        }
    }

    protected void btnMedIssueSave_Click1(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        
        string user = (string)Session["User"];
        //string query = "Update  [Call_Log] SET Issue_Response='" + txtMedIssueComment.Text
        //               + "',Issue_ResponseFlag='Y',Issue_ResponseBy='" + (string)Session["User"]
        //               + "',Issue_ResponseDate=getdate() where [Call_ID] ='" + hfCallID.Value + "'";

        SqlCommand sqlCmd = new SqlCommand("sp_Update_CallLog_Nurse", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter parm_user = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        parm_user.Value = user;

        SqlParameter parm_note = sqlCmd.Parameters.Add("@Note", SqlDbType.VarChar, 255);
        parm_note.Value = txtMedIssueComment.Text;

        SqlParameter parm_callid = sqlCmd.Parameters.Add("@CallID", SqlDbType.Int);
        if (hfCallID.Value != "")
            parm_callid.Value = int.Parse(hfCallID.Value);
        else
            parm_callid.Value = Convert.DBNull;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objUALog.LogUserActivity(conStr, user, "Updated  Call Log while processing Med Issue. [Call_ID] =" + hfCallID.Value, "Call_Log", 0);
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }

        objNLog.Info("Event Completed...");
    }

    protected void rbtnPhrm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnPhrm.SelectedValue == "1")
        {
            txtPharmacy.Visible = true;
        }
       // else
       // {
       //     txtPharmacy.Visible = false;
       // }

    }

    protected void rbtnFilterByDate_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnFilterByDate.Checked == true)
        {
            pnlDateSelect.Visible = true;
            pnlFacilitySelect.Visible = false;
            pnlDoctorSelect.Visible = false;
        }
    }
    protected void rbtnFilterByFac_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnFilterByFac.Checked == true)
        {
            pnlDateSelect.Visible = false;
            pnlFacilitySelect.Visible = true;
            pnlDoctorSelect.Visible = false;
        }
    }
    protected void rbtnFilterByDoc_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnFilterByDoc.Checked == true)
        {
            pnlDateSelect.Visible = false;
            pnlFacilitySelect.Visible = false;
            pnlDoctorSelect.Visible = true;
        }
    }
    protected void ddlFilterClinic_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            ddlFilterClinic.Items.Insert(0, new ListItem("All Organizations", "0"));
            ddlFilterClinic.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");

    }
    protected void ddlFilterClinic_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            BindLocation(ddlFilterClinic.SelectedValue);

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void ddlFilterFacility_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlFilterFacility_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            ddlFilterFacility.Items.Insert(0, new ListItem("All Locations", "0"));
            ddlFilterFacility.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");

    }
    protected void BindLocation(string clinicID)
    {
        objNLog.Info("Function Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getFacilities", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp_ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
        sp_ClinicID.Value = clinicID;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsFacilityList = new DataSet();
        try
        {
            sqlDa.Fill(dsFacilityList, "FacilityList");
            ddlFilterFacility.DataTextField = "Facility_Name";
            ddlFilterFacility.DataValueField = "Facility_ID";
            ddlFilterFacility.DataSource = dsFacilityList;
            ddlFilterFacility.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void btnQueueList_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
     protected void imgBtnSendCallLog_Click(object sender, ImageClickEventArgs e)
     {
         txtEmailFrom.Text = GetUserEmailID((string)Session["User"]);
         popEmailCallLog.Show();
     }

     private string GetUserEmailID(string userID)
     {
         string userEmailID = "";
         SqlConnection sqlCon = new SqlConnection(conStr);

         SqlCommand sqlCmd = new SqlCommand("sp_getUsersEmailID", sqlCon);
         sqlCmd.CommandType = CommandType.StoredProcedure;

         SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
         par_UserID.Value = (string)Session["User"];

         SqlParameter par_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
         par_UserRole.Value = (string)Session["Role"];



         try
         {
             sqlCon.Open();
             userEmailID = sqlCmd.ExecuteScalar().ToString();
             sqlCon.Close();

         }
         catch (Exception ex)
         {
             objNLog.Error("Error : " + ex.Message);
         }
         return userEmailID;
     }

     private void SendEmailMessage(string Message)
     {
         try
         {
             SmtpSettings objSmtp = new SmtpSettings();
             DataSet dsSmtp = objSmtp.GetSmtpSettings();
             if (dsSmtp.Tables.Count > 0)
             {
                 if (dsSmtp.Tables[0].Rows.Count > 0)
                 {
                     SMTPProperties objSmtpProp = new SMTPProperties();
                     SMTPSendEMail objEmail = new SMTPSendEMail();

                     objSmtpProp.Server = dsSmtp.Tables[0].Rows[0][0].ToString();
                     objSmtpProp.Port = int.Parse(dsSmtp.Tables[0].Rows[0][1].ToString());
                     objSmtpProp.UserName = dsSmtp.Tables[0].Rows[0][2].ToString();
                     objSmtpProp.PassWord = dsSmtp.Tables[0].Rows[0][3].ToString();

                     if (txtEmailFrom.Text != "")
                         objSmtpProp.MailFrom = txtEmailFrom.Text;
                     else
                         objSmtpProp.MailFrom = dsSmtp.Tables[0].Rows[0][4].ToString();

                     objSmtpProp.MailTo = txtEmailTo.Text;
                     objSmtpProp.IsSSL = bool.Parse(dsSmtp.Tables[0].Rows[0][6].ToString());
                     objSmtpProp.IsHTML = bool.Parse(dsSmtp.Tables[0].Rows[0][7].ToString());

                     char mailPriority = char.Parse(dsSmtp.Tables[0].Rows[0][8].ToString());
                     switch (mailPriority)
                     {
                         case 'L': { objSmtpProp.MailPriority = SMTPProperties.Priority.Low; break; }
                         case 'H': { objSmtpProp.MailPriority = SMTPProperties.Priority.High; break; }
                         case 'N': { objSmtpProp.MailPriority = SMTPProperties.Priority.Normal; break; }
                     }


                     if (txtEmailSubject.Text != "")
                         objSmtpProp.MailSubject = txtEmailSubject.Text;
                     else
                         objSmtpProp.MailSubject = "Call Log";

                     if (txtEmailMessage.Text != "")
                         objSmtpProp.MailMessage = txtEmailMessage.Text + "<br/>" + Message;
                     else
                         objSmtpProp.MailMessage = Message;

                     MailResponse objMailResp = objEmail.SendEmail(objSmtpProp);
                     string str = "alert('" + objMailResp.StatusMessage + "');";
                     ScriptManager.RegisterStartupScript(imgBtnSendEmail, typeof(Page), "alert", str, true);
                 }
             }
         }
         catch (Exception ex)
         {
             objNLog.Error("Error : " + ex.Message);
         }
     }
     protected void imgBtnSendEmail_Click(object sender, ImageClickEventArgs e)
     {
         try
         {
             SendEmailMessage(GetCallLogFormat());
         }
         catch (Exception ex)
         {
             objNLog.Error("Error : " + ex.Message);
         }
     }
     protected void imgBtnPrintCallLog_Click(object sender, ImageClickEventArgs e)
     {
         objNLog.Info("Event Started...");
         try
         {
             string strPrintCallLog = "printWin = window.open('', 'CallLogPrint', 'scrollbars=1,menubar=1,resizable=1');"

                         + "if (printWin != null) "
                         + "{"
                         + "printWin.document.open();"
                         + @"printWin.document.write(""" + GetCallLogFormat() + @""");"
                         + "printWin.document.close();"
                         + "printWin.print();"
                         + "}";
             ScriptManager.RegisterStartupScript(imgBtnPrintCallLog, typeof(Page), "alert", strPrintCallLog, true);

         }
         catch (Exception ex)
         {
             objNLog.Info("Error: " + ex.Message);
         }
         objNLog.Info("Event Completed...");
     }
     private string GetCallLogFormat()
     {
         DataTable dt= (DataTable)Session["CallLog_Nurse"];
         StringBuilder sb = new StringBuilder();
        

         sb.Append("<table style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;padding:3;'>");
         sb.Append("<tr>");

         for (int colIndx = 0; colIndx < dt.Columns.Count - 1; colIndx++)
         {
             sb.Append("<td style='border: solid 1px Silver; font-size:12px; font-family:Arial;border-collapse:collapse;font-weight:bold; color:#FFFFFF;padding:3;background:#4f81bc;vertical-align:middle;text-align:center;'>");
             sb.Append(dt.Columns[colIndx].ColumnName);
             sb.Append("</td>");
         }

         sb.Append("</tr>");
         for (int rowIndx = 0; rowIndx < dt.Rows.Count; rowIndx++)
         {
             sb.Append("<tr>");
             for (int colIndx = 0; colIndx < dt.Columns.Count - 1; colIndx++)
             {
                 if (rowIndx % 2 == 0)
                 {
                     if (colIndx == 0)
                         sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:right;background:#d6e3ec;'>");
                     else
                         sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:left;background:#d6e3ec;'>");
                 }
                 else
                 {
                     if (colIndx == 0)
                         sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:right;background:#eaf0f4;'>");
                     else
                         sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:left;background:#eaf0f4;'>");

                 }

                 sb.Append(dt.Rows[rowIndx][colIndx].ToString());
                 sb.Append("</td>");
             }
             sb.Append("</tr>");

         }
         sb.Append("</table>");
         return sb.ToString();
     }
}


