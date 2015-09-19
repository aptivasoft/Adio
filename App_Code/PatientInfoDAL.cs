using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using NLog;
using Adio.UALog;

/// <summary>
/// Summary description for PatientInfoDAL
/// </summary>
public class PatientInfoDAL
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
	
    public PatientInfoDAL()
	{ }

    public DataTable get_Patients(string prefixText,int count)
    {
        objNLog.Info("Function Started with prefixText,count as arguments...");
        DataSet dsPatient_Names = new DataSet();
        bool successFlag = false;
            SqlConnection sqlCon = new SqlConnection(conStr);
            string pat_Fname;
            string pat_Lname;
            string sqlQuery;
            try
            {
                if (prefixText.Contains(","))
                {
                    string[] pat_Name = prefixText.Split(',');

                    pat_Lname = pat_Name[0].ToString();
                    pat_Fname = pat_Name[1].TrimStart().TrimEnd().ToString();

                    sqlQuery = "select * from Patient_Info where Pat_LName like '" + pat_Lname + "' and Pat_FName like '" + pat_Fname + "%'";
                }
                else
                    sqlQuery = "select * from Patient_Info where Pat_LName like '" + prefixText + "%'";

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);


                sqlDa.Fill(dsPatient_Names, "Patients");
                successFlag = true;
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Searching  Patient Names.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Searching  Patient Names.", ex);
            }
            finally
            {
                objNLog.Info("Finally Block: " + successFlag);
            }

            objNLog.Info("Function Completed...");
                return dsPatient_Names.Tables[0];
            }
  
    public DataTable get_Patient_Details(string patID)
    {
        objNLog.Info("Function Started with patID as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        
        DataSet  dsPatient = new DataSet();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getPatientInfo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_patID = sqlCmd.Parameters.Add("@pat_ID", SqlDbType.Int);
            sp_patID.Value = Int32.Parse(patID);

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsPatient, "patDetails");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving  Patient Details.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving  Patient Details.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsPatient.Tables["patDetails"];
    }

    public DataTable get_FacilityNames(string prefixText, int count, string contextKey)
    {
        objNLog.Info("Function Started with prefixText,count,contextKey as arguments...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsFacNames = new DataSet();
        try
        {
            string sqlQuery = "select * from Facility_Info where Facility_Name like '" + prefixText + "%'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(dsFacNames, "FacNames");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Facility Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Facility Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsFacNames.Tables[0];
    }

    public DataTable get_DrugRxItemID(string prefixText)
    {
        objNLog.Info("Function Started with prefixText as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
       
        DataSet DrugName = new DataSet();
        try
        {
            string sqlQuery = "select Rx_ItemID from Rx_Drug_Info where Rx_DrugName = '" + prefixText + "'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(DrugName, "DrugName");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Drug  RxItemID.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Drug  RxItemID.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return DrugName.Tables["DrugName"];
    }

    public DataTable get_DrugName(string prefixText)
    {
        objNLog.Info("Function Started with prefixText as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet DrugName = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Rx_DrugName,Rx_ItemID from Rx_Drug_Info where Rx_DrugName like '" + prefixText + "%'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(DrugName, "DrugName");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Drug Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Drug Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return DrugName.Tables["DrugName"];
    }

    public DataTable get_DoctorNames(string prefixText)
    {
        objNLog.Info("Function Started with prefixText as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsdocnames = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Doc_FName,Doc_LName,Doc_ID from Doctor_Info where Status<>'N' and UserType='D' and Doc_LName like '" + prefixText + "%'";
            //objNLog.Error("DocQuery : " + sqlQuery);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(dsdocnames, "Doc_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Doctor Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Doctor Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Doc_Name"];
    }

    public DataTable get_NurseNames(string prefixText)
    {
        objNLog.Info("Function Started with prefixText as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        DataSet dsdocnames = new DataSet();
        try
        {
            string sqlQuery = "select Doc_FName,Doc_LName,Doc_ID from Doctor_Info where Status<>'N' and UserType='N' and Doc_LName like '" + prefixText + "%'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(dsdocnames, "Doc_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Nurse Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Nurse Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Doc_Name"];
    }

    public DataTable get_DoctorNames(string prefixText, string contextKey)
    {
        objNLog.Info("Function Started with prefixText, contextKey as arguments...");
        DataTable doc_names=new DataTable ();
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsdocnames = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Doc_FName,Doc_LName,Doc_ID from Doctor_Info where Status<>'N' and UserType='D' and  Clinic_ID=" + contextKey + " and Doc_LName like '" + prefixText + "%'";

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);

            sqlDa.Fill(dsdocnames, "Doc_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Doctor Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Doctor Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Doc_Name"];
    }

    public DataTable get_PatDoctorNames(string prefixText, string contextKey)
    {
        objNLog.Info("Function Started with prefixText, contextKey as arguments...");
        DataSet dsdocnames = new DataSet();
        bool successFlag = false;
        try
        {
            DataTable doc_names = new DataTable();
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getDoctorNames", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlParm_searchText = sqlCmd.Parameters.Add("@SearchText",SqlDbType.VarChar,50);
            sqlParm_searchText.Value = prefixText;
            SqlParameter sqlParm_contextKey = sqlCmd.Parameters.Add("@ContextKey", SqlDbType.Int);
            sqlParm_contextKey.Value = Int32.Parse(contextKey);
            //string sqlQuery = "select Doctor_Info.Doc_FName,Doctor_Info.Doc_LName,Doctor_Info.Doc_ID from Doctor_Info,Patient_Info where Doctor_Info.Status<>'N' and Doctor_Info.Doc_LName like '" + prefixText + "%' and Patient_Info.Facility_ID in (select Facility_Info.Facility_ID from Facility_Info where Facility_Info.Clinic_ID=Doctor_Info.Clinic_ID) and Patient_Info.Pat_ID=" + contextKey;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsdocnames, "Doc_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Doctor Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Doctor Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Doc_Name"];
    }

    public DataTable get_AllDoctors(string prefixText)
    {
        objNLog.Info("Function Started with prefixText, contextKey as arguments...");
        DataSet dsdocnames = new DataSet();
        bool successFlag = false;
        try
        {
            DataTable doc_names = new DataTable();
            SqlConnection sqlCon = new SqlConnection(conStr);
            //SqlCommand sqlCmd = new SqlCommand("sp_getDoctors", sqlCon);
            //sqlCmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter sqlParm_searchText = sqlCmd.Parameters.Add("@SearchText", SqlDbType.VarChar, 50);
            //sqlParm_searchText.Value = prefixText;
           
            string sqlQuery = "select Doctor_Info.Doc_FName,Doctor_Info.Doc_LName,Doctor_Info.Doc_ID from Doctor_Info  where Doctor_Info.Doc_LName like '" + prefixText + "%'";

            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);                     
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dsdocnames, "Doc_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Doctor Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Doctor Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Doc_Name"];
    }


    public DataRow get_DoctorNames(PatientPersonalDetails DocFullName)
    {
        objNLog.Info("Function Started with DocFullName as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        
        DataSet dsdocnames = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Doc_FName,Doc_LName,Doc_ID from Doctor_Info  where Status<>'N' and Doc_FullName like  '%" + DocFullName.DocFullName + "%'";
            //objNLog.Error("DocQuery3 : " + sqlQuery);

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(dsdocnames, "Doc_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Doctor Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Doctor Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Doc_Name"].Rows[0];
    }

    public DataTable get_ClinicFacilityNames(string prefixText)
    {
        objNLog.Info("Function Started with prefixText as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        
        DataSet dsdocnames = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Facility_Name,Facility_ID,Clinic_ID from Facility_Info where Facility_Name like '" + prefixText + "%'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(dsdocnames, "Facility_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Clinic's Facility Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Clinic's Facility Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Facility_Name"];
    }

    public DataTable get_ClinicFacilityNames(string prefixText, string contextKey)
    {
        objNLog.Info("Function Started with prefixText,contextKey as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsdocnames = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Facility_Name,Facility_ID,Clinic_ID from Facility_Info where Clinic_ID=" + contextKey + " and  Facility_Name like '" + prefixText + "%'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(dsdocnames, "Facility_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Clinic's Facility Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Clinic's Facility Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsdocnames.Tables["Facility_Name"];
    }

    public DataTable get_MedicationNames(string prefixText)
    {
        objNLog.Info("Function Started with prefixText as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsMednames = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Drug_Name,Drug_ID from Drug_Info where Drug_Name like '" + prefixText + "%'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            sqlDa.Fill(dsMednames, "Drug_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Medication Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Medication Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsMednames.Tables["Drug_Name"];
    }

    public DataTable get_SIGNames(string prefixText)
    {
        objNLog.Info("Function Started with prefixText as argument...");
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet SIG_Codes = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select * from SIG_Codes where SIG_Name like '" + prefixText + "%'";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
            
            sqlDa.Fill(SIG_Codes, "SIG_Codes");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving SIG Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving SIG Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return SIG_Codes.Tables["SIG_Codes"];
    }

    public DataTable get_PatientNames(string prefixText, string UserRole, string contextKey)
    {
        objNLog.Info("Function Started with prefixText,UserRole,contextKey as arguments...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsClincNames = new DataSet();
        bool successFlag = false; 
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getPatients", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_UserID.Value = contextKey;

            SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
            sp_UserRole.Value = UserRole;

            if (prefixText.IndexOf(',') == -1)
            {
                SqlParameter sp_Pat_Lname = sqlCmd.Parameters.Add("@Lname", SqlDbType.VarChar, 50);
                sp_Pat_Lname.Value = prefixText;
            }
            else
            {
                string[] patname = prefixText.Split(',');

                SqlParameter sp_Pat_Lname = sqlCmd.Parameters.Add("@Lname", SqlDbType.VarChar, 50);
                sp_Pat_Lname.Value = patname[0];
                SqlParameter sp_Pat_Fname = sqlCmd.Parameters.Add("@Fname", SqlDbType.VarChar, 50);
                sp_Pat_Fname.Value = patname[1];
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsClincNames, "Patient_Name");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsClincNames.Tables[0];
    }

    public DataSet get_Patient_Details()
    {
        objNLog.Info("Function Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        DataSet dsPatient = new DataSet();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("select * from Patient_Info", sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsPatient, "patDetails");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Info.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsPatient;
    }

    public DataRow Get_Docname(int DocID)
    {
        objNLog.Info("Function Started with DocID as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsDocName = new DataSet();
        bool successFlag = false;
        try
        {
        SqlCommand sqlCmd = new SqlCommand("sp_getDoctor", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_DocId = sqlCmd.Parameters.Add("@Doc_Id", SqlDbType.Int);
        sp_DocId.Value = DocID;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        
        sqlDa.Fill(dsDocName, "DocName");
        successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving doctor Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving doctor Info.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsDocName.Tables["DocName"].Rows[0];
    
    }

    public DataRow Get_Facility(int FacID)
    {
        objNLog.Info("Function Started with FacID as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsFacName = new DataSet();
        bool successFlag = false;
        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getFacility", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_FacId = sqlCmd.Parameters.Add("@Facility_Id", SqlDbType.Int);
            sp_FacId.Value = FacID;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dsFacName, "FacName");

            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " Retrieving Facility Info. ", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Facility Info.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsFacName.Tables["FacName"].Rows[0];
    }

    public DataTable get_Patient_Details(string patFName,string patLName)
    {
        objNLog.Info("Function Started with patFName,patLName as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsPatient = new DataSet();
        bool successFlag = false;
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getPatientInfoByName", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_patFN = sqlCmd.Parameters.Add("@pat_FName", SqlDbType.VarChar);
            sp_patFN.Value = patFName;

            SqlParameter sp_patLN = sqlCmd.Parameters.Add("@Pat_LName", SqlDbType.VarChar);
            sp_patLN.Value = patLName;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsPatient, "patDetails");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Info. ", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Info.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsPatient.Tables["patDetails"];
    }

    public DataTable get_DrugInfo(PatientPersonalDetails Pat_Det)
    {
        objNLog.Info("Function Started with Pat_Det as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet ds = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQry = "sp_getDrugInfo";
            SqlCommand cmd = new SqlCommand(sqlQry);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlCon;
            SqlParameter par_RxID = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            par_RxID.Value = Pat_Det.Pat_ID;
          
            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            sqlDa.Fill(ds, "Pat_DrugInfo");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Drug Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Drug Info.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return ds.Tables["Pat_DrugInfo"];
    }
 
    public string set_PatientInfo(string userID,PatientName Pat_Name, PatientPersonalDetails Pat_Info)
    {
        objNLog.Info("Function Started with Pat_Info, Pat_Name as argument...");
        bool successFlag = false;
        string pID = "0";
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_set_PatientInfo";
            cmd.Connection = sqlCon;

            SqlParameter sql_FacId = cmd.Parameters.Add("@FacId", SqlDbType.Int);
            sql_FacId.Value = Pat_Info.FacID;

            SqlParameter sql_PatMRN = cmd.Parameters.Add("@Pat_MRN", SqlDbType.VarChar, 15);
            sql_PatMRN.Value = Pat_Info.MRN;

            SqlParameter sql_PatFname = cmd.Parameters.Add("@Pat_Fname", SqlDbType.VarChar, 50);
            sql_PatFname.Value = Pat_Name.FirstName;

            SqlParameter sql_PatMname = cmd.Parameters.Add("@Pat_Mname", SqlDbType.VarChar, 50);
            sql_PatMname.Value = Pat_Name.MiddleName;

            SqlParameter sql_PatLname = cmd.Parameters.Add("@Pat_Lname", SqlDbType.VarChar, 50);
            sql_PatLname.Value = Pat_Name.LastName;

            SqlParameter sql_PatGender = cmd.Parameters.Add("@Pat_Gender", SqlDbType.Char, 1);
            sql_PatGender.Value = Pat_Info.Gender;

            SqlParameter sql_PatDob = cmd.Parameters.Add("@Pat_DOB", SqlDbType.DateTime2, 7);
            sql_PatDob.Value = Convert.ToDateTime(Pat_Info.DOB);

            SqlParameter sql_PatSSN = cmd.Parameters.Add("@Pat_SSN", SqlDbType.VarChar, 16);
            sql_PatSSN.Value = Pat_Info.SSN;

            SqlParameter sql_PatAdd1 = cmd.Parameters.Add("@Pat_Address1", SqlDbType.VarChar, 50);
            sql_PatAdd1.Value = Pat_Info.PatientAddress1;

            SqlParameter sql_PatAdd2 = cmd.Parameters.Add("@Pat_Address2", SqlDbType.VarChar, 50);
            sql_PatAdd2.Value = Pat_Info.PatientAddress2;

            SqlParameter sql_PatCity = cmd.Parameters.Add("@Pat_City", SqlDbType.VarChar, 50);
            sql_PatCity.Value = Pat_Info.Pat_City;

            SqlParameter sql_PatState = cmd.Parameters.Add("@Pat_State", SqlDbType.VarChar, 14);
            sql_PatState.Value = Pat_Info.Pat_state;

            SqlParameter sql_PatZip = cmd.Parameters.Add("@Pat_Zip", SqlDbType.VarChar, 10);
            sql_PatZip.Value = Pat_Info.Pat_ZIP;

            SqlParameter sql_PatSAdd1 = cmd.Parameters.Add("@Pat_SAddress1", SqlDbType.VarChar, 50);
            sql_PatSAdd1.Value = Pat_Info.PatientShipAddress1;

            SqlParameter sql_PatSAdd2 = cmd.Parameters.Add("@Pat_SAddress2", SqlDbType.VarChar, 50);
            sql_PatSAdd2.Value = Pat_Info.PatientShipAddress2;

            SqlParameter sql_PatSCity = cmd.Parameters.Add("@Pat_SCity", SqlDbType.VarChar, 50);
            sql_PatSCity.Value = Pat_Info.Pat_SCity;

            SqlParameter sql_PatSState = cmd.Parameters.Add("@Pat_SState", SqlDbType.VarChar, 14);
            sql_PatSState.Value = Pat_Info.Pat_Sstate;

            SqlParameter sql_PatSZip = cmd.Parameters.Add("@Pat_SZip", SqlDbType.VarChar, 10);
            sql_PatSZip.Value = Pat_Info.Pat_SZIP;

            SqlParameter sql_PatPhone = cmd.Parameters.Add("@Pat_Phone", SqlDbType.VarChar, 16);
            sql_PatPhone.Value = Pat_Info.Pat_Phone;//@Pat_WPhone

            SqlParameter sql_PatCPhone = cmd.Parameters.Add("@Pat_CPhone", SqlDbType.VarChar, 16);
            sql_PatCPhone.Value = Pat_Info.Pat_CellPhone;

            SqlParameter sql_PatWPhone = cmd.Parameters.Add("@Pat_WPhone", SqlDbType.VarChar, 16);
            sql_PatWPhone.Value = Pat_Info.Pat_WPhone;

            SqlParameter sql_patDoc = cmd.Parameters.Add("@Pat_PDoc", SqlDbType.VarChar, 50);
            sql_patDoc.Value = Pat_Info.Pat_Pre_Doc;

            SqlParameter sql_AutoFill = cmd.Parameters.Add("@AutoFill", SqlDbType.Char, 1);
            sql_AutoFill.Value = Pat_Info.Pat_AutoFill;

            SqlParameter sql_DocId = cmd.Parameters.Add("@DocId", SqlDbType.Int);
            sql_DocId.Value = Pat_Info.DocID;

            SqlParameter sql_PatPrmIns = cmd.Parameters.Add("@Pat_PrimInsu_ID", SqlDbType.Int);
            if (Pat_Info.PrimaryInsID != 0)
                sql_PatPrmIns.Value = Pat_Info.PrimaryInsID;

            SqlParameter par_DiagnnosisCode = cmd.Parameters.Add("@DiagnnosisCode", SqlDbType.VarChar, 255);

            par_DiagnnosisCode.Value = Pat_Info.Pat_Diagnosis;

            SqlParameter sql_HIPPA_Notice = cmd.Parameters.Add("@HIPPA", SqlDbType.Char, 1);
            sql_HIPPA_Notice.Value = Pat_Info.HIPPANotice;
            SqlParameter sql_HIPPA_Date = cmd.Parameters.Add("@HIPPADate", SqlDbType.Date);
            if (Pat_Info.HIPPANotice == "Y")
                sql_HIPPA_Date.Value = Pat_Info.HIPPADate;

            SqlParameter sql_userid = cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            sql_userid.Value = userID;

            SqlParameter sql_ecFNAME = cmd.Parameters.Add("@ecFNAME", SqlDbType.VarChar, 20);
            sql_ecFNAME.Value = Pat_Info.eContactFName;

            SqlParameter sql_ecLNAME = cmd.Parameters.Add("@ecLNAME", SqlDbType.VarChar, 20);
            sql_ecLNAME.Value = Pat_Info.eContactLName;
            SqlParameter sql_ecPHONE = cmd.Parameters.Add("@ecPHONE", SqlDbType.VarChar, 50);
            sql_ecPHONE.Value = Pat_Info.eContactPhone;
            SqlParameter sql_ecREL = cmd.Parameters.Add("@ecREL", SqlDbType.Char, 1);
            sql_ecREL.Value = Pat_Info.eContactRelation;


            SqlParameter sql_IsActive = cmd.Parameters.Add("@Pat_Status", SqlDbType.Char, 1);
            sql_IsActive.Value = Pat_Info.PatientStatus;

            SqlParameter sql_Pat_ID = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            sql_Pat_ID.Direction = ParameterDirection.Output;
        
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            pID = sql_Pat_ID.Value.ToString();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Created New Patient Profile. with PatID = " + pID, "Patient_Info",Int32.Parse((string)pID));
        
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting Patient Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting Patient Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return pID;
    }

    public void update_patInfo(string userID,PatientPersonalDetails Pat_Info, PatientName Pat_Name)
    {
        objNLog.Info("Function Started with Pat_Info, Pat_Name as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "sp_update_PatientInfo";
        cmd.Connection = sqlCon;

        SqlParameter sql_FacId = cmd.Parameters.Add("@FacId", SqlDbType.Int);
        sql_FacId.Value = Pat_Info.FacID;

        SqlParameter sql_PatMRN = cmd.Parameters.Add("@Pat_MRN", SqlDbType.VarChar,15);
        sql_PatMRN.Value = Pat_Info.MRN;

        SqlParameter sql_PatFname = cmd.Parameters.Add("@Pat_Fname", SqlDbType.VarChar, 50);
        sql_PatFname.Value = Pat_Name.FirstName;

        SqlParameter sql_PatMname = cmd.Parameters.Add("@Pat_Mname", SqlDbType.VarChar, 50);
        sql_PatMname.Value = Pat_Name.MiddleName;

        SqlParameter sql_PatLname = cmd.Parameters.Add("@Pat_Lname", SqlDbType.VarChar, 50);
        sql_PatLname.Value = Pat_Name.LastName;

        SqlParameter sql_PatGender = cmd.Parameters.Add("@Pat_Gender", SqlDbType.Char, 1);
        sql_PatGender.Value = Pat_Info.Gender;

        SqlParameter sql_PatDob = cmd.Parameters.Add("@Pat_DOB", SqlDbType.DateTime2, 7);
        sql_PatDob.Value = Convert.ToDateTime(Pat_Info.DOB);

        SqlParameter sql_PatSSN = cmd.Parameters.Add("@Pat_SSN", SqlDbType.VarChar, 16);
        sql_PatSSN.Value = Pat_Info.SSN;

        SqlParameter sql_PatAdd1 = cmd.Parameters.Add("@Pat_Address1", SqlDbType.VarChar, 50);
        sql_PatAdd1.Value = Pat_Info.PatientAddress1;

        SqlParameter sql_PatAdd2 = cmd.Parameters.Add("@Pat_Address2", SqlDbType.VarChar, 50);
        sql_PatAdd2.Value = Pat_Info.PatientAddress2;

        SqlParameter sql_PatCity = cmd.Parameters.Add("@Pat_City", SqlDbType.VarChar, 50);
        sql_PatCity.Value = Pat_Info.Pat_City ;

        SqlParameter sql_PatState = cmd.Parameters.Add("@Pat_State", SqlDbType.VarChar, 14);
        sql_PatState.Value = Pat_Info.Pat_state;

        SqlParameter sql_PatZip = cmd.Parameters.Add("@Pat_Zip", SqlDbType.VarChar, 10);
        sql_PatZip.Value = Pat_Info.Pat_ZIP;

        SqlParameter sql_PatSAdd1 = cmd.Parameters.Add("@Pat_SAddress1", SqlDbType.VarChar, 50);
        sql_PatSAdd1.Value = Pat_Info.PatientShipAddress1;

        SqlParameter sql_PatSAdd2 = cmd.Parameters.Add("@Pat_SAddress2", SqlDbType.VarChar, 50);
        sql_PatSAdd2.Value = Pat_Info.PatientShipAddress2;

        SqlParameter sql_PatSCity = cmd.Parameters.Add("@Pat_SCity", SqlDbType.VarChar, 50);
        sql_PatSCity.Value = Pat_Info.Pat_SCity;

        SqlParameter sql_PatSState = cmd.Parameters.Add("@Pat_SState", SqlDbType.VarChar, 14);
        sql_PatSState.Value = Pat_Info.Pat_Sstate;

        SqlParameter sql_PatSZip = cmd.Parameters.Add("@Pat_SZip", SqlDbType.VarChar, 10);
        sql_PatSZip.Value = Pat_Info.Pat_SZIP;

        SqlParameter sql_PatPhone = cmd.Parameters.Add("@Pat_Phone", SqlDbType.VarChar, 16);
        sql_PatPhone.Value = Pat_Info.Pat_Phone;//@Pat_WPhone

        SqlParameter sql_PatCPhone = cmd.Parameters.Add("@Pat_CPhone", SqlDbType.VarChar, 16);
        sql_PatCPhone.Value = Pat_Info.Pat_CellPhone;

        SqlParameter sql_PatWPhone = cmd.Parameters.Add("@Pat_WPhone", SqlDbType.VarChar, 16);
        sql_PatWPhone.Value = Pat_Info.Pat_WPhone;

        SqlParameter sql_patDoc = cmd.Parameters.Add("@Pat_PDoc", SqlDbType.VarChar, 50);
        sql_patDoc.Value = Pat_Info.Pat_Pre_Doc;

        SqlParameter sql_AutoFill = cmd.Parameters.Add("@AutoFill", SqlDbType.Char, 1);
        sql_AutoFill.Value = Pat_Info.Pat_AutoFill;

        SqlParameter sql_DocId = cmd.Parameters.Add("@DocId", SqlDbType.Int);
        sql_DocId.Value = Pat_Info.DocID;

        SqlParameter sql_PatPrmIns = cmd.Parameters.Add("@Pat_PrimInsu_ID", SqlDbType.Int);
        if(Pat_Info.PrimaryInsID!=0)
            sql_PatPrmIns.Value = Pat_Info.PrimaryInsID;

        SqlParameter par_DiagnnosisCode = cmd.Parameters.Add("@DiagnnosisCode", SqlDbType.VarChar, 255);

        par_DiagnnosisCode.Value = Pat_Info.Pat_Diagnosis;

        SqlParameter par_Patid = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
        if (Pat_Info.Pat_ID != null)
            par_Patid.Value = Pat_Info.Pat_ID;
            
        //SqlParameter sql_Pharmacy = cmd.Parameters.Add("@Pharmacy", SqlDbType.VarChar, 255);
        //sql_Pharmacy.Value = Pat_Info.PhacyName;
        SqlParameter sql_userid = cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
        sql_userid.Value = userID;

        SqlParameter sql_HIPPA_Notice = cmd.Parameters.Add("@HIPPA", SqlDbType.Char, 1);
        sql_HIPPA_Notice.Value = Pat_Info.HIPPANotice ;
        SqlParameter sql_HIPPA_Date = cmd.Parameters.Add("@HIPPADate", SqlDbType.Date , 7);
        if(Pat_Info.HIPPANotice=="Y")
            sql_HIPPA_Date.Value = Pat_Info.HIPPADate;

        SqlParameter sql_ecFNAME = cmd.Parameters.Add("@ecFNAME", SqlDbType.VarChar, 20);
        sql_ecFNAME.Value = Pat_Info.eContactFName;

        SqlParameter sql_ecLNAME = cmd.Parameters.Add("@ecLNAME", SqlDbType.VarChar, 20);
        sql_ecLNAME.Value = Pat_Info.eContactLName;
        SqlParameter sql_ecPHONE = cmd.Parameters.Add("@ecPHONE", SqlDbType.VarChar, 50);
        sql_ecPHONE.Value = Pat_Info.eContactPhone;
        SqlParameter sql_ecREL = cmd.Parameters.Add("@ecREL", SqlDbType.Char, 1);
        sql_ecREL.Value = Pat_Info.eContactRelation;

        SqlParameter sql_IsActive = cmd.Parameters.Add("@Pat_Status", SqlDbType.Char, 1);
        sql_IsActive.Value = Pat_Info.PatientStatus;

            sqlCon.Open();
            cmd.ExecuteNonQuery();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Updated Patient Profile Info. with PatID = " + Pat_Info.Pat_ID.ToString(), "Patient_Info", Pat_Info.Pat_ID );
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");     
    }
   
    public DataTable update_PatientInfo()
    {
        objNLog.Info("Function Started with as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsPatient = new DataSet();
        try
        {
            string sqlQuery = "select Patient_Med_History.RxDate,Patient_Med_History.Medicine,Patient_Med_History.Qty,Patient_Med_History.SIG,Patient_Med_History.Refills from patient_Med_History ";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            
            da.Fill(dsPatient, "Med_His");
            da.Fill(dsPatient, "patMedDetails");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsPatient.Tables["patMedDetails"];
    }

    public void update_patMedInfo(PatinetMedHistoryInfo Pat_med) //Updates the medical Inforamation
    {
        objNLog.Info("Function Started with Pat_med as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            string sqlQuery = "update Patient_Med_History set Qty=" + Pat_med.Quantity + ",SIG='" + Pat_med.SIG + "',Refills=" + Pat_med.Refills + " where CM_ID=" + Pat_med.CM_ID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Med Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Med Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
    }

    public void delete_patMedInfo(string CMID) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function Started with CMID as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            string sqlQuery = "Delete Patient_Med_History  where CM_ID=" + CMID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Med Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Med Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
    }
     
    public int Patient_Exist(string FName,string LName,string Gender,string DOB,string SSN)
    {
        objNLog.Info("Function Started with Fname,LName,Gender,DOB,SSN as argument...");
        bool successFlag = false;
        int flag = 0;
            SqlConnection sqlCon = new SqlConnection(conStr);
            try
            {
                //DataSet ds = new DataSet();
                //SqlDataAdapter da = new SqlDataAdapter("select count(pat_id) from Patient_Info where "
                //                                        + " replace(Pat_SSN,'-','')='" + SSN.Trim() + "' or (Pat_FName='" + Fname.Trim() + "' and Pat_LName='" + LName.Trim() + "' and"
                //                                        + " Pat_Gender = '" + Gender.Trim() + "' and convert(date,Pat_DOB,0)=convert(date,'" + DOB.Trim() + "',0))", sqlCon);
                //da.Fill(ds, "PatID");
                //if (ds.Tables["PatID"].Rows.Count > 0)
                //    flag = int.Parse(ds.Tables["PatID"].Rows[0][0].ToString());
                //else
                //   flag=0;

                SqlCommand sqlCmd = new SqlCommand("sp_IsPatientExists", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_Pat_FName = sqlCmd.Parameters.Add("@Pat_FName", SqlDbType.VarChar, 50);
                if (FName != "")
                    sp_Pat_FName.Value = FName.Trim();
                else
                    sp_Pat_FName.Value = System.DBNull.Value;

                SqlParameter sp_Pat_LName = sqlCmd.Parameters.Add("@Pat_LName", SqlDbType.VarChar, 50);
                if (LName != "")
                    sp_Pat_LName.Value = LName.Trim();
                else
                    sp_Pat_LName.Value = System.DBNull.Value;

                SqlParameter sp_Pat_Gender = sqlCmd.Parameters.Add("@Pat_Gender", SqlDbType.Char, 1);
                if (Gender != "")
                    sp_Pat_Gender.Value = char.Parse(Gender);
                else
                    sp_Pat_Gender.Value = System.DBNull.Value;

                SqlParameter sp_Pat_DOB = sqlCmd.Parameters.Add("@Pat_DOB", SqlDbType.DateTime);
                if (DOB != "")
                    sp_Pat_DOB.Value = DOB;
                else
                    sp_Pat_DOB.Value = System.DBNull.Value;

                SqlParameter sp_Pat_SSN = sqlCmd.Parameters.Add("@Pat_SSN", SqlDbType.VarChar, 16);
                if (SSN != "")
                    sp_Pat_SSN.Value = SSN.Trim();
                else
                    sp_Pat_SSN.Value = System.DBNull.Value;

                sqlCon.Open();
                flag = int.Parse(sqlCmd.ExecuteScalar().ToString());
                

                successFlag = true;
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while checking Patient exist or not.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while checking Patient exist or not.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }

            objNLog.Info("Function Completed...");
            return flag;

    }

    public int Get_DocID(string DocName)
    {
        objNLog.Info("Function Started with DocName as argument...");
        bool successFlag = false;
        int flag = 0;
        try
        {
            string[] Dnames = DocName.Split(',');
            if (Dnames.Length == 2)
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select Doc_ID from Doctor_Info where Status<>'N' and Doc_FName='" + Dnames[1] + "' and Doc_LName='" + Dnames[0] + "'", sqlCon);
                da.Fill(ds, "DocID");
                if (ds.Tables["DocID"].Rows.Count > 0)
                    flag = int.Parse(ds.Tables["DocID"].Rows[0][0].ToString());
                else
                    flag = 0;
            }
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Doctor ID.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Doctor ID.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return flag;
    }

    public int Get_MedID(string MedName)
    {
        objNLog.Info("Function Started with DocName as argument...");
        bool successFlag = false;
        int flag = 0;
        try
        {
                        
                SqlConnection sqlCon = new SqlConnection(conStr);
                string sqlQuery = "select Drug_ID from Drug_Info where Drug_Name like '" + MedName + "%'";
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
                DataSet ds = new DataSet();
                sqlDa.Fill(ds, "DrugID");
                if (ds.Tables["DrugID"].Rows.Count > 0)
                    flag = int.Parse(ds.Tables["DrugID"].Rows[0][0].ToString());
                else
                    flag = 0;
                successFlag = true;
  
                   
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Doctor ID.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Doctor ID.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return flag;
    }

    public int Get_FacID(string FacName)
    {
        objNLog.Info("Function Started with InsName as argument...");
        bool successFlag = false;
        int flag = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("select Facility_ID from Facility_Info where Facility_Name='" + FacName + "'", sqlCon);
            da.Fill(ds, "FacID");
            if (ds.Tables["FacID"].Rows.Count > 0)
                flag = int.Parse(ds.Tables["FacID"].Rows[0][0].ToString());
            else
                flag = 0;
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Facility ID.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Facility ID.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return flag;
    }

    public int Get_InsuranceID(string InsName)
    {
        objNLog.Info("Function Started with InsName as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet ds = new DataSet();
        int flag = 0;
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("select Ins_ID from Insurance_Info where Ins_Name='" + InsName + "'", sqlCon);
            da.Fill(ds, "InsID");
            if (ds.Tables["InsID"].Rows.Count > 0)
                flag= int.Parse(ds.Tables["InsID"].Rows[0][0].ToString());
            else
               flag= 0;
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Insurance ID.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Insurance ID.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return flag;
    }

    public void set_newMedication(string userID, string DrugName)
    {
        objNLog.Info("Function Started with DrugName as argument...");
        bool successFlag = false;

        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "sp_set_DrugInfo";
        cmd.Connection = sqlCon;

        SqlParameter sql_DrugName = cmd.Parameters.Add("@DName", SqlDbType.VarChar,50);
        sql_DrugName.Value = DrugName;

        SqlParameter sql_UserID = cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 15);
        sql_UserID.Value = userID;

        SqlParameter sql_DrugTypeID = cmd.Parameters.Add("@DType_ID", SqlDbType.Int);
        sql_DrugTypeID.Value = 1;

        SqlParameter sql_DCostIndex = cmd.Parameters.Add("@DCostIndex", SqlDbType.Char, 1);
        sql_DCostIndex.Value = '0';

        
        // SqlCommand cmd = new SqlCommand("insert into Drug_Info(Drug_Info.Drug_Name, DrugType_ID, Drug_Cost_Index, LastModified, LastModifiedBy) values('"+DrugName+"',1,'0','"+userID+"')", sqlCon);
        try
        {
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting New Med Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting New Med Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    public void set_newPatMedication(PatinetMedHistoryInfo Pat_MedInfo)
    {
        objNLog.Info("Function Started with Pat_MedInfo");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            SqlCommand cmd = new SqlCommand("sp_set_PatientMedicalInfo", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_Medicine = cmd.Parameters.Add("@Medicine", SqlDbType.VarChar, 50);
            par_Medicine.Value = Pat_MedInfo.Madicine;

            SqlParameter par_Quantity = cmd.Parameters.Add("@Quantity", SqlDbType.Int);
            par_Quantity.Value = Pat_MedInfo.Quantity;

            SqlParameter par_SIG = cmd.Parameters.Add("@SIG", SqlDbType.VarChar, 50);
            par_SIG.Value = Pat_MedInfo.SIG;

            SqlParameter par_Refills = cmd.Parameters.Add("@Refills", SqlDbType.Int);
            par_Refills.Value = Pat_MedInfo.Refills;

            SqlParameter par_DocID = cmd.Parameters.Add("@DocID", SqlDbType.Int);
            par_DocID.Value = Pat_MedInfo.Doc_ID;

            SqlParameter par_DocFullname=cmd.Parameters.Add("@Doc_Name",SqlDbType.VarChar,50);
            par_DocFullname.Value = Pat_MedInfo.Doc_FullName;

            SqlParameter par_RxDate = cmd.Parameters.Add("@RxDate", SqlDbType.DateTime);
            par_RxDate.Value = Pat_MedInfo.Rx_Date;
            SqlParameter par_Pat_ID = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            par_Pat_ID.Value = Pat_MedInfo.Pat_ID;
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting Patient Med Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting Patient Med Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    public DataTable getDeliveryModes()
    {
        objNLog.Info("Function Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet Rx_Delivery_Modes = new DataSet();
        bool successFlag = false;
        try
        {
            SqlCommand sqlCmd = new SqlCommand("select * from Rx_Delivery_Modes", sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(Rx_Delivery_Modes, "Rx_Delivery_Modes");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Delivery Modes.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Delivery Modes.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return Rx_Delivery_Modes.Tables["Rx_Delivery_Modes"];
    }

    public DataSet GetPatientAppointments(Property objProp)
    {
        objNLog.Info("Function Started...");
        bool successFlag = false ;
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet ds_Appointments = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand("sp_GetPatAppointments", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp_DocID = cmd.Parameters.Add("@DocID", SqlDbType.Int);
            sp_DocID.Value = Int32.Parse(objProp.DocID);
            SqlParameter sp_date = cmd.Parameters.Add("@date", SqlDbType.Date);
            sp_date.Value = DateTime.Parse(objProp.ApptDate);
            SqlDataAdapter SqlDa = new SqlDataAdapter(cmd);
            
            SqlDa.Fill(ds_Appointments, "Appointments");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Appointments.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Appointments.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return ds_Appointments;
    }

    #region SIG
    public DataTable get_SIGCodes(string prefixText, int count, string contextKey)
    {
        objNLog.Info("Function Started with prefixText,count,contextKey as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select * from SIG_Codes where SIG_Code like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dssigNames = new DataSet();
        bool successFlag = false;
        try
        {
            sqlDa.Fill(dssigNames, "SigCode");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " ", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving SIG Codes.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return dssigNames.Tables[0];
    }
    #endregion

    #region PAYMENTS
    public DataTable get_PatientPaymentInfo(PatientPersonalDetails Pat_med)//Gets Patient InsuranceInfo
    {
        objNLog.Info("Function Started with Pat_med as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        DataSet dsPatAllergies = new DataSet();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getPayments", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Par_patId = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Par_patId.Value = Pat_med.Pat_ID;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            
            sqlDa.Fill(dsPatAllergies, "PatPayments");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " ", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Payment Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return dsPatAllergies.Tables["PatPayments"];
    }
    #endregion

    #region RX HISTORY
    public DataTable get_PatientMedHistory(PatientPersonalDetails pat_Info)
    {
        objNLog.Info("Function Started with pat_Info as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        DataSet dsPatient = new DataSet();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getRxHistory", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Par_patId = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Par_patId.Value = pat_Info.Pat_ID;

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            
           // da.Fill(dsPatient, "Med_His");

            da.Fill(dsPatient, "patMedDetails");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Rx History.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Rx History.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return dsPatient.Tables["patMedDetails"];
    }
  
    #endregion

    #region PRESCRIPTIONS
   
        public void Refill_patPrescriptionMedInfo(string CMID,string UserID,int patID) 
    {
        objNLog.Info("Function Started with CMID, UserID as arguments...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
            try
            {
                string sqlQuery = "INSERT INTO [Rx_Drug_Info]  ([Rx_ID],[Rx_Type],[Rx_DrugID],[Rx_DrugName],[Rx_Dosage],[Rx_Qty],[Rx_SIG],[Rx_Status],Rx_Refills,Rx_ApprovedBy,Rx_ApprovedDate)  SELECT [Rx_ID],'F',[Rx_DrugID],[Rx_DrugName],[Rx_Dosage],[Rx_Qty],[Rx_SIG],'N','0','" + UserID + "',getdate()  FROM [Rx_Drug_Info] where Rx_ItemID=" + CMID;
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                successFlag = true;
                objUALog.LogUserActivity(conStr, UserID, "Added Rx Refill.", "", patID);

            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Refill Patient Rx Med Info.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Refill Patient Rx Med Info.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");  
            
        }
     
        public void update_patPrescriptionMedInfo(string userID,int key,string drugName, int Qty, string sig, int refills, char status, char rxType,int rxItemID,int patID,string rxCmts, char setrxDt, string rxFillDt, byte[] rxDoc) //Updates the medical Inforamation
        {
            objNLog.Info("Function Started with key, drugName, Qty,  sig,  refills,  status,  rxItemID as arguments...");

            SqlConnection sqlCon = new SqlConnection(conStr);
            bool successFlag = false;
            try
            {
                //string sqlQuery;
                
                //if (key == 0)
                //    sqlQuery = "update Rx_Drug_Info set Rx_DrugName='" + drugName + "', Rx_Qty=" + Qty + ",Rx_SIG='" + sig + "',Rx_Refills='" + refills + "',Rx_Status='" + status + "', Rx_Type='" + rxType + "', Rx_Comments='" + rxCmts +"' where Rx_ItemID=" + rxItemID;
                //else
                //    sqlQuery = "update Rx_Drug_Requests set Rx_DrugName='" + drugName + "', Rx_Qty=" + Qty + ",Rx_SIG='" + sig + "',Rx_Refills='" + refills + "',Rx_Status='" + status + "', Rx_Type='" + rxType + "', Rx_Request_Comments='" + rxCmts + "' where Rx_Req_ID=" + rxItemID;

                SqlCommand sqlCmd = new SqlCommand("sp_Update_RxDrugInfo", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;


                SqlParameter sparm_Key = sqlCmd.Parameters.Add("@RxKey", SqlDbType.Int);
                sparm_Key.Value = key;

                SqlParameter sparm_RxItemID = sqlCmd.Parameters.Add("@RxItemID", SqlDbType.Int);
                sparm_RxItemID.Value = rxItemID;

                SqlParameter sparm_DrugName = sqlCmd.Parameters.Add("@RxDrugName", SqlDbType.VarChar,200);
                sparm_DrugName.Value = drugName;

                SqlParameter sparm_DrugQty = sqlCmd.Parameters.Add("@RxDrugQty", SqlDbType.Int);
                sparm_DrugQty.Value = Qty;

                SqlParameter sparm_DrugSIG = sqlCmd.Parameters.Add("@RxDrugSIG", SqlDbType.VarChar,200);
                sparm_DrugSIG.Value = sig;

                SqlParameter sparm_RxRefills = sqlCmd.Parameters.Add("@RxRefills", SqlDbType.Int);
                sparm_RxRefills.Value = refills;

                SqlParameter sparm_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char,1);
                sparm_RxStatus.Value = status;

                SqlParameter sparm_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char,1);
                sparm_RxType.Value = rxType;

                
                SqlParameter sparm_RxComments = sqlCmd.Parameters.Add("@RxComments", SqlDbType.VarChar,2000);
                sparm_RxComments.Value = rxCmts;

                SqlParameter sparm_RxDt = sqlCmd.Parameters.Add("@SetRxDate", SqlDbType.Char,1);
                sparm_RxDt.Value = setrxDt;

                SqlParameter sparm_RxFillDt = sqlCmd.Parameters.Add("@RxFillDate", SqlDbType.VarChar);
                sparm_RxFillDt.Value = rxFillDt;

                SqlParameter sparm_User = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar,50);
                sparm_User.Value = userID;

                SqlParameter Rx_Doc = sqlCmd.Parameters.Add("@RxDoc", SqlDbType.Image);
                if (rxDoc != null)
                    Rx_Doc.Value = rxDoc;
                else
                    Rx_Doc.Value = Convert.DBNull;
               

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                successFlag = true;
                
                if (key == 0) 
                    objUALog.LogUserActivity(conStr, userID, "Updated Patient Rx Info with Rx_ItemID=" + rxItemID, "Rx_Drug_Info",patID);
                else
                    objUALog.LogUserActivity(conStr, userID, "Updated Patient Rx Info with Rx_Req_ID=" + rxItemID, "Rx_Drug_Requests",patID);
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Rx Med Info.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Updating Patient Rx Med Info.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
        }
     
        public void delete_patPrescriptionMedInfo(string userID,int key, string CMID,int patID) //Updates the Patient Note added by sat
        {
            objNLog.Info("Function satrted with key, CMID as arguments...");
            SqlConnection sqlCon = new SqlConnection(conStr);
            bool successFlag = false;
            try
            {
                string sqlQuery;
               
                if (key == 0)
                    sqlQuery = "Delete Rx_Drug_Info  where Rx_ItemID=" + CMID;
                else
                    sqlQuery = "Delete Rx_Drug_Requests  where Rx_Req_ID=" + CMID;
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                successFlag = true;
                if (key == 0)
                    objUALog.LogUserActivity(conStr, userID, "Deleted Patient Rx Info with Rx_ItemID = " + CMID, "Rx_Drug_Info",patID);
                else
                    objUALog.LogUserActivity(conStr, userID, "Deleted Patient Rx Info with Rx_Req_ID = " + CMID, "Rx_Drug_Requests",patID);
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Rx Med Info.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Deleting Patient Rx Med Info.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
        }

        public void delete_patPrescriptionMedReqInfo(string CMID) //Updates the Patient Note added by sat
        {
            objNLog.Info("Function started with CMID as argument...");
            SqlConnection sqlCon = new SqlConnection(conStr);
            bool successFlag = false;
            try
            {
                
                string sqlQuery = "Delete Rx_Drug_Requests  where Rx_Req_ID=" + CMID;
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                successFlag = true;
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Med Request Info.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Deleting Patient Med Request Info.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
        }

        public DataTable GetPatRxDocument(int RxItemID)
        {
            objNLog.Info("Function Started with RxItemID as argument...");
            SqlConnection sqlCon = new SqlConnection(conStr);
            DataSet dsPatient = new DataSet();
            bool successFlag = false;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("sp_GetRxAttachment", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parm_RxItemID = sqlCmd.Parameters.Add("@RxItemID", SqlDbType.Int);
                parm_RxItemID.Value = RxItemID;

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsPatient, "PatRxDoc");
                successFlag = true;
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Rx Documnet. ", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Retrieving Patient Rx Documnet.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
            return dsPatient.Tables[0];
        }
        
        public DataTable get_PatientPriscription()
        {
            objNLog.Info("Function started...");
            SqlConnection sqlCon = new SqlConnection(conStr);
            bool successFlag = false;
            DataSet dsPatient = new DataSet();
            try
            {
            string sqlQuery = "select Rx_Drug_Info.Rx_DrugName,Rx_Drug_Info.Rx_Refills,Rx_Drug_Info.Rx_Qty,Rx_Drug_Info.Rx_SIG,Rx_Drug_Info.Rx_Status,Rx_Drug_Info.Rx_Type  from Rx_Drug_Info";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            
            da.Fill(dsPatient, "Med_Priscription");
            da.Fill(dsPatient, "Med_Priscription");
            successFlag = true;
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Rx.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Retrieving Patient Rx.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
            return dsPatient.Tables["Med_Priscription"];
        }
        
        public void Set_Drug_Inventory(char processType,string drugName , string qty , string comments , int patID , string lotNumber,string expDate ,string userID)
        {
            objNLog.Info("Function Started..");
            SqlConnection sqlCon = new SqlConnection(conStr);
            bool successFlag = false;
            try
            {
                //string sqlQuery = "update Call_Log set Call_Desc='" + Pat_CallDesc + "' where Call_ID=" + CallID;
                //SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

                SqlCommand sqlCmd = new SqlCommand("sp_Set_Drug_Inventory", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;  

                SqlParameter sp_Pat_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
                sp_Pat_ID.Value = patID;

                SqlParameter sp_Inv_Group_Code = sqlCmd.Parameters.Add("@Inv_Group_Code", SqlDbType.Char, 1);
                sp_Inv_Group_Code.Value = processType;

                SqlParameter sp_Drug_Name = sqlCmd.Parameters.Add("@Drug_Name", SqlDbType.VarChar, 50);
                sp_Drug_Name.Value = drugName;

                SqlParameter sp_Qty = sqlCmd.Parameters.Add("@Qty", SqlDbType.Int);
                sp_Qty.Value = int.Parse("-"+qty);

                SqlParameter sp_Inv_Desc = sqlCmd.Parameters.Add("@Inv_Desc", SqlDbType.VarChar, 255);
                sp_Inv_Desc.Value = comments;

                SqlParameter sp_Lot_Num = sqlCmd.Parameters.Add("@Lot_Num", SqlDbType.VarChar, 50);
                if(lotNumber!="")
                   sp_Lot_Num.Value = lotNumber;
                else
                   sp_Lot_Num.Value = System.DBNull.Value;

                SqlParameter sp_Expiry_Date = sqlCmd.Parameters.Add("@Expiry_Date", SqlDbType.DateTime);
                if(expDate!="")
                  sp_Expiry_Date.Value = expDate;
                else
                  sp_Expiry_Date.Value = System.DBNull.Value;

                SqlParameter sp_LastModifiedBy = sqlCmd.Parameters.Add("@LastModifiedBy", SqlDbType.VarChar, 50);
                sp_LastModifiedBy.Value = userID;

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                successFlag = true;
                objUALog.LogUserActivity(conStr, userID, "Inserted Record into table while saving process info.", "Drug_Inventory", 0);
           
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Call Log.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Updating Patient Call Log.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
        
        }

    public void Update_Drug_Info(int rxItemID,string userID)
        {
            objNLog.Info("Function Started..");
            SqlConnection sqlCon = new SqlConnection(conStr);
            bool successFlag = false;
            try
            {
                //string sqlQuery = "update Call_Log set Call_Desc='" + Pat_CallDesc + "' where Call_ID=" + CallID;
                //SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

                SqlCommand sqlCmd = new SqlCommand("sp_Update_Process_DrugInfo", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_RxItemID = sqlCmd.Parameters.Add("@RxItemID", SqlDbType.Int);
                sp_RxItemID.Value = rxItemID;
 
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                successFlag = true;
                objUALog.LogUserActivity(conStr, userID, "Updated field Rx_Status='D' where Rx_ItemID=" + rxItemID.ToString() + " while saving process info.", "Rx_Drug_Info", 0);
                
                 
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Call Log.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Updating Patient Call Log.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
        
        }
    #endregion

    #region NOTES
        public void Set_patNote(int patID, string Pat_Note, string userID) //Updates the Patient Note added by sat
        {
            objNLog.Info("Function started with Pat_Note, NoteID as arguments...");
            SqlConnection sqlCon = new SqlConnection(conStr);
            bool successFlag = false;
            try
            {

                //string sqlQuery = "update Pat_Rx_Notes set Note_Description='" + Pat_Note + "' where Note_ID=" + NoteID;
                SqlCommand sqlCmd = new SqlCommand("sp_Set_Pat_Notes", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_PatID = sqlCmd.Parameters.Add("@PatID",SqlDbType.Int);
                sp_PatID.Value=patID;

                SqlParameter sp_PatNote = sqlCmd.Parameters.Add("@Pat_Note", SqlDbType.VarChar, 1000);
                sp_PatNote.Value=Pat_Note;

                SqlParameter sp_UserID = sqlCmd.Parameters.Add("@Pat_NoteBy",SqlDbType.VarChar,50);
                sp_UserID.Value=userID;

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                successFlag = true;
                objUALog.LogUserActivity(conStr, userID, "Added New Patient Notes Info. with PatID = " + patID.ToString(), "Pat_Rx_Notes", patID);
            }
            catch (SqlException SqlEx)
            {
                objNLog.Error("SQLException : " + SqlEx.Message);
                throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Notes.", SqlEx);
            }
            catch (Exception ex)
            {
                objNLog.Error("Exception : " + ex.Message);
                throw new Exception("**Error occured while Updating Patient Notes.", ex);
            }
            finally
            {
                sqlCon.Close();
                objNLog.Info("Finally Block: " + successFlag);
            }
            objNLog.Info("Function Completed...");
        }


    public void update_patNote(string userID, string Pat_Note, string NoteID,int patID) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function started with Pat_Note, NoteID as arguments...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        try
        {
            
            //string sqlQuery = "update Pat_Rx_Notes set Note_Description='" + Pat_Note + "' where Note_ID=" + NoteID;
            //SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

            SqlCommand sqlCmd = new SqlCommand("sp_Update_Pat_Notes", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_PatNoteID = sqlCmd.Parameters.Add("@Pat_NoteID", SqlDbType.Int);
            sp_PatNoteID.Value =int.Parse(NoteID);

            SqlParameter sp_PatNote = sqlCmd.Parameters.Add("@Pat_Note", SqlDbType.VarChar, 1000);
            sp_PatNote.Value = Pat_Note;


            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Updated Patient Notes Info. with Note_ID = " + NoteID, "Pat_Rx_Notes",patID );
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Notes.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Notes.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    public void delete_patNote(string userID,  string NoteID,int patID) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function Started with NoteID as arguments...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        try
        {
            
            string sqlQuery = "Delete Pat_Rx_Notes  where Note_ID=" + NoteID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Deleted Patient Notes Info. with Note_ID = " + NoteID, "Pat_Rx_Notes",patID );
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Notes.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Notes.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed..."); 
    }

    public DataTable get_PatientNote(PatientPersonalDetails pat_Info)
    {
        objNLog.Info("Function Started with pat_Info as arguments...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Note_ID,Note_Description,Note_Date,Note_By from Pat_Rx_Notes where pat_ID=" + pat_Info.Pat_ID + " Order By Note_Date Desc";
        //SqlCommand sqlCmd = new SqlCommand(sqlQuery, sql);
        SqlCommand cmd = new SqlCommand("sp_get_PatientNotes", sqlCon);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter par_pat_ID = cmd.Parameters.Add("@Pat_Id", SqlDbType.Int);
        par_pat_ID.Value = pat_Info.Pat_ID;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsPatient = new DataSet();

        try
        {
            da.Fill(dsPatient, "pat_Note");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Notes.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Notes.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return dsPatient.Tables["pat_Note"];
    }

    #endregion

    #region CALL LOGS

    public DataTable get_PatientCallLogInfo(PatientPersonalDetails Pat_med)//Gets Patient InsuranceInfo
    {
        objNLog.Info("Function Started with Pat_med as arguments...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsPatAllergies = new DataSet();
        bool successFlag = false;
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getCallLogs", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter Par_patId = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Par_patId.Value = Pat_med.Pat_ID;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsPatAllergies, "PatCallLog");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Call Log Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Call Log Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return dsPatAllergies.Tables["PatCallLog"];
    }

    public void update_patCallLog(string userID,string Pat_CallDesc, string CallID,int patID) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function Started with Pat_CallDesc,CallID as arguments...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        try
        {
            //string sqlQuery = "update Call_Log set Call_Desc='" + Pat_CallDesc + "' where Call_ID=" + CallID;
            //SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

            SqlCommand sqlCmd = new SqlCommand("sp_Update_Pat_CallLog", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_Pat_CallID = sqlCmd.Parameters.Add("@Pat_CallID", SqlDbType.Int);
            sp_Pat_CallID.Value = int.Parse(CallID);

            SqlParameter sp_Pat_CallDesc = sqlCmd.Parameters.Add("@Pat_CallDesc", SqlDbType.VarChar, 1000);
            sp_Pat_CallDesc.Value = Pat_CallDesc;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Updated Patient Call Log Info. with Call_ID = " + CallID, "Call_Log",patID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Call Log.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Call Log.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    public void delete_patCallLog(string userID,string CallID,int patID) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function Started with CallID as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        try
        {
            string sqlQuery = "Delete Call_Log  where Call_ID=" + CallID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Deleted Patient Call Log Info. with Call_ID = " + CallID, "Call_Log",patID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Call Log.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Call Log.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    #endregion

    #region PAYMENTS

    public void update_patPayments(string userID, string Pat_PayAmount, string Pat_PayDesc, string Pat_ChequeorCC, string PayID,int patID) //Updates the Patient Note added by sat
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        try
        {
            //string sqlQuery = "update Patient_Payments set Check_OR_CC_Number='" + Pat_ChequeorCC + "',Payment_Amount='" + Pat_PayAmount + "',Payment_Notes='" + Pat_PayDesc + "' where Payment_ID=" + PayID;
            //SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

            SqlCommand sqlCmd = new SqlCommand("sp_Update_Pat_Payments", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_PaymentID = sqlCmd.Parameters.Add("@PaymentID", SqlDbType.Int);
            sp_PaymentID.Value = PayID;

            SqlParameter sp_CheckCCNumber = sqlCmd.Parameters.Add("@CheckCCNumber", SqlDbType.VarChar, 50);
            sp_CheckCCNumber.Value = Pat_ChequeorCC;

            SqlParameter sp_PaymentAmt = sqlCmd.Parameters.Add("@PaymentAmt", SqlDbType.Money);
            sp_PaymentAmt.Value = Pat_PayAmount;

            SqlParameter sp_PaymentDesc = sqlCmd.Parameters.Add("@PaymentDesc", SqlDbType.VarChar, 2000);
            sp_PaymentDesc.Value = Pat_PayDesc;

            


            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Updated Payment Info. with Payment Amt = " + Pat_PayAmount, "Patient_Payments",patID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Payments.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Payments.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }
    public void delete_patPayments(string userID,string PayID,int patID) //Delete the Patient Payments
    {
        objNLog.Info("Function Started with PayID as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            string sqlQuery = "Delete Patient_Payments  where Payment_ID=" + PayID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Deleted Patient Payment Info. with Payment_ID = " + PayID, "Patient_Payments",patID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Payments.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Payments.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    #endregion

    #region DOCUMENTS

    public string Set_Patient_Documents(int patID,string docName, string docDesc, string fileName) 
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        try
        {
             
            SqlCommand sqlCmd = new SqlCommand("sp_Set_Pat_Document", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_PatID = sqlCmd.Parameters.Add("@PatID", SqlDbType.Int);
            sp_PatID.Value = patID;

            SqlParameter sp_DocName = sqlCmd.Parameters.Add("@Doc_Name", SqlDbType.VarChar, 50);
            sp_DocName.Value = docName;

            SqlParameter sp_DocDesc = sqlCmd.Parameters.Add("@Doc_Desc", SqlDbType.VarChar,255);
            sp_DocDesc.Value = docDesc;

            SqlParameter sp_FileName = sqlCmd.Parameters.Add("@File_Name", SqlDbType.NVarChar, 50);
            sp_FileName.Value = fileName;

            SqlParameter sp_FileID = sqlCmd.Parameters.Add("@File_ID", SqlDbType.Int);
            sp_FileID.Direction = ParameterDirection.Output;


            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            
            successFlag = true;
           return  sp_FileID.Value.ToString();
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Payments.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Payments.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }
   

    public DataTable get_PatientDocuments(PatientPersonalDetails pat_Info)
    {
        objNLog.Info("Function Started with pat_Info as argument...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsPatient = new DataSet();
        bool successFlag = false;
        try
        {
            string sqlQuery = "select Pat_Doc_ID,Doc_Name,Doc_Desc,FileName,'Documents/' + Convert(varchar,Pat_Doc_ID,0) + '_' + FileName as hLink from Pat_Documents where pat_ID=" + pat_Info.Pat_ID + " Order By Pat_Doc_ID Desc";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, sqlCon);
            da.Fill(dsPatient, "pat_Doc");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Documnet. ", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Documnet.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return dsPatient.Tables["pat_Doc"];
    }
    
    public void delete_patDocument(string DocID) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function Started with DocID as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
           
            string sqlQuery = "Delete Pat_Documents  where Pat_Doc_ID=" + DocID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Documnet. ", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Documnet.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    public void Delete_PatBilling(string BillNo) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function Started with PatID as argument...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {

            string sqlQuery = "Delete from Billing  where Trans_No=" + BillNo;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Billing. ", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Billing.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }


    #endregion

    #region APPOINTMENTS

    public string set_PatientAppointments(string userID,PatinetMedHistoryInfo Pat_MedInfo)
    {
        objNLog.Info("Function started with Pat_MedInfo as Argument...");
        bool successFlag = false;
        string flag = "";
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
        SqlCommand cmd = new SqlCommand("sp_set_PatientAppointments", sqlCon);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter Pat_ID = cmd.Parameters.Add("@PAT_ID", SqlDbType.Int);
        Pat_ID.Value = Pat_MedInfo.Pat_ID;

        SqlParameter Fac_ID = cmd.Parameters.Add("@Fac_ID", SqlDbType.Int);
        Fac_ID.Value = Pat_MedInfo.Fac_ID;

        SqlParameter Doc_ID = cmd.Parameters.Add("@Doc_ID", SqlDbType.Int);
        Doc_ID.Value = Pat_MedInfo.Doc_ID;

        SqlParameter App_Type = cmd.Parameters.Add("@APPT_Type", SqlDbType.Char, 1);
        App_Type.Value = Pat_MedInfo.AppointmentType;

        SqlParameter App_Date = cmd.Parameters.Add("@APPT_Date", SqlDbType.DateTime);
        App_Date.Value = Convert.ToDateTime(Pat_MedInfo.AppoitmentDate.ToString());

        SqlParameter App_Time = cmd.Parameters.Add("@APPT_Time", SqlDbType.DateTime);
        App_Time.Value = Convert.ToDateTime(Pat_MedInfo.AppointmentTime.ToString());

        SqlParameter App_Purp = cmd.Parameters.Add("@App_Purp", SqlDbType.VarChar, 255);
        App_Purp.Value = Pat_MedInfo.App_Purp;

        SqlParameter APPT_Status_Code = cmd.Parameters.Add("@APPT_Status_Code", SqlDbType.Char, 1);
        APPT_Status_Code.Value = 'S';

        SqlParameter App_Note = cmd.Parameters.Add("@App_Note", SqlDbType.VarChar, 255);
        App_Note.Value = Pat_MedInfo.App_Note;

        
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            flag = "Appointment Created Successfully...";
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Created Patient Appointment for PatID = " + Pat_MedInfo.Pat_ID.ToString() + " with Doctor ID = " + Pat_MedInfo.Doc_ID.ToString(), "Patient_Appointments", Pat_MedInfo.Pat_ID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Adding Patient Appointment.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Adding Patient Appointment.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
        return flag;
    }
   
    public void delete_PatAppointments(string userID,string App_ID)
    {
        objNLog.Info("Function Started with App_ID as Parameter...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            int AppID = int.Parse(App_ID);
            
            string sqlQuery = "Delete Patient_Appointments  where APPT_ID=" + AppID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objUALog.LogUserActivity(conStr, userID, "Deleted Patient Appointment with APPT_ID = " + AppID.ToString(), "Patient_Appointments",0);
           successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Appointment.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Appointment.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    public bool  Has_Appt(string PatID)
    {
        bool flag=false;
        objNLog.Info("Function started with PatID...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("Select count(APPT_ID) from  Patient_Appointments where PAT_ID=" + PatID + " and convert(date,APPT_Date,0)=convert(date,getdate(),0) ", sqlCon);
        try
        {
            sqlCon.Open();
            string val = sqlCmd.ExecuteScalar().ToString();
            if (val == "0")
                flag= false;
            else
                flag= true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Searching Patient Appointment.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Searching Patient Appointment.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Function Completed...");
        }
        
        return flag;
    }

    public DataSet Get_Appt(string PatID)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet ds = new DataSet();
        
        try
        {
            SqlCommand sqlCmd = new SqlCommand("Select PA.APPT_Time, doc.Doc_FName + ' ' + doc.Doc_LName as Doctor_Name from  Patient_Appointments PA, Doctor_Info doc where PA.PAT_ID=" + PatID + " and convert(date,PA.APPT_Date,0)=convert(date,getdate(),0) and PA.APPT_Party_ID=doc.Doc_ID", sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(ds, "Pat_Appts");
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retreving Patient Appointment.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retreving Patient Appointment.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Function Completed...");
        }
        return ds;
    }

    #endregion

    #region ALLERGIES

    public void Set_Pat_Allergies(string userID,PatientAllergies pAllergyDetails)
    {
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        objNLog.Info("Function Started with parameter pAllergyDetails");
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_Set_Pat_Allergies", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Pat_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Pat_ID.Value = pAllergyDetails.PatID;

            SqlParameter PI_GroupNo = sqlCmd.Parameters.Add("@AllergicTo", SqlDbType.VarChar, 255);
            PI_GroupNo.Value = pAllergyDetails.AllergicTo;


            SqlParameter PI_PolicyID = sqlCmd.Parameters.Add("@AllergyDesc", SqlDbType.VarChar, 255);
            PI_PolicyID.Value = pAllergyDetails.AllergyDescription;
             
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

            objNLog.Info("Allergy Info Added Successfully...");
            objNLog.Info("<sp_Set_Pat_Allergies>Stored Procedure Executed Successfully...");
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Added New Allergy Info. for the Patient(PatID)=" + pAllergyDetails.PatID, "Patient_Allergies", pAllergyDetails.PatID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Adding Allergy.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Adding Allergy.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }

    public DataTable get_PatientAllergies(PatientPersonalDetails Pat_alg)//Gets Patient Allergies
    {
        objNLog.Info("Function Started with Pat_alg");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getPatAllergies", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter Par_patId = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
        Par_patId.Value = Pat_alg.Pat_ID;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsPatAllergies = new DataSet();
        try
        {
            sqlDa.Fill(dsPatAllergies, "PatAllergies");
            objNLog.Info("Allergy Info Retrieved Successfully...");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Allergy Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Allergy Info.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }
        
        objNLog.Info("Function Terminated...");
        return dsPatAllergies.Tables["PatAllergies"];
    }

    public void update_patAllergy(string userID, string patAllergyTO, string patAllegryDesc, string Pat_AID,int patID)
    {
        objNLog.Info("Function Started with patAllergyTO,patAllegryDesc and Pat_AID");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            //string sqlQuery = "update Patient_Allergies set Pat_Allergic_To='" + patAllergyTO + "',Pat_Allergy_Desc='" + patAllegryDesc + "' where PA_ID=" + Pat_AID;
            ////SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon );

            SqlCommand sqlCmd = new SqlCommand("sp_Update_Pat_Allergies", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_PAID = sqlCmd.Parameters.Add("@PA_ID", SqlDbType.Int);
            sp_PAID.Value = int.Parse(Pat_AID);

            SqlParameter sp_AllergicTo= sqlCmd.Parameters.Add("@AllergicTo", SqlDbType.VarChar, 255);
            sp_AllergicTo.Value = patAllergyTO;


            SqlParameter sp_AllergyDesc = sqlCmd.Parameters.Add("@AllergyDesc", SqlDbType.VarChar, 255);
            sp_AllergyDesc.Value = patAllegryDesc;

           
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objNLog.Info("Allergy Info Updated Successfully...");
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Updated Patient Allergy Info with Patient Allergy ID=" + Pat_AID, "Patient_Allergies", patID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Allergy Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Allergy Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }
    
    public  void  delete_patAllergy(string  userID,string PA_ID,int patID)
    {

        objNLog.Info("Function Started with PA_ID");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            string sqlQuery = "Delete Patient_Allergies  where PA_ID=" + PA_ID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objNLog.Info("Allergy Info Delete Successfully...");
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Deleted Patient Allergy Info with Patient Allergy ID=" + PA_ID, "Patient_Allergies", patID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Allergy Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Allergy Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
     }

    #endregion

    #region INSURANCE

    public DataTable get_InsuranceNames(string prefixText, int count, string contextKey)
    {
        objNLog.Info("Function Started with prefixText,count and contextKey");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select * from Insurance_Info where Ins_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsInsNames = new DataSet();
        try
        {
            sqlDa.Fill(dsInsNames, "InsNames");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Searching Insurance By Name.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Searching Insurance By Name.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
        return dsInsNames.Tables[0];
    }

    public DataTable get_PatientInsuranceInfo(PatientPersonalDetails Pat_med)//Gets Patient InsuranceInfo
    {
        // sp_getPatAllergies
        objNLog.Info("Function Started with parameter Pat_med");
        bool successFlag = false;
        SqlConnection sql = new SqlConnection(conStr);
        DataSet dsPatIns = new DataSet();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getPatInsurance", sql);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Par_patId = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Par_patId.Value = Pat_med.Pat_ID;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dsPatIns, "PatIns");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Insurance.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Insurance.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
        return dsPatIns.Tables["PatIns"];
    }

    public DataTable get_InsNames(string prefixText)
    {
        objNLog.Info("Function Started with parameter prefixText");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Ins_Name,Ins_ID from Insurance_Info where Ins_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsInsNames = new DataSet();
        try
        {
            sqlDa.Fill(dsInsNames, "InsNames");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Searching Insurance.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Searching Insurance.", ex);
        }
        finally
        {

            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
         
        return dsInsNames.Tables["InsNames"];
    }

    public DataTable get_PatInsNames(string patID)
    {
        objNLog.Info("Function Started with parameter patID");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Insurance_Info.Ins_Name,Patient_Ins.Patient_Ins_ID from Insurance_Info,Patient_Ins where Insurance_Info.Ins_ID=Patient_Ins.Ins_ID and Patient_Ins.Pat_ID='" + patID + "' ";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsInsNames = new DataSet();
        try
        {
            sqlDa.Fill(dsInsNames, "InsNames");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Insurance.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Insurance.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
        return dsInsNames.Tables["InsNames"];
    }

    public void Set_Pat_Insurance(string userID,PatientInsuranceDetails pInsDetails)
    {
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        objNLog.Info("Function Started with parameter pInsDetails");
        try
        {

            SqlCommand sqlCmd = new SqlCommand("sp_set_Pat_Insurance_Info", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Pat_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Pat_ID.Value = pInsDetails.Pat_ID;
            SqlParameter Ins_ID = sqlCmd.Parameters.Add("@Ins_ID", SqlDbType.Int);
            Ins_ID.Value = pInsDetails.InsuranceID;
            SqlParameter PI_PolicyID = sqlCmd.Parameters.Add("@PI_PolicyID", SqlDbType.VarChar, 25);
            PI_PolicyID.Value = pInsDetails.PI_PolicyID;
            SqlParameter PI_GroupNo = sqlCmd.Parameters.Add("@PI_GroupNo", SqlDbType.VarChar, 25);
            PI_GroupNo.Value = pInsDetails.PI_GroupNo;
            SqlParameter PI_BINNo = sqlCmd.Parameters.Add("@PI_BINNo", SqlDbType.VarChar, 25);
            PI_BINNo.Value = pInsDetails.PI_BINNo;
            SqlParameter PI_InsdName = sqlCmd.Parameters.Add("@PI_InsdName", SqlDbType.VarChar, 50);
            PI_InsdName.Value = pInsDetails.InsuredName;
            SqlParameter PI_InsdDoB = sqlCmd.Parameters.Add("@PI_InsdDoB", SqlDbType.VarChar, 50);
            PI_InsdDoB.Value = pInsDetails.InsuredDOB;
            SqlParameter PI_InsdSSN = sqlCmd.Parameters.Add("@PI_InsdSSN", SqlDbType.VarChar, 50);
            PI_InsdSSN.Value = pInsDetails.InsuredSSN;
            SqlParameter PI_InsdRel = sqlCmd.Parameters.Add("@PI_InsdRel", SqlDbType.VarChar, 50);
            PI_InsdRel.Value = pInsDetails.InsuredRelation;
            SqlParameter PI_ActiveFlagYN = sqlCmd.Parameters.Add("@PI_ActiveFlagYN", SqlDbType.Char, 1);
            PI_ActiveFlagYN.Value = pInsDetails.IsActive;
            SqlParameter IsPrimaryIns = sqlCmd.Parameters.Add("@IsPrimaryIns", SqlDbType.Char, 1);
            IsPrimaryIns.Value = pInsDetails.IsPrimaryIns;
            SqlParameter PI_InsPhone = sqlCmd.Parameters.Add("@PI_InsPh", SqlDbType.VarChar,25);
            PI_InsPhone.Value = pInsDetails.InsPhone;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

            objNLog.Info("Insurance Added Successfully...");
            objNLog.Info("<sp_set_Pat_Insurance_Info>Stored Procedure Executed Successfully...");
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Added New Patient Insurance Info with PatID=" + pInsDetails.Pat_ID.ToString(), "Patient_Ins", pInsDetails.Pat_ID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Adding Patient Insurance.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Adding Patient Insurance.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }

    public void Update_Pat_Insurance(string userID,PatientInsuranceDetails pInsDetails,int patID)
    {
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        objNLog.Info("Function Started with parameter pInsDetails");
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_Update_Pat_Ins", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Pat_Ins_ID = sqlCmd.Parameters.Add("@Pat_Ins_ID", SqlDbType.Int);
            Pat_Ins_ID.Value = pInsDetails.PI_ID;

            SqlParameter Pat_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Pat_ID.Value = pInsDetails.Pat_ID;
            SqlParameter Ins_ID = sqlCmd.Parameters.Add("@Ins_ID", SqlDbType.Int);
            Ins_ID.Value = pInsDetails.InsuranceID;
            SqlParameter PI_PolicyID = sqlCmd.Parameters.Add("@PI_PolicyID", SqlDbType.VarChar, 25);
            PI_PolicyID.Value = pInsDetails.PI_PolicyID;
            SqlParameter PI_GroupNo = sqlCmd.Parameters.Add("@PI_GroupNo", SqlDbType.VarChar, 25);
            PI_GroupNo.Value = pInsDetails.PI_GroupNo;
            SqlParameter PI_BINNo = sqlCmd.Parameters.Add("@PI_BINNo", SqlDbType.VarChar, 25);
            PI_BINNo.Value = pInsDetails.PI_BINNo;
            SqlParameter PI_InsdName = sqlCmd.Parameters.Add("@PI_InsdName", SqlDbType.VarChar, 50);
            PI_InsdName.Value = pInsDetails.InsuredName;
            SqlParameter PI_InsdDoB = sqlCmd.Parameters.Add("@PI_InsdDoB", SqlDbType.VarChar, 50);
            PI_InsdDoB.Value = pInsDetails.InsuredDOB;
            SqlParameter PI_InsdSSN = sqlCmd.Parameters.Add("@PI_InsdSSN", SqlDbType.VarChar, 50);
            PI_InsdSSN.Value = pInsDetails.InsuredSSN;
            SqlParameter PI_InsdRel = sqlCmd.Parameters.Add("@PI_InsdRel", SqlDbType.VarChar, 50);
            PI_InsdRel.Value = pInsDetails.InsuredRelation;
            SqlParameter PI_ActiveFlagYN = sqlCmd.Parameters.Add("@PI_ActiveFlagYN", SqlDbType.Char, 1);
            PI_ActiveFlagYN.Value = pInsDetails.IsActive;
            SqlParameter IsPrimaryIns = sqlCmd.Parameters.Add("@IsPrimaryIns", SqlDbType.Char, 1);
            IsPrimaryIns.Value = pInsDetails.IsPrimaryIns;
            SqlParameter PI_InsPhone = sqlCmd.Parameters.Add("@PI_InsPhone", SqlDbType.VarChar, 25);
            PI_InsPhone.Value = pInsDetails.InsPhone;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

            objNLog.Info("Insurance Updated Successfully...");
            objNLog.Info("<sp_Update_Pat_Ins>Stored Procedure Executed Successfully...");
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Updated Patient Insurance Info", "Patient_Ins",patID);
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient Insurance.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient Insurance.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }

    public void Delete_Patient_Insurance(string userID,string PatientInsID,int patID)  
    {
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        objNLog.Info("Function Started with parameter PatientInsID");
        
        try
        {
            
            SqlCommand sqlCmd = new SqlCommand("sp_Delete_Pat_Ins", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sqlParm_PatInsId = sqlCmd.Parameters.Add("@PatInsID", SqlDbType.Int);
            sqlParm_PatInsId.Value = Int32.Parse(PatientInsID);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objNLog.Info("Insurance Deleted Successfully...");
            objNLog.Info("<sp_Delete_Pat_Ins>Stored Procedure Executed Successfully...");
            successFlag = true;
            objUALog.LogUserActivity(conStr, userID, "Deleted Patient Insurance Info with PatientInsID= " + PatientInsID, "Patient_Ins", patID);
           
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Insurance.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Insurance.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }

    public int IsPrimaryInsurance(int patInsPkID)
    {
        bool successFlag = false;
        int isPrimary = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        objNLog.Info("Function Started with parameter PatientInsID");

        try
        {

            SqlCommand sqlCmd = new SqlCommand("sp_IsPrimaryInsurance", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sqlParm_PatId = sqlCmd.Parameters.Add("@PatInsPkID", SqlDbType.Int);
            sqlParm_PatId.Value = patInsPkID;

            SqlParameter sqlParm_PrimaryIns = sqlCmd.Parameters.Add("@PrimaryIns", SqlDbType.Int);
            sqlParm_PrimaryIns.Direction = ParameterDirection.Output;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            isPrimary=int.Parse(sqlParm_PrimaryIns.Value.ToString());
            successFlag = true;

        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting Patient Insurance.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting Patient Insurance.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
        return isPrimary;
    }
    #endregion

    #region SHIP LOGS

    public void set_ShipLog(PatinetMedHistoryInfo Pat_MedInfo)
    {
        objNLog.Info("Function started with Pat_MedInfo as parameter.");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
        SqlCommand cmd = new SqlCommand("sp_set_ShipLogInfo", sqlCon);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter Rx_ItemID = cmd.Parameters.Add("@Rx_ItemID", SqlDbType.Int);
        Rx_ItemID.Value = Pat_MedInfo.RXItemID;

        SqlParameter Date_Shipped = cmd.Parameters.Add("@Date_Shipped", SqlDbType.SmallDateTime);
        Date_Shipped.Value = Pat_MedInfo.ShipDate;

        SqlParameter Tracking_Number = cmd.Parameters.Add("@Tracking_Number", SqlDbType.VarChar, 255);
        Tracking_Number.Value = Pat_MedInfo.TrackingNo;

        SqlParameter Delivery_Mode = cmd.Parameters.Add("@Delivery_Mode", SqlDbType.VarChar, 1);
        Delivery_Mode.Value = Pat_MedInfo.DeliveryMode;

        SqlParameter Shipping_Details = cmd.Parameters.Add("@Shipping_Details", SqlDbType.VarChar, 255);
        Shipping_Details.Value = Pat_MedInfo.ShippingDetails;

        SqlParameter Ship2Address = cmd.Parameters.Add("@Ship2Address", SqlDbType.VarChar, 255);
        Ship2Address.Value = Pat_MedInfo.ShippingAdd;
        //@Delivery_Confirmation
        SqlParameter Delivery_Confirmation = cmd.Parameters.Add("@Delivery_Confirmation", SqlDbType.VarChar, 255);
        Delivery_Confirmation.Value = Pat_MedInfo.DeliveryConfirm;

        SqlParameter Delivery_Status = cmd.Parameters.Add("@Delivery_Status", SqlDbType.VarChar, 255);
        Delivery_Status.Value = Pat_MedInfo.DeliveryStatus;
            
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            successFlag=true;
       
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Adding ShipLog.", SqlEx);
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Adding ShipLog.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
            
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }

    public DataTable get_ShipLog(string pat_ID)
    {
        objNLog.Info("Function started.");
        bool successFlag = false;
        try
        {
                SqlConnection sqlCon = new SqlConnection(conStr);
                string sqlQry = "sp_getShipLogs";
                SqlCommand cmd = new SqlCommand(sqlQry);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlCon;
                SqlParameter Par_patId = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
                Par_patId.Value = pat_ID;
                DataSet ds = new DataSet();
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(ds, "Pat_ShipLofInfo");
                successFlag = true;
                return ds.Tables["Pat_ShipLofInfo"];
             
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving ShipLog..", SqlEx);
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving ShipLog..", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
            objNLog.Info("Status: " + successFlag);
            objNLog.Info("Function Terminated...");
        }
       
    }

    public void  delete_ShipLogInfo(string RxItemID) //Updates the Patient Note added by sat
    {
        objNLog.Info("Function started with RxItemID as Parameters.");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            int itemID = int.Parse(RxItemID);
            string sqlQuery = "Delete Rx_Delivery_Tracking  where  Rx_ItemID=" + itemID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Deleting ShipLog..", SqlEx);
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Deleting ShipLog..", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }

    public void update_patShipLogdInfo(string status, string ShipDet, string DConfirm, int rxItemID) //Updates the medical Inforamation
    {
        objNLog.Info("Function started with status,ShipDet,DConfirm,rxItemID as Parameters.");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            string sqlQuery = "update Rx_Delivery_Tracking set Delivery_Status='" + status + "',Shipping_Details='" + ShipDet + "',Delivery_Confirmation='" + DConfirm + "' where Rx_ItemID=" + rxItemID;
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Updating Patient ShipLog Info.", SqlEx);
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Updating Patient ShipLog Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Status: " + successFlag);
        objNLog.Info("Function Terminated...");
    }

    #endregion

    #region BILLING
    public void SetPatientBillingInfo(PatientBilling Pat_BillInfo)
    {
        objNLog.Info("Function Started with Pat_BillInfo");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            SqlCommand cmd = new SqlCommand("sp_set_PatientBillingInfo", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Pat_ID = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Pat_ID.Value = Pat_BillInfo.PatID;

            SqlParameter Trans_Date = cmd.Parameters.Add("@TransDate", SqlDbType.DateTime);
            Trans_Date.Value = DateTime.Parse(Pat_BillInfo.TransactionDate);

            SqlParameter Trans_Type = cmd.Parameters.Add("@TransType", SqlDbType.Char,1);
            Trans_Type.Value = Pat_BillInfo.TransactionType;

            SqlParameter Trans_Mode = cmd.Parameters.Add("@TransMode", SqlDbType.Char, 1);
            Trans_Mode.Value = Pat_BillInfo.TransactionMode;

            SqlParameter Trans_Amt = cmd.Parameters.Add("@TransAmt", SqlDbType.VarChar);
            Trans_Amt.Value =  Pat_BillInfo.TransactionAmount;

            SqlParameter Trans_Det = cmd.Parameters.Add("@TransDet", SqlDbType.VarChar);
            Trans_Det.Value = Pat_BillInfo.TransactionDetails;

            SqlParameter Last_ModifiedBy = cmd.Parameters.Add("@LastModifiedBy", SqlDbType.VarChar);
            Last_ModifiedBy.Value = Pat_BillInfo.User;

            SqlParameter  Trans_SysFlag = cmd.Parameters.Add("@Trans_SysFlag", SqlDbType.VarChar,50);
            Trans_SysFlag.Value = Pat_BillInfo.TransSysFlag;

            sqlCon.Open();
            cmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting Patient Billing Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting Patient Med Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }
    public DataTable GetPatientBillingInfo(int patID)
    {
        objNLog.Info("Function started.");
        bool successFlag = false;
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            string sqlQry = "sp_getPatientBilling";
            SqlCommand cmd = new SqlCommand(sqlQry);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlCon;
            SqlParameter Par_patId = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Par_patId.Value = patID;
            DataSet ds = new DataSet();
            SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            sqlDa.Fill(ds, "Pat_BillingInfo");
            successFlag = true;
            return ds.Tables["Pat_BillingInfo"];

        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving Patient Billing Info..", SqlEx);

        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving Patient Billing Info..", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
            objNLog.Info("Status: " + successFlag);
            objNLog.Info("Function Terminated...");
        }
    }
    public void SetPatientAdjustBillingInfo(PatientBilling Pat_AdjBillInfo)
    {
        objNLog.Info("Function Started with Pat_BillInfo");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            SqlCommand cmd = new SqlCommand("sp_set_PatientAdjustBillingInfo", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Pat_ID = cmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            Pat_ID.Value = Pat_AdjBillInfo.PatID;

            SqlParameter Trans_Date = cmd.Parameters.Add("@TransDate", SqlDbType.DateTime);
            Trans_Date.Value = DateTime.Parse(Pat_AdjBillInfo.TransactionDate);

            SqlParameter Trans_Type = cmd.Parameters.Add("@TransType", SqlDbType.Char, 1);
            Trans_Type.Value = Pat_AdjBillInfo.TransactionType;

            SqlParameter Trans_Flag = cmd.Parameters.Add("@TransFlag", SqlDbType.Char, 1);
            Trans_Flag.Value = Pat_AdjBillInfo.TransactionFlag;

            SqlParameter Trans_Amt = cmd.Parameters.Add("@TransAmt", SqlDbType.VarChar);
            Trans_Amt.Value = Pat_AdjBillInfo.TransactionAmount;

            SqlParameter Trans_Det = cmd.Parameters.Add("@TransDet", SqlDbType.VarChar);
            Trans_Det.Value = Pat_AdjBillInfo.TransactionDetails;

            SqlParameter Last_ModifiedBy = cmd.Parameters.Add("@LastModifiedBy", SqlDbType.VarChar);
            Last_ModifiedBy.Value = Pat_AdjBillInfo.User;

            SqlParameter Trans_SysFlag = cmd.Parameters.Add("@Trans_SysFlag", SqlDbType.VarChar,50);
            Trans_SysFlag.Value = Pat_AdjBillInfo.TransSysFlag;

            sqlCon.Open();
            cmd.ExecuteNonQuery();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Inserting Patient Billing Info.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Inserting Patient Med Info.", ex);
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }
    #endregion


}



    

    


