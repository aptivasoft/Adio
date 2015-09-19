function AutoCompleteSelected_Doctor(source, eventArgs) {
    var DocID = document.getElementById('ctl00_ContentPlaceHolder1_hidDocID');
    var values = eventArgs.get_value();
    DocID.value = values;
}
function addMessage(msgid, val, chkID)
{
    msg = document.getElementById(msgid);
    msgvalue =   msg.value ;
    chkvalue=document.getElementById(chkID);

    if (chkvalue.checked) {
        if (msgvalue.trim() == '')
            msg.value = msgvalue.trim()  + val;
        else
            msg.value = msgvalue.trim() + ',' + val ;
        }
        else {
            msgvalue = ',' + msgvalue.trim() + ',';
            msgvalue = msgvalue.replace(',' + val + ',', ',');

            if (msgvalue != ',')
                msg.value = msgvalue.substring(1, msgvalue.length - 1);
            else
                msg.value = '';
        
        }
}


function ClientSidePrint(URL) {
    var w = 800;
    var h = 600;
   
    var l = (window.screen.availWidth - w) / 2;
    var t = (window.screen.availHeight - h) / 2;

    var sOption = "toolbar=no,location=no,directories=no,menubar=no,top=0,left=0,width=" + w + ",height=" + h;
    
    // Open a new window
    var objWindow = window.open(URL, "Stamp");
    
    // Print the window            
    //objWindow.print();
   
}


function TestCheckBox()
{
    var TargetBaseControl = document.getElementById('ctl00_ContentPlaceHolder1_gridRx2');

    if (TargetBaseControl == null) return false;

    //get target child control.
    var TargetChildControl = "chkRxNum";

    //get all the control of the type INPUT in the base control.
    var Inputs = TargetBaseControl.getElementsByTagName("input");

    for (var n = 0; n < Inputs.length; ++n)
        if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
        return true;

    alert('Select at least one Drug!');
    return false;
}

function StampGenerateValid() {

    var shipDate = document.getElementById('ctl00_ContentPlaceHolder1_txtShipDate').value;
    var weightlbs = document.getElementById('ctl00_ContentPlaceHolder1_txtlbsWeight');
    var weightoz = document.getElementById('ctl00_ContentPlaceHolder1_txtozWeight');
    

    if (isDate(shipDate.trim())) {
        var sDate = new Date(shipDate);
        var eDate = new Date();
        with (eDate) setDate(getDate() - 1)
        if (eDate > sDate) {
            alert("Ship Date Can't be past date!");
            return false;
        }
    }

    if (weightlbs.value.trim() == "")
        weightlbs.value = "0.0";
    if (weightoz.value.trim() == "")
        weightoz.value = "0.0";

    if (weightlbs.value == 0 && weightoz.value == 0) {

        alert("Enter weight either in lbs or oz.. ");
        return false;
    }
    if (weightlbs.value > 0 && weightoz.value > 0) {

        alert("Enter weight either in lbs or oz.. ");
        return false;
    }

    return TestCheckBox();
    
    

}


function SetFocusToCheckNoCC() 
{
    setTimeout(function() { document.getElementById(GetClientId('txtChequeorCC')).focus(); }, 200);
    document.getElementById(GetClientId('txtCCAuth')).disabled = true;

}
function SetFocusToCC() {
    setTimeout(function() { document.getElementById(GetClientId('txtChequeorCC')).focus(); }, 200);
    document.getElementById(GetClientId('txtCCAuth')).disabled = false;

}
function SetFocusToPaymentAmount() 
{
    setTimeout(function(){document.getElementById(GetClientId('txtPayAmount')).focus();}, 200);
}

function AlertProcess()
 {

     if (ValidateDate() == false) 
     {
         return false;
     }
    var lblQtyinStock1 = document.getElementById(GetClientId("lblQtyinStock1"));
    if (lblQtyinStock1.innerText == "No Stock") {
        alert('No stock to process..!');
        return false;
    }
   
}
 

function OpenHelp()
{
    var formName = window.location.href;
    var words = formName.split("/")
    var fileName = words[words.length - 1];
    fileName = fileName.split(".")
    fileName = "../Help/" + fileName[0] + ".html";
    window.open(fileName, "mywindow", "toolbar = no,width =800,height=680,scrollbars=yes");
}


function frmchng(val, ID) 
{
    for (i = 0; i < document.all(ID).length; i++) 
    {
        if (document.all(ID).options[i].value == val) {
            document.all(ID).selectedIndex = i
        }
    }
}

function highlightUserON()
{
    var lblUser = document.getElementById("ctl00_adioHeader_lblUsername");
    lblUser.style.color = '#2B60DE';
    lblUser.style.fontWeight = 'bold';
    lblUser.style.cursor = 'hand';
}

function highlightUserOFF() 
{
    var lblUser = document.getElementById("ctl00_adioHeader_lblUsername");
    lblUser.style.color = '#000066';
    lblUser.style.fontWeight = 'normal';
}

