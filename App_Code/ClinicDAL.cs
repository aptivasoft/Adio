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
/// Summary description for ClinicDAL
/// </summary>
public class ClinicDAL
{
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
	public ClinicDAL()
	{ 
	}
    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();

    public string Ins_Clinic(Clinic clinic,string userID)
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandText = "sp_Set_ClinicInfo";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter pClinicName = sqlCmd.Parameters.Add("@ClinicName",SqlDbType.VarChar,50);
            pClinicName.Value =  clinic.ClinicName;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
           
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
        finally
        {
            sqlCon.Close(); 
        }

        return "Clinic Inserted Successfully...";

    }

    public string Delete_Clinic(Clinic clinic)
    {
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_delete_Clinic";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter clinicName = sqlCmd.Parameters.Add("@ClinicName", SqlDbType.VarChar, 50);
            clinicName.Value = clinic.ClinicName;

            con.Open();
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
        finally
        {
            con.Close();
        }

        return "Clinic Deleted Successfully...";

    }


    public DataTable get_ClinicNames(string prefixText, int count, string contextKey)
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        string sqlQuery = "select * from Clinic_Info where Clinic_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsClincNames = new DataSet();
        try
        {
            sqlDa.Fill(dsClincNames, "Clinic_Name");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsClincNames.Tables[0];
    }

    public int getClinicID(Clinic clinic)
    {
        int clinicID = 0;
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand("select Clinic_ID from Clinic_Info where Clinic_Name = '" + clinic.ClinicName + "'", con);
            con.Open();
            clinicID = Convert.ToInt32(sqlCmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return clinicID;
    }

    public string getClinicName(Clinic clinic)
    {
        string clinicName = "";
        SqlConnection con = new SqlConnection(ConStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand("select Clinic_Name from Clinic_Info where Clinic_ID = '" + clinic.ClinicID + "'", con);
            con.Open();
            clinicName = sqlCmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return clinicName;
    }
    public DataTable GetClinicList()
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_GetClinicList", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsClinicList = new DataSet();
        try
        {
            sqlDa.Fill(dsClinicList, "ClinicList");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsClinicList.Tables[0];
    }
}
