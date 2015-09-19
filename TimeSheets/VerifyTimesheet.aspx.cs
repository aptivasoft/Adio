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
using System.Data.SqlClient;
using System.Net.Mail;
using NLog;
using Adio.UALog;

public partial class VerifyTimesheet : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    private UserActivityLog objUALog = new UserActivityLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!IsPostBack)
        {
            Filldata();
        }
    }

    protected void Filldata()
    {
        objNLog.Info("Function Started..");
        try
        {
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_getTimeSheet", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter par_PPID = sqlCmd.Parameters.Add("@PPID", SqlDbType.Int);
        par_PPID.Value = Request.QueryString["PPID"];
        SqlParameter par_EmpID = sqlCmd.Parameters.Add("@empId", SqlDbType.Int);
        par_EmpID.Value = Request.QueryString["empID"];
        SqlParameter par_THours = sqlCmd.Parameters.Add("@THours", SqlDbType.Int);
        par_THours.Direction = ParameterDirection.Output;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsTimesheet = new DataSet();
        
            sqlDa.Fill(dsTimesheet);
            gridTimeSheetList.DataSource = dsTimesheet.Tables[0];
            gridTimeSheetList.DataBind();
            Label lb;
            lb = (Label)gridTimeSheetList.FooterRow.FindControl("lblTHours");
            lb.Text = par_THours.Value.ToString();
            if (dsTimesheet.Tables[1].Rows.Count > 0)
            {

                lblempName1.Text = dsTimesheet.Tables[1].Rows[0]["empName"].ToString();
                lblPhone1.Text = dsTimesheet.Tables[1].Rows[0]["Emp_Phone"].ToString();
                lblMailId1.Text = dsTimesheet.Tables[1].Rows[0]["Emp_Email"].ToString();
                lblSupervisor1.Text = dsTimesheet.Tables[1].Rows[0]["Sup1"].ToString();
                AltlblSupervisor1.Text = dsTimesheet.Tables[1].Rows[0]["Sup2"].ToString();
               
            }
            if (dsTimesheet.Tables[2].Rows.Count > 0)
            {
                if (dsTimesheet.Tables[2].Rows[0][0].ToString() != "C" && dsTimesheet.Tables[2].Rows[0][0].ToString() != "")
                {
                    lblSubmittedby.Text = "<b>Submitted By:</b> " + lblempName1.Text;
                    lblSubmittedDate.Text = "<b>Date/Time :</b> " + dsTimesheet.Tables[2].Rows[0][1].ToString();
                    if (dsTimesheet.Tables[2].Rows[0][0].ToString() == "A")
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtComments.Enabled = false;
                        lblApprovedby.Text = "<b>Approved/Rejected By:</B> " + dsTimesheet.Tables[2].Rows[0][2].ToString();
                        lblApprovedDate.Text = "<b>Date/Time :</B> " + dsTimesheet.Tables[2].Rows[0][3].ToString();
                    }
                    if (dsTimesheet.Tables[2].Rows[0][0].ToString() == "R")
                    {
                        
                        lblApprovedby.Text = "<b>Approved/Rejected By:</B> " + dsTimesheet.Tables[2].Rows[0][2].ToString();
                        lblApprovedDate.Text = "<b>Date/Time :</B> " + dsTimesheet.Tables[2].Rows[0][3].ToString();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
   
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        SqlConnection sqlCon = new SqlConnection(conStr);
        string userID = (string)Session["User"];

        SqlCommand sqlCmd = new SqlCommand("Update Timesheets SET TS_Status='A' ,TS_ApprovedDate=getdate(), TS_ApprovedBy='" + (string)Session["User"] + "',TS_Comments='" + (string)txtComments.Text  + "' where TS_EmployeeId='" + (string)Request.QueryString["empID"] + "' and TS_PPD='" + (string)Request.QueryString["PPID"] + "'", sqlCon);

        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objUALog.LogUserActivity(conStr, userID, "Approved Time Sheet. [TS_PPD]=" + (string)Request.QueryString["PPID"], "Timesheets",0);
            Response.Redirect("TimeSheetReport.aspx");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        objNLog.Info("Event Completed..");
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("Update Timesheets SET TS_Status='R',TS_ApprovedDate=getdate(),TS_Comments='" + (string)txtComments.Text + "'  where TS_EmployeeId='" + (string)Request.QueryString["empID"] + "' and TS_PPD='" + (string)Request.QueryString["PPID"] + "'", sqlCon);
        string userID = (string)Session["User"];
        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objUALog.LogUserActivity(conStr, userID, "Rejected Time Sheet. [TS_PPD]=" + (string)Request.QueryString["PPID"], "Timesheets",0);
            Response.Redirect("TimeSheetReport.aspx");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        { 
            sqlCon.Close(); 
        }
        objNLog.Info("Event Completed..");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TimeSheetReport.aspx");
    }
    protected string getEmpID(string UserName)
    {
        objNLog.Info("Function Started..");
        string empID="";
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
