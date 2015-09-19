using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Text;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using NLog;
 

public partial class Login : System.Web.UI.Page
{
    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
   
    protected void Page_Load(object sender, EventArgs e)
    { }
   
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            renderLoginControls();
            addControlEvents();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    public void renderLoginControls()
    {
        try
        {
            string XmlPath = Server.MapPath("xml/UserLogin.xml");
            string XsltPath = Server.MapPath("xslt/UserLogin.xslt");

            XPathDocument xdoc = new XPathDocument(XmlPath);

            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(XsltPath);

            StringWriter sw = new StringWriter();

            //Transform
            transform.Transform(xdoc, null, sw);
            string result = sw.ToString();

            //Remove Namespace
            result = result.Replace("xmlns:asp=\"remove\"", "");
            result = result.Replace("xmlns:cc1=\"remove\"", "");

            //Parse Control
            Control ctrl = Page.ParseControl(result);
            phLogin.Controls.Add(ctrl);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    ImageButton btnLogin;
    public void addControlEvents()
    {
        try
        {
            TextBox txtUserID = (TextBox)phLogin.FindControl("txtUserID");
            txtUserID.Focus();
            txtUserID.Attributes.Add("onfocus", "ClearFields();");
            btnLogin = (ImageButton)phLogin.FindControl("btnLogin");
            btnLogin.Click += new System.Web.UI.ImageClickEventHandler(btnLogin_Click);
            btnLogin.Attributes.Add("onClientClick", "return validateUser();");
            Panel pnlForgotPwdPh = (Panel)phLogin.FindControl("pnlForgotPwdPh");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    [System.Web.Services.WebMethod]
    public static string[] GetFacilityNames(string prefixText, int count)
    {
        List<string> facility_List = new List<string>();
        FacilityInfoDAL fact_Info = new FacilityInfoDAL();
        DataTable dtFacility = fact_Info.get_FacilityNames(prefixText, count);
            foreach (DataRow dr in dtFacility.Rows)
            {
                facility_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Facility_Name"].ToString(), dr["Facility_ID"].ToString()));
            }
            return facility_List.ToArray();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            Property objProp = new Property();
            UserLoginBLL userLog = new UserLoginBLL();

            EncryptPassword encPwd = new EncryptPassword();

            TextBox txtUserID = (TextBox)phLogin.FindControl("txtUserID");
            TextBox txtPwd = (TextBox)phLogin.FindControl("txtPassword");

            string encPassword = encPwd.EncryptText(txtPwd.Text, "helloworld");
            objProp.UserID = txtUserID.Text.Trim();
            objProp.Password = encPassword.Trim();

            if (userLog.LoginUser(objProp))
            {
                Session["User"] = objProp.UserID;
                if ((string)Session["Role"] == "D")
                    Response.Redirect("Home/DoctorHome.aspx");
                else if ((string)Session["Role"] == "N")
                    Response.Redirect("Home/NurseHome.aspx");
                else if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
                    Response.Redirect("Home/PharmacistHome.aspx");
                else if ((string)Session["Role"] == "C")
                    Response.Redirect("Home/CSRHome.aspx");
                else
                    Response.Redirect("Patient/AllPatientProfile.aspx");
            }
            else
            {
                Session["User"] = null;
                Label lblStatus = (Label)phLogin.FindControl("lblStatus");
                lblStatus.Visible = true;
                lblStatus.Text = "Invalid UserID/Password..!";
                objNLog.Error("Login failed for the user - " + txtUserID.Text);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
}
