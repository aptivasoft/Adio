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

public partial class InsuranceList : System.Web.UI.Page
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
            string sqlQuery = "select Ins_Name as InsuranceName, Ins_Number as InsNumber, Ins_Company as Company, (Ins_Address1+','+Ins_Address2+','+Ins_City+','+Ins_State) as InsAddress,Ins_Phone as Phone from Insurance_Info order by InsuranceName";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsDocList = new DataSet();
            DataView dvDocList = new DataView();
            sqlDa.Fill(dsDocList, "insuranceList");
            GVList.DataSource = dsDocList.Tables["InsuranceList"];
            GVList.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }


    }

}