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
/// Summary description for InsuranceInfo
/// </summary>
public class InsuranceInfo
{
	public InsuranceInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    private string _insName;
    private string _insNumber;
    private string _insCompany;
    private string _insAddress1;
    private string _insAddress2;
    private string _insCity;
    private string _insState;
    private string _insZip;
    private string _insPhone;
    private string _insFax;
    private int _insID;


    public Int32 InsID
    {
        get { return _insID; }
        set { _insID = value; }
    }
 

     public String InsName
    {
        get { return _insName; }
        set { _insName = value; }
    }

     public String InsNumber
     {
         get { return _insNumber; }
         set { _insNumber = value; }
     }

     public String InsCompany
     {
         get { return _insCompany; }
         set { _insCompany = value; }
     }
    
    public String InsAddress1
    {
        get { return _insAddress1; }
        set { _insAddress1 = value; }
    }

    public String InsAddress2
    {
        get { return _insAddress2; }
        set { _insAddress2 = value; }
    }

    public String InsCity
    {
        get { return _insCity; }
        set { _insCity = value; }
    }

    public String InsState
    {
        get { return _insState; }
        set { _insState = value; }
    }

    public String InsZip
    {
        get { return _insZip; }
        set { _insZip = value; }
    }

    public String InsPhone
    {
        get { return _insPhone; }
        set { _insPhone = value; }
    }

    public String InsFax
    {
        get { return _insFax; }
        set { _insFax = value; }
    }

    
}
