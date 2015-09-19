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
using System.Drawing;
using NLog;

public partial class Masters_AssignReports : System.Web.UI.Page
{
    UserReportsDAL objUserRpts = new UserReportsDAL(); 
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
            BindReportsList();
            BindUserRolesList();
            BindUsersList();
        }
    }
    
    private void BindUserRolesList()
    {
        objNLog.Info("Function Started..");
        try
        {
            ddlUserRoles.DataTextField = "Role_Name";
            ddlUserRoles.DataValueField = "Role_Code";
            ddlUserRoles.DataSource = objUserRpts.GetUserRolesList();
            ddlUserRoles.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    private void BindUsersList()
    {
        objNLog.Info("Function Started..");
        try
        {
            lstUserNames.DataTextField = "UserName";
            lstUserNames.DataValueField = "UserID";

            DataSet dsUsersList = objUserRpts.GetUsersList(char.Parse(ddlUserRoles.SelectedValue.ToString()), ddlReportNames.SelectedValue);

            if (dsUsersList.Tables.Count > 0)
            {
                lstUserNames.DataSource = dsUsersList.Tables[0];
                lstUserNames.DataBind();

                if (dsUsersList.Tables[1].Rows.Count > 0)
                {
                    foreach (ListItem li in lstUserNames.Items)
                    {
                        foreach (DataRow dr in dsUsersList.Tables[1].Rows)
                        {
                            if (li.Value.Equals(dr[0].ToString().Trim()))
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

    private void BindReportsList()
    {
        objNLog.Info("Function Started..");
        try
        {
            ddlReportNames.DataTextField = "ReportName";
            ddlReportNames.DataValueField = "ReportID";
            ddlReportNames.DataSource = objUserRpts.GetReportsList();
            ddlReportNames.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void ddlReportNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        BindUsersList();
        objNLog.Info("Event Completed..");
    }  

    protected void ddlUserRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        BindUsersList();
        objNLog.Info("Event Completed..");
    }

    protected void btnAssignReport_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            string userID = (string)Session["User"];
            int flag=0;
            foreach (ListItem li in lstUserNames.Items)
            {
                if (li.Selected == true)
                {
                    flag = objUserRpts.SetUserReports(li.Value, int.Parse(ddlReportNames.SelectedValue), userID);
                }
            }
            if (flag==1)
            {
                string str = "alert('Report Assigned Successfully...');";
                ScriptManager.RegisterStartupScript(btnAssignReport, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Report already assigned to the selected user(s)..!');";
                ScriptManager.RegisterStartupScript(btnAssignReport, typeof(Page), "alert", str, true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
     
    protected void chkAllUsers_CheckedChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            if (chkAllUsers.Checked == true)
            {
                foreach (ListItem item in lstUserNames.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in lstUserNames.Items)
                {
                    item.Selected = false;
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
            ddlReportNames.ClearSelection();
            ddlUserRoles.ClearSelection();
            lstUserNames.ClearSelection();
            chkAllUsers.Checked = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
}
