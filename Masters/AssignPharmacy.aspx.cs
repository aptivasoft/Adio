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
using NLog;
public partial class Masters_AssignPharmacy : System.Web.UI.Page
{
    FacilityInfoDAL objFacInfo = new FacilityInfoDAL();
    ClinicDAL objClinicInfo = new ClinicDAL();
    PharmacyInfoDAL objPhrmInfo = new PharmacyInfoDAL();
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
            BindPharmacyList();
            BindPharmacyTechList();
        }
    }

    protected void rbtnPhrmTech_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPhrmTech.Checked == true)
        {
            pnlPhrmTech.Visible = true;
            pnlPhrmFacility.Visible = false;
            ddlPharmacyNames.ClearSelection();
            BindPharmacyList();
            BindPharmacyTechList();
        }
    }

    protected void rbtnFacility_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnFacility.Checked == true)
        {
            pnlPhrmFacility.Visible = true;
            pnlPhrmTech.Visible = false;
            ddlPharmacyNames.ClearSelection();
            BindClinicList();
            BindFacilityList();
        }
    }

    private void BindPharmacyList()
    {
        objNLog.Info("Function Started..");
        try
        {
            ddlPharmacyNames.DataTextField = "Phrm_Name";
            ddlPharmacyNames.DataValueField = "Phrm_Loc";
            ddlPharmacyNames.DataSource = objPhrmInfo.GetPharmacyList();
            ddlPharmacyNames.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    private void BindClinicList()
    {
        objNLog.Info("Function Started..");
        try
        {
            ddlClinicNames.DataTextField = "Clinic_Name";
            ddlClinicNames.DataValueField = "Clinic_ID";
            ddlClinicNames.DataSource = objClinicInfo.GetClinicList();
            ddlClinicNames.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    private void BindFacilityList()
    {
        objNLog.Info("Function Started..");
        try
        {
            lstFacilityNames.DataTextField = "Facility_Name";
            lstFacilityNames.DataValueField = "Facility_ID";
            DataSet dsFacilities = objFacInfo.GetFacilityList(int.Parse(ddlClinicNames.SelectedValue), ddlPharmacyNames.SelectedValue);

            if (dsFacilities.Tables.Count > 0)
            {
                lstFacilityNames.DataSource = dsFacilities.Tables[0];
                lstFacilityNames.DataBind();

                if (dsFacilities.Tables[1].Rows.Count > 0)
                {
                    foreach (ListItem li in lstFacilityNames.Items)
                    {
                        foreach (DataRow dr in dsFacilities.Tables[1].Rows)
                        {
                            if (li.Text.Equals(dr[0].ToString().Trim()))
                            {
                                li.Selected = true;

                            }
                        }
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

    private void BindPharmacyTechList()
    {
        objNLog.Info("Function Started..");
        try
        {
            lstPhrmTechNames.DataTextField = "UserName";
            lstPhrmTechNames.DataValueField = "UserID";

            DataSet dsPhrmTechs = objPhrmInfo.GetPharmacyTechPharmacy(ddlPharmacyNames.SelectedValue);

            if (dsPhrmTechs.Tables.Count > 0)
            {
                lstPhrmTechNames.DataSource = dsPhrmTechs.Tables[0];
                lstPhrmTechNames.DataBind();

                if (dsPhrmTechs.Tables[1].Rows.Count > 0)
                {
                    foreach (ListItem li in lstPhrmTechNames.Items)
                    {
                        foreach (DataRow dr in dsPhrmTechs.Tables[1].Rows)
                        {
                            if (li.Value.ToLower().Equals(dr[0].ToString().ToLower().Trim()))
                            {
                                li.Selected = true;

                            }
                        }
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

    protected void ddlPharmacyNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Function Started..");
        if(rbtnPhrmTech.Checked==true)
            BindPharmacyTechList();
        
        if(rbtnFacility.Checked==true)
            BindFacilityList();
        objNLog.Info("Function Completed..");

    }

    protected void ddlClinicNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Function Started..");
        BindFacilityList();
        objNLog.Info("Function Completed..");
    }

    protected void btnAssignPharmacy_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");
        
            try
            {
                if (rbtnPhrmTech.Checked == true)
                {
                    string userID = (string)Session["User"];
                    int flag = 0;
                    foreach (ListItem li in lstPhrmTechNames.Items)
                    {
                        if (li.Selected == true)
                        {
                            objPhrmInfo.SetPharmacyTechPharmacy(li.Value, ddlPharmacyNames.SelectedValue);
                            flag = 1;
                        }
                    }
                    if (flag == 1)
                    {
                        string str = "alert('Pharmacy Assigned Successfully...');";
                        ScriptManager.RegisterStartupScript(btnAssignPharmacy, typeof(Page), "alert", str, true);
                    }
                }
                else if (rbtnFacility.Checked == true)
                {
                    string userID = (string)Session["User"];
                    int flag = 0;
                    foreach (ListItem li in lstFacilityNames.Items)
                    {
                        if (li.Selected == true)
                        {
                            objFacInfo.SetFacilityPharmacy(int.Parse(li.Value), ddlPharmacyNames.SelectedValue, userID);
                            flag = 1;
                        } else
                            objFacInfo.SetFacilityPharmacy(int.Parse(li.Value), ddlPharmacyNames.SelectedValue, userID);

                    }
                    if (flag == 1)
                    {
                        string str = "alert('Pharmacy Assigned Successfully...');";
                        ScriptManager.RegisterStartupScript(btnAssignPharmacy, typeof(Page), "alert", str, true);
                    }
                }
            }
            catch (Exception ex)
            {
                objNLog.Error("Error : " + ex.Message);
            }
        objNLog.Info("Event Completed..");
    }    

    protected void btnCancelAssignment_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");
        ClearPanel();
        objNLog.Info("Event Completed..");
    }

    private void ClearPanel()
    {
        objNLog.Info("Function Started..");
        try
        {
            if (rbtnFacility.Checked == true)
            {
                ddlPharmacyNames.ClearSelection();
                ddlClinicNames.ClearSelection();
                lstFacilityNames.ClearSelection();
                BindFacilityList();
            }
            else
            {
                ddlPharmacyNames.ClearSelection();
                lstPhrmTechNames.ClearSelection();
                BindPharmacyTechList();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
}
