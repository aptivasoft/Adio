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
using Microsoft.Reporting.WebForms;
using NLog;

public partial class ReportMedLog : System.Web.UI.Page 
{
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

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
                //ddlOrganization.DataSource = dsClinicList;
                //ddlOrganization.DataTextField = "Clinic_Name";
                //ddlOrganization.DataValueField = "Clinic_ID";
                //ddlOrganization.DataBind();
                //if (dsClinicList.Tables[0].Rows.Count < 2)
                //{
                    //ddlOrganization.Items.RemoveAt(0);
                //}
                //else
                //{
                    //ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));
                    //ddlLocation.SelectedIndex = 0;
                //}
                //ddlOrganization.SelectedIndex = 0;

                //BindLocation(ddlOrganization.SelectedValue);
            }
            catch (Exception ex)
            {
                objNLog.Error("Error: " + ex.Message);
            }

            int date = int.Parse(DateTime.Now.Day.ToString()) - 1;
           
            //helper.ApplyGroupSort();
            if (Request.QueryString["patID"] != null)
                Filldata(0, 0, Request.QueryString["patID"].ToString());
        }
}

    //protected void BindLocation(string clinicID)
    //{
    //    SqlConnection sqlCon = new SqlConnection(conStr);
    //    SqlCommand sqlCmd = new SqlCommand("sp_getFacilities", sqlCon);
    //    sqlCmd.CommandType = CommandType.StoredProcedure;
    //    SqlParameter sp_ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
    //    sp_ClinicID.Value = clinicID;

    //    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
    //    DataSet dsFacilityList = new DataSet();
    //    try
    //    {
    //        //ddlLocation.DataTextField = "Facility_Name";
    //        //ddlLocation.DataValueField = "Facility_ID";
    //        //sqlDa.Fill(dsFacilityList, "FacilityList");
    //        //ddlLocation.DataSource = dsFacilityList;
    //        //ddlLocation.DataBind();
    //        if(Request.QueryString["patID"]!=null)
    //            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), Request.QueryString["patID"].ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        objNLog.Error("Error: " + ex.Message);
    //    }
    //}

    public DataTable GetData(int ClinicID, int FacilityID, string PatID )
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_ReportMedLog", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_LocID = sqlCmd.Parameters.Add("@LocId", SqlDbType.Int);
        sp_LocID.Value = FacilityID;
        SqlParameter sp_PatID = sqlCmd.Parameters.Add("@PatId", SqlDbType.Int);
        sp_PatID.Value = int.Parse(PatID);

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsPatient = new DataSet();
        try
        {
            sqlDa.Fill(dsPatient, "patDetails");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error: " + ex.Message);
        }
        return dsPatient.Tables["patDetails"];
    }

    //protected void ddlOrganization_DataBound(object sender, EventArgs e)
    //{
    //        ddlOrganization.Items.Insert(0, new ListItem("All Organizations", "0"));
    //        ddlOrganization.SelectedIndex = 0;
    //}

    //protected void ddlLocation_DataBound(object sender, EventArgs e)
    //{
    //    ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));
    //    ddlLocation.SelectedIndex = 0;
    //}

    //protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindLocation(ddlOrganization.SelectedValue);
    //}

    //protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   // Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate1.Text, txtDate2.Text);
    //}

    //protected void btnRxReport_Click(object sender, EventArgs e)
    //{
    //   if(Request.QueryString["patID"]!=null)
    //       Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), Request.QueryString["patID"].ToString());
    //}

    protected void Filldata(int ClinicID, int FacilityID, string PatientID)
    {
        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("eCareXdb_NewDataSet_sp_ReportMedLog");
        DataTable dtPatInfo =GetData(ClinicID, FacilityID, PatientID);
        rds.Value = dtPatInfo;
       
        ReportViewer2.LocalReport.ReportPath = "Reports/RptMedLog.rdlc";
        ReportParameter PatID = new ReportParameter("PatientID", PatientID);
        ReportParameter[] rp = new ReportParameter[] {PatID};
        
        ReportViewer2.LocalReport.SetParameters(rp);
        ReportViewer2.LocalReport.DataSources.Clear();
        ReportViewer2.LocalReport.DataSources.Add(rds);
        ReportViewer2.LocalReport.Refresh();
    }
}
