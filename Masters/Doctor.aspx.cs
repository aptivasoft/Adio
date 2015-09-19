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
using System.Data.SqlClient;
using System.Text;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NLog;

public partial class Doctor : System.Web.UI.Page
{
    ProviderInfo ProvInfo = new ProviderInfo();
    ProviderInfoDAL ProvInfoDAL = new ProviderInfoDAL();
    Clinic clinic = new Clinic();
    ClinicDAL cDAL = new ClinicDAL();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        RenderJSArrayWithCliendIds(txtState,txtFName,txtLName,txtClinic);
        txtState.Attributes.Add("onkeyup", "toUppercaseProv()");
      
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        string insPatStatus;

        if (txtFName.Text != "")
            ProvInfo.FirstName = txtFName.Text;
        if (txtMName.Text != "")
            ProvInfo.MiddleName = txtMName.Text;
        if (txtLName.Text != "")
            ProvInfo.LastName = txtLName.Text;
        if (txtDegree.Text != "")
            ProvInfo.Degree = txtDegree.Text;

        if (txtLocation.Text != "")
            ProvInfo.Location = txtLocation.Text;
        if (txtAdd1.Text != "")
            ProvInfo.Address1 = txtAdd1.Text;
        if (txtAdd2.Text != "")
            ProvInfo.Address2 = txtAdd2.Text;
        if (txtCity.Text != "")
            ProvInfo.City = txtCity.Text;
        if (txtState.Text != "")
            ProvInfo.State = txtState.Text;
        if (txtZip.Text != "")
            ProvInfo.Zip = txtZip.Text;
        if (txtResPhoneNo.Text != "")
            ProvInfo.HPhone = txtResPhoneNo.Text;

        if (txtCPNo.Text != "")
            ProvInfo.CPhone = txtCPNo.Text;
        if (txtEMail.Text != "")
            ProvInfo.EMail = txtEMail.Text;
        if (txtFax.Text != "")
            ProvInfo.Fax = txtFax.Text;

        if (txtSpeciality.Text != "")
            ProvInfo.Speciality = txtSpeciality.Text;
        if (txtClinic.Text != "")
        {
            clinic.ClinicName = txtClinic.Text;
            clinic.ClinicID = cDAL.getClinicID(clinic);
        }

    
        if (ddlStatus.SelectedItem.Text == "Active")
            ProvInfo.Status = "Y";
        else
            ProvInfo.Status = "N";

        if (txtDEANo.Text != "")
            ProvInfo.DeaNumber = txtDEANo.Text;
        if (txtLicNo.Text != "")
            ProvInfo.LicNo = txtLicNo.Text;
        if (txtNPI.Text != "")
            ProvInfo.NPI = txtNPI.Text;

        ProvInfo.UserType = char.Parse(rbtnUserType.SelectedValue);

