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
/// Summary description for PatientAllergies
/// </summary>
public class PatientAllergies
{
	public PatientAllergies()
	{
		
	}


    private int _patID;
    private string _allergicTo;
    private string _allergDescription;


    public Int32 PatID
    {
        get { return _patID; }
        set { _patID = value; }
    }

    public String AllergicTo
    {
        get { return _allergicTo; }
        set { _allergicTo = value; }
    }

    public String AllergyDescription
    {
        get { return _allergDescription; }
        set { _allergDescription = value; }
    }

    
}
