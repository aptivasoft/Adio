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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.XPath;
using NLog;

public partial class Patient_PatAppointments : System.Web.UI.Page
{
    PatinetMedHistoryInfo Pat_Details = new PatinetMedHistoryInfo();
    
    string conStr = ConfigurationManager.AppSettings["conStr"];
    PatientInfoDAL objPatInfo = new PatientInfoDAL();
    Property objProp = new Property();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    //GetDoctor Names
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names = Pat_Info.get_PatDoctorNames(prefixText, contextKey);
        foreach (DataRow dr in Pat_Names.Rows)
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString(), dr[2].ToString()));
        }
        return Pat_List.ToArray();
    }
    
    //GetFacility Names
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetClinicFaclityNames(string prefixText, int count)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names = Pat_Info.get_ClinicFacilityNames(prefixText);
        foreach (DataRow dr in Pat_Names.Rows)
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
        }
        return Pat_List.ToArray();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                DateTime timeSpan = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0, DateTimeKind.Utc);
                int timeDur = 15;
                lbappointmentTime.Items.Add(timeSpan.Add(TimeSpan.FromMinutes(0)).ToString("HH:mm tt"));
                for (int i = 1; i <= 44; i++)
                {
                    lbappointmentTime.Items.Add(timeSpan.Add(TimeSpan.FromMinutes(timeDur)).ToString("HH:mm tt"));
                    timeDur += 15;
                }
                Filldata();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    public void fill_AppointmentsGrid(string DocID, string date)
    {
        objNLog.Info("Function Started..");
        try
        {
            objProp.DocID = DocID;
            objProp.ApptDate = date;
            DataSet ds_Appointments = objPatInfo.GetPatientAppointments(objProp);
            gridApointments.DataSource = ds_Appointments.Tables["Appointments"];
            gridApointments.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error: " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
   
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        // Subit Click
        objNLog.Info("Event Started..");
        try
        {
            PatientInfoDAL Pat_Info = new PatientInfoDAL();
            Pat_Details.App_Note = txtNotes.Text;
            Pat_Details.App_Purp = txtAppPurpose.Text;
            Pat_Details.Fac_ID = Pat_Info.Get_FacID(txtFacility.Text);
            Pat_Details.Doc_ID = Pat_Info.Get_DocID(txtDoc.Text);
            char AppType;
            if (rbtnCSR.Checked == true)
            {
                AppType = 'C';
            }
            else
            {
                AppType = 'D';
            }
            Pat_Details.AppointmentType = AppType;
            Pat_Details.AppoitmentDate = txtDate.Text;
            Pat_Details.AppointmentTime = lbappointmentTime.SelectedItem.ToString();
            Pat_Details.AppStatus = 'S';
            Pat_Details.Pat_ID = (int)Session["Pat_ID"];
            string userID = (string)Session["User"];
            string Stat=Pat_Info.set_PatientAppointments(userID,Pat_Details);
            string str = "alert('" + Stat + "');";
            ScriptManager.RegisterStartupScript(btnSubmit, typeof(Page), "alert", str, true);
            fill_AppointmentsGrid(hidDocID.Value, txtDate.Text);

            Response.Redirect("AllPatientProfile.aspx?patID=" + Request.QueryString["patID"].ToString());
        }
        catch (Exception ex)
        {
            string str = "alert('Problem In Adding ...');";
            ScriptManager.RegisterStartupScript(btnSubmit, typeof(Page), "alert", str, true);
            objNLog.Error("Error : " + ex.Message);
        }
        clearTextboxes();
        objNLog.Info("Event Completed..");
    }
    
    protected void clearTextboxes()
    {
        objNLog.Info("Function Started..");
        try
        {
            txtAppPurpose.Text = String.Empty;
            txtDate.Text = String.Empty;
            txtDoc.Text = String.Empty;
            txtFacility.Text = String.Empty;
            txtNotes.Text = String.Empty;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
    
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            fill_AppointmentsGrid(hidDocID.Value, txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    
    protected void gridApointments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            gridApointments.PageIndex = e.NewPageIndex;
            fill_AppointmentsGrid(hidDocID.Value, txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    protected void gridApointments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            fill_AppointmentsGrid(hidDocID.Value, txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    
    protected void gridApointments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            PatientInfoDAL Pat_Info = new PatientInfoDAL();
            if (e.CommandName == "Delete")
            {
                string userID = (string)Session["User"];
                Pat_Info.delete_PatAppointments(userID,e.CommandArgument.ToString());
                fill_AppointmentsGrid(hidDocID.Value, txtDate.Text);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AllPatientProfile.aspx?patID=" + Request.QueryString["patID"].ToString());
    }

    protected void Filldata()
    {
        objNLog.Info("Function Started..");
        try
        {
            PatientInfoDAL Pat_Info = new PatientInfoDAL();
            DataTable dtPatientDetails = new DataTable();


            AutoCompleteExtender1.ContextKey = Request.QueryString["patID"].ToString();
            dtPatientDetails = Pat_Info.get_Patient_Details(Request.QueryString["patID"].ToString());
            if (dtPatientDetails.Rows.Count > 0)
            {

                lblPatAccount1.Text = dtPatientDetails.Rows[0]["Pat_LName"].ToString() + ", " + dtPatientDetails.Rows[0]["Pat_FName"].ToString();


                int Pat_FacId = int.Parse(dtPatientDetails.Rows[0]["Facility_ID"].ToString());

                txtDoc.Text = dtPatientDetails.Rows[0]["Pat_PDoc"].ToString();

                hidDocID.Value = dtPatientDetails.Rows[0]["Doc_ID"].ToString();
                txtFacility.Text = Pat_Info.Get_Facility(Pat_FacId)[0].ToString();
                txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                fill_AppointmentsGrid(hidDocID.Value, txtDate.Text);

            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
    
}
