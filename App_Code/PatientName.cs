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
public class PatientName
{
    public PatientName()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _lastName;
    private string _firstName;
    private string _middleName;
    private int _patID;

    public Int32 Pat_ID
    {
        get { return _patID; }
        set { _patID = value; }
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
    

}
