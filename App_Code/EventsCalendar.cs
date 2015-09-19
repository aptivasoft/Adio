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

/// <summary>
/// Summary description for Events_Calender
/// </summary>
public class EventsCallendar
{
	public EventsCallendar()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _eventID;
    private string _eventDate;
    private string _eventTime;
    private string _eventInfo;
    private string _clinic;
    private string _facility;
    private string _aByName;
    private string _annDate;
    private string _expDate;
    private string _message;
    private string _duration;

    public String Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }

    public String AnnBy_Name
    {
        get { return _aByName; }
        set { _aByName = value; }
    }


    public String AnnouncementDate
    {
        get { return _annDate; }
        set { _annDate = value; }
    }

    public String ExpDate
    {
        get { return _expDate; }
        set { _expDate = value; }
    }

    public String Message
    {
        get { return _message; }
        set { _message = value; }
    }



    public Int32 EventID
    {
        get { return _eventID; }
        set { _eventID = value; }
    }
    
    public String EventDate
    {
        get { return _eventDate; }
        set { _eventDate = value; }
    }


    public String EventTime
    {
        get { return _eventTime; }
        set { _eventTime = value; }
    }

    public String EventInfo
    {
        get { return _eventInfo; }
        set { _eventInfo = value; }
    }

    public String Clinic
    {
        get { return _clinic; }
        set { _clinic = value; }
    }

    public String Facility
    {
        get { return _facility; }
        set { _facility = value; }
    }

}
