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

public partial class Clinics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if ((string)Session["Role"] == "C")
        {
            Server.Transfer("../Patient/AccessDenied.aspx");

        }
        RenderJSArrayWithCliendIds(txtClinicName);
    }
   
    Clinic clinic = new Clinic();
    ClinicDAL cDAL = new ClinicDAL();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetClinic(string prefixText, int count, string contextKey)
    {
        List<string> Clinic_List = new List<string>();
        ClinicDAL Clinic_Info = new ClinicDAL();
        if (contextKey == "0")
        {
            Clinic_List.Clear();
            DataTable dtclinic = Clinic_Info.get_ClinicNames(prefixText, count, contextKey);
            foreach (DataRow dr in dtclinic.Rows)
            {
                Clinic_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Clinic_Name"].ToString(), dr[0].ToString()));
            }
        }

        return Clinic_List.ToArray();
    }
    protected void btnClinicSave_Click(object sender, ImageClickEventArgs e)
    {
       
        string insStatus;
        try
        {
           string userID = (string)Session["User"];
            clinic.ClinicName = txtClinicName.Text;
            insStatus = cDAL.Ins_Clinic(clinic, userID);
            string str = "alert('" + insStatus + "');";
            ScriptManager.RegisterStartupScript(btnClinicSave, typeof(Page), "alert", str, true);
            txtClinicName.Text = "";
        }
        catch (Exception ex)
        {
            insStatus = ex.Message;
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnClinicDelete_Click(object sender, ImageClickEventArgs e)
    {
        string insStatus;
        try
        {
            clinic.ClinicName = txtClinicName.Text;
            clinic.ClinicID = cDAL.getClinicID(clinic);
            insStatus = cDAL.Delete_Clinic(clinic);
            string str = "alert('Deleted successfully');";
            ScriptManager.RegisterStartupScript(btnClinicDelete, typeof(Page), "alert", str, true);
            txtClinicName.Text = "";
        }
        catch (Exception ex)
        {
            insStatus = ex.Message;
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
