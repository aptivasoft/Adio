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

public partial class Reports_ReportDoctorUserAccounts : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        Filldata();
    }
    protected void Filldata()
    {
        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DS_sp_ReportNewPrescription");
        rds.Value = GetData();
        rvUserLoginInfo.LocalReport.ReportPath = "Reports/RptDoctorNurseUsers.rdlc";
        rvUserLoginInfo.LocalReport.DataSources.Clear();
        rvUserLoginInfo.LocalReport.DataSources.Add(rds);
        rvUserLoginInfo.LocalReport.Refresh();
    }
    public DataTable GetData()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_GetDoctor_Nurse_PharmTech_LoginInfo", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsUsers = new DataSet();
        try
        {
            sqlDa.Fill(dsUsers, "UserLoginInfo");
        }
        catch (Exception ex)
        {

        }
        return dsUsers.Tables["UserLoginInfo"];
     }
}
