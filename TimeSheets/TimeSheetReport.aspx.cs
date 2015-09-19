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

public partial class TimeSheetReport : System.Web.UI.Page
{
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            lblHeading.Text = "TIMESHEET REPORT";

            string empID = getEmpID((string)Session["User"]);

            if (empID == "" && (string)Session["Role"] != "A")
            {
                Server.Transfer("AccessDenied.aspx");

            }
            else
            {
                Filldata(empID, null, null, null, null);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetEmpNames(string prefixText, int count)
    {
        List<string> User_List = new List<string>();
        RegisterUserDAL reg_Usr_DAL = new RegisterUserDAL();

        User_List.Clear();

        DataTable dtFac = reg_Usr_DAL.get_EmpNames_Sup(prefixText, count, (string)HttpContext.Current.Session["User"]);
        foreach (DataRow dr in dtFac.Rows)
        {
            User_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[2].ToString() + "," + dr[1].ToString(), dr[0].ToString()));
        }

        return User_List.ToArray();
    }

    protected void Filldata(string empID,string status,string SDate,string EDate,string empName)
    {
        objNLog.Info("Function Started..");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
 
            SqlCommand sqlCmd = new SqlCommand("sp_getTimeSheetReport", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_EmpID = sqlCmd.Parameters.Add("@empId", SqlDbType.Int);
            if (empID != "")
                par_EmpID.Value = empID;
            SqlParameter par_status = sqlCmd.Parameters.Add("@status", SqlDbType.Char, 1);
            if (status != null && status != "ALL")
                par_status.Value = status;
            SqlParameter par_SDate = sqlCmd.Parameters.Add("@startDate", SqlDbType.Date);
            if (SDate != null && SDate != "")
                par_SDate.Value = SDate;
            SqlParameter par_EDate = sqlCmd.Parameters.Add("@endDate", SqlDbType.Date);
            if (EDate != null && EDate != "")
                par_EDate.Value = SDate;
            SqlParameter par_EmpFName = sqlCmd.Parameters.Add("@empFName", SqlDbType.VarChar, 25);

            SqlParameter par_EmpLName = sqlCmd.Parameters.Add("@empLName", SqlDbType.VarChar, 25);
            if (empName != null && empName != "")
            {
                string[] eName = empName.Split(',');
                if (eName.Length == 2)
                {
                    par_EmpLName.Value = eName[0];
                    par_EmpFName.Value = eName[1];
                }

            }
 
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsTimesheetList = new DataSet();

            sqlDa.Fill(dsTimesheetList, "TimesheetList");

            gridTimeSheetList.DataSource = dsTimesheetList;
            gridTimeSheetList.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");

        try
        {
            string empID = getEmpID((string)Session["User"]);
            if (empID == "" && (string)Session["Role"] != "A")
            {
                Response.Redirect("AccessDenied.aspx");

            }
            else
            {
                Filldata(empID, ddlStatus.SelectedValue.ToString(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), txtEmployeeName.Text);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

 


    protected void gridTimeSheetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         objNLog.Info("Event Started..");
        if (e.CommandName == "Edit")
        {

            Response.Redirect("Timesheet.aspx?PPID=" + e.CommandArgument.ToString());
 
        }
        if (e.CommandName == "View")
        {

            Response.Redirect("Timesheet.aspx?PPID=" + e.CommandArgument.ToString() + "&mode=view");
 
        }
        objNLog.Info("Event Completed..");
    }
    

    public string display_link1(object un, object un1)
    {
        objNLog.Info("Function Started..");
        //check username here and return a bool
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
        objNLog.Info("Function Completed..");          
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


        SqlCommand sqlCmd = new SqlCommand("SELECT [Emp_ID] from [Users] where [User_Id]='" + UserName + "' and Emp_ID in (select Emp_SupID1 from Employee union select Emp_SupID2 from Employee)   ", sqlCon);

        try
        {
            sqlCon.Open();
            empID = sqlCmd.ExecuteScalar().ToString();

            sqlCon.Close();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
        return empID;

    }


    
    protected void gridTimeSheetList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((DataBinder.Eval(e.Row.DataItem, "status")).ToString() == "Rejected") //Other Pharmacy Rx
            {
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F62217");
            }
            else if ((DataBinder.Eval(e.Row.DataItem, "status")).ToString() == "Submitted")
            {
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F660AB");
            }
            else if ((DataBinder.Eval(e.Row.DataItem, "status")).ToString() == "Approved")
            {
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4AA02C");
            }
        }
    }
}
