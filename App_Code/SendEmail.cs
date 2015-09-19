using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mail;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{
	public SendEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    bool flag = false;
    public bool sendEmail(Property objProp)
    {
        MailMessage mail = new MailMessage();
        mail.To = objProp.EmailID;
        mail.From = "apkumarg@gmail.com";
        mail.Subject = "New Password";
        string msgBody="Hi Please click the following link:";
        mail.Body = msgBody;
        try
        {
            SmtpMail.SmtpServer = "localhost";
            SmtpMail.Send(mail);
            flag = true;
           
        }
        catch (Exception ex)
        {
            flag = false;
        }
        return flag;
    }
}