function validateEvent() 
{
     var txtDate = document.getElementById(GetClientId("txtDate"));
     var txtEventDesc = document.getElementById(GetClientId("txtEventDesc"));
     var rblLocations = document.getElementById(GetClientId("rblLocation"));
     var clinic = document.getElementById(GetClientId("ddlClinicName"));
     var facility = document.getElementById(GetClientId("ddlFacilityName"));

     if (txtDate.value == "") 
     {
         alert('Enter Date for the event...');
         return false;
     }
     else if (txtEventDesc.value == "") 
     {
         alert('Enter Event Description...');
         return false;
     }
}

function validateAnnouncements() 
{
     var txtDate = document.getElementById(GetClientId("txtDate"));
     var txtMessage = document.getElementById(GetClientId("txtMessage"));
     var txtExpiryDate = document.getElementById(GetClientId("txtExpiryDate"));
     var rblLocations = document.getElementById(GetClientId("rblLocation"));
     var clinic = document.getElementById(GetClientId("ddlClinicName"));
     var facility = document.getElementById(GetClientId("ddlFacilityName"));

     if (txtDate.value == "") 
     {
         alert('Enter Date for the Announcement...');
         return false;
     }
     else if (txtMessage.value == "") 
     {
         alert('Enter Announcement Message...');
         return false;
     }
     else if (txtExpiryDate.value == "") 
     {
         alert('Enter ExpiryDate for the Announement...');
         return false;
     }  
}

function compareDates() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_';
    var sDate = new Date(document.getElementById(prefix + 'txtDate1').value);
    var eDate = new Date(document.getElementById(prefix + 'txtDate2').value);
    if (sDate > eDate) 
    {
        alert("From Date can't be greater than To Date..!");
        return false;
    }
    else if (sDate <= eDate) 
    {
        return true;
    }
}

function ValidatePatientAllergy() 
{
    var txtAlgTO = document.getElementById(GetClientId("txtAllergyTo"));
    if (txtAlgTO.value.trim() == "") 
    {
        alert("Allergy To Can't be empty..!");
        return false;
    }
}

function ValidatePatientInsurance() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Ins_'; //txt_P_Med
    var txtInsName = document.getElementById(prefix + 'txtInsName');
    var txtPID = document.getElementById(prefix + 'txtPID');
    var txtGNO = document.getElementById(prefix + 'txtGNO');
    var txtBNO = document.getElementById(prefix + 'txtBNO');
    var txtIDOB = document.getElementById(prefix + 'txtIDOB');
    //var txtISSN = document.getElementById(prefix + 'txtISSN');
    if (txtInsName.value.trim() == "") 
    {
        alert("Insurance Name Can't be empty..!");
        return false;
    }
//    if (txtPID.value.trim() == "") 
//    {
//        alert("Policy ID Can't be empty..!");
//        return false;
//    }
//    if (txtGNO.value.trim() == "") 
//    {
//        alert("Group Number Can't be empty..!");
//        return false;
//    }
//    if (txtBNO.value.trim() == "") 
//    {
//        alert("Bin Number Can't be empty..!");
//        return false;
//    }
    if (txtIDOB.value.trim() != "") 
    {
       if (isDate(txtIDOB.value.trim()) == false) 
       {
            txtIDOB.value = txtIDOB.value.trim();
            txtIDOB.focus();
            return false;
        }
    }
}

function ValidatePatientInsurance2() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabCntPatient_Patient_Ins_'; //txt_P_Med
    var txtInsName = document.getElementById(prefix + 'txtInsName');
    var txtPID = document.getElementById(prefix + 'txtPID');
    var txtGNO = document.getElementById(prefix + 'txtGNO');
    var txtBNO = document.getElementById(prefix + 'txtBNO');
    var txtIDOB = document.getElementById(prefix + 'txtIDOB');
    if (txtInsName.value.trim() == "") 
    {
        alert("Insurance Name Can't be empty..!");
        return false;
    }
//    if (txtPID.value.trim() == "") 
//    {
//        alert("Policy ID Can't be empty..!");
//        return false;
//    }
//    if (txtGNO.value.trim() == "") 
//    {
//        alert("Group Number Can't be empty..!");
//        return false;
//    }
//    if (txtBNO.value.trim() == "") 
//    {
//        alert("Bin Number Can't be empty..!");
//        return false;
//    }
    if (txtIDOB.value.trim() != "") 
    {
        if (isDate(txtIDOB.value.trim()) == false) 
        {
            txtIDOB.value = txtIDOB.value.trim();
            txtIDOB.focus()
            return false
        }
    }
}

