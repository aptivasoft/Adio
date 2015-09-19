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
/// Summary description for Login
/// </summary>
public class UserLoginBLL
{
    bool flag = false;
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
   
    public UserLoginBLL()
	{
    }
    
    public bool LoginUser(Property objProp)
    {
        try
        {
            UserLoginDAL userLog = new UserLoginDAL();
            if (userLog.getUser(objProp) == 1)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return flag;
    }
}
