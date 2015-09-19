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
/// Summary description for TimeSheetDAL
/// </summary>
public class TimeSheetDAL
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    SqlConnection con;
	public TimeSheetDAL()
	{
		//
		// TODO: Add constructor logic here
		//
         con= new SqlConnection(conStr);
	}
    public string GetLocation(TimeSheetDetails Emp_Det)
    {
        SqlCommand cmd = new SqlCommand("sp_GetLocation", con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        try
        {
            SqlParameter LocId = cmd.Parameters.Add("@Loc_Id", SqlDbType.Int);
            LocId.Value = Emp_Det.LocID;
            SqlDataAdapter da_empData = new SqlDataAdapter(cmd);
            da_empData.Fill(ds, "EMPDATA");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return ds.Tables["EMPDATA"].Rows[0][0].ToString();
    }
    public DataTable GetEmpDetails(TimeSheetDetails Emp_Det)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand("select * from Employee where Emp_ID=" + Emp_Det.EMP_ID, con);
            SqlDataAdapter da_empData = new SqlDataAdapter(cmd);
            da_empData.Fill(ds, "EMPDATA");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return ds.Tables["EMPDATA"];
    }
    public string GetEmpLocation(TimeSheetDetails Emp_Det)
    {        
        SqlCommand cmd = new SqlCommand("select* from Locations where Loc_ID=" + Emp_Det.LocID, con);
        con.Open();
        string flag = "";
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                flag= dr[0].ToString();
            else
                flag = String.Empty;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }
    public int GetEmpID(TimeSheetDetails Emp_Det)
    {
        SqlCommand cmd = new SqlCommand("select Emp_ID from Users where User_Id='" + Emp_Det.User_ID+"'", con);
        con.Open();
        int flag = 0;
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                flag= int.Parse(dr[0].ToString());
            else
               flag= 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }
    public bool CheckTimeSheetSubmitDate(TimeSheetDetails Emp_Det)
    {
        SqlCommand cmd = new SqlCommand("select TS_SubmitDate from Timesheets where Timesheets.TS_EmployeeId=" + Emp_Det.EMP_ID, con);
        con.Open();
        bool flag = false;
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr[0].ToString() == String.Empty)
                {
                    flag= true;
                }
                else
                    flag= false;
            }
            else
                flag= false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }
    public bool IsSubmit(TimeSheetDetails Emp_Det)
    {
        SqlCommand cmd = new SqlCommand("select TS_SubmitDate from Timesheets where Timesheets.TS_EmployeeId=" + Emp_Det.EMP_ID+" and TS_PPD="+Emp_Det.PayPeriodID, con);
        con.Open();
        bool flag = false;
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr[0].ToString()!=String.Empty)
                    flag= true;
                else
                    flag= false;
            }
            else
                flag= false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }
    public string GetActivityName(int TsCode)
    {
        string flag = "";
        try
        {
            SqlCommand cmd = new SqlCommand("select Activity_Name from Activities where Activity_Code=" + TsCode, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                flag= dr[0].ToString();
            }
            else
                flag= "";

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }


    public string GetAbsentName(int TsCode)
    {
        string flag = "";
        try
        {
            SqlCommand cmd = new SqlCommand("select Activity_Name from Absent_Info where Absent_Code=" + TsCode, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                flag= dr[0].ToString();
            }
            else
                flag= "";

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }

    public DataTable GetDateDuration(TimeSheetDetails Emp_Det)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand("sp_GetDateDuration", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter PPID = cmd.Parameters.Add("@PPID", SqlDbType.Int);
            PPID.Value = Emp_Det.PayPeriodID;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "DateDuration");

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        if (ds.Tables[0].Rows.Count > 0)
            return ds.Tables[0];
        else
            return null;
    }
    public int GetEmployeeTSID(TimeSheetDetails Emp_Det)
    {
        SqlCommand cmd = new SqlCommand("select TS_PPD from Timesheets where Timesheets.TS_EmployeeId=" + Emp_Det.EMP_ID, con);
        con.Open();
        int flag = 0;
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                flag = int.Parse(dr[0].ToString());
            }
            else
                flag = 0;
                
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }
    public int GetAbsentCode(string Activity_Name)
    {
        int flag = 0;
        SqlCommand cmd = new SqlCommand("select Absent_Code from Absent_Info where Activity_Name='" + Activity_Name + "'", con);
        con.Open();
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                flag= int.Parse(dr[0].ToString());
            }
            else
                flag= 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }
    public int GetActivetyCode(string ActivetyName)
    {
        SqlCommand cmd = new SqlCommand("select Activity_Code from Activities where Activity_Name='" + ActivetyName+"'", con);
        con.Open();
        int flag=0;
        try
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                flag= int.Parse(dr[0].ToString());
            }
            else
                flag= 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }

    public DataTable Get_EmpTimeSheetDetails(TimeSheetDetails TS_Det)
    {
        DataSet ds_TSDet = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getEMPTimeSheetDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            SqlParameter TSID = cmd.Parameters.Add("@TSID", SqlDbType.Int);
            TSID.Value = TS_Det.TimeSheetID;

            //SqlParameter PPID = cmd.Parameters.Add("@PPID", SqlDbType.Int);
            //PPID.Value = TS_Det.PayPeriodID;

           

            da.Fill(ds_TSDet, "TSDet");

            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return ds_TSDet.Tables["TSDet"];
       
    }
    public string getStatus(TimeSheetDetails TS_Det)
    {
        SqlCommand cmd = new SqlCommand("select TS_Status from Timesheets where TS_Id=" + TS_Det.TimeSheetID, con);
        SqlDataReader dr;
        string flag = "";
        try
        {
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                flag= dr[0].ToString();
            }
            else
                flag= null;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        return flag;
    }
    public DataTable Get_EmpTimeSheetID(TimeSheetDetails TS_Det)
    {
        //[sp_getTimeSheetID]
        DataSet ds = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getTimeSheetID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter empId = cmd.Parameters.Add("@empId", SqlDbType.Int);
            empId.Value = TS_Det.EMP_ID;

            SqlParameter PPID = cmd.Parameters.Add("@PPID", SqlDbType.Int);
            PPID.Value = TS_Det.PayPeriodID;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

           
            da.Fill(ds, "TSData");

           

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return ds.Tables[0];
            //con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    return Int32.Parse(dr[0].ToString());
            //}
            //else
            //    return 0;
       

    }



    //string[] Date,string[] hrs,string[] OTOUT,string[]  AbsCode,string[] ActivityCode,string[] Comments,
    public string set_EMPTimeSheets(TimeSheetDetails TS_Det)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_setTimeSheets", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter empId = cmd.Parameters.Add("@empId", SqlDbType.Int);
            empId.Value = TS_Det.EMP_ID;

            SqlParameter PPID = cmd.Parameters.Add("@PPID", SqlDbType.Int);
            PPID.Value = TS_Det.PayPeriodID;

            SqlParameter StartDate = cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            StartDate.Value = TS_Det.TSStartDate;

            SqlParameter EndDate = cmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
            EndDate.Value = TS_Det.TSEndDate;

            SqlParameter Status = cmd.Parameters.Add("@Status", SqlDbType.Char);
            @Status.Value = TS_Det.Status;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return "Successfully added.";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            return  ex.Message;
        }
    }
    public string SubmitTimeSheetDetails(string[] Date, string[] hrs, string[] OTOUT, string[] AbsCode, string[] ActivityCode, string[] Comments, TimeSheetDetails TS_Det)
    {
        string flag="";
        try
        {
            for (int i = 0; i < Date.Length; i++)
            {
                SqlCommand cmd = new SqlCommand("sp_SubmitTimeSheetDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter EmpID = cmd.Parameters.Add("@EmpID", SqlDbType.Int);
                EmpID.Value = TS_Det.EMP_ID;

                SqlParameter PPID = cmd.Parameters.Add("@PPID", SqlDbType.Int);
                PPID.Value = TS_Det.PayPeriodID;

                    SqlParameter TSID = cmd.Parameters.Add("@TSID", SqlDbType.Int);
                    TSID.Value = TS_Det.TimeSheetID;

                    SqlParameter TSDate = cmd.Parameters.Add("@Date", SqlDbType.Date);
                    TSDate.Value = Date[i];

                    SqlParameter TSAbsCode = cmd.Parameters.Add("@AbsCode", SqlDbType.Int);
                    if (GetAbsentCode(AbsCode[i]) == 0)
                    {
                        TSAbsCode.Value = Convert.DBNull;
                    }
                    else
                    {
                        TSAbsCode.Value = GetAbsentCode(AbsCode[i]);
                    }

                    SqlParameter TSActivityCode = cmd.Parameters.Add("@ActvCode", SqlDbType.Int);
                    if (GetActivetyCode(ActivityCode[i]) == 0)
                    {
                        TSActivityCode.Value = Convert.DBNull;
                    }
                    else
                    {
                        TSActivityCode.Value = GetActivetyCode(ActivityCode[i]);
                    }

                    SqlParameter TSHrs = cmd.Parameters.Add("@Hrs", SqlDbType.Float);
                    if (hrs[i] != String.Empty)
                        TSHrs.Value = float.Parse(hrs[i]);
                    else
                        TSHrs.Value = Convert.DBNull;

                    SqlParameter TsOTOut = cmd.Parameters.Add("@OTOut", SqlDbType.Float);
                    if (OTOUT[i] != String.Empty)
                        TsOTOut.Value = float.Parse(OTOUT[i]);
                    else
                        TsOTOut.Value = Convert.DBNull;

                    SqlParameter TSCmnts = cmd.Parameters.Add("@Cmnts", SqlDbType.VarChar, 255);
                    if (Comments[i] != String.Empty)
                        TSCmnts.Value = Comments[i];
                    else
                        TSCmnts.Value = Convert.DBNull;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
            }//'" + DateTime.Parse(Date[i]) + "'
           flag= "Successfully Submited";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
         
        }
        return flag;

    }
    //[sp_updateTimeSheetDetails]

    public string updateTimeSheetDetails(string[] Date, string[] hrs, string[] OTOUT, string[] AbsCode, string[] ActivityCode, string[] Comments, TimeSheetDetails TS_Det)
    {
        string flag = "";
        try
        {
            for (int i = 0; i < Date.Length; i++)
            {
                SqlCommand cmd = new SqlCommand("sp_updateTimeSheetDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter TSID = cmd.Parameters.Add("@TSID", SqlDbType.Int);
                TSID.Value = TS_Det.TimeSheetID;

                SqlParameter TSDate = cmd.Parameters.Add("@Date", SqlDbType.Date);
                TSDate.Value = Date[i];

                SqlParameter TSAbsCode = cmd.Parameters.Add("@AbsCode", SqlDbType.Int);
                if (GetAbsentCode(AbsCode[i]) == 0)
                {
                    TSAbsCode.Value = Convert.DBNull;
                }
                else
                {
                    TSAbsCode.Value = GetAbsentCode(AbsCode[i]);
                }

                SqlParameter TSActivityCode = cmd.Parameters.Add("@ActvCode", SqlDbType.Int);
                if (GetActivetyCode(ActivityCode[i]) == 0)
                {
                    TSActivityCode.Value = Convert.DBNull;
                }
                else
                {
                    TSActivityCode.Value = GetActivetyCode(ActivityCode[i]);
                }

                SqlParameter TSHrs = cmd.Parameters.Add("@Hrs", SqlDbType.Float);
                if (hrs[i] != String.Empty)
                    TSHrs.Value = float.Parse(hrs[i]);
                else
                    TSHrs.Value = Convert.DBNull;

                SqlParameter TsOTOut = cmd.Parameters.Add("@OTOut", SqlDbType.Float);
                if (OTOUT[i] != String.Empty)
                    TsOTOut.Value = float.Parse(OTOUT[i]);
                else
                    TsOTOut.Value = Convert.DBNull;

                SqlParameter TSCmnts = cmd.Parameters.Add("@Cmnts", SqlDbType.VarChar, 255);
                if (Comments[i] != String.Empty)
                    TSCmnts.Value = Comments[i];
                else
                    TSCmnts.Value = Convert.DBNull;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }//'" + DateTime.Parse(Date[i]) + "'
            flag= "Successfully Submited";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            return ex.Message;

        }
        return flag;
    }


    public string saveTimeSheetDetails(string[] Date, string[] hrs, string[] OTOUT, string[] AbsCode, string[] ActivityCode, string[] Comments, TimeSheetDetails TS_Det)
    {
        string flag = "";
        
        try
        {
            for (int i = 0; i < Date.Length; i++)
            {
                SqlCommand cmd = new SqlCommand("sp_setTimeSheetDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter TSID = cmd.Parameters.Add("@TSID", SqlDbType.Int);
                TSID.Value = TS_Det.TimeSheetID;

                SqlParameter TSDate = cmd.Parameters.Add("@Date", SqlDbType.Date);
                TSDate.Value = Date[i];

                SqlParameter TSAbsCode = cmd.Parameters.Add("@AbsCode", SqlDbType.Int);
                if (GetAbsentCode(AbsCode[i]) == 0)
                {
                    TSAbsCode.Value = Convert.DBNull;
                }
                else
                {
                    TSAbsCode.Value = GetAbsentCode(AbsCode[i]);
                }

                SqlParameter TSActivityCode = cmd.Parameters.Add("@ActvCode", SqlDbType.Int);
                if (GetActivetyCode(ActivityCode[i]) == 0)
                {
                    TSActivityCode.Value = Convert.DBNull;
                }
                else
                {
                    TSActivityCode.Value = GetActivetyCode(ActivityCode[i]);
                }

                SqlParameter TSHrs = cmd.Parameters.Add("@Hrs", SqlDbType.Float);
                if (hrs[i] != String.Empty)
                    TSHrs.Value = float.Parse(hrs[i]);
                else
                    TSHrs.Value = Convert.DBNull;

                SqlParameter TsOTOut = cmd.Parameters.Add("@OTOut", SqlDbType.Float);
                if (OTOUT[i] != String.Empty)
                    TsOTOut.Value = float.Parse(hrs[i]);
                else
                    TsOTOut.Value = Convert.DBNull;

                SqlParameter TSCmnts = cmd.Parameters.Add("@Cmnts", SqlDbType.VarChar, 255);
                if (Comments[i] != String.Empty)
                    TSCmnts.Value = Comments[i];
                else
                    TSCmnts.Value = Convert.DBNull;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }//'" + DateTime.Parse(Date[i]) + "'
            flag= "Successfully Submited";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
           
        }
        return flag;
    }
    public DataTable getEmpTimeSheetDetails(TimeSheetDetails TS_Det)
    {
        DataSet ds_TSEmpData = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Timesheets where TS_EmployeeId=" + TS_Det.EMP_ID, con);
          
            da.Fill(ds_TSEmpData, "DSEMPDATA");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);

        }
        return ds_TSEmpData.Tables[0];
    }

}
