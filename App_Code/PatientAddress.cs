using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PageInfo
/// </summary>
public abstract class  PatientAddress
{
    public  PatientAddress()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     
    private string _address1;
    private string _address2;
    private string _city;
    private string _state;
    private string _zip;
 
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
}
 
