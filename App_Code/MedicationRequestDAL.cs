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
using Adio.UALog;
using NLog;
/// <summary>
/// Summary description for PatientInfoDAL
/// </summary>
public class MedicationRequestDAL
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
    public MedicationRequestDAL()
	{
		
	}

    public int Set_MedRequest(Property objProp)
    {
        objNLog.Info("Function Started with objProp as parameter.");
        SqlConnection sqlCon = new SqlConnection(conStr);
        bool successFlag = false;
        int flag = 0;
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_set_PatientMedRequest", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_patID = sqlCmd.Parameters.Add("@pat_ID", SqlDbType.Int);
            sp_patID.Value = Int32.Parse(objProp.PatID);

            SqlParameter sp_MedicationName = sqlCmd.Parameters.Add("@Medicine", SqlDbType.VarChar, 50);
            sp_MedicationName.Value = objProp.DrugName;

            SqlParameter sp_Qty = sqlCmd.Parameters.Add("@Quantity", SqlDbType.Int);
            sp_Qty.Value = Int32.Parse(objProp.DrugQty);

            SqlParameter sp_SIG = sqlCmd.Parameters.Add("@SIG", SqlDbType.VarChar, 50);
            sp_SIG.Value = objProp.DrugSIG;

            SqlParameter sp_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
            sp_DocID.Value = Int32.Parse(objProp.DocID);

            SqlParameter sp_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            sp_RxType.Value = objProp.RxType;

            SqlParameter sp_DMode = sqlCmd.Parameters.Add("@DeliveryMode", SqlDbType.Char, 1);
            sp_DMode.Value = objProp.DeliveryMode;

            SqlParameter sp_ReqType = sqlCmd.Parameters.Add("@RequestType", SqlDbType.VarChar, 10);
            sp_ReqType.Value = objProp.RequestType;

            SqlParameter sp_Comments = sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 2000);
            sp_Comments.Value = objProp.Comments;

            SqlParameter sp_PatPhone = sqlCmd.Parameters.Add("@PatPhone", SqlDbType.VarChar,25);
            sp_PatPhone.Value = objProp.Phone;

            SqlParameter sp_Phrm = sqlCmd.Parameters.Add("@Phrm", SqlDbType.VarChar, 50);
            sp_Phrm.Value = objProp.Pharmacy;

            SqlParameter sp_User = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_User.Value = objProp.UserID;
            sqlCon.Open();
             sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            successFlag = true;
            flag = 1;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while adding Med Request.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while adding Med Request.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return flag;
    }

    public DataSet GetRxItem(int rxItemID)
    {
        objNLog.Info("Function Started with rxItemID as parameter.");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsRxItem = new DataSet();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getRxItem", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_rxItemID = sqlCmd.Parameters.Add("@rxItemID", SqlDbType.Int);
            sp_rxItemID.Value = rxItemID;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            
            sqlDa.Fill(dsRxItem);
            successFlag = true;
             
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving RxItem.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving RxItem.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsRxItem;
    }

    public DataSet GetDeliveryModes()
    {
        objNLog.Info("Function Started.");
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsDeliveryModes = new DataSet();
        bool successFlag = false;
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getDeliveryModes", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsDeliveryModes);
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
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsDeliveryModes;
    }

    public DataSet GetClinics(Property objProp)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsClinicList = new DataSet();
        bool successFlag = false;
        try
        {
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("sp_getClinics", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_UserID.Value = objProp.UserID;

            SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
            sp_UserRole.Value = objProp.UserRole;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dsClinicList, "ClinicList");
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving clinic names.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving clinic names.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");
        return dsClinicList;
    }

    public string GetClinicID(int rxItemID)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsClinicList = new DataSet();
        string clinicID;
        bool successFlag = false;
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_getClinicID",sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp_rxItemID = sqlCmd.Parameters.Add("@RxItemID", SqlDbType.Int, 20);
            sp_rxItemID.Value = rxItemID;
            sqlCon.Open();
            clinicID = sqlCmd.ExecuteScalar().ToString();
            sqlCon.Close();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
            throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " while Retrieving clinic id.", SqlEx);
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            throw new Exception("**Error occured while Retrieving clinic id.", ex);
        }
        finally
        {
            objNLog.Info("Finally Block: " + successFlag);
        }

        objNLog.Info("Function Completed...");

        return clinicID;
    }
     
    //public MSGResponse insert_Medication_Request(MedicationRequest Message,string PharmacyName,string UserName)
    //{
    //    //string sqlQuery = "select p.[pat_FName], p.[pat_LName], p.[pat_DOB], p.[LastModified],pin.[PI_PolicyID],pa.[PA_Desc]  from Patient_Info p, Patient_Ins pin, Patient_Allergies pa  where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID);
    //    MSGResponse msgResponse = new MSGResponse();
    //    SqlConnection sqlCon = new SqlConnection(conStr);

    //    //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

    //    SqlCommand sqlCmd = new SqlCommand("Insert Into T_Medication_Request ([MR_RequestType],[MR_DrugName],[MR_RxDate],[MR_Dosage],[MR_Qty],[MR_SIG],[MR_Refills] ,[MR_PharmacyName] ,[MR_ProviderName],[MR_PatientName],[UserName]) Values ('" + Message.medicationRequestType + "','" + Message.medicationreq.Medicine + "','" + Message.medicationreq.RxDate + "','" + Message.medicationreq.Dosage + "','" + Message.medicationreq.Qty + "','" + Message.medicationreq.SIG + "','" + Message.medicationreq.Refills + "','" + PharmacyName + "','" + Message.providerFullName + "','" + Message.patientFullName + "','" + UserName + "')", sqlCon);
        
    //    try
    //    {
    //        sqlCon.Open();
    //        sqlCmd.ExecuteNonQuery();
    //        sqlCon.Close();
    //        //sqlDa.Fill(dsPatientIns, "patInsData");
    //        msgResponse.error = "0";
    //        msgResponse.message = "Success";
    //        msgResponse.tokenID = "";
    //        return msgResponse;
    //    }
    //    catch (Exception ex)
    //    {
    //        msgResponse.error = "1";
    //        msgResponse.message = "Failed to Add due to the error: " + ex.Message;
    //        msgResponse.tokenID = "";
    //        return msgResponse;
    //    }
       
    //}
    //public MedicationResponse[] get_Medication_Request_Status(string startDate, string endDate,string userName)
    //{
    //    //string sqlQuery = "select p.[pat_FName], p.[pat_LName], p.[pat_DOB], p.[LastModified],pin.[PI_PolicyID],pa.[PA_Desc]  from Patient_Info p, Patient_Ins pin, Patient_Allergies pa  where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID);
    //    MedicationResponse[] medResponse ;
    //    MedicationResponse medRes;
    //    Medication med = new Medication();
    //    SqlConnection sqlCon = new SqlConnection(conStr);
    //    string query="";
    //     SqlCommand sqlCmd = new SqlCommand("sp_getMedication_Request", sqlCon);
    //    sqlCmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter UserName = sqlCmd.Parameters.Add("@username", SqlDbType.VarChar, 50);
    //    UserName.Value = userName;
        

    //    //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
    //    if (startDate.Trim() != "" )
    //    {
       
    //          SqlParameter StartDate = sqlCmd.Parameters.Add("@startdate", SqlDbType.DateTime , 50);
    //    StartDate.Value = startDate;
    //    }
    //    if (endDate.Trim() != "" )
    //    {

    //        SqlParameter EndDate = sqlCmd.Parameters.Add("@enddate", SqlDbType.DateTime, 50);
    //    EndDate.Value = endDate;
            
    //    }

        
    //    DataSet ds = new DataSet();
    //    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
    //    try
    //    {
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            medResponse = new MedicationResponse[ds.Tables[0].Rows.Count];
    //            int i = 0;
    //            foreach (DataRow dr in ds.Tables[0].Rows)
    //            {
    //                medRes = new MedicationResponse();
    //                med = new Medication();
    //                med.Dosage = dr["MR_Dosage"].ToString();
    //                med.Medicine = dr["MR_DrugName"].ToString();
    //                med.Qty = int.Parse(dr["MR_Qty"].ToString());
    //                med.Refills = int.Parse(dr["MR_Refills"].ToString());
    //                med.RxDate = dr["MR_RxDate"].ToString();
    //                med.SIG = dr["MR_SIG"].ToString();
    //                medRes.medicationreq = med;
    //                medRes.medicationRequestType = dr["MR_RequestType"].ToString();
    //                if(dr["Status"].ToString()=="N")
    //                     medRes.medicationStaus ="Not Respond" ;
                    
    //                if (dr["Status"].ToString() == "A")
    //                     medRes.medicationStaus = "Approved";

    //                if (dr["Status"].ToString() == "R")
    //                    medRes.medicationStaus = "Reject";

                   
    //                medRes.providerFullName = dr["MR_ProviderName"].ToString();
    //                medRes.patientFullName = dr["MR_PatientName"].ToString();
    //                medResponse[i] = medRes;
    //                i = i + 1;
    //            }
    //            return medResponse;
    //        }
    //        else
    //        {
    //            //sqlDa.Fill(dsPatientIns, "patInsData");
    //            medResponse = new MedicationResponse[0];
    //            return medResponse;
    //        }
    //        //sqlDa.Fill(dsPatientIns, "patInsData");

            
    //    }
    //    catch (Exception ex)
    //    {
    //        medResponse = new MedicationResponse[0];
    //        return medResponse;
    //    }

    //}
    //public string ValidateUser(Adio.AuthHeader auth_header)
    //{
    //    //string sqlQuery = "select p.[pat_FName], p.[pat_LName], p.[pat_DOB], p.[LastModified],pin.[PI_PolicyID],pa.[PA_Desc]  from Patient_Info p, Patient_Ins pin, Patient_Allergies pa  where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID);
    //    MSGResponse msgResponse = new MSGResponse();
    //    SqlConnection sqlCon = new SqlConnection(conStr);

    //    //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

    //    SqlCommand sqlCmd = new SqlCommand("select password,Phrm_Name from [T_Medication_Request_Login_Info] where [User_Name]='" + auth_header.Username + "'", sqlCon);
    //    DataSet ds = new DataSet();
    //    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
    //    try
    //    {
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count == 1)
    //        {
    //            if (ds.Tables[0].Rows[0][0].ToString() == auth_header.Password)
    //            {
    //                return ds.Tables[0].Rows[0][1].ToString();
    //            }
    //            else
    //                return "-1";

    //        }
    //        else
    //        {
    //            //sqlDa.Fill(dsPatientIns, "patInsData");
                
    //            return "-1";
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        return "-1" ;
    //    }

    //}

    

}
