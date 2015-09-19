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
/// Summary description for ProviderInfo
/// </summary>
public class ProviderInfo
{
	public ProviderInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    private int _ProvID;
    private string _lastName;
    private string _firstName;
    private string _middleName;
    private string _location;
    private string _address1;
    private string _address2;
    private string _city;
    private string _state;
    private string _zip;
    private string _hPhone;
    private string _wPhone;
    private string _cPhone;
    private string _eMail;
    private string _fax;
    private string _licNo;
    private int _docType;
    private string _status;
    private string _degree;
    private string _deaNo;
    private string _fullName;
    private string _npi;
    private byte[] _signature;
    private string _speciality;
    private string _userID;
    private int _DocId;
    private int _FacId;
    private char _userType;
      
    public Int32 Doc_Id
    {
        get { return _DocId; }
        set{_DocId = value;}
    }

    public Int32 Fac_Id
    {
        get { return _FacId; }
        set { _FacId = value; }
    }

    /*************************************************/
    public Int32 Prov_ID
    {
        get { return _ProvID; }
        set { _ProvID = value; }
    }

    public String LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public String FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public String MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; }
    }

    public String Location
    {
        get { return _location; }
        set { _location = value; }
    }



    public String Address1
    {
        get { return _address1; }
        set { _address1 = value; }
    }

    public String Address2
    {
        get { return _address2; }
        set { _address2 = value; }
    }

    public String City
    {
        get { return _city; }
        set { _city = value; }
    }

    public String State
    {
        get { return _state; }
        set { _state = value; }
    }

    public String Zip
    {
        get { return _zip; }
        set { _zip = value; }
    }

    public String HPhone
    {
        get { return _hPhone; }
        set { _hPhone = value; }
    }

    public String WPhone
    {
        get { return _wPhone; }
        set { _wPhone = value; }
    }

    public String CPhone
    {
        get { return _cPhone; }
        set { _cPhone = value; }
    }

    public String EMail
    {
        get { return _eMail; }
        set { _eMail = value; }
    }

    public String Fax
    {
        get { return _fax; }
        set { _fax = value; }
    }

    public String LicNo
    {
        get { return _licNo; }
        set { _licNo = value; }
    }

    public Int32 DocType
    {
        get { return _docType; }
        set { _docType = value; }
    }

    public String Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public String Degree
    {
        get { return _degree; }
        set { _degree = value; }
    }
    public String DeaNumber
    {
        get { return _deaNo; }
        set { _deaNo = value; }
    }

    public String FullName
    {
        get { return _fullName; }
        set { _fullName = value; }
    }
    public String NPI
    {
        get { return _npi; }
        set { _npi = value; }
    }
    public String Speciality
    {
        get { return _speciality; }
        set { _speciality = value; }
    }
    public String UserID
    {
        get { return _userID; }
        set { _userID = value; }
    }
    public byte[] Signature
    {
        get { return _signature; }
        set { _signature = value; }
    }
    public Char UserType
    {
        get { return _userType; }
        set { _userType = value; }
    }
}
