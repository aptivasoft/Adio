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
/// Summary description for DrugInfo
/// </summary>
public class DrugInfo
{
	public DrugInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _drugID;
    private string _drugName;
    private string _drugPrice;
    private string _effDate;
    private string _sample;
    private string _category;
    private int _drugTypeID;
    private string _dFType;
    private string _dFName;
    private string _dType;
    private string _dFDescription;
    private int _dFID;
    private string _dFForm;
    private string _dLimits;
    private string _dCostIndex;
    private string _dAuthCode;
    private Int32 _rxID;


    public Byte[] Image1;
    public Byte[] Image2;

    public Int32 Rx_ID
    {
        get { return _rxID; }
        set { _rxID = value; }
    }


    public String DrugLimits
    {
        get { return _dLimits; }
        set { _dLimits = value; }
    }

    public String DrugForm
    {
        get { return _dFForm; }
        set { _dFForm = value; }
    }

    public Int32 DrugID
    {
        get { return _drugID; }
        set { _drugID = value; }
    }

    public String DrugName
    {
        get { return _drugName; }
        set { _drugName = value; }
    }

    public String DrugPrice
    {
        get { return _drugPrice; }
        set { _drugPrice = value; }
    }

    public String EffDate
    {
        get { return _effDate; }
        set { _effDate = value; }
    }

    public String Sample
    {
        get { return _sample; }
        set { _sample = value; }
    }

    public String Category
    {
        get { return _category; }
        set { _category = value; }
    }

    public String DFormularyType
    {
        get { return _dFType; }
        set { _dFType = value; }
    }

    public String DFormularyName
    {
        get { return _dFName; }
        set { _dFName = value; }
    }
    
    public String DFormularyDescription
    {
        get { return _dFDescription; }
        set { _dFDescription = value; }
    }

    public String DrugType
    {
        get { return _dType; }
        set { _dType = value; }
    }

    public Int32 DrugTypeID
    {

        get { return _drugTypeID; }
        set { _drugTypeID = value; }
    
    }

    public Int32 DFormID
    {
        get { return _dFID; }
        set { _dFID = value; }
    }

    public String DCostIndex
    {
        get { return _dCostIndex; }
        set { _dCostIndex = value; }
    }

    public String DAuthCode
    {
        get { return _dAuthCode; }
        set { _dAuthCode = value; }
    }
}
