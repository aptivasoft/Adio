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
public class PatientPersonalDetails
{
    public PatientPersonalDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private String _patPhone;
    private String _mrn;
    private PatientName _patientName;
    private String _gender;
    private String _dob;
    private String _ssn;
    private String _pDoc;
    private String _phrmID;
    private Int32 _docID;
    private String _patientAddress1;
    private String _patientSAddress1;
    private String _patientSState;
    private String _patientSCity;
    private String _patientSZip;

    private String _patientAddress2;
    private String _patientSAddress2;
    private String _patientState;
    private String _patientCity;
    private String _patientZip;
    private String _patAutoFill;
    private Int32 _praimaryInsID;
    private Int32 _pID;
    private Int32 _facID;
    private String _cellPhone;
    private String _workPhone;
    private String _diagnosis;
    private String _pharmacyName;

    private String _doctorFullName;

    private String _patHIPPANotice;
    private String _patHIPPADate;

    private String _ecFName;
    private String _ecLName;
    private String _ecPhone;
    private String _ecRelation;

    private char _patIsActive = 'Y';

    public char PatientStatus
    {
        set { _patIsActive = value; }
        get{return _patIsActive;}
    }

    public String HIPPANotice
    {
        get { return _patHIPPANotice; }
        set { _patHIPPANotice = value; }
    }

    public String HIPPADate
    {
        get { return _patHIPPADate; }
        set { _patHIPPADate = value; }
    }

    public String DocFullName
    {
        get { return _doctorFullName; }
        set { _doctorFullName = value; }
    }

    public String PhacyName
    {
        get { return _pharmacyName; }
        set { _pharmacyName = value; }
    }

    public Int32 PrimaryInsID
    {
        get { return _praimaryInsID; }
        set { _praimaryInsID = value; }
    }
    public String Pat_AutoFill
    {
        get { return _patAutoFill; }
        set { _patAutoFill = value; }
    }

    public String Pat_Phone
    {
        get { return _patPhone; }
        set { _patPhone = value; }
    }


    public String PatientAddress1
    {
        get { return _patientAddress1; }
        set { _patientAddress1 = value; }
    }
    public String PatientAddress2
    {
        get { return _patientAddress2; }
        set { _patientAddress2 = value; }
    }
    public String Pat_ZIP
    {
        get { return _patientZip; }
        set { _patientZip = value; }
    }
    public String Pat_City
    {
        get { return _patientCity; }
        set { _patientCity = value; }
    }
    public String Pat_state
    {
        get { return _patientState; }
        set { _patientState = value; }
    }
    public String Pat_SZIP
    {
        get { return _patientSZip; }
        set { _patientSZip = value; }
    }
    public String Pat_SCity
    {
        get { return _patientSCity; }
        set { _patientSCity = value; }
    }
    public String Pat_Sstate
    {
        get { return _patientSState; }
        set { _patientSState = value; }
    }
    public String Pat_Diagnosis
    {
        get { return _diagnosis; }
        set { _diagnosis = value; }
    }
    public String Pat_WPhone
    {
        get { return _workPhone; }
        set { _workPhone = value; }
    }
    public String Pat_CellPhone
    {
        get { return _cellPhone; }
        set { _cellPhone = value; }
    }
    public Int32 FacID
    {
        get { return _facID; }
        set { _facID = value; }
    }
    public Int32 Pat_ID
    {
        get { return _pID; }
        set { _pID = value; }
    }
    public String PatientShipAddress1
    {
        get { return _patientSAddress1; }
        set { _patientSAddress1 = value; }
    }
    public String PatientShipAddress2
    {
        get { return _patientSAddress2; }
        set { _patientSAddress2 = value; }
    }

    public String Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }

    public String DOB
    {
        get { return _dob; }
        set { _dob = value; }
    }

    public String SSN
    {
        get { return _ssn; }
        set { _ssn = value; }
    }

    public String Pat_Pre_Doc
    {
        get { return _pDoc; }
        set { _pDoc = value; }
    }

    public String PhrmID
    {
        get { return _phrmID; }
        set { _phrmID = value; }
    }

    public Int32 DocID
    {
        get { return _docID; }
        set { _docID = value; }
    }

    public String MRN
    {
        get { return _mrn; }
        set { _mrn = value; }
    }

    public String eContactFName
    {
        get { return _ecFName; }
        set { _ecFName = value; }
    }

    public String eContactLName
    {
        get { return _ecLName; }
        set { _ecLName = value; }
    }
    public String eContactPhone
    {
        get { return _ecPhone; }
        set { _ecPhone = value; }
    }
    public String eContactRelation
    {
        get { return _ecRelation; }
        set { _ecRelation = value; }
    }

}
