using System;
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
using System.Collections.Generic;
using System.Text;
using System.IO;
using NLog;
//using Microsoft.ReportingServices.ReportRendering;
using Microsoft.Reporting.WebForms;
//using ReportingServiceRender.MyReportService;
using Microsoft.SqlServer.ReportingServices2005;
public partial class ReportSample : System.Web.UI.Page 
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
            ddlStatus.DataBind();
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getClinics", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_UserID.Value = (string)Session["User"];

            SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
            sp_UserRole.Value = (string)Session["Role"];



            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsClinicList = new DataSet();
            try
            {

                sqlDa.Fill(dsClinicList, "ClinicList");
                ddlOrganization.DataSource = dsClinicList;
                ddlOrganization.DataTextField = "Clinic_Name";
                ddlOrganization.DataValueField = "Clinic_ID";
                ddlOrganization.DataBind();
                if (dsClinicList.Tables[0].Rows.Count < 2)
                {
                    ddlOrganization.Items.RemoveAt(0);


                }
                else
                {
                    ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));
                    ddlLocation.SelectedIndex = 0;
                }
                ddlOrganization.SelectedIndex = 0;

                bindLocation(ddlOrganization.SelectedValue);


            }
            catch (Exception ex)
            {

            }





            int date = int.Parse(DateTime.Now.Day.ToString()) - 1;
            //helper.ApplyGroupSort();
            txtDate1.Text = DateTime.Now.AddDays(-date).ToString("MM/dd/yyyy");
            txtDate2.Text = DateTime.Now.ToString("MM/dd/yyyy");
            Filldata(int.Parse(ddlOrganization.SelectedValue), 0, ddlStatus.SelectedValue, txtDate1.Text, txtDate2.Text);



        }


        //ReportViewer2..RefreshReport(); 
    }
    protected void bindLocation(string clinicID)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getFacilities", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp_ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
        sp_ClinicID.Value = clinicID;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsFacilityList = new DataSet();
        try
        {
            ddlLocation.DataTextField = "Facility_Name";
            ddlLocation.DataValueField = "Facility_ID";
            sqlDa.Fill(dsFacilityList, "FacilityList");
            ddlLocation.DataSource = dsFacilityList;
            ddlLocation.DataBind();
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), ddlStatus.SelectedValue, txtDate1.Text, txtDate2.Text);
        }

        catch (Exception ex)
        {

        }
    }
    public DataTable GetDate(int ClinicID, int FacilityID, string rxstatus, string date1, string date2)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

        SqlCommand sqlCmd = new SqlCommand("sp_ReportSample", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_OrgID = sqlCmd.Parameters.Add("@OrgId", SqlDbType.Int);
        sp_OrgID.Value = ClinicID;
        SqlParameter sp_LocID = sqlCmd.Parameters.Add("@LocId", SqlDbType.Int);
        sp_LocID.Value = FacilityID;
        SqlParameter sp_Date1 = sqlCmd.Parameters.Add("@Date1", SqlDbType.Date);
        sp_Date1.Value = date1;
        SqlParameter sp_Date2 = sqlCmd.Parameters.Add("@Date2", SqlDbType.Date);
        sp_Date2.Value = date2;
        SqlParameter sp_status = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.VarChar);
        sp_status.Value = rxstatus;


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
    protected void ddlOrganization_DataBound(object sender, EventArgs e)
    {

        
            ddlOrganization.Items.Insert(0, new ListItem("All Organizations", "0"));

            ddlOrganization.SelectedIndex = 0;
       

    }
    protected void ddlStatus_DataBound(object sender, EventArgs e)
    {
        ddlStatus.Items.Insert(0, new ListItem("All", "%"));
        ddlStatus.SelectedIndex = 0;
    }
    protected void ddlLocation_DataBound(object sender, EventArgs e)
    {

        ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));

        ddlLocation.SelectedIndex = 0;

    }
    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindLocation(ddlOrganization.SelectedValue);
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate1.Text, txtDate2.Text);
    }
    protected void btnRxReport_Click(object sender, EventArgs e)
    {
        Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), ddlStatus.SelectedValue, txtDate1.Text, txtDate2.Text);

    }
    protected void Filldata(int ClinicID, int FacilityID, string rxstatus, string date1, string date2)
    {
        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DS_ReportSample_sp_ReportSample");
        //rds.Name = "table1";
        rds.Value = GetDate(ClinicID, FacilityID, rxstatus, date1, date2);
        ReportViewer2.LocalReport.ReportPath = "Reports/RptSample.rdlc";

        ReportParameter Date1 = new ReportParameter("FromDate", date1);
        ReportParameter Date2 = new ReportParameter("ToDate", date2);
        ReportParameter[] rp = new ReportParameter[] { Date1, Date2 };

        ReportViewer2.LocalReport.SetParameters(rp);
        //ReportViewer2.LocalReport.SetParameters(rp);
        //ReportViewer2.LocalReport.SetParameters(new ReportParameter("Date2", "8/8/2009"));
        ReportViewer2.LocalReport.DataSources.Clear();
        ReportViewer2.LocalReport.DataSources.Add(rds);
        ReportViewer2.LocalReport.Refresh();
        //ReportViewer2.DataBind();

    }
   
   

}
