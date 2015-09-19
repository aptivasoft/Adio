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

public partial class ProvidersList : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        GVDocList.EnableSortingAndPagingCallbacks = true;
        GVDoc_BindData();

    }
    protected void GVDocList_DataBound(object sender, EventArgs e)
    {

    }
    protected void GVDocList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDocList.PageIndex = e.NewPageIndex;
        GVDoc_BindData();
    }

    private void GVDoc_BindData()
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            string sqlQuery = "select (d.Doc_LName + ','+SPACE(1)+ d.Doc_FName) As ProviderName,c.Clinic_Name As Location,(d.Doc_Address1+ ','+ d.Doc_Address2+','+d.Doc_City+','+ d.Doc_State+','+d.Doc_Zip) As ProviderAddress,d.Doc_CPhone As CellPhone, d.Doc_Speciality as Speciality from Doctor_Info d , Clinic_info c where d.Clinic_ID = c.Clinic_ID order by Doc_LName,Doc_FName";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsDocList = new DataSet();
            DataView dvDocList = new DataView();
            sqlDa.Fill(dsDocList, "DoctorList");
            GVDocList.DataSource = dsDocList.Tables["DoctorList"];
            GVDocList.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

}
