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
using System.Text;

public partial class Patient_EventsCalendar : System.Web.UI.Page
{
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if ((string)Session["Role"] == "A" || (string)Session["Role"] == "E")
        {
            if (!Page.IsPostBack)
            {
                FillClinic();
                this.Page.Form.Enctype = "multipart/form-data";
                RenderJSArrayWithCliendIds(txtDate, txtEventDesc, ddlClinicName, ddlFacilityName, rblLocation);
            }
        }
        else
        {
            Server.Transfer("../TimeSheets/AccessDenied.aspx");

        }
    }

    EventsCallendar ev_cal = new EventsCallendar();
    EventsCallendarDAL ev_calBLL = new EventsCallendarDAL();
    string Status;
    
    protected void ddlClinicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ev_cal.Clinic = ddlClinicName.SelectedValue;
            FillFacility(ev_cal);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnEInfoSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            if (rblLocation.SelectedValue != "All Clinics")
            {
                ev_cal.Clinic = ddlClinicName.SelectedValue;
                ev_cal.Facility = ddlFacilityName.SelectedValue;
            }
            else
            {
                ev_cal.Facility = "0";
            }
            ev_cal.Clinic = ddlClinicName.SelectedValue;
            ev_cal.Facility = ddlFacilityName.SelectedValue;
            ev_cal.EventInfo = txtEventDesc.Text;
            ev_cal.EventDate = txtDate.Text;
            ev_cal.Duration = txtDuration.Text;
            string posted_By = (string)Session["User"];
            ev_cal.EventTime = ddlHrs.SelectedValue + ":" + ddlMins.SelectedValue + ":00 " +ddlAmPm.SelectedValue ;
            ev_cal.EventDate = ev_cal.EventDate + " " + ev_cal.EventTime;
            Status = ev_calBLL.Ins_EventInfo(ev_cal,posted_By,userID);
            string str = "alert('" + Status + "');";
            ScriptManager.RegisterStartupScript(btnEInfoSave, typeof(Page), "alert", str, true);
            clearEventData();
            // DrugName();

        }
        catch (Exception ex)
        {
            Status = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }
    }

    
    protected void btnEInfoCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearEventData();
        }
        catch (Exception ex)
        {
             
            objNLog.Error("Error : " + ex.Message);
        }
    }



    public void FillClinic()
    {
        try
        {
            ddlClinicName.DataTextField = "Clinic_Name";
            ddlClinicName.DataValueField = "Clinic_ID";
            ddlClinicName.DataSource = (DataSet)ev_calBLL.GetClinic();
            ddlClinicName.DataBind();
            ddlFacilityName.Items.Insert(0, new ListItem("Select Facility", "0"));
            ddlFacilityName.SelectedIndex = 0;
            updatePanelEventInfo.Update();
        }
        catch (Exception ex)
        {

            objNLog.Error("Error : " + ex.Message);
        }

    }

    public void FillFacility(EventsCallendar ev_cal)
    {
        try
        {
            ddlFacilityName.DataTextField = "Facility_Name";
            ddlFacilityName.DataValueField = "Facility_ID";
            ddlFacilityName.DataSource = (DataSet)ev_calBLL.GetFacility(ev_cal);
            ddlFacilityName.DataBind();
            updatePanelEventInfo.Update();
        }
        catch (Exception ex)
        {

            objNLog.Error("Error : " + ex.Message);
        }
    }

    public void clearEventData()
    {
        try
        {
            txtDate.Text = string.Empty;
            txtEventDesc.Text = string.Empty;
            //ddlSec.ClearSelection();
            ddlHrs.ClearSelection();
            ddlMins.ClearSelection();
            ddlAmPm.ClearSelection();
            txtDuration.Text = string.Empty;
            ddlClinicName.ClearSelection();
            ddlFacilityName.Items.Clear();
            ddlFacilityName.Items.Insert(0, new ListItem("Select Facility", "0"));

            ddlFacilityName.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            objNLog.Error("Error : " + ex.Message);
        }
    }

    public void RenderJSArrayWithCliendIds(params Control[] wc)
    {
        try
        {
            if (wc.Length > 0)
            {
                StringBuilder arrClientIDValue = new StringBuilder();
                StringBuilder arrServerIDValue = new StringBuilder();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Now loop through the controls and build the client and server id's
                for (int i = 0; i < wc.Length; i++)
                {
                    arrClientIDValue.Append("\"" + wc[i].ClientID + "\",");
                    arrServerIDValue.Append("\"" + wc[i].ID + "\",");
                }
                // Register the array of client and server id to the client
                cs.RegisterArrayDeclaration("MyClientID", arrClientIDValue.ToString().Remove(arrClientIDValue.ToString().Length - 1, 1));
                cs.RegisterArrayDeclaration("MyServerID", arrServerIDValue.ToString().Remove(arrServerIDValue.ToString().Length - 1, 1));
                // Now register the method GetClientId, used to get the client id of tthe control
                cs.RegisterStartupScript(this.Page.GetType(), "key", "\nfunction GetClientId(serverId)\n{\nfor(i=0; i<MyServerID.length; i++)\n{\nif ( MyServerID[i] == serverId )\n{\nreturn MyClientID[i];\nbreak;\n}\n}\n}", true);
            }
        }
        catch (Exception ex)
        {

            objNLog.Error("Error : " + ex.Message);
        }
    }


    protected void ddlClinicName_DataBound(object sender, EventArgs e)
    {
        try
        {
            ddlClinicName.Items.Insert(0, new ListItem("Select Clinic", "0"));

            ddlClinicName.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void ddlFacilityName_DataBound(object sender, EventArgs e)
    {
        try
        {
            ddlFacilityName.Items.Insert(0, new ListItem("Select Facility", "0"));

            ddlFacilityName.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void rblLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblLocation.SelectedValue != "All Clinics")
            {
                ddlClinicName.Enabled = true;
                ddlFacilityName.Enabled = true;
            }
            else
            {
                ddlClinicName.Enabled = false;
                ddlFacilityName.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            objNLog.Error("Error : " + ex.Message);
        }
    }
    
}
