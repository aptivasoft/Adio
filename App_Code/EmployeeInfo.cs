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
/// Summary description for EmployeeInfo
/// </summary>
public class EmployeeInfo
{
	public EmployeeInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private Int32 _empId;
    private String _Fname; 
    private String _Mname;
    private String _Lname;
    private String _empAddress1;
    private String _empAddress2;
    private String _empState;
    private String _empCity;
    private String _empZip;
    private String _phone;
    private String _hPhone;
    private String _EMPFullName;
    private String _email;
    private DateTime _HDate;
    private DateTime _TDate;
    private Int32 _locId;
    private Int32 _titleId;
    private String _role;
    private String _isApprov;
    private String _status;
    private String _comments;
    private String _title;
    private String _desc;
    private Char _timeEntryReq;
    private Int32 _empSup1;
    private Int32 _empSup2;


    public Int32 EMPSup1
    {
        get { return _empSup1; }
        set { _empSup1 = value; }
    }

    public Int32 EMPSup2
    {
        get { return _empSup2; }
        set { _empSup2 = value; }
    }


    public Char TimeEntry
    {
        get { return _timeEntryReq; }
        set { _timeEntryReq = value; }
    }

    public String Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public String TitleDesc
    {
        get { return _desc; }
        set { _desc = value; }
    }
    
    public String Comments
    {
        get { return _comments; }
        set { _comments = value; }
    }

    public String Status
    {
        get { return _status; }
        set { _status = value; }
    }
    public String IsApprove
    {
        get { return _isApprov; }
        set { _isApprov = value; }
    }
    public String Role
    {
        get { return _role; }
        set { _role = value; }
    }
    public Int32 EMPID
    {
        get { return _empId; }
        set { _empId = value; }
    }

    public String EMPFName
    {
        get { return _Fname; }
        set { _Fname = value; }
    }

    public String EMPMName
    {
        get { return _Mname; }
        set { _Mname = value; }
    }

    public String EMPLName
    {
        get { return _Lname; }
        set { _Lname = value; }
    }
    public String  Address1
    {
        get { return _empAddress1; }
        set { _empAddress1 = value; }
    }
    public String  Address2
    {
        get { return _empAddress2; }
        set { _empAddress2 = value; }
    }
    public String ZIP
    {
        get { return _empZip; }
        set { _empZip = value; }
    }
    public String City
    {
        get { return _empCity; }
        set { _empCity = value; }
    }
    public String State
    {
        get { return _empState; }
        set { _empState = value; }
    }
    public String Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }
    public String Pat_CellPhone
    {
        get { return _hPhone; }
        set { _hPhone = value; }
    }
    public String EMPFullName
    {
        get { return _EMPFullName; }
        set { _EMPFullName = value; }
    }
    public String EMail
    {
        get { return _email; }
        set { _email = value; }
    }
    public DateTime EMPHireDate
    {
        get { return _HDate; }
        set { _HDate = value; }
    }
    public DateTime EMPTermDate
    {
        get { return _TDate; }
        set { _TDate = value; }
    }
    public Int32 LocationID
    {
        get { return _locId; }
        set { _locId = value; }
    }
    public Int32 TitleID
    {
        get { return _titleId; }
        set { _titleId = value; }
    }
}
