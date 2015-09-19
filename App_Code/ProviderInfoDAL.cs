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
/// Summary description for ProviderInfoDAL
/// </summary>
public class ProviderInfoDAL
{
	public ProviderInfoDAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();


    public DataTable GetDoc_FacilityInfo(ProviderInfo Pro_Info)// Newly Added
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlDataAdapter da = new SqlDataAdapter("select * from Doctor_Facility_Info where Doc_Id="+Pro_Info.Doc_Id+" and Facility_ID="+Pro_Info.Fac_Id, con);
        DataSet ds = new DataSet();
        da.Fill(ds, "DocFac_Det");
        return ds.Tables["DocFac_Det"];
    }

    public DataTable GetFacilities()// Newly Added
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlDataAdapter da = new SqlDataAdapter("select * from Facility_Info", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "Fac_Det");
        return ds.Tables["Fac_Det"];    
    }

    public DataTable GetProvider_Info()// Newly Added
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlDataAdapter da = new SqlDataAdapter("select * from Doctor_Info where Status<>'N'", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "Doc_Det");
        return ds.Tables["Doc_Det"];
    }

    public string Update_ProvFacility(ProviderInfo Prov_FaclityInfo, DateTime LastUTime, string Admin_User)
    {
        string msg="";
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_Update_ProviderFacilities", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;

        SqlParameter Doc_Id = sqlCmd.Parameters.Add("@Doc_Id", SqlDbType.Int);
        if (Prov_FaclityInfo.Doc_Id != null)
            Doc_Id.Value = Prov_FaclityInfo.Doc_Id;
        else
            Doc_Id.Value = Convert.DBNull;


        SqlParameter Fac_Id = sqlCmd.Parameters.Add("@Fac_Id", SqlDbType.Int);
        if (Prov_FaclityInfo.Fac_Id != null)
            Fac_Id.Value = Prov_FaclityInfo.Fac_Id;
        else
            Fac_Id.Value = Convert.DBNull;

        SqlParameter Doc_WPNo = sqlCmd.Parameters.Add("@Doc_WPNo", SqlDbType.VarChar, 25);
        if (Prov_FaclityInfo.WPhone != null)
            Doc_WPNo.Value = Prov_FaclityInfo.WPhone;
        else
            Doc_WPNo.Value = Convert.DBNull;

        SqlParameter Doc_Mail = sqlCmd.Parameters.Add("@Doc_Mail", SqlDbType.VarChar, 50);
        if (Prov_FaclityInfo.EMail != null)
            Doc_Mail.Value = Prov_FaclityInfo.EMail;
        else
            Doc_Mail.Value = Convert.DBNull;

        SqlParameter Doc_Fax = sqlCmd.Parameters.Add("@Doc_Fax", SqlDbType.VarChar, 25);
        if (Prov_FaclityInfo.Fax != null)
            Doc_Fax.Value = Prov_FaclityInfo.Fax;
        else
            Doc_Fax.Value = Convert.DBNull;

        SqlParameter Doc_NPINo = sqlCmd.Parameters.Add("@Doc_NPINo", SqlDbType.VarChar, 15);
        if (Prov_FaclityInfo.NPI != null)
            Doc_NPINo.Value = Prov_FaclityInfo.NPI;
        else
            Doc_NPINo.Value = Convert.DBNull;

        SqlParameter Doc_LICNo = sqlCmd.Parameters.Add("@Doc_LICNo", SqlDbType.VarChar, 25);
        if (Prov_FaclityInfo.LicNo != null)
            Doc_LICNo.Value = Prov_FaclityInfo.LicNo;
        else
            Doc_LICNo.Value = Convert.DBNull;

        SqlParameter Doc_DEANo = sqlCmd.Parameters.Add("@Doc_DEANo", SqlDbType.VarChar, 25);
        if (Prov_FaclityInfo.DeaNumber != null)
            Doc_DEANo.Value = Prov_FaclityInfo.DeaNumber;
        else
            Doc_DEANo.Value = Convert.DBNull;


        SqlParameter Stat = sqlCmd.Parameters.Add("@Stat", SqlDbType.VarChar, 50);
        if (Prov_FaclityInfo.Status != null)
            Stat.Value = Prov_FaclityInfo.Status;
        else
            Stat.Value = Convert.DBNull;

        SqlParameter LUT = sqlCmd.Parameters.Add("@lastUpdateTime", SqlDbType.DateTime2, 7);
        LUT.Value = LastUTime;

        SqlParameter LModBy = sqlCmd.Parameters.Add("@LastModTimeBy", SqlDbType.VarChar, 50);
        LModBy.Value = Admin_User;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "Successfully Updated"; 
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Provider Facility.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Provider Facility.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return msg;      
    }


    public string Save_ProvFacility(ProviderInfo Pro_FaclityDet,DateTime LastUTime,string Admin_User)// Newly Added
    {
        string msg = "";
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_setProviderFacilityInfo", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;
                
        
        SqlParameter Doc_Id = sqlCmd.Parameters.Add("@Doc_Id", SqlDbType.Int);
        if (Pro_FaclityDet.Doc_Id != null)
            Doc_Id.Value = Pro_FaclityDet.Doc_Id;
        else
            Doc_Id.Value = Convert.DBNull;

        
        SqlParameter Fac_Id = sqlCmd.Parameters.Add("@Fac_Id", SqlDbType.Int);
        if (Pro_FaclityDet.Fac_Id != null)
            Fac_Id.Value = Pro_FaclityDet.Fac_Id;
        else
            Fac_Id.Value = Convert.DBNull;

        SqlParameter Doc_WPNo = sqlCmd.Parameters.Add("@Doc_WPNo", SqlDbType.VarChar,25);
        if (Pro_FaclityDet.WPhone != null)
            Doc_WPNo.Value = Pro_FaclityDet.WPhone;
        else
            Doc_WPNo.Value = Convert.DBNull;

        SqlParameter Doc_Mail = sqlCmd.Parameters.Add("@Doc_Mail", SqlDbType.VarChar, 50);
        if (Pro_FaclityDet.EMail != null)
            Doc_Mail.Value = Pro_FaclityDet.EMail;
        else
            Doc_Mail.Value = Convert.DBNull;

        SqlParameter Doc_Fax = sqlCmd.Parameters.Add("@Doc_Fax", SqlDbType.VarChar, 25);
        if (Pro_FaclityDet.Fax != null)
            Doc_Fax.Value = Pro_FaclityDet.Fax;
        else
            Doc_Fax.Value = Convert.DBNull;

        SqlParameter Doc_NPINo = sqlCmd.Parameters.Add("@Doc_NPINo", SqlDbType.VarChar, 15);
        if (Pro_FaclityDet.NPI != null)
            Doc_NPINo.Value = Pro_FaclityDet.NPI;
        else
            Doc_NPINo.Value = Convert.DBNull;
        
        SqlParameter Doc_LICNo = sqlCmd.Parameters.Add("@Doc_LICNo", SqlDbType.VarChar, 25);
        if (Pro_FaclityDet.LicNo != null)
            Doc_LICNo.Value = Pro_FaclityDet.LicNo;
        else
            Doc_LICNo.Value = Convert.DBNull;

        SqlParameter Doc_DEANo = sqlCmd.Parameters.Add("@Doc_DEANo", SqlDbType.VarChar, 25);
        if (Pro_FaclityDet.DeaNumber != null)
            Doc_DEANo.Value = Pro_FaclityDet.DeaNumber;
        else
            Doc_DEANo.Value = Convert.DBNull;


        SqlParameter Stat = sqlCmd.Parameters.Add("@Stat", SqlDbType.VarChar, 50);
        if (Pro_FaclityDet.Status != null)
            Stat.Value = Pro_FaclityDet.Status;
        else
            Stat.Value = Convert.DBNull;

        SqlParameter LUT = sqlCmd.Parameters.Add("@lastUpdateTime", SqlDbType.DateTime2, 7);
        LUT.Value = LastUTime;
        
        SqlParameter LModBy = sqlCmd.Parameters.Add("@LastModTimeBy", SqlDbType.VarChar, 50);
        LModBy.Value = Admin_User;

        try
        {           
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "Successfully Inserted";   
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting Provider Facility.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting Provider Facility.", ex);
        }
        finally
        {
            sqlCon.Close();
        }
        return msg;
            
    }

    public string Save_Provider(ProviderInfo ProvInfo,Clinic clinic,string userID)
    {
        string msg="";
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_set_Provider_Details", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter P_FName = sqlCmd.Parameters.Add("@Prov_FName", SqlDbType.VarChar, 50);
        if (ProvInfo.FirstName != null)
            P_FName.Value = ProvInfo.FirstName;
        else
            P_FName.Value = Convert.DBNull;

        SqlParameter P_MName = sqlCmd.Parameters.Add("@Prov_MName", SqlDbType.VarChar, 2);
        if (ProvInfo.MiddleName != null)
            P_MName.Value = ProvInfo.MiddleName;
        else
            P_MName.Value = Convert.DBNull;

        SqlParameter P_LName = sqlCmd.Parameters.Add("@Prov_LName", SqlDbType.VarChar, 50);
        if (ProvInfo.LastName != null)
            P_LName.Value = ProvInfo.LastName;
        else
            P_LName.Value = Convert.DBNull;
        
        SqlParameter P_Degree = sqlCmd.Parameters.Add("@Prov_Degree", SqlDbType.VarChar, 50);
        if (ProvInfo.Degree != null)
            P_Degree.Value = ProvInfo.Degree;
        else
            P_Degree .Value = Convert.DBNull;

        SqlParameter P_Spec = sqlCmd.Parameters.Add("@Prov_Speciality", SqlDbType.VarChar, 25);
        if (ProvInfo.Speciality != null)
            P_Spec.Value = ProvInfo.Speciality;
        else
            P_Spec.Value = Convert.DBNull;
        SqlParameter P_DeaNo = sqlCmd.Parameters.Add("@Prov_DeaNo", SqlDbType.VarChar, 50);
        if (ProvInfo.DeaNumber != null)
            P_DeaNo.Value = ProvInfo.DeaNumber;
        else
            P_DeaNo.Value = Convert.DBNull;

        SqlParameter P_Location = sqlCmd.Parameters.Add("@Prov_Location", SqlDbType.VarChar, 50);
        if (ProvInfo.Location != null)
            P_Location.Value = ProvInfo.Location;
        else
            P_Location.Value = Convert.DBNull;

        SqlParameter P_Address1 = sqlCmd.Parameters.Add("@Prov_Address1", SqlDbType.VarChar, 50);
        if (ProvInfo.Address1 != null)
            P_Address1.Value = ProvInfo.Address1;
        else
            P_Address1.Value = Convert.DBNull;

        SqlParameter P_Address2 = sqlCmd.Parameters.Add("@Prov_Address2", SqlDbType.VarChar, 50);
        if (ProvInfo.Address2 != null)
            P_Address2.Value = ProvInfo.Address2;
        else
            P_Address2.Value = Convert.DBNull;

        SqlParameter P_City = sqlCmd.Parameters.Add("@Prov_City", SqlDbType.VarChar, 50);
        if (ProvInfo.City != null)
            P_City.Value = ProvInfo.City;
        else
            P_City.Value = Convert.DBNull;

        SqlParameter P_State = sqlCmd.Parameters.Add("@Prov_State", SqlDbType.VarChar, 50);
        if (ProvInfo.State != null)
            P_State.Value = ProvInfo.State;
        else
            P_State.Value = Convert.DBNull;
        SqlParameter P_ZIP = sqlCmd.Parameters.Add("@Prov_Zip", SqlDbType.VarChar, 50);
        if (ProvInfo.Zip != null)
            P_ZIP.Value = ProvInfo.Zip;
        else
            P_ZIP.Value = Convert.DBNull;      


        SqlParameter P_HPhone = sqlCmd.Parameters.Add("@Prov_HPhone", SqlDbType.VarChar, 50);
        if (ProvInfo.HPhone != null)
            P_HPhone.Value = ProvInfo.HPhone;
        else
            P_HPhone.Value = Convert.DBNull;
 

        SqlParameter P_CPhone = sqlCmd.Parameters.Add("@Prov_CPhone", SqlDbType.VarChar, 50);
        if (ProvInfo.CPhone != null)
            P_CPhone.Value = ProvInfo.CPhone;
        else
            P_CPhone.Value = Convert.DBNull;
        SqlParameter P_EMail = sqlCmd.Parameters.Add("@Prov_EMail", SqlDbType.VarChar, 50);
        if (ProvInfo.EMail != null)
            P_EMail.Value = ProvInfo.EMail;
        else
            P_EMail.Value = Convert.DBNull;

        SqlParameter P_Fax = sqlCmd.Parameters.Add("@Prov_Fax", SqlDbType.VarChar, 50);
        if (ProvInfo.Fax != null)
            P_Fax.Value = ProvInfo.Fax;
        else
            P_Fax.Value = Convert.DBNull;

        SqlParameter P_LICNo = sqlCmd.Parameters.Add("@Prov_LICNo", SqlDbType.VarChar, 50);
        if (ProvInfo.LicNo != null)
            P_LICNo.Value = ProvInfo.LicNo;
        else
            P_LICNo.Value = Convert.DBNull;
       
        SqlParameter P_Status = sqlCmd.Parameters.Add("@Prov_Status", SqlDbType.VarChar, 50);
        if (ProvInfo.Status != null)
            P_Status.Value = ProvInfo.Status;
        else
            P_Status.Value = Convert.DBNull;


        SqlParameter ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
        if (clinic.ClinicID != null)
            ClinicID.Value = clinic.ClinicID;
        else
            ClinicID.Value = Convert.DBNull;
 

        SqlParameter P_NPI = sqlCmd.Parameters.Add("@Prov_NPI", SqlDbType.VarChar, 50);
        if (ProvInfo.NPI != null)
            P_NPI.Value = ProvInfo.NPI;
        else
            P_NPI.Value = Convert.DBNull;

        SqlParameter P_Sign = sqlCmd.Parameters.Add("@Prov_Sign", SqlDbType.Image);
        if (ProvInfo.Signature != null)
            P_Sign.Value = ProvInfo.Signature;
        else
            P_Sign.Value = Convert.DBNull;

        SqlParameter P_Type = sqlCmd.Parameters.Add("@Prov_Type", SqlDbType.Char);
        P_Type.Value = ProvInfo.UserType;

        SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
        pUserID.Value = userID;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "Provider Registered Successfully";
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting Provider Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting Provider Info.", ex);
        }
        finally
        {
            sqlCon.Close();
        }

        return msg;
    }

    public DataTable getProviderSearch(ProviderInfo ProvInfo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select * from Doctor_Info where Doc_FName ='" + ProvInfo.FirstName + "' and Doc_LName = '" + ProvInfo.LastName + "'", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        sqlDr = sqlCmd.ExecuteReader();
        dtable.Load(sqlDr);
        con.Close();
        return dtable;
    }

    public DataTable get_Provider(string prefixText, int count)
    {
        SqlConnection sqlCon = new SqlConnection(ConStr);
        string prov_Fname;
        string prov_Lname;
        string sqlQuery;
        if (prefixText.Contains(","))
        {
            string[] prov_Name = prefixText.Split(',');

            prov_Lname = prov_Name[0].ToString();
            prov_Fname = prov_Name[1].TrimStart().TrimEnd().ToString();
            //string sqlQuery = "select * from Patient_Info where pat_FName like '" + prefixText + "%'";
            sqlQuery = "select * from Doctor_Info where Doc_LName like '" + prov_Lname + "' and Doc_FName like '" + prov_Fname + "%'";
        }
        else
            sqlQuery = "select * from Doctor_Info where Doc_LName like '" + prefixText + "%'";

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsProvider_Names = new DataSet();
        try
        {
            sqlDa.Fill(dsProvider_Names, "Provider");
        }
        catch (Exception ex)
        {

        }
        return dsProvider_Names.Tables[0];
    }
  

   

    public string Update_Provider(ProviderInfo ProvInfo, Clinic clinic,string userID)
    {
        string msg = "";
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_update_Provider_Details", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter P_ID = sqlCmd.Parameters.Add("@Prov_ID", SqlDbType.Int);
        P_ID.Value = ProvInfo.Prov_ID;

        SqlParameter P_MName = sqlCmd.Parameters.Add("@Prov_MName", SqlDbType.VarChar, 2);
        if (ProvInfo.MiddleName != null)
            P_MName.Value = ProvInfo.MiddleName;
        else
            P_MName.Value = Convert.DBNull;

        SqlParameter P_Spec = sqlCmd.Parameters.Add("@Prov_Speciality", SqlDbType.VarChar, 25);
        if (ProvInfo.Speciality != null)
            P_Spec.Value = ProvInfo.Speciality;
        else
            P_Spec.Value = Convert.DBNull;

        SqlParameter P_Degree = sqlCmd.Parameters.Add("@Prov_Degree", SqlDbType.VarChar, 50);
        if (ProvInfo.Degree != null)
            P_Degree.Value = ProvInfo.Degree;
        else
            P_Degree.Value = Convert.DBNull;
        SqlParameter P_DeaNo = sqlCmd.Parameters.Add("@Prov_DeaNo", SqlDbType.VarChar, 50);
        if (ProvInfo.DeaNumber != null)
            P_DeaNo.Value = ProvInfo.DeaNumber;
        else
            P_DeaNo.Value = Convert.DBNull;

        SqlParameter P_Location = sqlCmd.Parameters.Add("@Prov_Location", SqlDbType.VarChar, 50);
        if (ProvInfo.Location != null)
            P_Location.Value = ProvInfo.Location;
        else
            P_Location.Value = Convert.DBNull;

        SqlParameter P_Address1 = sqlCmd.Parameters.Add("@Prov_Address1", SqlDbType.VarChar, 50);
        if (ProvInfo.Address1 != null)
            P_Address1.Value = ProvInfo.Address1;
        else
            P_Address1.Value = Convert.DBNull;

        SqlParameter P_Address2 = sqlCmd.Parameters.Add("@Prov_Address2", SqlDbType.VarChar, 50);
        if (ProvInfo.Address2 != null)
            P_Address2.Value = ProvInfo.Address2;
        else
            P_Address2.Value = Convert.DBNull;

        SqlParameter P_City = sqlCmd.Parameters.Add("@Prov_City", SqlDbType.VarChar, 50);
        if (ProvInfo.City != null)
            P_City.Value = ProvInfo.City;
        else
            P_City.Value = Convert.DBNull;

        SqlParameter P_State = sqlCmd.Parameters.Add("@Prov_State", SqlDbType.VarChar, 50);
        if (ProvInfo.State != null)
            P_State.Value = ProvInfo.State;
        else
            P_State.Value = Convert.DBNull;
        SqlParameter P_ZIP = sqlCmd.Parameters.Add("@Prov_Zip", SqlDbType.VarChar, 50);
        if (ProvInfo.Zip != null)
            P_ZIP.Value = ProvInfo.Zip;
        else
            P_ZIP.Value = Convert.DBNull;
     


        SqlParameter P_HPhone = sqlCmd.Parameters.Add("@Prov_HPhone", SqlDbType.VarChar, 50);
        if (ProvInfo.HPhone != null)
            P_HPhone.Value = ProvInfo.HPhone;
        else
            P_HPhone.Value = Convert.DBNull;
         
        SqlParameter P_CPhone = sqlCmd.Parameters.Add("@Prov_CPhone", SqlDbType.VarChar, 50);
        if (ProvInfo.CPhone != null)
            P_CPhone.Value = ProvInfo.CPhone;
        else
            P_CPhone.Value = Convert.DBNull;
        SqlParameter P_EMail = sqlCmd.Parameters.Add("@Prov_EMail", SqlDbType.VarChar, 50);
        if (ProvInfo.EMail != null)
            P_EMail.Value = ProvInfo.EMail;
        else
            P_EMail.Value = Convert.DBNull;

        SqlParameter P_Fax = sqlCmd.Parameters.Add("@Prov_Fax", SqlDbType.VarChar, 50);
        if (ProvInfo.Fax != null)
            P_Fax.Value = ProvInfo.Fax;
        else
            P_Fax.Value = Convert.DBNull;


        SqlParameter P_LICNo = sqlCmd.Parameters.Add("@Prov_LICNo", SqlDbType.VarChar, 50);
        if (ProvInfo.LicNo != null)
            P_LICNo.Value = ProvInfo.LicNo;
        else
            P_LICNo.Value = Convert.DBNull;
      

        SqlParameter P_Status = sqlCmd.Parameters.Add("@Prov_Status", SqlDbType.VarChar, 50);
        if (ProvInfo.Status != null)
            P_Status.Value = ProvInfo.Status;
        else
            P_Status.Value = Convert.DBNull;


        SqlParameter ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
        if (clinic.ClinicID != null)
            ClinicID.Value = clinic.ClinicID;
        else
            ClinicID.Value = Convert.DBNull;

         

        SqlParameter P_NPI = sqlCmd.Parameters.Add("@Prov_NPI", SqlDbType.VarChar, 50);
        if (ProvInfo.NPI != null)
            P_NPI.Value = ProvInfo.NPI;
        else
            P_NPI.Value = Convert.DBNull;

        SqlParameter P_Sign = sqlCmd.Parameters.Add("@Prov_Sign", SqlDbType.Image);
        if (ProvInfo.Signature != null)
            P_Sign.Value = ProvInfo.Signature;
        else
            P_Sign.Value = Convert.DBNull;

        SqlParameter P_Type = sqlCmd.Parameters.Add("@Prov_Type", SqlDbType.Char);
        P_Type.Value = ProvInfo.UserType;

         SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar,20);
         pUserID.Value =userID;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "Updated Provider Information Successfully";
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updaing Provider Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updaing Provider Info.", ex);
        }
        finally
        {
            sqlCon.Close();
        }

        return msg;
    }

    public string Delete_Provider(ProviderInfo ProvInfo,string userID)
    {
        string msg = "";
        SqlConnection sqlCon = new SqlConnection(ConStr);
        
        SqlCommand sqlCmd = new SqlCommand("sp_delete_Provider_Details", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        
        SqlParameter P_ID = sqlCmd.Parameters.Add("@Prov_ID", SqlDbType.Int);
        P_ID.Value = ProvInfo.Prov_ID;

        SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
        pUserID.Value = userID;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "Deleted Provider Information Successfully";
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Provider Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Provider Info.", ex);
        }
        finally
        {
            sqlCon.Close();
        }

        return msg;
    }

    public string Delete_ProvFacility(ProviderInfo Pro_FaclityDet)// Newly Added
    {       
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_Delete_ProviderFacilities", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;

        string msg="";

        SqlParameter Doc_Id = sqlCmd.Parameters.Add("@Doc_Id", SqlDbType.Int);
        if (Pro_FaclityDet.Doc_Id != null)
            Doc_Id.Value = Pro_FaclityDet.Doc_Id;
        else
            Doc_Id.Value = Convert.DBNull;


        SqlParameter Fac_Id = sqlCmd.Parameters.Add("@Fac_Id", SqlDbType.Int);
        if (Pro_FaclityDet.Fac_Id != null)
            Fac_Id.Value = Pro_FaclityDet.Fac_Id;
        else
            Fac_Id.Value = Convert.DBNull;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            msg = "Deleted Provider Facility Information Successfully";
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Provider Facility Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Provider Facility Info.", ex);
        }
        finally
        {
            sqlCon.Close();
        }

        return msg; 
    }
}
