using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing;
using NLog;
using Adio.UALog;
using System.IO;

public partial class Patient_PatientProfile : System.Web.UI.Page
{
    int patID;
    int patFacID;

    string conStr = ConfigurationManager.AppSettings["conStr"];
    
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();

    DataTable dtPatientDetails = new DataTable();
    PatientInfoDAL objPat_Info = new PatientInfoDAL();
    PatientPersonalDetails objPat_Pat_Det = new PatientPersonalDetails();
    PatientInsuranceDetails objPat_Ins_Det = new PatientInsuranceDetails();
    PatinetMedHistoryInfo objPat_Med_His_Info = new PatinetMedHistoryInfo();

    private void ssss()
    {
        //Access the DB
        //convert the DT to JSON object
        //write the JSOn on Respons sterm.
        //End the response.
    }

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        //Check url if you have getquestion as request parameter
        //if Call is for get question


        try
        {
            rbtnAdioPharmacy.Attributes.Add("onclick", "document.getElementById('" + txtPharmacy.ClientID + "').style.visibility='hidden'; document.getElementById('" + txtPharmacy.ClientID + "').value='';document.getElementById('" + rbtnPAP.ClientID + "').disabled=false;document.getElementById('" + rbtnSample.ClientID + "').disabled=false;document.getElementById('" + ddl_P_Status.ClientID + "').selectedIndex = 0;");
            rbtnOtherPharmacy.Attributes.Add("onclick", "document.getElementById('" + txtPharmacy.ClientID + "').style.visibility='visible';document.getElementById('" + txtPharmacy.ClientID + "').value='Other Pharmacy';document.getElementById('" + rbtnRegular.ClientID + "').checked=true;document.getElementById('" + rbtnPAP.ClientID + "').disabled=false;document.getElementById('" + rbtnSample.ClientID + "').disabled=false;frmchng('O','" + ddl_P_Status.ClientID + "'); ");
            //chkRx30Patient.Attributes.Add("onclick", "if(document.getElementById('" + chkRx30Patient.ClientID + "').checked){document.getElementById('" + chkRx30Patient.ClientID + "').nextSibling.innerHTML ='Prodigy';}else {document.getElementById('" + chkRx30Patient.ClientID + "').nextSibling.innerHTML='Rx30';}");
            rbtnPayCash.Attributes.Add("onmousedown", "SetFocusToPaymentAmount();");
            rbtnPayCheck.Attributes.Add("onmousedown", "SetFocusToCheckNoCC();");
            rbtnPayCreditCard.Attributes.Add("onmousedown", "SetFocusToCC();");

            lblTransactionDate.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            lblTransAdjDate.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            AutocompletePatients.ContextKey = (string)Session["User"];

            if (Session["Role"].ToString() == "A" || Session["Role"].ToString() == "M")
            {
                gridPatAllergies.Columns[0].Visible = true;
                gridPayments.Columns[0].Visible = true;
                gridPrisInfo.Columns[0].Visible = true;
                gridCallog.Columns[0].Visible = true;
                gridPatInsurance.Columns[0].Visible = true;
                gridNotes.Columns[0].Visible = true;
                
                gridPatAllergies.Columns[1].Visible = true;
                gridPayments.Columns[1].Visible = true;
                gridPrisInfo.Columns[1].Visible = true;
                gridCallog.Columns[1].Visible = true;
                gridPatInsurance.Columns[1].Visible = true;
                gridNotes.Columns[1].Visible = true;
                
                lnkAdjustBilling.Enabled = true;

                
            }
            else
            {
                gridPatAllergies.Columns[1].Visible = false;
                gridPayments.Columns[1].Visible = false;
                gridPrisInfo.Columns[1].Visible = false;
                gridCallog.Columns[1].Visible = false;
                gridPatInsurance.Columns[1].Visible = true;
                gridNotes.Columns[1].Visible = false;

                if (Session["Role"].ToString() == "D" || Session["Role"].ToString() == "N")
                {
                    btnEdit.Visible = false;
                    lnkAdjustBilling.Enabled = false;
                    lnkAddBilling.Enabled = false;

                    Patient_Payments.Visible= false;
                    Billing.Visible = false;
                }

                if ((string)Session["Role"] != "C")
                {
                    gridPatAllergies.Columns[0].Visible = false;
                    gridPayments.Columns[0].Visible = false;
                    grdBilling.Columns[0].Visible = false;
                    gridPrisInfo.Columns[0].Visible = true;
                    gridCallog.Columns[0].Visible = false;
                    gridPatInsurance.Columns[0].Visible = true;
                    gridNotes.Columns[0].Visible = false;
                    gridPrisInfo.Columns[1].Visible = true;
                }
                if ((string)Session["Role"] == "A" || (string)Session["Role"] == "T")
                {
                   
                    chkRx30Patient.Enabled = true;
                }
                else
                {
                   
                    chkRx30Patient.Enabled = false;
                }
                lnkAdjustBilling.Enabled = false;
 
            }

            RenderJSArrayWithCliendIds(txtPatientName1, txtAllergyTo, txtAllergyDesc, txtInsName, txtInsPhone, txtPID, txtGNO, txtBNO, txtIName, txtIDOB, txtISSN, txtIRel, rbtnIActive, FileUpRxDoc, lblQtyinStock1, lblChequeorCC, lblCCAuth, lblCCAuth1, txtCCAuth, txtChequeorCC, txtPayAmount, lblDOB1, txtExpiryDate, txtTransAmt, txtTransDetails, rbtnPAPFilling, rbtnShippingCharges, chkTimeBilling, txtAdjBillAmt, txtAdjBillDetails, rbtnCredit, rbtnDebit, txtCallReason, txtCallLogDoctor, txtCallLogPharmacist, txtCallLogOther, txtCallDesc, rbtnMedicalIssueC, rbtnMedicalIssuePA, rbtnMedicalIssueA, rbtnMedicalIssueD, rbtnMedicalIssueN, rbtnMedicalIssueP, divCallLogFor);
            if (!Page.IsPostBack)
            {

                if (Session["Prodigy"] != null)
                {
                    if (Session["Prodigy"].ToString() == "P")
                    {
                        chkRx30Patient.Checked = true;
                    }
                    else
                    {
                        chkRx30Patient.Checked = false;
                    }
                }
                tabContainer.Enabled = false;
               
                if (Request.QueryString["patID"] != null)
                {
                    btnMedLogReport.Visible = true;
                    Filldata(int.Parse(Request.QueryString["patID"].ToString()));
                }
                ddl_P_Status.DataBind();
                txtPayAmount.Focus();
                ddlEditRxStatus.DataBind();
                int refCount = int.Parse((string)ConfigurationManager.AppSettings["RefillCount"]);
                for(int i=0;i<=refCount;i++)
                {
                    ListItem item = new ListItem();
                    item.Text = i.ToString();
                    item.Value = i.ToString();
                    ddlEditRxRefills.Items.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Patient Search Click Event Started...");
        try
        {
            
            if (txtPatientName1.Text == string.Empty)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "<br>" + "Patient Name Can't Be Empty ..!";
            }
            else
            {
                lblMsg.Visible = false;

                tabContainer.Visible = true;


                if (Session["Role"].ToString() == "D" || Session["Role"].ToString() == "N")
                    btnEdit.Visible = false;
                else
                    btnEdit.Visible = true;


                SetlblVisibility(1);

                lblSSN.Visible = false;


                lblGender.Visible = true;
                lblClinicFacility.Visible = true;
                lblPrimInsurance.Visible = true;
                lblAddress.Visible = true;
                lblPrimInsurance.Visible = true;
                lblPhone.Visible = true;
                lblDOB.Visible = true;
                lblDoctor.Visible = true;
                lblBalance.Visible = true;
                lblSSN.Visible = true;
                divpatList.Visible = false;
                btnAppointments.Visible = true;
                btnMedLogReport.Visible = true;

                ImageButton ib = (ImageButton)sender;
                if (ib.ID == "btnSearch1")
                {

                    Filldata(int.Parse(hf_PatID.Value));
                }
                else
                {
                    if (checkPatientData(txtPatientName1.Text))
                    {
                        Filldata(0);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Patient Search Click Event Completed...");
    }
    
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        

        try
        {
           // FillgridPatMedInfo();
            if (tabContainer.ActiveTabIndex == 0)
            {
                FillgridPriscrition();
                //FillgridPatMedInfo();
            }
            else if (tabContainer.ActiveTabIndex == 1)
            {
                //FillgridPriscrition();
                FillgridPatMedInfo();
            }
            else if (tabContainer.ActiveTabIndex == 2)
            {
                FillgridPatAllergies();
            }
            else if (tabContainer.ActiveTabIndex == 3)
            {
                FillgridPatInsurance();
            }
            else if (tabContainer.ActiveTabIndex == 4)
            {
                FillgridNotes();
            }
            else if (tabContainer.ActiveTabIndex == 5)
            {
                FillgridPatCallLog();
            }
            else if (tabContainer.ActiveTabIndex == 6)
            {
                if (Session["Role"].ToString() == "D" || Session["Role"].ToString() == "N")
                    tabContainer.ActiveTabIndex = 0;
                else
                    FillgridPatPayments();
            }
            else if (tabContainer.ActiveTabIndex == 7)
            {
                FillShipLog();
            }
            else if (tabContainer.ActiveTabIndex == 8)
            {
                FillgridDocument();
            }
            else if (tabContainer.ActiveTabIndex == 9)
            {
                if (Session["Role"].ToString() == "D" || Session["Role"].ToString() == "N")
                    tabContainer.ActiveTabIndex = 0;
                else
                    FillPatientBillingGrid();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    public void RenderJSArrayWithCliendIds(params Control[] wc)
    {
        try
        {
            if (wc.Length > 0)
            {
                StringBuilder arrClientIDValue = new StringBuilder();
                StringBuilder arrServerIDValue = new StringBuilder();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Now loop through the controls and build the client and server id's
                for (int i = 0; i < wc.Length; i++)
                {
                    arrClientIDValue.Append("\"" + wc[i].ClientID + "\",");
                    arrServerIDValue.Append("\"" + wc[i].ID + "\",");
                }
                // Register the array of client and server id to the client
                cs.RegisterArrayDeclaration("MyClientID", arrClientIDValue.ToString().Remove(arrClientIDValue.ToString().Length - 1, 1));
                cs.RegisterArrayDeclaration("MyServerID", arrServerIDValue.ToString().Remove(arrServerIDValue.ToString().Length - 1, 1));
                // Now register the method GetClientId, used to get the client id of the control
                cs.RegisterStartupScript(this.Page.GetType(), "key", "\nfunction GetClientId(serverId)\n{\nfor(i=0; i<MyServerID.length; i++)\n{\nif ( MyServerID[i] == serverId )\n{\nreturn MyClientID[i];\nbreak;\n}\n}\n}", true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected bool checkPatientData(string patName)
    {
        bool flag = false;
        try
        {
            PatientInfoDAL objPat_Info = new PatientInfoDAL();

            DataTable Pat_Names = objPat_Info.get_PatientNames(patName, (string)HttpContext.Current.Session["Role"], (string)HttpContext.Current.Session["User"]);
            DataRow[] foundRows;
            foundRows = Pat_Names.Select("Pat_LName + ',' + Pat_FName ='" + patName + "'");
            if (foundRows.Length == 0)
            {
                flag= true;
            }
            else
            {
                if (foundRows.Length == 1)
                {
                    flag= true;
                }
                else
                {
                    listPatient(foundRows);
                    flag= false;
                }

            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

        return flag;
    }

    protected void listPatient(DataRow[] Pat_Names)
    {
       
        try
        {
            string hlist = "";

            for (int i = 0; i <= Pat_Names.GetUpperBound(0); i++)
            {

                hlist = hlist + @"<a href='javascript:void(0);' onclick=""PatSearch('" + Pat_Names[i][2].ToString() + @"');""> " + Pat_Names[i][0].ToString() + "," + Pat_Names[i][0].ToString() + " - " + Pat_Names[i][5].ToString() + "</a><br>";
            }
           


            divpatList.Visible = true;
            divpatList.InnerHtml = hlist;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnSaveMed_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Medication Click Event Started...");
        string userID = (string)Session["User"];
        PatinetMedHistoryInfo objPat_Med_His_Info = new PatinetMedHistoryInfo();
        try
        {
            if (DateTime.Parse(txtRXDate.Text) > DateTime.Parse(DateTime.Now.ToShortDateString()))
            {
                string str = "alert('RxDate Can't Exceed Current Date ... !');";
                ScriptManager.RegisterStartupScript(btnSave, typeof(Page), "alert", str, true);
                AddMedcation.Show();
            }
            else
            {
                if (objPat_Info.get_MedicationNames(txtMedicationName.Text).Rows.Count <= 0)
                {
                    objPat_Info.set_newMedication(userID, txtMedicationName.Text);
                    objPat_Med_His_Info.Madicine = txtMedicationName.Text;
                }
                else
                {
                    objPat_Med_His_Info.Madicine = txtMedicationName.Text;
                }
                TextBox popuptxtQty = ((TextBox)pnlAddMedication.FindControl("txtQty"));
                objPat_Med_His_Info.Quantity = int.Parse(popuptxtQty.Text);
                TextBox popuptxtMedName = ((TextBox)pnlAddMedication.FindControl("txtMedicationName"));
                objPat_Med_His_Info.Madicine = popuptxtMedName.Text;
                //Added for SIG Codes Description - START.
                string[] sig;
                if (txtDirection.Text != "" && txtDirection.Text.Contains("||"))
                {
                    sig = txtDirection.Text.Split(new string[] { "||" }, StringSplitOptions.None);
                    objPat_Med_His_Info.SIG = sig[1].TrimStart().ToString();
                }
                else
                    objPat_Med_His_Info.SIG = txtDirection.Text;
                //Added for SIG Codes Description - END.
                objPat_Med_His_Info.Rx_Date = DateTime.Parse(txtRXDate.Text);
                string DocLName;
                SqlConnection sqlCon = new SqlConnection(conStr);
                if ((txtDoc.Text).IndexOf(",") > 0)
                {
                    objPat_Med_His_Info.Doc_FullName = ((TextBox)pnlAddMedication.FindControl("txtDoc")).Text;
                    string[] arInfo;
                    char[] splitter = { ',' };
                    arInfo = (txtDoc.Text).Split(splitter);
                    DocLName = arInfo[0];
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.CommandText = "select Doc_ID from Doctor_Info where Status<>'N' and Doc_LName='" + DocLName + "'";
                    sqlCmd.Connection = sqlCon;
                    sqlCon.Open();
                    SqlDataReader dr = sqlCmd.ExecuteReader();
                    if (dr.Read())
                    {
                        objPat_Med_His_Info.Doc_ID = int.Parse(dr[0].ToString());
                    }
                    else
                    {
                        objPat_Med_His_Info.Doc_ID = 0;
                    }
                    sqlCon.Close();
                }
                else
                {
                    objPat_Med_His_Info.Doc_ID = 0;
                }

                objPat_Med_His_Info.Doc_FullName = txtDoc.Text;
                objPat_Med_His_Info.Pat_ID = int.Parse(hidPatID.Value);
                objPat_Med_His_Info.Refills = int.Parse(ddlPopupRefills.SelectedValue.ToString());
                objPat_Info.set_newPatMedication(objPat_Med_His_Info);
                FillgridPatMedInfo();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Save Medication Click Event Completed...");
    }

    #endregion
    

    #region WebMethods

    //GetInsuranceNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDrugNames(string prefixText, int count)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names = objPat_Info.get_DrugName(prefixText);
        foreach (DataRow dr in Pat_Names.Rows)
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
        }
        return Pat_List.ToArray();
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Doc_Names = objPat_Info.get_PatDoctorNames(prefixText, contextKey);
        if (Doc_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Doc_Names.Rows)
            {
                Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString().Trim() + "," + dr[0].ToString().Trim(), dr[2].ToString()));
            }
        }
        else
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Doctor Found...", "0"));
        }
        return Pat_List.ToArray();
    }


    //GetMedicationNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetMedicationNames(string prefixText, int count)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Med_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Med_List.Clear();
        DataTable Med_Names = objPat_Info.get_MedicationNames(prefixText.Trim());
        if (Med_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Med_Names.Rows)
            {
                Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
            }
        }
        else
        {
                Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Medications Found!","0"));
        }
        return Med_List.ToArray();
    }


    //GetInsuranceNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetInsuranceNames(string prefixText, int count)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names = objPat_Info.get_InsNames(prefixText);
        if (Pat_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Pat_Names.Rows)
            {
                Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
            }
        }
        else
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Insurance Found...", "0"));
        }
        return Pat_List.ToArray();
    }

    //Added SIG Code Web Method - START
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSIGNames(string prefixText, int count, string contextKey)
    {
        List<string> sig_List = new List<string>();
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        if (contextKey == "0")
        {
            sig_List.Clear();
            DataTable dtsig = objPat_Info.get_SIGCodes(prefixText, count, contextKey);
            foreach (DataRow dr in dtsig.Rows)
            {
                sig_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["SIG_Code"].ToString() + "||" + dr["SIG_Name"].ToString(), dr[0].ToString()));
            }
        }

        return sig_List.ToArray();
    }

    //Added SIG Code Web Method - END


    //GetPatientNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetPatientNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names = objPat_Info.get_PatientNames(prefixText, (string)HttpContext.Current.Session["Role"], contextKey);
        foreach (DataRow dr in Pat_Names.Rows)
        {
            String mname = "";
            if (dr[6].ToString().Trim() != mname)
                mname = " "+ dr[6].ToString().Trim()+".";
            if ((string)HttpContext.Current.Session["Role"] == "A" || (string)HttpContext.Current.Session["Role"] == "T")
                Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString() + mname + " -" + dr[5].ToString() + " - [ " + dr[7].ToString() + " ]", dr[2].ToString()));
            else
                Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString() + mname + " -" + dr[5].ToString(), dr[2].ToString()));
        }
        return Pat_List.ToArray();
    }

    #endregion


    #region Patient Profile

    protected void SetlblVisibility(int Flag)
    {
        objNLog.Info("Function started with Flag as argument...");
        try
        {
            if (Flag == 0)
            {
                lblCliniFacility1.Visible = false;
                lblDoctor1.Visible = false;
                lblGender1.Visible = false;
                lblPrimIns1.Visible = false;
                lblPhone1.Visible = false;
                lblSSN1.Visible = false;
                lblPrimIns1.Visible = false;
                lblDOB1.Visible = false;
                lblAddress12.Visible = false;
                lblAutofill1.Visible = false;
                lblBalance1.Visible = false;
                lblCell1.Visible = false;
                lblSAddress12.Visible = false;
                lblWPhone1.Visible = false;
                lbldcode1.Visible = false;
                gridPatMedicalInfo.DataSource = null;
                gridPatMedicalInfo.DataBind();
                btnEdit.Visible = false;
                btnAppointments.Visible = false;
                btnMedLogReport.Visible = false;
                tabContainer.ActiveTabIndex = 0;

            }
            else if (Flag == 1)
            {
                lblCliniFacility1.Visible = true;
                lblDoctor1.Visible = true;
                lblGender1.Visible = true;
                lblPrimIns1.Visible = true;
                lblPhone1.Visible = true;
                lblSSN1.Visible = true;
                lblPrimIns1.Visible = true;
                lblDOB1.Visible = true;
                lblAddress12.Visible = true;
                lblAutofill1.Visible = true;
                lblBalance1.Visible = true;
                lblCell1.Visible = true;
                lblSAddress12.Visible = true;
                lblWPhone1.Visible = true;
                lbldcode1.Visible = true;

            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed...");
    }

    public PatientPersonalDetails GetPatientId(string Pat_FullName)
    {
        objNLog.Info("Function started with Pat_FullName as argument...");
        try
        {
            DataTable dtPatientDetails = new DataTable();
            string[] arInfo;

            char[] splitter = { ',' };
            arInfo = Pat_FullName.Split(splitter);
            if (arInfo.Length == 2)
            {
                dtPatientDetails = objPat_Info.get_Patient_Details(arInfo[1], arInfo[0]);
                objPat_Pat_Det.Pat_ID = (int)dtPatientDetails.Rows[0][12];
            }
            else
            {
                objPat_Pat_Det.Pat_ID = 0;
            }
        }
        catch(Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed...");
        return objPat_Pat_Det;
    }

    public static string FormatUSPhone(string num)
    {
        string results = string.Empty;
        try
        {
            num = num.Replace("(", "").Replace(")", "").Replace("-", "");

            string formatPattern = @"(\d{3})(\d{3})(\d{4})";
            results = Regex.Replace(num, formatPattern, "($1) $2-$3");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);  
        } 
        return results;
    }

    public static string FormatUSSSN(string num)
    {

        //first we must remove all non numeric characters

        num = num.Replace("(", "").Replace(")", "").Replace("-", "");

        string results = string.Empty;

        string formatPattern = @"(\d{3})(\d{2})(\d{4})";

        results = Regex.Replace(num, formatPattern, "$1-$2-$3");

        //now return the formatted phone number
        return results;

    }

    protected void btnAppointments_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatAppointments.aspx?patID=" + hidPatID.Value);
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("PatientProfile.aspx?patID=" + hidPatID.Value);
    }

    protected void btnMedLogReport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ReportMedLog.aspx?patID=" + hidPatID.Value);
    }

    private void PatientLastprescription(string pid)
    {
        objNLog.Info("Function started with pid as argument...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_Pat_LastPrescription";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;

            SqlParameter pPatID = sqlCmd.Parameters.Add("@PatID", SqlDbType.Int);
            if (pid != "")
                pPatID.Value = pid;
            else
                pPatID.Value = Convert.DBNull;

            da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() != "")
                {
                    DateTime dt = (DateTime)ds.Tables[0].Rows[0][0];
                    lblLastP_R1.Text = dt.ToString("MM/dd/yyyy");
                }
                else
                    lblLastP_R1.Text = "No Prescription/Refill";
            }
            else
                lblLastP_R1.Text = "No Prescription/Refill";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed...");
    }

    private void PatientLastVisit(string patID)
    {
        objNLog.Info("Function started with patID as argument...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getPatientLastVisit", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_patID = sqlCmd.Parameters.Add("@pat_ID", SqlDbType.Int);
            sp_patID.Value = Int32.Parse(patID);

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();


            sqlDa.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() != "")
                {
                    lblLastV_C1.Text = ds.Tables[0].Rows[0][0].ToString();

                }
                else

                    lblLastV_C1.Text = "No Visits";
            }
            else

                lblLastV_C1.Text = "No Visits";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed...");
    }

    protected void Filldata(int pat_ID)
    {
        objNLog.Info("Function started with patID as argument...");
        try
        {
            tabContainer.ActiveTabIndex = 0;
            PatientInfoDAL objPat_Info = new PatientInfoDAL();
            DataTable dtPatientDetails = new DataTable();
            string patFulName = txtPatientName1.Text;
            if (patFulName.IndexOf(",") < 0 && pat_ID == 0)
            {
                SetlblVisibility(0);

                string script = @"if(confirm('Patient does not exist. Do you want to add a new Patient?')) { window.location.href='PatientProfile.aspx?patname=" + txtPatientName1.Text + "';}";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
            }
            else
            {
                tabContainer.Enabled = true;
                if (pat_ID == 0)
                {
                    lblMsg.Text = string.Empty;
                    string[] arInfo = new string[2];
                    char[] splitter = { ',' };
                    arInfo = patFulName.Split(splitter);
                    dtPatientDetails = objPat_Info.get_Patient_Details(arInfo[1], arInfo[0]);
                }
                else
                    dtPatientDetails = objPat_Info.get_Patient_Details(pat_ID.ToString());

                if (dtPatientDetails.Rows.Count > 0)
                {

                    if (dtPatientDetails.Rows[0][1].ToString() != null)
                    {

                        lblPhone1.Text = FormatUSPhone(dtPatientDetails.Rows[0]["Pat_Phone"].ToString());
                    }
                    else
                    {

                    }
                    if (dtPatientDetails.Rows[0]["Pat_Gender"].ToString() == "M")
                    {

                        lblGender1.Text = "Male";
                    }
                    else
                    {
                        lblGender1.Text = "Female";
                    }

                    lblWPhone1.Text = FormatUSPhone(dtPatientDetails.Rows[0]["Pat_WorkPhone"].ToString());
                    lblCell1.Text = FormatUSPhone(dtPatientDetails.Rows[0]["Pat_CellPhone"].ToString());
                    lblDOB1.Text = ((DateTime)dtPatientDetails.Rows[0]["Pat_DOB"]).ToShortDateString();

                    String contRel = "";
                    switch (dtPatientDetails.Rows[0]["econtact_relation"].ToString().Trim())
                    {
                        case "M": contRel = "Mother";
                            break;
                        case "F": contRel = "Father";
                            break;
                        case "B": contRel = "Brother";
                            break;
                        case "S": contRel = "Sister";
                            break;
                        case "P": contRel = "Spouse";
                            break;
                        case "A": contRel = "GrandFather";
                            break;
                        case "G": contRel = "GrandMother";
                            break;
                        case "O": contRel = "Other";
                            break;
                    }


                    lbleContactName.Text = dtPatientDetails.Rows[0]["econtact_fname"].ToString().Trim() +
                                            " " + dtPatientDetails.Rows[0]["econtact_lname"].ToString().Trim();
                    if (contRel != "")
                        lbleContactName.Text = lbleContactName.Text.Trim() + "(" + contRel + ")";

                    if (dtPatientDetails.Rows[0]["econtact_phone"].ToString().Trim() != "")
                        lbleContactName.Text = lbleContactName.Text.Trim() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>Phone: </B>" + FormatUSPhone(dtPatientDetails.Rows[0]["econtact_phone"].ToString().Trim());

                    if (dtPatientDetails.Rows[0]["Pat_Primary_Ins_ID"].ToString() != "")
                        lblPrimIns1.Text = dtPatientDetails.Rows[0]["Ins_Name"].ToString();
                    else
                        lblPrimIns1.Text = "N/A";


                    lblSSN1.Text = FormatUSSSN(dtPatientDetails.Rows[0]["Pat_SSN"].ToString());

                    if (dtPatientDetails.Rows[0]["Balance"] != DBNull.Value)
                    {
                        lblBalance1.Text = dtPatientDetails.Rows[0]["Balance"].ToString();
                    }
                    string address = "";
                    if (dtPatientDetails.Rows[0]["Pat_Address1"].ToString().Trim() != "")
                    {
                        address = address + dtPatientDetails.Rows[0]["Pat_Address1"].ToString().Trim() + ", ";
                    }
                    if (dtPatientDetails.Rows[0]["Pat_Address2"].ToString().Trim() != "")
                    {

                        address = address + dtPatientDetails.Rows[0]["Pat_Address2"].ToString().Trim() + ", ";
                    }
                    if (dtPatientDetails.Rows[0]["Pat_City"].ToString().Trim() != "")
                    {
                        address = address + dtPatientDetails.Rows[0]["Pat_City"].ToString().Trim() + ",";
                    }
                    string Saddress = "";
                    if (dtPatientDetails.Rows[0]["Pat_Ship_Address1"].ToString().Trim() != "")
                    {
                        Saddress = Saddress + dtPatientDetails.Rows[0]["Pat_Ship_Address1"].ToString().Trim() + ", ";
                    }
                    if (dtPatientDetails.Rows[0]["Pat_Ship_Address2"].ToString().Trim() != "")
                    {

                        Saddress = Saddress + dtPatientDetails.Rows[0]["Pat_Ship_Address2"].ToString().Trim() + ", ";
                    }
                    if (dtPatientDetails.Rows[0]["Pat_Ship_City"].ToString().Trim() != "")
                    {
                        Saddress = Saddress + dtPatientDetails.Rows[0]["Pat_Ship_City"].ToString().Trim() + ",";
                    }

                    lblAddress12.Text = address + dtPatientDetails.Rows[0]["Pat_State"].ToString() + " " + dtPatientDetails.Rows[0]["Pat_Zip"].ToString();
                    lblSAddress12.Text = Saddress + dtPatientDetails.Rows[0]["Pat_Ship_State"].ToString() + " " + dtPatientDetails.Rows[0]["Pat_Ship_Zip"].ToString();
                   
                    lblPatientContact1.Text = dtPatientDetails.Rows[0]["Pat_Address1"].ToString() + "," + dtPatientDetails.Rows[0]["Pat_Address2"].ToString() + ", " + dtPatientDetails.Rows[0]["Pat_City"].ToString() + ", " + dtPatientDetails.Rows[0]["Pat_State"].ToString() + " " + dtPatientDetails.Rows[0]["Pat_Zip"].ToString() + ", " + FormatUSPhone(dtPatientDetails.Rows[0]["Pat_Phone"].ToString());
                    lblPatAccountName.Text = txtPatientName1.Text;
                    patID = (int)(dtPatientDetails.Rows[0]["Pat_ID"]);
                    Session["Pat_ID"] = patID;
                    patFacID = int.Parse(dtPatientDetails.Rows[0]["Facility_ID"].ToString());

                    txtCallLogDoctor.Text = dtPatientDetails.Rows[0]["Pat_PDoc"].ToString();
                    lblDoctor1.Text = dtPatientDetails.Rows[0]["Pat_PDoc"].ToString();

                    lblCliniFacility1.Text = objPat_Info.Get_Facility(patFacID)[0].ToString();
                    string[] Diagn = new string[3];
                    char[] split = { ',' };
                    string Data = dtPatientDetails.Rows[0][18].ToString();
                    lbldcode1.Text = Data;
                    Diagn = Data.Split(',');
                    if (dtPatientDetails.Rows[0][19].ToString() == "Y")

                        lblAutofill1.Text = "Yes";
                    else
                        lblAutofill1.Text = "No";

                    hidPatID.Value = dtPatientDetails.Rows[0]["Pat_ID"].ToString();
                    AutoCompleteExtender3.ContextKey = dtPatientDetails.Rows[0]["Pat_ID"].ToString();
                    ACE_CLOG_Doctor.ContextKey = dtPatientDetails.Rows[0]["Pat_ID"].ToString();
                    if (Session["Role"].ToString() == "D")
                        btnEdit.Visible = false;
                    else
                    {
                        btnEdit.Visible = true;
                        btnAppointments.Visible = true;
                    }

                    if (dtPatientDetails.Rows[0]["HIPPA"].ToString() == "Y")
                    {
                        lblHippa1.Text = "Yes ( " + dtPatientDetails.Rows[0]["HIPPADate"].ToString() + " )";
                    }
                    else
                        lblHippa1.Text = "No";


                    //Hipaa End
                    PatientLastprescription(dtPatientDetails.Rows[0]["Pat_ID"].ToString());
                    PatientLastVisit(dtPatientDetails.Rows[0]["Pat_ID"].ToString());
                    String mname = "";
                    if (dtPatientDetails.Rows[0]["Pat_MName"].ToString().Trim() != mname)
                        mname = " " + dtPatientDetails.Rows[0]["Pat_MName"].ToString().Trim() + ".";

                    txtPatientName1.Text = dtPatientDetails.Rows[0]["Pat_LName"].ToString() + "," + dtPatientDetails.Rows[0]["Pat_FName"].ToString();
                    //Added Patient Appointments - START.
                    DataSet dsAppts = objPat_Info.Get_Appt(patID.ToString());
                    if (dsAppts != null && dsAppts.Tables.Count > 0)
                    {
                        if (dsAppts.Tables[0].Rows.Count > 0)
                        {
                            ContentPlaceHolder mpContentPlaceHolder;
                            mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                            if (mpContentPlaceHolder != null)
                            {
                                HtmlContainerControl divAppt = (HtmlContainerControl)mpContentPlaceHolder.FindControl("divAppt");
                                if (divAppt != null)
                                {
                                    DataGrid grdAppts = new DataGrid();
                                    grdAppts.AutoGenerateColumns = false;
                                    grdAppts.HeaderStyle.CssClass = "medication_info_th1";
                                    grdAppts.ItemStyle.CssClass = "medication_info_tr-odd";
                                    grdAppts.ItemStyle.Font.Bold = false;
                                    grdAppts.AlternatingItemStyle.Font.Bold = false;
                                    grdAppts.AlternatingItemStyle.CssClass = "medication_info_tr-even";

                                    BoundColumn nameColumn = new BoundColumn();

                                    nameColumn = new BoundColumn();
                                    nameColumn.HeaderText = "Time";
                                    nameColumn.DataField = "APPT_Time";
                                    nameColumn.ItemStyle.Width = 100;
                                    grdAppts.Columns.Add(nameColumn);

                                    nameColumn = new BoundColumn();
                                    nameColumn.HeaderText = "Doctor";
                                    nameColumn.DataField = "Doctor_Name";
                                    nameColumn.ItemStyle.Width = 100;
                                    grdAppts.Columns.Add(nameColumn);

                                    grdAppts.DataSource = dsAppts;
                                    grdAppts.DataBind();
                                    System.IO.StringWriter sw = new System.IO.StringWriter();
                                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                                    grdAppts.RenderControl(htw);

                                    StringBuilder sb = new StringBuilder();

                                    sb.Append("<a onmouseout=popUp(event,'divPatAppt')  onmouseover=popUp(event,'divPatAppt') style='cursor:hand' title='Appointments'><img src='../Images/pat_appt.gif' width='32px' height='32px' alt=''/></a>");

                                    sb.Append("<div id='divPatAppt' class='tip' style='width:auto;'>");
                                    sb.Append(sw.ToString());
                                    sb.Append("</div>");
                                    divAppt.InnerHtml = sb.ToString();
                                }
                            }
                        }
                    }
                    ShowPatientAllergies(pat_ID);
                    ClearAllPatientTabs();
                    FillgridPriscrition();

                }
                // //Added Patient Appointments - END.
                else
                {
                    SetlblVisibility(0);
                    hidPatID.Value = "";
                    string script = @"if(confirm('Patient does not exist, Do you want to add a new Patient!')) { window.location.href='PatientProfile.aspx?patname=" + txtPatientName1.Text + "';}";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed...");
    }

    private void ClearAllPatientTabs()
    {
        gridPrisInfo.DataSource = null;
        gridPrisInfo.DataBind();

        gridPatMedicalInfo.DataSource = null;
        gridPatMedicalInfo.DataBind();

        gridPatAllergies.DataSource = null;
        gridPatAllergies.DataBind();

        gridPatInsurance.DataSource = null;
        gridPatInsurance.DataBind();

        gridNotes.DataSource = null;
        gridNotes.DataBind();
       
        gridCallog.DataSource = null;
        gridCallog.DataBind();       

        gridPayments.DataSource = null;
        gridPayments.DataBind();
              
        gridShipLog.DataSource = null;
        gridShipLog.DataBind();

        gridDocuments.DataSource = null;
        gridDocuments.DataBind();

        grdBilling.DataSource = null;
        grdBilling.DataBind();

    }

    #endregion


    #region Patient Prescriptions

    protected void FillgridPriscrition()
    {
        objNLog.Info("Function Started...");
        try
        {
            if (lblDOB1.Text == "" || lblGender1.Text == "")
            {
                btnAddPresc.Enabled = false;
                btnAddAllergy.Enabled = false;
                btnAddCallLog.Enabled = false;
                btnAddPayments.Enabled = false;
                btnAddMed1.Enabled = false;
                btnAddIns.Enabled = false;
                btnAddDoc.Enabled = false;
            }
            else
            {
                btnAddPresc.Enabled = true;
                btnAddAllergy.Enabled = true;
                btnAddCallLog.Enabled = true;
                btnAddPayments.Enabled = true;
                btnAddMed1.Enabled = true;
                btnAddIns.Enabled = true;
                btnAddDoc.Enabled = true;
            }
            if (hidPatID.Value.ToString() != "")
            {
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
                gridPrisInfo.DataSource = objPat_Info.get_DrugInfo(objPat_Pat_Det);
                gridPrisInfo.DataBind();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed...");
    }
    protected void gridPrisInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gridPrisInfo.EditIndex = e.NewEditIndex;
            FillgridPriscrition();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPrisInfo_DataBound(object sender, GridViewRowEventArgs e) 
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkRxDoc = (LinkButton)e.Row.Cells[14].FindControl("lnkRxAttachment");
                Label lblRxItemID = (Label)e.Row.Cells[2].FindControl("lblRXItem_ID");
                DataTable dtRxDoc = objPat_Info.GetPatRxDocument(int.Parse(lblRxItemID.Text));
                if (dtRxDoc.Rows.Count > 0)
                {
                    if (dtRxDoc.Rows[0][0] != DBNull.Value)
                        lnkRxDoc.Visible = true;
                    else
                        lnkRxDoc.Visible = false;
                }
                else
                    lnkRxDoc.Visible = false;
                if ((DataBinder.Eval(e.Row.DataItem, "RXStatusDesc")).ToString() == "Transmitted" && (string)Session["Role"] == "C") 
                {
                    e.Row.Cells[8].Enabled = false;
                    e.Row.Cells[0].FindControl("lnkEditDrug").Visible = true;
                }
                if ((DataBinder.Eval(e.Row.DataItem, "RXStatusDesc")).ToString() != "Transmitted" && (string)Session["Role"] == "C") 
                {
                    e.Row.Cells[1].FindControl("lnkDeleteDrug").Visible = false;
                    e.Row.Cells[0].FindControl("lnkEditDrug").Visible = false;
                }
                if ((string)Session["Role"] == "D" || (string)Session["Role"] == "N")
                {
                    e.Row.Cells[1].FindControl("lnkDeleteDrug").Visible = false;
                    e.Row.Cells[0].FindControl("lnkEditDrug").Visible = false;
                }
                if ((DataBinder.Eval(e.Row.DataItem, "RXTypeDesc")).ToString() == "Sample")
                {
                    LinkButton lnkmedrequest = (LinkButton)e.Row.Cells[10].FindControl("lnkMedRequest");
                    lnkmedrequest.Text = "SampleRequest";
                }
                if ((DataBinder.Eval(e.Row.DataItem, "RXStatusDesc")).ToString() == "Other Pharmacy") //Other Pharmacy Rx
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#330099");
                    e.Row.ToolTip = "Other Pharmacy Rx";
                    if ((string)Session["Role"] == "C")
                    {
                        e.Row.Cells[1].FindControl("lnkDeleteDrug").Visible = true;
                        e.Row.Cells[0].FindControl("lnkEditDrug").Visible = true;
                    }
                }
                else if ((DataBinder.Eval(e.Row.DataItem, "flag")).ToString() == "2") //Med Requests
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#990033");
                    e.Row.ToolTip = "MedRequest";

                    e.Row.Cells[9].FindControl("lnkRefills").Visible = false;
                    e.Row.Cells[10].FindControl("lnkMedRequest").Visible = false;
                    e.Row.Cells[11].FindControl("lnkProcess").Visible = false;
                    e.Row.Cells[10].FindControl("lnkDC").Visible = false;
                    e.Row.Cells[1].FindControl("lnkDeleteDrug").Visible = false;
                    e.Row.Cells[0].FindControl("lnkEditDrug").Visible = false;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#336600");
                    e.Row.ToolTip = "ADiO Rx";
                }
                if ((DataBinder.Eval(e.Row.DataItem, "RXStatusDesc")).ToString() != "Dispatched")
                {
                    e.Row.Cells[10].FindControl("lnkDC").Visible = false;
                }
                if ((DataBinder.Eval(e.Row.DataItem, "RXStatusDesc")).ToString() == "Discontinued")
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0080");
                    e.Row.ToolTip = "Discontinued";
                    e.Row.Cells[9].FindControl("lnkRefills").Visible = false;
                }
                if ((string)Session["Role"] == "A" || (string)Session["Role"] == "M")
                {
                    e.Row.Cells[0].FindControl("lnkEditDrug").Visible = true;
                    e.Row.Cells[1].FindControl("lnkDeleteDrug").Visible = true;
                }
                
                if ((DataBinder.Eval(e.Row.DataItem, "Rx_SysFlag")).ToString() == "P")
                {
                    if ((string)Session["Role"] == "A" || (string)Session["Role"] == "T")
                    {
                        e.Row.Cells[0].FindControl("lnkEditDrug").Visible = true;
                        e.Row.Cells[1].FindControl("lnkDeleteDrug").Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[0].FindControl("lnkEditDrug").Visible = false;
                        e.Row.Cells[1].FindControl("lnkDeleteDrug").Visible = false;
                    }
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8D38C9");
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPrisInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gridPrisInfo.EditIndex = -1;
            FillgridPriscrition();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPrisInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            Label lblRxItemID = new Label();
            lblRxItemID = (Label)(gridPrisInfo.Rows[e.RowIndex].FindControl("lblRXItem_ID"));
            Label lblRxFlag = (Label)gridPrisInfo.Rows[e.RowIndex].FindControl("lblRxFlag");
            GridView grd = sender as GridView;
            int key = 0;
            if (lblRxFlag.Text == "2")
                key = 1;
            else
                key = 0;
            objPat_Info.delete_patPrescriptionMedInfo(userID, key, lblRxItemID.Text,int.Parse((string)hidPatID.Value));
            FillgridPriscrition();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPrisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRx")
            {
                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);

                Label lblRxItemID = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblRXItem_ID");
                Label lblMed = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lbldrugName");
                Label lblQty = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblQty");
                Label lblRefills = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblRefills");
                Label lblSIG = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblSIG");
                Label lblSatus = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblSatus");
                Label lblRxStatusCode = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblRxStatusCode");
                Label lblrxType = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblrxType");
                Label lblPhrm = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblPhrm");
                Label lblRxFlag = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblRxFlag");
                Label lblRxCmt = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblRxCmts");
                Label lblRxFillDate = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblRxFillDate");
                Label lblDocName = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblDoc_FullName");

                lblEditDocName.Text = lblDocName.Text.Trim();

                lblEditPhrm.Text = lblPhrm.Text.Trim();

                hidRxItemID.Value = lblRxItemID.Text.Trim();

                txtEditDrugName.Text = lblMed.Text.Trim();

                txtEditQty.Text = lblQty.Text.Trim();

                ddlEditRxRefills.SelectedIndex = int.Parse(lblRefills.Text.Trim());

                txtEditSIG.Text = lblSIG.Text.Trim();

                //ddlEditRxStatus.SelectedItem.Text = lblSatus.Text.Trim();

                ddlEditRxStatus.SelectedValue = lblRxStatusCode.Text.Trim();

                chkSetRxDate.Checked = false;

                txtEditFillDate.Text = lblRxFillDate.Text.Trim();

                txtEditRxCmt.Text = lblRxCmt.Text.Trim();

                if ((string)Session["Role"] != "A" && (string)Session["Role"] != "M")
                    chkSetRxDate.Enabled = false;
                //if (lblSatus.Text.Trim() == "Transmitted" && (string)Session["Role"] == "C")
                //    ddlEditRxStatus.Enabled = false;

                if (lblrxType.Text.Trim() == "Regular")
                {
                    rbtnEditRxTypeR.Checked = true;
                    rbtnEditRxTypeP.Checked = false;
                    rbtnEditRxTypeS.Checked = false;
                }

                if (lblrxType.Text.Trim() == "PAP")
                {
                    rbtnEditRxTypeP.Checked = true;
                    rbtnEditRxTypeR.Checked = false;
                    rbtnEditRxTypeS.Checked = false;
                }

                if (lblrxType.Text.Trim() == "Sample")
                {
                    rbtnEditRxTypeS.Checked = true;
                    rbtnEditRxTypeP.Checked = false;
                    rbtnEditRxTypeR.Checked = false;
                }
                hidEditRxFlag.Value = lblRxFlag.Text;
                mpeEditPrescription.Show();
            }

            if (e.CommandName == "RxAttachment")
            {
                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);
                Label lblRxItemID = (Label)gridPrisInfo.Rows[intRowIndex].FindControl("lblRXItem_ID");
                
               // DataTable dtRxDoc  = objPat_Info.GetPatRxDocument(int.Parse(lblRxItemID.Text));

                LinkButton lnkRxDoc = (LinkButton)gridPrisInfo.Rows[intRowIndex].FindControl("lnkRxAttachment");

              //  if (dtRxDoc.Rows.Count > 0)
              //  {
              //      if (dtRxDoc.Rows[0][0] != DBNull.Value)
              //      {
              //          if (Session["image"] != null) 
              //          {
              //              Session["image"] = null;
              //          } 
                      
              //          ////System.Drawing.Image newImage;
              //          byte[] rxDoc = (byte[])dtRxDoc.Rows[0][0];
              //          Session["image"] = rxDoc;
              //          imgRxDoc.ImageUrl = "RxAttachment.aspx";
              //          MPERxAttachment.Show();
              //}
              //  }

                        imgRxDoc.ImageUrl = "RxAttachment.aspx?RxItemID=" + lblRxItemID.Text;
                        MPERxAttachment.Show();
                    
           

                //Added Doctor Signature Upload - END.
            }
            
            if (e.CommandName == "Refill")
            {
                objPat_Info.Refill_patPrescriptionMedInfo(e.CommandArgument.ToString(), (string)Session["User"], int.Parse((string)hidPatID.Value));
                FillgridPriscrition();
            }
            if (e.CommandName == "MedRequest")
            {
                Response.Redirect("~/Rx/MedRequest.aspx?RxItemID=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Processing")
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("sp_getSampleDrugInfo", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par_Rx_drugId = sqlCmd.Parameters.Add("@RxDrugid", SqlDbType.Int);
                par_Rx_drugId.Value = e.CommandArgument.ToString();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                hfRXItemID.Value = e.CommandArgument.ToString();
                if (ds.Tables[0].Rows[0][1].ToString() == "S")
                {
                    //if (int.Parse(ds.Tables[0].Rows[0][3].ToString()) > 2)
                    //    lblProcesingAlert.Visible = true;
                    //else
                    //    lblProcesingAlert.Visible = false;

                    lblLotNum.Visible = true;
                    txtLotNum.Visible = true;
                    lblExpiryDate.Visible = true;
                    txtExpiryDate.Visible = true;
                    lblExpFormat.Visible = true;
                    txtLotNum.Text = "";
                    txtExpiryDate.Text = "";
                    txtQtyProcessed.Text = "";
                    txtProcessingComments.Text = "";
                }
                else
                {

                    lblLotNum.Visible = false;
                    txtLotNum.Visible = false;
                    lblExpiryDate.Visible = false;
                    txtExpiryDate.Visible = false;
                    lblExpFormat.Visible = false;

                }
                lblProcessingDrug1.Text = ds.Tables[0].Rows[0][0].ToString();
                lblQtyinStock1.Text = ds.Tables[0].Rows[0][2].ToString();
                if (lblQtyinStock1.Text == "" || lblQtyinStock1.Text == "0")
                {
                    lblQtyinStock1.Text = "No Stock";
                    //btn_Process_Save.Enabled = false;
                }
                else
                {
                    btn_Process_Save.Enabled = true;
                }
                rbtnProcessingType.SelectedValue = ds.Tables[0].Rows[0][1].ToString();
                rbtnProcessingType.Enabled = false;
                MPE_SamplePAPProcessing.Show();
            }
            if (e.CommandName == "DC")
            {

                hfDC_RXItemID.Value = e.CommandArgument.ToString();
                MPE_DC_Processing.Show();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPrisInfo_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridPrisInfo.PageIndex = e.NewPageIndex;
            FillgridPriscrition();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void btn_Process_Save_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Process Click Event Started...");
        string userID = (string)Session["User"];
        //SqlConnection sqlCon = new SqlConnection(conStr);
        //SqlCommand sqlCmd;
        string sqlQuery = "";
        int i, j;
        i = int.Parse(lblQtyinStock1.Text);
        j = int.Parse(txtQtyProcessed.Text);

        if (j > i)
        {
            string str = "alert('Quantity removed can not be greater than available stock!');";
            //else
            // str = "alert('Select Drug');";
            ScriptManager.RegisterStartupScript(btn_Process_Save, typeof(Page), "alert", str, true);
            MPE_SamplePAPProcessing.Show();
        }
        else
        {
              
            //if (rbtnProcessingType.SelectedValue == "S")
            //{
            //    sqlQuery = "Insert INTO Drug_Inventory   ([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],Pat_ID,Facility_ID,Lot_Num,Expiry_Date,LastModified,LastModifiedBy,Clinic_ID)    VALUES ('"
            //   + rbtnProcessingType.SelectedValue + "','R',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + lblProcessingDrug1.Text + "'),'-" + txtQtyProcessed.Text + "','" + txtProcessingComments.Text + "','Tablet','" + hidPatID.Value.ToString() + "',(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hidPatID.Value.ToString() + "),'" + txtLotNum.Text + "','" + txtExpiryDate.Text + "', getdate(),'" + Session["User"] + "', (select Clinic_ID from Facility_Info where Facility_ID=(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hidPatID.Value.ToString() + ")))";

            //}
            //else
            //{
            //    sqlQuery = "Insert INTO Drug_Inventory   ([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],Pat_ID,Facility_ID,LastModified,LastModifiedBy, Clinic_ID)    VALUES ('"
            //    + rbtnProcessingType.SelectedValue + "','R',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + lblProcessingDrug1.Text + "'),'-" + txtQtyProcessed.Text + "','" + txtProcessingComments.Text + "','Tablet'," + hidPatID.Value.ToString() + ",(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hidPatID.Value.ToString() + "),getdate(),'" + Session["User"] + "', (select Clinic_ID from Facility_Info where Facility_ID=(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hidPatID.Value.ToString() + ")))";

            //}

            //if(rbtnProcessingType.SelectedValue=="S")
            //  sqlQuery = "Insert INTO Drug_Inventory([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],Pat_ID,Facility_ID,Lot_Num,Expiry_Date,LastModified,LastModifiedBy)    VALUES ('"
            //+ rbtnProcessingType.SelectedValue + "','R',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + lblProcessingDrug1.Text + "'),'-" + txtQtyProcessed.Text + "','" + txtProcessingComments.Text + "','Tablet','" + hidPatID.Value.ToString() + "',(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hidPatID.Value.ToString() + "),'" + txtLotNum.Text  + "','" + txtExpiryDate.Text  + "', getdate(),'" + Session["User"] + "')";
            //else
            //  sqlQuery = "Insert INTO Drug_Inventory   ([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],Pat_ID,Facility_ID,LastModified,LastModifiedBy)    VALUES ('"
            //+ rbtnProcessingType.SelectedValue + "','R',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + lblProcessingDrug1.Text + "'),'-" + txtQtyProcessed.Text + "','" + txtProcessingComments.Text + "','Tablet','" + hidPatID.Value.ToString() + "',(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hidPatID.Value.ToString() + "), getdate(),'" + Session["User"] + "')";

            //if (ddlProcessingStatus.SelectedValue != "R")
            //    sqlQuery = sqlQuery +" "+ "Update Rx_Drug_Info SET Rx_Status='D' where Rx_ItemID=" + hfRXItemID.Value;

            try
            {
                objPat_Info.Set_Drug_Inventory(char.Parse(rbtnProcessingType.SelectedValue), lblProcessingDrug1.Text, txtQtyProcessed.Text, txtProcessingComments.Text, int.Parse(hidPatID.Value.ToString()), txtLotNum.Text, txtExpiryDate.Text, userID);

                if (ddlProcessingStatus.SelectedValue != "R")
                {
                    objPat_Info.Update_Drug_Info(int.Parse(hfRXItemID.Value),userID);
                }
                //sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                //sqlCon.Open();
                //sqlCmd.ExecuteNonQuery();

                //if (ddlProcessingStatus.SelectedValue != "R")
                //    objUALog.LogUserActivity(conStr, userID, "Updated field Rx_Status='D' where Rx_ItemID=" + hfRXItemID.Value + " while saving process info.", "Rx_Drug_Info", 0);
                
                //if (rbtnProcessingType.SelectedValue == "S")
                //    objUALog.LogUserActivity(conStr, userID, "Inserted Record into table while saving process info.", "Drug_Inventory", 0);
            }
            catch (Exception ex)
            {
                objNLog.Error("Error : " + ex.Message);
            }
            //finally
            //{
            //    sqlCon.Close();
            //}
            FillgridPriscrition();
        }
        objNLog.Info("Save Process Click Event Completed...");
    }
    protected void btn_DC_Save_Click(object sender, EventArgs e)
    {
        
        SqlConnection sqlCon = new SqlConnection(conStr);
        
        //string sqlQuery = "";
        string userID = (string)Session["User"];

        //sqlQuery = "Update Rx_Drug_Info SET Rx_Status='X', Rx_Comments='" + txtDCComments.Text + "',Rx_ApprovedBy='" + (string)Session["User"] + "' where Rx_ItemID=" + hfDC_RXItemID.Value;
        try
        {
            //sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            SqlCommand sqlCmd=new SqlCommand("sp_Update_RxDrugInfo_DC_Status", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_RxItemID = sqlCmd.Parameters.Add("@Rx_ItemID", SqlDbType.Int);
            sp_RxItemID.Value=int.Parse(hfDC_RXItemID.Value);

            SqlParameter sp_Rx_Status = sqlCmd.Parameters.Add("@Rx_Status", SqlDbType.Char, 1);
            sp_Rx_Status.Value = 'X';

            SqlParameter sp_Rx_Comments = sqlCmd.Parameters.Add("@Rx_Comments", SqlDbType.VarChar,1000);
            sp_Rx_Comments.Value = txtDCComments.Text;

            SqlParameter sp_Rx_ApprovedBy = sqlCmd.Parameters.Add("@Rx_ApprovedBy", SqlDbType.VarChar,50);
            sp_Rx_ApprovedBy.Value = userID;
           
            sqlCon.Open();

            sqlCmd.ExecuteNonQuery();

            objUALog.LogUserActivity(conStr, userID, "Updated Patient Rx Info with Rx_ItemID=" + hfDC_RXItemID.Value.ToString(), "Rx_Drug_Info", int.Parse((string)hidPatID.Value));
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        FillgridPriscrition();
    }
    protected void btn_P_Save_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Prescription Click Event Started...");
        //if (CheckIfDuplicateRequest() )
        //{
       //     FillgridPriscrition();
       // }
        //else
       // {
            PatinetMedHistoryInfo objPat_Med_His_Info = new PatinetMedHistoryInfo();
            string userID = (string)Session["User"];

            // if (objPat_Info.get_MedicationNames(txt_P_Med.Text).Rows.Count <= 0)
            // {
                //objPat_Info.set_newMedication(userID, txt_P_Med.Text);
            // }

            int DrugID = objPat_Info.Get_MedID(txt_P_Med.Text);

            int DocID = objPat_Info.Get_DocID(txtDocName.Text);

            if (DocID > 0 && DrugID > 0)
            {
                string Pharmacy = "Adio Pharmacy";
                if (rbtnOtherPharmacy.Checked)
                    Pharmacy = txtPharmacy.Text;
                string rxtype = "";
                if (rbtnPAP.Checked)
                    rxtype = "P";
                if (rbtnSample.Checked)
                    rxtype = "S";
                if (rbtnRegular.Checked)
                    rxtype = "R";

                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("SetRXData", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                try
                {
                    if (hidRXID.Value == "")
                    {
                        SqlParameter sql_Doc_ID = sqlCmd.Parameters.Add("@Doc_ID", SqlDbType.Int);
                        sql_Doc_ID.Value = DocID;
                        SqlParameter sql_Rx_PharmacyName = sqlCmd.Parameters.Add("@Rx_PharmacyName", SqlDbType.VarChar, 50);
                        sql_Rx_PharmacyName.Value = Pharmacy;
                    }
                    else
                    {
                        SqlParameter sql_Rx_ID = sqlCmd.Parameters.Add("@Rx_ID", SqlDbType.Int);
                        sql_Rx_ID.Value = int.Parse(hidRXID.Value);
                    }
                    SqlParameter sql_Pat_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
                    sql_Pat_ID.Value = int.Parse(hidPatID.Value);
                    SqlParameter sql_Rx_Type = sqlCmd.Parameters.Add("@Rx_Type", SqlDbType.Char, 1);
                    sql_Rx_Type.Value = rxtype;
                    SqlParameter sql_Rx_DrugName = sqlCmd.Parameters.Add("@Rx_DrugName", SqlDbType.VarChar, 50);
                    sql_Rx_DrugName.Value = txt_P_Med.Text.Trim();
                    SqlParameter sql_Rx_Qty = sqlCmd.Parameters.Add("@Rx_Qty", SqlDbType.Int);
                    sql_Rx_Qty.Value = txt_P_Qty.Text;
                    SqlParameter sql_Rx_Refills = sqlCmd.Parameters.Add("@Rx_Refills", SqlDbType.Int);
                    sql_Rx_Refills.Value = ddl_P_Refills.SelectedValue;
                    //Added for SIG Codes Description - START.
                    string[] sig;
                    if (txt_P_sig.Text != "" && txt_P_sig.Text.Contains("||"))
                    {
                        sig = txt_P_sig.Text.Split(new string[] { "||" }, StringSplitOptions.None);
                        objPat_Med_His_Info.SIG = sig[1].TrimStart().ToString();
                    }
                    else
                        objPat_Med_His_Info.SIG = txt_P_sig.Text;

                    SqlParameter sql_Rx_SIG = sqlCmd.Parameters.Add("@Rx_SIG", SqlDbType.VarChar, 50);
                    sql_Rx_SIG.Value = objPat_Med_His_Info.SIG;
                    //Added for SIG Codes Description - END.
                    SqlParameter sql_Rx_Status = sqlCmd.Parameters.Add("@Rx_Status", SqlDbType.Char, 1);
                    sql_Rx_Status.Value = ddl_P_Status.SelectedValue;

                   

                    SqlParameter sql_Rx_Comments = sqlCmd.Parameters.Add("@Rx_Comments", SqlDbType.VarChar, 1000);
                    sql_Rx_Comments.Value = txtRxComments.Text;

                    SqlParameter sql_Rx_MBy = sqlCmd.Parameters.Add("@Rx_ModifiedBy", SqlDbType.VarChar, 50);
                    sql_Rx_MBy.Value = (string)Session["User"];

                    SqlParameter sql_Rx_FillDate = sqlCmd.Parameters.Add("@Rx_FillDate", SqlDbType.VarChar, 50);
                    sql_Rx_FillDate.Value = txtFillDate.Text;
                    if (txtFillDate.Text.Trim() == "")
                        sql_Rx_FillDate.Value = DateTime.Now.ToShortDateString();


                    //Added for Rx Document Attachment 

                    byte[] rxDoc = null;
                    if (FileUpRxDoc.PostedFile != null && FileUpRxDoc.PostedFile.FileName != "")
                    {
                        rxDoc = new byte[FileUpRxDoc.PostedFile.ContentLength];
                        HttpPostedFile Image = FileUpRxDoc.PostedFile;
                        Image.InputStream.Read(rxDoc, 0, (int)FileUpRxDoc.PostedFile.ContentLength);
                    }

                    SqlParameter Rx_Doc = sqlCmd.Parameters.Add("@Rx_Doc", SqlDbType.Image);
                    if (rxDoc != null)
                        Rx_Doc.Value = rxDoc;
                    else
                        Rx_Doc.Value = Convert.DBNull;
                    //Added for Rx Document Attachment 

                    SqlParameter sql_Rx_SysFlag = sqlCmd.Parameters.Add("@Rx_SysFlag", SqlDbType.VarChar, 50);
                    if(chkRx30Patient.Checked==true)
                        sql_Rx_SysFlag.Value = "P";
                    else
                        sql_Rx_SysFlag.Value = "R";

                    SqlParameter sql_RXID = sqlCmd.Parameters.Add("@RXID", SqlDbType.Int);
                    sql_RXID.Direction = ParameterDirection.Output;

                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();
                    objUALog.LogUserActivity(conStr, userID, "Added New Prescription(Rx) Info. RxID = " + sql_RXID.Value.ToString(), "Patient_Rx, Rx_Drug_Info", int.Parse((string)hidPatID.Value));
                    FillgridPriscrition();
                    //Session["DupRxRecord"] = txt_P_Med.Text;
                    //Session["DupPatRecord"] = hidPatID.Value;
                    //Session["DupRxFillDt"] = txtFillDate.Text;
                    rbtnAdioPharmacy.Checked = true;
                    rbtnOtherPharmacy.Checked = false;
                    lblPresStatus.Text = "";
                    //lblPresStatus.Visible = false;
                    lblPresStatus.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "hidden";
                    Response.Redirect("AllPatientProfile.aspx?patID=" + hidPatID.Value.ToString());

                }
                catch (Exception ex)
                {
                    objNLog.Error("Error : " + ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }

            }
            else if (DrugID == 0)
            {
               // lblPresStatus.Visible = true;
                lblPresStatus.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

                lblPresStatus.Text="Error: Invalid Drug Name..!";
                addPrescription.Show();

            }
            else if (DocID == 0)
            {
                //lblPresStatus.Visible = true;
                lblPresStatus.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

                lblPresStatus.Text = "Error: Invalid Doctor Name..!";
                addPrescription.Show();

            }
             
        //}
        objNLog.Info("Save Prescription Click Event Completed...");
    }
    protected void imgBtnUpdateRx_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            char setrxdate = 'N';
            int key = 0;
            if (hidEditRxFlag.Value == "2")
                key = 2;
            else
                key = 0;
            char rxType = 'R';
            if (rbtnEditRxTypeR.Checked == true) rxType = 'R';
            if (rbtnEditRxTypeS.Checked == true) rxType = 'S';
            if (rbtnEditRxTypeP.Checked == true) rxType = 'P';

            if (chkSetRxDate.Checked == true) setrxdate = 'Y';
           
            byte[] rxDoc = null;
            if (fupEditRxDoc.PostedFile != null && fupEditRxDoc.PostedFile.FileName != "")
            {
                rxDoc = new byte[fupEditRxDoc.PostedFile.ContentLength];
                HttpPostedFile Image = fupEditRxDoc.PostedFile;
                Image.InputStream.Read(rxDoc, 0, (int)fupEditRxDoc.PostedFile.ContentLength);
            }

         
            objPat_Info.update_patPrescriptionMedInfo(userID, key, txtEditDrugName.Text.Trim(), int.Parse(txtEditQty.Text), txtEditSIG.Text.Trim(), int.Parse(ddlEditRxRefills.SelectedValue), char.Parse(ddlEditRxStatus.SelectedValue), rxType, int.Parse(hidRxItemID.Value.ToString()), int.Parse(hidPatID.Value.ToString()), txtEditRxCmt.Text, setrxdate, txtEditFillDate.Text, rxDoc);
            gridPrisInfo.EditIndex = -1;
            FillgridPriscrition();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btn_P_Save_Continue_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Prescription Continue Click Event Started...");
        //if (CheckIfDuplicateRequest())
        //{
        //    FillgridPriscrition();
        //}
        //else
        //{
            PatinetMedHistoryInfo objPat_Med_His_Info = new PatinetMedHistoryInfo();
            string userID = (string)Session["User"];
            int DocID = objPat_Info.Get_DocID(txtDocName.Text);
            int DrugID = objPat_Info.Get_MedID(txt_P_Med.Text);

            if (DocID > 0 && DrugID > 0)
            {
                string Pharmacy = "AdiO Pharmacy";
                if (rbtnOtherPharmacy.Checked)
                {
                    if (txtPharmacy.Text.Trim() == "")
                        Pharmacy = "Other Pharmacy";
                    else
                        Pharmacy = txtPharmacy.Text;
                }

                string rxtype = "";
                if (rbtnPAP.Checked)
                    rxtype = "P";
                if (rbtnSample.Checked)
                    rxtype = "S";
                if (rbtnRegular.Checked)
                    rxtype = "R";
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("SetRXData", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;

                try
                {
                    if (hidRXID.Value == "")
                    {
                        SqlParameter sql_Doc_ID = sqlCmd.Parameters.Add("@Doc_ID", SqlDbType.Int);
                        sql_Doc_ID.Value = DocID;

                        SqlParameter sql_Rx_PharmacyName = sqlCmd.Parameters.Add("@Rx_PharmacyName", SqlDbType.VarChar, 50);
                        sql_Rx_PharmacyName.Value = Pharmacy;
                    }
                    else
                    {
                        SqlParameter sql_Rx_ID = sqlCmd.Parameters.Add("@Rx_ID", SqlDbType.Int);
                        sql_Rx_ID.Value = int.Parse(hidRXID.Value);
                    }

                    SqlParameter sql_Pat_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
                    sql_Pat_ID.Value = int.Parse(hidPatID.Value);
                    SqlParameter sql_Rx_Type = sqlCmd.Parameters.Add("@Rx_Type", SqlDbType.Char, 1);
                    sql_Rx_Type.Value = rxtype;
                    SqlParameter sql_Rx_DrugName = sqlCmd.Parameters.Add("@Rx_DrugName", SqlDbType.VarChar, 50);
                    sql_Rx_DrugName.Value = txt_P_Med.Text.Trim();
                    SqlParameter sql_Rx_Qty = sqlCmd.Parameters.Add("@Rx_Qty", SqlDbType.Int);
                    sql_Rx_Qty.Value = txt_P_Qty.Text;
                    SqlParameter sql_Rx_Refills = sqlCmd.Parameters.Add("@Rx_Refills", SqlDbType.Int);
                    sql_Rx_Refills.Value = ddl_P_Refills.SelectedValue;
                    //Added for SIG Codes Description - START.
                    string[] sig;
                    if (txt_P_sig.Text != "" && txt_P_sig.Text.Contains("||"))
                    {
                        sig = txt_P_sig.Text.Split(new string[] { "||" }, StringSplitOptions.None);
                        objPat_Med_His_Info.SIG = sig[1].TrimStart().ToString();
                    }
                    else
                        objPat_Med_His_Info.SIG = txt_P_sig.Text;

                    SqlParameter sql_Rx_SIG = sqlCmd.Parameters.Add("@Rx_SIG", SqlDbType.VarChar, 50);
                    sql_Rx_SIG.Value = objPat_Med_His_Info.SIG;
                    //Added for SIG Codes Description - END.
                    SqlParameter sql_Rx_Status = sqlCmd.Parameters.Add("@Rx_Status", SqlDbType.Char, 1);
                    sql_Rx_Status.Value = ddl_P_Status.SelectedValue;

                   

                    SqlParameter sql_Rx_Comments = sqlCmd.Parameters.Add("@Rx_Comments", SqlDbType.VarChar, 1000);
                    sql_Rx_Comments.Value = txtRxComments.Text;

                    SqlParameter sql_Rx_FillDate = sqlCmd.Parameters.Add("@Rx_FillDate", SqlDbType.VarChar, 50);
                    sql_Rx_FillDate.Value = txtFillDate.Text;
                    if (txtFillDate.Text.Trim() == "")
                        sql_Rx_FillDate.Value = DateTime.Now.ToShortDateString();

                    SqlParameter sql_Rx_MBy = sqlCmd.Parameters.Add("@Rx_ModifiedBy", SqlDbType.VarChar, 50);
                    sql_Rx_MBy.Value = (string)Session["User"];

                    //Added for Rx Document Attachment 
                    byte[] rxDoc = null;
                    if (FileUpRxDoc.PostedFile != null && FileUpRxDoc.PostedFile.FileName != "")
                    {
                        rxDoc = new byte[FileUpRxDoc.PostedFile.ContentLength];
                        HttpPostedFile Image = FileUpRxDoc.PostedFile;
                        Image.InputStream.Read(rxDoc, 0, (int)FileUpRxDoc.PostedFile.ContentLength);
                    }

                    SqlParameter Rx_Doc = sqlCmd.Parameters.Add("@Rx_Doc", SqlDbType.Image);
                    if (rxDoc != null)
                        Rx_Doc.Value = rxDoc;
                    else
                        Rx_Doc.Value = Convert.DBNull;

                    SqlParameter sql_Rx_SysFlag = sqlCmd.Parameters.Add("@Rx_SysFlag", SqlDbType.VarChar, 50);
                    if (chkRx30Patient.Checked == true)
                        sql_Rx_SysFlag.Value = "P";
                    else
                        sql_Rx_SysFlag.Value = "R";


                    //Added for Rx Document Attachment

                    SqlParameter sql_RXID = sqlCmd.Parameters.Add("@RXID", SqlDbType.Int);
                    sql_RXID.Direction = ParameterDirection.Output;
                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();
                    objUALog.LogUserActivity(conStr, userID, "Added New Prescription(Rx) Info. RxID = " + sql_RXID.Value.ToString(), "Patient_Rx, Rx_Drug_Info", int.Parse((string)hidPatID.Value));
                    FillgridPriscrition();
                    hidRXID.Value = sql_RXID.Value.ToString();
                    txtDocName.Enabled = false;
                    addPrescription.Show();
                    rbtnAdioPharmacy.Checked = true;
                    rbtnOtherPharmacy.Checked = false;
                    txt_P_Med.Text = "";
                    txt_P_Qty.Text = "";
                    txt_P_sig.Text = "";
                    txtRxComments.Text = "";
                    ddl_P_Refills.SelectedIndex = 0;
                    ddl_P_Status.SelectedIndex = 0;
                    txtFillDate.Text = DateTime.Now.ToShortDateString();
                    rbtnRegular.Checked = true;
                    //Session["DupRxRecord"] = txt_P_Med.Text;
                    //Session["DupPatRecord"] = hidPatID.Value;
                    //Session["DupRxFillDt"] = txtFillDate.Text;
                    lblPresStatus.Text = "";
                    lblPresStatus.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "hidden";

                    //lblPresStatus.Visible = false;

                }
                catch (Exception ex)
                {
                    objNLog.Error("Error : " + ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
            else if (DrugID == 0)
            {
                //lblPresStatus.Visible = true;
                lblPresStatus.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblPresStatus.Text = "Error: Invalid Drug Name..!";
                addPrescription.Show();
            }
            else if (DocID == 0)
            {
                lblPresStatus.Attributes.CssStyle[HtmlTextWriterStyle.Visibility] = "visible";

                //lblPresStatus.Visible = true;
                lblPresStatus.Text = "Error: Invalid Doctor Name..!";
                addPrescription.Show();

            }
            objNLog.Info("Save Prescription Continue Click Event Completed...");
        //}
}
    private Boolean CheckIfDuplicateRequest()
    {
        bool flag = false;
        if (Session["DupRxRecord"] == null && Session["DupPatRecord"] == null)
        {
            //Probably submitting the page for the first time
            return false;

        }
        string previousValue1 = (string)Session["DupRxRecord"];
        string previousValue2 = (string)Session["DupPatRecord"];
        string previousValue3 = (string)Session["DupRxFillDt"];

        if (previousValue1 == txt_P_Med.Text && previousValue2 == hidPatID.Value && previousValue3 == txtFillDate.Text)
        {
            //Submitting the page with a different set of values

            //string script = "needconfirm();" +
            //             "function needconfirm(){if(confirm('Duplicate Rx found for this Patient. Do you want to add same Rx again?')) { flag=false; return true; } \n else { flag=true; return false;} }";

            //ScriptManager.RegisterStartupScript(this, typeof(Page),"jsCall", script, true);

            flag = true;
        }
        else
            flag = false;
        //Duplicate request.
        return flag;

    }
    public bool display_link(string un)
    {
        //check username here and return a bool
        if (un == "true")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool display_link1(string type, string status)
    {
           //check username here and return a bool
        if ((type == "Sample" || type == "PAP") && status != "Hold" && status != "Dispatched" && status != "Denied" && status != "Discontinued")
            {
                return true;
            }
            else
            {
                return false;
            }
         
    }
    
    #endregion


    #region Patient Rx History

    protected void FillgridPatMedInfo()
    {
        objNLog.Info("Function Started...");
        try
        {
            if (hidPatID.Value.ToString() != "")
            {
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
                gridPatMedicalInfo.DataSource = objPat_Info.get_PatientMedHistory(objPat_Pat_Det);
                gridPatMedicalInfo.DataBind();
                btnAddMed1.Attributes.Add("style", "visibility:show");
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void gridPatMedicalInfo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gridPatMedicalInfo.EditIndex = e.NewEditIndex;
            FillgridPatMedInfo();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatMedicalInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txtQty1 = (TextBox)gridPatMedicalInfo.Rows[e.RowIndex].FindControl("txtQty");
            TextBox txtSIG1 = (TextBox)gridPatMedicalInfo.Rows[e.RowIndex].FindControl("txtSIG");
            DropDownList txtRefills1 = (DropDownList)gridPatMedicalInfo.Rows[e.RowIndex].FindControl("ddlRefills");
            objPat_Med_His_Info.Quantity = Int32.Parse(txtQty1.Text);
            objPat_Med_His_Info.Refills = Int32.Parse(txtRefills1.SelectedValue.ToString());
            objPat_Med_His_Info.SIG = txtSIG1.Text;
            Label lblRxItemID = new Label();
            lblRxItemID = (Label)(gridPatMedicalInfo.Rows[e.RowIndex].FindControl("lblRxid"));
            objPat_Med_His_Info.CM_ID = Int32.Parse(lblRxItemID.Text);
            objPat_Info.update_patMedInfo(objPat_Med_His_Info);
            gridPatMedicalInfo.EditIndex = -1;
            FillgridPatMedInfo();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatMedicalInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gridPatMedicalInfo.EditIndex = -1;
            FillgridPatMedInfo();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatMedicalInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            TextBox txtNote = (TextBox)gridNotes.Rows[e.RowIndex].FindControl("txtNoteDesc");
            Label lblID = (Label)gridNotes.Rows[e.RowIndex].FindControl("lblNoteID");

            objPat_Info.update_patNote(userID, txtNote.Text, lblID.Text, int.Parse((string)hidPatID.Value));
            gridNotes.EditIndex = -1;
            FillgridPatMedInfo();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatMedicalInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                objPat_Info.delete_patMedInfo(e.CommandArgument.ToString());
                FillgridPatMedInfo();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatMedicalInfo_DataBound(object sender, GridViewRowEventArgs e)
    {
      
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if ((DataBinder.Eval(e.Row.DataItem, "RxType")).ToString() == "F") //Other Pharmacy Rx
                {
                    //e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#330099");
                    //e.Row.ToolTip = "Refill";
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#330099");
                    e.Row.ToolTip = "Refill";


                }
                else
                {
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#336600");
                    e.Row.ToolTip = "Rx";
                }


            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatMedicalInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridPatMedicalInfo.PageIndex = e.NewPageIndex;
            FillgridPatMedInfo();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    #endregion


    #region Patient Allergies
    protected void ShowPatientAllergies(int patID)
    {
        objNLog.Info("Function Started...");
        try
        {
            lblPatAllergies1.Text = "";
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = patID;
                DataTable dtPatAllergies = objPat_Info.get_PatientAllergies(objPat_Pat_Det);
                if (dtPatAllergies.Rows.Count > 0)
                { 
                    for(int i=0;i<dtPatAllergies.Rows.Count;i++)
                    {
                    if(lblPatAllergies1.Text=="")
                        lblPatAllergies1.Text = dtPatAllergies.Rows[i]["Pat_Allergic_To"].ToString();
                    else
                        lblPatAllergies1.Text = lblPatAllergies1.Text + ", " + dtPatAllergies.Rows[i]["Pat_Allergic_To"].ToString();
                    }
                }
             
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void FillgridPatAllergies()
    {
        objNLog.Info("Function Started...");
        try
        {
            if (hidPatID.Value.ToString() != "")
            {
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());

                gridPatAllergies.DataSource = objPat_Info.get_PatientAllergies(objPat_Pat_Det);
                gridPatAllergies.DataBind();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void gridPatAllergies_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gridPatAllergies.EditIndex = e.NewEditIndex;
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void gridPatAllergies_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            TextBox txtATO = (TextBox)gridPatAllergies.Rows[e.RowIndex].FindControl("txtAlleryTO");
            TextBox txtADesc = (TextBox)gridPatAllergies.Rows[e.RowIndex].FindControl("txtAllergyDesc");

            Label lblPA_ID = new Label();
            lblPA_ID = (Label)(gridPatAllergies.Rows[e.RowIndex].FindControl("lblPA_ID"));

            objPat_Info.update_patAllergy(userID,txtATO.Text, txtADesc.Text, lblPA_ID.Text,int.Parse((string)hidPatID.Value));
            gridPatAllergies.EditIndex = -1;
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatAllergies_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gridPatAllergies.EditIndex = -1;
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatAllergies_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatAllergies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            if (e.CommandName == "Delete")
            {
                objPat_Info.delete_patAllergy(userID,e.CommandArgument.ToString(),int.Parse((string)hidPatID.Value));
                FillgridPatAllergies();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatAllergies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridPatAllergies.PageIndex = e.NewPageIndex;
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnSaveAllergy_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Allergry Click Event Started");
        try
        {
            string userID = (string)Session["User"];
            PatientAllergies pAllergyDetails = new PatientAllergies();
            pAllergyDetails.PatID = Int32.Parse(hidPatID.Value.ToString());
            pAllergyDetails.AllergicTo = txtAllergyTo.Text;
            pAllergyDetails.AllergyDescription = txtAllergyDesc.Text;
            objPat_Info.Set_Pat_Allergies(userID,pAllergyDetails);
            FillgridPatAllergies();

        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        objNLog.Error("Function Terminated...");
    }

    #endregion


    #region Patient Insurance

    protected void FillgridPatInsurance()
    {
        objNLog.Info("Function Started...");
        try
        {
           
            if (hidPatID.Value.ToString() != "")
            {
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
                gridPatInsurance.DataSource = objPat_Info.get_PatientInsuranceInfo(objPat_Pat_Det);
                gridPatInsurance.DataBind();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void btnSaveInsurance_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Insurance Click Event Started");
        int InsuranceID = objPat_Info.Get_InsuranceID(txtInsName.Text);//Find Ins ID
        char active = 'N';

        if (rbtnIActive.Checked)
            active = 'Y';

        try
        {
            objPat_Ins_Det.Pat_ID = Int32.Parse(hidPatID.Value.ToString());
            objPat_Ins_Det.InsuranceID = InsuranceID;
            objPat_Ins_Det.PI_PolicyID = txtPID.Text;
            objPat_Ins_Det.PI_GroupNo = txtGNO.Text;
            objPat_Ins_Det.PI_BINNo = txtBNO.Text;
            objPat_Ins_Det.InsuredName = txtIName.Text;
            objPat_Ins_Det.InsuredDOB = txtIDOB.Text;
            objPat_Ins_Det.InsuredSSN = txtISSN.Text;
            objPat_Ins_Det.InsuredRelation = txtIRel.Text;
            objPat_Ins_Det.IsActive = active;
            objPat_Ins_Det.InsPhone = txtInsPhone.Text;

            if (chkIsPrimary.Checked == true)
            {
                objPat_Ins_Det.IsPrimaryIns = 'Y';
                lblPrimIns1.Text = txtInsName.Text;
            }
            else
                objPat_Ins_Det.IsPrimaryIns = 'N';
            string userID = (string)Session["User"];
            if (InsuranceID == 0)
            {
                string str = "alert('Invalid Insurance..!');";
                ScriptManager.RegisterStartupScript(btnSave, typeof(Page), "alert", str, true);
            }
            else
            {
                objPat_Info.Set_Pat_Insurance(userID, objPat_Ins_Det);
                FillgridPatInsurance();
                
            }
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        objNLog.Error("Function Terminated...");
    }
    protected void gridPatInsurance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lblPrimIns1.Visible == true)
                {
                    if (lblPrimIns1.Text != "" && ((Label)(e.Row.Cells[3].FindControl("lblpatIns"))).Text.Trim() == lblPrimIns1.Text.Trim())
                    {
                        e.Row.Cells[1].FindControl("lnkDeleteIns").Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatInsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridPatInsurance.PageIndex = e.NewPageIndex;
            FillgridPatInsurance();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatInsurance_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            objPat_Ins_Det.InsuranceID = Int32.Parse(((Label)gridPatInsurance.Rows[e.RowIndex].FindControl("lblpatInsID")).Text);
            objPat_Ins_Det.PI_PolicyID = ((TextBox)gridPatInsurance.Rows[e.RowIndex].FindControl("txtpatInsNo")).Text;
            objPat_Ins_Det.PI_GroupNo = ((TextBox)gridPatInsurance.Rows[e.RowIndex].FindControl("txtpatGRPNo")).Text;
            objPat_Ins_Det.PI_BINNo = ((TextBox)gridPatInsurance.Rows[e.RowIndex].FindControl("txtpatBINNo")).Text;
            objPat_Ins_Det.InsuredName = ((TextBox)gridPatInsurance.Rows[e.RowIndex].FindControl("txtINSDName")).Text;
            objPat_Ins_Det.InsPhone = ((TextBox)gridPatInsurance.Rows[e.RowIndex].FindControl("txtpatPhNo")).Text;
            objPat_Ins_Det.InsuredRelation = ((TextBox)gridPatInsurance.Rows[e.RowIndex].FindControl("txtrelINSD")).Text;
            string userID = (string)Session["User"];
            objPat_Info.Update_Pat_Insurance(userID, objPat_Ins_Det,int.Parse((string)hidPatID.Value));
            gridPatInsurance.EditIndex = -1;
            FillgridPatInsurance();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPatInsurance_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridPatInsurance.EditIndex = e.NewEditIndex;
        FillgridPatInsurance();
    }
    protected void gridPatInsurance_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridPatInsurance.EditIndex = -1;
        FillgridPatInsurance();
    }
    protected void gridPatInsurance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            Label lblPatInsID = (Label)(gridPatInsurance.Rows[e.RowIndex].FindControl("lblpatInsID"));
            objPat_Info.Delete_Patient_Insurance(userID, lblPatInsID.Text, int.Parse((string)hidPatID.Value));
            FillgridPatInsurance();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    #endregion
    
    
    #region Patient Notes & Comments
    
    protected void FillgridNotes()
    {
        objNLog.Info("Function Started...");
        try
        {
            if (hidPatID.Value.ToString() != "")
            {
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
                gridNotes.DataSource = objPat_Info.get_PatientNote(objPat_Pat_Det);
                gridNotes.DataBind();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void gridNotes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((Label)e.Row.Cells[2].FindControl("lblNoteID")).Text == "0")
                {
                    e.Row.Cells[0].FindControl("lnkEditNotes").Visible = false;
                    e.Row.Cells[1].FindControl("lnkDeleteNotes").Visible = false;
                }
                if ((string)Session["Role"] == "A" || (string)Session["Role"] == "M")
                {
                    e.Row.Cells[0].FindControl("lnkEditNotes").Visible = true;
                    e.Row.Cells[1].FindControl("lnkDeleteNotes").Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridNotes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gridNotes.EditIndex = e.NewEditIndex;
            FillgridNotes();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridNotes_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            TextBox txtNote = (TextBox)gridNotes.Rows[e.RowIndex].FindControl("txtNoteDesc");
            Label lblID = (Label)gridNotes.Rows[e.RowIndex].FindControl("lblNoteID");

            objPat_Info.update_patNote(userID, txtNote.Text, lblID.Text, int.Parse((string)hidPatID.Value));
            gridNotes.EditIndex = -1;
            FillgridNotes();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridNotes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            Label lblID = (Label)gridNotes.Rows[e.RowIndex].FindControl("lblNoteID");
            objPat_Info.delete_patNote(userID, lblID.Text, int.Parse((string)hidPatID.Value));
            gridNotes.EditIndex = -1;
            FillgridNotes();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridNotes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gridNotes.EditIndex = -1;
            FillgridNotes();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnSaveNote_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Notes Click Event Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        //SqlCommand sqlCmd;
        string userID = (string)Session["User"];
        //string sqlQuery = "Insert INTO Pat_Rx_Notes(Pat_ID,Note_Description,Note_Date,Note_By) VALUES ('" + hidPatID.Value.ToString() + "','" + txtNoteDesc.Text + "',getdate(),'" + Session["User"].ToString() + "')";
        try
        {
            objPat_Info.Set_patNote(int.Parse(hidPatID.Value.ToString()), txtNoteDesc.Text, Session["User"].ToString());
        
            //sqlCmd = new SqlCommand(sqlQuery, sqlCon);
            //sqlCon.Open();
            //sqlCmd.ExecuteNonQuery();
            //objUALog.LogUserActivity(conStr, userID, "Added New Patient Notes Info. with PatID = " + hidPatID.Value.ToString(), "Pat_Rx_Notes",int.Parse((string)hidPatID.Value));
            //sqlCon.Close();
            FillgridNotes();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Save Notes Click Event Completed...");
    }
    protected void gridNotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridNotes.PageIndex = e.NewPageIndex;
            FillgridNotes();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    #endregion
    
    
    #region Patient Call Logs & Med Issues

    protected void FillgridPatCallLog()
    {
        objNLog.Info("Function Started...");
        try
        {
            if (hidPatID.Value.ToString() != "")
            {
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
                gridCallog.DataSource = objPat_Info.get_PatientCallLogInfo(objPat_Pat_Det);
                gridCallog.DataBind();
            }
            lblCallLogReceiver.Visible = false;
            lblCallLogReceiver.Text = "";

            rbtnMedicalIssueC.Checked = true;
            rbtnMedicalIssueA.Checked = false;
            rbtnMedicalIssueN.Checked = false;
            rbtnMedicalIssueD.Checked = false;
            rbtnMedicalIssueP.Checked = false;
            rbtnMedicalIssuePA.Checked = false;

            txtCallLogDoctor.Text = lblDoctor1.Text;
            txtCallLogPharmacist.Text = "";

            txtCallLogPharmacist.Visible = false;
            txtCallLogDoctor.Visible = false;
            txtCallLogOther.Text = "";
            txtCallLogOther.Visible = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Started...");
    }
    protected void gridCallog_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gridCallog.EditIndex = e.NewEditIndex;
            FillgridPatCallLog();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void gridCallog_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            TextBox txtCallDesc = (TextBox)gridCallog.Rows[e.RowIndex].FindControl("txtCallDesc");
            Label lblCID = (Label)gridCallog.Rows[e.RowIndex].FindControl("lblCallLogID");
            objPat_Info.update_patCallLog(userID, txtCallDesc.Text, lblCID.Text, int.Parse((string)hidPatID.Value));
            gridCallog.EditIndex = -1;
            FillgridPatCallLog();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridCallog_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            FillgridPatCallLog();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridCallog_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            if (e.CommandName == "Delete")
            {
                objPat_Info.delete_patCallLog(userID, e.CommandArgument.ToString(), int.Parse((string)hidPatID.Value));
                FillgridPatCallLog();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridCallog_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gridCallog.EditIndex = -1;
            FillgridPatCallLog();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnSaveCallLog_Click(object sender, EventArgs e)
    {
        SqlConnection sqlCon = new SqlConnection(conStr); 
        objNLog.Info("Save Call Log Click Event Started...");
        try
        {
            string userID = (string)Session["User"];
            string callIntiator = "", CSR = userID, issueFlag = "N", issueFor = "";
            
            if (rbtnCSR.Checked)
                callIntiator = "CSR";
            
            if(rbtnPatient.Checked)
                callIntiator = txtPatientName1.Text;

            if (rbtnAdmin.Checked)
                callIntiator = "Administrator";

            if (rbtnDoctor.Checked)
                callIntiator = "Doctor";

            if (rbtnPharm.Checked)
                callIntiator = "Pharmacy";

            if (rbtnOther.Checked)
                callIntiator = "Other";

            if (rbtnMedicalIssueD.Checked)
            {
                issueFlag = "D";
                issueFor = txtCallLogDoctor.Text;
            }

            if (rbtnMedicalIssueP.Checked)
            {
                issueFlag = "P";
                issueFor = txtCallLogPharmacist.Text;
            }

            if (rbtnMedicalIssueA.Checked)
            {
                issueFlag = "A";
                issueFor = "Administrator"; 
            }

            if (rbtnMedicalIssuePA.Checked)
            {
                issueFlag = "T";
                issueFor = txtPatientName1.Text;
            }

            if (rbtnMedicalIssueC.Checked)
            {
                issueFlag = "C";
                issueFor = "CSR";
            }
            if (rbtnMedicalIssueN.Checked)
            {
                issueFlag = "O";
                issueFor = txtCallLogOther.Text;
            }
            SqlCommand sqlCmd;
            //string sqlQuery = "";
            //if (issueFlag == "N")
            //    sqlQuery = "Insert INTO Call_Log(Pat_ID,Call_Initiator,Call_Reason,CSR_Name,Call_Desc,Call_Time) VALUES ('" + hidPatID.Value.ToString() + "','" + callIntiator + "','" + txtCallReason.Text + "','" + CSR + "','" + txtCallDesc.Text + "',getdate())";
            //else
            //    sqlQuery = "Insert INTO Call_Log(Pat_ID,Call_Initiator,Call_Reason,CSR_Name,Call_Desc,Call_Time,Issueflag,Issuefor) VALUES ('" + hidPatID.Value.ToString() + "','" + callIntiator + "','" + txtCallReason.Text + "','" + CSR + "','" + txtCallDesc.Text + "',getdate(),'" + issueFlag + "','" + issueFor + "')";

            sqlCmd = new SqlCommand("sp_SetPatientCallLog", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter spPatID = new SqlParameter("@PatID", SqlDbType.Int);
            spPatID.Value = int.Parse(hidPatID.Value.ToString());
            sqlCmd.Parameters.Add(spPatID);

            SqlParameter spCall_Initiator = new SqlParameter("@Call_Initiator", SqlDbType.VarChar,50);
            spCall_Initiator.Value = callIntiator;
            sqlCmd.Parameters.Add(spCall_Initiator);

            SqlParameter spCall_Reason = new SqlParameter("@Call_Reason", SqlDbType.VarChar, 50);
            spCall_Reason.Value = txtCallReason.Text;
            sqlCmd.Parameters.Add(spCall_Reason);

            SqlParameter spCSR_Name = new SqlParameter("@CSR_Name", SqlDbType.VarChar, 50);
            spCSR_Name.Value = CSR;
            sqlCmd.Parameters.Add(spCSR_Name);

            SqlParameter spCall_Desc = new SqlParameter("@Call_Desc", SqlDbType.VarChar, 50);
            spCall_Desc.Value = txtCallDesc.Text;
            sqlCmd.Parameters.Add(spCall_Desc);

            SqlParameter spIssueFlag = new SqlParameter("@IssueFlag", SqlDbType.Char,1);
            spIssueFlag.Value = issueFlag;
            sqlCmd.Parameters.Add(spIssueFlag);

            SqlParameter spIssueFor = new SqlParameter("@IssueFor", SqlDbType.VarChar,50);
            spIssueFor.Value = issueFor;
            sqlCmd.Parameters.Add(spIssueFor);

            

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

            objUALog.LogUserActivity(conStr, userID, "Added New Patient Call Log Info. with PatID = " + hidPatID.Value.ToString(), "Call_Log",Int32.Parse((string)hidPatID.Value));
            FillgridPatCallLog();
            ClearCallLogPopup();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        objNLog.Info("Save Call Log Click Event Completed...");
    }
    protected void gridCallog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridCallog.PageIndex = e.NewPageIndex;
            FillgridPatCallLog();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void rbtnMedicalIssue_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            RadioButton rbtn = (RadioButton)sender;
            if (rbtn.Text == "CSR")
            {
                divCallLogFor.Style.Add("Visibility", "hidden");
                lblCallLogReceiver.Visible = false;
                lblCallLogReceiver.Text = "";

                txtCallLogOther.Visible = false;
                txtCallLogDoctor.Visible = false;
                txtCallLogPharmacist.Visible = false;
            }

            if (rbtn.Text == "Patient")
            {
                divCallLogFor.Style.Add("Visibility", "hidden");
                lblCallLogReceiver.Visible = false;
                lblCallLogReceiver.Text = "";

                txtCallLogOther.Visible = false;
                txtCallLogDoctor.Visible = false;
                txtCallLogPharmacist.Visible = false;
            }

            if (rbtn.Text == "Administrator")
            {
                divCallLogFor.Style.Add("Visibility", "hidden");
                lblCallLogReceiver.Visible = false;
                lblCallLogReceiver.Text = "";

                txtCallLogOther.Visible = false;
                txtCallLogDoctor.Visible = false;
                txtCallLogPharmacist.Visible = false;
            }

            if (rbtn.Text == "Doctor")
            {
                divCallLogFor.Style.Add("Visibility", "show");
                lblCallLogReceiver.Visible = true;
                lblCallLogReceiver.Text = "Doctor Name: ";

                txtCallLogDoctor.Visible = true;
                txtCallLogDoctor.Text =lblDoctor1.Text;
                txtCallLogPharmacist.Visible = false;
                txtCallLogOther.Visible = false;
            }
            if (rbtn.Text == "Pharmacy")
            {
                divCallLogFor.Style.Add("Visibility", "show");
                lblCallLogReceiver.Visible = true;
                lblCallLogReceiver.Text = "Pharmacy Name: ";

                txtCallLogDoctor.Visible = false;
                txtCallLogPharmacist.Visible = true;
                txtCallLogPharmacist.Text = "";
                txtCallLogOther.Visible = false;
            }

            if (rbtn.Text == "Other")
            {
                divCallLogFor.Style.Add("Visibility", "show");
                lblCallLogReceiver.Visible = true;
                lblCallLogReceiver.Text = "Doctor/Nurse/Employee Name: ";

                txtCallLogOther.Visible = true;
                txtCallLogOther.Text = "";
                txtCallLogDoctor.Visible = false;
                txtCallLogPharmacist.Visible = false;
            }

           
            

           
            AddCallLog.Show();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    private void ClearCallLogPopup()
    {
        lblCallLogReceiver.Visible = false;
        lblCallLogReceiver.Text = "";

        rbtnMedicalIssueC.Checked = true;
        rbtnMedicalIssueA.Checked = false;
        rbtnMedicalIssueN.Checked = false;
        rbtnMedicalIssueD.Checked = false;
        rbtnMedicalIssueP.Checked = false;
        rbtnMedicalIssuePA.Checked = false;

        txtCallLogDoctor.Text = lblDoctor1.Text;
        txtCallLogPharmacist.Text = "";
        txtCallLogOther.Text = "";

        txtCallLogPharmacist.Visible = false;
        txtCallLogDoctor.Visible = false;        
        txtCallLogOther.Visible = false;
    }
    #endregion
    
    
    #region Patient Payments
     
    protected void FillgridPatPayments()
    {
        objNLog.Info("Function Started...");
        try
        {
            if (hidPatID.Value.ToString() != "")
            {
                objPat_Pat_Det = new PatientPersonalDetails();
                objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
                gridPayments.DataSource = objPat_Info.get_PatientPaymentInfo(objPat_Pat_Det);
                gridPayments.DataBind();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message );
        }
        objNLog.Info("Function Completed...");
    }
    protected void gridPayments_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gridPayments.EditIndex = e.NewEditIndex;
            FillgridPatPayments();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void gridPayments_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            TextBox txtPayDesc = (TextBox)gridPayments.Rows[e.RowIndex].FindControl("txtPayDesc");
            TextBox txtPayAmount = (TextBox)gridPayments.Rows[e.RowIndex].FindControl("txtPayAmount");
            TextBox txtChequeorCC = (TextBox)gridPayments.Rows[e.RowIndex].FindControl("txtChequeorCC");
            Label lblPayID = (Label)gridPayments.Rows[e.RowIndex].FindControl("lblPayID");

            objPat_Info.update_patPayments(userID, txtPayAmount.Text, txtPayDesc.Text, txtChequeorCC.Text, lblPayID.Text, int.Parse((string)hidPatID.Value));
            gridPayments.EditIndex = -1;
            FillgridPatPayments();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            FillgridPatPayments();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPayments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            if (e.CommandName == "Delete")
            {
                objPat_Info.delete_patPayments(userID, e.CommandArgument.ToString(), int.Parse((string)hidPatID.Value));
                FillgridPatPayments();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPayments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gridPayments.EditIndex = -1;
            FillgridPatPayments();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnSavePayment_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Payment Click Event Started...");
        string Payment_Mode = "";
        if (rbtnPayCash.Checked)
            Payment_Mode = "Cash";
        if (rbtnPayCheck.Checked)
            Payment_Mode = "Check";
        if (rbtnPayCreditCard.Checked)
            Payment_Mode = "Credit Card";
        string userID = (string)Session["User"];
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd=new SqlCommand();
        //string sqlQuery = "Insert INTO Patient_Payments(Pat_ID,Payment_Mode,Check_OR_CC_Number,CCPay_Confirmation,"
        //                                                + "Payment_Date,Payment_Amount,Payment_Notes) VALUES ('"
        //                                                + hidPatID.Value.ToString() 
        //                                                + "','" 
        //                                                + Payment_Mode 
        //                                                + "','" 
        //                                                + txtChequeorCC.Text 
        //                                                + "','" 
        //                                                + txtCCAuth.Text 
        //                                                + "',getdate(),'" 
        //                                                + txtPayAmount.Text 
        //                                                + "','" 
        //                                                + txtPaymentNote.Text 
        //                                                + "')";
        try
        {
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandText = "sp_Set_PatientPayments";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sql_Pat_ID = sqlCmd.Parameters.Add("@Pat_ID", SqlDbType.Int);
            sql_Pat_ID.Value = int.Parse(hidPatID.Value);

            SqlParameter sql_Payment_Mode = sqlCmd.Parameters.Add("@Payment_Mode", SqlDbType.VarChar,20);
            sql_Payment_Mode.Value = Payment_Mode;

            SqlParameter sql_Check_OR_CC_Number = sqlCmd.Parameters.Add("@Check_OR_CC_Number", SqlDbType.VarChar, 50);
            sql_Check_OR_CC_Number.Value = txtChequeorCC.Text;

            SqlParameter sql_CCPay_Confirmation = sqlCmd.Parameters.Add("@CCPay_Confirmation", SqlDbType.VarChar, 50);
            sql_CCPay_Confirmation.Value = txtCCAuth.Text;

            SqlParameter sql_Payment_Date = sqlCmd.Parameters.Add("@Payment_Date", SqlDbType.SmallDateTime);
            sql_Payment_Date.Value = System.DateTime.Now.ToString();

            SqlParameter sql_Payment_Amount = sqlCmd.Parameters.Add("@Payment_Amount", SqlDbType.Money);
            sql_Payment_Amount.Value = txtPayAmount.Text;

            SqlParameter sql_Payment_Notes = sqlCmd.Parameters.Add("@Payment_Notes", SqlDbType.VarChar,255);
            sql_Payment_Notes.Value = txtPaymentNote.Text;

            SqlParameter sql_Payment_SysFlag = sqlCmd.Parameters.Add("@Payment_SysFlag", SqlDbType.VarChar, 50);
            if(chkRx30Patient.Checked==true)
                 sql_Payment_SysFlag.Value ="P";
            else
                sql_Payment_SysFlag.Value = "R";

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objUALog.LogUserActivity(conStr, userID, "Added New Payment Info for the Patient with PatID=" + (string)hidPatID.Value, "Patient_Payments", Int32.Parse((string)hidPatID.Value));
           
            sqlCon.Close();
            FillgridPatPayments();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void gridPayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridPayments.PageIndex = e.NewPageIndex;
            FillgridPatPayments();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void gridPayments_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       try
       {
           if (e.Row.RowType == DataControlRowType.DataRow)
           {

               if ((DataBinder.Eval(e.Row.DataItem, "Payment_SysFlag")).ToString() == "P")
               {
                    if ((string)Session["Role"] == "A" || (string)Session["Role"] == "T")
                    {
                        e.Row.Cells[0].FindControl("lnkEditPayments").Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[0].FindControl("lnkEditPayments").Visible = false;
                    }
                   e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8D38C9");
               }
               
                
           }
       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
   }
    #endregion


    #region Patient ShippLog
    
   protected void FillShipLog()
   {
       objNLog.Info("Function Started...");
       try
       {
           if (hidPatID.Value.ToString() != "")
           {
               //objPat_Pat_Det = new PatientPersonalDetails();
              // objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
               gridShipLog.DataSource = objPat_Info.get_ShipLog(hidPatID.Value.ToString());
               gridShipLog.DataBind();
           }
       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
       objNLog.Info("Function Completed...");
   }
 
   protected void gridShipLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
   {
       try
       {
           gridShipLog.PageIndex = e.NewPageIndex;
           FillShipLog();
       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
   }
   
   
 
  
   #endregion


    #region Patient Documents

   protected int FillgridDocument()
   {
       int flag=0;
       objNLog.Info("Function Started...");
       try
       {
           if (hidPatID.Value.ToString() != "")
           {

               objPat_Pat_Det = new PatientPersonalDetails();
               objPat_Pat_Det.Pat_ID = int.Parse(hidPatID.Value.ToString());
               gridDocuments.DataSource = objPat_Info.get_PatientDocuments(objPat_Pat_Det);
               gridDocuments.DataBind();
               flag=1;
           }
       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
       objNLog.Info("Function Completed...");
       return flag;
   }

   protected void gridDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
   {
       try
       {
           gridDocuments.PageIndex = e.NewPageIndex;
           FillgridDocument();
       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
   }
   protected void gridDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
   {
       try
       {
           FillgridDocument();
       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
   }
   protected void gridDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
   {
       try
       {
           if (e.CommandName == "Delete")
           {
               objPat_Info.delete_patDocument(e.CommandArgument.ToString());
               FillgridDocument();
           }
       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
   }

   protected void btnSaveDoc_Click(object sender, ImageClickEventArgs e)
   {
       objNLog.Info("Save Document Click Event Started...");
       try
       {
           //SqlConnection sqlCon = new SqlConnection(conStr);
           //SqlCommand sqlCmd;

           if (fuDocument.HasFile)
           {
               string filename = System.IO.Path.GetFileName(fuDocument.FileName);
               //fuDocument.SaveAs("C:\temp" + filename);
               //string sqlQuery = "Insert INTO Pat_Documents(Pat_Id,Doc_Name,Doc_Desc,FileName) VALUES ('" + hidPatID.Value.ToString() + "','" + txtDocuName.Text + "','" + txtDocDesc.Text + "','" + filename + "') Select @@identity";

               //sqlCmd = new SqlCommand(sqlQuery, sqlCon);
               //sqlCon.Open();
               string fileid = "";
               //fileid = sqlCmd.ExecuteScalar().ToString();
               //sqlCmd.ExecuteNonQuery();
               fileid= objPat_Info.Set_Patient_Documents(int.Parse(hidPatID.Value.ToString()), txtDocuName.Text, txtDocDesc.Text, filename);
               string fillpath = Server.MapPath("Documents/" + fileid + "_" + filename);
               fuDocument.SaveAs(fillpath);
               //sqlCon.Close();
               FillgridDocument();
           }

       }
       catch (Exception ex)
       {
           objNLog.Error("Error : " + ex.Message);
       }
       objNLog.Info("Save Document Click Event Completed...");
   }

   #endregion

    
    #region Patient Billing
       protected void imgBtnSaveBilling_Click(object sender, ImageClickEventArgs e)
       {
           objNLog.Info("Function Started...");
           try
           {
               string userID = (string)Session["User"];

               PatientBilling objPatBill = new PatientBilling();
               PatientInfoDAL objPatInfo = new PatientInfoDAL();

               objPatBill.PatID = int.Parse(hidPatID.Value);

               objPatBill.TransactionDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                 
               if(rbtnPAPFilling.Checked==true)
                    objPatBill.TransactionType= 'P';
               else
                   objPatBill.TransactionType='S';

               if(chkTimeBilling.Checked==true)
                    objPatBill.TransactionMode='Y';
               else
                   objPatBill.TransactionMode = 'N';

               objPatBill.TransactionAmount=txtTransAmt.Text;

               objPatBill.TransactionDetails=txtTransDetails.Text;

               objPatBill.User = userID;

               if (chkRx30Patient.Checked)
                   objPatBill.TransSysFlag = "P";
               else
                   objPatBill.TransSysFlag = "R";

               objPatInfo.SetPatientBillingInfo(objPatBill);
               
               FillPatientBillingGrid();

               ClearBillingPanel();
          
           }
           catch (Exception ex)
           {
               objNLog.Error("Error : " + ex.Message);
           }
           objNLog.Info("Function completed...");
       }
       protected void btnSaveAdjBill_Click(object sender, ImageClickEventArgs e)
       {
           objNLog.Info("Function Started...");
           try
           {
               string userID = (string)Session["User"];

               PatientBilling objPatBill = new PatientBilling();
               PatientInfoDAL objPatInfo = new PatientInfoDAL();

               objPatBill.PatID = int.Parse(hidPatID.Value);

               objPatBill.TransactionDate = lblTransAdjDate.Text;

               if (rbtnCredit.Checked == true)
                   objPatBill.TransactionType = 'C';
               else
                   objPatBill.TransactionType = 'D';

               objPatBill.TransactionFlag='A';

               objPatBill.TransactionAmount = txtAdjBillAmt.Text;

               objPatBill.TransactionDetails = txtAdjBillDetails.Text;

               objPatBill.User = userID;

               if (chkRx30Patient.Checked)
                   objPatBill.TransSysFlag = "P";
               else
                   objPatBill.TransSysFlag = "R";

               objPatInfo.SetPatientAdjustBillingInfo(objPatBill);

               FillPatientBillingGrid();

               ClearBillingAdjustmentPanel();

           }
           catch (Exception ex)
           {
               objNLog.Error("Error : " + ex.Message);
           }
           objNLog.Info("Function completed...");
       }
       public void FillPatientBillingGrid()
       {
           objNLog.Info("Function Started...");
           try
           {
               PatientInfoDAL objPatInfo = new PatientInfoDAL();
               grdBilling.DataSource = objPatInfo.GetPatientBillingInfo(int.Parse(hidPatID.Value.ToString()));
               grdBilling.DataBind();
               
           }
           catch (Exception ex)
           {
               objNLog.Error("Error : " + ex.Message);
           }
           objNLog.Info("Function completed...");
       }
       public void ClearBillingPanel()
       { 
         txtTransAmt.Text= "";
         txtTransDetails.Text ="";
         chkTimeBilling.Checked=false;
         rbtnPAPFilling.Checked = true;
         rbtnShippingCharges.Checked = false;
       }
       public void ClearBillingAdjustmentPanel()
       {
           txtAdjBillAmt.Text = "";
           txtAdjBillDetails.Text = "";
           rbtnCredit.Checked = true;
           rbtnDebit.Checked = false;
       }
       protected void grdBilling_RowDataBound(object sender, GridViewRowEventArgs e)
       {
           try
           {
               if (e.Row.RowType == DataControlRowType.DataRow)
               {
                   CheckBox chkFirst = (CheckBox)e.Row.Cells[2].FindControl("chkFirstTrans");
                   if ((DataBinder.Eval(e.Row.DataItem, "Trans_Mode")).ToString() == "Y")
                   {
                       chkFirst.Checked = true;
                   }
                   else
                   {
                       chkFirst.Checked = false;
                   }

                   if ((DataBinder.Eval(e.Row.DataItem, "Trans_flag")).ToString() == "A") 
                   {
                       e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#C11B17");
                       e.Row.ToolTip = "Adjustment";
                       chkFirst.Visible = false;
                   }

                   if ((DataBinder.Eval(e.Row.DataItem, "Trans_SysFlag")).ToString() == "P")
                   {
                       if ((DataBinder.Eval(e.Row.DataItem, "Trans_flag")).ToString() == "A")
                           e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#C11B17");
                       else
                            e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#8D38C9");
                   }

                    
               }
           }
           catch (Exception ex)
           {
               objNLog.Error("Error : " + ex.Message);
           }
       }
       protected void grdBilling_PageIndexChanging(object sender, GridViewPageEventArgs e)
       {
           grdBilling.PageIndex = e.NewPageIndex;
           FillPatientBillingGrid();
       }
       protected void gridBilling_RowDeleting(object sender, GridViewDeleteEventArgs e)
       {
           try
           {
               FillPatientBillingGrid();
           }
           catch (Exception ex)
           {
               objNLog.Error("Error : " + ex.Message);
           }
       }
       protected void grdBilling_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           try
           {
               if (e.CommandName == "Delete")
               {
                   objPat_Info.Delete_PatBilling(e.CommandArgument.ToString());
                   FillPatientBillingGrid();
               }
           }
           catch (Exception ex)
           {
               objNLog.Error("Error : " + ex.Message);
           }
       }
       protected void gridPatInsurance_RowCommand(object sender, GridViewCommandEventArgs e)
       {
           try
           {
               if (e.CommandName == "EditInsurance")
               {
                   GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                   int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);

                   Label lblpatInsID = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblpatInsID");

                   Label lblpatIns = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblpatIns");

                   Label lblpatPolicyID = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblpatPolicyID");

                   Label lblpatBINNo = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblpatBINNo");

                   Label lblpatGRPNo = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblpatGRPNo");

                   Label lblpatPhNo = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblpatPhNo");

                   Label lblrelINSDRel = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblINSDRel");

                   Label lblINSDName = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblINSDName");

                   Label lblINSDDOB = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblINSDDOB");

                   Label lblINSDSSN = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblINSDSSN");

                   Label lblIsActive = (Label)gridPatInsurance.Rows[intRowIndex].FindControl("lblIsActive");


                   hidPatInsID.Value = lblpatInsID.Text;
                   txtEditInsName.Text = lblpatIns.Text;
                   txtEditInsPhone.Text = lblpatPhNo.Text;
                   txtEditInsPolicyID.Text =lblpatPolicyID.Text;
                   txtEditInsGroupNo.Text =lblpatGRPNo.Text;
                   txtEditInsBinNo.Text =lblpatBINNo.Text;
                   txtEditInsdName.Text = lblINSDName.Text;
                   txtEditInsDOB.Text = lblINSDDOB.Text;
                   txtEditInsSSN.Text = lblINSDSSN.Text;
                   txtEditInsRelation.Text = lblrelINSDRel.Text;

                   if (objPat_Info.IsPrimaryInsurance(Int32.Parse(lblpatInsID.Text)) == 1)
                   {
                       chkEditIsPrimary.Checked = true;
                   }
                   else
                   {
                       chkEditIsPrimary.Checked = false;
                   }

                   if (lblIsActive.Text == "Y")
                       rbtnEditInsActive.Checked = true;
                   else
                       rbtnEditInsInActive.Checked = true;
     
                   MPEEditInsurance.Show();
               }

           }
           catch (Exception ex)
           {
               objNLog.Error("Error : " + ex.Message);
           }
       }
       protected void btnEditInsurance_Click(object sender, ImageClickEventArgs e)
       {
           objNLog.Info("Save Insurance Click Event Started");
           int InsuranceID = objPat_Info.Get_InsuranceID(txtEditInsName.Text.Trim());//Find Ins ID
           char active = 'N';

           if (rbtnEditInsActive.Checked)
               active = 'Y';

           try
           {
               objPat_Ins_Det.PI_ID = Int32.Parse(hidPatInsID.Value.ToString());
               objPat_Ins_Det.Pat_ID = Int32.Parse(hidPatID.Value.ToString());
               objPat_Ins_Det.InsuranceID = InsuranceID;
               objPat_Ins_Det.PI_PolicyID = txtEditInsPolicyID.Text;
               objPat_Ins_Det.PI_GroupNo = txtEditInsGroupNo.Text;
               objPat_Ins_Det.PI_BINNo = txtEditInsBinNo.Text;
               objPat_Ins_Det.InsuredName = txtEditInsdName.Text;
               objPat_Ins_Det.InsuredDOB = txtEditInsDOB.Text;
               objPat_Ins_Det.InsuredSSN =  txtEditInsSSN.Text;
               objPat_Ins_Det.InsuredRelation =txtEditInsRelation.Text;
               objPat_Ins_Det.IsActive = active;
               objPat_Ins_Det.InsPhone =  txtEditInsPhone.Text;

               if (chkEditIsPrimary.Checked == true)
               {
                   objPat_Ins_Det.IsPrimaryIns = 'Y';
                   lblPrimIns1.Text = txtEditInsName.Text.Trim();
               }
               else
                   objPat_Ins_Det.IsPrimaryIns = 'N';

               string userID = (string)Session["User"];

               if (InsuranceID == 0)
               {
                   string str = "alert('Invalid Insurance..!');";
                   ScriptManager.RegisterStartupScript(btnSave, typeof(Page), "alert", str, true);
               }
               else
               {
                   objPat_Info.Update_Pat_Insurance(userID, objPat_Ins_Det,patID);
                   FillgridPatInsurance();

               }

           }
           catch (Exception ex)
           {
               objNLog.Error("Exception : " + ex.Message);
           }
           objNLog.Error("Function Terminated...");
       }
       protected void chkRx30Patient_CheckedChanged(Object sender, EventArgs e)
   {
       if (chkRx30Patient.Checked == true)
       {
           Session["Prodigy"] = "P";
       }
       else
       {
           Session["Prodigy"] = "R";
       }


   }
   #endregion
}