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
/// Summary description for TimeSheetDetails
/// </summary>
public class TimeSheetDetails
{
	public TimeSheetDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private Int32 _empTSID;
    private Int32 _empID;
    private Int32 _timeSID;
    private Int32 _ppid;
    private DateTime _TDDate;
    private String _location;
    private Int32 _locID;
    private String _userID;
    private DateTime _subDate;
    private string _state;
    private string _zip;
    private DateTime _sdate;
    private DateTime _edate;
    private Char _status;
    private Int32 _TSID;

    public Int32 TimeSheetID
    {
        get { return _TSID; }
        set { _TSID = value; }
    }
    
    public Char Status
    {
        get { return _status; }
        set { _status = value; }
    }
    public DateTime TSEndDate
    {
        get { return _edate; }
        set { _edate = value; }
    }
    public DateTime TSStartDate
    {
        get { return _sdate; }
        set { _sdate = value; }
    }
    

    public Int32 PayPeriodID
    {
        get { return _ppid; }
        set { _ppid = value; }
    }
    public String User_ID
    {
        get { return _userID; }
        set { _userID = value; }
    }
    public Int32 LocID
    {
        get { return _locID; }
        set { _locID = value; }
    }
    public String Location
    {
        get { return _location; }
        set { _location = value; }
    }
    public Int32 EMP_TSID
    {
        get { return _empTSID; }
        set { _empTSID = value; }
    }
    public Int32 EMP_ID
    {
        get { return _empID; }
        set { _empID = value; }
    }
    //public Int32 TimeSheetID
    //{
    //    get { return _timeSID; }
    //    set { _timeSID = value; }
    //}
    public DateTime TimeSheet_SubmitDate
    {
        get { return _subDate; }
        set { _subDate = value; }
    }
    public DateTime TDDate
    {
        get { return _TDDate; }
        set { _TDDate = value; }
    }





}
