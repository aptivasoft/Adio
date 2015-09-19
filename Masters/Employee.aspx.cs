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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using NLog;

public partial class Patient_setEmployeeInfo : System.Web.UI.Page
{
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");
        if ((string)Session["Role"] == "C")
        {
            Server.Transfer("../Patient/AccessDenied.aspx");

        }
        if (!IsPostBack)
        {
            provRoles();
            if (Request.QueryString["EmpID"] != null)
            {
                publishEMPData(int.Parse((string)Request.QueryString["EmpID"]));
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
            }
        }
        RenderJSArrayWithCliendIds(txtTitle, txtDesc,txtempFName,txtempLName,txtHireDate,txtLoc);
        hdnSup2.Value = txtSup1.Text;
        hdnSup1.Value = txtempLName.Text + ',' + txtempFName.Text;
    }

    public void provRoles()
    {
        try
        {
            EmployeeInfoDAL emp_Info = new EmployeeInfoDAL();
            ddlempRole.DataTextField = "Role";
            ddlempRole.DataValueField = "Role_Code";
            ddlempRole.DataSource = (DataTable)emp_Info.getUserRole();
            ddlempRole.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetEmpNames(string prefixText, int count)
    {
        EmployeeInfoDAL emp_Info = new EmployeeInfoDAL();
        List<string> emp_List = new List<string>();
        emp_Info = new EmployeeInfoDAL();
        emp_List.Clear();
        DataTable _Names = emp_Info.get_EMPNames(prefixText);
        foreach (DataRow dr in _Names.Rows)
        {
            emp_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString() + " , " + dr[2].ToString(), dr[2].ToString()));
        }
        return emp_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSup1Names(string prefixText, int count, string contextKey)
    {
        EmployeeInfoDAL emp_Info = new EmployeeInfoDAL();
        List<string> emp_List = new List<string>();
        emp_Info = new EmployeeInfoDAL();
        emp_List.Clear();
        DataTable _Names = emp_Info.get_SUP1Names(prefixText,contextKey);
        foreach (DataRow dr in _Names.Rows)
        {
            emp_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString(), dr[2].ToString()));
        }
        return emp_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSup2Names(string prefixText, int count, string contextKey)
    {
        EmployeeInfoDAL emp_Info = new EmployeeInfoDAL();
        List<string> emp_List = new List<string>();
        emp_Info = new EmployeeInfoDAL();
        emp_List.Clear();
        DataTable _Names = emp_Info.get_SUP2Names(prefixText, contextKey);
        foreach (DataRow dr in _Names.Rows)
        {
            emp_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString() + "," + dr[0].ToString(), dr[2].ToString()));
        }
        return emp_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetLocationNames(string prefixText, int count)
    {
        EmployeeInfoDAL emp_Info = new EmployeeInfoDAL();
        List<string> emp_List = new List<string>();
        emp_Info = new EmployeeInfoDAL();
        emp_List.Clear();
        DataTable _Names = emp_Info.get_LocationNames(prefixText);
        foreach (DataRow dr in _Names.Rows)
        {
            emp_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString()+ "," + dr[1].ToString(), dr[2].ToString()));
        }
        return emp_List.ToArray();
    }
 
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetTitleNames(string prefixText, int count)
    {
        EmployeeInfoDAL emp_Info = new EmployeeInfoDAL();
        List<string> emp_List = new List<string>();
        emp_Info = new EmployeeInfoDAL();
        emp_List.Clear();
        DataTable _Names = emp_Info.get_TitleNames(prefixText);
        foreach (DataRow dr in _Names.Rows)
        {
            emp_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
        }
        return emp_List.ToArray();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            publishEMPData();
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EmployeeInfoDAL EMP_data = new EmployeeInfoDAL();
            EmployeeInfo EMP_Info = new EmployeeInfo();
            DataTable dtEmpData = new DataTable();
            EMP_Info.Address1 = txtAddress1.Text;
            EMP_Info.Address2 = txtAddress2.Text;
            EMP_Info.City = txtCity.Text;
            EMP_Info.Comments = txtComments.Text;
            EMP_Info.EMail = txtemail.Text;

            EMP_Info.EMPHireDate = DateTime.Parse(txtHireDate.Text);
            EMP_Info.EMPID = int.Parse(hdnEmpID.Value.ToString());

            if (txtTDate.Text != "")
                EMP_Info.EMPTermDate = DateTime.Parse(txtTDate.Text);
            EMP_Info.IsApprove = ddlApprov.SelectedValue.ToString();

            if (txtSup1.Text != "")
            {
                string[] arInfo1 = new string[2];
                char[] splitter1 = { ',' };
                arInfo1 = txtSup1.Text.Split(splitter1);
                EMP_Info.EMPLName = arInfo1[0].ToString();
                EMP_Info.EMPFName = arInfo1[1].ToString();
                dtEmpData = EMP_data.get_EMPDataByNames(EMP_Info);

                if (dtEmpData.Rows.Count > 0)
                {
                    EMP_Info.EMPSup1 = int.Parse(dtEmpData.Rows[0]["Emp_ID"].ToString());
                }
                else
                {
                    string script = @"alert(Supervisor1 donot exist);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);

                }
            }
            if (txtSup2.Text != "")
            {
                string[] arInfo2 = new string[2];
                char[] splitter2 = { ',' };
                arInfo2 = txtSup2.Text.Split(splitter2);
                EMP_Info.EMPLName = arInfo2[0].ToString();
                EMP_Info.EMPFName = arInfo2[1].ToString();
                dtEmpData = EMP_data.get_EMPDataByNames(EMP_Info);

                if (dtEmpData.Rows.Count > 0)
                {
                    EMP_Info.EMPSup2 = int.Parse(dtEmpData.Rows[0]["Emp_ID"].ToString());
                }
                else
                {
                    string script = @"alert(Supervisor2 donot exist);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);

                }

            }

            EMP_Info.EMPFName = txtempFName.Text;
            EMP_Info.EMPLName = txtempLName.Text;
            EMP_Info.EMPMName = txtempMName.Text;

            if (txtLoc.Text != "")
            {
                EMP_Info.LocationID = int.Parse(EMP_data.get_LocationNamesByName(txtLoc.Text).Rows[0]["Facility_ID"].ToString());
            }
            EMP_Info.Pat_CellPhone = txtHomePhone.Text;
            EMP_Info.Phone = txtPhone.Text;
            EMP_Info.Role = ddlempRole.SelectedValue.ToString();
            EMP_Info.State = txtState.Text;
            EMP_Info.Status = ddlStatus.SelectedValue.ToString();
            if (txtTitle.Text != "")
            {
                EMP_Info.TitleID = int.Parse(EMP_data.get_TitleNamesByNames(txtTitle.Text).Rows[0]["Title_ID"].ToString());
            }
            EMP_Info.ZIP = txtZip.Text;
            string userID = (string)Session["User"];
            int flag = EMP_data.update_EmpInfo(EMP_Info,userID);
            if (flag == 1)
            {
                string script2 = @"alert('Employee Info Updated Successfully...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script2, true);
                Cleartxt();
            }
            else
            {
                string script2 = @"alert('Failed To Update Employee Info...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script2, true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EmployeeInfoDAL EMP_data = new EmployeeInfoDAL();
            EmployeeInfo EMP_Info = new EmployeeInfo();
            DataTable dtEmpData = new DataTable();
            EMP_Info.Address1 = txtAddress1.Text;
            EMP_Info.Address2 = txtAddress2.Text;
            EMP_Info.City = txtCity.Text;
            EMP_Info.Comments = txtComments.Text;
            EMP_Info.EMail = txtemail.Text;

            EMP_Info.EMPHireDate = DateTime.Parse(txtHireDate.Text);
            //EMP_Info.EMPID = int.Parse(hdnEmpID.Value.ToString());

            if (txtSup1.Text != "")
            {
                string[] arInfo1 = new string[2];
                char[] splitter1 = { ',' };
                arInfo1 = txtSup1.Text.Split(splitter1);
                EMP_Info.EMPLName = arInfo1[0].ToString();
                EMP_Info.EMPFName = arInfo1[1].ToString();
                dtEmpData = EMP_data.get_EMPDataByNames(EMP_Info);

                if (dtEmpData.Rows.Count > 0)
                {
                    EMP_Info.EMPSup1 = int.Parse(dtEmpData.Rows[0]["Emp_ID"].ToString());
                }
                else
                {
                    string script = @"alert(Supervisor1 does not exist);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);

                }
            }
            if (txtSup2.Text != "")
            {
                string[] arInfo2 = new string[2];
                char[] splitter2 = { ',' };
                arInfo2 = txtSup2.Text.Split(splitter2);
                EMP_Info.EMPLName = arInfo2[0].ToString();
                EMP_Info.EMPFName = arInfo2[1].ToString();
                dtEmpData = EMP_data.get_EMPDataByNames(EMP_Info);

                if (dtEmpData.Rows.Count > 0)
                {
                    EMP_Info.EMPSup2 = int.Parse(dtEmpData.Rows[0]["Emp_ID"].ToString());
                }
                else
                {
                    string script = @"alert(Supervisor2 does not exist);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);

                }

            }

            EMP_Info.EMPFName = txtempFName.Text;
            EMP_Info.EMPLName = txtempLName.Text;
            EMP_Info.EMPMName = txtempMName.Text;

            if (txtTDate.Text != string.Empty)
            {
                EMP_Info.EMPTermDate = DateTime.Parse(txtTDate.Text);
            }
            EMP_Info.IsApprove = ddlApprov.SelectedValue.ToString();
            if (txtLoc.Text != string.Empty)
                EMP_Info.LocationID = int.Parse(EMP_data.get_LocationNamesByName(txtLoc.Text).Rows[0]["Facility_ID"].ToString());
            EMP_Info.Pat_CellPhone = txtHomePhone.Text;
            EMP_Info.Phone = txtPhone.Text;
            EMP_Info.Role = ddlempRole.SelectedValue.ToString();
            EMP_Info.State = txtState.Text;
            EMP_Info.Status = ddlStatus.SelectedValue.ToString();

            if (txtTitle.Text != string.Empty)
                EMP_Info.TitleID = int.Parse(EMP_data.get_TitleNames(txtTitle.Text).Rows[0]["Title_ID"].ToString());
            EMP_Info.ZIP = txtZip.Text;
            string userID = (string)Session["User"];
            int flag = EMP_data.set_EmpInfo(EMP_Info, userID);
            if (flag == 1)
            {
                string script2 = @"alert('Employee Added Successfully...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script2, true);
            }
            else
            {
                string script2 = @"alert('Failed To Add Employee..!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script2, true);
            }
            Cleartxt();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            EmployeeInfoDAL EMP_data = new EmployeeInfoDAL();
            EmployeeInfo EMP_Info = new EmployeeInfo();
            EMP_Info.EMPID = int.Parse(hdnEmpID.Value.ToString());

            int flag = EMP_data.DeleteEMPData(EMP_Info, userID);
            if (flag == 1)
            {
                string script = @"alert('Employee Info Deleted Successfully...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
                Cleartxt();
            }
            else
            {
                string script = @"alert('Failed To Delete Employee Info...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        Cleartxt();
    }

    protected void chkTitle_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkTitle.Checked == true)
                PnlTitle.Visible = true;
            else
                PnlTitle.Visible = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnTitleSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            EmployeeInfoDAL EMP_data = new EmployeeInfoDAL();
            EmployeeInfo EMP_Info = new EmployeeInfo();
            EMP_Info.Title = txtNewTitle.Text;
            EMP_Info.TitleDesc = txtDesc.Text;
            if (ddlTimeEntry.SelectedItem.Text == "Yes")
                EMP_Info.TimeEntry = Convert.ToChar("Y");
            else
                EMP_Info.TimeEntry = Convert.ToChar("N");

            string str = EMP_data.set_Title(EMP_Info);
            string script = @"alert('" + str + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
            txtNewTitle.Text = string.Empty;
            txtDesc.Text = string.Empty;
            PnlTitle.Visible = false;
            chkTitle.Checked = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnTitleCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtNewTitle.Text = string.Empty;
            txtDesc.Text = string.Empty;
            //PnlTitle.Visible = false; 
        }
        catch (Exception ex)
        {
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

    public void publishEMPData()
    {
        try
        {
            EmployeeInfoDAL EMP_data = new EmployeeInfoDAL();
            EmployeeInfo EMP_Info = new EmployeeInfo();
            DataTable dtEmpData = new DataTable();
            string empFulName = txtEmpFullName.Text;
            if (empFulName.IndexOf(",") < 0)
            {
                string script = @"alert('No match found ...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
                Cleartxt();
            }
            else
            {
                string[] arInfo = new string[3];
                char[] splitter = { ',' };
                arInfo = empFulName.Split(splitter);
                EMP_Info.EMPLName = arInfo[0].ToString();
                EMP_Info.EMPFName = arInfo[1].ToString();
                EMP_Info.EMPID =   int.Parse(arInfo[2].ToString());
                dtEmpData = EMP_data.get_EMPDataByID(EMP_Info.EMPID);
            }

            if (dtEmpData.Rows.Count > 0)
            {
                hdnEmpID.Value = dtEmpData.Rows[0]["Emp_ID"].ToString();
                txtAddress1.Text = dtEmpData.Rows[0]["Emp_Address"].ToString();
                txtAddress2.Text = dtEmpData.Rows[0]["Emp_Address2"].ToString();
                txtCity.Text = dtEmpData.Rows[0]["Emp_City"].ToString();
                txtComments.Text = dtEmpData.Rows[0]["Emp_Comments"].ToString();
                txtemail.Text = dtEmpData.Rows[0]["Emp_Email"].ToString();
                txtempFName.Text = dtEmpData.Rows[0]["Emp_FName"].ToString();
                txtempLName.Text = dtEmpData.Rows[0]["Emp_LName"].ToString();
                txtempMName.Text = dtEmpData.Rows[0]["Emp_MName"].ToString();
                DateTime dt = DateTime.Parse(dtEmpData.Rows[0]["Emp_HireDate"].ToString());
                txtHireDate.Text = dt.Date.ToShortDateString();
                txtHomePhone.Text = dtEmpData.Rows[0]["Emp_HomePhone"].ToString();
                string str = dtEmpData.Rows[0]["Emp_LocID"].ToString();
                if (str != "")
                {
                    int LocID = (int)dtEmpData.Rows[0]["Emp_LocID"];//location
                    txtLoc.Text = EMP_data.get_LocationByID(LocID).Rows[0]["Facility_Name"].ToString();
                }

                string sup1 = dtEmpData.Rows[0]["Emp_SupID1"].ToString();
                if (sup1 != "")
                {
                    int Emp_Sup = (int)dtEmpData.Rows[0]["Emp_SupID1"];
                    txtSup1.Text = EMP_data.get_EMPSupName(Emp_Sup).Rows[0]["Emp_SupName"].ToString();
                }

                string sup2 = dtEmpData.Rows[0]["Emp_SupID2"].ToString();
                if (sup2 != "")
                {
                    int Emp_Sup = (int)dtEmpData.Rows[0]["Emp_SupID2"];
                    txtSup2.Text = EMP_data.get_EMPSupName(Emp_Sup).Rows[0]["Emp_SupName"].ToString();
                }




                txtPhone.Text = dtEmpData.Rows[0]["Emp_Phone"].ToString();
                txtState.Text = dtEmpData.Rows[0]["Emp_State"].ToString();

                if (dtEmpData.Rows[0]["Emp_TermDate"] != DBNull.Value)
                {
                    DateTime dt2 = DateTime.Parse(dtEmpData.Rows[0]["Emp_TermDate"].ToString());
                    txtTDate.Text = dt2.ToShortDateString();
                }
                txtTitle.Text = dtEmpData.Rows[0]["Emp_Email"].ToString();//Title
                txtZip.Text = dtEmpData.Rows[0]["Emp_Zip"].ToString();
                ddlApprov.SelectedValue = dtEmpData.Rows[0]["Emp_IS_Approver_Y_N"].ToString();
                ddlempRole.SelectedValue = dtEmpData.Rows[0]["Emp_Role"].ToString();
                ddlStatus.SelectedValue = dtEmpData.Rows[0]["Emp_Status"].ToString();


                if (dtEmpData.Rows[0]["Emp_TitleID"].ToString() != "")
                {
                    int TitleID = int.Parse(dtEmpData.Rows[0]["Emp_TitleID"].ToString());
                    txtTitle.Text = EMP_data.get_TitleByID(TitleID).Rows[0]["Title_Name"].ToString();
                }
                txtemail.Text = dtEmpData.Rows[0]["Emp_Email"].ToString();
            }
            else
            {
                string script = @"alert('No records found');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    public void Cleartxt()
    {
        try
        {
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtComments.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtempFName.Text = string.Empty;
            txtEmpFullName.Text = string.Empty;
            txtempMName.Text = string.Empty;
            txtempLName.Text = string.Empty;
            txtHireDate.Text = string.Empty;
            txtHomePhone.Text = string.Empty;
            txtSup1.Text = string.Empty;
            txtSup2.Text = string.Empty;
            txtLoc.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtState.Text = string.Empty;
            txtTDate.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtZip.Text = string.Empty;
            btnDelete.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            PnlTitle.Visible = false;
            chkTitle.Checked = false;
            ddlempRole.ClearSelection();
            ddlStatus.ClearSelection();
            ddlApprov.ClearSelection();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void lnkbtnPop_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeList.aspx");
    }
    
    public void publishEMPData(int empID)
    {
        try
        {
            EmployeeInfoDAL EMP_data = new EmployeeInfoDAL();
            EmployeeInfo EMP_Info = new EmployeeInfo();
            DataTable dtEmpData = new DataTable();
             
           
            dtEmpData = EMP_data.get_EMPDataByID(empID);
            

            if (dtEmpData.Rows.Count > 0)
            {
                hdnEmpID.Value = dtEmpData.Rows[0]["Emp_ID"].ToString();
                txtAddress1.Text = dtEmpData.Rows[0]["Emp_Address"].ToString();
                txtAddress2.Text = dtEmpData.Rows[0]["Emp_Address2"].ToString();
                txtCity.Text = dtEmpData.Rows[0]["Emp_City"].ToString();
                txtComments.Text = dtEmpData.Rows[0]["Emp_Comments"].ToString();
                txtemail.Text = dtEmpData.Rows[0]["Emp_Email"].ToString();
                txtempFName.Text = dtEmpData.Rows[0]["Emp_FName"].ToString();
                txtempLName.Text = dtEmpData.Rows[0]["Emp_LName"].ToString();
                txtempMName.Text = dtEmpData.Rows[0]["Emp_MName"].ToString();
                DateTime dt = DateTime.Parse(dtEmpData.Rows[0]["Emp_HireDate"].ToString());
                txtHireDate.Text = dt.Date.ToShortDateString();
                txtHomePhone.Text = dtEmpData.Rows[0]["Emp_HomePhone"].ToString();
                string str = dtEmpData.Rows[0]["Emp_LocID"].ToString();
                if (str != "")
                {
                    int LocID = (int)dtEmpData.Rows[0]["Emp_LocID"];//location
                    txtLoc.Text = EMP_data.get_LocationByID(LocID).Rows[0]["Facility_Name"].ToString();
                }

                string sup1 = dtEmpData.Rows[0]["Emp_SupID1"].ToString();
                if (sup1 != "")
                {
                    int Emp_Sup = (int)dtEmpData.Rows[0]["Emp_SupID1"];
                    txtSup1.Text = EMP_data.get_EMPSupName(Emp_Sup).Rows[0]["Emp_SupName"].ToString();
                }

                string sup2 = dtEmpData.Rows[0]["Emp_SupID2"].ToString();
                if (sup2 != "")
                {
                    int Emp_Sup = (int)dtEmpData.Rows[0]["Emp_SupID2"];
                    txtSup2.Text = EMP_data.get_EMPSupName(Emp_Sup).Rows[0]["Emp_SupName"].ToString();
                }




                txtPhone.Text = dtEmpData.Rows[0]["Emp_Phone"].ToString();
                txtState.Text = dtEmpData.Rows[0]["Emp_State"].ToString();
                DateTime dt2 = DateTime.Parse(dtEmpData.Rows[0]["Emp_TermDate"].ToString());
                txtTDate.Text = dt2.ToShortDateString();
                txtTitle.Text = dtEmpData.Rows[0]["Emp_Email"].ToString();//Title
                txtZip.Text = dtEmpData.Rows[0]["Emp_Zip"].ToString();
                ddlApprov.SelectedValue = dtEmpData.Rows[0]["Emp_IS_Approver_Y_N"].ToString();
                ddlempRole.SelectedValue = dtEmpData.Rows[0]["Emp_Role"].ToString();
                ddlStatus.SelectedValue = dtEmpData.Rows[0]["Emp_Status"].ToString();


                if (dtEmpData.Rows[0]["Emp_TitleID"].ToString() != "")
                {
                    int TitleID = int.Parse(dtEmpData.Rows[0]["Emp_TitleID"].ToString());
                    txtTitle.Text = EMP_data.get_TitleByID(TitleID).Rows[0]["Title_Name"].ToString();
                }
                txtemail.Text = dtEmpData.Rows[0]["Emp_Email"].ToString();

                
            }
            else
            {
                string script = @"alert('No records found');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
}
