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
/// Summary description for InsuranceInfoDAL
/// </summary>
public class InsuranceInfoDAL
{
	public InsuranceInfoDAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();

    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public int Insert_InsuranceInfo(InsuranceInfo insInfo,string userID)
    {
        int flag = 0;
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_set_Insurance_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter InsuranceName = sqlCmd.Parameters.Add("@Ins_Name", SqlDbType.VarChar, 50);
            if (insInfo.InsName != null)
                InsuranceName.Value = insInfo.InsName;
            else
                InsuranceName.Value = Convert.DBNull;
            SqlParameter InsuranceNumber = sqlCmd.Parameters.Add("@Ins_Number", SqlDbType.VarChar, 50);
            if(insInfo.InsNumber != null)
            InsuranceNumber.Value = insInfo.InsNumber;
            else
                InsuranceNumber.Value = Convert.DBNull;
            SqlParameter InsuranceCompany = sqlCmd.Parameters.Add("@Ins_Company", SqlDbType.VarChar, 50);
            if (insInfo.InsCompany != null)
                InsuranceCompany.Value = insInfo.InsCompany;
            else
                InsuranceCompany.Value = Convert.DBNull;
            SqlParameter InsuranceAddress1 = sqlCmd.Parameters.Add("@Ins_Address1", SqlDbType.VarChar, 50);
            if (insInfo.InsAddress1 != null)
            InsuranceAddress1.Value = insInfo.InsAddress1;
            else
                InsuranceAddress1.Value = Convert.DBNull;
            SqlParameter InsuranceAddress2 = sqlCmd.Parameters.Add("@Ins_Address2", SqlDbType.VarChar, 50);
            if (insInfo.InsAddress2 != null)
            InsuranceAddress2.Value = insInfo.InsAddress2;
            else
                InsuranceAddress2.Value = Convert.DBNull;
            SqlParameter InsuranceCity = sqlCmd.Parameters.Add("@Ins_City", SqlDbType.VarChar, 50);
            if (insInfo.InsCity != null)
            InsuranceCity.Value = insInfo.InsCity;
            else
                InsuranceCity.Value = Convert.DBNull;
            SqlParameter InsuranceState = sqlCmd.Parameters.Add("@Ins_State", SqlDbType.VarChar, 50);
            if (insInfo.InsState != null)
            InsuranceState.Value = insInfo.InsState;
            else
                InsuranceState.Value = Convert.DBNull;
            SqlParameter InsuranceZip = sqlCmd.Parameters.Add("@Ins_Zip", SqlDbType.VarChar, 50);
            if (insInfo.InsZip != null) 
                InsuranceZip.Value = insInfo.InsZip;
            else
                InsuranceZip.Value =Convert.DBNull;
            SqlParameter InsurancePhone = sqlCmd.Parameters.Add("@Ins_Phone", SqlDbType.VarChar, 50);
            if (insInfo.InsPhone != null) 
                InsurancePhone.Value = insInfo.InsPhone;
            else
                InsurancePhone.Value = Convert.DBNull;
            SqlParameter InsuranceFax = sqlCmd.Parameters.Add("@Ins_Fax", SqlDbType.VarChar, 50);
            if (insInfo.InsFax != null) 
                InsuranceFax.Value = insInfo.InsFax;
            else
                InsuranceFax.Value = Convert.DBNull;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;


            con.Open();
            sqlCmd.ExecuteNonQuery();
            flag = 1;
            con.Close();

        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            
        }

