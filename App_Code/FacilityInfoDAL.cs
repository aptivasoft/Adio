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
using System.Data .SqlClient;
using NLog;
/// <summary>
/// Summary description for FacilityInfoDAL
/// </summary>
public class FacilityInfoDAL
{
	public FacilityInfoDAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string conStr = ConfigurationManager.AppSettings["conStr"].ToString();

    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public int Ins_FacilityInfo(FacilityInfo facInfo,Clinic clinic,string userID)
    {
        int flag = 0;
        try
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_set_Facility_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter FacilityID = sqlCmd.Parameters.Add("@Facility_ID", SqlDbType.VarChar, 15);
            if (facInfo.FacilityID != null)
            FacilityID.Value = facInfo.FacilityID;
            else
            FacilityID.Value = Convert.DBNull;

            SqlParameter FacilityName = sqlCmd.Parameters.Add("@Facility_Name", SqlDbType.VarChar, 50);
            if (facInfo.FacilityName != null)
                FacilityName.Value = facInfo.FacilityName;
            else
                FacilityName.Value = Convert.DBNull;

            SqlParameter FacilityAddress = sqlCmd.Parameters.Add("@Facility_Address", SqlDbType.VarChar, 50);
            if(facInfo.FacilityAddress != null)
            FacilityAddress.Value = facInfo.FacilityAddress;
            else
                FacilityAddress.Value = Convert.DBNull;

            SqlParameter FacilityCity = sqlCmd.Parameters.Add("@Facility_City", SqlDbType.VarChar, 50);
            if (facInfo.FacilityCity != null)
            FacilityCity.Value = facInfo.FacilityCity;
            else
                FacilityCity.Value = Convert.DBNull;

            SqlParameter FacilityState = sqlCmd.Parameters.Add("@Facility_State", SqlDbType.Char, 2);
            if (facInfo.FacilityState != null)
            FacilityState.Value = facInfo.FacilityState.ToCharArray();
            else
                 FacilityState.Value = Convert.DBNull;

            SqlParameter FacilityZip = sqlCmd.Parameters.Add("@Facility_Zip", SqlDbType.Char, 10);
            if (facInfo.FacilityZip != null) 
                FacilityZip.Value = facInfo.FacilityZip.ToCharArray();
            else
                FacilityZip.Value = Convert.DBNull;

            SqlParameter FacilityTPhone = sqlCmd.Parameters.Add("@Facility_TPhone", SqlDbType.VarChar, 15);
            if (facInfo.FacilityTPhone != null) 
                FacilityTPhone.Value = facInfo.FacilityTPhone;
            else 
                FacilityTPhone.Value = Convert.DBNull;

            SqlParameter FacilityFax = sqlCmd.Parameters.Add("@Facility_Fax", SqlDbType.VarChar, 15);
            if (FacilityFax != null) 
                FacilityFax.Value = facInfo.FacilityFax;
            else
                FacilityFax.Value = Convert.DBNull;
            SqlParameter FacilityEMail = sqlCmd.Parameters.Add("@Facility_EMail", SqlDbType.VarChar, 50);
            if (facInfo.FacilityEMail != null) 
                FacilityEMail.Value = facInfo.FacilityEMail;
            else
                FacilityEMail.Value = Convert.DBNull;

            SqlParameter FacilityTaxID = sqlCmd.Parameters.Add("@Facility_TaxID", SqlDbType.VarChar, 20);
            if (facInfo.FacilityTaxID != null) 
                FacilityTaxID.Value = facInfo.FacilityTaxID;
            else
                FacilityTaxID.Value = Convert.DBNull;

            SqlParameter FacilitySpeciality = sqlCmd.Parameters.Add("@Facility_Speciality", SqlDbType.VarChar, 20);
            if (facInfo.FacilitySpeciality != null) 
                FacilitySpeciality.Value = facInfo.FacilitySpeciality;
            else
                FacilitySpeciality.Value =Convert.DBNull;

            SqlParameter FacilityProvID = sqlCmd.Parameters.Add("@Facility_ProvID", SqlDbType.VarChar, 20);
            if (facInfo.FacilityProvID != null) 
                FacilityProvID.Value = facInfo.FacilityProvID;
            else
                FacilityProvID.Value = Convert.DBNull;

            SqlParameter ClinicID = sqlCmd.Parameters.Add("@ClinicID",SqlDbType.Int);
            if (clinic.ClinicID != null)
                ClinicID.Value = clinic.ClinicID;
            else
                ClinicID.Value = Convert.DBNull;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            SqlParameter parmIsStamps = sqlCmd.Parameters.Add("@IsStamps", SqlDbType.Char, 1);
            parmIsStamps.Value = facInfo.IsStamps;

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

    
    public int Delete_facInfo(FacilityInfo facInfo,string userID)
    {
        int flag = 0;
        try
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_delete_Facility_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter FacilityNo = sqlCmd.Parameters.Add("@Facility_No", SqlDbType.Int);
            FacilityNo.Value = facInfo.FacilityNO;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            SqlParameter parmFlag = sqlCmd.Parameters.Add("@Flag", SqlDbType.Int);
            parmFlag.Direction = ParameterDirection.Output;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            flag = (int)parmFlag.Value;
            con.Close();

        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            
        }

        return flag ;

    }


