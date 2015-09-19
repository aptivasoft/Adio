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
using System.Data.SqlClient;
using NLog;

/// <summary>
/// Summary description for SendEmail
/// </summary>
/// 
public class CheckEmailDAL
{
    int eMailFlag = 0;
    string conStr = ConfigurationManager.AppSettings["conStr"];

    public CheckEmailDAL()
    { }
    
    public int getUser(Property objUser)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_verfiyEmail", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter eMailID = sqlCmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 50);
        eMailID.Value = objUser.EmailID;

        SqlParameter passWordRem = sqlCmd.Parameters.Add("@PwdRem", SqlDbType.VarChar, 50);
        passWordRem.Value = objUser.PwdRem;

        SqlParameter flag = sqlCmd.Parameters.Add("@Flag", SqlDbType.Int);
        flag.Direction = ParameterDirection.Output;
        flag.Value = 0;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            if ((int)flag.Value == 1)
                eMailFlag = 1;
            else
                eMailFlag = 0;
        }
        catch (Exception ex)
        { }
        finally
        {
            sqlCon.Close();
        }

        return eMailFlag;
    }
   
}