function validateEmployee() 
{
    var txtempFName = document.getElementById(GetClientId("txtempFName"));
    if (txtempFName.value == "") 
    {
        alert('Employee "First Name" cannot be empty ...');
        return false;
    }
    var txtempLName = document.getElementById(GetClientId("txtempLName"));
    if (txtempLName.value == "") 
    {
        alert('Employee "Last Name" cannot be empty ...');
        return false;
    }
    var txtHireDate = document.getElementById(GetClientId("txtHireDate"));
    if (txtHireDate.value == "") 
    {
        alert('"Hire Date" cannot be empty ...');
        return false;
    }
    var txtHireDate = document.getElementById(GetClientId("txtLoc"));
    if (txtHireDate.value == "") 
    {
        alert('"Location" cannot be empty, Please Search/select from autofill capability...');
        return false;
    }
    var txtHireDate = document.getElementById(GetClientId("txtTitle"));
    if (txtHireDate.value == "") 
    {
        alert('"Title" cannot be empty, Please Search/select from autofill capability...');
        return false;
    }
    if (txtempFName.value == txtempLName.value) 
    {
        if (confirm('Employee FirstName and LastName are same, Do you want to continue...?')) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
function ClearFields() 
{
    var txtUserID = document.getElementById('txtUserID');
    txtUserID.value = "";
    var txtPassword = document.getElementById('txtPassword');
    txtPassword.value = "";
    
}
function validateAddUser() 
{
    var userId = document.getElementById(GetClientId("txtUserID"));
    if (userId.value == "") 
    {
        alert("UserID can't be Empty...");
        return false;
    }
    
    var pwd = document.getElementById(GetClientId("txtPassword"));
    if (pwd.value == "") 
    {
        alert("Password can't be Empty...");
        return false;
    }

    if (pwd.value.length < 7) {
        alert("Password must contain atleast 7 characters...");
        return false;
    }
    
    var checkAvailable = document.getElementById(GetClientId("lblAvailability"));
    if (checkAvailable.innerText == "UserId Unavailable") 
    {
        alert('User already exists...!');
        return false;
    }
}

function validateUser() 
{
    var txtUserID = document.getElementById('txtUserID');
    var txtPassword = document.getElementById('txtPassword');
    var lblStatus = document.getElementById('lblStatus'); 
    if (txtUserID.value == "") 
    {
        //alert("User ID can't be Empty..!");
        lblStatus.innerText = "User ID can't be Empty..!";
        return false;
    }
    if (txtPassword.value == "") 
    {
        //alert("Password can't be Empty..!");
        lblStatus.innerText = "Password can't be Empty..!";
        return false;
    }
}

function validateShipLog() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Shipping_Log_';
    var txtDrug = document.getElementById(prefix + 'txtDrug');
    var txtDate_Shipped = document.getElementById(prefix + 'txtDate_Shipped');
    var txtTracking_Number = document.getElementById(prefix + 'txtTracking_Number');
    var txtShip2Address = document.getElementById(prefix + 'txtShip2Address');
    if (txtDrug.value == "") 
    {
        alert("Drug Name Can't be empty..!");
        return false;
    }
    if (txtDate_Shipped.value == "") 
    {
        alert("Ship Date Can't be empty..!");
        return false;
    }
    if (txtTracking_Number.value == "") 
    {
        alert("Tracking Number Can't be empty..!");
        return false;
    }
    if (txtShip2Address.value == "") 
    {
        alert("Shipping Address Can't be empty..!");
        return false;
    }
}

var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;

function isInteger(s) 
{
    var i;
    for (i = 0; i < s.length; i++) 
    {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag) 
{
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) 
    {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary(year) 
{
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}

function DaysArray(n) 
{
    for (var i = 1; i <= n; i++) 
    {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}

function isDate(dtStr) 
{
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strMonth = dtStr.substring(0, pos1)
    var strDay = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) 
    {
        if (strYr.charAt(0) == "0" && strYr.length > 1) 
            strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) 
    {
        alert("The date format should be : mm/dd/yyyy")
        return false
    }
    if (strMonth.length < 1 || month < 1 || month > 12) 
    {
        alert("Please enter a valid month")
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) 
    {
        alert("Please enter a valid day")
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) 
    {
        alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) 
    {
        alert("Please enter a valid date")
        return false
    }
    return true
}

function ValidateForm() 
{
    var dt = document.frmSample.txtDate
    if (isDate(dt.value) == false) 
    {
        dt.focus()
        return false
    }
    return true
}

function IsNumeric(sText) 
{
    var ValidChars = "0123456789.";
    var IsNumber = true;
    var Char;
    for (i = 0; i < sText.length && IsNumber == true; i++) 
    {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) 
        {
            IsNumber = false;
        }
    }
    return IsNumber;
}

function copyMailAddress() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_';
    var chkMAddress = document.getElementById(prefix + 'chkboxMAddress')
    
    if (chkMAddress.checked == true) {
        
        document.getElementById(prefix + 'txtSAddr1').value = document.getElementById(prefix + 'txtAddress1');
        document.getElementById(prefix + 'txtSAddr2').value = document.getElementById(prefix + 'txtAddress2');
        document.getElementById(prefix + 'txtCity1').value = document.getElementById(prefix + 'txtCity');
        document.getElementById(prefix + 'txtSState').value = document.getElementById(prefix + 'txtState');
        document.getElementById(prefix + 'txtSZip').value = document.getElementById(prefix + 'txtZip');

    }

    return true;

}
function ValidatePatientProf() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_';
    var txtFName = document.getElementById(prefix + 'txtPatientFName');
    var txtLName = document.getElementById(prefix + 'txtPatientLName');
    var txtDocName = document.getElementById(prefix + 'txtDoctor');
    var txtFacName = document.getElementById(prefix + 'txtClinicFacility');
    var txtDOB = document.getElementById(prefix + 'txtDOB');
    var txtSSN = document.getElementById(prefix + 'txtSSN');
    var txtHippachk = document.getElementById(prefix + 'chkHippa');
    var txtHippaDate = document.getElementById(prefix + 'txtHippaDate');

    if (txtFName.value.trim() == "") 
    {
        alert("Patient First Name Can't be empty..!");
        return false;
    }
    if (txtLName.value.trim() == "") 
    {
        alert("Patient Last Name Can't be empty..!");
        return false;
    }
    if (txtDOB.value.trim() == "") 
    {
        alert("Patient Date of Birth Can't be empty..!");
        return false;
    }
    if (isDate(txtDOB.value.trim()) == false) 
    {
        txtDOB.focus()
        return false
    }
    if (txtFacName.value.trim() == "") 
    {
        alert("Patient Facility Can't be empty..!");
        return false;
    }
    if (txtDocName.value.trim() == "") 
    {
        alert("Doctor Name Can't be empty..!");
        return false;
    }
    if (txtSSN.value == "") 
    {
        alert("Please Provide Patient SSN ..!");
        return false;
    }
    if (txtSSN.value.trim() != "" && txtSSN.value.length != 9) 
    {
        alert("Invalid Patient SSN..!");
        return false;
    }
    if (txtHippachk.checked == true) 
    {
        if (txtHippaDate.value.trim() == "") 
        {
            alert("Patient HIPPA Date Can't be empty..!");
            return false;
        }
        else 
        {
            if (isDate(txtHippaDate.value.trim()) == false) 
            {
                txtHippaDate.focus()
                return false
            }
        }
    }
}

