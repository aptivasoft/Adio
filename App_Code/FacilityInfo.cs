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
/// Summary description for FacilityInfo
/// </summary>
public class FacilityInfo
{
	public FacilityInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _facilityNo;
    private string _facilityID;
    private string _facilityName;
    private string _facilityAddress;
    private string _facilityCity;
    private string _facilityState;
    private string _facilityZip;
    private string _facilityTPhone;
    private string _facilityFax;
    private string _facilityEMail;
    private string _facilityTaxID;
    private string _facilitySpeciality;
    private string _facilityProvID;
    private char _facIsStampAddr;

    public Int32 FacilityNO
    {
        get { return _facilityNo; }
        set { _facilityNo = value; }
    }

    public String FacilityID
    {
        get { return _facilityID; }
        set { _facilityID = value; }
    }

    public String FacilityName
    {
        get { return _facilityName; }
        set { _facilityName = value; }
    }

    public String FacilityAddress
    {
        get { return _facilityAddress; }
        set { _facilityAddress = value; }
    }

    public String FacilityCity
    {
        get { return _facilityCity; }
        set { _facilityCity = value; }
    }

    public String FacilityState
    {
        get { return _facilityState; }
        set { _facilityState = value; }
    }

    public String FacilityZip
    {
        get { return _facilityZip; }
        set { _facilityZip = value; }
    }

    public String FacilityTPhone
    {
        get { return _facilityTPhone; }
        set { _facilityTPhone = value; }
    }

    public String FacilityFax
    {
        get { return _facilityFax; }
        set { _facilityFax = value; }
    }

    public String FacilityEMail
    {
        get { return _facilityEMail; }
        set { _facilityEMail = value; }
    }

    public String FacilityTaxID
    {
        get { return _facilityTaxID; }
        set { _facilityTaxID = value; }
    }

    public String FacilitySpeciality
    {
        get { return _facilitySpeciality; }
        set { _facilitySpeciality = value; }
    }

    public String FacilityProvID
    {
        get { return _facilityProvID; }
        set { _facilityProvID = value; }
    }
    public Char IsStamps
    {
        get { return _facIsStampAddr; }
        set { _facIsStampAddr = value; }
    }
}
