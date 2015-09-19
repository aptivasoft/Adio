using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NLog;

public partial class TimeSheetList : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            lblHeading.Text = "VIEW/EDIT TIMESHEETS";
            string empID = getEmpID((string)Session["User"]);
            if (empID == "")
            {
                Server.Transfer("AccessDenied.aspx");
            }
            else
            {
                Filldata(empID);
            }
        }
        catch(Exception ex) 
        {
                objNLog.Error("Error : " +ex.Message);
         }
    }

    protected void Filldata(string empID)
    {
        objNLog.Info("Function Started..");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getTimeSheetList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_EmpID = sqlCmd.Parameters.Add("@empId", SqlDbType.Int);
            par_EmpID.Value = empID;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsTimesheetList = new DataSet();
            sqlDa.Fill(dsTimesheetList, "TimesheetList");
            gridTimeSheetList.DataSource = dsTimesheetList;
            gridTimeSheetList.DataBind();
        }
        catch(Exception ex) 
       {
                objNLog.Error("Error : " +ex.Message);
       }
        objNLog.Info("Function Completed..");
    }

    protected void gridTimeSheetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            if (e.CommandName == "Edit")
            {

                Response.Redirect("Timesheet.aspx?PPID=" + e.CommandArgument.ToString());
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
            if (e.CommandName == "View")
            {

                Response.Redirect("Timesheet.aspx?PPID=" + e.CommandArgument.ToString() + "&mode=view");
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    
    public string display_link1(object un, object un1)
    {
        //check username here and return a bool
        //objNLog.Info("Function Started..");
        int i=0, j=0;
        try
        {
            string s = un.ToString();
            string s1 = un1.ToString();
            if (s != "")
            {
                i = int.Parse(s);
            }
            if (s1 != "")
            {
                j = int.Parse(s1);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        //objNLog.Info("Function Completd..");
         return (i+j).ToString();
    }

    public bool display_link(string un,int i)
    {
        //check username here and return a bool
        if (i == 0)
        {
            if (un == "OPEN")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (i == 1)
        {
            if (un == "Rejected" || un == "Saved")
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        if (i == 2)
        {
            if (un == "OPEN")
            {
                return false ;
            }
            else
            {
                return true ;
            }


        }
        return false;

    }

    protected string getEmpID(string UserName)
    {
        objNLog.Info("Function Started..");
        string empID = "";
        
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("SELECT [Emp_ID] from [Users] where [User_Id]='" + UserName + "'", sqlCon);

        try
        {
            sqlCon.Open();
            empID = sqlCmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        objNLog.Info("Function Completed..");
        return empID;

    }
}
