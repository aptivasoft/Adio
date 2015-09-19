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

public partial class RxRequestQueue : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetDoctorNames(string prefixText, int count, string contextKey)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Doc_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Doc_List.Clear();
        DataTable Doc_Names = objPat_Info.get_AllDoctors(prefixText);
        if (Doc_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Doc_Names.Rows)
            {
                Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[1].ToString().Trim() + "," + dr[0].ToString().Trim(), dr[2].ToString()));
            }
        }
        else
        {
            Doc_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Doctor Found...", "0"));
        }
        return Doc_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetMedicationNames(string prefixText, int count)
    {
        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        List<string> Med_List = new List<string>();
        objPat_Info = new PatientInfoDAL();
        Med_List.Clear();
        DataTable Med_Names = objPat_Info.get_MedicationNames(prefixText.Trim());
        if (Med_Names.Rows.Count > 0)
        {
            foreach (DataRow dr in Med_Names.Rows)
            {
                Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString()));
            }
        }
        else
        {
            Med_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Medications Found!", "0"));
        }
        return Med_List.ToArray();
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
                row.Cells[0].Text = "&nbsp;&nbsp;<b>" + row.Cells[0].Text + "</b>"; 
            }
            if (groupName == "Facility_Name")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "&nbsp;&nbsp;<b>Location :&nbsp;" + row.Cells[0].Text+"</b>";
            }
            if (groupName == "doctorName")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "<b>Doctor :&nbsp;" + row.Cells[0].Text + "</b>"; 
            }
            
            if (groupName == "Rx_Date")
            {
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
                row.HorizontalAlign = HorizontalAlign.Left;
                row.Cells[0].Text = "<b>Refills for " + DateTime.Parse(row.Cells[0].Text).ToString("MM/dd/yyyy") + "</b>";
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridRxRequestQueue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            if (e.CommandName == "RXApproval")
            {
                //Response.Redirect("RxApproval.aspx?RxRequestID=" + e.CommandArgument.ToString());
                //string str = "window.open(RxApproval.aspx?RxRequestID=" + e.CommandArgument.ToString() + ");";
                
                GridViewRow selectedRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                int intRowIndex = Convert.ToInt32(selectedRow.RowIndex);
                string lblRxItemID = e.CommandArgument.ToString();
                setcontextkey(lblRxItemID);

                //if ((string)Session["Role"] == "D")
                //{
                //    lblDoctor.Visible = true;
                //    txtDocName.Visible = false;
                //}

                SqlConnection sqlCon = new SqlConnection(conStr);

                string sqlQuery = "SELECT [Rx_Type],[Rx_Type_Description] FROM [Rx_Types]";

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, sqlCon);
                DataSet dsDeliveryModes = new DataSet();
                sqlDa.Fill(dsDeliveryModes);

                rbtnRequestType.DataSource = dsDeliveryModes;
                rbtnRequestType.DataTextField = "Rx_Type_Description";
                rbtnRequestType.DataValueField = "Rx_Type";
                rbtnRequestType.DataBind();
                
                //rbtnRequestType.SelectedIndex = 0;
               
                FillRxData(lblRxItemID);
                mpeRxApproval.Show();
                
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "window.open(\"RxApproval.aspx?RxRequestID=" + e.CommandArgument.ToString() + "\")", true);
                //ScriptManager.RegisterStartupScript(btnQueueList, typeof(Page), "alert", str, true);
            }

            if (e.CommandName == "ReportRxReq")
            {
                string RxStat = e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("-")+1);
                string RxIDs = e.CommandArgument.ToString().Substring(0,e.CommandArgument.ToString().IndexOf("-"));
                if (RxStat == "Pending")
                   ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "window.open(\"ReportRxReq.aspx?RxRequestID="+RxIDs+"\")", true);
                
                //Response.Redirect("ReportRxReq.aspx?RxRequestID=" + RxIDs);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    
    protected void gridRxRequestQueue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridRxRequestQueue.PageIndex = e.NewPageIndex;
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
   
    // Fill Rx Request Queue List 
    protected void Filldata()
    {
        objNLog.Info("Function Started...");
        try
        {
          
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getRxRequestQueue", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            if ((string)Session["Role"] == "D" || (string)Session["Role"] == "N")
            {
                SqlParameter par_User = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
                par_User.Value = (string)Session["User"];
            }

            SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            par_RxType.Value = ddlRxType.SelectedValue;

            SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
            par_RxStatus.Value = ddlStatus.SelectedValue;

            if (rbtnFilterByDate.Checked == true)
            {
                SqlParameter par_RxReqStartDate = sqlCmd.Parameters.Add("@RxReqStartDate", SqlDbType.DateTime);
                if (txtStartDate.Text != "")
                    par_RxReqStartDate.Value = Convert.ToDateTime(txtStartDate.Text);
                else
                    par_RxReqStartDate.Value = DBNull.Value;

                SqlParameter par_RxReqEndDate = sqlCmd.Parameters.Add("@RxReqEndDate", SqlDbType.DateTime);
                if (txtEndDate.Text != "")
                    par_RxReqEndDate.Value = Convert.ToDateTime(txtEndDate.Text);
                else
                    par_RxReqEndDate.Value = DBNull.Value;
            
            }
            if (rbtnFilterByFac.Checked == true)
            {
                SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@ClinicID", SqlDbType.Int);
                par_ClinicID.Value = int.Parse(ddlFilterClinic.SelectedValue);

                SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@FacilityID", SqlDbType.Int);
                par_FacilityID.Value = int.Parse(ddlFilterFacility.SelectedValue);
            }
            if (rbtnFilterByDoc.Checked == true)
            {
                SqlParameter par_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
                if (hidDocID.Value != "")
                    par_DocID.Value = int.Parse(hidDocID.Value);
                else
                    par_DocID.Value = DBNull.Value;
            }
            
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxRequestQueue = new DataSet();

            sqlDa.Fill(dsRxRequestQueue, "RxRequestQueue");

            DataView dv = null;
            dv= new DataView(dsRxRequestQueue.Tables[0]);

            if ((string)Session["Role"] == "T" || (string)Session["Role"] == "P")
            {
                dv.RowFilter = "status='Approved'";
            }
            if (rbtnDoctor.Checked)
            {
                dv.Sort = "doctorName";
            }
            if (rbtnFacility.Checked)
            {
                dv.Sort = "Clinic_Name,Facility_Name,Rx_Date Desc";
            }
            if (rbtnShowAll.Checked)
            {
                dv.Sort = "Rx_Date Desc";
            }
            if (dv.Count > 0)
            {
                gridRxRequestQueue.DataSource = dv;
                gridRxRequestQueue.DataBind();

                lblcount.Text = "<B>Count: " + dv.Count.ToString() + "</B>";
                lblcount.Visible = true;

            }
            else
            {
                gridRxRequestQueue.DataSource = dsRxRequestQueue.Tables[0];
                gridRxRequestQueue.DataBind();

                lblcount.Text = "<B>Count: " + dsRxRequestQueue.Tables[0].Rows.Count.ToString() + "</B>";
                lblcount.Visible = true;

            }
            updateRxQueue.Update();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                sqlDa.Fill(dsClinicList, "ClinicList");
                ddlFilterClinic.DataTextField = "Clinic_Name";
                ddlFilterClinic.DataValueField = "Clinic_ID";
                ddlFilterClinic.DataSource = dsClinicList;
                ddlFilterClinic.DataBind();
                if (dsClinicList.Tables[0].Rows.Count < 2)
                {
                    ddlFilterClinic.Items.RemoveAt(0);
                }
                else
                {
                    ddlFilterFacility.Items.Insert(0, new ListItem("All Locations", "0"));
                    ddlFilterFacility.SelectedIndex = 0;
                }
                ddlFilterClinic.SelectedIndex = 0;
                BindLocation(ddlFilterClinic.SelectedValue);
                
            }
           
                GridViewHelper helper = new GridViewHelper(this.gridRxRequestQueue);

                //if ((string)Session["Role"] == "D" || (string)Session["Role"] == "N")
               
                 if ((string)Session["Role"] == "D")
                 {   
                    lblsortby.Visible = false;
                    rbtnShowAll.Visible = false;
                    rbtnFacility.Visible = false;
                    rbtnDoctor.Visible = false;
                    gridRxRequestQueue.Columns[2].Visible = true;
                    gridRxRequestQueue.Columns[4].Visible = false;
                    helper.RegisterGroup("Rx_Date", true, true);
                   
                    ddlStatus.Enabled = false;
                }
                else
                {
                    if (rbtnDoctor.Checked)
                    {
                        helper.RegisterGroup("doctorName", true, true);
                        gridRxRequestQueue.Columns[2].Visible = true;
                        gridRxRequestQueue.Columns[4].Visible = false;
                       
                    }
                    if (rbtnFacility.Checked)
                    {
                        helper.RegisterGroup("Clinic_Name", true, true);
                        helper.RegisterGroup("Facility_Name", true, true);
                        gridRxRequestQueue.Columns[2].Visible = true;
                        gridRxRequestQueue.Columns[4].Visible = true;
                       
                    }

                    if (rbtnShowAll.Checked)
                    {
                        helper.RegisterGroup("Rx_Date", true, true);
                        gridRxRequestQueue.Columns[2].Visible = false;
                        gridRxRequestQueue.Columns[4].Visible = true;
                       
                    }
            }
                Filldata();
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                helper.ApplyGroupSort();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnQueueList_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void radio_CheckChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            RadioButton rbtn = (RadioButton)sender;
            
            string s = rbtn.ID;
            if (s == rbtnShowAll.ID)
            {

                //Filldata();
                pnlDateSelect.Visible = false;
                //rbtnLMonth.Text= DateTime.Today.AddDays(7).ToString();
            }
            if (s == rbtnFacility.ID)
            {
                //Filldata();
                pnlDateSelect.Visible = false;
                //rbtnLMonth.Text= DateTime.Today.AddDays(7).ToString();
            }
            if (s == rbtnDoctor.ID)
            {
                //Filldata();
                pnlDateSelect.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridRxRequestQueue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //objNLog.Info("Event Started...");
        //try
        //{ 
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        ContentPlaceHolder mpContentPlaceHolder;
        //        mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        //        if (mpContentPlaceHolder != null)
        //        {
        //            Label lb;
        //            lb = (Label)e.Row.Cells[2].FindControl("lblRxDate");
        //            ((Label)e.Row.Cells[2].FindControl("lblRxDate")).Text = DateTime.Parse(lb.Text).ToString("MM/dd/yyyy");
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    objNLog.Error("Error : " + ex.Message);
        //}
        //objNLog.Info("Event Completed...");
    }

  
    protected void rbtnFilterByDate_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnFilterByDate.Checked == true)
        {
            pnlDateSelect.Visible = true;
            pnlFacilitySelect.Visible = false;
            pnlDoctorSelect.Visible = false;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            hidDocID.Value = "";
            Filldata();
        }
    }
    protected void rbtnFilterByFac_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnFilterByFac.Checked == true)
        {
            pnlDateSelect.Visible = false;
            pnlFacilitySelect.Visible = true;
            pnlDoctorSelect.Visible = false;
            ddlFilterClinic.SelectedIndex = 0;
            ddlFilterFacility.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            hidDocID.Value = "";
            Filldata();
        }
    }
    protected void rbtnFilterByDoc_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnFilterByDoc.Checked == true)
        {
            pnlDateSelect.Visible = false;
            pnlFacilitySelect.Visible = false;
            pnlDoctorSelect.Visible = true;
            txtFilterDoctor.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            hidDocID.Value = "";
            Filldata();
        }
    }

    protected void BindLocation(string clinicID)
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
            sqlDa.Fill(dsFacilityList, "FacilityList");
            ddlFilterFacility.DataTextField = "Facility_Name";
            ddlFilterFacility.DataValueField = "Facility_ID";
            ddlFilterFacility.DataSource = dsFacilityList;
            ddlFilterFacility.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void ddlFilterClinic_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            ddlFilterClinic.Items.Insert(0, new ListItem("All Organizations", "0"));
            ddlFilterClinic.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
   
    }
    protected void ddlFilterClinic_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            BindLocation(ddlFilterClinic.SelectedValue);
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void ddlFilterFacility_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void ddlFilterFacility_DataBound(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            ddlFilterFacility.Items.Insert(0, new ListItem("All Locations", "0"));
            ddlFilterFacility.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    
    }

    protected void btnRefreshQueueList_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        PatientInfoDAL Pat_Info = new PatientInfoDAL();
        try
        {
            if (rbtnDecision.SelectedValue == "R")
            {
                UpdateRxMedRequest(hidRxReqID.Value);
            }
            else
            {
                string drugName = txtDrug.Text.Trim();

                if (rbtnDecision.SelectedValue == "A")
                {
                    if (chkSubDrugName.Checked == true)
                    {
                        if (txtSubDrugName.Text != "")
                        {
                            drugName = txtSubDrugName.Text;
                        }
                        else
                        {
                            lblMsg.Text = "Substitute Drug Name Can Not Be Empty..!";
                            lblMsg.Visible = true;
                            mpeRxApproval.Show();
                            Filldata();
                        }
                    }
                
                }
                int DrugID = Pat_Info.Get_MedID(drugName);

                if (DrugID > 0)  // If entered drug name doesn't exist
                {
                    if (txtQuantity.Text == "" || txtQuantity.Text == "0")
                    {
                        lblMsg.Text = "Drug Quantity can not be empty or zero.";
                        lblMsg.Visible = true;
                        mpeRxApproval.Show();
                        Filldata();
                    }
                    else
                    {
                        UpdateRxMedRequest(hidRxReqID.Value);

                    }
                }
                else
                {
                    lblMsg.Text = "Invalid Drug Name - " + drugName;
                    lblMsg.Visible = true;
                    mpeRxApproval.Show();
                    Filldata();
                }
            }
             
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    private void UpdateRxMedRequest(string RxReqID)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_Update_PatientMedRequest", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        try
        {
            string drugName = "";
            if (rbtnDecision.SelectedValue == "A")
            {
                if (chkSubDrugName.Checked == true)
                {
                    drugName = txtSubDrugName.Text;
                }
            }

            SqlParameter sp_MedicationName = sqlCmd.Parameters.Add("@Medicine", SqlDbType.VarChar, 50);
            SqlParameter sp_SubMedicationName = sqlCmd.Parameters.Add("@Old_Medicine", SqlDbType.VarChar, 50);
            if (drugName != "")
            {
                sp_MedicationName.Value = drugName;
                sp_SubMedicationName.Value = txtDrug.Text.Trim();

            }
            else
            {
                sp_MedicationName.Value = txtDrug.Text.Trim();
                sp_SubMedicationName.Value = DBNull.Value;

            }


           
           
            SqlParameter sp_Qty = sqlCmd.Parameters.Add("@Quantity", SqlDbType.Int);
            sp_Qty.Value = Int32.Parse(txtQuantity.Text);

            SqlParameter sp_ReqID = sqlCmd.Parameters.Add("@Rx_Req_ID", SqlDbType.Int);
            sp_ReqID.Value = Int32.Parse(RxReqID);

            SqlParameter sp_SIG = sqlCmd.Parameters.Add("@SIG", SqlDbType.VarChar, 50);
            sp_SIG.Value = txtSIG.Text;

            SqlParameter sp_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            sp_RxType.Value = rbtnRequestType.SelectedValue;

            SqlParameter sp_RxStatus = sqlCmd.Parameters.Add("@RX_Status", SqlDbType.Char, 1);
            sp_RxStatus.Value = rbtnDecision.SelectedValue;

            SqlParameter sp_Comments = sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 2000);
            sp_Comments.Value = txtComments.Text;

            SqlParameter sp_Refills = sqlCmd.Parameters.Add("@RxRefills", SqlDbType.Int);
            sp_Refills.Value = Int32.Parse(ddlRefills.SelectedValue);

            SqlParameter sp_Phrm = sqlCmd.Parameters.Add("@Rx_Phrm", SqlDbType.VarChar, 50);

            if (rbtnPhrm.SelectedValue == "0")
            {
                sp_Phrm.Value = "ADiO Pharmacy";
            }
            else
            {
                sp_Phrm.Value = txtPharmacy.Text;
            }

            SqlParameter sp_User = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_User.Value = (string)Session["User"];

            SqlParameter sp_DocID = sqlCmd.Parameters.Add("@DocID", SqlDbType.Int);
            sp_DocID.Value = Int32.Parse(hidDocID.Value);

            byte[] rxDoc = null;
            if (FileUpRxDoc.PostedFile != null && FileUpRxDoc.PostedFile.FileName != "")
            {
                rxDoc = new byte[FileUpRxDoc.PostedFile.ContentLength];
                HttpPostedFile Image = FileUpRxDoc.PostedFile;
                Image.InputStream.Read(rxDoc, 0, (int)FileUpRxDoc.PostedFile.ContentLength);
            }

            SqlParameter Rx_Doc = sqlCmd.Parameters.Add("@Rx_Doc", SqlDbType.Image);
            if (rxDoc != null)
                Rx_Doc.Value = rxDoc;
            else
                Rx_Doc.Value = Convert.DBNull;
            //Added for Rx Document Attachment 

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            Filldata();
        }

        catch (Exception ex)
        {
            lblMsg.Text = "Error occured while adding due to - " + ex.Message;
            lblMsg.Visible = true;
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void rbtnPhrm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnPhrm.SelectedValue == "1")
        {
            txtPharmacy.Text = "Other Pharmacy";
            txtPharmacy.Visible = true;
            mpeRxApproval.Show();
        }
        else
        {
            txtPharmacy.Visible = false;
            mpeRxApproval.Show();
        }

    }
    protected void setcontextkey(string RxReqID)
    {
        objNLog.Info("Function Started..setcontextkey.");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd;

            if ((string)Session["Role"] == "C" || (string)Session["Role"] == "D")
            {
                sqlCmd = new SqlCommand("sp_getClinics", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
                sp_UserID.Value = (string)Session["User"];

                SqlParameter sp_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
                sp_UserRole.Value = (string)Session["Role"];



                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet dsClinicList = new DataSet();


                sqlDa.Fill(dsClinicList, "ClinicList");

                if (dsClinicList.Tables[0].Rows.Count > 0)
                {
                    AutoCompleteExtender2.ContextKey = dsClinicList.Tables[0].Rows[0]["Clinic_ID"].ToString();

                }
            }
            else if (RxReqID != null)
            {

                sqlCmd = new SqlCommand("select facility_Info.Clinic_ID from facility_Info,Patient_Info,Rx_Drug_Requests where facility_Info.Facility_ID=Patient_Info.Facility_ID and  Patient_Info.pat_ID=Rx_Drug_Requests.pat_ID  and Rx_Drug_Requests.Rx_Req_ID=" + int.Parse(RxReqID), sqlCon);

                sqlCon.Open();
                string clinicID = sqlCmd.ExecuteScalar().ToString();
                sqlCon.Close();
                AutoCompleteExtender2.ContextKey = clinicID;

            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void FillRxData(string RxRequestID)
    {
        objNLog.Info("Function Started...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getRxRequestQueue", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_RequestID = sqlCmd.Parameters.Add("@Rx_Req_ID", SqlDbType.Int);
            par_RequestID.Value =  RxRequestID ;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxRequest = new DataSet();
            sqlDa.Fill(dsRxRequest, "RxRequest");
            txtDocName.Text = dsRxRequest.Tables[0].Rows[0]["doctorName"].ToString();
            hidDocID.Value = dsRxRequest.Tables[0].Rows[0]["Doc_ID"].ToString();
            lblpatientName.Text = dsRxRequest.Tables[0].Rows[0]["PatientName"].ToString();
            lblPatContact.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Pat_Contact"].ToString();
            txtDrug.ReadOnly = false;
            txtDrug.Text = dsRxRequest.Tables[0].Rows[0]["Rx_DrugName"].ToString();
            txtDrug.ReadOnly = true;
            txtQuantity.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Qty"].ToString();
            txtSIG.Text = dsRxRequest.Tables[0].Rows[0]["Rx_SIG"].ToString();
            ddlRefills.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Refills"].ToString();
            txtPharmacy.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Phrm"].ToString();
            txtComments.Text = dsRxRequest.Tables[0].Rows[0]["Rx_Request_Comments"].ToString();
            rbtnDecision.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Status"].ToString();
            rbtnRequestType.SelectedValue = dsRxRequest.Tables[0].Rows[0]["Rx_Type"].ToString();

            if (txtPharmacy.Text.Trim() == "ADiO Pharmacy")
            {
                rbtnPhrm.SelectedValue = "0";
                txtPharmacy.Visible = false;
            }
            else
            {
                rbtnPhrm.SelectedValue = "1";
                txtPharmacy.Visible = true; 
                if(txtPharmacy.Text=="")
                 txtPharmacy.Text = "Other Pharmacy";
            }
            chkSubDrugName.Checked = false;
            chkSubDrugName.Enabled = false;
            pnlSubDrugName.Visible = false;

            if (dsRxRequest.Tables[0].Rows[0]["Rx_Status"].ToString() != "N")
            {
                if ((string)Session["Role"] != "A" && (string)Session["Role"] != "M")
                {
                    btnSave.Visible = false;
                }
            }
            if (rbtnDecision.SelectedValue == "A")
            {
                btnSave.Enabled = false;
            }
            hidRxReqID.Value = RxRequestID;
            lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }
    protected void chkSubDrugName_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSubDrugName.Checked == true)
        {
            pnlSubDrugName.Visible = true;
            txtSubDrugName.Text = "";
        }
        else
        {
            pnlSubDrugName.Visible = false;
            txtSubDrugName.Text = "";
        }
        mpeRxApproval.Show();
    }
    protected void rbtnDecision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnDecision.SelectedValue == "A")
            chkSubDrugName.Enabled = true;
        else
            chkSubDrugName.Enabled = false;
        mpeRxApproval.Show();
    }
}
