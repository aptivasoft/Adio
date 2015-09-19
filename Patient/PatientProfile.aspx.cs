using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Text;
using NLog;

public partial class Patient_PatinetProfile : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    PatientPersonalDetails Pat_Det = new PatientPersonalDetails();
    PatientInsuranceDetails Pat_InsDet = new PatientInsuranceDetails();
    PatientInfoDAL Pat_Info = new PatientInfoDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            chkHippa.Attributes.Add("onclick", "document.getElementById('" + txtHippaDate.ClientID + "').disabled=!this.checked;");

            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            if ((string)Session["Role"] == "A")
            {
                ddlPatStatus.Enabled = true;
            }
            else
            {
                ddlPatStatus.Enabled = false;
            }
            RenderJSArrayWithCliendIds(txtMRN, txtSSN, txtDOB, txtDoctor, txtClinicFacility, txtPatientFName, txtPatientLName, txtAllergyTo, txtAllergyDesc, txtInsName,txtInsPhone, txtPID, txtGNO, txtBNO, txtIName, txtIDOB, txtISSN, txtIRel, rbtnIActive);
            if (!Page.IsPostBack)
            {
                setcontextkey();
                ddlPrimInsurance.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlPrimInsurance.SelectedIndex = 0;
                if (Request.QueryString["patID"] != null)
                {
                    PatientInfoDAL Pat_Info = new PatientInfoDAL();

                    ddlPrimInsurance.DataSource = Pat_Info.get_PatInsNames(Request.QueryString["patID"].ToString());
                    ddlPrimInsurance.DataTextField = "Ins_Name";
                    ddlPrimInsurance.DataValueField = "Patient_Ins_ID";
                    ddlPrimInsurance.DataBind();
                    Filldata();
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                }
                if (Request.QueryString["patname"] != null)
                {
                    string[] patname = Request.QueryString["patname"].Split(',');
                    if (patname.Length == 2)
                    {
                        txtPatientLName.Text = patname[0];
                        txtPatientFName.Text = patname[1];
                    }
                    else
                    {
                        txtPatientLName.Text = patname[0];
                    }
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = true;
                }

                txtPatientFName.Focus();
                if (chkHippa.Checked == false)
                    txtHippaDate.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error :" + ex.Message);
        }
    }
    //get_ClinicFacilityNames

    protected void setcontextkey()
    {
        objNLog.Info("Function Started..");
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd;
        try
        {
            if ((string)Session["Role"] == "C" || (string)Session["Role"] == "D")
            {
                sqlCmd = new SqlCommand("sp_getClinics", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
                sp_UserID.Value = (string)Session["User"];

                SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
                sp_UserRole.Value = (string)Session["Role"];



                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet dsClinicList = new DataSet();

                sqlDa.Fill(dsClinicList, "ClinicList");

                if (dsClinicList.Tables[0].Rows.Count > 0)
                {
                    AutoCompleteExtender1.ContextKey = dsClinicList.Tables[0].Rows[0]["Clinic_ID"].ToString();
                    AutocompletePatients.ContextKey = dsClinicList.Tables[0].Rows[0]["Clinic_ID"].ToString();
                }

            }
            else if (Request.QueryString["patID"] != null)
            {
                    sqlCmd = new SqlCommand("select facility_Info.Clinic_ID from facility_Info,Patient_Info where facility_Info.Facility_ID=Patient_Info.Facility_ID and  Patient_Info.pat_ID=" + (string)Request.QueryString["patID"], sqlCon);
                    sqlCon.Open();
                    string clinicID = sqlCmd.ExecuteScalar().ToString();
                    sqlCon.Close();
                    AutoCompleteExtender1.ContextKey = clinicID;
                    AutocompletePatients.ContextKey = clinicID;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Doc_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Doc_List.Clear();
        DataTable Doc_Names = Pat_Info.get_DoctorNames(prefixText,  contextKey);
        if (Doc_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Doc_Names.Rows)
            {
                Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString().Trim() + "," + dr[0].ToString().Trim(), dr[2].ToString()));
            }
        }
        else
        {
            Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Doctor Found under selected Location.", "0"));
        }
        return Doc_List.ToArray();
    }

    //GetInsuranceNames
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetInsuranceNames(string prefixText, int count)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Ins_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Ins_List.Clear();
        DataTable Ins_Names = Pat_Info.get_InsNames(prefixText);
        if (Ins_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Ins_Names.Rows)
            {
                Ins_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString().Trim(), dr[1].ToString()));
            }
        }
        else
        {
            Ins_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Insurance Found..!", "0"));
        }
        return Ins_List.ToArray();
    }

      [System.Web.Services.WebMethod]
      [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetClinicFaclityNames(string prefixText, int count, string contextKey)
      {
          PatientInfoDAL Pat_Info = new PatientInfoDAL();
          List<string> Pat_List = new List<string>();
          Pat_Info = new PatientInfoDAL();
          Pat_List.Clear();
          DataTable Pat_Names;
          
         // if(contextKey==null)
             Pat_Names = Pat_Info.get_ClinicFacilityNames(prefixText);
          //else
           //   Pat_Names = Pat_Info.get_ClinicFacilityNames(prefixText, contextKey);
          
          if (Pat_Names.Rows.Count > 0)
          {
              foreach (DataRow dr in Pat_Names.Rows)
              {
                  Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[2].ToString()));
              }
          }
          else
          {
              Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Facility Found Under This Clinic..!","0"));
          }
          return Pat_List.ToArray();
      }
      
    public void RenderJSArrayWithCliendIds(params Control[] wc)
    {
        objNLog.Info("Event Started..");
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
                // Now register the method GetClientId, used to get the client id of tthe control
                cs.RegisterStartupScript(this.Page.GetType(), "key", "\nfunction GetClientId(serverId)\n{\nfor(i=0; i<MyServerID.length; i++)\n{\nif ( MyServerID[i] == serverId )\n{\nreturn MyClientID[i];\nbreak;\n}\n}\n}", true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void ddlPrimInsurance_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            ddlPrimInsurance.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPrimInsurance.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void chkboxMAddress_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxMAddress.Checked)
        {
            txtSAddr1.Text = txtAddress1.Text.Trim();
            txtSAddr2.Text = txtAddress2.Text.Trim();
            txtSCity1.Text = txtCity.Text.Trim();
            txtSState.Text = txtState.Text.Trim();
            txtSZIP.Text = txtZip.Text.Trim();
        }
    }

    protected void Filldata()
    {
        objNLog.Info("Function Started..");
        try
        {
            
            PatientInfoDAL Pat_Info = new PatientInfoDAL();
            DataTable dtPatientDetails = new DataTable();
            dtPatientDetails = Pat_Info.get_Patient_Details(Request.QueryString["patID"].ToString());
            if (dtPatientDetails.Rows.Count > 0)
            {
                txtDoctor.Enabled = true;
                if (dtPatientDetails.Rows[0][1].ToString() != null)
                {
                    txtPhone.Text = dtPatientDetails.Rows[0][1].ToString();

                }
                else
                {
                    txtPhone.Text = null;
                }
                if (dtPatientDetails.Rows[0][2].ToString() == "M")
                {
                    rbtnMale.Checked = true;
                }
                else
                {
                    rbtnFeMale.Checked = true;
                }

                txtPatientFName.Text = dtPatientDetails.Rows[0]["Pat_FName"].ToString().Trim();
                txtPatientLName.Text = dtPatientDetails.Rows[0]["Pat_LName"].ToString().Trim();
                txtPatientMName.Text = dtPatientDetails.Rows[0]["Pat_MName"].ToString().Trim();

                txtDOB.Text = ((DateTime)dtPatientDetails.Rows[0]["Pat_DOB"]).ToShortDateString();
                if (ddlPrimInsurance.Enabled == true && dtPatientDetails.Rows[0]["Pat_Primary_Ins_ID"].ToString() != "")
                    ddlPrimInsurance.SelectedValue = dtPatientDetails.Rows[0]["Pat_Primary_Ins_ID"].ToString();
                txtSSN.Text = dtPatientDetails.Rows[0]["Pat_SSN"].ToString();
                //txtBalance.Text = dtPatientDetails.Rows[0][6].ToString();

                txtecontactFNAME.Text = dtPatientDetails.Rows[0]["econtact_fname"].ToString();
                txtecontactLNAME.Text = dtPatientDetails.Rows[0]["econtact_lname"].ToString();
                txtecontactPHONE.Text = dtPatientDetails.Rows[0]["econtact_phone"].ToString();
                ddlecontactREL.SelectedValue = dtPatientDetails.Rows[0]["econtact_relation"].ToString();


                txtAddress1.Text = dtPatientDetails.Rows[0]["Pat_Address1"].ToString();
                txtAddress2.Text = dtPatientDetails.Rows[0]["Pat_Address2"].ToString();
                txtCity.Text = dtPatientDetails.Rows[0]["Pat_City"].ToString();

                txtState.Text = dtPatientDetails.Rows[0]["Pat_State"].ToString();
                txtZip.Text = dtPatientDetails.Rows[0]["Pat_Zip"].ToString();
                txtWorkPhone.Text = dtPatientDetails.Rows[0]["Pat_WorkPhone"].ToString();
                txtCellPhone.Text = dtPatientDetails.Rows[0]["Pat_CellPhone"].ToString();

                hidPat_ID.Value = (dtPatientDetails.Rows[0]["Pat_ID"].ToString());
                int Pat_FacId = int.Parse(dtPatientDetails.Rows[0]["Facility_ID"].ToString());

                txtDoctor.Text = dtPatientDetails.Rows[0]["Pat_PDoc"].ToString().Trim();

                txtClinicFacility.Text = Pat_Info.Get_Facility(Pat_FacId)[0].ToString();
                txtSAddr1.Text = dtPatientDetails.Rows[0]["Pat_Ship_Address1"].ToString();
                txtSAddr2.Text = dtPatientDetails.Rows[0]["Pat_Ship_Address2"].ToString();

                txtSCity1.Text = dtPatientDetails.Rows[0]["Pat_Ship_City"].ToString();
                txtSState.Text = dtPatientDetails.Rows[0]["Pat_Ship_State"].ToString();

                txtSZIP.Text = dtPatientDetails.Rows[0]["Pat_Ship_Zip"].ToString();
                string[] Diagn = new string[3];
                char[] split = { ',' };
                string Data = dtPatientDetails.Rows[0][18].ToString();
                Diagn = Data.Split(',');
                txtDiagn1.Text = Diagn[0];
                if (Diagn.Length > 1)
                    txtDiagn2.Text = Diagn[1];
                if (Diagn.Length > 2)
                    txtDiagn3.Text = Diagn[2];
                if (Diagn.Length > 3)
                    txtDiagn4.Text = Diagn[3];
                if (Diagn.Length > 4)
                    txtDiagn5.Text = Diagn[4];

                if (dtPatientDetails.Rows[0]["Pat_Rx_Auto_Refill_Y_N"].ToString() == "Y")
                    chkAutoreFillOption.Checked = true;
                else
                    chkAutoreFillOption.Checked = false;

                if (dtPatientDetails.Rows[0]["HIPPA"].ToString() == "Y")
                {
                    chkHippa.Checked = true;
                    txtHippaDate.Text = DateTime.Parse( dtPatientDetails.Rows[0]["HIPPADate"].ToString()).ToString("MM/dd/yyyy");
                }
                else
                    chkAutoreFillOption.Checked = false;

                txtMRN.Text = dtPatientDetails.Rows[0]["Pat_MRN"].ToString();

                if (dtPatientDetails.Rows[0]["Pat_IsActive"] != System.DBNull.Value)
                {
                    if (ddlPatStatus.Enabled == true)
                    {
                        if (dtPatientDetails.Rows[0]["Pat_IsActive"].ToString() == "Y")
                            ddlPatStatus.SelectedIndex = 0;
                        else
                            ddlPatStatus.SelectedIndex = 1;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");

        //if (lblStatus.Visible == true)
        //    lblStatus.Visible = false;

        PatientPersonalDetails Pat_Det = new PatientPersonalDetails();
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        try
        {
            PatientName Pat_Name = new PatientName();
            int facID=Pat_Info.Get_FacID(txtClinicFacility.Text);
           
            if (facID == 0)
            {
                Pat_Det.FacID = 0;
            }
            else
            {
                Pat_Det.FacID = facID;
            }

            Pat_Det.MRN = txtMRN.Text;
            Pat_Name.FirstName = txtPatientFName.Text;
            Pat_Name.LastName = txtPatientLName.Text;
            Pat_Name.MiddleName = txtPatientMName.Text;

            if (rbtnMale.Checked == true)
            {
                Pat_Det.Gender = "M";
            }
            else
            {
                Pat_Det.Gender = "F";
            }

            Pat_Det.DOB = txtDOB.Text;
            Pat_Det.SSN = txtSSN.Text;

            Pat_Det.eContactFName = txtecontactFNAME.Text.Trim();
            Pat_Det.eContactLName = txtecontactLNAME.Text.Trim();
            Pat_Det.eContactPhone = txtecontactPHONE.Text.Trim();
            if (ddlecontactREL.SelectedValue != "0")
                Pat_Det.eContactRelation = ddlecontactREL.SelectedValue;

            Pat_Det.PatientAddress1 = txtAddress1.Text;
            Pat_Det.PatientAddress2 = txtAddress2.Text;
            Pat_Det.Pat_City = txtCity.Text;
            Pat_Det.Pat_state = txtState.Text;
            Pat_Det.Pat_ZIP = txtZip.Text;

            Pat_Det.PatientShipAddress1 = txtSAddr1.Text;
            Pat_Det.PatientShipAddress2 = txtSAddr2.Text;
            Pat_Det.Pat_SCity = txtSCity1.Text;
            Pat_Det.Pat_Sstate = txtSState.Text;
            Pat_Det.Pat_SZIP = txtSZIP.Text;

            Pat_Det.Pat_Phone = txtPhone.Text;
            Pat_Det.Pat_WPhone = txtWorkPhone.Text;
            Pat_Det.Pat_CellPhone = txtCellPhone.Text;
            string diag_Code = "";
            if (txtDiagn1.Text.Trim() != "")
                diag_Code = txtDiagn1.Text.Trim();
            if (txtDiagn2.Text.Trim() != "")
            {
                if (diag_Code.Trim() != "")

                    diag_Code = diag_Code + "," + txtDiagn2.Text.Trim();
                else
                    diag_Code = diag_Code + txtDiagn2.Text.Trim();
            }
            if (txtDiagn3.Text.Trim() != "")
            {
                if (diag_Code.Trim() != "")

                    diag_Code = diag_Code + "," + txtDiagn3.Text.Trim();
                else
                    diag_Code = diag_Code + txtDiagn3.Text.Trim();
            }
            if (txtDiagn4.Text.Trim() != "")
            {
                if (diag_Code.Trim() != "")

                    diag_Code = diag_Code + "," + txtDiagn4.Text.Trim();
                else
                    diag_Code = diag_Code + txtDiagn4.Text.Trim();
            }
            if (txtDiagn5.Text.Trim() != "")
            {
                if (diag_Code.Trim() != "")

                    diag_Code = diag_Code + "," + txtDiagn5.Text.Trim();
                else
                    diag_Code = diag_Code + txtDiagn5.Text.Trim();
            }
            Pat_Det.Pat_Diagnosis = diag_Code;

            Pat_Det.Pat_Pre_Doc = txtDoctor.Text;
            Pat_Det.DocID = Int32.Parse(Pat_Info.Get_DocID(txtDoctor.Text).ToString());

            if (chkAutoreFillOption.Checked)

                Pat_Det.Pat_AutoFill = "Y";
            else
                Pat_Det.Pat_AutoFill = "N";

            if (chkHippa.Checked)
            {
                Pat_Det.HIPPANotice = "Y";
                Pat_Det.HIPPADate = txtHippaDate.Text;
            }
            else
                Pat_Det.HIPPANotice = "N";

            Pat_Det.Pat_ID = int.Parse(hidPat_ID.Value.ToString());

            if (ddlPrimInsurance.Enabled == true )
                Pat_Det.PrimaryInsID = int.Parse(ddlPrimInsurance.SelectedValue.ToString());

            if (ddlPatStatus.Enabled == true)
            Pat_Det.PatientStatus = char.Parse(ddlPatStatus.SelectedValue);
            
            string userID = (string)Session["User"];
            if (Pat_Det.FacID == 0)
            { 
                //lblStatus.Visible=true;
                //lblStatus.Text = "No Facilities Found / Invalid Facility..!";

                divPatWarning.Visible = true;
                divPatError.Visible = false;
                divPatSuccess.Visible = false;
                lblAddPatientStatus.Text = "No Facilities Found / Invalid Facility..!";
                mpeAddPatientStatus.Show();

            }
            else if (Pat_Det.DocID == 0)
            {
                //lblStatus.Visible = true;
                //lblStatus.Text = "No Doctors Found / Invalid Doctor..!";

                divPatWarning.Visible = true;
                divPatError.Visible = false;
                divPatSuccess.Visible = false;
                lblAddPatientStatus.Text = "No Doctors Found / Invalid Doctor..!";
                mpeAddPatientStatus.Show();

            }
            else
            {
                Pat_Info.update_patInfo(userID, Pat_Det, Pat_Name);
                Response.Redirect("AllPatientProfile.aspx?patID=" + Request.QueryString["patID"].ToString());
            }
            
           

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["patID"] != null)
            {
                //PatientInfoDAL Pat_Info = new PatientInfoDAL();
                //DataTable dtPatientDetails = new DataTable();
                //dtPatientDetails = Pat_Info.get_Patient_Details(Request.QueryString["patID"].ToString());
                Response.Redirect("AllPatientProfile.aspx?patID=" + Request.QueryString["patID"].ToString());
            }
            else
            {
                if (hidPat_ID.Value != "")
                    Response.Redirect("AllPatientProfile.aspx?patID=" + hidPat_ID.Value.ToString());
                else
                    Response.Redirect("AllPatientProfile.aspx");
            }
        }
       catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");
        
        try
        {
            //if (lblStatus.Visible == true)
            //    lblStatus.Visible = false;
            string patGender="";

            if(rbtnMale.Checked)
                patGender="M";
            else
                patGender="F";


            int isDupPatient = Pat_Info.Patient_Exist(txtPatientFName.Text, txtPatientLName.Text, patGender, txtDOB.Text, txtSSN.Text);

            if (isDupPatient == 0)
            {
               AddPatientRecord(isDupPatient); 
            }
            else
            {
                //string str = "alert('Patient Already Exists..!');";
                //ScriptManager.RegisterStartupScript(btnSave , typeof(Page), "alert", str, true);
                lblAddDupPatient.Text = "Patient already exists..! Do you like to continue adding a new Patient?";
                mpeAddDuplicatePatient.Show();

            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void tabCntPatient_ActiveTabChanged(object sender, EventArgs e)
    {

        if (tabCntPatient.ActiveTabIndex == 0)
        {
            
        }
        else if (tabCntPatient.ActiveTabIndex == 1)
        {
             
        }
    }
    protected void btnSaveInsurance_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Insurance Click Event Started");
        int InsuranceID = Pat_Info.Get_InsuranceID(txtInsName.Text);//Find Ins ID
        char active = 'N';

        if (rbtnIActive.Checked)
            active = 'Y';
        string userID = (string)Session["User"];
         
        try
        {
          Pat_InsDet.Pat_ID = Int32.Parse(hidPat_ID.Value);
          Pat_InsDet.InsuranceID = InsuranceID;
          Pat_InsDet.PI_PolicyID =  txtPID.Text;
          Pat_InsDet.PI_GroupNo =  txtGNO.Text;
          Pat_InsDet.PI_BINNo =  txtBNO.Text;
          Pat_InsDet.InsuredName =  txtIName.Text;
          Pat_InsDet.InsuredDOB =  txtIDOB.Text;
          Pat_InsDet.InsuredSSN =  txtISSN.Text;
          Pat_InsDet.InsuredRelation =  txtIRel.Text;
          Pat_InsDet.IsActive =   active ;
          Pat_InsDet.InsPhone = txtInsPhone.Text;
          
            if(chkIsPrimary.Checked ==true)
                Pat_InsDet.IsPrimaryIns = 'Y';
            else
                Pat_InsDet.IsPrimaryIns = 'N';
          
            if (InsuranceID > 0)
            {
                Pat_Info.Set_Pat_Insurance(userID, Pat_InsDet);
                FillgridPatInsurance();
            }
            else
            {
                string str = "alert('Invalid Insurance..!');";
                ScriptManager.RegisterStartupScript(btnSave, typeof(Page), "alert", str, true);
            }
            

        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    protected void btnSaveAllergy_Click(object sender, EventArgs e)
    {
        objNLog.Info("Save Allergy Click Event Started");
        try
        {
            string userID = (string)Session["User"];
            PatientAllergies pAllergyDetails = new PatientAllergies();
            pAllergyDetails.PatID = Int32.Parse(hidPat_ID.Value);
            pAllergyDetails.AllergicTo = txtAllergyTo.Text;
            pAllergyDetails.AllergyDescription = txtAllergyDesc.Text;
            Pat_Info.Set_Pat_Allergies(userID,pAllergyDetails);
            FillgridPatAllergies();

        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        objNLog.Info("Event Completed..");

    }
   
    #region PatientAllergy Grid Events
    //Allergy
    protected void gridPatAllergies_RowEditing(object sender, GridViewEditEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            gridPatAllergies.EditIndex = e.NewEditIndex;
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);

        }
        objNLog.Info("Event Completed..");

    }
    protected void gridPatAllergies_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            string userID = (string)Session["User"];
            TextBox txtATO = (TextBox)gridPatAllergies.Rows[e.RowIndex].FindControl("txtAlleryTO");
            TextBox txtADesc = (TextBox)gridPatAllergies.Rows[e.RowIndex].FindControl("txtAllergyDesc");

            Label lblPA_ID = new Label();
            lblPA_ID = (Label)(gridPatAllergies.Rows[e.RowIndex].FindControl("lblPA_ID"));

            Pat_Info.update_patAllergy(userID,txtATO.Text, txtADesc.Text, lblPA_ID.Text,int.Parse((string)hidPat_ID.Value));
            gridPatAllergies.EditIndex = -1;
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    protected void gridPatAllergies_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            gridPatAllergies.EditIndex = -1;
            FillgridPatAllergies();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void gridPatAllergies_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {    }

    protected void gridPatAllergies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            string userID = (string)Session["User"];
            if (e.CommandName == "Delete")
            {
                Pat_Info.delete_patAllergy(userID,e.CommandArgument.ToString(),int.Parse((string)hidPat_ID.Value));
                FillgridPatAllergies();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    #endregion

    protected void FillgridPatAllergies()
    {
        objNLog.Info("Function Started..");
        try
        {
            if (hidPat_ID.Value != "")
            {
                Pat_Det = new PatientPersonalDetails();
                Pat_Det.Pat_ID = int.Parse(hidPat_ID.Value);

                gridPatAllergies.DataSource = Pat_Info.get_PatientAllergies(Pat_Det);
                gridPatAllergies.DataBind();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void FillgridPatInsurance()
    {
        objNLog.Info("Function Started..");
        try
        {
            if (hidPat_ID.Value != "")
            {
                Pat_Det = new PatientPersonalDetails();
                Pat_Det.Pat_ID = int.Parse(hidPat_ID.Value);

                gridPatInsurance.DataSource = Pat_Info.get_PatientInsuranceInfo(Pat_Det);
                gridPatInsurance.DataBind();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }




    protected void btnAddDuplicatePatient_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");
         
        try
        {
            int isDupPatient = 1;
            AddPatientRecord(isDupPatient); 
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    private void AddPatientRecord(int isDupPatient)
    {
        
        if (rbtnMale.Checked == true)
        {
            Pat_Det.Gender = "M";
        }
        else
        {
            Pat_Det.Gender = "F";
        }

        PatientName Pat_Name = new PatientName();
        int patFacID;

        patFacID = Pat_Info.Get_FacID(txtClinicFacility.Text);

        if (patFacID == 0)
        {
            Pat_Det.FacID = 0;
        }
        else
        {
            Pat_Det.FacID = patFacID;
        }

        Pat_Det.MRN = txtMRN.Text;

        Pat_Name.FirstName = txtPatientFName.Text;

        Pat_Name.MiddleName = txtPatientMName.Text;

        Pat_Name.LastName = txtPatientLName.Text;


        if (rbtnMale.Checked == true)
        {
            Pat_Det.Gender = "M";
        }
        else
        {
            Pat_Det.Gender = "F";
        }

        Pat_Det.DOB = txtDOB.Text;

        Pat_Det.SSN = txtSSN.Text;

        Pat_Det.eContactFName = txtecontactFNAME.Text.Trim();

        Pat_Det.eContactLName = txtecontactLNAME.Text.Trim();

        Pat_Det.eContactPhone = txtecontactPHONE.Text.Trim();

        if (ddlecontactREL.SelectedValue != "0")
            Pat_Det.eContactRelation = ddlecontactREL.SelectedValue;

        Pat_Det.PatientAddress1 = txtAddress1.Text;
        Pat_Det.PatientAddress2 = txtAddress2.Text;
        Pat_Det.Pat_City = txtCity.Text;
        Pat_Det.Pat_state = txtState.Text;
        Pat_Det.Pat_ZIP = txtZip.Text;

        Pat_Det.PatientShipAddress1 = txtSAddr1.Text;
        Pat_Det.PatientShipAddress2 = txtSAddr2.Text;
        Pat_Det.Pat_SCity = txtSCity1.Text;
        Pat_Det.Pat_Sstate = txtSState.Text;
        Pat_Det.Pat_SZIP = txtSZIP.Text;

        Pat_Det.Pat_Phone = txtPhone.Text;
        Pat_Det.Pat_WPhone = txtWorkPhone.Text;
        Pat_Det.Pat_CellPhone = txtCellPhone.Text;

        string diag_Code = "";

        if (txtDiagn1.Text.Trim() != "")
            diag_Code = txtDiagn1.Text.Trim();

        if (txtDiagn2.Text.Trim() != "")
        {
            if (diag_Code.Trim() != "")

                diag_Code = diag_Code + "," + txtDiagn2.Text.Trim();
            else
                diag_Code = diag_Code + txtDiagn2.Text.Trim();
        }

        if (txtDiagn3.Text.Trim() != "")
        {
            if (diag_Code.Trim() != "")

                diag_Code = diag_Code + "," + txtDiagn3.Text.Trim();
            else
                diag_Code = diag_Code + txtDiagn3.Text.Trim();
        }

        if (txtDiagn4.Text.Trim() != "")
        {
            if (diag_Code.Trim() != "")

                diag_Code = diag_Code + "," + txtDiagn4.Text.Trim();
            else
                diag_Code = diag_Code + txtDiagn4.Text.Trim();
        }

        if (txtDiagn5.Text.Trim() != "")
        {
            if (diag_Code.Trim() != "")

                diag_Code = diag_Code + "," + txtDiagn5.Text.Trim();
            else
                diag_Code = diag_Code + txtDiagn5.Text.Trim();
        }

        Pat_Det.Pat_Diagnosis = diag_Code;

        Pat_Det.Pat_Pre_Doc = txtDoctor.Text;

        Pat_Det.DocID = Int32.Parse(Pat_Info.Get_DocID(txtDoctor.Text).ToString());

        if (chkAutoreFillOption.Checked)
            Pat_Det.Pat_AutoFill = "Y";
        else
            Pat_Det.Pat_AutoFill = "N";

        if (chkHippa.Checked)
        {
            Pat_Det.HIPPANotice = "Y";
            Pat_Det.HIPPADate = txtHippaDate.Text;
        }
        else
            Pat_Det.HIPPANotice = "N";


        if (ddlPrimInsurance.Enabled == true)

            Pat_Det.PrimaryInsID = int.Parse(ddlPrimInsurance.SelectedValue.ToString());
        else
            Pat_Det.PrimaryInsID = 0;

        //if(ddlPatStatus.Enabled==true)
        Pat_Det.PatientStatus = 'Y';

        string userID = (string)Session["User"];

        if (Pat_Det.FacID == 0)
        {
            //lblStatus.Visible = true;
            //lblStatus.Text = "No Facilities Found / Invalid Facility..!";

            divPatWarning.Visible = true;
            divPatError.Visible = false;
            divPatSuccess.Visible = false;
            lblAddPatientStatus.Text = "No Facilities Found / Invalid Facility..!";
            mpeAddPatientStatus.Show();

        }
        else if (Pat_Det.DocID == 0)
        {
            //lblStatus.Visible = true;
            //lblStatus.Text = "No Doctors Found / Invalid Doctor..!";

            divPatWarning.Visible = true;
            divPatError.Visible = false;
            divPatSuccess.Visible = false;
            lblAddPatientStatus.Text = "No Doctors Found / Invalid Doctor..!";
            mpeAddPatientStatus.Show();
        }
        else
        {
            //Insert New Patient Details
            string patID = Pat_Info.set_PatientInfo(userID, Pat_Name, Pat_Det);
            hidPat_ID.Value = patID;

            //lblStatus.Visible = true;
            //lblStatus.Text = "Patient Added Successfully..!";

            tabCntPatient.Visible = true;
            tabCntPatient.ActiveTabIndex = 0;

            divPatWarning.Visible = false;
            divPatError.Visible = false;
            divPatSuccess.Visible = true;
            //if(isDupPatient==0)
            //   lblAddPatientStatus.Text = "Patient Record Added Successfully..!";
            //else
            //    lblAddPatientStatus.Text = "Patient Duplicate Record Added Successfully..!";
             
                lblAddPatientStatus.Text = "Patient Record Added Successfully..!";
            
            mpeAddPatientStatus.Show();
        }
    }
}