function ValidatePatientMedication()
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Med_History_'; //txt_P_Med txtPharmacy
    var txtMedName = document.getElementById(prefix + 'txtMedicationName');
    var txtQty = document.getElementById(prefix + 'txtQty');
    var txtRxDate = document.getElementById(prefix + 'txtRXDate');
    if (txtMedName.value.trim() == "") 
    {
        alert("Medication  Name Can't be empty..!");
        return false;
    }
    if (txtQty.value.trim() == "") 
    {
        txtQty.value = 0;
    }
    if (isInteger(txtQty.value.trim()) == false) 
    {
        alert("Quantity should be number..!");
        return false;
    }
    if (txtRxDate.value.trim() == "") 
    {
        alert("Rx Date  Can't be empty..!");
        return false;
    }
    if (isDate(txtRxDate.value.trim()) == false) 
    {
        txtRxDate.value = txtRxDate.value.trim();
        txtRxDate.focus()
        return false
    }
}

function ValidatePatientprescription() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Rx_'; //txt_P_Med txtPharmacy 
    var txtMedName = document.getElementById(prefix + 'txt_P_Med');
    var txtQty = document.getElementById(prefix + 'txt_P_Qty');
    var txtDocName = document.getElementById(prefix + 'txtDocName');

    if (txtDocName.value.trim() == "") 
    {
        alert("Doctor Name Can't be empty..!");
        return false;
    }
    if (txtMedName.value.trim() == "") 
    {
        alert("Medication Name Can't be empty..!");
        return false;
    }
    if (txtQty.value.trim() == "") 
    {
        txtQty.value = 0;
    }
    if (isInteger(txtQty.value.trim()) == false) 
    {
        alert("Quantity should be number..!");
        return false;
    }
    var fuData = document.getElementById(GetClientId("FileUpRxDoc"));
    var FileUploadPath = fuData.value;

    if (FileUploadPath != '') {

        var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

        if (Extension == "gif" || Extension == "png" || Extension == "jpeg" || Extension == "tif" || Extension == "jpg") {
            return true; // Valid file type
        }
        else {
            alert('Only files of format .gif/.tif/.jpeg/.jpg/.png are allowed..!');
            return false; // Not valid file type
        }
    }
}

function ValidatePatientNote() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Pat_Rx_Notes_'; //txt_P_Med
    var txtNote = document.getElementById(prefix + 'txtNoteDesc');
    if (txtNote.length > 255) 
    {
        alert("Note should not exceed 255 characters!");
        return false;
    }
}

function ValidatePatientCallLog() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Info_'; //txt_P_Med
    var txtcallReason = document.getElementById(prefix + 'txtCallReason');
    var txtCallDesc = document.getElementById(prefix + 'txtCallDesc');
    if (txtcallReason.value.trim() == "") 
    {
        alert("Call Reason Can't be empty..!");
        return false;
    }
    if (txtCallDesc.value.trim() == "") 
    {
        alert("Description Can't be empty..!");
        return false;
    }
}

function ValidatePatientPayments() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Payments_'; //txt_P_Med
    var rbtncheque = document.getElementById(prefix + 'rbtnPayCheck');
    var rbtnCC = document.getElementById(prefix + 'rbtnPayCreditCard');
    var txtChequeorCC = document.getElementById(prefix + 'txtChequeorCC');
    var txtCCAuth = document.getElementById(prefix + 'txtCCAuth');
    var txtPayAmount = document.getElementById(prefix + 'txtPayAmount');
    if (rbtncheque.checked || rbtnCC.checked) 
    {
        if (txtChequeorCC.value.trim() == "") 
        {
            alert("ChequeNo/CC# Can't be empty..!");
            return false;
        }
        if (isInteger(txtChequeorCC.value.trim()) == false) 
        {
            alert("Cheque No/CC# should be a number..!");
            return false;
        }
    }
    if (rbtnCC.checked) 
    {
        if (txtCCAuth.value.trim() == "") 
        {
            alert("Payment Auth# Can't be empty..!");
            return false;
        }
        if (isInteger(txtCCAuth.value.trim()) == false) 
        {
            alert("Payment Auth# should be a number..!");
            return false;
        }
    }
    if (txtPayAmount.value.trim() == "") 
    {
        alert("Amount Can't be empty..!");
        return false;
    }
    if (IsNumeric(txtPayAmount.value.trim()) == false) 
    {
        alert("Amount should be Numeric..!");
        return false;
    }
}

