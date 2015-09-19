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

public partial class DoctorHome : System.Web.UI.Page
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
            RenderJSArrayWithCliendIds(txtQuantity);
            if (!Page.IsPostBack)
            {
                gridRxMedRequest.PageIndex = 0;
                gridRxMedIssue.PageIndex = 0;
                Filldata();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
   
    protected void btnRxApprovalSave_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            string userID = (string)Session["User"];
            SqlCommand sqlCmd = new SqlCommand("sp_Update_MedRequest", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp_Qty = sqlCmd.Parameters.Add("@Quantity", SqlDbType.Int);

            SqlParameter sp_ReqID = sqlCmd.Parameters.Add("@Rx_Req_ID", SqlDbType.Int);
            sp_ReqID.Value = hfID.Value;
            SqlParameter sp_SIG = sqlCmd.Parameters.Add("@SIG", SqlDbType.VarChar, 50);

            SqlParameter sp_RxStatus = sqlCmd.Parameters.Add("@RX_Status", SqlDbType.Char, 1);
            sp_RxStatus.Value = hfStatus.Value;
            SqlParameter sp_Comments = sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 255);
            sp_Comments.Value = txtNote.Text;
            SqlParameter sp_User = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_User.Value = userID;
            SqlParameter sp_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
            if (hidDocID.Value != "")
                sp_DocID.Value = Int32.Parse(hidDocID.Value);
            else
                sp_DocID.Value = Convert.DBNull;

            //if (hfStatus.Value == "A")
            //{
                sp_Qty.Value = Int32.Parse(txtQuantity.Text);
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
        objNLog.Info("Event Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd;
        string user = (string)Session["User"];
        string query ="Update  [Call_Log] SET Issue_Response='" + txtMedIssueComment.Text 
                    + "',Issue_ResponseFlag='Y',Issue_ResponseBy='" + (string)Session["User"] 
                    + "',Issue_ResponseDate=getdate() where [Call_ID] ='" + hfCallID.Value + "'";
        sqlCmd = new SqlCommand(query, sqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            
            objUALog.LogUserActivity(conStr,user,"Updated  Call Log while Med Issue. [Call_ID] =" + hfCallID.Value  ,"Call_Log",0);

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

    protected void gridRxMedRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Label lblMed;
            Label lblDocID;
            if (e.CommandName == "Approve")
            {

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);

                lblMed = (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblMed");
                string[] s = new string[3];
                s = lblMed.Text.Split(';');
                lblMedicationName.Text = s[0];

                txtSIG.Text = s[2];
                txtQuantity.Text = s[1];
                txtNote.Text = "";
                
                hfID.Value = (string)e.CommandArgument;
                hfStatus.Value = "A";

                txtQuantity.Enabled = true;
                txtSIG.Enabled = true;
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

                hfStatus.Value = "R";

                lblDocID = (Label)gridRxMedRequest.Rows[intRowIndex].FindControl("lblDoc_ID");
                hidDocID.Value = lblDocID.Text;

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
            objNLog.Error("Error : " + ex.Message);
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
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridRxMedRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
    }
   
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

    protected void gridRxMedIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gridRxMedRequest_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    SqlCommand sqlCmd = new SqlCommand("sp_getRxMedReqByID", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlReqID = sqlCmd.Parameters.Add("@Req_ID", SqlDbType.Int);
                   
                    Label lb;
                    lb = (Label)e.Row.Cells[5].FindControl("lblReqID");
                    sqlReqID.Value = Int32.Parse(lb.Text);

                    SqlParameter sqlUser = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
                    sqlUser.Value = (string)Session["User"];
                    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

                    sqlDa.Fill(dsEvts, "RxMedReq");
                    // HtmlContainerControl divEvt = (HtmlContainerControl)mpContentPlaceHolder.FindControl("divEvt");
                    if (dsEvts != null && dsEvts.Tables.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Patient: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][1].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Rx Date: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][2].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Medication: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][3].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Qty: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][4].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("SIG: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][5].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Clinic Name: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][7].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Rx Request By: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][8].ToString());
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

    private void FillTodaysActivities()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd=new SqlCommand("sp_getTodayActivities", sqlCon);
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

    protected void Filldata()
    {
        objNLog.Info("Function Started...");
        int pagecount = 0;
        int pageSize = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getRxMedRequest", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = (string)Session["User"];


        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsRxQueue = new DataSet();
        try
        {
            sqlDa.Fill(dsRxQueue);
            gridRxMedRequest.DataSource = dsRxQueue.Tables[0];
            gridRxMedRequest.DataBind();

            gridRxMedIssue.DataSource = dsRxQueue.Tables[1];
            gridRxMedIssue.DataBind();

            Session["CallLog_Doctor"] = dsRxQueue.Tables[1];
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

            pageSize = gridRxMedRequest.PageSize;

            pagecount = dsRxQueue.Tables[0].Rows.Count / pageSize;

            if (pagecount * pageSize < dsRxQueue.Tables[0].Rows.Count)
            {
                pagecount = pagecount + 1;

            }
            if (gridRxMedRequest.PageIndex > 0)
            {
                btnPMR.Enabled = true;
                btnPMR.CommandArgument = (gridRxMedRequest.PageIndex - 1).ToString();
            }
            else
            {
                btnPMR.Enabled = false;
            }

            if (gridRxMedRequest.PageIndex == pagecount - 1 || pagecount == 0)
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
                btnNMR.Visible = true;
                btnPMR.Visible = true;
                btnALLMR.Visible = true;
            }

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
         DataTable dt= (DataTable)Session["CallLog_Doctor"];
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
     protected void btnMedIssueSave_Click(object sender, ImageClickEventArgs e)
     {
         objNLog.Info("Event Started...");
         SqlConnection sqlCon = new SqlConnection(conStr);
         
         string user = (string)Session["User"];
         //string query = "Update  [Call_Log] SET Issue_Response='" + txtMedIssueComment.Text
         //               + "',Issue_ResponseFlag='Y',Issue_ResponseBy='" + (string)Session["User"]
         //               + "',Issue_ResponseDate=getdate() where [Call_ID] ='" + hfCallID.Value + "'";

         SqlCommand sqlCmd = new SqlCommand("sp_Update_CallLog_Doctor", sqlCon);
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
}
