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

public partial class Masters_DeletePatient : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            //GridViewHelper helper = new GridViewHelper(this.gridPatientList);

            //helper.RegisterGroup("Clinic_Name", true, true);
            //helper.RegisterGroup("Facility_Name", true, true);

            //helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            if (!Page.IsPostBack)
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("sp_getClinics", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
                sp_UserID.Value = (string)Session["User"];

                SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
                sp_UserRole.Value = (string)Session["Role"];



                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet dsClinicList = new DataSet();


                sqlDa.Fill(dsClinicList, "ClinicList");
                ddlOrganization.DataSource = dsClinicList;
                ddlOrganization.DataTextField = "Clinic_Name";
                ddlOrganization.DataValueField = "Clinic_ID";
                ddlOrganization.DataBind();
                if (dsClinicList.Tables[0].Rows.Count < 2)
                {
                    ddlOrganization.Items.RemoveAt(0);


                }
                else
                {
                    ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));
                    ddlLocation.SelectedIndex = 0;
                }
                ddlOrganization.SelectedIndex = 0;

                bindLocation(ddlOrganization.SelectedValue);




                //Filldata(int.Parse(ddlOrganization.SelectedValue), 0, 0);
            }
            lblHeading.Text = "Patient List";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void bindLocation(string clinicID)
    {
        objNLog.Info("Function Started..");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getFacilities", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp_ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
            sp_ClinicID.Value = clinicID;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsFacilityList = new DataSet();

            ddlLocation.DataTextField = "Facility_Name";
            ddlLocation.DataValueField = "Facility_ID";
            sqlDa.Fill(dsFacilityList, "FacilityList");
            ddlLocation.DataSource = dsFacilityList;
            ddlLocation.DataBind();
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
    protected void ddlOrganization_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            ddlOrganization.Items.Insert(0, new ListItem("All Organizations", "0"));
            ddlOrganization.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void ddlLocation_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));
            ddlLocation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        objNLog.Info("Event Started..");
        try
        {
            if (groupName == "Clinic_Name")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
            }
            if (groupName == "Facility_Name")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;Location :&nbsp;" + row.Cells[0].Text;
            }
            if (groupName == "DoctorName")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;Doctor :&nbsp;" + row.Cells[0].Text;
            }
            if (groupName == "PatientName")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Patient :&nbsp;" + row.Cells[0].Text;
            }
            if (groupName != "Facility_Name" && groupName != "Clinic_Name")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + row.Cells[0].Text;
            }
            row.HorizontalAlign = HorizontalAlign.Left;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void FilldataNew(int ClinicID, int FacilityID, int DocID, string sType, string sValue)
    {
        objNLog.Info("Function Started..");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getPatientListSearch", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ClinicID;
            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = FacilityID;
            SqlParameter par_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
            par_DocID.Value = 0;

            SqlParameter par_sValue = sqlCmd.Parameters.Add("@sValue", SqlDbType.VarChar, 16);
            par_sValue.Value = sValue;

            SqlParameter par_sType = sqlCmd.Parameters.Add("@sType", SqlDbType.VarChar, 16);
            par_sType.Value = sType;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsPatientList = new DataSet();

            sqlDa.Fill(dsPatientList, "PatientList");
            gridPatientList.DataSource = dsPatientList;
            gridPatientList.DataBind();
            lblPageCount.Text = "Page " + (gridPatientList.PageIndex + 1) + " Of " + gridPatientList.PageCount.ToString();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void Filldata(int ClinicID, int FacilityID, int DocID)
    {
        objNLog.Info("Function Started..");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getPatientList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ClinicID;
            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = FacilityID;
            SqlParameter par_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
            par_DocID.Value = 0;

            SqlParameter usr_Role = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
            usr_Role.Value = char.Parse((string)Session["Role"]);

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsPatientList = new DataSet();

            sqlDa.Fill(dsPatientList, "PatientList");
            if (dsPatientList != null)
            {
                if (dsPatientList.Tables.Count > 0)
                {
                    gridPatientList.DataSource = dsPatientList;
                    gridPatientList.DataBind();
                    lblPageCount.Text = "Page " + (gridPatientList.PageIndex + 1) + " Of " + gridPatientList.PageCount.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }





    protected void gridPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            if (e.CommandName == "Summary")
            {

                Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString());
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
            if (e.CommandName == "Payment")
            {

                Response.Redirect("RxPayment.aspx?Fac_ID=" + e.CommandArgument.ToString());
                //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
                ////gridNotes.EditIndex = -1;
                //FillgridPriscrition();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void gridPatientList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            TableCell cell = e.Row.Cells[0];
            cell.ColumnSpan = 7;

            for (int i = 65; i <= (65 + 25); i++)
            {
                LinkButton lb = new LinkButton();

                lb.Text = Char.ConvertFromUtf32(i) + "&nbsp;";
                lb.CommandArgument = Char.ConvertFromUtf32(i);
                lb.CommandName = "AlphaPaging";
                lb.CssClass = "pager";
                cell.Controls.Add(lb);
            }
            LinkButton lb1 = new LinkButton();
            lb1.Text = "&nbsp;[ALL]";
            lb1.CommandName = "AlphaPaging";
            lb1.CssClass = "pager";
            cell.Controls.Add(lb1);
        }

    }

    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            bindLocation(ddlOrganization.SelectedValue);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), 0);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void btnPatList_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), 0);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

     

    protected void lnkbtnView_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            
                //string str = "alert('Provide Search Criteria...');";
                //ScriptManager.RegisterStartupScript(lnkbtnView, typeof(Page), "alert", str, true);
                Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), 0);
             
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            if (int.Parse(ddlOrganization.SelectedValue) == 0 && int.Parse(ddlLocation.SelectedValue) == 0)
                Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), 0);
            else if (int.Parse(ddlOrganization.SelectedValue) > 0 && int.Parse(ddlLocation.SelectedValue) == 0)
                Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), 0);
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }

    protected void gridPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            gridPatientList.PageIndex = e.NewPageIndex;
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), 0);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }
}
