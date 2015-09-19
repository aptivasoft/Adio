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
using NLog;

public partial class Patient_EmployeeList : System.Web.UI.Page
{
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    string conStr = ConfigurationManager.AppSettings["conStr"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        GVList.EnableSortingAndPagingCallbacks = true;
        GV_BindData();

    }
    protected void GVList_DataBound(object sender, EventArgs e)
    {

    }
    protected void GVList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVList.PageIndex = e.NewPageIndex;
        GV_BindData();
    }

    private void GV_BindData()
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            //string sqlQuery = "select e.Emp_ID, (e.Emp_LName+','+SPACE(1)+e.Emp_FName) As EmpName,e.Emp_Status,(e.Emp_Address+','+e.Emp_Address2+ ','+ e.Emp_City+','+ e.Emp_State+','+e.Emp_Zip) As EmpAddress,e.Emp_Phone As Phone,r.Role as EmpRole,f.Facility_Name as Location,t.Title_Name as Title from Employee e , Facility_Info f,Titles t,Roles r where e.Emp_LocID = f.Facility_ID and e.Emp_TitleID = t.Title_ID and e.Emp_Role = r.Role_Code order by EmpName";

            SqlCommand sqlCmd = new SqlCommand("sp_getEmpList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsDocList = new DataSet();
            DataView dvDocList = new DataView();
            sqlDa.Fill(dsDocList, "Employee");
            GVList.DataSource = dsDocList.Tables["Employee"];
            GVList.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }


    }

}