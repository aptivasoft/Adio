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
/// Summary description for SIGCode
/// </summary>
public class SIGCodes
{
	public SIGCodes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _sigCode;
    private string _sigName;
    private int _sigID;
    private string _sigFactor;

    public Int32 SIG_ID
    {
        get { return _sigID; }
        set { _sigID = value; }
    }

    public String SIGCode
    {
        get { return _sigCode; }
        set { _sigCode = value; }
    }

    public String SIGName
    {
        get { return _sigName; }
        set { _sigName = value; }
    }

    public String SIGFactor
    {
        get { return _sigFactor; }
        set { _sigFactor = value; }
    }



}