function ValidatePatientDocument() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Documents_'; //txt_P_Med
    var txtDocName = document.getElementById(prefix + 'txtDocuName');
    var txtDocDesc = document.getElementById(prefix + 'txtDocDesc');

    if (txtDocName.value.trim() == "") 
    {
        alert("Document Name Can't be empty..!");
        return false;
    }
    if (txtDocDesc.length > 255) 
    {
        alert("Note should not exceed 255 characters..!");
        return false;
    }
}

function ValidateMedRequest() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_'; //txt_P_Med
    var txtPatName = document.getElementById(prefix + 'txtPatname');
    var txtDocName = document.getElementById(prefix + 'txtDocName');
    var txtMedName = document.getElementById(prefix + 'txtMedicationName');
    var txtQty = document.getElementById(prefix + 'txtQty');
    var txtSIG = document.getElementById(prefix + 'txtSIG');
    var txtComments = document.getElementById(prefix + 'txtComments');
    if (txtPatName.value.trim() == "") 
    {
        alert("Patient Name Can't be empty..!");
        return false;
    }
    if (txtDocName.value.trim() == "") 
    {
        alert("Doctor Name Can't be empty..!");
        return false;
    }
    if (txtMedName.value.trim() == "") 
    {
        alert("Document Name Can't be empty..!");
        return false;
    }
    if (txtQty.value.trim() == "") 
    {
        alert("Quantity Can't be empty..!");
        return false;
    }
    if (txtSIG.value.trim() == "") 
    {
        alert("SIG Can't be empty..!");
        return false;
    }
    if (txtComments.length > 255) 
    {
        alert("Note should not exceed 255 characters...!");
        return false;
    }
}

function PatientDetails_Result(ObjPatientResults) 
{
    
}

function clearMedicationPanel() 
{
    var currentTime = new Date();
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Med_History_'; //txt_P_Med txtPharmacy
    document.getElementById(prefix + 'txtMedicationName').value = "";
    document.getElementById(prefix + 'txtQty').value = "";
    document.getElementById(prefix + 'txtDirection').value = "";
    document.getElementById(prefix + 'txtDoc').value = document.getElementById('ctl00_ContentPlaceHolder1_lblDoctor1').innerHTML;
    document.getElementById(prefix + 'txtRXDate').value = currentTime.format("MM/dd/yyyy");
    document.getElementById(prefix + 'ddlPopupRefills').selectedIndex = 0;
}

function clearPrescriptionPanel() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Rx_'; //txt_P_Med txtPharmacy 
    document.getElementById(prefix + 'txtDocName').value = "";
    document.getElementById(prefix + 'txtDocName').disabled = false;
    document.getElementById(prefix + 'txtPharmacy').value = "";
    document.getElementById(prefix + 'txtPharmacy').disabled = false;
    document.getElementById(prefix + 'rbtnAdioPharmacy').Checked = true;
//    document.getElementById(prefix + 'rbtnOtherPharmacy').Checked = false;
    document.getElementById(prefix + 'txt_P_Med').value = "";
    document.getElementById(prefix + 'txt_P_Qty').value = "";
    
    var date3 = new Date();
    document.getElementById(prefix + 'txtFillDate').value = date3.format("MM/dd/yyyy");
    
    document.getElementById(prefix + 'txt_P_sig').value = "";
    document.getElementById(prefix + 'hidRXID').value = "";
    document.getElementById(prefix + 'txtRxComments').value = "";
    document.getElementById(prefix + 'rbtnRegular').checked = true;
    document.getElementById(prefix + 'ddl_P_Refills').selectedIndex = 0;
    document.getElementById(prefix + 'ddl_P_Status').selectedIndex = 0;
    document.getElementById(prefix + 'lblPresStatus').innerHTML = "";
    
}



function clearAllergyPanel() 
{
    document.getElementById(GetClientId("txtAllergyTo")).value = "";
    document.getElementById(GetClientId("txtAllergyDesc")).value = "";
}

function clearInsurancePanel() 
{
    document.getElementById(GetClientId("txtInsName")).value = "";
    document.getElementById(GetClientId("txtInsPhone")).value = "";
    document.getElementById(GetClientId("txtPID")).value = "";
    document.getElementById(GetClientId("txtGNO")).value = "";
    document.getElementById(GetClientId("txtBNO")).value = "";
    document.getElementById(GetClientId("txtIName")).value = "";
    document.getElementById(GetClientId("txtIDOB")).value = "";
    document.getElementById(GetClientId("txtISSN")).value = "";
    document.getElementById(GetClientId("txtIRel")).value = "";
    document.getElementById(GetClientId("rbtnIActive")).checked = true;
}

