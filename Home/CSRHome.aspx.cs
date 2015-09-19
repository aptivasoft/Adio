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

public partial class CSRHome : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");


            if (!Page.IsPostBack)
            {
                Filldata();
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
        try
        {
            FillAnnouncements();
            FillAppointments();
            FillEvents();
            FillTodaysActions();
            FillTodaysActivities();
            FillMedIssues();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }



    protected void gridEvents_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void gridEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            if (e.Row.RowState == DataControlRowState.Alternate)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='FFFFCC';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#eaf0f4';");
            }
            else
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='FFFFCC';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#d6e3ec';");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                if (mpContentPlaceHolder != null)
                {
                    DataSet dsEvts = new DataSet();
                    SqlConnection sqlCon = new SqlConnection(conStr);
                    SqlCommand sqlCmd = new SqlCommand("sp_getEventsByEvtID", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlEventID = sqlCmd.Parameters.Add("@Event_ID", SqlDbType.Int);
                    Label lb;
                    lb = (Label)e.Row.Cells[0].FindControl("lblEventID");
                    sqlEventID.Value = Int32.Parse(lb.Text);

                    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

                    sqlDa.Fill(dsEvts, "Events");
                    HtmlContainerControl divEvt = (HtmlContainerControl)mpContentPlaceHolder.FindControl("divEvt");
                    if (divEvt != null && dsEvts != null && dsEvts.Tables.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Date: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][0].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Description: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][1].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Duration: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][2].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Posted By: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][3].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Posted Date: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][4].ToString() + "");
                        e.Row.ToolTip = sb.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridActions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridActions.PageIndex = e.NewPageIndex;
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridEvents.PageIndex = e.NewPageIndex;
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridAppointments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridAppointments.PageIndex = e.NewPageIndex;
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridMessages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridMessages.PageIndex = e.NewPageIndex;
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void rbtnActions_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            FillTodaysActions();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    public String display_link(string ApprovedName,string doctorName)
    {
        string retName=string.Empty;;
        objNLog.Info("Function Started...");
        try
        {
            if (ApprovedName != "")
            {
                retName = doctorName + " ( " + ApprovedName + " )";
            }
            else
            {
                retName = doctorName;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
        return retName;
    }

    private void FillAppointments()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getAppointments", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = (string)Session["User"];

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsAppointment = new DataSet();

        sqlDa.Fill(dsAppointment);
        gridAppointments.DataSource = dsAppointment;
        gridAppointments.DataBind();
    }

    private void FillEvents()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();

        sqlCmd.CommandText = "sp_getEvents";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;
        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = (string)Session["User"];
        SqlParameter sp_Role = sqlCmd.Parameters.Add("@Role", SqlDbType.Char, 1);
        sp_Role.Value = char.Parse((string)Session["Role"]);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsEvents = new DataSet();
        sqlDa.Fill(dsEvents);
        gridEvents.DataSource = dsEvents;
        gridEvents.DataBind();
    }

    private void FillAnnouncements()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "sp_getAnnouncements";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;
        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = (string)Session["User"];
        SqlParameter sp_Role = sqlCmd.Parameters.Add("@Role", SqlDbType.Char, 1);
        sp_Role.Value = char.Parse((string)Session["Role"]);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsAnnouncements= new DataSet();
        sqlDa.Fill(dsAnnouncements);
        gridMessages.DataSource = dsAnnouncements;
        gridMessages.DataBind();
    }

    private void FillTodaysActions()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "sp_getTodaysDocActions";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;
        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = (string)Session["User"];
        SqlParameter sp_Option = sqlCmd.Parameters.Add("@Option", SqlDbType.Char, 1);
        sp_Option.Value = rbtnActions.SelectedValue.ToString();
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsActions = new DataSet();
        sqlDa.Fill(dsActions);
        gridActions.DataSource = dsActions;
        gridActions.DataBind();
    }

    private void FillTodaysActivities()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText="sp_getTodayActivities";
        sqlCmd.Connection = sqlCon;
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp_UserID1 = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID1.Value = (string)Session["User"];
        SqlParameter sp_LocName = sqlCmd.Parameters.Add("@LocName", SqlDbType.VarChar, 50);
        sp_LocName.Direction = ParameterDirection.Output;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsActivities = new DataSet();
        sqlDa.Fill(dsActivities);
        lblHeading2.Text = "Today’s Activity for " + sp_LocName.Value;
        if (dsActivities.Tables[0].Rows.Count > 0)
        {
            lblNONP1.Text = dsActivities.Tables[0].Rows[0][0].ToString();
            lblNOP1.Text = dsActivities.Tables[0].Rows[0][1].ToString();
            lblNOS1.Text = dsActivities.Tables[0].Rows[0][2].ToString();
            lblNOMR1.Text = dsActivities.Tables[0].Rows[0][3].ToString();
            lblNOPC1.Text = dsActivities.Tables[0].Rows[0][4].ToString();
        }
    }

    protected void gridMessages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
             
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                if (mpContentPlaceHolder != null)
                {
                     
                    Label lbDate;
                    lbDate = (Label)e.Row.Cells[1].FindControl("lblAnnDate");
                    ((Label)e.Row.Cells[1].FindControl("lblAnnDate")).Text = DateTime.Parse(lbDate.Text).ToString("MM/dd/yyyy");
                    
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridActions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((DataBinder.Eval(e.Row.DataItem, "flag")).ToString() == "1") //Med Requests
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#990033");
                    e.Row.ToolTip = "Med Request";
                }
                else if ((DataBinder.Eval(e.Row.DataItem, "flag")).ToString() == "2")
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#336600");
                    e.Row.ToolTip = "Med Issue";
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnPMI_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            LinkButton lbtn = (LinkButton)sender;
            gridRxMedIssue.PageIndex = int.Parse(lbtn.CommandArgument.ToString());
            FillMedIssues();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void gridRxMedIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    private void FillMedIssues()
    {
        objNLog.Info("Function Started...");
        int pagecount = 0;
        int pageSize = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getMedIssues_CSR", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = (string)Session["User"];



        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsMedIssues = new DataSet();
        try
        {
            sqlDa.Fill(dsMedIssues);

            gridRxMedIssue.DataSource = dsMedIssues;
            gridRxMedIssue.DataBind();

           Session["CallLog_CSR"] = dsMedIssues;
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

            pageSize = gridRxMedIssue.PageSize;
            pagecount = dsMedIssues.Tables[0].Rows.Count / pageSize;

            if (pagecount * pageSize < dsMedIssues.Tables[0].Rows.Count)
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

            if (dsMedIssues.Tables[0].Rows.Count <= pageSize)
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
        }
        catch (Exception ex)
        {
            objNLog.Error("Error: " + ex.Message);
        }
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
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void btnMedIssueSave_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);

        string user = (string)Session["User"];

        //string query = "Update  [Call_Log] SET Issue_Response='" + txtMedIssueComment.Text
        //            + "',Issue_ResponseFlag='Y',Issue_ResponseBy='" + (string)Session["User"]
        //            + "',Issue_ResponseDate=getdate() where [Call_ID] ='" + hfCallID.Value + "'";

        SqlCommand sqlCmd = new SqlCommand("sp_Update_CallLog_CSR", sqlCon);
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

            objUALog.LogUserActivity(conStr, user, "Updated Call Log while Processing Med Issue. [Call_ID] =" + hfCallID.Value, "Call_Log", 0);

            FillMedIssues();
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
        DataSet ds = (DataSet)Session["CallLog_CSR"];
        StringBuilder sb = new StringBuilder();
        DataTable dt = ds.Tables[0];

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
