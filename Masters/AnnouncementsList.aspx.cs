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

public partial class AnnouncementsList : System.Web.UI.Page
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
            string sqlQuery = "select a.Announcement_ID,Case when a.FacilityID=0 then 'All Clinics' else (select f.Facility_Name from Facility_Info f where a.FacilityID = f.Facility_ID and a.FacilityID >0) end  as FacilityName,convert(varchar,a.Announcement_Date,101) as Announcement_Date,a.Ann_Message as Message,convert(varchar,a.Expiry_Date,101) as Expiry_Date,a.AnnouncementBy,a.PostedBy from Announcements a ";
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsDocList = new DataSet();
            DataView dvDocList = new DataView();
            sqlDa.Fill(dsDocList, "AnnouncementsList");
            GVList.DataSource = dsDocList.Tables["AnnouncementsList"];
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
        Label lblAnnouncement = (Label)row.FindControl("lblAnnouncementID");

        TextBox AnnDate = (TextBox)row.FindControl("txtAnnDate");

        TextBox ExpiryDate = (TextBox)row.FindControl("txtExpiryDate");

        TextBox Message = (TextBox)row.FindControl("txtMessage");
        GVList.EditIndex = -1;
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand("update Announcements set Announcement_Date = '" + AnnDate.Text + "',Expiry_Date = '" + ExpiryDate.Text + "', Ann_Message = '" + Message.Text + "' where Announcement_ID = '" + lblAnnouncement.Text + "'", sqlCon);
        cmd.ExecuteNonQuery();
        sqlCon.Close();
        GV_BindData();

    }
    protected void GVList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        GridViewRow row = (GridViewRow)GVList.Rows[e.RowIndex];
        Label lblAnnouncement = (Label)row.FindControl("lblAnnouncementID");
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand("Delete Announcements where Announcement_ID = '" + lblAnnouncement.Text + "'", sqlCon);
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
