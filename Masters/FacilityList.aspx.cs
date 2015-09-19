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
public partial class Patient_FacilityList : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
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
            string sqlQuery = "select f.Facility_Name As FacilityName,f.Facility_Code as FacilityCode,(f.Facility_Address+ ','+ f.Facility_City+','+ f.Facility_State+','+f.Facility_Zip) As Address,f.Facility_TPhone As Phone,c.Clinic_Name As Clinic from Facility_Info f , Clinic_info c where f.Clinic_ID = c.Clinic_ID order by FacilityName";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsDocList = new DataSet();
            DataView dvDocList = new DataView();
            sqlDa.Fill(dsDocList, "FacilityList");
            GVList.DataSource = dsDocList.Tables["FacilityList"];
            GVList.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }

}