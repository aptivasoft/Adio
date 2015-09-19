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
//using Microsoft.ReportingServices.ReportRendering;
using Microsoft.Reporting.WebForms;
//using ReportingServiceRender.MyReportService;
using Microsoft.SqlServer.ReportingServices2005;
public partial class ReportTimesheet : System.Web.UI.Page 
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            string sqlQuery = "";// "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
            //if (Session["Role"].ToString() == "D")
            //    sqlQuery = "Select Clinic_Name,Clinic_ID from Clinic_info where Clinic_ID=(Select Clinic_ID from Doctor_Info where UserID='" + Session["User"].ToString() + "');Select Facility_Name,Facility_ID from Facility_Info where Clinic_ID=(Select Clinic_ID from Doctor_Info where UserID='" + Session["User"].ToString() + "');";
            //else
            sqlQuery = "Select [PP_ID],'Pay Period (' + convert(varchar,convert(date,[PP_StartDate],0),1) + ' - ' + + convert(varchar,convert(date,[PP_EndDate],0),1) as pp from PayPeriods where [PP_EndDate]<getdate() order by PP_StartDate desc";

            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);


            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsPPList = new DataSet();
            try
            {
                
                
                    sqlDa.Fill(dsPPList);
                    ddlPP.DataSource = dsPPList.Tables[0];
                    ddlPP.DataTextField = "pp";
                    ddlPP.DataValueField = "PP_ID";
                    ddlPP.DataBind();

                  
            }
            catch (Exception ex)
            {

            }



            int date = int.Parse(DateTime.Now.Day.ToString()) - 1;
            //helper.ApplyGroupSort();
            //txtDate1.Text = DateTime.Now.AddDays(-date).ToString("MM/dd/yyyy");
            //txtDate2.Text = DateTime.Now.ToString("MM/dd/yyyy");
          //  Filldata(int.Parse(d.SelectedValue), 0, txtDate1.Text, txtDate2.Text);
            Filldata(int.Parse(ddlPP.SelectedValue));



        }


        //ReportViewer2..RefreshReport(); 
    }
    public DataTable GetData(int PPID)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

        SqlCommand sqlCmd = new SqlCommand("sp_ReportTimeSheet", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_PPID = sqlCmd.Parameters.Add("@PPID", SqlDbType.Int);
        sp_PPID.Value = PPID;

        SqlParameter sp_Date1 = sqlCmd.Parameters.Add("@SDate", SqlDbType.Date);
        sp_Date1.Direction = ParameterDirection.Output;
        SqlParameter sp_Date2 = sqlCmd.Parameters.Add("@EDate", SqlDbType.Date);
        sp_Date2.Direction = ParameterDirection.Output;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsPatient = new DataSet();
        try
        {
            sqlDa.Fill(dsPatient);
            ReportParameter Date1 = new ReportParameter("FromDate", sp_Date1.Value.ToString());
            ReportParameter Date2 = new ReportParameter("ToDate", sp_Date2.Value.ToString());
            ReportParameter[] rp = new ReportParameter[] { Date1, Date2 };

            ReportViewer2.LocalReport.SetParameters(rp);


        }
        catch (Exception ex)
        {

        }
        return dsPatient.Tables[0];
        //eCareXdb_NewDataSetTableAdapters.sp_ReportNewPrescriptionTableAdapter db = new eCareXdb_NewDataSetTableAdapters.sp_ReportNewPrescriptionTableAdapter();
        //return db.GetData(6, 166, DateTime.Parse("7/7/2009"), DateTime.Parse("8/8/2009"));

    }

    protected void ddlPP_DataBound(object sender, EventArgs e)
    {

        //ddlPP.Items.Insert(0, new ListItem("Select", "0"));

        //ddlPP.SelectedIndex = 0;

    }
    //protected void ddlOrganization_DataBound(object sender, EventArgs e)
    //{

    //    if (Session["Role"].ToString() != "D")
    //    {
    //        ddlOrganization.Items.Insert(0, new ListItem("All Organizations", "0"));

    //        ddlOrganization.SelectedIndex = 0;
    //    }


    //}
    //protected void ddlLocation_DataBound(object sender, EventArgs e)
    //{

    //    ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));

    //    ddlLocation.SelectedIndex = 0;

    //}
    //protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    SqlConnection sqlCon = new SqlConnection(conStr);

    //    //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

    //    SqlCommand sqlCmd = new SqlCommand("Select Facility_Name,Facility_ID from Facility_Info where Clinic_ID= " + ddlOrganization.SelectedValue.ToString() + " order by Facility_Name", sqlCon);
    //    //sqlCmd.CommandType = CommandType.StoredProcedure;



    //    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
    //    DataSet dsFacilityList = new DataSet();
    //    try
    //    {
    //        sqlDa.Fill(dsFacilityList, "FacilityList");
    //        ddlLocation.DataSource = dsFacilityList;
    //        ddlLocation.DataTextField = "Facility_Name";
    //        ddlLocation.DataValueField = "Facility_ID";
    //        ddlLocation.DataBind();
    //        // Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate1.Text, txtDate2.Text);
    //    }

    //    catch (Exception ex)
    //    {

    //    }
    //}
    //protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    // Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate1.Text, txtDate2.Text);
    //}
    protected void btnRxReport_Click(object sender, EventArgs e)
    {
        Filldata(int.Parse(ddlPP.SelectedValue));

    }
    protected void Filldata(int PPID)
    {
        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSetLowCost");
        //rds.Name = "table1";
        //rds.Value = GetData(PPID);
        ReportViewer2.LocalReport.ReportPath = "Reports/RptTimesheet.rdlc";
        SqlConnection sqlCon = new SqlConnection(conStr);

        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

        SqlCommand sqlCmd = new SqlCommand("sp_ReportTimeSheet", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_PPID = sqlCmd.Parameters.Add("@PPID", SqlDbType.Int);
        sp_PPID.Value = PPID;

        SqlParameter sp_Date1 = sqlCmd.Parameters.Add("@SDate", SqlDbType.Date);
        sp_Date1.Direction = ParameterDirection.Output;
        SqlParameter sp_Date2 = sqlCmd.Parameters.Add("@EDate", SqlDbType.Date);
        sp_Date2.Direction = ParameterDirection.Output;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsPatient = new DataSet();
        try
        {
            sqlDa.Fill(dsPatient);
            
            ReportParameter Date1 = new ReportParameter("FromDate", sp_Date1.Value.ToString());
            ReportParameter Date2 = new ReportParameter("ToDate", sp_Date2.Value.ToString());
            ReportParameter[] rp = new ReportParameter[] { Date1, Date2 };

            ReportViewer2.LocalReport.SetParameters(rp);
            rds.Value = dsPatient.Tables[0];


        }
        catch (Exception ex)
        {

        }

        //ReportViewer2.LocalReport.SetParameters(rp);
        //ReportViewer2.LocalReport.SetParameters(new ReportParameter("Date2", "8/8/2009"));
        ReportViewer2.LocalReport.DataSources.Clear();
        ReportViewer2.LocalReport.DataSources.Add(rds);
        ReportViewer2.LocalReport.Refresh();
        //ReportViewer2.DataBind();

    }
   
   

}
