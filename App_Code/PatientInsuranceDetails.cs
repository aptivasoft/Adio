using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PageInfo
/// </summary>
public class PatientInsuranceDetails
{
    public PatientInsuranceDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    private string _insuranceName;
    private int _insuranceID;
    private string _insuranceCompany;
    private int _piPolicyNo;
    private string _piPolicyID;
    private string _piGroupNo;
    private string _BINNo;
    private string _insuredName;
    private string _insuredDOB;
    private string _insuredSSN;
    private string _insuredRelation;
    private int _PiID;
    private int _patID;
    private char _isActive;
    private char _isPrimaryIns;
    private string _insPhone;


    public Char IsPrimaryIns
    {
        get {return _isPrimaryIns; }
        set { _isPrimaryIns = value; }
    }
    public Int32 Pat_ID
    {
        get { return _patID; }
        set { _patID = value; }
    }

    public Int32 PI_ID
    {
        get { return _PiID; }
        set { _PiID = value; }
    }
    
    public String InsuranceName
    {
        get { return _insuranceName; }
        set { _insuranceName = value; }
    }

    public Int32 InsuranceID
    {
        get { return _insuranceID; }
        set { _insuranceID = value; }
    }

    public String InsuranceCompany
    {
        get { return _insuranceCompany; }
        set { _insuranceCompany = value; }
    }

    public int PI_PolicyNo
    {
        get { return _piPolicyNo; }
        set { _piPolicyNo = value; }
    }

    public String PI_PolicyID
    {
        get { return _piPolicyID; }
        set { _piPolicyID = value; }
    }

    public String PI_GroupNo
    {
        get { return _piGroupNo; }
        set { _piGroupNo = value; }
    }

    public String PI_BINNo
    {
        get { return _BINNo; }
        set { _BINNo = value; }
    }

    public String InsuredName
    {
        get { return _insuredName; }
        set { _insuredName = value; }
    }

    public String InsuredDOB
    {
        get { return _insuredDOB; }
        set { _insuredDOB = value; }
    }

    public String InsuredSSN
    {
        get { return _insuredSSN; }
        set { _insuredSSN = value; }
    }

    public String InsuredRelation
    {
        get { return _insuredRelation; }
        set { _insuredRelation = value; }
    }

    public Char IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }
    public String InsPhone
    {
        get { return _insPhone; }
        set { _insPhone = value; }
    }
   

}