        return flag;
    }

    public int Delete_InsInfo(InsuranceInfo insInfo,string userID)
    {
        int flag = 0;
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_delete_Insurance_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter InsuranceID = sqlCmd.Parameters.Add("@Ins_ID", SqlDbType.Int);
            InsuranceID.Value = insInfo.InsID;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;


            con.Open();
            sqlCmd.ExecuteNonQuery();
            flag = 1;
            con.Close();

        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            
        }

        return flag;

    }


    public int Update_InsInfo(InsuranceInfo insInfo,string userID)
    {
        int flag = 0;
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_update_Insurance_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter InsuranceID = sqlCmd.Parameters.Add("@Ins_ID", SqlDbType.Int);
            InsuranceID.Value = insInfo.InsID;
            SqlParameter InsuranceNumber = sqlCmd.Parameters.Add("@Ins_Number", SqlDbType.VarChar, 50);
            if (insInfo.InsNumber != null)
                InsuranceNumber.Value = insInfo.InsNumber;
            else
                InsuranceNumber.Value = Convert.DBNull;

            SqlParameter InsuranceCompany = sqlCmd.Parameters.Add("@Ins_Company", SqlDbType.VarChar, 50);
            if (insInfo.InsCompany != null)
                InsuranceCompany.Value = insInfo.InsCompany;
            else
                InsuranceCompany.Value = Convert.DBNull;
            SqlParameter InsuranceAddress1 = sqlCmd.Parameters.Add("@Ins_Address1", SqlDbType.VarChar, 50);
            if (insInfo.InsAddress1 != null)
                InsuranceAddress1.Value = insInfo.InsAddress1;
            else
                InsuranceAddress1.Value = Convert.DBNull;
            SqlParameter InsuranceAddress2 = sqlCmd.Parameters.Add("@Ins_Address2", SqlDbType.VarChar, 50);
            if (insInfo.InsAddress2 != null)
                InsuranceAddress2.Value = insInfo.InsAddress2;
            else
                InsuranceAddress2.Value = Convert.DBNull;
            SqlParameter InsuranceCity = sqlCmd.Parameters.Add("@Ins_City", SqlDbType.VarChar, 50);
            if (insInfo.InsCity != null)
                InsuranceCity.Value = insInfo.InsCity;
            else
                InsuranceCity.Value = Convert.DBNull;
            SqlParameter InsuranceState = sqlCmd.Parameters.Add("@Ins_State", SqlDbType.VarChar, 50);
            if (insInfo.InsState != null)
                InsuranceState.Value = insInfo.InsState;
            else
                InsuranceState.Value = Convert.DBNull;
            SqlParameter InsuranceZip = sqlCmd.Parameters.Add("@Ins_Zip", SqlDbType.VarChar, 50);
            if (insInfo.InsZip != null)
                InsuranceZip.Value = insInfo.InsZip;
            else
                InsuranceZip.Value = Convert.DBNull;
            SqlParameter InsurancePhone = sqlCmd.Parameters.Add("@Ins_Phone", SqlDbType.VarChar, 50);
            if (insInfo.InsPhone != null)
                InsurancePhone.Value = insInfo.InsPhone;
            else
                InsurancePhone.Value = Convert.DBNull;
            SqlParameter InsuranceFax = sqlCmd.Parameters.Add("@Ins_Fax", SqlDbType.VarChar, 50);
            if (insInfo.InsFax != null)
                InsuranceFax.Value = insInfo.InsFax;
            else
                InsuranceFax.Value = Convert.DBNull;

            SqlParameter pUserID= sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            flag = 1;
            con.Close();

        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
             
        }

        return flag;

    }

    public DataTable getInsSearch(InsuranceInfo insInfo)
    {
        DataTable dtable = new DataTable();
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand("select * from Insurance_Info where Ins_Name = '" + insInfo.InsName + "'", con);
            SqlDataReader sqlDr;
            con.Open();
            sqlDr = sqlCmd.ExecuteReader();
            dtable.Load(sqlDr);
            con.Close();
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dtable;
    }

    public int getInsuranceID(InsuranceInfo insInfo)
    {
        int InsuranceID = 0;
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand("select Ins_ID from Insurance_Info where Ins_Name = '" + insInfo.InsName + "'", con);
            con.Open();
            InsuranceID = Convert.ToInt32(sqlCmd.ExecuteScalar());
            con.Close();
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return InsuranceID;
    }
}
