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
using Adio.UALog;
using NLog;
public partial class MedIssueQueue : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            if (!Page.IsPostBack)
            {
                Filldata();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        objNLog.Info("Event Started..");
        try
        {

            if (groupName == "Clinic_Name")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            }
            if (groupName == "Facility_Name")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;Location :&nbsp;" + row.Cells[0].Text;
            }
            if (groupName == "doctorName")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "Doctor :&nbsp;" + row.Cells[0].Text;
            }
            row.HorizontalAlign = HorizontalAlign.Left;
            if (groupName == "Rx_Date")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "Refills for " + row.Cells[0].Text;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }


    protected void Filldata()
    {
        objNLog.Info("Function Started..");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

            SqlCommand sqlCmd = new SqlCommand("sp_getRxMedRequest", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_UserID.Value = (string)Session["User"];



            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxQueue = new DataSet();

            sqlDa.Fill(dsRxQueue);

            gridRxMedIssue.DataSource = dsRxQueue.Tables[1];
            gridRxMedIssue.DataBind();

        }

        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void btnMedIssueSave_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        SqlConnection sqlCon = new SqlConnection(conStr);
        string userID = (string)Session["User"];

        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_Update_CallLog", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_IssueResponse = sqlCmd.Parameters.Add("@IssueResponse", SqlDbType.VarChar, 1000);
            sp_IssueResponse.Value = txtMedIssueComment.Text;

            SqlParameter sp_CallID = sqlCmd.Parameters.Add("@CallID", SqlDbType.Int);
            sp_CallID.Value = int.Parse(hfCallID.Value);

            //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
            //sqlCmd = new SqlCommand("Update  [Call_Log] SET Issue_Response='" + txtMedIssueComment.Text + "' where [Call_ID] ='" + hfCallID.Value + "'", sqlCon);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            objUALog.LogUserActivity(conStr, userID, "Updated Med Issue with the [Call_ID] = " + hfCallID.Value.ToString(), "Call_Log",0);

            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        objNLog.Info("Event Completed..");
    }

 
    protected void gridRxMedIssue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            Label lbl;
            if (e.CommandName == "Action")
            {

                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);

                lbl = (Label)gridRxMedIssue.Rows[intRowIndex].FindControl("lblDesc");

                lblMedIssue1.Text = lbl.Text;
                hfCallID.Value = (string)e.CommandArgument;

            }
            popMedIssue.Show();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
 
}
