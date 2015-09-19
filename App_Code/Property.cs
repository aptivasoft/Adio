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
/// Summary description for Property
/// </summary>
public class Property
{
	public Property()
	{
		
	}
    private string _userID;
    private string _passWord;
    private string _facilityID;
    private string _oldPassword;
    private string _userRole;
    private string _userState;

    private string _patID;
    private string _docID;
    private int _rxID;
    private string _rxItemID;
    private char _rxType;
    private string _rxDate;
    private string _rxDrugID;
    private string _rxExpDate;
    private string _rxPhrmacyID;
    private string _rxPhrmacyName;

    private char _rxStatus;
    private string _rxApprovedBy;
    private string _rxApprovedDate;
    private string _rxLastModifiedDate;
    private string _rxLastModifiedBy;
    private string _rxAmount;


    private string _drugName;
    private string _drugForm;
    private string _drugQty;
    private string _drugType;
    private string _drugSIG;
    private string _drugNote;
    private string _rxRefills;

    private string _eMailID;
    private string _pwdRem;
    private string _phone;

    private char _allowSub;
    private string _pIID;
    private Int32 _empid;
    private String _emplname;
    private String _empfname;

    private char _delMode;
    private string _reqType;
    private string _comments;
    private string _apptDate;
    private string _stampLoc;
    private string _phrm;

    public String Comments
    {
        get { return _comments; }
        set { _comments = value; }
    }
    public String EMPFName
    {
        get { return _empfname; }
        set { _empfname = value; }
    }

    public String EMPLName
    {
        get { return _emplname; }
        set { _emplname = value; }
    }


    private string _tokenID;

    public Int32 EMPID
    {
        get { return _empid; }
        set { _empid = value; }
    }

    public String UserID
    {
        get { return _userID; }
        set { _userID = value; }
    }

    public String Password
    {
        get { return _passWord; }
        set { _passWord = value; }
    }

    public String FacilityID
    {
        get { return _facilityID; }
        set { _facilityID = value; }
    }

    public String UserRole
    {
        get { return _userRole; }
        set { _userRole = value; }
    }

    public String UserState
    {
        get { return _userState; }
        set { _userState = value; }
    }

    public String DrugName
    {
        get { return _drugName; }
        set { _drugName = value; }
    }

    public String DrugForm
    {
        get { return _drugForm; }
        set { _drugForm = value; }
    }

    public String DrugQty
    {
        get { return _drugQty; }
        set { _drugQty = value; }
    }

    public String DrugType
    {
        get { return _drugType; }
        set { _drugType = value; }
    }

    public String DrugSIG
    {
        get { return _drugSIG; }
        set { _drugSIG = value; }
    }

    public String DrugNote
    {
        get { return _drugNote; }
        set { _drugNote = value; }
    }

    public String RxRefills
    {
        get { return _rxRefills; }
        set { _rxRefills = value; }
    }

    public String PatID
    {
        get { return _patID; }
        set { _patID = value; }
    }

    public String DocID
    {
        get { return _docID; }
        set { _docID = value; }
    }

    public Char RxType
    {
        get { return _rxType; }
        set { _rxType = value; }
    }

    public String RxDate
    {
        get { return _rxDate; }
        set { _rxDate = value; }
    }

    public String RxDrugID
    {
        get { return _rxDrugID; }
        set { _rxDrugID = value; }
    }

    public String RxExpDate
    {
        get { return _rxExpDate; }
        set { _rxExpDate = value; }
    }

    public String RxPharmacyID
    {
        get { return _rxPhrmacyID; }
        set { _rxPhrmacyID = value; }
    }

    public Char RxStatus
    {
        get { return _rxStatus; }
        set { _rxStatus = value; }
    }

    public String RxApprovedBy
    {
        get { return _rxApprovedBy; }
        set { _rxApprovedBy = value; }
    }

    public String RxApprovedDate
    {
        get { return _rxApprovedDate; }
        set { _rxApprovedDate = value; }
    }

    public String RxModifiedDate
    {
        get { return _rxLastModifiedDate; }
        set { _rxLastModifiedDate = value; }
    }

    public String RxModifiedBy
    {
        get { return _rxLastModifiedBy; }
        set { _rxLastModifiedBy = value; }
    }

    public String RxAmount
    {
        get { return _rxAmount; }
        set { _rxAmount = value; }
    }

    public String RxPharmacyName
    {
        get { return _rxPhrmacyName; }
        set { _rxPhrmacyName = value; }
    }
    public String RxItemID
    {
        get { return _rxItemID; }
        set { _rxItemID = value; }
    }
    public String EmailID
    {
        get { return _eMailID; }
        set { _eMailID = value; }
    }
    public String PwdRem
    {
        get { return _pwdRem; }
        set { _pwdRem = value; }
    }
    public String Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }
    public Char AllowSub
    {
        get { return _allowSub; }
        set { _allowSub = value; }
    }
    public String  PIID
    {
        get { return _pIID; }
        set { _pIID = value; }
    }
    public int RxID
    {
        get { return _rxID; }
        set { _rxID = value; }
    }

    public String TokenID
    {
        get { return _tokenID; }
        set { _tokenID = value; }
    }

    public Char DeliveryMode
    {
        get { return _delMode ; }
        set { _delMode  = value; }
    }

    public String RequestType
    {
        get { return _reqType; }
        set { _reqType = value; }
    }

    public String ApptDate
    {
        get { return _apptDate; }
        set { _apptDate = value; }
    }

    public String OldPassword
    {
        get { return _oldPassword; }
        set { _oldPassword = value; }
    }
    
    public String StampLoc
    {
        get { return _stampLoc;  }
        set {  _stampLoc = value; }
    }

    public String Pharmacy
    {
        get { return _phrm; }
        set { _phrm = value; }
    }
   
}
