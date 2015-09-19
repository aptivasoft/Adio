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
/// Summary description for DefaultValues
/// </summary>
public class DefaultValues
{
	public DefaultValues()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    public static String  GetUserName(string userID)
    {
        string UserFullName = userID;
        string conStr = ConfigurationManager.AppSettings["conStr"];
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getUserName", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = userID;

        try
        {
            sqlCon.Open();
            UserFullName = sqlCmd.ExecuteScalar().ToString();
            sqlCon.Close();
            
        }
        catch (Exception ex)
        {
            objNLog.Info("Error : " + ex.Message);
        }
        if (UserFullName != "")
            return UserFullName;
        else
            return userID;
    }

    public static String GetUserDisplayName(string userID)
    {
        string UserFullName = userID;
        string conStr = ConfigurationManager.AppSettings["conStr"];
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getUserDisplayName", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = userID;

        try
        {
            sqlCon.Open();
            UserFullName = sqlCmd.ExecuteScalar().ToString();
            sqlCon.Close();

        }
        catch (Exception ex)
        {
            objNLog.Info("Error : " + ex.Message);
        }
        if (UserFullName != "")
            return UserFullName;
        else
            return userID;
    }

    public DataTable GetDoctorNames(string prefixText)
    {
        DataSet dsdocnames = new DataSet();
        try
        {
            DataTable doc_names = new DataTable();
            string conStr = ConfigurationManager.AppSettings["conStr"];
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getDoctors", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlParm_searchText = sqlCmd.Parameters.Add("@SearchText", SqlDbType.VarChar, 50);
            sqlParm_searchText.Value = prefixText;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsdocnames, "Doc_Name");
        }
        catch (Exception ex)
        {
            objNLog.Info("Error : " + ex.Message);
        }
        return dsdocnames.Tables["Doc_Name"];
    }
}
