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
/// Summary description for PharmacyInfo
/// </summary>
public class PharmacyInfo
{
	public PharmacyInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _phrmName;
    private string _phrmAddress1;
    private string _phrmAddress2;
    private string _phrmCity;
    private string _phrmState;
    private string _phrmZip;
    private string _phrmPhone;
    private string _phrmFax;
    private int _phrmID;

    public Int32 PhrmID
    {
        get { return _phrmID; }
        set { _phrmID = value; }
    }


    public String PhrmName
    {
        get { return _phrmName; }
        set { _phrmName = value; }
    }

    public String PhrmAddress1
    {
        get { return _phrmAddress1; }
        set { _phrmAddress1 = value; }
    }

    public String PhrmAddress2
    {
        get { return _phrmAddress2; }
        set { _phrmAddress2 = value; }
    }

    public String PhrmCity
    {
        get { return _phrmCity; }
        set { _phrmCity = value; }
    }

    public String PhrmState
    {
        get { return _phrmState; }
        set { _phrmState = value; }
    }

    public String PhrmZip
    {
        get { return _phrmZip; }
        set { _phrmZip = value; }
    }

    public String PhrmPhone
    {
        get { return _phrmPhone; }
        set { _phrmPhone = value; }
    }

    public String PhrmFax
    {
        get { return _phrmFax; }
        set { _phrmFax = value; }
    }
}
