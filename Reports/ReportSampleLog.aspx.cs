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
public partial class ReportSampleLog : System.Web.UI.Page 
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
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
            Filldata(int.Parse(ddlOrganization.SelectedValue), 0, ddlSType.SelectedValue, txtDate1.Text, txtDate2.Text);



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
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), ddlSType.SelectedValue, txtDate1.Text, txtDate2.Text);
        }

        catch (Exception ex)
        {

        }
    }
    public DataTable GetData(int ClinicID, int FacilityID, string rxstatus, string date1, string date2)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

        SqlCommand sqlCmd = new SqlCommand("sp_ReportDrugActivity", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_OrgID = sqlCmd.Parameters.Add("@OrgId", SqlDbType.Int);
        sp_OrgID.Value = ClinicID;
        SqlParameter sp_LocID = sqlCmd.Parameters.Add("@LocId", SqlDbType.Int);
        sp_LocID.Value = FacilityID;
        //SqlParameter sp_Date1 = sqlCmd.Parameters.Add("@Date1", SqlDbType.Date);
        //sp_Date1.Value = date1;
        //SqlParameter sp_Date2 = sqlCmd.Parameters.Add("@Date2", SqlDbType.Date);
        //sp_Date2.Value = date2;
        SqlParameter sp_dtype = sqlCmd.Parameters.Add("@dType", SqlDbType.VarChar);
        sp_dtype.Value = rxstatus;


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

    public DataTable GetInventData(int DrugID, int FacID, string DType, string date1, string date2)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

        SqlCommand sqlCmd = new SqlCommand("sp_ReportDrugActivityLog", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_drugID = sqlCmd.Parameters.Add("@drugID", SqlDbType.Int);
        sp_drugID.Value = DrugID;
        SqlParameter sp_dType = sqlCmd.Parameters.Add("@type", SqlDbType.VarChar);
        sp_dType.Value = DType;
        SqlParameter sp_facilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
        sp_facilityID.Value = FacID;
        SqlParameter sp_Date1 = sqlCmd.Parameters.Add("@Date1", SqlDbType.Date);
        sp_Date1.Value = date1;
        SqlParameter sp_Date2 = sqlCmd.Parameters.Add("@Date2", SqlDbType.Date);
        sp_Date2.Value = date2;


        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsDrugInventory = new DataSet();
        try
        {
            sqlDa.Fill(dsDrugInventory, "SInvDetails");
        }
        catch (Exception ex)
        {

        }
        return dsDrugInventory.Tables["SInvDetails"];
        //eCareXdb_NewDataSetTableAdapters.sp_ReportNewPrescriptionTableAdapter db = new eCareXdb_NewDataSetTableAdapters.sp_ReportNewPrescriptionTableAdapter();
        //return db.GetData(6, 166, DateTime.Parse("7/7/2009"), DateTime.Parse("8/8/2009"));

    }
    protected void ddlOrganization_DataBound(object sender, EventArgs e)
    {

        
            ddlOrganization.Items.Insert(0, new ListItem("All Organizations", "0"));

            ddlOrganization.SelectedIndex = 0;
       

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
        Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), ddlSType.SelectedValue, txtDate1.Text, txtDate2.Text);

    }
    protected void Filldata(int ClinicID, int FacilityID, string rxstatus, string date1, string date2)
    {
        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet4_sp_ReportDrugActivity");
        //rds.Name = "table1";
        if (rxstatus != "P" && rxstatus != "S")
            rxstatus = "%";
        

        rds.Value = GetData(ClinicID, FacilityID, rxstatus, date1, date2);
        ReportViewer2.Reset();
        ReportViewer2.LocalReport.ReportPath = "Reports/RptSLog.rdlc";

        ReportParameter Date1 = new ReportParameter("FromDate", date1);
        ReportParameter Date2 = new ReportParameter("ToDate", date2);
        ReportParameter[] rp = new ReportParameter[] { Date1, Date2 };

        ReportViewer2.LocalReport.EnableHyperlinks = true;   
        ReportViewer2.LocalReport.SetParameters(rp);
        //ReportViewer2.LocalReport.SetParameters(rp);
        //ReportViewer2.LocalReport.SetParameters(new ReportParameter("Date2", "8/8/2009"));
        ReportViewer2.LocalReport.DataSources.Clear();
        ReportViewer2.LocalReport.DataSources.Add(rds);
        ReportViewer2.LocalReport.Refresh();
        //ReportViewer2.DataBind();

    }



    protected void ReportViewer2_Drillthrough(object sender, DrillthroughEventArgs e)
    {
        Microsoft.Reporting.WebForms.ReportDataSource rdis = new Microsoft.Reporting.WebForms.ReportDataSource("dsDInventLog_sp_ReportDrugActivityLog");
        //rds.Name = "table1";
        int rdrugID=0, rfacID=0;
        String rdrugName="", rclinName="", rdType="S";
        String date1="", date2="";
        LocalReport report = (LocalReport)e.Report;
        //ReportViewer2.LocalReport.ReportPath = "Reports/RptSInventLog.rdlc";

        IList<ReportParameter> list = report.OriginalParametersToDrillthrough;

        int i = 0;
        foreach (ReportParameter param in list) {

            switch (i)
            {
                case 0:
                    rdrugID = Convert.ToInt32(param.Values[0].ToString());
                    break;
                case 1:
                    rdrugName = param.Values[0].ToString();
                    break;

                case 2:
                    rdType = param.Values[0].ToString();
                    break;
                case 3:
                    rfacID = Convert.ToInt32(param.Values[0].ToString());
                    break;
                case 4:
                    rclinName = param.Values[0].ToString();
                    break;

                case 5:
                    date1 = param.Values[0].ToString();
                    break;
                case 6:
                    date2 = param.Values[0].ToString();
                    break;
            }
            i++;
        }

        rdis.Value = GetInventData(rdrugID, rfacID, rdType, date1, date2);
        ReportParameter DrugID = new ReportParameter("DrugID", rdrugID.ToString());
        ReportParameter DType = new ReportParameter("DName", rdrugName);
        ReportParameter DName = new ReportParameter("DType", rdType);
        ReportParameter FacID = new ReportParameter("FacID", rfacID.ToString());
        ReportParameter CName = new ReportParameter("CName", rclinName);
        ReportParameter Date1 = new ReportParameter("FromDate", date1);
        ReportParameter Date2 = new ReportParameter("ToDate", date2);
        ReportParameter[] rp = new ReportParameter[] { DrugID, DType, FacID, Date1, Date2 };

        report.SetParameters(rp);
        //ReportViewer2.LocalReport.SetParameters(rp);
        //ReportViewer2.LocalReport.SetParameters(new ReportParameter("Date2", "8/8/2009"));
        report.DataSources.Add(rdis);
        //report.LocalReport.DataSources.Clear();
        //report.LocalReport.DataSources.Add(rds);
        //report.LocalReport.Refresh();
        //ReportViewer2.DataBind();

      }
}
