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
/// Summary description for SIGCodesDAL
/// </summary>
public class SIGCodesDAL
{
	public SIGCodesDAL()
	{ }

    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();

    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public string Ins_SIGCodes(SIGCodes sig,string userID)
    {
        string msg = string.Empty;
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;

            sqlCmd.CommandText = "sp_set_SIGCodes";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter pSIGCode = sqlCmd.Parameters.Add("@SIGCode", SqlDbType.VarChar, 10);
            pSIGCode.Value = sig.SIGCode;

            SqlParameter pSIGName = sqlCmd.Parameters.Add("@SIGName", SqlDbType.VarChar, 50);
            pSIGName.Value = sig.SIGName;

            SqlParameter pSIGFactor = sqlCmd.Parameters.Add("@SIGFactor", SqlDbType.Float);
            if (sig.SIGFactor != "")
                pSIGFactor.Value = float.Parse(sig.SIGFactor);
            else
                pSIGFactor.Value = Convert.DBNull;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "SIG Code Inserted Successfully...";
             
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting SIG Code.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting SIG Code.", ex);
        }
        finally
        {
            con.Close();
           
        }
        

        return msg;

    }

    public string Delete_SIGCodes(SIGCodes sig,string userID)
    {
       
        string msg = string.Empty;
         
        SqlConnection con = new SqlConnection(ConStr);
        try
        {

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_delete_SIGCodes";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pSIG_ID = sqlCmd.Parameters.Add("@SIGID", SqlDbType.Int);
            pSIG_ID.Value = sig.SIG_ID;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "SIG Code Deleted Successfully...";
             
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting SIG Code.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Problem occured with Deleting SIG Code", ex);
        }
        finally
        {
            con.Close();
        }
        return msg;
    }

    public string Update_SIGCodes(SIGCodes sig,string userID)
    {
       
        string msg = string.Empty;
         
        SqlConnection con = new SqlConnection(ConStr);
        try
        {

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_update_SIGCodes";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            
            SqlParameter pSIGID = sqlCmd.Parameters.Add("@SIGID", SqlDbType.Int);
            pSIGID.Value = sig.SIG_ID;
            
            SqlParameter pSIGCode = sqlCmd.Parameters.Add("@SIGCode", SqlDbType.VarChar, 10);
            pSIGCode.Value = sig.SIGCode;
            
            SqlParameter pSIGName = sqlCmd.Parameters.Add("@SIGName", SqlDbType.VarChar, 50);
            pSIGName.Value = sig.SIGName;

            SqlParameter pSIGFactor = sqlCmd.Parameters.Add("@SIGFactor", SqlDbType.Float);
            pSIGFactor.Value = sig.SIGFactor;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "SIG Code Updated Successfully...";
             
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting SIG Code.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Problem occurred with Updating SIG Code", ex);
        }
        finally
        {
            con.Close();
        }
        
        return msg;

    }

    public DataTable getSIGSearch(SIGCodes sigInfo)
    {
       
        
        SqlConnection con = new SqlConnection(ConStr);
        DataTable dtable = new DataTable();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("select * from SIG_Codes where SIG_Code = '" + sigInfo.SIGCode + "'", con);
            SqlDataReader sqlDr;

            // DataRow dr;
            con.Open();
            sqlDr = sqlCmd.ExecuteReader();
            dtable.Load(sqlDr);
             
        }
       
        catch (Exception ex)
        {
            throw new Exception("**Problem occurred with Searching SIGCode", ex);
        }
        finally
        {
            con.Close();
           
        }
        return dtable;
    }
}
