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
using System.Collections;
using NLog;
/// <summary>
/// Summary description for PharmacyInfoDAL
/// </summary>
public class PharmacyInfoDAL
{
	public PharmacyInfoDAL()
	{
		
	}

    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();
    
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public string Ins_PharmacyInfo(PharmacyInfo phrmInfo)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_set_Pharmacy_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter PharmacyName = sqlCmd.Parameters.Add("@Phrm_Name", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmName != null)
                PharmacyName.Value = phrmInfo.PhrmName;
            else
                PharmacyName.Value = Convert.DBNull;
            SqlParameter PharmacyAddress1 = sqlCmd.Parameters.Add("@Phrm_Address1", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmAddress1 != null)
                PharmacyAddress1.Value = phrmInfo.PhrmAddress1;
            else
                PharmacyAddress1.Value =Convert.DBNull;
            SqlParameter PharmacyAddress2 = sqlCmd.Parameters.Add("@Phrm_Address2", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmAddress2 != null)
                PharmacyAddress2.Value = phrmInfo.PhrmAddress2;
            else
                PharmacyAddress2.Value =Convert.DBNull;
            SqlParameter PharmacyCity = sqlCmd.Parameters.Add("@Phrm_City", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmCity != null)
                PharmacyCity.Value = phrmInfo.PhrmCity;
            else
                PharmacyCity.Value = Convert.DBNull;
            SqlParameter PharmacyState = sqlCmd.Parameters.Add("@Phrm_State", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmState != null)
                PharmacyState.Value = phrmInfo.PhrmState;
            else
                PharmacyState.Value = Convert.DBNull;
            SqlParameter PharmacyZip = sqlCmd.Parameters.Add("@Phrm_Zip", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmZip != null)
                PharmacyZip.Value = phrmInfo.PhrmZip;
            else
                PharmacyZip.Value = Convert.DBNull;

            SqlParameter PharmacyPhone = sqlCmd.Parameters.Add("@Phrm_Phone", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmPhone != null)
                PharmacyPhone.Value = phrmInfo.PhrmPhone;
            else
                PharmacyPhone.Value = Convert.DBNull;
            SqlParameter PharmacyFax = sqlCmd.Parameters.Add("@Phrm_Fax", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmFax != null)
                PharmacyFax.Value = phrmInfo.PhrmFax;
            else
                PharmacyFax.Value = Convert.DBNull;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();

        }

        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            return ex.Message;
        }

        return "Pharmacy Information Inserted Successfully";
    }

    public string Update_PharmacyInfo(PharmacyInfo phrmInfo)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_update_Pharmacy_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter PharmacyID = sqlCmd.Parameters.Add("@Phrm_ID", SqlDbType.Int);
            PharmacyID.Value = phrmInfo.PhrmID;
            SqlParameter PharmacyAddress1 = sqlCmd.Parameters.Add("@Phrm_Address1", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmAddress1 != null)
                PharmacyAddress1.Value = phrmInfo.PhrmAddress1;
            else
                PharmacyAddress1.Value = Convert.DBNull;
            SqlParameter PharmacyAddress2 = sqlCmd.Parameters.Add("@Phrm_Address2", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmAddress2 != null)
                PharmacyAddress2.Value = phrmInfo.PhrmAddress2;
            else
                PharmacyAddress2.Value = Convert.DBNull;
            SqlParameter PharmacyCity = sqlCmd.Parameters.Add("@Phrm_City", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmCity != null)
                PharmacyCity.Value = phrmInfo.PhrmCity;
            else
                PharmacyCity.Value = Convert.DBNull;
            SqlParameter PharmacyState = sqlCmd.Parameters.Add("@Phrm_State", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmState != null)
                PharmacyState.Value = phrmInfo.PhrmState;
            else
                PharmacyState.Value = Convert.DBNull;
            SqlParameter PharmacyZip = sqlCmd.Parameters.Add("@Phrm_Zip", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmZip != null)
                PharmacyZip.Value = phrmInfo.PhrmZip;
            else
                PharmacyZip.Value = Convert.DBNull;

            SqlParameter PharmacyPhone = sqlCmd.Parameters.Add("@Phrm_Phone", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmPhone != null)
                PharmacyPhone.Value = phrmInfo.PhrmPhone;
            else
                PharmacyPhone.Value = Convert.DBNull;
            SqlParameter PharmacyFax = sqlCmd.Parameters.Add("@Phrm_Fax", SqlDbType.VarChar, 50);
            if (phrmInfo.PhrmFax != null)
                PharmacyFax.Value = phrmInfo.PhrmFax;
            else
                PharmacyFax.Value = Convert.DBNull;
            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close(); 
            return "Pharmacy Information Updated Successfully";

        }

        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            return ex.Message;
        }

       
    }

    public string Delete_PharmacyInfo(PharmacyInfo phrmInfo)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_delete_Pharmacy_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter PharmacyID = sqlCmd.Parameters.Add("@Phrm_ID", SqlDbType.Int);
            PharmacyID.Value = phrmInfo.PhrmID;
            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();
            return "Pharmacy Information Deleted Successfully";

        }

        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            return ex.Message;
        }
    }

    public DataTable getPhrmSearch(PharmacyInfo phrmInfo)
    {
        DataTable dtable = new DataTable();
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand("select * from Pharmacy_Info where Phrm_Name = '" + phrmInfo.PhrmName + "'", con);
            SqlDataReader sqlDr;
            
            con.Open();
            sqlDr = sqlCmd.ExecuteReader();
            dtable.Load(sqlDr);

            con.Close();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return dtable;
    }

    public DataTable GetPharmacyList()
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_GetPharmacyList", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsPhrmList = new DataSet();
        try
        {
            sqlDa.Fill(dsPhrmList, "PharmacyList");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsPhrmList.Tables[0];
    }

    public DataSet GetPharmacyTechPharmacy(string phrmLoc)
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_GetPharmacyTechList", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter spPhrmLoc = sqlCmd.Parameters.Add("@Phrm_Loc", SqlDbType.VarChar, 50);
        spPhrmLoc.Value = phrmLoc;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsPhrmTechList = new DataSet();
        try
        {
            sqlDa.Fill(dsPhrmTechList, "PharmacyTechList");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsPhrmTechList;
    }
    public void SetPharmacyTechPharmacy(string phrmUserID, string phrmLoc)
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_Set_PharmacyTech_Pharmacy", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter spPhrmUserID = sqlCmd.Parameters.Add("@PhrmTech_UserID", SqlDbType.VarChar, 50);
        spPhrmUserID.Value = phrmUserID;

        SqlParameter spPhrmLoc = sqlCmd.Parameters.Add("@Phrm_Loc", SqlDbType.VarChar, 50);
        spPhrmLoc.Value = phrmLoc;

       
        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        
    }
    
}
