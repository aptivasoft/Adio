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
/// Summary description for Clinic
/// </summary>
public class Clinic
{
	public Clinic()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _clinic;
    private int _clinicID;

    public String ClinicName
    {
        get { return _clinic; }
        set { _clinic = value; }
    }

    public Int32 ClinicID
    {
        get { return _clinicID; }
        set {_clinicID = value;}
    }

}
