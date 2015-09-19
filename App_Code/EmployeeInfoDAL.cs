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
/// <summary>
/// Summary description for EmployeeInfoDAL
/// </summary>
public class EmployeeInfoDAL
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
	public EmployeeInfoDAL() { }
    public DataTable GetEMPData(EmployeeInfo EmpInfo)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getEmployeeInfo", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_empID = sqlCmd.Parameters.Add("@EMP_ID", SqlDbType.Int);
        sp_empID.Value = EmpInfo.EMPID;
        
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsemp = new DataSet();
        try
        {
            sqlDa.Fill(dsemp, "Details");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsemp.Tables["Details"];    
    }

    public int DeleteEMPData(EmployeeInfo EmpInfo,string userID)
    {
        int flag=0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_delete_EmployeeInfo", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_empID = sqlCmd.Parameters.Add("@EMP_ID", SqlDbType.Int);
        sp_empID.Value = EmpInfo.EMPID;

        SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
        pUserID.Value = userID;
                
        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            flag = 1;
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
           
        }
        finally
        {
            sqlCon.Close();
        }
        return flag;
    }

    public DataTable get_EMPNames(string prefixText)
    {
        
        SqlConnection sqlCon = new SqlConnection(conStr);

        string emp_Fname;
        string emp_Lname;
        string sqlQuery;
        if (prefixText.Contains(","))
        {
            string[] prov_Name = prefixText.Split(',');

            emp_Lname = prov_Name[0].ToString();
            emp_Fname = prov_Name[1].TrimStart().TrimEnd().ToString();
            //string sqlQuery = "select * from Patient_Info where pat_FName like '" + prefixText + "%'";
            sqlQuery = "select Emp_FName,Emp_LName,Emp_ID from Employee where Emp_LName like '" + emp_Lname + "' and Emp_FName like '" + emp_Fname + "%'";
        }
        else
            sqlQuery = "select Emp_FName,Emp_LName,Emp_ID from Employee where Emp_LName like '" + prefixText + "%'";
      
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsEMPnames = new DataSet();
        try
        {
            sqlDa.Fill(dsEMPnames, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsEMPnames.Tables["Names"];
    }

    public DataTable get_EMPSupName(int EmpID)
    {

        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery;
        sqlQuery = "select (Emp_LName+','+Emp_FName) as Emp_SupName from Employee where Emp_ID = " + EmpID ;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsEMPname = new DataSet();
        try
        {
            sqlDa.Fill(dsEMPname, "EmpSName");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsEMPname.Tables["EmpSName"];
    }
  


    public DataTable get_SUP1Names(string prefixText, string contextKey)
    {

        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Emp_FName,Emp_LName,Emp_ID from Employee where Emp_LName like '" + prefixText + "%' and Emp_IS_Approver_Y_N = 'Y' and (Emp_LName+','+Emp_FName) != '" + contextKey + "'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsEMPnames = new DataSet();
        try
        {
            sqlDa.Fill(dsEMPnames, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsEMPnames.Tables["Names"];
    }

    public DataTable get_SUP2Names(string prefixText, string contextKey)
    {

        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Emp_FName,Emp_LName,Emp_ID from Employee where Emp_LName like '" + prefixText + "%' and Emp_IS_Approver_Y_N = 'Y' and (Emp_LName+','+Emp_FName) != '" + contextKey + "'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsEMPnames = new DataSet();
        try
        {
            sqlDa.Fill(dsEMPnames, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsEMPnames.Tables["Names"];
    }

    public int set_EmpInfo(EmployeeInfo EmpInfo,string userID)
    {
        int flag = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_set_MEmployee", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;


        SqlParameter Emp_FName = sqlCmd.Parameters.Add("@Emp_FName", SqlDbType.VarChar, 25);
        Emp_FName.Value = EmpInfo.EMPFName;

        SqlParameter Emp_MName = sqlCmd.Parameters.Add("@Emp_MName", SqlDbType.VarChar, 15);
        Emp_MName.Value = EmpInfo.EMPMName;

        SqlParameter Emp_LName = sqlCmd.Parameters.Add("@Emp_LName", SqlDbType.VarChar, 25);
        Emp_LName.Value = EmpInfo.EMPLName;

        SqlParameter Emp_Address1 = sqlCmd.Parameters.Add("@Emp_Address", SqlDbType.VarChar, 50);
        Emp_Address1.Value = EmpInfo.Address1;

        SqlParameter Emp_Address2 = sqlCmd.Parameters.Add("@Emp_Address2", SqlDbType.VarChar, 50);
        Emp_Address2.Value = EmpInfo.Address2;

        SqlParameter Emp_City = sqlCmd.Parameters.Add("@Emp_City", SqlDbType.VarChar, 50);
        Emp_City.Value = EmpInfo.City;

        SqlParameter Emp_State = sqlCmd.Parameters.Add("@Emp_State", SqlDbType.VarChar, 50);
        Emp_State.Value = EmpInfo.State;

        SqlParameter Emp_Zip = sqlCmd.Parameters.Add("@Emp_Zip", SqlDbType.VarChar, 10);
        Emp_Zip.Value = EmpInfo.ZIP;

        SqlParameter Emp_Phone = sqlCmd.Parameters.Add("@Emp_Phone", SqlDbType.VarChar, 12);
        Emp_Phone.Value = EmpInfo.Phone;

        SqlParameter Emp_HomePhone = sqlCmd.Parameters.Add("@Emp_HomePhone", SqlDbType.VarChar, 12);
        Emp_HomePhone.Value = EmpInfo.Pat_CellPhone;

        //@Emp_Email

        SqlParameter Emp_Email = sqlCmd.Parameters.Add("@Emp_Email", SqlDbType.VarChar, 50);
        Emp_Email.Value = EmpInfo.EMail;

        //@Emp_HireDate

        SqlParameter Emp_HireDate = sqlCmd.Parameters.Add("@Emp_HireDate", SqlDbType.Date);
        Emp_HireDate.Value = EmpInfo.EMPHireDate.ToShortDateString();

        SqlParameter Emp_TermDate = sqlCmd.Parameters.Add("@Emp_TermDate", SqlDbType.Date);
        DateTime value;
        if(!DateTime.TryParse(EmpInfo.EMPTermDate.ToString(),out value))
        {
            Emp_TermDate.Value = EmpInfo.EMPTermDate.ToShortDateString();
        }

        SqlParameter Emp_Sup1 = sqlCmd.Parameters.Add("@Emp_Sup1", SqlDbType.Int);
        if (EmpInfo.EMPSup1 != 0)
            Emp_Sup1.Value = EmpInfo.EMPSup1;
        SqlParameter Emp_Sup2 = sqlCmd.Parameters.Add("@Emp_Sup2", SqlDbType.Int);
        if (EmpInfo.EMPSup2 != 0)
            Emp_Sup2.Value = EmpInfo.EMPSup2;
 

        SqlParameter Emp_LocID = sqlCmd.Parameters.Add("@Emp_LocID", SqlDbType.Int);
        if(EmpInfo.LocationID!=0)
        Emp_LocID.Value = EmpInfo.LocationID;

        SqlParameter Emp_TitleID = sqlCmd.Parameters.Add("@Emp_TitleID", SqlDbType.Int);
        if(EmpInfo.TitleID!=0)
        Emp_TitleID.Value = EmpInfo.TitleID;

        //@Emp_Role
        SqlParameter Emp_Role = sqlCmd.Parameters.Add("@Emp_Role", SqlDbType.VarChar, 10);
        Emp_Role.Value = EmpInfo.Role;

        SqlParameter Isapprov = sqlCmd.Parameters.Add("@Emp_IsApporv", SqlDbType.Char, 1);
        Isapprov.Value = EmpInfo.IsApprove;

        SqlParameter Emp_Status = sqlCmd.Parameters.Add("@Emp_Status", SqlDbType.Char, 1);
        Emp_Status.Value = EmpInfo.Status;

        SqlParameter Emp_Comments = sqlCmd.Parameters.Add("@Emp_Comments", SqlDbType.Char, 255);
        Emp_Comments.Value = EmpInfo.Comments;

        SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
        pUserID.Value = userID;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

            flag = 1;
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        return flag;
    }


    public int update_EmpInfo(EmployeeInfo EmpInfo,string userID)
    {
        int flag = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);        
        SqlCommand sqlCmd = new SqlCommand("sp_update_MEmployee", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_empID = sqlCmd.Parameters.Add("@Emp_ID", SqlDbType.Int);
        sp_empID.Value = EmpInfo.EMPID;

        SqlParameter Emp_FName = sqlCmd.Parameters.Add("@Emp_FName", SqlDbType.VarChar, 25);
        Emp_FName.Value = EmpInfo.EMPFName;

        SqlParameter Emp_MName = sqlCmd.Parameters.Add("@Emp_MName", SqlDbType.VarChar, 15);
        Emp_MName.Value = EmpInfo.EMPMName;

        SqlParameter Emp_LName = sqlCmd.Parameters.Add("@Emp_LName", SqlDbType.VarChar, 25);
        Emp_LName.Value = EmpInfo.EMPLName;

        SqlParameter Emp_Address1 = sqlCmd.Parameters.Add("@Emp_Address", SqlDbType.VarChar, 50);
        Emp_Address1.Value = EmpInfo.Address1;

        SqlParameter Emp_Address2 = sqlCmd.Parameters.Add("@Emp_Address2", SqlDbType.VarChar, 50);
        Emp_Address2.Value = EmpInfo.Address2;

        SqlParameter Emp_City = sqlCmd.Parameters.Add("@Emp_City", SqlDbType.VarChar, 50);
        Emp_City.Value = EmpInfo.City;

        SqlParameter Emp_State = sqlCmd.Parameters.Add("@Emp_State", SqlDbType.VarChar, 50);
        Emp_State.Value = EmpInfo.State;

        SqlParameter Emp_Zip = sqlCmd.Parameters.Add("@Emp_Zip", SqlDbType.VarChar, 10);
        Emp_Zip.Value = EmpInfo.ZIP;

        SqlParameter Emp_Phone = sqlCmd.Parameters.Add("@Emp_Phone", SqlDbType.VarChar, 12);
        Emp_Phone.Value = EmpInfo.Phone;

        SqlParameter Emp_HomePhone = sqlCmd.Parameters.Add("@Emp_HomePhone", SqlDbType.VarChar, 12);
        Emp_HomePhone.Value = EmpInfo.Pat_CellPhone;

        //@Emp_Email

        SqlParameter Emp_Email = sqlCmd.Parameters.Add("@Emp_Email", SqlDbType.VarChar, 50);
        Emp_Email.Value = EmpInfo.EMail;

        //@Emp_HireDate

        SqlParameter Emp_HireDate = sqlCmd.Parameters.Add("@Emp_HireDate", SqlDbType.Date);
        Emp_HireDate.Value = EmpInfo.EMPHireDate.ToShortDateString();

        SqlParameter Emp_TermDate = sqlCmd.Parameters.Add("@Emp_TermDate", SqlDbType.Date);
        Emp_TermDate.Value = EmpInfo.EMPTermDate.ToShortDateString();

        SqlParameter Emp_Sup1 = sqlCmd.Parameters.Add("@Emp_Sup1", SqlDbType.Int);
        if (EmpInfo.EMPSup1 != 0)
            Emp_Sup1.Value = EmpInfo.EMPSup1;

        SqlParameter Emp_Sup2 = sqlCmd.Parameters.Add("@Emp_Sup2", SqlDbType.Int);
        if (EmpInfo.EMPSup2 != 0)
            Emp_Sup2.Value = EmpInfo.EMPSup2;

        SqlParameter Emp_LocID = sqlCmd.Parameters.Add("@Emp_LocID", SqlDbType.Int);
        if (EmpInfo.LocationID != 0)
        {
            Emp_LocID.Value = EmpInfo.LocationID;
        }
        else
            Emp_LocID.Value = Convert.DBNull;

        SqlParameter Emp_TitleID = sqlCmd.Parameters.Add("@Emp_TitleID", SqlDbType.Int);
        if (EmpInfo.TitleID != 0)
        {
            Emp_TitleID.Value = EmpInfo.TitleID;
        }
        else
            Emp_TitleID.Value = Convert.DBNull;

        //@Emp_Role
        SqlParameter Emp_Role = sqlCmd.Parameters.Add("@Emp_Role", SqlDbType.VarChar, 10);
        Emp_Role.Value = EmpInfo.Role;

        SqlParameter Isapprov = sqlCmd.Parameters.Add("@Isapprov", SqlDbType.Char, 1);
        Isapprov.Value = EmpInfo.IsApprove;

        SqlParameter Emp_Status = sqlCmd.Parameters.Add("@Emp_Status", SqlDbType.Char, 1);
        Emp_Status.Value = EmpInfo.Status;


        SqlParameter Emp_Comments = sqlCmd.Parameters.Add("@Emp_Comments", SqlDbType.Char, 255);
        Emp_Comments.Value = EmpInfo.Comments;

        SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
        pUserID.Value = userID;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

            flag = 1;
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);

        }
        finally
        {
            sqlCon.Close();
        }
        return flag;
    }
    

    public DataTable get_LocationNames(string prefixText)
    {        
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Facility_Name,Facility_Code,Facility_ID,Clinic_ID from Facility_Info where Facility_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsLocnames = new DataSet();
        try
        {
            sqlDa.Fill(dsLocnames, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsLocnames.Tables["Names"];
    }

    public DataTable get_LocationNamesByName(string prefixText)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        FacilityInfo facInfo = new FacilityInfo();
        string Fac_Search_String = prefixText;
        string[] words = Fac_Search_String.Split(',');
        facInfo.FacilityName = words[0];
        facInfo.FacilityID = words[1];

        string sqlQuery = "select Facility_ID from Facility_Info where Facility_Name = '" + facInfo.FacilityName + "' and Facility_Code = '" +facInfo.FacilityID+"'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsLocnames = new DataSet();
        try
        {
            sqlDa.Fill(dsLocnames, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsLocnames.Tables["Names"];
    }
    public DataTable get_TitleNames(string prefixText)
    {
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Title_Name,Title_ID from Titles where Title_Name like '" + prefixText + "%'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsTitlenames = new DataSet();
        try
        {
            sqlDa.Fill(dsTitlenames, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsTitlenames.Tables["Names"];
    }
    public DataTable get_TitleNamesByNames(string prefixText)
    {
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Title_Name,Title_ID from Titles where Title_Name = '" + prefixText + "'";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet dsTitlenames = new DataSet();
        try
        {
            sqlDa.Fill(dsTitlenames, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsTitlenames.Tables["Names"];
    }

    public DataTable get_LocationByID(int LocID)
    {
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select (Facility_Name+','+Facility_Code) as Facility_Name from Facility_Info where Facility_ID =" + LocID;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlDa.Fill(ds, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return ds.Tables["Names"];
    }

   


    public DataTable getUserRole()
    {
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("select * from Roles", con);
        SqlDataReader sqlDr;
        DataTable dtable = new DataTable();
        // DataRow dr;
        con.Open();
        sqlDr = sqlCmd.ExecuteReader();
        dtable.Load(sqlDr);
        con.Close();
        return dtable;
    }
    public DataTable get_TitleByID(int TitleID)
    {
        DataTable doc_names = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select Title_Name from Titles where Title_ID =" + TitleID;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlDa.Fill(ds, "Names");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return ds.Tables["Names"];
    }
    public DataTable get_EMPDataByNames(EmployeeInfo EMPInfo)
    {
        DataTable empdata = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select * from Employee where Employee.Emp_LName='" + EMPInfo.EMPLName + "' and Employee.Emp_FName='" + EMPInfo.EMPFName + "'";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sqlQuery;
        cmd.Connection = sqlCon;
 
        SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
        DataSet dsEMPnames = new DataSet();
        try
        {
            sqlDa.Fill(dsEMPnames, "Names");
            int a= dsEMPnames.Tables["Names"].Rows.Count;
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsEMPnames.Tables["Names"];
    }

    public DataTable get_EMPDataByID(int empID)
    {
        DataTable empdata = new DataTable();
        SqlConnection sqlCon = new SqlConnection(conStr);
        string sqlQuery = "select * from Employee where Employee.Emp_ID=" + empID.ToString();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sqlQuery;
        cmd.Connection = sqlCon;

        SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
        DataSet dsEMPnames = new DataSet();
        try
        {
            sqlDa.Fill(dsEMPnames, "Names");
            int a = dsEMPnames.Tables["Names"].Rows.Count;
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return dsEMPnames.Tables["Names"];
    }

    public string set_Title(EmployeeInfo EmpInfo)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_set_Title", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;


        SqlParameter Emp_Title = sqlCmd.Parameters.Add("@Title", SqlDbType.VarChar, 50);
        Emp_Title.Value = EmpInfo.Title;

        SqlParameter Emp_TDesc = sqlCmd.Parameters.Add("@TitleDesc", SqlDbType.VarChar, 255);
        Emp_TDesc.Value = EmpInfo.TitleDesc;

        SqlParameter Emp_TimeEntry = sqlCmd.Parameters.Add("@TimeEntry", SqlDbType.Char,1);
        Emp_TimeEntry.Value = EmpInfo.TimeEntry;

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            return "Title added successfully ..";
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
    }
}
