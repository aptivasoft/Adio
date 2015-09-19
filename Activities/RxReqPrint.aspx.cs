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
using System.IO;
using System.Data.SqlClient;
using NLog;

public partial class RxReqPrint : System.Web.UI.Page
{
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    string conStr = ConfigurationManager.AppSettings["conStr"];
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if (!Page.IsPostBack)
        {
            // ddlStatus.DataBind();
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
            //if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
            //{
            //    ddlRxType.SelectedValue = "R";
            //}
            //ddlStatus.SelectedValue = "N";
            //helper.ApplyGroupSort();
            txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }
        GridViewHelper helper = new GridViewHelper(this.gridRxReqList);
        string[] group = { "doctorName", "PatientName" };
        //helper.RegisterGroup(group, true, true);
        helper.RegisterGroup("Clinic_Name", true, true);
        helper.RegisterGroup("Facility_Name", true, true);
        helper.RegisterGroup(group, true, true);

        //helper.RegisterGroup("DoctorName", true, true);
        //helper.RegisterGroup("PatientName", true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
          Filldata(int.Parse(ddlOrganization.SelectedValue), 0, txtDate.Text);
       
        lblHeading.Text = "Rx Request Printing - " + txtDate.Text;
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

            SqlCommand sqlCmd = new SqlCommand("sp_RxReqPrint", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ClinicID;
            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = FacilityID;
            SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            par_Date.Value = DateTime.Parse(date);
            //SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            //par_RxType.Value = ddlRxType.SelectedValue;

            //SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
            //par_RxStatus.Value = 'N';

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxQueueList = new DataSet();

            sqlDa.Fill(dsRxQueueList, "RxQueueList");
            gridRxReqList.DataSource = dsRxQueueList;
            gridRxReqList.DataBind();
            DataTable dt = dsRxQueueList.Tables[0];
            //dt.DefaultView.RowFilter = "status='Transmitted'";  // Should be changed to Printed = false BN
            if (dt.DefaultView.Count > 0 && ((string)Session["Role"] == "C" || (string)Session["Role"] == "N" || (string)Session["Role"] == "D"))
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

            //if (ddlStatus.SelectedValue == "O")
            //{
            //    btnPrint.Visible = false;
            //    btnAdminPrint.Visible = false;
            //}
            //if ((string)Session["Role"] == "C" && dt.Rows.Count > 0 && (ddlRxType.SelectedValue == "S" || ddlRxType.SelectedValue == "P"))
            //{
            //    btnPrint.Visible = true;
            //}


        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void gridRxReqList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                //SqlCommand sqlCmd;
                LinkButton lkbtn = (LinkButton)e.CommandSource;
                string s = lkbtn.ToolTip.ToString();
                //string sqlQuery = "";
                //sqlQuery = "Update Rx_Drug_Info SET Rx_Status='T' where Rx_ItemID='" + e.CommandArgument.ToString() + "'";
                 
                //    sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                //    sqlCon.Open();

                //    sqlCmd.ExecuteNonQuery();
                //    sqlCon.Close();
                SqlCommand sqlCmd = new SqlCommand("sp_Update_RxDrugInfo_DC_Status", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_RxItemID = sqlCmd.Parameters.Add("@Rx_ItemID", SqlDbType.Int);
                sp_RxItemID.Value = int.Parse(e.CommandArgument.ToString());

                SqlParameter sp_Rx_Status = sqlCmd.Parameters.Add("@Rx_Status", SqlDbType.Char, 1);
                sp_Rx_Status.Value = 'T';

                SqlParameter sp_Rx_Comments = sqlCmd.Parameters.Add("@Rx_Comments", SqlDbType.VarChar, 1000);
                sp_Rx_Comments.Value = string.Empty; ;

                SqlParameter sp_Rx_ApprovedBy = sqlCmd.Parameters.Add("@Rx_ApprovedBy", SqlDbType.VarChar, 50);
                sp_Rx_ApprovedBy.Value =(string) Session["User"] ;

                Filldata(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
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


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        objNLog.Info("RxRequest Print Click Event Started...");
        try
        {
            if ((ddlOrganization.SelectedIndex != 0) || (ddlOrganization.SelectedIndex == 0 && (string)Session["Role"] == "C" || (string)Session["Role"] == "N" || (string)Session["Role"] == "D"))
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
                // if (((string)Session["Role"] == "P" || (string)Session["Role"] == "T") ||
                   //   (string)Session["Role"] == "C" && (ddlRxType.SelectedValue == "S" || ddlRxType.SelectedValue == "P" ))
                if ((string)Session["Role"] == "C" || (string)Session["Role"] == "D" || (string)Session["Role"] == "N" || (string)Session["Role"] == "A")
                {

                    string strConfirm = "document.getElementById('ctl00_ContentPlaceHolder1_btnConfirm').click();"
                                + "function needConfirm(){if (confirm('Is It Ok to Clear the Rx Request Queue?')) "
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
        objNLog.Info("RxRequest Print Click Event Completed...");
        }


    protected string getformat()
    {
        objNLog.Info("getformat  (RxRequest Print) Function Started...");
        string str = "";
        try
        {
        SqlConnection sqlCon = new SqlConnection(conStr);
        //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";
        SqlCommand sqlCmd = new SqlCommand("sp_RxReqPrint", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
        par_ClinicID.Value = ddlOrganization.SelectedValue;
        SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
        par_FacilityID.Value = ddlLocation.SelectedValue;
        SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
        par_Date.Value = DateTime.Parse(txtDate.Text);
        //SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
        // ddlRxType.SelectedValue;
        //SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
      
        if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
        {
            //par_RxType.Value = 'R';
            //par_RxStatus.Value = 'N';
        }
        else
        {
            //par_RxType.Value = ddlRxType.SelectedValue ;
            //par_RxStatus.Value = ddlStatus.SelectedValue  ;
        }
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsRxQueueList = new DataSet();

        txtPrintTime.Text = DateTime.Now.TimeOfDay.ToString();

            sqlDa.Fill(dsRxQueueList, "RxQueueList");
           
            if (dsRxQueueList.Tables[0].Rows.Count > 0)
            {

                DataView dv = new DataView(dsRxQueueList.Tables[0]);
                dv.Sort = "Doc_ID,PatientName";
                string patientName = "";
                string DocId = "";
                string pageHeader = "";
                pageHeader = "<TABLE  WIDTH=100%><TR><TD WIDTH=238px><IMG SRC='../Images/logo_audio_new.png'></TD>" +
                            "<TD style='text-align:center;font-weight:bold;font-size:16px; font-family : Arial, Helvetica, Verdana, sans-serif;'>RX REQUEST</TD>" + 
                            "<TD>&nbsp;</TD></TR></TABLE><BR>";
                int j = 0;
                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (patientName != (string)dv.Table.Rows[i]["PatientName"] || DocId != dv.Table.Rows[i]["Doc_ID"].ToString())
                    {
                        
                        j = 0;
                        if (patientName != "")
                        {
                           str = str + getDoc_Printformat(dv.Table.Rows[i-1]["Doc_ID"].ToString()) 
                                     + "</table> <H5 style='page-break-before:always'>&nbsp;</H5>" + pageHeader;
                        }
                        else
                            str = str + pageHeader;
                        
                        
                        str = str + "<table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'>";
                        //"<tr><td width='50%' align='left'><B> Date: </B>" + System.DateTime.Now.ToString("MM/dd/yyyy") + "</td><td width='50%' align='right'> <B>Entered by: </B> " + " </td></tr>";
                        
                        patientName = (string)dv.Table.Rows[i]["PatientName"];
                        DocId = dv.Table.Rows[i]["Doc_ID"].ToString();
                        str = str + "<tr><td  align='right' colspan='2'><B>Date :&nbsp;</B>" + Convert.ToDateTime(dv.Table.Rows[i]["rxdate"].ToString()).ToShortDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>"
                        + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                        + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + "<tr><td  align='left' colspan='2'> <B>Clinic/Location:&nbsp;&nbsp;</B> " + dv.Table.Rows[i]["Clinic_Name"].ToString() + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() + "</td></tr>"
                      + "<tr><td  align='left' colspan='2'><HR></td></tr>";
                      //+ getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString());

                    }

                    if (j % 4 == 0 && j != 0)
                    {


                        str = str + getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString()) 
                             + "</table><H5 style='page-break-before:always'>&nbsp;</H5>"
                             + pageHeader 
                             + "<table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'> ";

                        str = str + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                      + "<tr><td  align='left' colspan='2'><BR></td></tr>"
                      + "<tr><td  align='left' colspan='2'> <B>Clinic/Location:&nbsp;&nbsp;</B> " + dv.Table.Rows[i]["Clinic_Name"].ToString() + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() + "</td></tr>"
                      + "<tr><td  align='left' colspan='2'><HR></td></tr>";
                      //+ getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString());

                    }
                    
                    j = j + 1;
                   
                    str = str + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr><tr><td width='50%' align='left'> <B> " 
                              + j.ToString() 
                              + ".&nbsp;DRUG NAME :&nbsp;&nbsp; </B>" 
                              + dv.Table.Rows[i]["Rx_DrugName"].ToString() 
                              + "</td><td width='50%' align='left'> &nbsp; <B> QTY :&nbsp;&nbsp;</B>" 
                              + dv.Table.Rows[i]["Rx_Qty"].ToString()
                              + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <B> Refills :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_Refills"].ToString()
                              + " </td></tr>"
                              + "<tr><td width='50%' align='left'><B>&nbsp;&nbsp;&nbsp; SIG :&nbsp;&nbsp;</B>" 
                              + dv.Table.Rows[i]["Rx_SIG"].ToString()
                              + "</td><td width='50%' align='right'> &nbsp;RX Type:" + dv.Table.Rows[i]["RxType"].ToString();

                    if (dv.Table.Rows[i]["RxType"].ToString() != "ADiO Pharmacy")
                        str = str + "(" + dv.Table.Rows[i]["Rx_Phrm"].ToString() + ")</td></tr>";
                    else
                          str = str + "</td></tr>";

                    str=str  + "<tr><td COLSPAN=2 align='left'><B>&nbsp;&nbsp;&nbsp; Comments :&nbsp;&nbsp;</B>"
                                + dv.Table.Rows[i]["Rx_Request_Comments"].ToString().Replace("\"", "'").Replace("\r\n", "<p>")
                                + "</td></tr>";
                    
                   /* -- if (dv.Table.Rows[i]["Rx_Doc"] != DBNull.Value)
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
                        if ( imgH > 1400 || imgW > 1200 )
                            str = str + "<tr><td colspan=2><H5 style='page-break-after:always'><img src='RxDocuments/" + imgFile + "' height=1000 width=800 alt=''/></H5></td></tr>";
                        else
                            str = str + "<tr><td colspan=2><H5 style='page-break-after:always'><img src='RxDocuments/" + imgFile + "' alt=''/></H5></td></tr>";
                        PageSkipped = true;
                    } */
                }
                str = str + getDoc_Printformat(dv.Table.Rows[dsRxQueueList.Tables[0].Rows.Count-1]["Doc_ID"].ToString())+"</TABLE>";
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error (RxRequest Print) : " + ex.Message);
        }
        objNLog.Info("getformat (RxRequest Print) Function Completed...");
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
                string address = dv.Table.Rows[0]["Pat_Address1"].ToString() + " " + dv.Table.Rows[0]["Pat_Address2"].ToString();
                if (address.Trim() != "")
                    address = address + ", " + dv.Table.Rows[0]["Pat_City"].ToString();
                else
                    address = dv.Table.Rows[0]["Pat_City"].ToString();

                if (address.Trim() != "")
                    address = address + ", " + dv.Table.Rows[0]["Pat_State"].ToString();
                else
                    address = dv.Table.Rows[0]["Pat_State"].ToString();

                address = address + " " + dv.Table.Rows[0]["Pat_Zip"].ToString();

                str = str + "<tr><td colspan='2'><table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'>"
                    + "<tr><td width='45%' align='left'>  <B>Patient&nbsp;&nbsp;&nbsp;:&nbsp;</B>" + dv.Table.Rows[0]["Pat_FName"].ToString() + " " + dv.Table.Rows[0]["Pat_LName"].ToString()
                    + "</td><td width='25%' align='left'> <B>Gender:&nbsp;&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["Pat_Gender"].ToString()
                    + "</td><td width='30%' align='left'> <B>DOB:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</B> " + Convert.ToDateTime(dv.Table.Rows[0]["Pat_DOB"].ToString()).ToShortDateString() + " </td></tr>"
                    + "<tr><td  align='left' colspan='2'> <B>Address&nbsp;&nbsp;:&nbsp;</B>" + address

                    + "</td><td  align='left'>  <B>Phone:&nbsp;&nbsp;&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["Pat_Phone"].ToString() + " </td></tr>"
                    + "<tr><td  align='left' colspan='3'>&nbsp;</td></tr>"
                    + "<tr><td  align='left' > <B>Insurance:&nbsp;</B>" + dv.Table.Rows[0]["Ins_Name"].ToString()
                    + "</td><td  align='left'> <B>Policy #:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["PI_PolicyID"].ToString()
                    + "</td><td  align='left'> <B>Group  #:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["PI_GroupNo"].ToString() + " </td><td>&nbsp;</td></tr>"
                    + "<tr><td  align='left' colspan='3'>&nbsp;</td></tr>"
                + "<tr><td  align='left' colspan='3'> <B>Diagnosis Code:&nbsp;&nbsp;</B>" + dv.Table.Rows[0]["diag_Codes"].ToString()
                     + " </td></tr></table></td></tr>";
              
              


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
                    + "<tr><td colspan=2 align='left'><HR><BR><BR> "
                        + "<TABLE style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'>"
                        +" <tr> <td width=430px>  &nbsp;</td> <td> Signature:</td><td>___________________________________</td></tr> "
                        + "<tr> <td>  &nbsp;</td> <td> <B>Doctor:&nbsp;&nbsp;</B></td><td>"
                        + dv.Table.Rows[0]["Doc_FName"].ToString() + " " + dv.Table.Rows[0]["Doc_LName"].ToString() + degree
                        + "</td></tr> "
                        + "<tr> <td>  &nbsp;</td> <td> &nbsp;</td><td>"
                        + "<B>DEA#  :&nbsp;</B>" + dv.Table.Rows[0]["Doc_DeaNo"].ToString() + "<BR>" 
                        + "<B>Lic No:&nbsp;</B>" + dv.Table.Rows[0]["Doc_LicNo"].ToString() 
                        + "</td></tr></TABLE>"
                     + "</td></tr>";



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
            objNLog.Error("PrintTime: " + txtPrintTime.Text);
            string userID = (string)Session["User"];
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_UpdateRxReqPrint", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar);
            par_UserID.Value = userID;
            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ddlOrganization.SelectedValue;
            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = ddlLocation.SelectedValue;
            SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            par_Date.Value = DateTime.Parse(txtDate.Text);
            SqlParameter par_prtTime = sqlCmd.Parameters.Add("@prtTime", SqlDbType.VarChar);
            par_prtTime.Value = txtPrintTime.Text;
            //SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            //par_RxType.Value = ddlRxType.SelectedValue;// 'R';
            //SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
            //par_RxStatus.Value = 'N';

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
