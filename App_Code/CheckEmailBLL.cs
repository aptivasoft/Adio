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

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class CheckEmailBLL
{
    bool flag = false;
    
	public CheckEmailBLL()
	{
		
	}
    
    public bool SendEmail(Property objProp)
    {
        CheckEmailDAL objEmail = new CheckEmailDAL();
        if (objEmail.getUser(objProp) == 1)
        {
            SendEmail sendEmail = new SendEmail();
            if (sendEmail.sendEmail(objProp))
                flag = true;
            else
                flag = false;
        }
        else
        {
            flag = false; 
        }
        return flag;
    }
   
}