function clearNotePanel() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Pat_Rx_Notes_'; //txt_P_Med
    document.getElementById(prefix + 'txtNoteDesc').value = "";
}

function clearCallLogPanel() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Info_'; //txt_P_Med
    document.getElementById(prefix + 'rbtnCSR').checked = true;
    document.getElementById(prefix + 'txtCallReason').value = "";
    document.getElementById(prefix + 'txtCallDesc').value = "";
}

function clearPaymentsPanel() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Patient_Payments_'; //txt_P_Med
    document.getElementById(prefix + 'rbtnPayCash').checked = true;
    document.getElementById(prefix + 'txtChequeorCC').value = "";
    document.getElementById(prefix + 'txtCCAuth').value = "";
    document.getElementById(prefix + 'txtPayAmount').value = "";
    document.getElementById(prefix + 'txtPaymentNote').value = "";
    setTimeout(function() { document.getElementById(GetClientId('txtPayAmount')).focus(); }, 200);
}

function clearDocumentPanel() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_tabContainer_Documents_'; //txt_P_Med
    document.getElementById(prefix + 'txtDocuName').value = "";
    document.getElementById(prefix + 'txtDocDesc').value = "";
}

function publishAppInfo(source, eventArgs) 
{
    var DocID = document.getElementById('ctl00_ContentPlaceHolder1_hidDocID');
    DocID.value = eventArgs.get_value();

    var prefix = 'ctl00_ContentPlaceHolder1_btnPublish';
    var btn = document.getElementById(prefix);
    btn.click();
}

function publishAppInfoByDate() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_btnPublish';
    var btn = document.getElementById(prefix);
    btn.click();
}

function publishFacInfo() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_btnSearchFac';
    var btn = document.getElementById(prefix);
    btn.click();
}

function publishDocInfo() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_btnSearchProvider';
    var btn = document.getElementById(prefix);
    btn.click();
}

function publishEmpInfo() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_btnSearch';
    var btn = document.getElementById(prefix);
    btn.click();
}

function publishInsInfo() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_btnSearchIns';
    var btn = document.getElementById(prefix);
    btn.click();
}

function publishDrugInfo() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_btnSearchDrug';
    var btn = document.getElementById(prefix);
    btn.click();
}

function validateClinic() 
{
    var clinicName = document.getElementById(GetClientId("txtClinicName"));
    if (clinicName.value == "") 
    {
        alert("Clinic Name cannot be empty ...");
        return false;
    }
}

function validateNewFacility() 
{
    var FacID = document.getElementById(GetClientId("txtfacID"));
    if (FacID.value == "") 
    {
        alert('Facility Code cannot be empty ...');
        return false;
    }
    var FacName = document.getElementById(GetClientId("txtfacName"));
    if (FacName.value == "") 
    {
        alert('Facility Name cannot be empty ...');
        return false;
    }
    var ClinicName = document.getElementById(GetClientId("txtClinic"));
    if (ClinicName.value == "") 
    {
        alert('Facility Clinic cannot be empty ...');
        return false;
    }
}

function validateNewDoctor() 
{
    var FacID = document.getElementById(GetClientId("txtFName"));
    if (FacID.value == "") 
    {
        alert('Doctor First Name cannot be empty ...');
        return false;
    }
    var FacName = document.getElementById(GetClientId("txtLName"));
    if (FacName.value == "") 
    {
        alert('Doctor Last Name cannot be empty ...');
        return false;
    }
    var ClinicName = document.getElementById(GetClientId("txtClinic"));
    if (ClinicName.value == "") 
    {
        alert('Please Provide Clinic Name...');
        return false;
    }
}

function validateAppointment() 
{
    var prefix = 'ctl00_ContentPlaceHolder1_';
    var txtFacility = document.getElementById(prefix + 'txtFacility');
    if (txtFacility.value == "") 
    {
        alert('Facility Name cannot be empty ...');
        return false;
    }
    var txtNotes = document.getElementById(prefix + 'txtNotes');
    if (txtNotes.value == "") 
    {
        alert('Notes cannot be empty ...');
        return false;
    }
    var txtDoc = document.getElementById(prefix + 'txtDoc');
    if (txtDoc.value == "") 
    {
        alert('Doctor Name cannot be empty ...');
        return false;
    }
    var txtDate = document.getElementById(prefix + 'txtDate');
    if (txtDate.value == "") 
    {
        alert('Date cannot be empty ...');
        return false;
    }
    else
    {
        var objToDat = getDate();
        var date1 = new Date(txtDate);
        var date2 = new Date(objToDate);
        var date3 = new Date();
        var date4 = date3.getMonth() + "/" + date3.getDay() + "/" + date3.getYear();
        var currentDate = new Date(date4);
        if (date1 > date2) 
        {
            alert("fromdate should be less than todate");
            return false;
        }
        else if (date1 > currentDate) 
        {
            alert("From Date should be less than current date");
            return false;
        }
        else if (date2 > currentDate) 
        {
            alert("To Date should be less than current date");
            return false;
        }
    }
}

