﻿using System;
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
using System.IO;
using System.Data.SqlClient;
using NLog;
using Adio.UALog;

public partial class RxQueueList : System.Web.UI.Page
{
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
    PatientInfoDAL objPat_Info = new PatientInfoDAL();
    string conStr = ConfigurationManager.AppSettings["conStr"];
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
            ddlStatus.DataBind();
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
            }
            catch (Exception ex)
            {
                objNLog.Error("Error : " + ex.Message);
            }
            if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
            {
                ddlRxType.SelectedValue = "R";
            }
            ddlStatus.SelectedValue = "N";
            
            //helper.ApplyGroupSort();
            txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtLotNum.Text = "";
            txtExpiryDate.Text = "";
            txtQtyProcessed.Text = "";
            txtProcessingComments.Text = "";
            lblProcessingDrug1.Text = "";

        }
        GridViewHelper helper = new GridViewHelper(this.gridRxQueueList);
        string[] group = { "DoctorName", "PatientName" };
        //helper.RegisterGroup(group, true, true);
        helper.RegisterGroup("Clinic_Name", true, true);
        helper.RegisterGroup("Facility_Name", true, true);
        helper.RegisterGroup(group, true, true);

        //helper.RegisterGroup("DoctorName", true, true);
        //helper.RegisterGroup("PatientName", true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
          Filldata(int.Parse(ddlOrganization.SelectedValue), 0, txtDate.Text);
       
        lblHeading.Text = "Rx Queue List - " + txtDate.Text;
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
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected  void ddlOrganization_DataBound( object sender, EventArgs e )
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

    protected  void ddlLocation_DataBound( object sender, EventArgs e )
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

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        try
        {
            if (groupName == "Clinic_Name")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                //row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            }
            if (groupName == "Facility_Name")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;Location :&nbsp;" + row.Cells[0].Text;
            }
            if (groupName == "DoctorName")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;Doctor :&nbsp;" + row.Cells[0].Text;
            }
            if (groupName == "PatientName")
            {

                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Patient :&nbsp;" + row.Cells[0].Text;
            }
            if (groupName != "Facility_Name" && groupName != "Clinic_Name")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                //row.ForeColor = System.Drawing.Color.Blue;
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + row.Cells[0].Text;
            }
            //row.HorizontalAlign = HorizontalAlign.Left;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    
    protected void Filldata(int ClinicID,int FacilityID,string date)
    {
        objNLog.Info("Function Started...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

            SqlCommand sqlCmd = new SqlCommand("sp_getRxQueueList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ClinicID;
            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = FacilityID;
            SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            par_Date.Value = DateTime.Parse(date);
            SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            par_RxType.Value = ddlRxType.SelectedValue;

            SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
            par_RxStatus.Value = ddlStatus.SelectedValue;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxQueueList = new DataSet();

            sqlDa.Fill(dsRxQueueList, "RxQueueList");
            gridRxQueueList.DataSource = dsRxQueueList;
            gridRxQueueList.DataBind();
            DataTable dt = dsRxQueueList.Tables[0];

            if (ddlRxType.SelectedValue == "S" || ddlRxType.SelectedValue == "P")
                gridRxQueueList.Columns[10].Visible = true;
            else
                gridRxQueueList.Columns[10].Visible = false;

            dt.DefaultView.RowFilter = "status='Transmitted'";
            if (dt.DefaultView.Count > 0 && ((string)Session["Role"] == "P" || (string)Session["Role"] == "T"))
            {
                btnPrint.Visible = true;

            }
            else
            {
                btnPrint.Visible = false;

            }
            if (dt.Rows.Count > 0 && ((string)Session["Role"] == "A" || (string)Session["Role"] == "M" || (string)Session["Role"] == "E"))
                btnAdminPrint.Visible = true;
            else
                btnAdminPrint.Visible = false;

            if (ddlStatus.SelectedValue == "O")
            {
                btnPrint.Visible = false;
                btnAdminPrint.Visible = false;
            }
            if ((string)Session["Role"] == "C" && dt.Rows.Count > 0 && (ddlRxType.SelectedValue == "S" || ddlRxType.SelectedValue == "P"))
            {
                btnPrint.Visible = true;
            }


        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void gridRxQueueList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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
            if (e.CommandName == "Processing")
            {
                SqlConnection sqlCon = new SqlConnection(conStr);

                
                SqlCommand sqlCmd = new SqlCommand("sp_getSampleDrugInfo", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par_Rx_drugId = sqlCmd.Parameters.Add("@RxDrugid", SqlDbType.Int);
                par_Rx_drugId.Value = e.CommandArgument.ToString();
                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                objNLog.Error("Processing : RxItemID " + e.CommandArgument.ToString());
                hfRXItemID.Value = e.CommandArgument.ToString();
                hfPatId.Value = ds.Tables[0].Rows[0]["Pat_ID"].ToString();
                objNLog.Error("Processing : PatID " + hfPatId.Value);
                txtLotNum.Text = "";
                txtExpiryDate.Text = "";
                txtQtyProcessed.Text = "";
                txtProcessingComments.Text = "";
                if (ds.Tables[0].Rows[0][1].ToString() == "S")
                {
                    if (int.Parse(ds.Tables[0].Rows[0][3].ToString()) > 2)
                        lblProcesingAlert.Visible = true;
                    else
                        lblProcesingAlert.Visible = false;
                }
                lblProcessingDrug1.Text = ds.Tables[0].Rows[0][0].ToString();
                lblQtyinStock1.Text = ds.Tables[0].Rows[0][2].ToString();
                if (lblQtyinStock1.Text == "" || lblQtyinStock1.Text == "0")
                {
                    lblQtyinStock1.Text = "No Stock";
                    btn_Process_Save.Enabled = false;
                }
                else
                {
                    btn_Process_Save.Enabled = true;
                }
                rbtnProcessingType.SelectedValue = ds.Tables[0].Rows[0][1].ToString();
                rbtnProcessingType.Enabled = false;
                MPE_SamplePAPProcessing.Show();
                 
                //Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    // Samples/PAP Processing  Save Button
    protected void btn_Process_Save_Click(object sender, EventArgs e)
    {
        objNLog.Info("btn_Process_Save_Click (RxSample) Event Started...");
        int i, j;
        i = int.Parse(lblQtyinStock1.Text);
        j = int.Parse(txtQtyProcessed.Text);

        if (j > i)
        {
            string str = "alert('Quantity removed can not be greater than available stock!');";
            ScriptManager.RegisterStartupScript(btn_Process_Save, typeof(Page), "alert", str, true);
            MPE_SamplePAPProcessing.Show();
        }
        else
        {

            //SqlConnection sqlCon = new SqlConnection(conStr);

            try
            {
                //SqlCommand sqlCmd;

                string userID = (string)Session["User"];

                //int flag = 0;
                //string sqlQuery = "";

                //if (rbtnProcessingType.SelectedValue == "S")
                //{
                //    sqlQuery = "Insert INTO Drug_Inventory   ([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],Pat_ID,Facility_ID,Lot_Num,Expiry_Date,LastModified,LastModifiedBy,Clinic_ID)    VALUES ('"
                //   + rbtnProcessingType.SelectedValue + "','R',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + lblProcessingDrug1.Text + "'),'-" + txtQtyProcessed.Text + "','" + txtProcessingComments.Text + "','Tablet','" + hfPatId.Value.ToString() + "',(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hfPatId.Value.ToString() + "),'" + txtLotNum.Text + "','" + txtExpiryDate.Text + "', getdate(),'" + Session["User"] + "', (select Clinic_ID from Facility_Info where Facility_ID=(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hfPatId.Value.ToString() + "))) ";
                //    flag = 1;
                //}
                //else
                //{
                //    sqlQuery = "Insert INTO Drug_Inventory   ([Inv_Group_Code],[Inv_Trans_Code],[Inv_Date],[Drug_ID],[Qty],[Inv_Desc],[Drug_Form],Pat_ID,Facility_ID,LastModified,LastModifiedBy, Clinic_ID)    VALUES ('"
                //    + rbtnProcessingType.SelectedValue + "','R',getdate(),(select Drug_ID from Drug_Info where Drug_Name='" + lblProcessingDrug1.Text + "'),'-" + txtQtyProcessed.Text + "','" + txtProcessingComments.Text + "','Tablet'," + hfPatId.Value.ToString() + ",(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hfPatId.Value.ToString() + "),getdate(),'" + Session["User"] + "', (select Clinic_ID from Facility_Info where Facility_ID=(select Patient_Info.Facility_ID from Patient_Info where Patient_Info.Pat_ID=" + hfPatId.Value.ToString() + "))) ";
                //    flag = 1;
                //}
                //if (ddlProcessingStatus.SelectedValue != "R")
                //{
                //    sqlQuery = sqlQuery + "Update Rx_Drug_Info SET Rx_Status='D' where Rx_ItemID=" + hfRXItemID.Value;
                //    flag = 2;
                //}
                //sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                //sqlCon.Open();
                //sqlCmd.ExecuteNonQuery();

                //if (flag == 1)
                //    objUALog.LogUserActivity(conStr, userID, "Inserted New Record while processing Samples.", "Drug_Inventory", 0);
                //else
                //    if (flag == 2)
                //        objUALog.LogUserActivity(conStr, userID, "Updated Rx Drug Info while processing Samples. with Rx_ItemID=" + hfRXItemID.Value, "Rx_Drug_Info", 0);
                objPat_Info.Set_Drug_Inventory(char.Parse(rbtnProcessingType.SelectedValue), lblProcessingDrug1.Text, txtQtyProcessed.Text, txtProcessingComments.Text, int.Parse(hfPatId.Value.ToString()), txtLotNum.Text, txtExpiryDate.Text, userID);

                if (ddlProcessingStatus.SelectedValue != "R")
                {
                    objPat_Info.Update_Drug_Info(int.Parse(hfRXItemID.Value), userID);
                }
            }
            catch (Exception ex)
            {
                objNLog.Error("Error : " + ex.Message);
            }
            //finally
            //{
            //    sqlCon.Close();
            //}
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
        }
        objNLog.Info("Event Completed...");
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
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void btnQueueList_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..." + ddlRxType.SelectedValue);
        try
        {      
            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    public bool display_link(string type, string status)
    {
        //check username here and return a bool
        if (type != "Regular" && (status == "Transmitted" || status == "Received"))
        {
            //if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")

              return true;
            //else
             //   return false;
        }
        else
        {
            return false;
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            if ((ddlOrganization.SelectedIndex!=0) || (ddlOrganization.SelectedIndex==0 && (string)Session["Role"] == "C"))
            {
                string userID = (string)Session["User"];
                string str = "printWin = window.open('', 'RxPrint', 'scrollbars=1,menubar=1,resizable=1');"


                    + "if (printWin != null) "
                    + "{"
                    + "printWin.document.open();"
                    + @"printWin.document.write(""" + getformat() + @""");"
                    + "printWin.document.close();"
                    + "printWin.print();}";
                ScriptManager.RegisterStartupScript(btnPrint, typeof(Page), "alert", str, true);
                if (((string)Session["Role"] == "P" || (string)Session["Role"] == "T") ||
                      (string)Session["Role"] == "C" && (ddlRxType.SelectedValue == "S" || ddlRxType.SelectedValue == "P" ))
                {

                    string strConfirm = "document.getElementById('ctl00_ContentPlaceHolder1_btnConfirm').click();"
                                + "function needConfirm(){if (confirm('Is It Ok to Clear the Queue')) "
                                + "{"
                                + "document.getElementById('ctl00_ContentPlaceHolder1_hidConfirm').click();return true;"
                                + "}"
                                + "else"
                                + "{"
                                + "return false;"
                                + "}"
                                + "}";

                    ScriptManager.RegisterStartupScript(btnPrint, typeof(Page), "alert1", strConfirm, true);
           
                    
                }

                        
                   
            }
            else
            {
                string strSelectOrg = "alert('Please select an organization to print..!');";
                ScriptManager.RegisterStartupScript(btnPrint, typeof(Page), "alert", strSelectOrg, true);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
        }


    protected string getformat()
    {
        objNLog.Info("Function Started...");
        Boolean PageSkipped = false;
        string str = "";
        string RxDName = "";
        string fontColor = "#000000";
        try
        {
        SqlConnection sqlCon = new SqlConnection(conStr);
        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
        SqlCommand sqlCmd = new SqlCommand("sp_getRxQueueList", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
        par_ClinicID.Value = ddlOrganization.SelectedValue;
        SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
        par_FacilityID.Value = ddlLocation.SelectedValue;
        SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
        par_Date.Value = DateTime.Parse(txtDate.Text);
        SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
        // ddlRxType.SelectedValue;
        SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
      
        if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
        {
            par_RxType.Value = 'R';
            par_RxStatus.Value = 'N';
        }
        else
        {
            par_RxType.Value = ddlRxType.SelectedValue ;
            par_RxStatus.Value = ddlStatus.SelectedValue  ;
        }
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsRxQueueList = new DataSet();

        txtPrintTime.Text = DateTime.Now.TimeOfDay.ToString();
        
            sqlDa.Fill(dsRxQueueList, "RxQueueList");
           
            if (dsRxQueueList.Tables[0].Rows.Count > 0)
            {

                DataView dv = new DataView(dsRxQueueList.Tables[0]);
                dv.Sort = "PatientName";
                string patientName = "";
                int j = 0;
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (patientName != (string)dv.Table.Rows[i]["PatientName"])
                    {
                        
                        j = 0;
                        if (patientName != "")
                        {
                            if (PageSkipped )
                                str = str + "</table> ";
                            else
                                str = str + "</table> <H5 style='page-break-before:always'>&nbsp;</H5> ";
                        }
                        else
                            str = str + "<H5>&nbsp;</H5>";
                        
                        PageSkipped = false;
                        str = str + "<table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'>"
                                  + "<tr>"
                                  + "<td width='50%' align='left'>"
                                  + "<B>Date:</B>" 
                                  + dv.Table.Rows[i]["RxDate"].ToString() 
                                  + "</td>"
                                  + "<td width='50%' align='right'>"
                                  + "<B>Entered by: </B> " 
                                  + dv.Table.Rows[i]["User"].ToString() 
                                  + " </td>"
                                  + "</tr>";
                        
                        patientName = (string)dv.Table.Rows[i]["PatientName"];
                        
                        str = str + "<tr>"
                                  + "<td  align='left' colspan='2'>&nbsp;</td>"
                                  + "</tr>"
                                  + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                                  + "<tr>"
                                  + "<td align='left' colspan='2'>&nbsp;</td>"
                                  + "</tr>"
                                  + "<tr>"
                                  + "<td  align='left' colspan='2'>"
                                  + "<B>Clinic/Location:&nbsp;&nbsp;</B> " 
                                  + dv.Table.Rows[i]["Clinic_Name"].ToString() 
                                  + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() 
                                  + "</td>"
                                  + "</tr>"
                                  + "<tr>"
                                  + "<td align='left' colspan='2'>&nbsp;</td>"
                                  + "</tr>"
                                  + getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString());

                    }

                    if (j % 4 == 0 && j != 0)
                    {


                        str = str + "</table><H5 style='page-break-before:always'></H5>"
                             + "<table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'> <tr><td width='50%' align='left'><B>Date:</B>" + dv.Table.Rows[i]["RxDate"].ToString() + "</td><td width='50%' align='right'> <B>Entered by:</B>  " + dv.Table.Rows[i]["User"].ToString() + " </td></tr>";

                        str = str + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                      + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + "<tr><td  align='left' colspan='2'> <B>Clinic/Location:&nbsp;&nbsp;</B> " + dv.Table.Rows[i]["Clinic_Name"].ToString() + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() + "</td></tr>"
                      + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString());

                    } else
                    if (PageSkipped == true)
                    {
                        str = str + "<tr><td width='50%' align='left'><B> Date: </B>" + dv.Table.Rows[i]["RxDate"].ToString() + "</td><td width='50%' align='right'> <B>Entered by: </B> " + dv.Table.Rows[i]["User"].ToString() + " </td></tr>"
                                + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                                + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                                + "<tr><td  align='left' colspan='2'> <B>Clinic/Location:&nbsp;&nbsp;</B> " + dv.Table.Rows[i]["Clinic_Name"].ToString() + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() + "</td></tr>"
                                + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                                + getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString());
                        PageSkipped = false;
                    }

                    j = j + 1;

                    if (dv.Table.Rows[i]["RxDate"].ToString() == dv.Table.Rows[i]["RxFillDate"].ToString())
                        RxDName = dv.Table.Rows[i]["Rx_DrugName"].ToString();
                    else
                        RxDName = dv.Table.Rows[i]["Rx_DrugName"].ToString() + "<B> (Fill Date: " + dv.Table.Rows[i]["RxFillDate"].ToString() + ")</B>";

                    

                    str = str + "<tr>"
                              + "<td  align='left' colspan='2' style='color:" + fontColor + "'>&nbsp;</td>"
                              + "</tr>"
                              + "<tr>"
                              + "<td width='50%' align='left' style='color:" + fontColor + "'>"
                              + "<B>"
                              + j.ToString()
                              + ".&nbsp;DRUG NAME :&nbsp;&nbsp;"
                              + "</B>"
                              + RxDName
                              + "</td>"
                              + "<td width='50%' align='left' style='color:" + fontColor + "'> &nbsp; "
                              + "<B>QTY :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_Qty"].ToString()
                              + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                              + "<B>Refills :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_Refills"].ToString()
                              + "</td>"
                              + "</tr>"
                              + "<tr>"
                              + "<td width='50%' align='left' style='color:" + fontColor + "'>"
                              + "<B>&nbsp;&nbsp;&nbsp; SIG :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_SIG"].ToString()
                              + "</td>"
                              + "<td width='50%' align='right' style='color:" + fontColor + "'> &nbsp; </td>"
                              + "</tr>"
                              + "<tr>"
                              + "<td COLSPAN=2 align='left'  style='color:" + fontColor + "'>"
                              + "<B>&nbsp;&nbsp;&nbsp; Comments :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_Comments"].ToString().Replace("\"", "'").Replace("\r\n", "<p>")
                              + "</td>"
                              + "</tr>";
                    if (dv.Table.Rows[i]["status"].ToString().Equals("Discontinued"))
                    {
                        str = str + "<tr>"
                          + "<td COLSPAN=2 align='left'  style='font-size:20px;color:" + fontColor + "'>"
                          + "<B>Rx Status :&nbsp;&nbsp;"
                          + "<U>" + dv.Table.Rows[i]["status"].ToString().ToUpper() 
                          + "</U></B>"
                          + "</td>"
                          + "</tr>";
                    }


                    if (dv.Table.Rows[i]["Rx_Doc"] != DBNull.Value)
                    {
                        byte[] rxDoc = (byte[])dv.Table.Rows[i]["Rx_Doc"];
                        System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(rxDoc);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);

                        int imgH = image.Height;
                        int imgW = image.Width;

                        string dirPath = MapPath(".") + "\\" + "RxDocuments";
                        string imgFile = dv.Table.Rows[i]["Rx_ItemID"].ToString() + ".Gif";

                        if (Directory.Exists(dirPath))
                        {
                            image.Save(dirPath + "\\" + imgFile, System.Drawing.Imaging.ImageFormat.Gif);
                        }
                        else
                        {
                            Directory.CreateDirectory(dirPath);
                            image.Save(dirPath + "\\" + imgFile, System.Drawing.Imaging.ImageFormat.Gif);
                        }
                        memoryStream.Close();
                        if (imgH > 550)
                        {
                            if (imgH > 1400 || imgW > 1200)
                                str = str + "<tr><td colspan=2><H5 style='page-break-before:always'><img src='RxDocuments/" + imgFile + "' height=1000 width=800 alt=''/></H5></td></tr>";
                            else   
                                str = str + "<tr><td colspan=2><H5 style='page-break-before:always'><img src='RxDocuments/" + imgFile + "' alt=''/></H5></td></tr>";

                            str = str + "<tr><td colspan=2><H5 style='page-break-before:always'></H5></td></tr>"; // Skip the page again after printing the image
                        }
                        else  // Try printing the image on the same page
                            str = str + "<tr><td colspan=2><H5 style='page-break-after:always'><img src='RxDocuments/" + imgFile + "' alt=''/></H5></td></tr>";
                        PageSkipped = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
        return str;
    }

    protected string getPat_Printformat(string Pat_ID)
    {
        objNLog.Info("Function Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
        SqlCommand sqlCmd = new SqlCommand("sp_getPatientInfo", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter par_Pat_ID = sqlCmd.Parameters.Add("@pat_ID", SqlDbType.Int);
        par_Pat_ID.Value = Pat_ID;
        SqlParameter par_Flag = sqlCmd.Parameters.Add("@printStatus", SqlDbType.Int);
        par_Flag.Value = "1";
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsRxQueueList = new DataSet();
        string str = "";
        try
        {
            sqlDa.Fill(dsRxQueueList, "RxQueueList");

            if (dsRxQueueList.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(dsRxQueueList.Tables[0]);
                string address = dv.Table.Rows[0]["Pat_Ship_Address1"].ToString() + " " + dv.Table.Rows[0]["Pat_Ship_Address2"].ToString();
                if (address.Trim() != "")
                    address = address + ", " + dv.Table.Rows[0]["Pat_Ship_City"].ToString();
                else
                    address = dv.Table.Rows[0]["Pat_Ship_City"].ToString();

                if (address.Trim() != "")
                    address = address + ", " + dv.Table.Rows[0]["Pat_Ship_State"].ToString();
                else
                    address = dv.Table.Rows[0]["Pat_Ship_State"].ToString();

                address = address + " " + dv.Table.Rows[0]["Pat_Ship_Zip"].ToString();
                str = str + "<tr>"
                          + "<td colspan='2'>"
                          + "<table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'>"
                          + "<tr>"
                          + "<td width='45%' align='left'>"
                          + "<B>Patient:&nbsp;&nbsp;</B>" 
                          + dv.Table.Rows[0]["Pat_FName"].ToString() 
                          + " " 
                          + dv.Table.Rows[0]["Pat_LName"].ToString()
                          + "</td>"
                          + "<td width='25%' align='left'>"
                          + "<B>Gender:&nbsp;&nbsp;</B> " 
                          + dv.Table.Rows[0]["Pat_Gender"].ToString()
                          + "</td>"
                          + "<td width='30%' align='left'>"
                          + "<B>DOB:&nbsp;&nbsp;</B> " 
                          + dv.Table.Rows[0]["Pat_DOB"].ToString() 
                          + "</td>"
                          + "</tr>"
                          + "<tr>"
                          + "<td  align='left' colspan='2'>"
                          + "<B>Address :&nbsp;&nbsp;</B>" 
                          + address 
                          + "</td><td  align='left'>"
                          + "<B>Phone:&nbsp;&nbsp;</B> " 
                          + dv.Table.Rows[0]["Pat_Phone"].ToString() 
                          + " </td>"
                          + "</tr>"
                          + "<tr>"
                          + "<td  align='left' colspan='3'>&nbsp;</td>"
                          + "</tr>"
                          + "<tr>"
                          + "<td  align='left' >"
                          + "<B>Insurance:&nbsp;&nbsp;</B>" 
                          + dv.Table.Rows[0]["Ins_Name"].ToString()
                          + "</td>"
                          + "<td  align='left'>"
                          + "<B>Policy #:&nbsp;&nbsp;</B> " 
                          + dv.Table.Rows[0]["PI_PolicyID"].ToString()
                          + "</td>"
                          + "<td  align='left'>"
                          + "<B>Group#:&nbsp;&nbsp;</B> " 
                          + dv.Table.Rows[0]["PI_GroupNo"].ToString() 
                          + "</td><td>&nbsp;</td>"
                          + "</tr>"
                          + "<tr>"
                          + "<td  align='left' colspan='3'>&nbsp;</td>"
                          + "</tr>"
                          + "<tr>"
                          + "<td  align='left' colspan='3'>"
                          + "<B>Diagnosis Code:&nbsp;&nbsp;</B>" 
                          + dv.Table.Rows[0]["diag_Codes"].ToString()
                          + "</td>"
                          + "</tr>"
                          + "</table>"
                          + "</td>"
                          + "</tr>";
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");

        return str;
    }

    protected string getDoc_Printformat(string Doc_ID)
    {
        objNLog.Info("Function Started...");
        string str = "";

        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
            SqlCommand sqlCmd = new SqlCommand("sp_getDoctor", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par_DocID = sqlCmd.Parameters.Add("@Doc_Id", SqlDbType.Int);
            par_DocID.Value = Doc_ID;
            SqlParameter par_Flag = sqlCmd.Parameters.Add("@printStatus", SqlDbType.Int);
            par_Flag.Value = "1";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxQueueList = new DataSet();
           
            sqlDa.Fill(dsRxQueueList, "RxQueueList");

            if (dsRxQueueList.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(dsRxQueueList.Tables[0]);
                string degree = dv.Table.Rows[0]["Doc_Degree"].ToString();
                if (degree != "")
                    degree = ", " + degree;
                str = str
                    + "<tr><td width='50%' align='left'> <B>Doctor:&nbsp;&nbsp;</B>" + dv.Table.Rows[0]["Doc_FName"].ToString() + " " + dv.Table.Rows[0]["Doc_LName"].ToString()
                    + degree + "</td><td width='50%' align='left'> <B>Phone:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["Doc_CPhone"].ToString() + " </td></tr>"
                + "<tr><td width='50%' align='left'> <B>DEA#:&nbsp;&nbsp;</B>" + dv.Table.Rows[0]["Doc_DeaNo"].ToString()
                + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>NPI#:&nbsp;&nbsp;</B>" + dv.Table.Rows[0]["Doc_NPI"].ToString()
                             + "</td><td width='50%' align='left'><B>Lic No:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["Doc_LicNo"].ToString() + " </td></tr>";



            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

        objNLog.Info("Function Completed...");
        return str;
    }

    protected void ddlStatus_DataBound(object sender, EventArgs e)
    {
        //ddlStatus.Items.Insert(0, new ListItem("All", "A"));
    }

    protected void hidConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            string userID = (string)Session["User"];
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_UpdateRxQueueList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar);
            par_UserID.Value = userID;
            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ddlOrganization.SelectedValue;
            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = ddlLocation.SelectedValue;
            SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            par_Date.Value = DateTime.Parse(txtDate.Text);
            SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            par_RxType.Value = ddlRxType.SelectedValue;// 'R';
            SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
            par_RxStatus.Value = 'N';
            SqlParameter par_prtTime = sqlCmd.Parameters.Add("@prtTime", SqlDbType.VarChar);
            par_prtTime.Value = txtPrintTime.Text;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error: " + ex.Message);
        }
    }
    
}
