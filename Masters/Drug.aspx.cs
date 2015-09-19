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
using NLog;

public partial class Drug : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
            this.Page.Form.Enctype = "multipart/form-data";
            DTypeID();
        }
        RenderJSArrayWithCliendIds(txtDrugName, txtDTInfo, txtDrugCostIndex,chk_DType,PnlDType);
      
    }

    DrugInfo dInfo = new DrugInfo();
    DrugInfoDAL dInfoDAL = new DrugInfoDAL();
    string DStatus;
    protected System.Web.UI.HtmlControls.HtmlInputFile UploadFile;
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
        
    public void clearDrugTB()
    {
        try
        {
            txtDrugName.Text = string.Empty;
            txtDrugCostIndex.Text = string.Empty;
            txtSearchDrug.Text = string.Empty;
            ddlDrugType_ID.ClearSelection();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDrugNames(string prefixText, int count, string contextKey)
    {
        List<string> Drug_List = new List<string>();
        if (contextKey == "0")
        {
            Drug_List.Clear();
            DrugInfoDAL DInfoDAL = new DrugInfoDAL();
            DataTable dtFac = DInfoDAL.get_DrugNames(prefixText, count, contextKey);
            foreach (DataRow dr in dtFac.Rows)
            {
                Drug_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Drug_Name"].ToString(), dr[0].ToString()));
            }
        }

        return Drug_List.ToArray();
    }

   
    protected void btnDInfoSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            dInfo.DrugName = txtDrugName.Text;
            dInfo.DrugType = ddlDrugType_ID.SelectedItem.Text;
            dInfo.DCostIndex = txtDrugCostIndex.Text;
            DStatus = dInfoDAL.Ins_DrugInfo(dInfo, userID);
            string str = "alert('" + DStatus + "');";
            ScriptManager.RegisterStartupScript(btnDInfoSave, typeof(Page), "alert", str, true);
            clearDrugTB();
        }
        catch (Exception ex)
        {
            DStatus = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnSearchDrug_Click(object sender, ImageClickEventArgs e)
    {
        SearchBasedOnDrugName();
    }

    public void SearchBasedOnDrugName()
    {
        try
        {
            dInfo.DrugName = txtSearchDrug.Text;
            DataTable DrugData = dInfoDAL.getDrugSearch(dInfo);
            if (DrugData.Rows.Count > 0)
            {
                dInfo.DrugID = Convert.ToInt32(DrugData.Rows[0][0].ToString());
                dInfo.DrugName = DrugData.Rows[0][1].ToString();
                dInfo.DCostIndex = DrugData.Rows[0][2].ToString();
                string DTID = DrugData.Rows[0][3].ToString();

                if (DTID != "")
                    dInfo.DrugTypeID = Convert.ToInt32(DTID);
                ddlDrugType_ID.SelectedValue = dInfoDAL.getDrugType(dInfo);

                if (dInfo.DrugName != "")
                    txtDrugName.Text = dInfo.DrugName;
                if (dInfo.DCostIndex != "")
                    txtDrugCostIndex.Text = dInfo.DCostIndex;
                btnDInfoSave.Visible = false;
                btnDInfoDelete.Visible = true;
                btnDInfoUpdate.Visible = true;
                txtDrugName.ReadOnly = true;
                //DTypeID();
            }
            else
            {
                string str = "alert('No Records Found...');";
                ScriptManager.RegisterStartupScript(btnSearchDrug, typeof(Page), "alert", str, true);
                txtSearchDrug.Text = "";
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnDInfoUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            dInfo.DrugName = txtDrugName.Text;
            DataTable DrugData = dInfoDAL.getDrugSearch(dInfo);
            dInfo.DrugID = Convert.ToInt32(DrugData.Rows[0][0].ToString());

            dInfo.DrugType = ddlDrugType_ID.SelectedItem.Text;
            dInfo.DCostIndex = txtDrugCostIndex.Text;

            DStatus = dInfoDAL.update_DrugInfo(dInfo, userID);
            string str = "alert('" + DStatus + "');";
            ScriptManager.RegisterStartupScript(btnDInfoUpdate, typeof(Page), "alert", str, true);

            clearDrugTB();
            btnDInfoDelete.Visible = false;
            btnDInfoUpdate.Visible = false;
            btnDInfoSave.Visible = true;
            txtDrugName.ReadOnly = false;

        }
        catch (Exception ex)
        {
            DStatus = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnInfoCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearDrugTB();
            btnDInfoSave.Visible = true;
            btnDInfoDelete.Visible = false;
            btnDInfoUpdate.Visible = false;
            txtDrugName.ReadOnly = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void btnDInfoDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            dInfo.DrugName = txtDrugName.Text;
            DataTable DrugData = dInfoDAL.getDrugSearch(dInfo);
            dInfo.DrugID = Convert.ToInt32(DrugData.Rows[0][0].ToString());

            int flag=dInfoDAL.delete_DrugInfo(dInfo, userID);
            if (flag == 0)
            {
                string str = "alert('Can not delete drug info..!');";
                ScriptManager.RegisterStartupScript(btnDInfoDelete, typeof(Page), "alert", str, true);
            }
            else
            {
                string str = "alert('Drug information deleted successfully..!');";
                ScriptManager.RegisterStartupScript(btnDInfoDelete, typeof(Page), "alert", str, true);
            }
            clearDrugTB();
            btnDInfoDelete.Visible = false;
            btnDInfoUpdate.Visible = false;
            btnDInfoSave.Visible = true;
            txtDrugName.ReadOnly = false;           
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    public void DTypeID()
    {
        try
        {
            ddlDrugType_ID.DataTextField = "DrugType";
            ddlDrugType_ID.DataValueField = "DrugType";
            ddlDrugType_ID.DataSource = (DataSet)dInfoDAL.DTypeID();
            ddlDrugType_ID.DataBind();
            updatePanelDrugInfo.Update();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnDTInfoSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dInfo.DrugType = txtDTInfo.Text;
            int flag = dInfoDAL.Ins_DrugTypeInfo(dInfo,(string)Session["User"]);
            if (flag == 1)
            {
                string str = "alert('Added New Drug Type...');";
                ScriptManager.RegisterStartupScript(btnDTInfoSave, typeof(Page), "alert", str, true);
                txtDTInfo.Text = string.Empty;
                DTypeID();
                PnlDType.Visible = false;
                chk_DType.Checked = false;
            }
            else
            {
                string str = "alert('Failed To Add New Drug Type...');";
                ScriptManager.RegisterStartupScript(btnDTInfoSave, typeof(Page), "alert", str, true);
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

    protected void chk_DType_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chk_DType.Checked == true)
                PnlDType.Visible = true;
            else
                PnlDType.Visible = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
}
