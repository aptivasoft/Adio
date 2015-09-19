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

public partial class Reports_ReportEmpUserAccounts : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        Filldata();
    }
    protected void Filldata()
    {
        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DS_sp_ReportNewPrescription");
        //rds.Name = "table1";
        rds.Value = GetData();
        rvUserLoginInfo.LocalReport.ReportPath = "Reports/RptUsers.rdlc";


        rvUserLoginInfo.LocalReport.DataSources.Clear();
        rvUserLoginInfo.LocalReport.DataSources.Add(rds);
        rvUserLoginInfo.LocalReport.Refresh();
        //rvUserLoginInfo.DataBind();

    }
    public DataTable GetData()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

        SqlCommand sqlCmd = new SqlCommand("sp_GetEmployeeLoginInfo", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

             SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsPatient = new DataSet();
        try
        {
            sqlDa.Fill(dsPatient, "patDetails");
        }
        catch (Exception ex)
        {

        }
        return dsPatient.Tables["patDetails"];
        //eCareXdb_NewDataSetTableAdapters.sp_ReportNewPrescriptionTableAdapter db = new eCareXdb_NewDataSetTableAdapters.sp_ReportNewPrescriptionTableAdapter();
        //return db.GetData(6, 166, DateTime.Parse("7/7/2009"), DateTime.Parse("8/8/2009"));

    }
}