        //Added Doctor Signature Upload - START.
        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
        {
            byte[] docSign = new byte[FileUpload1.PostedFile.ContentLength];
            HttpPostedFile Image = FileUpload1.PostedFile;
            Image.InputStream.Read(docSign, 0, (int)FileUpload1.PostedFile.ContentLength);
            ProvInfo.Signature = docSign;
        }
        string userID = (string)Session["User"];
        //Added Doctor Signature Upload - END.
        try
        {
            insPatStatus = ProvInfoDAL.Save_Provider(ProvInfo, clinic, userID);
            string str = "alert('" + insPatStatus + "');";
            ScriptManager.RegisterStartupScript(btnSave, typeof(Page), "alert", str, true);
            clearAllInputs();
        }
        catch (Exception ex)
        {
            insPatStatus = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        string insPatStatus;
        ProvInfo.FirstName = txtFName.Text;
        ProvInfo.LastName = txtLName.Text;
        DataTable provData = ProvInfoDAL.getProviderSearch(ProvInfo);
        ProvInfo.Prov_ID = Convert.ToInt32(provData.Rows[0][0].ToString());


        if (txtMName.Text != "")
            ProvInfo.MiddleName = txtMName.Text;

        if (txtDegree.Text != "")
            ProvInfo.Degree = txtDegree.Text;

        if (txtLocation.Text != "")
            ProvInfo.Location = txtLocation.Text;
        if (txtAdd1.Text != "")
            ProvInfo.Address1 = txtAdd1.Text;
        if (txtAdd2.Text != "")
            ProvInfo.Address2 = txtAdd2.Text;
        if (txtCity.Text != "")
            ProvInfo.City = txtCity.Text;
        if (txtState.Text != "")
            ProvInfo.State = txtState.Text;
        if (txtZip.Text != "")
            ProvInfo.Zip = txtZip.Text;
        if (txtResPhoneNo.Text != "")
            ProvInfo.HPhone = txtResPhoneNo.Text;

        if (txtCPNo.Text != "")
            ProvInfo.CPhone = txtCPNo.Text;
        if (txtEMail.Text != "")
            ProvInfo.EMail = txtEMail.Text;
        if (txtFax.Text != "")
            ProvInfo.Fax = txtFax.Text;

        if (txtSpeciality.Text != "")
            ProvInfo.Speciality = txtSpeciality.Text;
        if (txtClinic.Text != "")
        {
            clinic.ClinicName = txtClinic.Text;
            clinic.ClinicID = cDAL.getClinicID(clinic);
        }
     
        if (ddlStatus.SelectedItem.Text == "Active")
            ProvInfo.Status = "Y";
        else
            ProvInfo.Status = "N";

        if (txtDEANo.Text != "")
            ProvInfo.DeaNumber = txtDEANo.Text;
        if (txtLicNo.Text != "")
            ProvInfo.LicNo = txtLicNo.Text;
        if (txtNPI.Text != "")
            ProvInfo.NPI = txtNPI.Text;

        //Added Doctor Signature Upload - START.
        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
        {
            byte[] docSign = new byte[FileUpload1.PostedFile.ContentLength];
            HttpPostedFile Image = FileUpload1.PostedFile;
            Image.InputStream.Read(docSign, 0, (int)FileUpload1.PostedFile.ContentLength);
            ProvInfo.Signature = docSign;
        }

        string userID = (string)Session["User"];
        //Added Doctor Signature Upload - END.
        ProvInfo.UserType = char.Parse(rbtnUserType.SelectedValue);
        try
        {
            insPatStatus = ProvInfoDAL.Update_Provider(ProvInfo, clinic, userID);
            string str = "alert('" + insPatStatus + "');";
            ScriptManager.RegisterStartupScript(btnSearchProvider, typeof(Page), "alert", str, true);
            clearAllInputs();
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            txtFName.ReadOnly = false;
            txtLName.ReadOnly = false;
            lblDocSign.Visible = false;
            imgDocSign.Visible = false;
            imgDocSign.ImageUrl = "";
        }
        catch (Exception ex)
        {
            insPatStatus = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearAllInputs();
            txtSearchProvider.Text = string.Empty;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            txtFName.ReadOnly = false;
            txtLName.ReadOnly = false;
            //Added Doctor Signature  - START.
            lblDocSign.Visible = false;
            imgDocSign.Visible = false;
            imgDocSign.ImageUrl = "";
            //Added Doctor Signature  - END.
        }
        catch (Exception ex)
        {  
            objNLog.Error("Error : " + ex.Message);
        }
         
    }
    public void clearAllInputs()
    {
        try
        {
            txtFName.Text = string.Empty;
            txtMName.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtLocation.Text = string.Empty;

            ddlStatus.ClearSelection();
            txtSpeciality.Text = string.Empty;
            txtAdd1.Text = string.Empty;
            txtAdd2.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZip.Text = string.Empty;
            txtDegree.Text = string.Empty;
            txtClinic.Text = string.Empty;
            txtDEANo.Text = string.Empty;
            txtLicNo.Text = string.Empty;
            txtNPI.Text = string.Empty;
            txtFax.Text = string.Empty;
            //txtUserID.Text = string.Empty;
            txtResPhoneNo.Text = string.Empty;
            txtCPNo.Text = string.Empty;
            txtEMail.Text = string.Empty;
            rbtnUserType.SelectedIndex = 0;
          
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
         string insPatStatus;
        ProvInfo.FirstName = txtFName.Text;
        ProvInfo.LastName = txtLName.Text;
        DataTable provData = ProvInfoDAL.getProviderSearch(ProvInfo);
        ProvInfo.Prov_ID = Convert.ToInt32(provData.Rows[0][0].ToString());
        string userID = (string)Session["User"];
        try
        {
            insPatStatus = ProvInfoDAL.Delete_Provider(ProvInfo, userID);
            string str = "alert('" + insPatStatus + "');";
            ScriptManager.RegisterStartupScript(btnSearchProvider, typeof(Page), "alert", str, true);
            clearAllInputs();
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            txtFName.ReadOnly = false;
            txtLName.ReadOnly = false;
        }
        catch (Exception ex)
        {
            insPatStatus = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }
    }
    [System.Web.Services.WebMethod]
    public static string[] GetProviderNames(string prefixText, int count)
    {
        List<string> pat_List = new List<string>();
        ProviderInfoDAL ProvInfoDAL = new ProviderInfoDAL();
        DataTable dtProvider = ProvInfoDAL.get_Provider(prefixText, count);
        foreach (DataRow dr in dtProvider.Rows)
        {
            pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Doc_LName"].ToString() + ", " + dr["Doc_FName"].ToString(), dr[0].ToString()));
        }
        return pat_List.ToArray();
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetClinic(string prefixText, int count, string contextKey)
    {
        List<string> Clinic_List = new List<string>();
        ClinicDAL Clinic_Info = new ClinicDAL();
        if (contextKey == "0")
        {
            Clinic_List.Clear();
            DataTable dtclinic = Clinic_Info.get_ClinicNames(prefixText, count, contextKey);
            foreach (DataRow dr in dtclinic.Rows)
            {
                Clinic_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Clinic_Name"].ToString(), dr[0].ToString()));
            }
        }

        return Clinic_List.ToArray();
    }


    protected void btnSearchProvider_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearAllInputs();
            //string FullName = txtSearchProvider.Text;
            //FullName = FullName.Replace(" ", string.Empty);
            string[] Names = txtSearchProvider.Text.Split(',');
            if (Names.Length == 2)
            {
                ProvInfo.FirstName = Names[1].Remove(0, 1);
                ProvInfo.LastName = Names[0];
                DataTable provData = ProvInfoDAL.getProviderSearch(ProvInfo);
                if (provData.Rows.Count > 0)
                {
                    ProvInfo.Prov_ID = Convert.ToInt32(provData.Rows[0][0].ToString());
                    ProvInfo.FirstName = provData.Rows[0][2].ToString();
                    ProvInfo.MiddleName = provData.Rows[0][3].ToString();
                    ProvInfo.LastName = provData.Rows[0][4].ToString();
                    ProvInfo.Location = provData.Rows[0][5].ToString();
                    ProvInfo.Address1 = provData.Rows[0][6].ToString();
                    ProvInfo.Address2 = provData.Rows[0][7].ToString();
                    ProvInfo.City = provData.Rows[0][8].ToString();
                    ProvInfo.State = provData.Rows[0][9].ToString();
                    ProvInfo.Zip = provData.Rows[0][10].ToString();
                    ProvInfo.HPhone = provData.Rows[0][11].ToString();
                    ProvInfo.CPhone = provData.Rows[0][12].ToString();
                    ProvInfo.EMail = provData.Rows[0][13].ToString();
                    ProvInfo.Fax = provData.Rows[0][14].ToString();
                    ProvInfo.Speciality = provData.Rows[0][15].ToString();
                    ProvInfo.Degree = provData.Rows[0][16].ToString();
                    //ProvInfo.UserID = provData.Rows[0][18].ToString();
                    string DocStatus = provData.Rows[0][23].ToString();
                    ProvInfo.UserType = char.Parse(provData.Rows[0][27].ToString());
                  
                    ProvInfo.DeaNumber=provData.Rows[0][19].ToString();
                    ProvInfo.LicNo = provData.Rows[0][18].ToString();
                    ProvInfo.NPI=provData.Rows[0][20].ToString();
                   
                    if (DocStatus == "Y")
                        ProvInfo.Status = "Y";
                    else
                        ProvInfo.Status = "N";

                    if (ProvInfo.Status == "Y")
                        ddlStatus.SelectedIndex = 0;
                    else
                        ddlStatus.SelectedIndex = 1;

                    if (provData.Rows[0][1] != DBNull.Value)
                    {
                        clinic.ClinicID = Convert.ToInt32(provData.Rows[0][1].ToString());
                        clinic.ClinicName = cDAL.getClinicName(clinic);
                    }

                    ////Added Doctor Signature Upload - START.

                    if (provData.Rows[0][21] != DBNull.Value)
                    {
                        //System.Drawing.Image newImage;
                        byte[] docSign = (byte[])provData.Rows[0][21];
                        Session["image"] = docSign;
                        imgDocSign.ImageUrl = "Image.aspx";
                        lblDocSign.Visible = true;
                        imgDocSign.Visible = true;
                    }
                    //Added Doctor Signature Upload - END.

                    if (ProvInfo.FirstName != "")
                        txtFName.Text = ProvInfo.FirstName;
                    if (ProvInfo.MiddleName != "")
                        txtMName.Text = ProvInfo.MiddleName;
                    if (ProvInfo.LastName != "")
                        txtLName.Text = ProvInfo.LastName;
                    if (ProvInfo.Degree != "")
                        txtDegree.Text = ProvInfo.Degree;

                    if (ProvInfo.Location != "")
                        txtLocation.Text = ProvInfo.Location;
                    if (ProvInfo.Address1 != "")
                        txtAdd1.Text = ProvInfo.Address1;
                    if (ProvInfo.Address2 != "")
                        txtAdd2.Text = ProvInfo.Address2;
                    if (ProvInfo.City != "")
                        txtCity.Text = ProvInfo.City;
                    if (ProvInfo.State != "")
                        txtState.Text = ProvInfo.State;
                    if (ProvInfo.Zip != "")
                        txtZip.Text = ProvInfo.Zip;
                    if (ProvInfo.HPhone != "")
                        txtResPhoneNo.Text = ProvInfo.HPhone;

                    if (ProvInfo.CPhone != "")
                        txtCPNo.Text = ProvInfo.CPhone;

                    if (ProvInfo.EMail != "")
                        txtEMail.Text = ProvInfo.EMail;
                    if (ProvInfo.Fax != "")
                        txtFax.Text = ProvInfo.Fax;

                    if (ProvInfo.Status != "")
                        ddlStatus.SelectedValue = ProvInfo.Status;
                    if (ProvInfo.Speciality != "")
                        txtSpeciality.Text = ProvInfo.Speciality;
                    if (clinic.ClinicName != "")
                        txtClinic.Text = clinic.ClinicName;

                    if (ProvInfo.NPI != "")
                        txtNPI.Text = ProvInfo.NPI;

                    if (ProvInfo.LicNo  != "")
                        txtLicNo.Text = ProvInfo.LicNo;

                    if (ProvInfo.DeaNumber  != "")
                        txtDEANo.Text = ProvInfo.DeaNumber;

                    rbtnUserType.SelectedValue = ProvInfo.UserType.ToString();
                    txtFName.ReadOnly = true;
                    txtLName.ReadOnly = true;
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                    txtSearchProvider.Text = "";
                }
                else
                {

                }
            }
            else
            {
                string str = "alert('No Records Found...');";
                ScriptManager.RegisterStartupScript(btnSearchProvider, typeof(Page), "alert", str, true);
                txtSearchProvider.Text = "";
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }

    public void RenderJSArrayWithCliendIds(params Control[] wc)
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
}
