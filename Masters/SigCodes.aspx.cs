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

public partial class Patient_SigCodes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RenderJSArrayWithCliendIds(txtSIGCode, txtSIGName, txtFactor);
    }
   
    SIGCodes sig = new SIGCodes();
    SIGCodesDAL sigDAL = new SIGCodesDAL();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void btnSIGSave_Click(object sender, ImageClickEventArgs e)
    {
        string insStatus;
        string userID = (string)Session["User"];
        try
        {
            lblErrorMsg.Visible = false;
            sig.SIGCode = txtSIGCode.Text;
            sig.SIGName = txtSIGName.Text;
            sig.SIGFactor = txtFactor.Text;
            insStatus = sigDAL.Ins_SIGCodes(sig, userID);
            string str = "alert('" + insStatus + "');";
            ScriptManager.RegisterStartupScript(btnSIGSave, typeof(Page), "alert", str, true);
            clearTextBoxes();

        }
        catch (Exception ex)
        {
            insStatus = ex.Message;
          
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = "Failed to Insert SIG Code. Please Try Again.";
         
            objNLog.Error("Error : " + ex.Message);
        
        }

    }

    public void clearTextBoxes()
    {
        try
        {
            txtSIGCode.Text = string.Empty;
            txtSIGName.Text = string.Empty;
            txtSearchSIG.Text = string.Empty;
            txtFactor.Text = string.Empty;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnSIGCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearTextBoxes();
            btnSIGUpdate.Visible = false;
            btnSIGDelete.Visible = false;
            btnSIGSave.Visible = true;
            lblErrorMsg.Visible = false;
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

    protected void btnSIGUpdate_Click(object sender, ImageClickEventArgs e)
    {
        string insStatus;
        string userID = (string)Session["User"];
        try
        {
            lblErrorMsg.Visible = false;
            sig.SIGCode = txtSearchSIG.Text;
            DataTable sigData = sigDAL.getSIGSearch(sig);
            sig.SIG_ID = Convert.ToInt32(sigData.Rows[0][0].ToString());
            sig.SIGCode = txtSIGCode.Text;
            sig.SIGName = txtSIGName.Text;
            sig.SIGFactor = txtFactor.Text;

            insStatus = sigDAL.Update_SIGCodes(sig,userID);
            string str = "alert('" + insStatus + "');";
            ScriptManager.RegisterStartupScript(btnSIGUpdate, typeof(Page), "alert", str, true);
            clearTextBoxes();
            btnSIGUpdate.Visible = false;
            btnSIGDelete.Visible = false;
            btnSIGSave.Visible = true;

        }
        catch (Exception ex)
        {
            insStatus = ex.Message;
            
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = "Failed to Update SIG Code. Please Try Again.";
            objNLog.Error("Error : " + ex.Message);
        }

    }

    protected void btnSIGDelete_Click(object sender, ImageClickEventArgs e)
    {
        string insStatus;
        string userID = (string)Session["User"];
        try
        {
            lblErrorMsg.Visible = false;
            sig.SIGCode = txtSearchSIG.Text;
            DataTable sigData = sigDAL.getSIGSearch(sig);
            sig.SIG_ID = Convert.ToInt32(sigData.Rows[0][0].ToString());
            insStatus = sigDAL.Delete_SIGCodes(sig,userID);
            string str = "alert('" + insStatus + "');";
            ScriptManager.RegisterStartupScript(btnSIGUpdate, typeof(Page), "alert", str, true);
            clearTextBoxes();
            btnSIGUpdate.Visible = false;
            btnSIGDelete.Visible = false;
            btnSIGSave.Visible = true;


        }
        catch (Exception ex)
        {
            insStatus = ex.Message;
           
            lblErrorMsg.Visible = true;
            objNLog.Error("Error : " + ex.Message);
            lblErrorMsg.Text = "Failed to Delete SIG Code. Please Try Again.";
        }

    }

    protected void btnSearchSIG_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblErrorMsg.Visible = false;
            sig.SIGCode = txtSearchSIG.Text;
            DataTable sigData = sigDAL.getSIGSearch(sig);
            if (sigData.Rows.Count > 0)
            {
                sig.SIG_ID = Convert.ToInt32(sigData.Rows[0][0].ToString());
                sig.SIGCode = sigData.Rows[0][1].ToString();
                sig.SIGName = sigData.Rows[0][2].ToString();
                sig.SIGFactor = sigData.Rows[0][3].ToString();
                txtSIGName.Text = sig.SIGName;
                txtSIGCode.Text = sig.SIGCode;
                txtFactor.Text = sig.SIGFactor;
                btnSIGSave.Visible = false;
                btnSIGUpdate.Visible = true;
                btnSIGDelete.Visible = true;
                //txtSearchSIG.Text = "";
            }
            else
            {
                string str = "alert('No Records Found...');";
                ScriptManager.RegisterStartupScript(btnSearchSIG, typeof(Page), "alert", str, true);
                txtSearchSIG.Text = "";
            }
        }
        catch (Exception ex)
        {
            string serStatus = ex.Message;
            
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = "Error occured while searching SIG Code. Please Try Again.";
            objNLog.Error("Error : " + ex.Message);
        }

    }


    //Added SIG Code Web Method - START
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSIGNames(string prefixText, int count, string contextKey)
    {
        List<string> sig_List = new List<string>();
        PatientInfoDAL pat_Info = new PatientInfoDAL();
        if (contextKey == "0")
        {
            sig_List.Clear();
            DataTable dtsig = pat_Info.get_SIGCodes(prefixText, count, contextKey);
            foreach (DataRow dr in dtsig.Rows)
            {
                sig_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["SIG_Code"].ToString(), dr[0].ToString()));
            }
        }

        return sig_List.ToArray();
    }
    //Added SIG Code Web Method - END
}