function calidateCurDate() 
{
    var objFromDate = document.getElementById("txtRXDate").value;
    var objToDat = getDate();
    var date1 = new Date(objFromDate);
    var date2 = new Date(objToDate);
    var date3 = new Date();
    var date4 = date3.getMonth() + "/" + date3.getDay() + "/" + date3.getYear();
    var currentDate = new Date(date4);
    if (date1 > date2) 
    {
        alert("fromdate should be less than todate");
        return false;
    }
    else if (date1 > currentDate) 
    {
        alert("From Date should be less than current date");
        return false;
    }
    else if (date2 > currentDate) 
    {
        alert("To Date should be less than current date");
        return false;
    }
}

function AutoCompleteSelectedUser(source, eventArgs) 
{
    var EmpID = document.getElementById('ctl00_ContentPlaceHolder1_empId');
    EmpID.value = eventArgs.get_value();
}

function AutoCompleteSelected_PatIns(source, eventArgs) 
{
    var Pat_Ins_ID = document.getElementById('ctl00_ContentPlaceHolder1_hidPat_INSID');
    Pat_Ins_ID.value = eventArgs.get_value();
}

function ACS_Facility(source, eventArgs) 
{
    var FacID = document.getElementById('ctl00_ContentPlaceHolder1_hidFacID');
    var drugID = document.getElementById('ctl00_ContentPlaceHolder1_hidDrugID');
    var values = eventArgs.get_value();
    FacID.value = values;
    if (drugID.value != "")
        //alert('Select Drug');
    //else 
    {
        var patPhone = document.getElementById('ctl00_ContentPlaceHolder1_btngetStock');
        patPhone.click();
    }
}

function ACS_Drug(source, eventArgs) 
{
    var drugID = document.getElementById('ctl00_ContentPlaceHolder1_hidDrugID');
    var FacID = document.getElementById('ctl00_ContentPlaceHolder1_hidFacID');
    var values = eventArgs.get_value();

    drugID.value = values;

    if (FacID.value == "")
        alert('Select Facility');
    else {
        var patPhone = document.getElementById('ctl00_ContentPlaceHolder1_btngetStock');
        patPhone.click();
    }
}

function toUppercaseProv() 
{
    document.getElementById(GetClientId("txtState")).value = document.getElementById(GetClientId("txtState")).value.toUpperCase();
}

function toUppercaseIns() 
{
    document.getElementById(GetClientId("txtInsState")).value = document.getElementById(GetClientId("txtInsState")).value.toUpperCase();
}

function toUppercaseFac() 
{
    document.getElementById(GetClientId("txtfacState")).value = document.getElementById(GetClientId("txtfacState")).value.toUpperCase();
}

function validateInsuranceInfo() 
{
    var insName = document.getElementById(GetClientId("txtInsName"));
    var insNumber = document.getElementById(GetClientId("txtInsNumber"));
    var insCompany = document.getElementById(GetClientId("txtInsCompany"));
    var insAdd1 = document.getElementById(GetClientId("txtInsAddress1"));
    var insAdd2 = document.getElementById(GetClientId("txtInsAddress2"));
    var insCity = document.getElementById(GetClientId("txtInsCity"));
    var insState = document.getElementById(GetClientId("txtInsState"));
    var insZip = document.getElementById(GetClientId("txtInsZip"));
    var insPhone = document.getElementById(GetClientId("txtInsPhone"));
    var insFax = document.getElementById(GetClientId("txtInsFax"));

    if (insName.value == "") 
    {
        alert('Enter Insurance Name... ');
        return false;
    }
    else if (insNumber.value == "") 
    {
        alert('Enter Insurance Number... ');
        return false;
    }
    else if (insCompany.value == "") 
    {
        alert('Enter Insurance Company... ');
        return false;
    }
    else if (insState.value == "") 
    {
        alert('Enter State... ');
        return false;
    }
}

function validateDrugInfo() 
{
    var txtDrugName = document.getElementById(GetClientId("txtDrugName"));
    var txtDrugCostIndex = document.getElementById(GetClientId("txtDrugCostIndex"));
    if (txtDrugName.value == "") 
    {
        alert('Drug Name cannot be empty ...');
        return false;
    }
    if (txtDrugCostIndex.value == "") 
    {
        alert('Drug Cost Index cannot be empty ...');
        return false;
    }
}

function validateDrugType() 
{
    var txtDTInfo = document.getElementById(GetClientId("txtDTInfo"));
    if (txtDTInfo.value == "") 
    {
        alert('Drug Type cannot be empty ...');
        return false;
    }
}

function validateTitle() 
{
    var txtTitle = document.getElementById(GetClientId("txtTitle"));
    var txtDesc = document.getElementById(GetClientId("txtDesc"));
    if (txtTitle.value == "") 
    {
        alert('Enter Title...');
        return false;
    }
    else if (txtDesc.value == "") 
    {
        alert('Enter Description...');
        return false;
    }
}

function validateSigCodes() 
{
    var txtSIG = document.getElementById(GetClientId("txtSIGCode"));
    if (txtSIG.value == "") 
    {
        alert('Enter SIG Code...');
        return false;
    }  
}

function CallPrint(strid) 
{
    var prtContent = document.getElementById(strid);
    var WinPrint = window.print();  
}

function checkUsernameUsage(username) 
{
    var spanAvailability1 = document.getElementById(GetClientId("lblAvailability"));
    var usernameCheckerTimer;
    if (username.length == 0)
        spanAvailability1.innerText = "";
    else 
    {
        spanAvailability1.innerText = "checking...";
        usernameCheckerTimer = setTimeout("checkUsernameUsageCheck('" + username + "');", 1000);
    }
}

