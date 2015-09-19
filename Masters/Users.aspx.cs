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
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NLog;


public partial class Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");
        if ((string)Session["Role"] == "C")
        {
            Server.Transfer("../Patient/AccessDenied.aspx");

        }
        if (!Page.IsPostBack)
        {
            provRoles();
            this.Page.Form.Enctype = "multipart/form-data";
        }
        RenderJSArrayWithCliendIds(txtUserID, txtPassword, txtEmployeeName, txtDocName,lblAvailability);
        txtUserID.Attributes.Add("onblur", "checkUsernameUsage(this.value)");
    }

    Property objProp = new Property();
    RegisterUserBLL objRegUser = new RegisterUserBLL();
    RegisterUserDAL regUserDAL = new RegisterUserDAL();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void btnRegister_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string user = (string)Session["User"];
            objProp.UserID = txtUserID.Text;
            objProp.Password = txtPassword.Text;
            objProp.UserRole = ddlUserRole.SelectedValue.ToString();
            objProp.Comments = txtComments.Text;

            if (chkStamps.Checked == true)
            {
                if (ddlStampLocation.Visible == true)
                {
                    objProp.StampLoc = ddlStampLocation.SelectedValue;
                }
            }

            string[] empName;
            if (ddlUserRole.SelectedValue == "D" || ddlUserRole.SelectedValue == "N")
                empName = txtDocName.Text.Split(',');
            else
                empName = txtEmployeeName.Text.Split(',');
            if (empName.Length == 2)
            {
                if (ddlUserRole.SelectedValue == "D" || ddlUserRole.SelectedValue == "N")
                    objProp.DocID = empId.Value;
                else
                    objProp.DocID = "0";

                objProp.EMPID = int.Parse(empId.Value);
                objProp.EMPFName = empName[1];
                objProp.EMPLName = empName[0];
            }
            else
            {
                objProp.DocID = "0";
                objProp.EMPID = 0;
                objProp.EMPFName = "";
                objProp.EMPLName = "";

            }
            if (objRegUser.RegisterNewUser(objProp, user))
            {
                string str = "alert('User Registered Successfully...');";
                ScriptManager.RegisterStartupScript(btnRegister, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed to register, Please try again...');";
                ScriptManager.RegisterStartupScript(btnRegister, typeof(Page), "alert", str, true);
            }
            clearTxtData();
            ddlUserRole.ClearSelection();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    public void clearTextBoxes()
    {
        try
        {
            txtPassword.Text = string.Empty;

            txtSearchUser.Text = string.Empty;
            ddlUserRole.ClearSelection();

            txtUserID.Visible = true;
            ddlUserRole.Enabled = true;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    

    protected void btnSearchUser_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objProp.UserID = txtSearchUser.Text;
            DataTable usrData = regUserDAL.getUserSearch(objProp);
            clearTextBoxes();

            txtPassword.Visible = false;
            lblPassword.Visible = false;
            lblPwdLength.Visible = false;

            if (usrData.Rows.Count > 0)
            {
                objProp.UserID = usrData.Rows[0]["User_Id"].ToString();
                txtUserID.Text = objProp.UserID;
                //objProp.Password = usrData.Rows[0]["Password"].ToString();
                objProp.PwdRem = usrData.Rows[0]["Comments"].ToString();
                txtComments.Text = objProp.PwdRem;
               
                objProp.UserRole = usrData.Rows[0]["User_Role"].ToString();
                
                ddlUserRole.Items.FindByValue(objProp.UserRole).Selected = true;
                if (objProp.UserRole == "D" || objProp.UserRole == "N")
                {
                    txtEmployeeName.Visible = false;
                    txtDocName.Text = usrData.Rows[0]["empName"].ToString();
                    txtDocName.Visible = true;
                    ACE_DoctorName.ContextKey = objProp.UserRole;

                }
                else
                {
                    txtEmployeeName.Visible = true;
                    txtEmployeeName.Text = usrData.Rows[0]["empName"].ToString();
                    txtDocName.Visible = false;
                }
                empId.Value = usrData.Rows[0]["empID"].ToString();
               
                objProp.StampLoc = usrData.Rows[0]["LocID"].ToString();
                
                if (objProp.StampLoc != "")
                {
                    chkStamps.Checked = true;
                    FillStampsLoc();
                    ddlStampLocation.SelectedValue = objProp.StampLoc;
                }
                
                btnUserDelete.Visible = true;
                btnUserUpdate.Visible = true;
                btnRegister.Visible = false;
                
                txtUserID.ReadOnly = true;
            }
            else
            {
                 
                string str = "alert('No Records Found...');";
                ScriptManager.RegisterStartupScript(btnSearchUser, typeof(Page), "alert", str, true);
                txtSearchUser.Text = "";
                empId.Value = "0";
                
                btnUserDelete.Visible = false;
                btnUserUpdate.Visible = false;
                btnRegister.Visible = true;
                txtUserID.ReadOnly = false;
                clearTxtData();
               
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnUserUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string usrStatus;
            string user = (string)Session["User"];
            objProp.UserID = txtUserID.Text;
            //objProp.Password = txtPassword.Text;
 
            objProp.UserRole = ddlUserRole.SelectedValue.ToString();
            objProp.Comments = txtComments.Text;

            if (chkStamps.Checked == true)
            {
                if (ddlStampLocation.Visible == true)
                {
                    objProp.StampLoc = ddlStampLocation.SelectedValue;
                }
            }

            string[] empName;

            if (ddlUserRole.SelectedValue == "D" || ddlUserRole.SelectedValue == "N")
                empName = txtDocName.Text.Split(',');
            else
                empName = txtEmployeeName.Text.Split(',');

            if (empName.Length == 2)
            {
                if (ddlUserRole.SelectedValue == "D" || ddlUserRole.SelectedValue == "N")
                    objProp.DocID = empId.Value;
                else
                    objProp.DocID = "0";

                objProp.EMPID = int.Parse(empId.Value);
                objProp.EMPFName = empName[1];
                objProp.EMPLName = empName[0];
            }
            else
            {
                objProp.DocID = "0";
                objProp.EMPID = 0;
                objProp.EMPFName = "";
                objProp.EMPLName = "";
            }

            usrStatus = regUserDAL.Update_UserInfo(objProp, user);

            string str = "alert('" + usrStatus + "');";
            ScriptManager.RegisterStartupScript(btnRegister, typeof(Page), "alert", str, true);
            btnUserDelete.Visible = false;
            btnUserUpdate.Visible = false;
            btnRegister.Visible = true;
            clearTxtData();
            txtUserID.ReadOnly = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    protected void btnUserCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearTxtData();

            txtPassword.Visible = true;
            lblPassword.Visible = true;
            lblPwdLength.Visible = true;

            btnUserDelete.Visible = false;
            btnUserUpdate.Visible = false;
            btnRegister.Visible = true;
            txtUserID.ReadOnly = false;
            empId.Value = "0";
            lblAvailability.Text = "Check Availability";
            lblAvailability.Visible = false;
            chkStamps.Checked = false;
            ddlStampLocation.Visible = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
   
    public void clearTxtData()
    {
        try
        {
            txtComments.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtSearchUser.Text = string.Empty;
            txtUserID.Text = string.Empty;
            txtDocName.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtDocName.Visible = false;
            txtEmployeeName.Visible = true;
            empId.Value = "0";
            ddlUserRole.ClearSelection();
            chkStamps.Checked = false;
            ddlStampLocation.Visible = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }


    protected void btnUserDelete_Click(object sender, ImageClickEventArgs e)
    {
        string usrStatus;
        try
        {
            string user = (string)Session["User"];
            objProp.UserID = txtUserID.Text;
            DataTable usrData = regUserDAL.getUserSearch(objProp);
            objProp.UserID = usrData.Rows[0][0].ToString();
            usrStatus = regUserDAL.Delete_UserInfo(objProp,user);
            string str = "alert('" + usrStatus + "');";
            ScriptManager.RegisterStartupScript(btnUserDelete, typeof(Page), "alert", str, true);
            clearTextBoxes();
            btnUserDelete.Visible = false;
            btnUserUpdate.Visible = false;
            btnRegister.Visible = true;
            txtUserID.Visible = true;
            txtUserID.ReadOnly = false;
            txtPassword.Visible = true;
            lblPassword.Visible = true;
            clearTxtData();

        }
        catch (Exception ex)
        {
            usrStatus = ex.Message;
           
            objNLog.Error("Error : " + ex.Message);
         
        }
    }

    public void provRoles()
    {
        try
        {
            ddlUserRole.DataTextField = "Role";
            ddlUserRole.DataValueField = "Role_Code";
            ddlUserRole.DataSource = (DataTable)regUserDAL.getUserRole();
            ddlUserRole.DataBind();
            txtEmployeeName.Visible = true;
            txtDocName.Visible = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserRole.SelectedValue == "D" || ddlUserRole.SelectedValue == "N")
            {
                txtEmployeeName.Visible = false;
                txtDocName.Visible = true;
                ACE_DoctorName.ContextKey = ddlUserRole.SelectedValue;
            }
            else
            {
                txtEmployeeName.Visible = true;
                txtDocName.Visible = false;

            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }


    [System.Web.Services.WebMethod]
    public static bool IsUserAvailable(string username)
    {
        
        bool returnValue;
        try
        {
            string conStr = ConfigurationManager.AppSettings["conStr"];
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_CheckUserAvailability", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@UserName", username.Trim());
            SqlParameter flag = sqlCmd.Parameters.Add("@flag", SqlDbType.Int);
            flag.Direction = ParameterDirection.Output;
            flag.Value = 0;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            if ((int)flag.Value == 1)
                returnValue = false;
            else
                returnValue = true;
            
        }
        catch  
        {
              return false;
        }

        return returnValue;

    }

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
            foreach (DataRow dr in dtFac.Rows)
            {
                Fac_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Facility_Name"].ToString(), dr[0].ToString()));
            }
        }

        return Fac_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        List<string> Pat_List = new List<string>();
        Pat_Info = new PatientInfoDAL();
        Pat_List.Clear();
        DataTable Pat_Names;
        if (contextKey == "N")
            Pat_Names = Pat_Info.get_NurseNames(prefixText);
        else
            Pat_Names = Pat_Info.get_DoctorNames(prefixText);
        if (Pat_Names.Rows.Count > 0)
        {

            foreach (DataRow dr in Pat_Names.Rows)
            {
                Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString(), dr[2].ToString()));
            }
        }
        else
        {
            Pat_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Doctor Found..!", "0"));
        }
        return Pat_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetEmpNames(string prefixText, int count)
    {
        List<string> User_List = new List<string>();
        RegisterUserDAL reg_Usr_DAL = new RegisterUserDAL();

        User_List.Clear();
        DataTable dtFac = reg_Usr_DAL.get_EmpNames(prefixText, count);
        foreach (DataRow dr in dtFac.Rows)
        {
            User_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[2].ToString() + "," + dr[1].ToString(), dr[0].ToString()));
        }

        return User_List.ToArray();
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetUserNames(string prefixText, int count)
    {
        List<string> User_List = new List<string>();
        RegisterUserDAL reg_Usr_DAL = new RegisterUserDAL();

        User_List.Clear();
        DataTable dtFac = reg_Usr_DAL.get_UserNames(prefixText, count);
        if (dtFac.Rows.Count > 0)
        {
            foreach (DataRow dr in dtFac.Rows)
            {
                User_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["User_Id"].ToString(), dr["User_Id"].ToString()));
            }
        }
        else
        {
            User_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No User Found..!", "0"));
        }

        return User_List.ToArray();
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
    protected void chkStamps_CheckedChanged(object sender, EventArgs e)
    {
        if (chkStamps.Checked == true)
        {
            FillStampsLoc();
        }
        else
        {
            ddlStampLocation.Visible = false;
        }
    }

    private void FillStampsLoc()
    {
        ddlStampLocation.Visible = true;
        ddlStampLocation.DataTextField = "LocID";
        ddlStampLocation.DataValueField = "LocID";
        ddlStampLocation.DataSource = objRegUser.GetStampLocations();
        ddlStampLocation.DataBind();
    }
}