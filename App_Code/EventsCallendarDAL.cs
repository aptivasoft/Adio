using System;
using System.Data;
using System.Configuration;
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
using NLog;
/// <summary>
/// Summary description for EventsCallendarDAL
/// </summary>
public class EventsCallendarDAL
{
	public EventsCallendarDAL()
	{
		
	}
    string ConStr = ConfigurationManager.AppSettings["ConStr"].ToString();
    
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    
    public DataSet GetClinic()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlDataAdapter sqlda = new SqlDataAdapter("select Clinic_Name,Clinic_ID from Clinic_Info", con);

            sqlda.Fill(ds, "Clinic");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return ds;
    }

    public DataSet GetFacility(EventsCallendar ev_cal)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlDataAdapter sqlda = new SqlDataAdapter("select Facility_Name,Facility_ID from Facility_Info where Clinic_ID = '" + ev_cal.Clinic + "'", con);
            sqlda.Fill(ds, "Facility");
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        return ds;
    }



    public string Ins_EventInfo(EventsCallendar ev_cal,string postedBy,string userID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_set_EventInfo";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            
            SqlParameter facility = sqlCmd.Parameters.Add("@Facility", SqlDbType.Int);
            facility.Value = Convert.ToInt32(ev_cal.Facility);
            
            SqlParameter EventInfo = sqlCmd.Parameters.Add("@EventInfo", SqlDbType.VarChar,255);
            EventInfo.Value = ev_cal.EventInfo;

            SqlParameter EventDate = sqlCmd.Parameters.Add("@EventDate", SqlDbType.DateTime);
            EventDate.Value = ev_cal.EventDate;

            SqlParameter EDuration = sqlCmd.Parameters.Add("@EventDuration", SqlDbType.NVarChar,50);
            EDuration.Value = ev_cal.Duration;

            SqlParameter PostedBy = sqlCmd.Parameters.Add("@PostedBy", SqlDbType.NVarChar,50);
            PostedBy.Value = postedBy;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
        return "Event Information Inserted Successfully...";       
    
    }

    public string Ins_Announcements(EventsCallendar ev_cal, string postedBy,string userID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandText = "sp_set_AnnouncementInfo";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            
            SqlParameter facility = sqlCmd.Parameters.Add("@Facility", SqlDbType.Int);
            facility.Value = Convert.ToInt32(ev_cal.Facility);

            SqlParameter Message = sqlCmd.Parameters.Add("@Message", SqlDbType.VarChar, 255);
            Message.Value = ev_cal.Message;

            SqlParameter Announcement_Date = sqlCmd.Parameters.Add("@Announcement_Date", SqlDbType.Date);
            Announcement_Date.Value = ev_cal.AnnouncementDate;

            SqlParameter ExpiryDate = sqlCmd.Parameters.Add("@ExpiryDate", SqlDbType.Date);
            ExpiryDate.Value = ev_cal.ExpDate;

            SqlParameter AnnBy = sqlCmd.Parameters.Add("@AnnBy", SqlDbType.NVarChar, 50);
            AnnBy.Value = ev_cal.AnnBy_Name;

            SqlParameter PostedBy = sqlCmd.Parameters.Add("@PostedBy", SqlDbType.NVarChar, 50);
            PostedBy.Value = postedBy;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();
        }

        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
            return ex.Message;
        }
        return "Announcement Information Added Successfully...";

    }
}
