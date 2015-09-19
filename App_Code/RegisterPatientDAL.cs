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

/// <summary>
/// Summary description for RegisterPatient
/// </summary>
public class RegisterPatientDAL
{

    int resultFlag = 0;
    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();
	public RegisterPatientDAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Reg_Patient(PatientName Pat_Name, PatientPersonalDetails Pat_Per_Details, PatientAddress Pat_Add,
                           PatientInsuranceDetails Pat_Ins_Details, PatientAllergies Pat_Allergies, ProviderInfo Prov_Info, 
                            FacilityInfo Fac_Info,PharmacyInfo Phrm_Info)
    {
       
        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_Reg_Patient", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter P_FName = sqlCmd.Parameters.Add("@Pat_FName", SqlDbType.VarChar, 50);
        if (Pat_Name.FirstName != null)
            P_FName.Value = Pat_Name.FirstName;
        else
            P_FName.Value = Convert.DBNull;

        SqlParameter P_MName = sqlCmd.Parameters.Add("@Pat_MName", SqlDbType.VarChar, 2);
        if (Pat_Name.MiddleName != null)
            P_MName.Value = Pat_Name.MiddleName;
        else
            P_MName.Value = Convert.DBNull;

        SqlParameter P_LName = sqlCmd.Parameters.Add("@Pat_LName", SqlDbType.VarChar, 50);
        if (Pat_Name.LastName != null)
            P_LName.Value = Pat_Name.LastName;
        else
            P_LName.Value = Convert.DBNull;

        SqlParameter P_Gender = sqlCmd.Parameters.Add("@Pat_Gender", SqlDbType.Char, 1);
        if (Pat_Per_Details.Gender != null)
            P_Gender.Value = Pat_Per_Details.Gender;
        else
            P_Gender.Value = Convert.DBNull;

        SqlParameter P_DOB = sqlCmd.Parameters.Add("@Pat_DOB", SqlDbType.DateTime);
        if (Pat_Per_Details.DOB != null)
            P_DOB.Value = DateTime.Parse( Pat_Per_Details.DOB);
        else
        {
            Nullable<DateTime> NullDate = null;
            P_DOB.Value = NullDate;
        }

        SqlParameter P_SSN = sqlCmd.Parameters.Add("@Pat_SSN", SqlDbType.VarChar, 50);
        if (Pat_Per_Details.SSN != null)
            P_SSN.Value = Pat_Per_Details.SSN;
        else
            P_SSN.Value = Convert.DBNull;

        SqlParameter P_Address1 = sqlCmd.Parameters.Add("@Pat_Address1", SqlDbType.VarChar, 50);
        if (Pat_Add.Address1 != null)
            P_Address1.Value = Pat_Add.Address1;
        else
            P_Address1.Value = Convert.DBNull;

        SqlParameter P_Address2 = sqlCmd.Parameters.Add("@Pat_Address2", SqlDbType.VarChar, 50);
        if (Pat_Add.Address2 != null)
            P_Address2.Value = Pat_Add.Address2;
        else
            P_Address2.Value = Convert.DBNull;

        SqlParameter P_City = sqlCmd.Parameters.Add("@Pat_City", SqlDbType.VarChar, 50);
        if(Pat_Add.City != null)
            P_City.Value = Pat_Add.City;
        else
            P_City.Value = Convert.DBNull;

        SqlParameter P_State = sqlCmd.Parameters.Add("@Pat_State", SqlDbType.VarChar, 50);
        if (Pat_Add.State != null)
            P_State.Value = Pat_Add.State;
        else
            P_State.Value = Convert.DBNull;

        SqlParameter P_ZIP = sqlCmd.Parameters.Add("@Pat_Zip", SqlDbType.VarChar, 50);
        if (Pat_Add.Zip != null)
            P_ZIP.Value = Pat_Add.Zip;
        else
            P_ZIP.Value = Convert.DBNull;

        SqlParameter P_PDoc = sqlCmd.Parameters.Add("@Pat_PDoc", SqlDbType.VarChar, 50);
        if (Pat_Per_Details.Pat_Pre_Doc != null)
            P_PDoc.Value = Pat_Per_Details.Pat_Pre_Doc;
        else
            P_PDoc.Value = Convert.DBNull;


        SqlParameter P_MRN = sqlCmd.Parameters.Add("@Pat_MRN", SqlDbType.VarChar, 15);
        if (Pat_Per_Details.MRN != null)
            P_MRN.Value = Pat_Per_Details.MRN;
        else
            P_MRN.Value = Convert.DBNull;

        SqlParameter Fac_ID = sqlCmd.Parameters.Add("@Fac_ID", SqlDbType.Int);
        if (Fac_Info.FacilityNO != null)
            Fac_ID.Value = Fac_Info.FacilityNO;
        else
            Fac_ID.Value = Convert.DBNull;

        SqlParameter Phrm_ID = sqlCmd.Parameters.Add("@Phrm_ID", SqlDbType.Int);
        if (Phrm_Info.PhrmID != null)
            Phrm_ID.Value = Phrm_Info.PhrmID;
        else
            Phrm_ID.Value = Convert.DBNull;

        //SqlParameter PI_InsName = sqlCmd.Parameters.Add("@Ins_Name", SqlDbType.VarChar, 50);
        //if (Pat_Ins_Details.InsuranceName != null)
        //    PI_InsName.Value = Pat_Ins_Details.InsuranceName;
        //else
        //    PI_InsName.Value = Convert.DBNull;

        SqlParameter Ins_ID = sqlCmd.Parameters.Add("@Ins_ID", SqlDbType.Int);
        if (Pat_Ins_Details.InsuranceID.ToString() != "0")
            Ins_ID.Value = Pat_Ins_Details.InsuranceID;
        else
            Ins_ID.Value = Convert.DBNull;

        SqlParameter PI_No = sqlCmd.Parameters.Add("@PI_Number", SqlDbType.Int);
        if (Pat_Ins_Details.PI_PolicyNo.ToString() != "0")
            PI_No.Value = Pat_Ins_Details.PI_PolicyNo;
        else
            PI_No.Value = Convert.DBNull;

        SqlParameter PI_PolicyID = sqlCmd.Parameters.Add("@PI_PolicyID", SqlDbType.Int);
        if (Pat_Ins_Details.PI_PolicyID.ToString() != "0")
            PI_PolicyID.Value = Pat_Ins_Details.PI_PolicyID;
        else
            PI_PolicyID.Value = Convert.DBNull;

        SqlParameter PI_GroupNo = sqlCmd.Parameters.Add("@PI_GroupNo", SqlDbType.Int);
        if (Pat_Ins_Details.PI_GroupNo.ToString() != "0")
            PI_GroupNo.Value = Pat_Ins_Details.PI_GroupNo;
        else
            PI_GroupNo.Value = Convert.DBNull;

        SqlParameter PI_BINNo = sqlCmd.Parameters.Add("@PI_BINNo", SqlDbType.Int);
        if (Pat_Ins_Details.PI_BINNo.ToString() != "0")
            PI_BINNo.Value = Pat_Ins_Details.PI_BINNo;  
        else
            PI_BINNo.Value = Convert.DBNull;

        SqlParameter PI_InsdName = sqlCmd.Parameters.Add("@PI_InsdName", SqlDbType.VarChar, 50);
        if (Pat_Ins_Details.InsuredName != null)
            PI_InsdName.Value = Pat_Ins_Details.InsuredName;
        else
            PI_InsdName.Value = Convert.DBNull;

        SqlParameter PI_InsdDOB = sqlCmd.Parameters.Add("@PI_InsdDOB", SqlDbType.DateTime);
        if (Pat_Ins_Details.InsuredDOB != null)
            PI_InsdDOB.Value = Pat_Ins_Details.InsuredDOB;
        else
        {
            Nullable<DateTime> NullDate = null;
            PI_InsdDOB.Value = NullDate;
        }
        SqlParameter PI_InsdSSN = sqlCmd.Parameters.Add("@PI_InsdSSN", SqlDbType.VarChar, 50);
        if (Pat_Ins_Details.InsuredSSN != null)
        PI_InsdSSN.Value = Pat_Ins_Details.InsuredSSN;
        else
            PI_InsdSSN.Value = Convert.DBNull;
        SqlParameter PI_InsdRel = sqlCmd.Parameters.Add("@PI_InsdRel", SqlDbType.VarChar, 50);
        if (Pat_Ins_Details.InsuredRelation != null)
        PI_InsdRel.Value = Pat_Ins_Details.InsuredRelation;
        else
            PI_InsdRel.Value = Convert.DBNull;

        SqlParameter PA_Desc = sqlCmd.Parameters.Add("@PA_Desc", SqlDbType.VarChar, 200);
        if (Pat_Allergies.AllergyDescription != null)
        PA_Desc.Value = Pat_Allergies.AllergyDescription;
        else
            PA_Desc.Value = Convert.DBNull;
     


        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            
          
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
        finally
        {

            sqlCon.Close();
            

        }

        return "Registered Successfully";
    }

