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
using System.Data.SqlClient;
using NLog;

public partial class DrugActivityLog : System.Web.UI.Page
{
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        
    }
   
    protected void Filldata()
    {
        objNLog.Info("Function Started...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getDrugActivityLog", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
        
            SqlParameter par_DrugID = sqlCmd.Parameters.Add("@drugID", SqlDbType.Int);
            par_DrugID.Value = (string) Request.QueryString["drugID"];
            SqlParameter par_type = sqlCmd.Parameters.Add("@type", SqlDbType.Char);
            par_type.Value = (string)Request.QueryString["type"];
            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = (string)Request.QueryString["facID"];

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxQueue = new DataSet();
        
            sqlDa.Fill(dsRxQueue, "RxQueue");
            gridRxQueue.DataSource = dsRxQueue.Tables[0];
            gridRxQueue.DataBind();
            if (dsRxQueue.Tables[1].Rows.Count > 0)
            {
                lblDrug1.Text = dsRxQueue.Tables[1].Rows[0][0].ToString();
                lblFacility1.Text = dsRxQueue.Tables[1].Rows[0][1].ToString();
            }

            lblStrength1.Text = (string)Request.QueryString["form"];
            if ((string)Request.QueryString["type"] == "S")
                lblType1.Text = "Sample";
            else
                lblType1.Text = "PAP";

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }


    protected void gridRxQueue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            if (e.CommandName == "Summary")
            {
                Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Payment")
            {
                Response.Redirect("RxPayment.aspx?Fac_ID=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Sample")
            {

                Response.Redirect("RxSample.aspx?Fac_ID=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "PAP")
            {
                Response.Redirect("RxPAP.aspx?Fac_ID=" + e.CommandArgument.ToString());
            }
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
   
}
