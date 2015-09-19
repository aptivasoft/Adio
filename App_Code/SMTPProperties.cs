using System;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
 
    public class SMTPProperties
    {

        public int Port { set; get; }

        public string Server { set; get; }

        public string UserName { set; get; }

        public string PassWord { set; get; }

        public string MailFrom { set; get; }

        public string  MailTo { set; get; }

        public string[] MailCc { set; get; }

        public string[] MailBcc { set; get; }

        public string MailSubject { set; get; }

        public string MailMessage { set; get; }

        public string MailAttachment { set; get; }

        private bool _isSsl = false;
        public bool IsSSL { set { _isSsl = value; } get { return _isSsl; } }

        private bool _isHtml = false;
        public bool IsHTML { set { _isHtml = value; } get { return _isHtml; } }

        public Priority MailPriority { set; get; }

        public enum Priority
        {
            Normal = 0,
            High = 1,
            Low = 2,
        }
        
        public char EmailPriority { get; set; }
    }
 