    public DataTable getPatSearch(PatientName Pat_Name)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select * from Patient_Info where Pat_FName ='"+Pat_Name.FirstName+"' and Pat_LName = '"+Pat_Name.LastName+ "'", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        sqlDr = sqlCmd.ExecuteReader();
        dtable.Load(sqlDr);
        con.Close();
        return dtable;
    }


    public DataTable getPatInsDetails(PatientName Pat_Name)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select * from Patient_Ins where Pat_ID = '" + Pat_Name.Pat_ID + "'", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        sqlDr = sqlCmd.ExecuteReader();
        dtable.Load(sqlDr);
        con.Close();
        return dtable;
    }

    public DataTable getPatAllergies(PatientName Pat_Name)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select * from Patient_Allergies where Pat_ID = '" + Pat_Name.Pat_ID + "'", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        sqlDr = sqlCmd.ExecuteReader();
        dtable.Load(sqlDr);
        con.Close();
        return dtable;
    }

    public string Update_Patient(PatientName Pat_Name, PatientPersonalDetails Pat_Per_Details, PatientAddress Pat_Add,
                           PatientInsuranceDetails Pat_Ins_Details, PatientAllergies Pat_Allergies,
                           FacilityInfo Fac_Info, PharmacyInfo Phrm_Info,ProviderInfo Prov_Info)
    {

        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_update_Patient", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter P_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
        P_ID.Value = Pat_Name.Pat_ID; 

        SqlParameter P_MName = sqlCmd.Parameters.Add("@Pat_MName", SqlDbType.VarChar, 50);
        if (Pat_Name.MiddleName != null)
            P_MName.Value = Pat_Name.MiddleName;
        else
            P_MName.Value = Convert.DBNull;

        SqlParameter P_Gender = sqlCmd.Parameters.Add("@Pat_Gender", SqlDbType.Char, 1);
        if (Pat_Per_Details.Gender != null)
            P_Gender.Value = Pat_Per_Details.Gender;
        else
            P_Gender.Value = Convert.DBNull;

        SqlParameter P_DOB = sqlCmd.Parameters.Add("@Pat_DOB", SqlDbType.DateTime);
        if (Pat_Per_Details.DOB != null)
            P_DOB.Value = DateTime.Parse(Pat_Per_Details.DOB);
        else
        {
            Nullable<DateTime> NullDate = null;
            P_DOB.Value = NullDate;
        }

        SqlParameter P_SSN = sqlCmd.Parameters.Add("@Pat_SSN", SqlDbType.VarChar, 50);
        if (Pat_Per_Details.SSN != null)
            P_SSN.Value = Pat_Per_Details.SSN;
        else
            P_SSN.Value = Convert.DBNull;

        SqlParameter P_Address1 = sqlCmd.Parameters.Add("@Pat_Address1", SqlDbType.VarChar, 50);
        if (Pat_Add.Address1 != null)
            P_Address1.Value = Pat_Add.Address1;
        else
            P_Address1.Value = Convert.DBNull;

        SqlParameter P_Address2 = sqlCmd.Parameters.Add("@Pat_Address2", SqlDbType.VarChar, 50);
        if (Pat_Add.Address2 != null)
            P_Address2.Value = Pat_Add.Address2;
        else
            P_Address2.Value = Convert.DBNull;

        SqlParameter P_City = sqlCmd.Parameters.Add("@Pat_City", SqlDbType.VarChar, 50);
        if (Pat_Add.City != null)
            P_City.Value = Pat_Add.City;
        else
            P_City.Value = Convert.DBNull;
        SqlParameter P_State = sqlCmd.Parameters.Add("@Pat_State", SqlDbType.VarChar, 50);
        if (Pat_Add.State != null)
            P_State.Value = Pat_Add.State;
        else
            P_State.Value = Convert.DBNull;
        SqlParameter P_ZIP = sqlCmd.Parameters.Add("@Pat_Zip", SqlDbType.VarChar, 50);
        if (Pat_Add.Zip != null)
            P_ZIP.Value = Pat_Add.Zip;
        else
            P_ZIP.Value = Convert.DBNull;
        SqlParameter P_PDoc = sqlCmd.Parameters.Add("@Pat_PDoc", SqlDbType.VarChar, 50);
        if (Pat_Per_Details.Pat_Pre_Doc != null)
            P_PDoc.Value = Pat_Per_Details.Pat_Pre_Doc;
        else
            P_PDoc.Value = Convert.DBNull;

        SqlParameter P_MRN = sqlCmd.Parameters.Add("@Pat_MRN", SqlDbType.VarChar, 15);
        if (Pat_Per_Details.MRN != null)
            P_MRN.Value = Pat_Per_Details.MRN;
        else
            P_MRN.Value = Convert.DBNull;

        SqlParameter Fac_ID = sqlCmd.Parameters.Add("@Fac_ID", SqlDbType.Int);
        if (Fac_Info.FacilityNO != null)
            Fac_ID.Value = Fac_Info.FacilityNO;
        else
            Fac_ID.Value = Convert.DBNull;

        SqlParameter Phrm_ID = sqlCmd.Parameters.Add("@Phrm_ID", SqlDbType.Int);
        if (Phrm_Info.PhrmID != null)
            Phrm_ID.Value = Phrm_Info.PhrmID;
        else
            Phrm_ID.Value = Convert.DBNull;

        //SqlParameter PI_InsName = sqlCmd.Parameters.Add("@Ins_Name", SqlDbType.VarChar, 50);
        //if (Pat_Ins_Details.InsuranceName != null)
        //    PI_InsName.Value = Pat_Ins_Details.InsuranceName;
        //else
        //    PI_InsName.Value = Convert.DBNull;

        int InsID = getInsuranceID(Pat_Ins_Details);
        Pat_Ins_Details.InsuranceID = InsID;

        SqlParameter Ins_ID = sqlCmd.Parameters.Add("@Ins_ID", SqlDbType.Int);
        if (Pat_Ins_Details.InsuranceID.ToString() != "0")
            Ins_ID.Value = Pat_Ins_Details.InsuranceID;
        else
            Ins_ID.Value = Convert.DBNull;

        SqlParameter PI_No = sqlCmd.Parameters.Add("@PI_Number", SqlDbType.Int);
        if (Pat_Ins_Details.PI_PolicyNo.ToString() != "0")
            PI_No.Value = Pat_Ins_Details.PI_PolicyNo;
        else
            PI_No.Value = Convert.DBNull;

        SqlParameter PI_PolicyID = sqlCmd.Parameters.Add("@PI_PolicyID", SqlDbType.Int);
        if (Pat_Ins_Details.PI_PolicyID.ToString() != "0")
            PI_PolicyID.Value = Pat_Ins_Details.PI_PolicyID;
        else
            PI_PolicyID.Value = Convert.DBNull;

        SqlParameter PI_GroupNo = sqlCmd.Parameters.Add("@PI_GroupNo", SqlDbType.Int);
        if (Pat_Ins_Details.PI_GroupNo.ToString() != "0")
            PI_GroupNo.Value = Pat_Ins_Details.PI_GroupNo;
        else
            PI_GroupNo.Value = Convert.DBNull;

        SqlParameter PI_BINNo = sqlCmd.Parameters.Add("@PI_BINNo", SqlDbType.Int);
        if (Pat_Ins_Details.PI_BINNo.ToString() != "0")
            PI_BINNo.Value = Pat_Ins_Details.PI_BINNo;
        else
            PI_BINNo.Value = Convert.DBNull;

        SqlParameter PI_InsdName = sqlCmd.Parameters.Add("@PI_InsdName", SqlDbType.VarChar, 50);
        if (Pat_Ins_Details.InsuredName != null)
            PI_InsdName.Value = Pat_Ins_Details.InsuredName;
        else
            PI_InsdName.Value = Convert.DBNull;

        SqlParameter PI_InsdDOB = sqlCmd.Parameters.Add("@PI_InsdDOB", SqlDbType.DateTime);
        if (Pat_Ins_Details.InsuredDOB != null)
            PI_InsdDOB.Value = DateTime.Parse( Pat_Ins_Details.InsuredDOB);
        else
        {
            Nullable<DateTime> NullDate = null;
            PI_InsdDOB.Value = NullDate;
        }
        SqlParameter PI_InsdSSN = sqlCmd.Parameters.Add("@PI_InsdSSN", SqlDbType.VarChar, 50);
        if (Pat_Ins_Details.InsuredSSN != null)
            PI_InsdSSN.Value = Pat_Ins_Details.InsuredSSN;
        else
            PI_InsdSSN.Value = Convert.DBNull;
        SqlParameter PI_InsdRel = sqlCmd.Parameters.Add("@PI_InsdRel", SqlDbType.VarChar, 50);
        if (Pat_Ins_Details.InsuredRelation != null)
            PI_InsdRel.Value = Pat_Ins_Details.InsuredRelation;
        else
            PI_InsdRel.Value = Convert.DBNull;

        SqlParameter PA_Desc = sqlCmd.Parameters.Add("@PA_Desc", SqlDbType.VarChar, 200);
        if (Pat_Allergies.AllergyDescription != null)
            PA_Desc.Value = Pat_Allergies.AllergyDescription;
        else
            PA_Desc.Value = Convert.DBNull;
        
        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();    
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
        finally
        {
            sqlCon.Close();
        }

        return "Updated Patient Information Successfully";
    }

    public string Delete_Patient(PatientName Pat_Name)
    {

        SqlConnection sqlCon = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("sp_delete_Patient", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter P_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
        P_ID.Value = Pat_Name.Pat_ID;
        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
        finally
        {
            sqlCon.Close();
        }

        return "Deleted Patient Information Successfully";
    }

    public int getInsuranceID(PatientInsuranceDetails Pat_Ins_Details)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Ins_ID from Insurance_Info where Ins_Name = '" + Pat_Ins_Details.InsuranceName + "'", con);
       // SqlDataReader sqlDr;
       // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        int InsuranceID = Convert.ToInt32( sqlCmd.ExecuteScalar());
        con.Close();
        return InsuranceID;
    }

    public string getInsuranceName(PatientInsuranceDetails Pat_Ins_Details)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Ins_Name from Insurance_Info where Ins_ID = '" + Pat_Ins_Details.InsuranceID + "'", con);
        // SqlDataReader sqlDr;
        // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        string InsuranceName = sqlCmd.ExecuteScalar().ToString();
        con.Close();
        return InsuranceName;
    }

    public int getFacilityID(FacilityInfo facInfo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Facility_ID from Facility_Info where Facility_Name = '" + facInfo.FacilityName + "'", con);
        // SqlDataReader sqlDr;
        // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        int FacilityID = Convert.ToInt32(sqlCmd.ExecuteScalar());
        con.Close();
        return FacilityID;
    }

    public string getFacilityName(FacilityInfo facInfo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Facility_Name from Facility_Info where Facility_ID = '" + facInfo.FacilityNO + "'", con);
        // SqlDataReader sqlDr;
        // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        string FacilityName = sqlCmd.ExecuteScalar().ToString();
        con.Close();
        return FacilityName;
    }

    public int getPharmacyID(PharmacyInfo phrmInfo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Phrm_ID from Pharmacy_Info where Phrm_Name = '" + phrmInfo.PhrmName + "'", con);
        // SqlDataReader sqlDr;
        // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        int FacilityID = Convert.ToInt32(sqlCmd.ExecuteScalar());
        con.Close();
        return FacilityID;
    }

    public string getPharmacyName(PharmacyInfo phrmInfo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Phrm_Name from Pharmacy_Info where Phrm_ID = '" + phrmInfo.PhrmID + "'", con);
        // SqlDataReader sqlDr;
        // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        string Phrm_Name = sqlCmd.ExecuteScalar().ToString();
        con.Close();
        return Phrm_Name;
    }

    public int getDOCID(ProviderInfo pInfo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Doc_ID from Doctor_Info where Status<>'N' and Doc_FullName = '" + pInfo.FullName + "'", con);
        // SqlDataReader sqlDr;
        // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        int DOCID = Convert.ToInt32(sqlCmd.ExecuteScalar());
        con.Close();
        return DOCID;
    }

    public string getDOCName(ProviderInfo pInfo)
    {
        SqlConnection con = new SqlConnection(ConStr);
        SqlCommand sqlCmd = new SqlCommand("select Doc_FullName from Doctor_Info where Status<>'N' and Doc_ID = '" + pInfo.Prov_ID + "'", con);
        // SqlDataReader sqlDr;
        // DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        string DOC_Name = sqlCmd.ExecuteScalar().ToString();
        con.Close();
        return DOC_Name;
    }
}
