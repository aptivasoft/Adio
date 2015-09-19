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
/// Summary description for SmtpSettings
/// </summary>
public class SmtpSettings
{
	public SmtpSettings()
	{

    }  
    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public string SetSmtpSettings(SMTPProperties objSmtp,string userID)
    {
        string msg = string.Empty;
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;

            sqlCmd.CommandText = "sp_Set_SmtpSettings";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter spSmtpServer = sqlCmd.Parameters.Add("@SmtpServer", SqlDbType.VarChar, 50);
            spSmtpServer.Value =objSmtp.Server;

            SqlParameter spSmtpPort = sqlCmd.Parameters.Add("@SmtpPort", SqlDbType.Int);
            spSmtpPort.Value = objSmtp.Port;

            SqlParameter spSmtpUserID = sqlCmd.Parameters.Add("@SmtpUserID", SqlDbType.VarChar, 50);
            spSmtpUserID.Value = objSmtp.UserName;

            SqlParameter spSmtpPwd = sqlCmd.Parameters.Add("@SmtpPwd", SqlDbType.VarChar, 50);
            spSmtpPwd.Value = objSmtp.PassWord;

            SqlParameter spSmtpMailFrom = sqlCmd.Parameters.Add("@EmailFrom", SqlDbType.VarChar, 50);
            spSmtpMailFrom.Value = objSmtp.MailFrom;

            SqlParameter spSmtpMailTo = sqlCmd.Parameters.Add("@EmailTo", SqlDbType.VarChar, 50);
            spSmtpMailTo.Value = objSmtp.MailTo;

            SqlParameter spSmtpIsSSL = sqlCmd.Parameters.Add("@IsSSL", SqlDbType.Bit);
            spSmtpIsSSL.Value = objSmtp.IsSSL;

            SqlParameter spSmtpIsHTML = sqlCmd.Parameters.Add("@IsHTML", SqlDbType.Bit);
            spSmtpIsHTML.Value = objSmtp.IsHTML;

            SqlParameter spSmtpMailPriority = sqlCmd.Parameters.Add("@EmailPriority", SqlDbType.Char,1);
            spSmtpMailPriority.Value = objSmtp.EmailPriority;

            SqlParameter spUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
            spUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "SMTP Settings Inserted Successfully...";

        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting SMTP Settings.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting SMTP Settings.", ex);
        }
        finally
        {
            con.Close();

        }


        return msg;

    }
    public string UpdateSmtpSettings(SMTPProperties objSmtp, string userID)
    {
        string msg = string.Empty;
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;

            sqlCmd.CommandText = "sp_Update_SmtpSettings";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter spSmtpServer = sqlCmd.Parameters.Add("@SmtpServer", SqlDbType.VarChar, 50);
            spSmtpServer.Value = objSmtp.Server;

            SqlParameter spSmtpPort = sqlCmd.Parameters.Add("@SmtpPort", SqlDbType.Int);
            spSmtpPort.Value = objSmtp.Port;

            SqlParameter spSmtpUserID = sqlCmd.Parameters.Add("@SmtpUserID", SqlDbType.VarChar, 50);
            spSmtpUserID.Value = objSmtp.UserName;

            SqlParameter spSmtpPwd = sqlCmd.Parameters.Add("@SmtpPwd", SqlDbType.VarChar, 50);
            spSmtpPwd.Value = objSmtp.PassWord;

            SqlParameter spSmtpMailFrom = sqlCmd.Parameters.Add("@EmailFrom", SqlDbType.VarChar, 50);
            spSmtpMailFrom.Value = objSmtp.MailFrom;

            SqlParameter spSmtpMailTo = sqlCmd.Parameters.Add("@EmailTo", SqlDbType.VarChar, 50);
            spSmtpMailTo.Value = objSmtp.MailTo;

            SqlParameter spSmtpIsSSL = sqlCmd.Parameters.Add("@IsSSL", SqlDbType.Bit);
            spSmtpIsSSL.Value = objSmtp.IsSSL;

            SqlParameter spSmtpIsHTML = sqlCmd.Parameters.Add("@IsHTML", SqlDbType.Bit);
            spSmtpIsHTML.Value = objSmtp.IsHTML;

            SqlParameter spSmtpMailPriority = sqlCmd.Parameters.Add("@EmailPriority", SqlDbType.Char, 1);
            spSmtpMailPriority.Value = objSmtp.EmailPriority;

            SqlParameter spUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
            spUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "SMTP Settings Updated Successfully...";

        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating SMTP Settings.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating SMTP Settings.", ex);
        }
        finally
        {
            con.Close();

        }


        return msg;

    }


    public DataSet GetSmtpSettings()
    {
        DataSet dsSmtp = new DataSet();
        SqlConnection SqlCon = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = SqlCon;

            sqlCmd.CommandText = "sp_Get_SmtpSettings";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsSmtp);

        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting SMTP Settings.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting SMTP Settings.", ex);
        }
        finally
        {
            SqlCon.Close();

        }


        return dsSmtp;

    }
}
