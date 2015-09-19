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
/// Summary description for PatientCurrentMedication
/// </summary>
public class MedicationResponse
{
    public MedicationResponse()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// N for Medication Fill,C for Change Medication
    /// </summary>
    public string medicationRequestType;
    public  string  patientFullName;
    public string providerFullName;
    public Medication  medicationreq;
    public string medicationStaus;
    
    

    
    
    

}