    public int Update_facInfo(FacilityInfo facInfo,Clinic clinic,string userID)
    {
        int flag = 0;
        try
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_update_Facility_Info";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter FacilityName = sqlCmd.Parameters.Add("@Facility_Name", SqlDbType.VarChar, 50);
            if (facInfo.FacilityName != null)
                FacilityName.Value = facInfo.FacilityName;
            else
                FacilityName.Value = Convert.DBNull;

            SqlParameter FacilityNo = sqlCmd.Parameters.Add("@Facility_No", SqlDbType.Int);
            if (facInfo.FacilityNO != null)
                FacilityNo.Value = facInfo.FacilityNO;
            else
                FacilityNo.Value = Convert.DBNull;

            SqlParameter FacilityID = sqlCmd.Parameters.Add("@Facility_ID", SqlDbType.VarChar, 15);
            if (facInfo.FacilityID != null)
                FacilityID.Value = facInfo.FacilityID;
            else
                FacilityID.Value = Convert.DBNull;

            SqlParameter FacilityAddress = sqlCmd.Parameters.Add("@Facility_Address", SqlDbType.VarChar, 50);
            if (facInfo.FacilityAddress != null)
                FacilityAddress.Value = facInfo.FacilityAddress;
            else
                FacilityAddress.Value = Convert.DBNull;

            SqlParameter FacilityCity = sqlCmd.Parameters.Add("@Facility_City", SqlDbType.VarChar, 50);
            if (facInfo.FacilityCity != null)
                FacilityCity.Value = facInfo.FacilityCity;
            else
                FacilityCity.Value = Convert.DBNull;

            SqlParameter FacilityState = sqlCmd.Parameters.Add("@Facility_State", SqlDbType.Char, 2);
            if (facInfo.FacilityState != null)
                FacilityState.Value = facInfo.FacilityState.ToCharArray();
            else
                FacilityState.Value = Convert.DBNull;

            SqlParameter FacilityZip = sqlCmd.Parameters.Add("@Facility_Zip", SqlDbType.Char, 10);
            if (facInfo.FacilityZip != null)
                FacilityZip.Value = facInfo.FacilityZip.ToCharArray();
            else
                FacilityZip.Value = Convert.DBNull;

            SqlParameter FacilityTPhone = sqlCmd.Parameters.Add("@Facility_TPhone", SqlDbType.VarChar, 15);
            if (facInfo.FacilityTPhone != null)
                FacilityTPhone.Value = facInfo.FacilityTPhone;
            else
                FacilityTPhone.Value = Convert.DBNull;

            SqlParameter FacilityFax = sqlCmd.Parameters.Add("@Facility_Fax", SqlDbType.VarChar, 15);
            if (FacilityFax != null)
                FacilityFax.Value = facInfo.FacilityFax;
            else
                FacilityFax.Value = Convert.DBNull;
            SqlParameter FacilityEMail = sqlCmd.Parameters.Add("@Facility_EMail", SqlDbType.VarChar, 50);
            if (facInfo.FacilityEMail != null)
                FacilityEMail.Value = facInfo.FacilityEMail;
            else
                FacilityEMail.Value = Convert.DBNull;

            SqlParameter FacilityTaxID = sqlCmd.Parameters.Add("@Facility_TaxID", SqlDbType.VarChar, 20);
            if (facInfo.FacilityTaxID != null)
                FacilityTaxID.Value = facInfo.FacilityTaxID;
            else
                FacilityTaxID.Value = Convert.DBNull;

            SqlParameter FacilitySpeciality = sqlCmd.Parameters.Add("@Facility_Speciality", SqlDbType.VarChar, 20);
            if (facInfo.FacilitySpeciality != null)
                FacilitySpeciality.Value = facInfo.FacilitySpeciality;
            else
                FacilitySpeciality.Value = Convert.DBNull;

            SqlParameter FacilityProvID = sqlCmd.Parameters.Add("@Facility_ProvID", SqlDbType.VarChar, 20);
            if (facInfo.FacilityProvID != null)
                FacilityProvID.Value = facInfo.FacilityProvID;
            else
                FacilityProvID.Value = Convert.DBNull;

            SqlParameter ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
            if (clinic.ClinicID != null)
                ClinicID.Value = clinic.ClinicID;
            else
                ClinicID.Value = Convert.DBNull;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            SqlParameter parmIsStamps = sqlCmd.Parameters.Add("@IsStamps", SqlDbType.Char, 1);
            parmIsStamps.Value = facInfo.IsStamps;

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

    public DataTable getFacSearch(FacilityInfo facInfo)
    {
        DataTable dtable = new DataTable();
        try
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("select * from Facility_Info where Facility_Name = '" + facInfo.FacilityName + "' and Facility_Code = '" + facInfo.FacilityID + "'", con);
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

    public DataTable get_FacilityNames(string prefixText, int count)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select * from Facility_Info where Facility_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsFactNames = new DataSet();
        try
        {
            sqlDa.Fill(dsFactNames, "ClinicNames");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsFactNames.Tables[0];
    }

    public DataSet GetFacilityList(int ClinicID,string PhrmLoc)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_GetFacilityList", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter spClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
        spClinicID.Value = ClinicID;

        SqlParameter spPhrm_Loc = sqlCmd.Parameters.Add("@Phrm_Loc", SqlDbType.VarChar,50);
        spPhrm_Loc.Value = PhrmLoc;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsFacList = new DataSet();
        try
        {
            sqlDa.Fill(dsFacList, "FacilityList");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsFacList;
    }

    public void SetFacilityPharmacy(int FacilityID,string Phrm_Loc, string modifiedBy)
    {
  
        SqlConnection sqlCon = new SqlConnection(conStr);

        try
        {

            SqlCommand sqlCmd = new SqlCommand("sp_Set_Facility_Pharmacy", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter spFacilityID = sqlCmd.Parameters.Add("@FacilityID", SqlDbType.Int);
            spFacilityID.Value = FacilityID;

            SqlParameter spPhrm_Loc = sqlCmd.Parameters.Add("@Phrm_Loc", SqlDbType.VarChar, 50);
            spPhrm_Loc.Value = Phrm_Loc;
            
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Mapping Pharmacy to Facility.", SqlEx);
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
       
    }
}
