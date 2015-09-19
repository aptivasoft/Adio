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

public partial class Patient_ActivityCodesList : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            GVList.EnableSortingAndPagingCallbacks = true;
            GV_BindData();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void GVList_DataBound(object sender, EventArgs e)
    {

    }
    protected void GVList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVList.PageIndex = e.NewPageIndex;
            GV_BindData();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    private void GV_BindData()
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            string sqlQuery = "Select Activity_Code,Activity_Name,Activity_Description from Activities";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsDocList = new DataSet();
            DataView dvDocList = new DataView();
            sqlDa.Fill(dsDocList, "Activities");
            GVList.DataSource = dsDocList.Tables["Activities"];
            GVList.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }

}
