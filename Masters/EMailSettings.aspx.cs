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
using System.Text;
using NLog;
public partial class Masters_EMailSettings : System.Web.UI.Page
{
    SMTPProperties objSmtpProp = new SMTPProperties();
    SmtpSettings objSmtp = new SmtpSettings(); 
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        GetSmtpSettings();
        RenderJSArrayWithCliendIds(txtSmtpServer,txtSmtpPort,txtSmtpUserID,txtSmtpPassword,txtMailFrom,txtMailTo);
    }
    protected void btnSmtpSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            objSmtpProp.Server = txtSmtpServer.Text;
            objSmtpProp.Port = int.Parse(txtSmtpPort.Text);
            objSmtpProp.UserName = txtSmtpUserID.Text;
            objSmtpProp.PassWord = txtSmtpPassword.Text;
            objSmtpProp.IsHTML = chkIsHTML.Checked;
            objSmtpProp.IsSSL = chkIsSSL.Checked;
            objSmtpProp.EmailPriority = char.Parse(ddlMailPriority.SelectedItem.Value);
            objSmtpProp.MailFrom = txtMailFrom.Text;
            objSmtpProp.MailTo = txtMailTo.Text;
            string str = "alert('" + objSmtp.SetSmtpSettings(objSmtpProp, userID) + "');";
            ScriptManager.RegisterStartupScript(btnSmtpSubmit, typeof(Page), "alert", str, true);
           
        }
        catch (Exception ex)
        {
           objNLog.Error("Error : " + ex.Message);
        }
            
    }
    protected void btnSmtpUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            objSmtpProp.Server = txtSmtpServer.Text;
            objSmtpProp.Port = int.Parse(txtSmtpPort.Text);
            objSmtpProp.UserName = txtSmtpUserID.Text;
            objSmtpProp.PassWord = txtSmtpPassword.Text;
            objSmtpProp.IsHTML = chkIsHTML.Checked;
            objSmtpProp.IsSSL = chkIsSSL.Checked;
            objSmtpProp.EmailPriority = char.Parse(ddlMailPriority.SelectedItem.Value);
            objSmtpProp.MailFrom = txtMailFrom.Text;
            objSmtpProp.MailTo = txtMailTo.Text;
            string str = "alert('" + objSmtp.UpdateSmtpSettings(objSmtpProp, userID) + "');";
            ScriptManager.RegisterStartupScript(btnSmtpUpdate, typeof(Page), "alert", str, true);

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    private void GetSmtpSettings()
    {
        SmtpSettings objSmtp = new SmtpSettings();
        DataSet dsSmtp = objSmtp.GetSmtpSettings();
        if (dsSmtp.Tables.Count > 0)
        {
            if (dsSmtp.Tables[0].Rows.Count > 0)
            {
                btnSmtpUpdate.Visible = true;
                btnSmtpSubmit.Visible = false;
                SMTPProperties objSmtpProp = new SMTPProperties();
                SMTPSendEMail objEmail = new SMTPSendEMail();
                
                if(dsSmtp.Tables[0].Rows[0][0]!= System.DBNull.Value)
                    txtSmtpServer.Text = dsSmtp.Tables[0].Rows[0][0].ToString();

                if (dsSmtp.Tables[0].Rows[0][1] != System.DBNull.Value)
                    txtSmtpPort.Text = dsSmtp.Tables[0].Rows[0][1].ToString();

                if (dsSmtp.Tables[0].Rows[0][2] != System.DBNull.Value)
                    txtSmtpUserID.Text = dsSmtp.Tables[0].Rows[0][2].ToString();

                if (dsSmtp.Tables[0].Rows[0][3] != System.DBNull.Value)
                    txtSmtpPassword.Text = dsSmtp.Tables[0].Rows[0][3].ToString();

                if (dsSmtp.Tables[0].Rows[0][4] != System.DBNull.Value)
                    txtMailFrom.Text = dsSmtp.Tables[0].Rows[0][4].ToString();

                if (dsSmtp.Tables[0].Rows[0][5] != System.DBNull.Value)
                    txtMailTo.Text = dsSmtp.Tables[0].Rows[0][5].ToString();

                if (dsSmtp.Tables[0].Rows[0][6] != System.DBNull.Value)
                    chkIsSSL.Checked = bool.Parse(dsSmtp.Tables[0].Rows[0][6].ToString());

                if (dsSmtp.Tables[0].Rows[0][7] != System.DBNull.Value)
                    chkIsHTML.Checked = bool.Parse(dsSmtp.Tables[0].Rows[0][7].ToString());
                
                char mailPriority='N';
                if (dsSmtp.Tables[0].Rows[0][8] != System.DBNull.Value)
                      mailPriority = char.Parse(dsSmtp.Tables[0].Rows[0][8].ToString());
                
                switch (mailPriority)
                {
                    case 'L': { ddlMailPriority.SelectedValue = "L"; break; }
                    case 'H': { ddlMailPriority.SelectedValue = "H"; break; }
                    case 'N': { ddlMailPriority.SelectedValue = "N"; break; }
                }
            }
        }
        else
        {
            btnSmtpSubmit.Visible = true;
            btnSmtpUpdate.Visible = false;
        }
    }
    public void RenderJSArrayWithCliendIds(params Control[] wc)
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
            // Now register the method GetClientId, used to get the client id of tthe control
            cs.RegisterStartupScript(this.Page.GetType(), "key", "\nfunction GetClientId(serverId)\n{\nfor(i=0; i<MyServerID.length; i++)\n{\nif ( MyServerID[i] == serverId )\n{\nreturn MyClientID[i];\nbreak;\n}\n}\n}", true);
        }
    }
}
