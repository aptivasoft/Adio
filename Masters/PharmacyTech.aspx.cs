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
public partial class Masters_PharmacyTech : System.Web.UI.Page
{
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

    protected void btnAssignPharmacy_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
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
            ddlPharmacyNames.ClearSelection();
            lstPhrmTechNames.ClearSelection();
            BindPharmacyTechList();
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
        BindPharmacyTechList();
        objNLog.Info("Function Completed..");
    }
   
}
