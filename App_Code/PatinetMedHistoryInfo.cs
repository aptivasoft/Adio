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
/// Summary description for PatinetMedHistoryInfo
/// </summary>
public class PatinetMedHistoryInfo
{
	public PatinetMedHistoryInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private DateTime _rxDate;
    private String _madicine;
    private String _sig;
    private Int32 _qty;
    private Int32 _reFills;
    private Int32 _cmID;
    private Int32 _patID;
    private Int32 _docID;
    private Int32 _facID;
    private String _docFullName;
    private DateTime _RxDate;
    private String _app_Date;
    private String _app_Time;
    private String _app_Note;
    private String _app_Purp;
    private Char _app_Type;
    private Char _app_Status;
    private DateTime _app_DateTime;
    private Int32 _rxitemID;
    private DateTime _shipDate;
    private String _TrackingNo;
    private String _deliveryMode;
    private String _shipDetails;
    private String _shipAddress;
    private String _deliveryConfrm;
    private String _deliverystat;

    public String DeliveryStatus
    {
        get { return _deliverystat; }
        set { _deliverystat = value; }
    }
    public String DeliveryConfirm
    {
        get { return _deliveryConfrm; }
        set { _deliveryConfrm = value; }
    }

    public String ShippingAdd
    {
        get { return _shipAddress; }
        set { _shipAddress = value; }
    }



    public String ShippingDetails
    {
        get { return _shipDetails; }
        set { _shipDetails = value; }
    }
    public String DeliveryMode
    {
        get { return _deliveryMode; }
        set { _deliveryMode = value; }
    }

    public String TrackingNo
    {
        get { return _TrackingNo; }
        set { _TrackingNo = value; }
    }


    public DateTime ShipDate
    {
        get { return _shipDate; }
        set { _shipDate = value; }
    }


    public Int32 RXItemID
    {
        get { return _rxitemID; }
        set { _rxitemID = value; }
    }

    public DateTime AppDateTime
    {
        get { return _app_DateTime; }
        set { _app_DateTime = value; }   
    }
    public Char AppStatus
    {
        get { return _app_Status; }
        set { _app_Status = value; }    
    }

    public Char AppointmentType
    {
        get { return _app_Type; }
        set { _app_Type = value; }
    }
    public String AppoitmentDate
    {
        get { return _app_Date; }
        set { _app_Date = value; }    
    }
    public String AppointmentTime
    {
        get { return _app_Time; }
        set { _app_Time = value; }
    }
    public String App_Note
    {
        get { return _app_Note; }
        set { _app_Note = value; }
    }
    public String App_Purp
    {
        get { return _app_Purp; }
        set { _app_Purp = value; }
    }

    public String Doc_FullName
    {
        get { return _docFullName; }
        set { _docFullName = value; }
    }
    public DateTime Rx_Date
    {
        get { return _rxDate; }
        set { _rxDate = value; }
    }
    public Int32 Doc_ID
    {
        get { return _docID; }
        set { _docID = value; }
    }

    public Int32 Fac_ID
    {
        get { return _facID; }
        set { _facID = value; }
    }
    public Int32 Pat_ID
    {
        get { return _patID; }
        set { _patID = value; }
    }

    public Int32 CM_ID
    {
        get { return _cmID; }
        set { _cmID = value; }
    }

    public DateTime RxDate
    {
        get { return _rxDate; }
        set { _rxDate = value; }
    }

    public String Madicine
    {
        get { return _madicine; }
        set { _madicine = value; }
    }

    public String SIG
    {
        get { return _sig; }
        set { _sig = value; }
    }

    public Int32 Quantity
    {
        get { return _qty; }
        set { _qty = value; }
    }

    public Int32 Refills
    {
        get { return _reFills; }
        set { _reFills = value; }
    }
}
