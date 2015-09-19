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
using NLog;

public partial class Events_List : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
        GVList.EnableSortingAndPagingCallbacks = true;
        GV_BindData();
        }
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
            string sqlQuery = "select e.EventID,Case when e.FacilityID=0 then 'All Clinics' else (select f.Facility_Name from Facility_Info f where e.FacilityID = f.Facility_ID and e.FacilityID >0 ) end as FacilityName,convert(varchar,e.EventDate,101) as EventDate,substring(convert(varchar(20), e.EventDate, 9), 13, 5)+ ' ' + substring(convert(varchar(30), e.EventDate, 9), 25, 2)as EventTime,case when Datepart(hh,e.EventDate)>12 then Datepart(hh,e.EventDate)-12 else Datepart(hh,e.EventDate) end as hTime,Datepart(mi,e.EventDate) as mTime,case when Datepart(hh,e.EventDate)>=12 then 'PM' else 'AM' end as AmPmTime, e.Duration,e.EventInfo as EventDesc,e.PostedBy from Events_Calender e";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsDocList = new DataSet();
            DataView dvDocList = new DataView();
            sqlDa.Fill(dsDocList, "EventsList");
            GVList.DataSource = dsDocList.Tables["EventsList"];
            GVList.DataBind();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void GVList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GVList.EditIndex = e.NewEditIndex;
        GV_BindData();
    }
    protected void GVList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        GridViewRow row = (GridViewRow)GVList.Rows[e.RowIndex];
        Label lblEvent = (Label)row.FindControl("lblEventID");

        TextBox EventDate = (TextBox)row.FindControl("txtEventDate");
        //TextBox EventTime = (TextBox)row.FindControl("txtEventTime");
        DropDownList ddlHrs = (DropDownList)row.FindControl("ddlHrs");
        DropDownList ddlMns = (DropDownList)row.FindControl("ddlMins");
        DropDownList ddlAmPm = (DropDownList)row.FindControl("ddlAmPm");
        string eventTime="";
        if (ddlHrs.SelectedValue == "0" && ddlMns.SelectedValue == "0" && ddlAmPm.SelectedValue == "PM")
        {
            string str = "alert('Invalid Time...');";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", str, true);
        }
        else
        {
            eventTime = ddlHrs.SelectedValue + ":" + ddlMns.SelectedValue + ":00 " + ddlAmPm.SelectedValue;
        }

        EventDate.Text = EventDate.Text + " " + eventTime.ToString();
        TextBox Duration = (TextBox)row.FindControl("txtDuration");
        TextBox EventDesc = (TextBox)row.FindControl("txtEventDesc");
        GVList.EditIndex = -1;
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand("update Events_Calender set EventDate = '" +EventDate.Text+"',Duration = '"+Duration.Text+"', EventInfo = '"+EventDesc.Text+"' where EventID = '"+lblEvent.Text+"'", sqlCon);
        cmd.ExecuteNonQuery();
        sqlCon.Close();
        GV_BindData();

    }
    protected void GVList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        GridViewRow row = (GridViewRow)GVList.Rows[e.RowIndex];
        Label lblEvent = (Label)row.FindControl("lblEventID");
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand("Delete Events_Calender where EventID = '" + lblEvent.Text + "'", sqlCon);
        cmd.ExecuteNonQuery();
        sqlCon.Close();
        GV_BindData();


    }
    protected void GVList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVList.EditIndex = -1;
        GV_BindData();
        
    }
    protected void GVList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
 

    }
}
