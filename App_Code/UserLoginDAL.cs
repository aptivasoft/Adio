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
using Adio.UALog;
using System.Data.SqlClient;
using NLog;

/// <summary>
/// Summary description for Login
/// </summary>
public class UserLoginDAL
{
    int userCount = 0;
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    public UserLoginDAL()
	{
    }
    private UserActivityLog objUALog = new UserActivityLog();

    public int getUser(Property objUser)
    {

        System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_checkUser", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter userID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar,50);
        userID.Value = objUser.UserID.Trim().ToString();

        SqlParameter passWord = sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 32);
        passWord.Value = objUser.Password;//UvT2kjVpYUq3rB30dpYiCw==

        SqlParameter flag = sqlCmd.Parameters.Add("@Flag", SqlDbType.Int);
        flag.Direction = ParameterDirection.Output;

        SqlParameter UserRole = sqlCmd.Parameters.Add("@Role", SqlDbType.Char,1);
        UserRole.Direction = ParameterDirection.Output;

        SqlParameter StampLoc = sqlCmd.Parameters.Add("@LocID", SqlDbType.VarChar, 50);
        StampLoc.Direction = ParameterDirection.Output;
        
       // flag.Value = 0;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            if ((int)flag.Value == 1)
            {
                userCount = 1;
                session["Role"] = (string)UserRole.Value;
                if(StampLoc.Value!=System.DBNull.Value)
                    session["LocID"] = (string)StampLoc.Value;
                string userRoleName = GetUserRoleName(objUser.UserID, (string)UserRole.Value);
                session["RoleName"] = userRoleName;
                objUALog.LogUserActivity(conStr, objUser.UserID,"Logged in as " + userRoleName,"",0);
            }
            else
                userCount = 0;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while User Login.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while User Login.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        
        return userCount;
    }
    private string GetUserRoleName(string userID,string userRole)
    {
        string uRoleName = string.Empty;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_getUserRoleName";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;

            SqlParameter sparm_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar);
            sparm_UserID.Value = userID;

            SqlParameter sparm_URole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
            if (userRole != "")
                sparm_URole.Value = char.Parse(userRole);
            else
                sparm_URole.Value = Convert.DBNull;

            sqlCon.Open();
            uRoleName = (string)sqlCmd.ExecuteScalar();
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from Data Layer with SQLError# " + SqlEx.Number + " while Retrieving User Role Name.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving User Role Name.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return uRoleName;
    }
}
