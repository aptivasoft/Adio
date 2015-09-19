using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using NLog;
using Adio.UALog;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Net;
using System.Net.Mail;

public partial class Masters_UserActivityLog : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private static UserActivityLog objUALog = new UserActivityLog();
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    SmtpSettings objSmtp = new SmtpSettings();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (rbtnUAToday.Checked == true) 
        { 
            pnlToday.Visible = true;
            BindUALogByToday(); 
        }
        RenderJSArrayWithCliendIds(txtDate1,txtDate2,txtDate,txtUserID);
    }

    protected void grdUserActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUserActivity.PageIndex = e.NewPageIndex;
        if(rbtnUAToday.Checked == true) BindUALogByToday();
        if(rbtnUAUserID.Checked==true) BindUALogByUserID();
        if(rbtnUADate.Checked == true) BindUALogByDate();
        if(rbtnUADateRange.Checked == true) BindUALogByDateRange();
    }

    private void BindUALogByUserID()
    {
        if (txtUserID.Text != "")
        {
            Session["UALog"] = null;
            Session["UALog"] = (DataSet)objUALog.GetUserActivityByUserID(conStr, txtUserID.Text);
            grdUserActivity.DataSource = (DataSet)objUALog.GetUserActivityByUserID(conStr, txtUserID.Text);
            grdUserActivity.DataBind();

            if (grdUserActivity.Rows.Count == 0)
            {
                grdUserActivity.EmptyDataText = "No Activity for the User " + txtUserID.Text;
                grdUserActivity.DataSourceID = string.Empty;
                grdUserActivity.DataBind();
            }
        }
    }


    private void BindUALogByDate()
    {
        if (txtDate.Text != "")
        {
            Session["UALog"] = null;
            Session["UALog"] = (DataSet)objUALog.GetUserActivityByDate(conStr, txtDate.Text);
            grdUserActivity.DataSource = (DataSet)objUALog.GetUserActivityByDate(conStr, txtDate.Text);
            grdUserActivity.DataBind();

            if (grdUserActivity.Rows.Count == 0)
            {
                grdUserActivity.EmptyDataText = "No Activity on " + txtDate.Text;
                grdUserActivity.DataSourceID = string.Empty;
                grdUserActivity.DataBind();
            }
        }
    }


    private void BindUALogByDateRange()
    {
        if (txtDate1.Text != "" && txtDate2.Text != "")
        {
                Session["UALog"] = null;
                Session["UALog"] = (DataSet)objUALog.GetUserActivityByDateRange(conStr, txtDate1.Text, txtDate2.Text);
                grdUserActivity.DataSource = (DataSet)objUALog.GetUserActivityByDateRange(conStr, txtDate1.Text, txtDate2.Text); 
                grdUserActivity.DataBind();

                if (grdUserActivity.Rows.Count == 0)
                {
                    grdUserActivity.EmptyDataText = "No Activity Between " + txtDate1.Text + " and " + txtDate2.Text;
                    grdUserActivity.DataSourceID = string.Empty;
                    grdUserActivity.DataBind();
                }
        } 
    }


    private void BindUALogByToday()
    {
            Session["UALog"] = null;
            Session["UALog"] = (DataSet)objUALog.GetUserActivityByToday(conStr);
            grdUserActivity.DataSource = (DataSet)objUALog.GetUserActivityByToday(conStr);
            grdUserActivity.DataBind();

            if (grdUserActivity.Rows.Count == 0)
            {
                grdUserActivity.EmptyDataText = "No Activity for Today...";
                grdUserActivity.DataSourceID = string.Empty;
                grdUserActivity.DataBind();
            }
    }


    protected void rbtnUAToday_CheckedChanged(object sender, EventArgs e)
    {
        BindUALogByToday();
        pnlToday.Visible = true;
        pnlUserID.Visible = false;
        pnlDate.Visible = false;
        pnlDateRange.Visible = false;
     
        ClearInputControls();
    }


    protected void rbtnUAUserID_CheckedChanged(object sender, EventArgs e)
    {

        pnlUserID.Visible = true;
        pnlDate.Visible = false;
        pnlDateRange.Visible = false;
        pnlToday.Visible = false;
        ClearInputControls();
    }


    protected void rbtnUADate_CheckedChanged(object sender, EventArgs e)
    {
        pnlDate.Visible = true;
        pnlUserID.Visible = false;
        pnlDateRange.Visible = false;
        pnlToday.Visible = false;
        ClearInputControls();
    }


    protected void rbtnUADateRange_CheckedChanged(object sender, EventArgs e)
    {
        pnlDateRange.Visible = true;
        pnlDate.Visible = false;
        pnlUserID.Visible = false;
        pnlToday.Visible = false;
        ClearInputControls();
    }


    protected void btnGetUALogUserID_Click(object sender, EventArgs e)
    {
        try
        {
            grdUserActivity.PageIndex = 0;
            BindUALogByUserID();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }


    protected void btnGetUALogByDate_Click(object sender, EventArgs e)
    {
        try
        {
            grdUserActivity.PageIndex = 0;
            BindUALogByDate();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }


    protected void btnGetUALogByDateRange_Click(object sender, EventArgs e)
    {
        try
        {
            grdUserActivity.PageIndex = 0;
            BindUALogByDateRange();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }


    private void ClearInputControls()
    {
        txtUserID.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtDate1.Text = string.Empty;
        txtDate2.Text = string.Empty;
        grdUserActivity.EmptyDataText = string.Empty;
        grdUserActivity.DataSourceID = string.Empty;
        grdUserActivity.DataBind();
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

    protected void grdUserActivity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int patID=0;
                string pID = ((Label)e.Row.Cells[6].FindControl("lblTempID")).Text;
                
                if(pID.Trim()!="") 
                    patID= Int32.Parse(pID);

                string row = ((Label)e.Row.Cells[3].FindControl("lblUActivity")).Text;

                if (patID > 0)
                {

                    ((Label)e.Row.Cells[3].FindControl("lblUActivity")).Text = @"<a href='../Patient/AllPatientProfile.aspx?patID=" + pID + "'>" + row + "</a>";
                }
                else
                {
                    ((Label)e.Row.Cells[3].FindControl("lblUActivity")).Text = row;
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
   

    private void ReportEmailUserActivity(string Message)
    {
        try
        {
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
                    objSmtpProp.MailFrom = dsSmtp.Tables[0].Rows[0][4].ToString();
                    //objSmtpProp.MailTo = dsSmtp.Tables[0].Rows[0][5].ToString();txtEmailID.Text
                    objSmtpProp.MailTo = txtEmailID.Text;
                    objSmtpProp.IsSSL = bool.Parse(dsSmtp.Tables[0].Rows[0][6].ToString());
                    objSmtpProp.IsHTML = bool.Parse(dsSmtp.Tables[0].Rows[0][7].ToString());
                    
                    char mailPriority = char.Parse(dsSmtp.Tables[0].Rows[0][8].ToString());

                    switch (mailPriority)
                    {
                        case 'L': { objSmtpProp.MailPriority = SMTPProperties.Priority.Low; break; }
                        case 'H': { objSmtpProp.MailPriority = SMTPProperties.Priority.High; break; }
                        case 'N': { objSmtpProp.MailPriority = SMTPProperties.Priority.Normal; break; }
                    }

                    if (rbtnUAUserID.Checked == true)
                        objSmtpProp.MailSubject = "User Activity Log By UserID: " + txtUserID.Text;

                    if (rbtnUADate.Checked == true)
                        objSmtpProp.MailSubject = "User Activity Log By Date: " + txtDate.Text;

                    if (rbtnUADateRange.Checked == true)
                        objSmtpProp.MailSubject = "User Activity Log By Date Range From: " + txtDate1.Text + " To: " + txtDate2.Text;

                    if (rbtnUAToday.Checked == true)
                        objSmtpProp.MailSubject = "Today's User Activity Log";

                    if (txtEmailMessage.Text != "")
                        objSmtpProp.MailMessage = txtEmailMessage.Text + "</br>" + Message;
                    else
                        objSmtpProp.MailMessage = Message;

                    MailResponse objMailResp = objEmail.SendEmail(objSmtpProp);
                    lblEmailActivityLogStatus.Text = objMailResp.StatusMessage + " To <i>" + objSmtpProp.MailTo + "</i>";
                    mpeEmailActivityLogStatus.Show();
                     
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnEmailActivityLog_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["UALog"] != null)
            {
                DataSet ds = (DataSet)Session["UALog"];
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

                ReportEmailUserActivity(sb.ToString());
            }
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message); 
        }

    }
    protected void imgEmail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataSet dsSmtp = objSmtp.GetSmtpSettings();
            txtEmailID.Text = dsSmtp.Tables[0].Rows[0][5].ToString();
           
            mpeEmailActivityLog.Show();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
}
