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
using Adio.UALog;

public partial class UserControls_ADiOHeader : System.Web.UI.UserControl
{
    private static UserActivityLog objUALog = new UserActivityLog();

    string conStr = ConfigurationManager.AppSettings["conStr"];

    protected void Page_Load(object sender, EventArgs e)
    {
        lblUsername.Attributes.Add("onmouseover", "highlightUserON();");
        lblUsername.Attributes.Add("onmouseout", "highlightUserOFF();");

        if (Session["User"] != null)
            lblUsername.Text = DefaultValues.GetUserDisplayName((string)Session["User"]);
        else
            Response.Redirect("../Login.aspx");

        if (Session["RoleName"] != null)
            lblUsername.ToolTip = "Logged in as " + (string)Session["RoleName"];

        if ((string)Session["Role"] == "A" || (string)Session["Role"] == "D" || (string)Session["Role"] == "P" || (string)Session["Role"] == "T"
             || (string)Session["Role"] == "C" || (string)Session["Role"] == "N")
        {
            if ((string)Session["Role"] == "A")
            {
                hlHome.NavigateUrl = "../Home/AdminHome.aspx";
            }
            else if ((string)Session["Role"] == "D")
            {
                hlHome.NavigateUrl = "../Home/DoctorHome.aspx";
            }
            else if ((string)Session["Role"] == "N")
            {
                hlHome.NavigateUrl = "../Home/NurseHome.aspx";
            }
            else if ((string)Session["Role"] == "C")
            {
                hlHome.NavigateUrl = "../Home/CSRHome.aspx";
            }
            else
            {
                hlHome.NavigateUrl = "../Home/PharmacistHome.aspx";
            }
            hlHome.Visible = true;
            lblSeprator.Visible = true;
        }
        else
        {
            hlHome.Visible = false;
            lblSeprator.Visible = false;
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        objUALog.LogUserActivity(conStr, (string)Session["User"], "Logged Out.", "", 0);
        Response.Redirect("../Login.aspx");
    }
}
