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
using NLog;

/// <summary>
/// Summary description for RegisterUser
/// </summary>
public class RegisterUserBLL
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
   
    RegisterUserDAL objUser = new RegisterUserDAL();
    
    public RegisterUserBLL()
	{ }
      
    public bool RegisterNewUser(Property objProp,string user)
    {
        bool flagNewUser = false;
        try
        {
            if (objUser.CreateUser(objProp, user) == 1)
            {
                flagNewUser = true;
            }
            else
            {
                flagNewUser = false;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Registering User Profile.", ex);
        }
        return flagNewUser;
    }

    public bool ChangePassword(Property objProp, string user)
    {
        bool flagNewPwd = false;
        try
        {
            if (objUser.ChangeUserPassword(objProp, user) == 1)
                flagNewPwd = true;
            else
                flagNewPwd = false;
        
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Changing Password.", ex);
        }
        return flagNewPwd;
    }

    public DataTable GetStampLocations()
    {
        DataTable  dtLoc = new DataTable(); 
        try
        {
           dtLoc=objUser.GetStampAddrLocations();
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Changing Password.", ex);
        }
        return dtLoc;
    }
}
