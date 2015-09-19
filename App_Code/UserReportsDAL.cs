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
/// Summary description for UserReports
/// </summary>
public class UserReportsDAL
{
    string conStr = ConfigurationManager.AppSettings["conStr"].ToString();
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public UserReportsDAL()
	{
		 
	}
    
    public DataTable GetUserRolesList()
    {
        DataTable dtUserRolesList = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_GetUserRolesList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dtUserRolesList);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dtUserRolesList;
    }

    public DataSet GetUsersList(char userRole,string reportID)
    {
        DataSet dsUsersList = new DataSet();
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getUsersList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter spUserRole = new SqlParameter("@UserRole", SqlDbType.Char, 1);
            spUserRole.Value = userRole;

            SqlParameter spReportID = new SqlParameter("@ReportID", SqlDbType.Int);
            spReportID.Value = int.Parse(reportID);

            sqlCmd.Parameters.Add(spUserRole);
            sqlCmd.Parameters.Add(spReportID);

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsUsersList);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsUsersList;
    }

    public DataTable GetReportsList()
    {
        DataTable dtReportsList = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getReportsList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dtReportsList);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dtReportsList;
    }
    public int SetUserReports(string userID,int reportID, string modifiedBy)
    {
        int resultFlag = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
       
        try
        {
           
            SqlCommand sqlCmd = new SqlCommand("sp_setUserReports", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter spUserID = new SqlParameter("@UserID", SqlDbType.VarChar, 50);
            spUserID.Value = userID;

            SqlParameter spReportID = new SqlParameter("@ReportID", SqlDbType.Int);
            spReportID.Value = reportID;

            SqlParameter spModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
            spModifiedBy.Value = modifiedBy;

            SqlParameter spFlag = new SqlParameter("@Flag", SqlDbType.Int);
            spFlag.Direction = ParameterDirection.Output ;

            sqlCmd.Parameters.Add(spUserID);
            sqlCmd.Parameters.Add(spReportID);
            sqlCmd.Parameters.Add(spModifiedBy);
            sqlCmd.Parameters.Add(spFlag);


            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            resultFlag = int.Parse(spFlag.Value.ToString());
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting User Reports.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Registering User Profile.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return resultFlag;
    }
    public DataTable GetUserReports(string userID)
    {
       
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataTable dtUserReports = new DataTable();
        try
        {

            SqlCommand sqlCmd = new SqlCommand("sp_getUserReports", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter spUserID = new SqlParameter("@UserID", SqlDbType.VarChar, 50);
            spUserID.Value = userID;
 
            sqlCmd.Parameters.Add(spUserID);
            
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dtUserReports);
            
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retieving User Reports.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Registering User Profile.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return dtUserReports;
    }
}
