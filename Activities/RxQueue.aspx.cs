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

public partial class RxQueue : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
   
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            GridViewHelper helper = new GridViewHelper(this.gridRxQueue);
            helper.RegisterGroup("Clinic_Name", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);

           
                Filldata();
                if (!Page.IsPostBack)
                {
                    lblHeading.Text = "Rx Queue - " + DateTime.Now.ToString("MM/dd/yyyy");
                }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        try
        {
            if (groupName == "Clinic_Name")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                
                row.HorizontalAlign = HorizontalAlign.Left;
                
            }
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

            SqlCommand sqlCmd = new SqlCommand("sp_getRxQueue", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_RxDate = sqlCmd.Parameters.Add("@RxDate", SqlDbType.DateTime);
            if (txtRxQueueSearchDate.Text != "")
                par_RxDate.Value = txtRxQueueSearchDate.Text;
            else
                par_RxDate.Value = System.DBNull.Value;

            SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            par_UserID.Value = (string)Session["User"];

            SqlParameter par_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
            par_UserRole.Value = (string)Session["Role"];


            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxQueue = new DataSet();

            sqlDa.Fill(dsRxQueue, "RxQueue");
            gridRxQueue.DataSource = dsRxQueue;
            gridRxQueue.DataBind();
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
                if(txtRxQueueSearchDate.Text!="")
                    Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString() + "&RxDate=" + txtRxQueueSearchDate.Text);
                else
                Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString());
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
            if (e.CommandName == "Payment")
            {
                if (txtRxQueueSearchDate.Text != "")
                    Response.Redirect("RxPayment.aspx?Fac_ID=" + e.CommandArgument.ToString() + "&RxDate=" + txtRxQueueSearchDate.Text);
                else
                    Response.Redirect("RxPayment.aspx?Fac_ID=" + e.CommandArgument.ToString());
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
            if (e.CommandName == "Sample")
            {
                if (txtRxQueueSearchDate.Text != "")
                    Response.Redirect("RxSample.aspx?Fac_ID=" + e.CommandArgument.ToString() + "&RxDate=" + txtRxQueueSearchDate.Text);
                else                    
                    Response.Redirect("RxSample.aspx?Fac_ID=" + e.CommandArgument.ToString());
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
            if (e.CommandName == "PAP")
            {
                if (txtRxQueueSearchDate.Text != "")
                    Response.Redirect("RxPAP.aspx?Fac_ID=" + e.CommandArgument.ToString() + "&RxDate=" + txtRxQueueSearchDate.Text);
                else 
                Response.Redirect("RxPAP.aspx?Fac_ID=" + e.CommandArgument.ToString());
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }


    protected void btnRxQueueSearchDate_Click(object sender, ImageClickEventArgs e)
    {
        Filldata();
        lblHeading.Text = "Rx Queue - " + txtRxQueueSearchDate.Text;
    }
}
