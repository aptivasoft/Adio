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
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
/// <summary>
/// Summary description for SendEMail
/// </summary>
public class SMTPSendEMail
{
    public System.Net.Mail.MailMessage msgToSend;
    public System.Net.Mail.SmtpClient emailClient;
    public System.Net.NetworkCredential smtpUserInfo;
    MailResponse objMsgResp = new MailResponse();


    public SMTPSendEMail()
	{

    }
    public MailResponse SendEmail(SMTPProperties objSmtp)
    {
        try
        {
            System.Net.Mail.MailMessage emailMsg = new System.Net.Mail.MailMessage();
            if (objSmtp.MailFrom != "")
                emailMsg.From = new System.Net.Mail.MailAddress(objSmtp.MailFrom);
 
            if(objSmtp.MailTo!="")
            emailMsg.To.Add( objSmtp.MailTo);
            

            if (objSmtp.MailCc != null)
            {
                for (int ccIndex = 0; ccIndex < objSmtp.MailCc.Count(); ccIndex++)
                {
                    emailMsg.CC.Add(new System.Net.Mail.MailAddress(objSmtp.MailCc[ccIndex]));
                }
            }

            if (objSmtp.MailBcc != null)
            {
                for (int bccIndex = 0; bccIndex < objSmtp.MailBcc.Count(); bccIndex++)
                {
                    emailMsg.Bcc.Add(new System.Net.Mail.MailAddress(objSmtp.MailBcc[bccIndex]));
                }
            }

            emailMsg.Subject = objSmtp.MailSubject;

            emailMsg.Body = objSmtp.MailMessage;

            emailMsg.IsBodyHtml = objSmtp.IsHTML;



            if (objSmtp.MailPriority == SMTPProperties.Priority.Normal)
                emailMsg.Priority = System.Net.Mail.MailPriority.Normal;

            if (objSmtp.MailPriority == SMTPProperties.Priority.High)
                emailMsg.Priority = System.Net.Mail.MailPriority.High;

            if (objSmtp.MailPriority == SMTPProperties.Priority.Low)
                emailMsg.Priority = System.Net.Mail.MailPriority.Low;

            if (objSmtp.MailAttachment != null)
            {
                Attachment att = new Attachment(objSmtp.MailAttachment);
                emailMsg.Attachments.Add(att);

            }
            System.Net.Mail.SmtpClient emailClient = new System.Net.Mail.SmtpClient();

            emailClient.EnableSsl = objSmtp.IsSSL;

            emailClient.Host = objSmtp.Server;

            emailClient.Port = objSmtp.Port;

            emailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            System.Net.NetworkCredential emailCredential = new System.Net.NetworkCredential();

            emailCredential.UserName = objSmtp.UserName;

            emailCredential.Password = objSmtp.PassWord;

            //emailClient.UseDefaultCredentials = true; 

            emailClient.Credentials = emailCredential;

            // Send the message to the defined e-mail for processing and delivery with feedback. 
            //string randomToken = "randonTokenTestValue"; 
            //emailClient.SendAsync( emailMsg, randomToken );
            object userState = emailMsg;
            try
            {
                //you can also call emailClient.SendAsync(emailMsg, userState);
                emailClient.Send(emailMsg);
                objMsgResp.StatusMessage = "Mail Sent Successfully";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                objMsgResp.StatusMessage = "SMTP Error: " + ex.Message;
            }
            emailMsg.Dispose();

        }
        catch (System.Net.Mail.SmtpException exSmtp)
        {
            objMsgResp.StatusMessage = "SMTP Error: " + exSmtp.Message;
        }
        catch (System.Exception exGen)
        {
            objMsgResp.StatusMessage = "Error: " + exGen.Message;
        }
        return objMsgResp;
    } 
}
