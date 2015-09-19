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
using NLog;

public partial class Facility : System.Web.UI.Page
{
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetFacilityNames(string prefixText, int count, string contextKey)
    {
        List<string> Fac_List = new List<string>();
        PatientInfoDAL pat_Info = new PatientInfoDAL();
        if (contextKey == "0")
        {
            Fac_List.Clear();
            DataTable dtFac = pat_Info.get_FacilityNames(prefixText, count, contextKey);
            if (dtFac.Rows.Count>0)
            {
                foreach (DataRow dr in dtFac.Rows)
                {
                    Fac_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Facility_Name"].ToString()+ "," + dr["Facility_Code"].ToString() , dr[0].ToString()));
                }
            }
            else
            {
                Fac_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Facility Found...","0"));
            }
        }

        return Fac_List.ToArray();
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
            if (dtclinic.Rows.Count > 0)
            {
                foreach (DataRow dr in dtclinic.Rows)
                {
                    Clinic_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Clinic_Name"].ToString(), dr[0].ToString()));
                }
            }
            else
            {
                Clinic_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Clinic found..!","0"));
            }
        }

        return Clinic_List.ToArray();
    }
   
    FacilityInfo facInfo = new FacilityInfo();
    FacilityInfoDAL facDAL = new FacilityInfoDAL();
    Clinic clinic = new Clinic();
    ClinicDAL cDAL = new ClinicDAL();
    public int facNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");
        if ((string)Session["Role"] == "C")
        {
            Server.Transfer("../Patient/AccessDenied.aspx");

        }
        txtfacState.Attributes.Add("onkeyup", "toUppercaseFac()");
        RenderJSArrayWithCliendIds(txtfacState,txtfacID,txtfacName,txtClinic);
    }

    protected void btnFacSave_Click(object sender, ImageClickEventArgs e)
    {
        string facStatus;
        try
        {
            if (chkStampAddr.Checked == true)
                facInfo.IsStamps = 'Y';
            else
                facInfo.IsStamps = 'N';

            facInfo.FacilityID = txtfacID.Text;
            facInfo.FacilityName = txtfacName.Text;
            facInfo.FacilityAddress = txtfacAdd.Text;
            facInfo.FacilityCity = txtfacCity.Text;
            facInfo.FacilityState = txtfacState.Text;
            facInfo.FacilityZip = txtfacZip.Text;
            facInfo.FacilityTPhone = txtfacPhone.Text;
            facInfo.FacilityFax = txtfacFax.Text;
            facInfo.FacilityEMail = txtfacEmail.Text;
            facInfo.FacilityTaxID = txtfacTaxID.Text;
            facInfo.FacilitySpeciality = txtfacSpeciality.Text;
            facInfo.FacilityProvID = txtfacProvID.Text;
            clinic.ClinicName = txtClinic.Text;
            clinic.ClinicID = cDAL.getClinicID(clinic);
            string userID = (string)Session["User"];
            int flag = facDAL.Ins_FacilityInfo(facInfo, clinic, userID);

            if (flag == 1)
            {
                string str = "alert('Added Facility Info Successfully...');";
                ScriptManager.RegisterStartupScript(btnFacUpdate, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed To Add New Facility Info...');";
                ScriptManager.RegisterStartupScript(btnFacUpdate, typeof(Page), "alert", str, true);
            }
            clearTextBoxes();

        }
        catch (Exception ex)
        {
            facStatus = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }
        
    }
    public void clearTextBoxes()
    {
        try
        {
            txtfacID.Text = string.Empty;
            txtfacName.Text = string.Empty;
            txtfacAdd.Text = string.Empty;
            txtfacCity.Text = string.Empty;
            txtfacState.Text = string.Empty;
            txtfacZip.Text = string.Empty;
            txtfacPhone.Text = string.Empty;
            txtfacFax.Text = string.Empty;
            txtfacEmail.Text = string.Empty;
            txtfacTaxID.Text = string.Empty;
            txtfacSpeciality.Text = string.Empty;
            txtfacProvID.Text = string.Empty;
            txtClinic.Text = string.Empty;
            chkStampAddr.Checked = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnSearchFac_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearTextBoxes();
            //facInfo.FacilityName = txtSearchFac.Text;

            string Fac_Search_String = txtSearchFac.Text;
            if (txtSearchFac.Text != string.Empty)
            {
                string[] words = Fac_Search_String.Split(',');
                facInfo.FacilityName = words[0];
                facInfo.FacilityID = words[1];

                DataTable facData = facDAL.getFacSearch(facInfo);
                if (facData.Rows.Count > 0)
                {
                    facInfo.FacilityNO = Convert.ToInt32(facData.Rows[0][0].ToString());
                    facInfo.FacilityID = facData.Rows[0][1].ToString();
                    facInfo.FacilityName = facData.Rows[0][2].ToString();
                    facInfo.FacilityAddress = facData.Rows[0][3].ToString();
                    facInfo.FacilityCity = facData.Rows[0][4].ToString();
                    facInfo.FacilityState = facData.Rows[0][5].ToString();
                    facInfo.FacilityZip = facData.Rows[0][6].ToString();
                    facInfo.FacilityTPhone = facData.Rows[0][7].ToString();
                    facInfo.FacilityFax = facData.Rows[0][8].ToString();
                    facInfo.FacilityEMail = facData.Rows[0][9].ToString();
                    facInfo.FacilityTaxID = facData.Rows[0][10].ToString();
                    facInfo.FacilitySpeciality = facData.Rows[0][11].ToString();
                    facInfo.FacilityProvID = facData.Rows[0][12].ToString();
                    
                    if (facData.Rows[0][13] != DBNull.Value)
                    {
                        clinic.ClinicID = Convert.ToInt32(facData.Rows[0][13].ToString());
                        clinic.ClinicName = cDAL.getClinicName(clinic);
                    }

                    facNo = facInfo.FacilityNO;
                    if (facInfo.FacilityID != "")
                        txtfacID.Text = facInfo.FacilityID.Trim();
                    if (facInfo.FacilityName != "")
                        txtfacName.Text = facInfo.FacilityName.Trim();
                    if (facInfo.FacilityAddress != "")
                        txtfacAdd.Text = facInfo.FacilityAddress.Trim();
                    if (facInfo.FacilityCity != "")
                        txtfacCity.Text = facInfo.FacilityCity.Trim();
                    if (facInfo.FacilityState != "")
                        txtfacState.Text = facInfo.FacilityState.Trim();
                    if (facInfo.FacilityZip != "")
                        txtfacZip.Text = facInfo.FacilityZip.Trim();
                    if (facInfo.FacilityTPhone != "")
                        txtfacPhone.Text = facInfo.FacilityTPhone.Trim();
                    if (facInfo.FacilityFax != "")
                        txtfacFax.Text = facInfo.FacilityFax.Trim();
                    if (facInfo.FacilityEMail != "")
                        txtfacEmail.Text = facInfo.FacilityEMail.Trim();
                    if (facInfo.FacilityTaxID != "")
                        txtfacTaxID.Text = facInfo.FacilityTaxID.Trim();
                    if (facInfo.FacilitySpeciality != "")
                        txtfacSpeciality.Text = facInfo.FacilitySpeciality.Trim();
                    if (facInfo.FacilityProvID != "")
                        txtfacProvID.Text = facInfo.FacilityProvID.Trim();
                    if (clinic.ClinicName != "")
                        txtClinic.Text = clinic.ClinicName.Trim();

                    txtfacName.ReadOnly = true;
                    btnFacSave.Visible = false;
                    btnFacUpdate.Visible = true;
                    btnFacDelete.Visible = true;
                    txtSearchFac.Text = "";
                }
                else
                {
                    string str = "alert('No Records Found...');";
                    ScriptManager.RegisterStartupScript(btnSearchFac, typeof(Page), "alert", str, true);
                    txtSearchFac.Text = "";
                }
            }
            else
            {
                string str = "alert('Enter Search Text...');";
                ScriptManager.RegisterStartupScript(btnSearchFac, typeof(Page), "alert", str, true);
                txtSearchFac.Text = "";
            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
       
    }
    protected void btnFacCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearTextBoxes();
            btnFacDelete.Visible = false;
            btnFacUpdate.Visible = false;
            btnFacSave.Visible = true;
            txtfacName.ReadOnly = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnFacDelete_Click(object sender, ImageClickEventArgs e)
    {
         
        string userID = (string)Session["User"];
        try
        {
            facInfo.FacilityName = txtfacName.Text;
            facInfo.FacilityID = txtfacID.Text;
            DataTable facData = facDAL.getFacSearch(facInfo);
            facInfo.FacilityNO = Convert.ToInt32(facData.Rows[0][0].ToString());

            
            int flag = facDAL.Delete_facInfo(facInfo,userID);
            if (flag == 1)
            {
                string str = "alert('Deleted Facility Info Successfully...');";
                ScriptManager.RegisterStartupScript(btnFacDelete, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed To Delete Facility Info...');";
                ScriptManager.RegisterStartupScript(btnFacDelete, typeof(Page), "alert", str, true);
            }
            clearTextBoxes();
            btnFacDelete.Visible = false;
            btnFacUpdate.Visible = false;
            btnFacSave.Visible = true;
            txtfacName.ReadOnly = false;
        }
        catch (Exception ex)
        {
            
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnFacUpdate_Click(object sender, ImageClickEventArgs e)
    {
        string facStatus;
        try
        {
            if (chkStampAddr.Checked == true)
                facInfo.IsStamps = 'Y';
            else
                facInfo.IsStamps = 'N';

            facInfo.FacilityName = txtfacName.Text;
            facInfo.FacilityID = txtfacID.Text;
            DataTable facData = facDAL.getFacSearch(facInfo);
            facInfo.FacilityNO = Convert.ToInt32(facData.Rows[0][0].ToString());
            facInfo.FacilityID = txtfacID.Text;
            facInfo.FacilityAddress = txtfacAdd.Text;
            facInfo.FacilityCity = txtfacCity.Text;
            facInfo.FacilityState = txtfacState.Text;
            facInfo.FacilityZip = txtfacZip.Text;
            facInfo.FacilityTPhone = txtfacPhone.Text;
            facInfo.FacilityFax = txtfacFax.Text;
            facInfo.FacilityEMail = txtfacEmail.Text;
            facInfo.FacilityTaxID = txtfacTaxID.Text;
            facInfo.FacilitySpeciality = txtfacSpeciality.Text;
            facInfo.FacilityProvID = txtfacProvID.Text;
            clinic.ClinicName = txtClinic.Text;
            clinic.ClinicID = cDAL.getClinicID(clinic);
            string userID = (string)Session["User"];
            int flag = facDAL.Update_facInfo(facInfo, clinic, userID);
            if (flag == 1)
            {
                string str = "alert('Updated Facility Info Successfully...');";
                ScriptManager.RegisterStartupScript(btnFacUpdate, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed To Update Facility Info...');";
                ScriptManager.RegisterStartupScript(btnFacUpdate, typeof(Page), "alert", str, true);
            }
            clearTextBoxes();
            btnFacDelete.Visible = false;
            btnFacUpdate.Visible = false;
            btnFacSave.Visible = true;
            txtfacName.ReadOnly = false;


        }
        catch (Exception ex)
        {
            facStatus = ex.Message;
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
                // Now register the method GetClientId, used to get the client id of tthe control
                cs.RegisterStartupScript(this.Page.GetType(), "key", "\nfunction GetClientId(serverId)\n{\nfor(i=0; i<MyServerID.length; i++)\n{\nif ( MyServerID[i] == serverId )\n{\nreturn MyClientID[i];\nbreak;\n}\n}\n}", true);
            }
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
}
