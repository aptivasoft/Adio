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

public partial class Patient_setINsuranceInfo : System.Web.UI.Page
{
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        txtInsState.Attributes.Add("onkeyup", "toUppercaseIns()");
        RenderJSArrayWithCliendIds(txtInsName, txtInsNumber, txtInsCompany, txtInsAddress2, txtInsAddress1, txtInsCity, txtInsState, txtInsZip, txtInsPhone, txtInsFax);
   
    }
    InsuranceInfo insInfo = new InsuranceInfo();
    InsuranceInfoDAL insDAL = new InsuranceInfoDAL();
    protected void btnInsSave_Click(object sender, ImageClickEventArgs e)
    {
        string userID = (string)Session["User"];
        int insStatus;
        try
        {
            insInfo.InsName = txtInsName.Text;
            insInfo.InsNumber = txtInsNumber.Text;
            insInfo.InsCompany = txtInsCompany.Text;
            insInfo.InsAddress1 = txtInsAddress1.Text;
            insInfo.InsAddress2 = txtInsAddress2.Text;
            insInfo.InsCity = txtInsCity.Text;
            insInfo.InsState = txtInsState.Text;
            insInfo.InsZip = txtInsZip.Text;
            insInfo.InsPhone = txtInsPhone.Text;
            insInfo.InsFax = txtInsFax.Text;
            insStatus = insDAL.Insert_InsuranceInfo(insInfo,userID);
            if (insStatus == 1)
            {
                string str = "alert('Added Insurance Info Successfully...');";
                ScriptManager.RegisterStartupScript(btnInsSave, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed To Add Added Insurance Info...');";
                ScriptManager.RegisterStartupScript(btnInsSave, typeof(Page), "alert", str, true);
            }
            clearTextBoxes();
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
            txtInsName.Text = string.Empty;
            txtInsNumber.Text = string.Empty;
            txtInsCompany.Text = string.Empty;
            txtInsAddress2.Text = string.Empty;
            txtInsAddress1.Text = string.Empty;
            txtInsCity.Text = string.Empty;
            txtInsState.Text = string.Empty;
            txtInsZip.Text = string.Empty;
            txtInsPhone.Text = string.Empty;
            txtInsFax.Text = string.Empty;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnInsUpdate_Click(object sender, ImageClickEventArgs e)
    {
        int insStatus;
        string userID = (string)Session["User"];
        try
        {
            insInfo.InsName = txtInsName.Text;
            DataTable insData = insDAL.getInsSearch(insInfo);
            insInfo.InsID = Convert.ToInt32(insData.Rows[0][0].ToString());
            insInfo.InsNumber = txtInsNumber.Text;
            insInfo.InsCompany = txtInsCompany.Text;
            insInfo.InsAddress1 = txtInsAddress1.Text;
            insInfo.InsAddress2 = txtInsAddress2.Text;
            insInfo.InsCity = txtInsCity.Text;
            insInfo.InsState = txtInsState.Text;
            insInfo.InsZip = txtInsZip.Text;
            insInfo.InsPhone = txtInsPhone.Text;
            insInfo.InsFax = txtInsFax.Text;
            insStatus = insDAL.Update_InsInfo(insInfo,userID);
            if (insStatus == 1)
            {
                string str = "alert('Updated Insurance Info Successfully...');";
                ScriptManager.RegisterStartupScript(btnInsUpdate, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed To Update Insurance Info...');";
                ScriptManager.RegisterStartupScript(btnInsUpdate, typeof(Page), "alert", str, true);
            }
            
            clearTextBoxes();
            btnInsUpdate.Visible = false;
            btnInsSave.Visible = true;
            btnInsDelete.Visible = false;
            txtInsName.ReadOnly = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void btnInsCancel_Click(object sender, ImageClickEventArgs e)
    {
        clearTextBoxes();
        btnInsUpdate.Visible = false;
        btnInsSave.Visible = true;
        btnInsDelete.Visible = false;
        txtInsName.ReadOnly = false;
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetInsuranceNames(string prefixText, int count, string contextKey)
    {
        List<string> Ins_List = new List<string>();
        PatientInfoDAL pat_Info = new PatientInfoDAL();
        if (contextKey == "0")
        {
            Ins_List.Clear();
            DataTable dtIns = pat_Info.get_InsuranceNames(prefixText, count, contextKey);
            foreach (DataRow dr in dtIns.Rows)
            {
                Ins_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Ins_Name"].ToString(), dr[0].ToString()));
            }
        }

        return Ins_List.ToArray();
    }
    protected void btnInsDelete_Click(object sender, ImageClickEventArgs e)
    {
        int  insStatus;
        string userID = (string)Session["User"];
        try
        {
            insInfo.InsName = txtInsName.Text;
            DataTable insData = insDAL.getInsSearch(insInfo);
            insInfo.InsID = Convert.ToInt32(insData.Rows[0][0].ToString());
            insStatus = insDAL.Delete_InsInfo(insInfo,userID);

            if (insStatus == 1)
            {
                string str = "alert('Deleted Insurance Info Successfully...');";
                ScriptManager.RegisterStartupScript(btnInsDelete, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Failed To Delete Insurance Info...');";
                ScriptManager.RegisterStartupScript(btnInsDelete, typeof(Page), "alert", str, true);
            }
            
            clearTextBoxes();
            btnInsUpdate.Visible = false;
            btnInsSave.Visible = true;
            btnInsDelete.Visible = false;
            txtInsName.ReadOnly = false;
        }
        catch (Exception ex)
        {
            
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnSearchIns_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtSearchIns.Text != "")
                insInfo.InsName = txtSearchIns.Text;
            DataTable insData = insDAL.getInsSearch(insInfo);
            if (insData.Rows.Count > 0)
            {
                insInfo.InsID = Convert.ToInt32(insData.Rows[0][0].ToString());
                insInfo.InsName = insData.Rows[0][1].ToString();
                insInfo.InsNumber = insData.Rows[0][2].ToString();
                insInfo.InsCompany = insData.Rows[0][3].ToString();
                insInfo.InsAddress1 = insData.Rows[0][4].ToString();
                insInfo.InsAddress2 = insData.Rows[0][5].ToString();
                insInfo.InsCity = insData.Rows[0][6].ToString();
                insInfo.InsState = insData.Rows[0][7].ToString();
                insInfo.InsZip = insData.Rows[0][8].ToString();
                insInfo.InsPhone = insData.Rows[0][9].ToString();
                insInfo.InsFax = insData.Rows[0][10].ToString();
                if (insInfo.InsName != "")
                    txtInsName.Text = insInfo.InsName;
                if (insInfo.InsNumber != "")
                    txtInsNumber.Text = insInfo.InsNumber;
                if (insInfo.InsCompany != "")
                    txtInsCompany.Text = insInfo.InsCompany;
                if (insInfo.InsAddress1 != "")
                    txtInsAddress2.Text = insInfo.InsAddress1;
                if (insInfo.InsAddress2 != "")
                    txtInsAddress1.Text = insInfo.InsAddress2;
                if (insInfo.InsCity != "")
                    txtInsCity.Text = insInfo.InsCity;
                if (insInfo.InsState != "")
                    txtInsState.Text = insInfo.InsState;
                if (insInfo.InsZip != "")
                    txtInsZip.Text = insInfo.InsZip;
                if (insInfo.InsPhone != "")
                    txtInsPhone.Text = insInfo.InsPhone;
                if (insInfo.InsFax != "")
                    txtInsFax.Text = insInfo.InsFax;
                btnInsSave.Visible = false;
                txtInsName.ReadOnly = true;
                btnInsUpdate.Visible = true;
                btnInsDelete.Visible = true;
                txtSearchIns.Text = "";
            }
            else
            {
                string str = "alert('No Records Found...');";
                ScriptManager.RegisterStartupScript(btnSearchIns, typeof(Page), "alert", str, true);
                txtSearchIns.Text = "";
            }
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
}
