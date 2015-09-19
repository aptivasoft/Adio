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
public class PatientBilling
{
    public PatientBilling()
	{
		
	}


    private int _patID;
    private string _transDate;
    private char _transType;
    private char _transFirst;
    private string _transAmt;
    private string _transDetails;
    private string _lastModifiedBy;
    private char _transFlag;
    private string _transSysFlag;

    public Int32 PatID
    {
        get { return _patID; }
        set { _patID = value; }
    }

    public string TransactionDate
    {
        get { return _transDate; }
        set { _transDate = value; }
    }
    public char TransactionType
    {
        get { return _transType; }
        set { _transType = value; }
    }
    public char TransactionMode
    {
        get { return _transFirst; }
        set { _transFirst = value; }
    }
    public char TransactionFlag
    {
        get { return _transFlag; }
        set { _transFlag = value; }
    }
    public string TransactionAmount
    {
        get { return _transAmt; }
        set { _transAmt = value; }
    }
    public string TransactionDetails
    {
        get { return _transDetails; }
        set { _transDetails = value; }
    }
    public string User
    {
        get { return _lastModifiedBy; }
        set { _lastModifiedBy = value; }
    }

    public string TransSysFlag
    {
        get { return _transSysFlag; }
        set { _transSysFlag = value; }
    }
     

    
}