function checkUsernameUsageCheck(username) 
{
    PageMethods.IsUserAvailable(username, OnSucceeded);
}

// Callback function invoked on successful completion of the page method.
function OnSucceeded(result, userContext, methodName) 
{

    var spanAvailability = document.getElementById(GetClientId("lblAvailability"));
    var spanAvailability1 = document.getElementById(GetClientId("ShowProcess"));
    if (methodName == "IsUserAvailable") {
        if (result == true) {
            spanAvailability.style.visibility = "visible";
            spanAvailability.innerHTML = "<span style='color: DarkGreen;'>Available</span>";
        }
        else {
            spanAvailability.style.visibility = "visible";
            spanAvailability.innerHTML = "<span style='color: Red;'>UserId Unavailable</span>";
        }

    }
}

function AutoCompleteSelected_ClinicFaclityNames(source, eventArgs) 
{
    var txtClinicDoc = document.getElementById(GetClientId("txtDoctor"));
    txtClinicDoc.disabled = false;
    txtClinicDoc.value = "";
    $find('acCustodianEx').set_contextKey(eventArgs.get_value());
}

function ValidateChangePassword() 
{
    var userId = document.getElementById(GetClientId("txtUserID"));
    if (userId.value == "") {
        alert("UserID can't be Empty...");
        return false;
    }
    
    var oldPwd = document.getElementById(GetClientId("txtOldPwd"));
    if (oldPwd.value == "") {
        alert("Old Password can't be Empty...");
        return false;
    }
    
    var newPwd = document.getElementById(GetClientId("txtNewPassword"));
    if (newPwd.value == "") {
        alert("Password can't be Empty...");
        return false;
    }

    if (newPwd.value.length < 7) {
        alert("Password must contain atleast 7 characters...");
        return false;
    }

    var confmPwd = document.getElementById(GetClientId("txtConfirmPassword"));
    if (newPwd.value != confmPwd.value) {
        alert("New Password Does Not Match With Confirm Password...");
        return false;
    }
}

function ValidateDate() {
    var txtExpiryDate = document.getElementById(GetClientId("txtExpiryDate"));
    if (isDate(txtExpiryDate.value.trim()) == false) {
        txtExpiryDate.value = txtExpiryDate.value.trim();
        txtExpiryDate.focus();
        return false;
    }
}

function ApproveDrug() 
{
        
        var txtQuantity = document.getElementById(GetClientId("txtQuantity"));
        if (txtQuantity.value == "0" || txtQuantity.value == "") 
        {
            alert('Quantity cannot be empty or zero..!');
            return false;
        }
        else
            return true;
   
}



 

function clearAdjustBillingPanel() {
    document.getElementById(GetClientId('txtAdjBillAmt')).value = "";
    document.getElementById(GetClientId('txtAdjBillDetails')).value = "";
    document.getElementById(GetClientId('rbtnCredit')).checked = true;
    document.getElementById(GetClientId('rbtnDebit')).checked = false;
}





function ValidateSmtpSettings() {
    
    var smtpServer = document.getElementById(GetClientId("txtSmtpServer"));
    if (smtpServer.value == "") 
    {
        alert('SMTP Server Name Can Not Be Empty ...');
        return false;
    }
    var smtpPort = document.getElementById(GetClientId("txtSmtpPort"));
    if (smtpPort.value == "") 
    {
        alert('SMTP Port Number Can Not Be Empty ...');
        return false;
    }
    var smtpUserID = document.getElementById(GetClientId("txtSmtpUserID"));
    if (smtpUserID.value == "") 
    {
        alert('SMTP Mail User ID Can Not Be Empty ...');
        return false;
    }
    var smtpPassword = document.getElementById(GetClientId("txtSmtpPassword"));
    if (smtpPassword.value == "") 
    {
        alert('SMTP Mail Password Can Not Be Empty ...');
        return false;
    }
    var smtpMailFrom = document.getElementById(GetClientId("txtMailFrom"));
    if (smtpMailFrom.value == "") 
    {
        alert('From E-Mail Can Not Be Empty ...');
        return false;
    }
    var smtpMailTo = document.getElementById(GetClientId("txtMailTo"));
    if (smtpMailTo.value == "") 
    {
        alert('To E-Mail Can Not Be Empty ...');
        return false;
    }
   
}

function ClearCallLog() {
    document.getElementById(GetClientId('txtCallReason')).value = "";
    document.getElementById(GetClientId('txtCallDesc')).value = "";

    document.getElementById(GetClientId('rbtnMedicalIssueC')).checked = true;
    document.getElementById(GetClientId('rbtnMedicalIssuePA')).checked = false;
    document.getElementById(GetClientId('rbtnMedicalIssueA')).checked = false;
    document.getElementById(GetClientId('rbtnMedicalIssueD')).checked = false;
    document.getElementById(GetClientId('rbtnMedicalIssueN')).checked = false;
    document.getElementById(GetClientId('rbtnMedicalIssueP')).checked = false;
    document.getElementById(GetClientId('divCallLogFor')).style.visibility = 'hidden'; 
  
}
