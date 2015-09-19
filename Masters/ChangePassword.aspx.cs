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
using System.Text;
using NLog;

public partial class Masters_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null)
        {
            if ((string)Session["Role"] == "A")
            {
                txtUserID.ReadOnly = false  ;
            }
            else
            {
                txtUserID.Text = (string)Session["User"];
                txtUserID.ReadOnly = true;
            }
        }
        RenderJSArrayWithCliendIds(txtUserID, txtOldPwd, txtNewPassword,txtConfirmPassword);
    }
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    
    protected void btnSaveNewPwd_Click(object sender, ImageClickEventArgs e)
    {
        
        RegisterUserBLL objUser = new RegisterUserBLL();
        Property objProp = new Property();

        objProp.UserID = txtUserID.Text;
        objProp.OldPassword = txtOldPwd.Text;
        objProp.Password = txtNewPassword.Text;

        string user = (string)Session["User"];

        try
        {
            if (objUser.ChangePassword(objProp, user))
            {
                string str = "alert('Password Changed Successfully...');";
                ScriptManager.RegisterStartupScript(btnSaveNewPwd, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed to Change Password, Please try again...');";
                ScriptManager.RegisterStartupScript(btnSaveNewPwd, typeof(Page), "alert", str, true);
            }
            ClearControls();
        }
        catch(Exception ex)
        {
            objNLog.Error("Error: " + ex.Message);
        }
    }
    private void ClearControls()
    {
        txtUserID.Text = "";
        txtOldPwd.Text = "";
        txtNewPassword.Text = "";
    }
    protected void btnChgPwdCancel_Click(object sender, ImageClickEventArgs e)
    {
        ClearControls();
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
