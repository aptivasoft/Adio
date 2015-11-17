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

public partial class Reports_ReportTrouble : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");
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
            try
            {

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

                Filldata(int.Parse(ddlOrganization.SelectedValue), 0);
               
            }
            catch (Exception ex)
            {
                objNLog.Error("Error : " + ex.Message);
            }
        }

    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        objNLog.Info("Event Started...");
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
        objNLog.Info("Event Completed...");
    }
    protected void bindLocation(string clinicID)
    {
        objNLog.Info("Function Started...");

        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getFacilities", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter sp_ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
        sp_ClinicID.Value = clinicID;

        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsFacilityList = new DataSet();
        try
        {
            ddlLocation.DataTextField = "Facility_Name";
            ddlLocation.DataValueField = "Facility_ID";
            sqlDa.Fill(dsFacilityList, "FacilityList");
            ddlLocation.DataSource = dsFacilityList;
            ddlLocation.DataBind();
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue));
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void ddlOrganization_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            ddlOrganization.Items.Insert(0, new ListItem("All Organizations", "0"));

            ddlOrganization.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }


    protected void save_OnClick(object sender, EventArgs e)
    {
        string[] val = HiddenField1.Value.Split(',');
        SurveyQuestions.saveReportComments(Convert.ToInt32(val[0]), Convert.ToChar(val[1]) , txtArea.InnerHtml);
        Response.Redirect("../Reports/ReportTrouble.aspx");
    }

    protected void ddlLocation_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            ddlLocation.Items.Insert(0, new ListItem("All Locations", "0"));
            ddlLocation.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void Filldata(int ClinicID, int FacilityID)
    {
        objNLog.Info("Function Started...");
        try
        {
            //SqlConnection sqlCon = new SqlConnection(conStr);

            //SqlCommand sqlCmd = new SqlCommand("sp_getPatSurveyList", sqlCon);
            //sqlCmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@OrgId", SqlDbType.Int);
            //par_ClinicID.Value = ClinicID;

            //SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@LocId", SqlDbType.Int);
            //par_FacilityID.Value = FacilityID;

            //SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            //DataSet dsRxQueue = new DataSet();

            //sqlDa.Fill(dsRxQueue, "RxQueue");
            gridRxQueue.DataSource = SurveyQuestions.get_ReportDataTable(ClinicID, FacilityID);
            gridRxQueue.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            bindLocation(ddlOrganization.SelectedValue);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue));
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }


    protected void gridRxQueue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            //if (e.CommandName == "Summary")
            //{

            //    Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString());
            //    //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
            //    ////gridNotes.EditIndex = -1;
            //    //FillgridPriscrition();
            //}
            //if (e.CommandName == "Payment")
            //{

            //    Response.Redirect("RxPayment.aspx?Fac_ID=" + e.CommandArgument.ToString());
            //    //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
            //    ////gridNotes.EditIndex = -1;
            //    //FillgridPriscrition();
            //}
            //if (e.CommandName == "Sample")
            //{

            //    Response.Redirect("RxSample.aspx?Fac_ID=" + e.CommandArgument.ToString());
            //    //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
            //    ////gridNotes.EditIndex = -1;
            //    //FillgridPriscrition();
            //}
            //if (e.CommandName == "PAP")
            //{

            //    Response.Redirect("RxPAP.aspx?Fac_ID=" + e.CommandArgument.ToString());
            //    //Pat_Info.delete_patPrescriptionMedInfo(e.CommandArgument.ToString());
            //    ////gridNotes.EditIndex = -1;
            //    //FillgridPriscrition();
            //}



            if (e.CommandName == "Summary")
            {

                Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString());
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

    protected void gridRxQueue_Sorting(Object sender, GridViewSortEventArgs e)
    {
        //// Cancel the sorting operation if the user attempts
        //// to sort by address.
        //if (e.SortExpression == "Address")
        //{
        //    e.Cancel = true;

        //}

    }

    protected void gridRxQueue_Sorted(Object sender, EventArgs e)
    {
        //// Display the sort expression and sort direction.
        //SortInformationLabel.Text = "Sorting by " +
        //  gridRxQueue.SortExpression.ToString() +
        //  " in " + gridRxQueue.SortDirection.ToString() +
        //  " order.";
    }


}