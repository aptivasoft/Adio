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
using System.IO;
using System.Text;
using System.Data.SqlClient;
using NLog;
using Adio.UALog;

public partial class PharmacistHome : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
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
                BindLocation(ddlOrganization.SelectedValue);
                FillData();

                if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
                {
                    ddlStatus.SelectedValue = "N";
                    ddlRxType.SelectedValue = "R";
                }
                txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                FillAnnouncements();
                FillEvents();
                gridRxMedIssue.PageIndex = 0;
                FillMedIssues();
            }
            GridViewHelper helper = new GridViewHelper(this.gridRxQueueList);
            string[] group = { "DoctorName", "PatientName" };
            helper.RegisterGroup("Clinic_Name", true, true);
            helper.RegisterGroup("Facility_Name", true, true);
            helper.RegisterGroup(group, true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            FillData(int.Parse(ddlOrganization.SelectedValue), 0, txtDate.Text);
            lblHeading.Text = "Rx Queue List - " + txtDate.Text;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void BindLocation(string clinicID)
    {
        objNLog.Info("Function Started...");

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
            FillData(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
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
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void FillData(int ClinicID, int FacilityID, string date)
    {
        objNLog.Info("Function Started...");

        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getRxQueueList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ClinicID;

            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = FacilityID;

            SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            if (date != "")
                par_Date.Value = DateTime.Parse(date);
            else
                par_Date.Value = System.DBNull.Value;

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
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void gridRxQueueList_RowCommand(object sender, GridViewCommandEventArgs e)
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

            if (e.CommandName == "Processing")
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                //SqlCommand sqlCmd;
                LinkButton lkbtn = (LinkButton)e.CommandSource;
                string s = lkbtn.ToolTip.ToString();
                //string sqlQuery = "";
                //sqlQuery = "Update Rx_Drug_Info SET Rx_Status='T' where Rx_ItemID='" + e.CommandArgument.ToString() + "'";

                //sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                SqlCommand sqlCmd = new SqlCommand("sp_Update_RxDrugInfo_DC_Status", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sp_RxItemID = sqlCmd.Parameters.Add("@Rx_ItemID", SqlDbType.Int);
                sp_RxItemID.Value = int.Parse(e.CommandArgument.ToString());

                SqlParameter sp_Rx_Status = sqlCmd.Parameters.Add("@Rx_Status", SqlDbType.Char, 1);
                sp_Rx_Status.Value = 'T';

                SqlParameter sp_Rx_Comments = sqlCmd.Parameters.Add("@Rx_Comments", SqlDbType.VarChar, 1000);
                sp_Rx_Comments.Value = DBNull.Value;

                SqlParameter sp_Rx_ApprovedBy = sqlCmd.Parameters.Add("@Rx_ApprovedBy", SqlDbType.VarChar, 50);
                sp_Rx_ApprovedBy.Value = (string)Session["User"];
                
                
                sqlCon.Open();

                sqlCmd.ExecuteNonQuery();


                sqlCon.Close();

                FillData(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");

        try
        {
            BindLocation(ddlOrganization.SelectedValue);
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
            FillData(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
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
            FillData(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    public bool DisplayLink(string type, string status)
    {
        objNLog.Info("Function Started...");

        bool flag = false;
        try
        {
            if (type == "Regular" && status != "Hold" && status != "Dispatched" && status != "Denied" && status != "Received")
            {
                if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")

                    flag = true;
                else
                    flag = false;
            }
            else
            {
                flag = false;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error :" + ex.Message);
        }
        objNLog.Info("Function Completed...");
        return flag;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");

        if (Directory.Exists(MapPath(".") + "\\" + "RxDocuments"))
        {
            DirectoryInfo di = new DirectoryInfo(MapPath(".") + "\\" + "RxDocuments");
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
            Directory.Delete(MapPath(".") + "\\" + "RxDocuments");
        }
        string userID = (string)Session["User"];
        string str = "printWin = window.open('', 'RxPrint', 'scrollbars=1,menubar=1,resizable=1');"

                    + "if (printWin != null) "
                    + "{"
                    + "printWin.document.open();"
                    + @"printWin.document.write(""" + GetFormat() + @""");"
                    + "printWin.document.close();"
                    + "printWin.print();"
                    + "}";

        ScriptManager.RegisterStartupScript(btnPrint, typeof(Page), "alert", str, true);
       
        if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
        {
            string strConfirm = "document.getElementById('ctl00_ContentPlaceHolder1_btnConfirm').click();"
                                + "function needConfirm(){if (confirm('Is It Ok to Clear the Queue')) "
                                + "{"
                                + "document.getElementById('ctl00_ContentPlaceHolder1_hidConfirm').click();return true;"
                                + "}"
                                +"else"
                                +"{" 
                                +"return false;"
                                + "}"
                                + "}";
            
            ScriptManager.RegisterStartupScript(btnPrint, typeof(Page), "alert1", strConfirm, true);
            
        }
                        
        
        FillData(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);

        objNLog.Info("Event Completed...");
   
}

    protected string GetFormat()
    {
        objNLog.Info("Function Started...");
        string str = "";
        string RxDName = "";
        string fontColor = "#000000";
        Boolean PageSkipped = true;

        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getRxQueueList", sqlCon);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
            par_ClinicID.Value = ddlOrganization.SelectedValue;

            SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            par_FacilityID.Value = ddlLocation.SelectedValue;

            SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            par_Date.Value = DateTime.Parse(txtDate.Text);

            SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);

            if ((string)Session["Role"] == "P" || (string)Session["Role"] == "T")
            {
                par_RxType.Value = 'R';
                par_RxStatus.Value = 'N';
            }
            else
            {
                par_RxType.Value = ddlRxType.SelectedValue;
                par_RxStatus.Value = ddlStatus.SelectedValue;
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
                            if (PageSkipped)
                                str = str + "</table> ";
                            else
                                str = str + "</table> <H5 style='page-break-before:always'>&nbsp;</H5> ";
                        }
                        else
                            str = str + "<H5>&nbsp;</H5>";

                        PageSkipped = false;
                        str = str + "<table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'> <tr><td width='50%' align='left'><B> Date: </B>" + dv.Table.Rows[i]["RxDate"].ToString() + "</td><td width='50%' align='right'> <B>Entered by: </B> " + dv.Table.Rows[i]["User"].ToString() + " </td></tr>";

                        patientName = (string)dv.Table.Rows[i]["PatientName"];
                        str = str + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                        + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                        + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + "<tr><td  align='left' colspan='2'> <B>Clinic/Location:&nbsp;&nbsp;</B> " + dv.Table.Rows[i]["Clinic_Name"].ToString() + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() + "</td></tr>"
                      + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
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
                   
                     


                    str = str + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr><tr><td width='50%' align='left'> <B> "
                              + j.ToString()
                              + ".&nbsp;DRUG NAME :&nbsp;&nbsp; </B>"
                              + RxDName
                              + "</td><td width='50%' align='left'> &nbsp; <B> QTY :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_Qty"].ToString()
                              + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <B> Refills :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_Refills"].ToString()
                              + " </td></tr>"
                              + "<tr><td width='50%' align='left'><B>&nbsp;&nbsp;&nbsp; SIG :&nbsp;&nbsp;</B>"
                              + dv.Table.Rows[i]["Rx_SIG"].ToString()
                              + "</td><td width='50%' align='right'> &nbsp; </td></tr>"
                              + "<tr><td COLSPAN=2 align='left'><B>&nbsp;&nbsp;&nbsp; Comments :&nbsp;&nbsp;</B>"
                             + dv.Table.Rows[i]["Rx_Comments"].ToString().Replace("\"", "'").Replace("\r\n", "<p>")
                             + "</td></tr>";
                    
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
                //string address = dv.Table.Rows[0]["Pat_Address1"].ToString() + " " + dv.Table.Rows[0]["Pat_Address2"].ToString();
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

                str = str + "<tr><td colspan='2'><table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'>"
                    + "<tr><td width='45%' align='left'> <B>Patient:&nbsp;&nbsp;</B>" + dv.Table.Rows[0]["Pat_FName"].ToString() + " " + dv.Table.Rows[0]["Pat_LName"].ToString()
                    + "</td><td width='25%' align='left'> <B>Gender:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["Pat_Gender"].ToString()
                    + "</td><td width='30%' align='left'> <B>DOB:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["Pat_DOB"].ToString() + " </td></tr>"
                    + "<tr><td  align='left' colspan='2'> <B>Address :&nbsp;&nbsp;</B>" + address

                    + "</td><td  align='left'> <B>Phone:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["Pat_Phone"].ToString() + " </td></tr>"
                    + "<tr><td  align='left' colspan='3'>&nbsp;</td></tr>"
                    + "<tr><td  align='left' > <B>Insurance:&nbsp;&nbsp;</B>" + dv.Table.Rows[0]["Ins_Name"].ToString()
                    + "</td><td  align='left'> <B>Policy #:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["PI_PolicyID"].ToString()
                    + "</td><td  align='left'> <B>Group#:&nbsp;&nbsp;</B> " + dv.Table.Rows[0]["PI_GroupNo"].ToString() + " </td><td>&nbsp;</td></tr>"
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
        string str = "";
        try
        {
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
        objNLog.Info("Event Started...");
        try
        {
            ddlStatus.Items.Insert(0, new ListItem("All", "A"));
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void gridActions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridActions.PageIndex = e.NewPageIndex;
            FillData();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void FillData()
    {
        objNLog.Info("Function Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getTodaysMedRequest_Pharmacist", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        try
        {
            SqlParameter sp_Option = sqlCmd.Parameters.Add("@Option", SqlDbType.Char, 1);
            sp_Option.Value = rbtnActions.SelectedValue.ToString();

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsAppointment = new DataSet();
            sqlDa.Fill(dsAppointment);
            gridActions.DataSource = dsAppointment;
            gridActions.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

    protected void rbtnActions_SelectedIndexChanged(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            FillData();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    public String display_Doctor(string ApprovedName, string doctorName)
    {
        objNLog.Info("Function Started...");
        string retName = string.Empty;
        try
        {
            if (ApprovedName != "")
            {
                retName = doctorName + " ( " + ApprovedName + " )";
            }
            else
            {
                retName = doctorName;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
        return retName;
    }

    private void FillEvents()
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand();
            DataSet dsEvents = new DataSet();
            SqlDataAdapter sqlDa;
            sqlCmd.CommandText = "sp_getEvents";
            sqlCmd.Connection = sqlCon;
            SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_UserID.Value = (string)Session["User"];

            SqlParameter sp_Role = sqlCmd.Parameters.Add("@Role", SqlDbType.Char, 1);
            sp_Role.Value = char.Parse((string)Session["Role"]);
            sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsEvents);
            gridEvents.DataSource = dsEvents;
            gridEvents.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error :" + ex.Message);
        }
    }
    private void FillAnnouncements()
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand();
            DataSet dsAppointment = new DataSet();
            SqlDataAdapter sqlDa;
            sqlCmd.CommandText = "sp_getAnnouncements";
            sqlCmd.Connection = sqlCon;
            SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
            sp_UserID.Value = (string)Session["User"];

            SqlParameter sp_Role = sqlCmd.Parameters.Add("@Role", SqlDbType.Char, 1);
            sp_Role.Value = char.Parse((string)Session["Role"]);
            sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsAppointment);
            gridMessages.DataSource = dsAppointment;
            gridMessages.DataBind();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }
    protected void gridEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            if (e.Row.RowState == DataControlRowState.Alternate)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='FFFFCC';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#eaf0f4';");
            }
            else
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='FFFFCC';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#d6e3ec';");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                if (mpContentPlaceHolder != null)
                {
                    DataSet dsEvts = new DataSet();
                    SqlConnection sqlCon = new SqlConnection(conStr);
                    SqlCommand sqlCmd = new SqlCommand("sp_getEventsByEvtID", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlEventID = sqlCmd.Parameters.Add("@Event_ID", SqlDbType.Int);
                    Label lb;
                    lb = (Label)e.Row.Cells[0].FindControl("lblEventID");
                    sqlEventID.Value = Int32.Parse(lb.Text);

                    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

                    sqlDa.Fill(dsEvts, "Events");
                    // HtmlContainerControl divEvt = (HtmlContainerControl)mpContentPlaceHolder.FindControl("divEvt");
                    if (dsEvts != null && dsEvts.Tables.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Date: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][0].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Description: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][1].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Duration: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][2].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Posted By: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][3].ToString() + "");
                        sb.Append(Environment.NewLine);
                        sb.Append(Environment.NewLine);
                        sb.Append("Posted Date: ");
                        sb.Append("" + dsEvts.Tables[0].Rows[0][4].ToString() + "");
                        e.Row.ToolTip = sb.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void gridEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridEvents.PageIndex = e.NewPageIndex;
            FillEvents();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void gridMessages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            gridMessages.PageIndex = e.NewPageIndex;
            FillAnnouncements();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void btnPMI_Click(object sender, EventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            LinkButton lbtn = (LinkButton)sender;
            gridRxMedIssue.PageIndex = int.Parse(lbtn.CommandArgument.ToString());
            FillMedIssues();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    protected void gridRxMedIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    private void FillMedIssues()
    {
        objNLog.Info("Function Started...");
        int pagecount = 0;
        int pageSize = 0;
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("sp_getMedIssues_Pharmacist", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter sp_UserID = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        sp_UserID.Value = (string)Session["User"];



        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsMedIssues = new DataSet();
        try
        {
            sqlDa.Fill(dsMedIssues);

            gridRxMedIssue.DataSource = dsMedIssues;
            gridRxMedIssue.DataBind();

            Session["CallLog_Phrm"] = dsMedIssues;
            if (gridRxMedIssue.Rows.Count > 0)
            {
                imgBtnSendCallLog.Enabled = true;
                imgBtnPrintCallLog.Enabled = true;
            }
            else
            {
                imgBtnSendCallLog.Enabled = false;
                imgBtnPrintCallLog.Enabled = false;
            }

            pageSize = gridRxMedIssue.PageSize;
            pagecount = dsMedIssues.Tables[0].Rows.Count / pageSize;

            if (pagecount * pageSize < dsMedIssues.Tables[0].Rows.Count)
            {
                pagecount = pagecount + 1;

            }
            if (gridRxMedIssue.PageIndex > 0)
            {
                btnPMI.Enabled = true;
                btnPMI.CommandArgument = (gridRxMedIssue.PageIndex - 1).ToString();
            }
            else
            {
                btnPMI.Enabled = false;
            }
            if (gridRxMedIssue.PageIndex == pagecount - 1 || pagecount == 0)
            {
                btnNMI.Enabled = false;
            }
            else
            {
                btnNMI.CommandArgument = (gridRxMedIssue.PageIndex + 1).ToString();
                btnNMI.Enabled = true;
            }

            if (dsMedIssues.Tables[0].Rows.Count <= pageSize)
            {
                btnNMI.Visible = false;
                btnPMI.Visible = false;
                btnAllMI.Visible = false;
            }
            else
            {
                btnNMI.Visible = true;
                btnPMI.Visible = true;
                btnAllMI.Visible = true;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error: " + ex.Message);
        }
    }
    protected void gridRxMedIssue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
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
        objNLog.Info("Event Completed...");
    }
    protected void btnMedIssueSave_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        SqlConnection sqlCon = new SqlConnection(conStr);
   
        string user = (string)Session["User"];
       
        //string query = "Update  [Call_Log] SET Issue_Response='" + txtMedIssueComment.Text
        //            + "',Issue_ResponseFlag='Y',Issue_ResponseBy='" + (string)Session["User"]
        //            + "',Issue_ResponseDate=getdate() where [Call_ID] ='" + hfCallID.Value + "'";
        
        SqlCommand sqlCmd  = new SqlCommand("sp_Update_CallLog_Pharmacist", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter parm_user = sqlCmd.Parameters.Add("@User", SqlDbType.VarChar, 20);
        parm_user.Value = user;

        SqlParameter parm_note = sqlCmd.Parameters.Add("@Note", SqlDbType.VarChar, 255);
        parm_note.Value = txtMedIssueComment.Text;

        SqlParameter parm_callid = sqlCmd.Parameters.Add("@CallID", SqlDbType.Int);
        if(hfCallID.Value!="")
            parm_callid.Value = int.Parse(hfCallID.Value);
        else
            parm_callid.Value = Convert.DBNull;

        
        try
        {
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

             objUALog.LogUserActivity(conStr, user, "Updated Call Log while Processing Med Issue. [Call_ID] =" + hfCallID.Value, "Call_Log", 0);

            FillMedIssues();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        objNLog.Info("Event Completed...");
    }
    protected void gridMessages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                if (mpContentPlaceHolder != null)
                {
                    Label lb;
                    lb = (Label)e.Row.Cells[1].FindControl("lblAnnDate");
                    ((Label)e.Row.Cells[1].FindControl("lblAnnDate")).Text = DateTime.Parse(lb.Text).ToString("MM/dd/yyyy");
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    protected void hidConfirm_Click(object sender, EventArgs e)
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
            par_RxType.Value = 'R';// ddlRxType.SelectedValue;
            SqlParameter par_RxStatus = sqlCmd.Parameters.Add("@RxStatus", SqlDbType.Char, 1);
            par_RxStatus.Value = 'N';
            SqlParameter par_prtTime = sqlCmd.Parameters.Add("@prtTime", SqlDbType.VarChar);
            par_prtTime.Value = txtPrintTime.Text;

            try
            {
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                FillData(int.Parse(ddlOrganization.SelectedValue), int.Parse(ddlLocation.SelectedValue), txtDate.Text);

            }
            catch (Exception ex)
            {
                objNLog.Error("Error : " + ex.Message);
            }
        }
    protected void imgBtnSendCallLog_Click(object sender, ImageClickEventArgs e)
    {
        txtEmailFrom.Text = GetUserEmailID((string)Session["User"]);
        popEmailCallLog.Show();
    }

    private string GetUserEmailID(string userID)
    {
        string userEmailID = "";
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_getUsersEmailID", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
        par_UserID.Value = (string)Session["User"];

        SqlParameter par_UserRole = sqlCmd.Parameters.Add("@UserRole", SqlDbType.Char, 1);
        par_UserRole.Value = (string)Session["Role"];



        try
        {
            sqlCon.Open();
            userEmailID = sqlCmd.ExecuteScalar().ToString();
            sqlCon.Close();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return userEmailID;
    }

    private void SendEmailMessage(string Message)
    {
        try
        {
            SmtpSettings objSmtp = new SmtpSettings();
            DataSet dsSmtp = objSmtp.GetSmtpSettings();
            if (dsSmtp.Tables.Count > 0)
            {
                if (dsSmtp.Tables[0].Rows.Count > 0)
                {
                    SMTPProperties objSmtpProp = new SMTPProperties();
                    SMTPSendEMail objEmail = new SMTPSendEMail();

                    objSmtpProp.Server = dsSmtp.Tables[0].Rows[0][0].ToString();
                    objSmtpProp.Port = int.Parse(dsSmtp.Tables[0].Rows[0][1].ToString());
                    objSmtpProp.UserName = dsSmtp.Tables[0].Rows[0][2].ToString();
                    objSmtpProp.PassWord = dsSmtp.Tables[0].Rows[0][3].ToString();

                    if (txtEmailFrom.Text != "")
                        objSmtpProp.MailFrom = txtEmailFrom.Text;
                    else
                        objSmtpProp.MailFrom = dsSmtp.Tables[0].Rows[0][4].ToString();

                    objSmtpProp.MailTo = txtEmailTo.Text;
                    objSmtpProp.IsSSL = bool.Parse(dsSmtp.Tables[0].Rows[0][6].ToString());
                    objSmtpProp.IsHTML = bool.Parse(dsSmtp.Tables[0].Rows[0][7].ToString());

                    char mailPriority = char.Parse(dsSmtp.Tables[0].Rows[0][8].ToString());
                    switch (mailPriority)
                    {
                        case 'L': { objSmtpProp.MailPriority = SMTPProperties.Priority.Low; break; }
                        case 'H': { objSmtpProp.MailPriority = SMTPProperties.Priority.High; break; }
                        case 'N': { objSmtpProp.MailPriority = SMTPProperties.Priority.Normal; break; }
                    }


                    if (txtEmailSubject.Text != "")
                        objSmtpProp.MailSubject = txtEmailSubject.Text;
                    else
                        objSmtpProp.MailSubject = "Call Log";


                    if (txtEmailMessage.Text != "")
                        objSmtpProp.MailMessage = txtEmailMessage.Text + "<br/>" + Message;
                    else
                        objSmtpProp.MailMessage = Message;

                    MailResponse objMailResp = objEmail.SendEmail(objSmtpProp);
                    string str = "alert('" + objMailResp.StatusMessage + "');";
                    ScriptManager.RegisterStartupScript(imgBtnSendEmail, typeof(Page), "alert", str, true);
                }
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void imgBtnSendEmail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SendEmailMessage(GetCallLogFormat());
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
    protected void imgBtnPrintCallLog_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
            string strPrintCallLog = "printWin = window.open('', 'CallLogPrint', 'scrollbars=1,menubar=1,resizable=1');"

                        + "if (printWin != null) "
                        + "{"
                        + "printWin.document.open();"
                        + @"printWin.document.write(""" + GetCallLogFormat() + @""");"
                        + "printWin.document.close();"
                        + "printWin.print();"
                        + "}";
            ScriptManager.RegisterStartupScript(btnPrint, typeof(Page), "alert", strPrintCallLog, true);

        }
        catch (Exception ex)
        {
            objNLog.Info("Error: " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }
    private string GetCallLogFormat()
    {
        DataSet ds = (DataSet)Session["CallLog_Phrm"];
        StringBuilder sb = new StringBuilder();
        DataTable dt = ds.Tables[0];

        sb.Append("<table style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;padding:3;'>");
        sb.Append("<tr>");

        for (int colIndx = 0; colIndx < dt.Columns.Count - 1; colIndx++)
        {
            sb.Append("<td style='border: solid 1px Silver; font-size:12px; font-family:Arial;border-collapse:collapse;font-weight:bold; color:#FFFFFF;padding:3;background:#4f81bc;vertical-align:middle;text-align:center;'>");
            sb.Append(dt.Columns[colIndx].ColumnName);
            sb.Append("</td>");
        }

        sb.Append("</tr>");
        for (int rowIndx = 0; rowIndx < dt.Rows.Count; rowIndx++)
        {
            sb.Append("<tr>");
            for (int colIndx = 0; colIndx < dt.Columns.Count - 1; colIndx++)
            {
                if (rowIndx % 2 == 0)
                {
                    if (colIndx == 0)
                        sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:right;background:#d6e3ec;'>");
                    else
                        sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:left;background:#d6e3ec;'>");
                }
                else
                {
                    if (colIndx == 0)
                        sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:right;background:#eaf0f4;'>");
                    else
                        sb.Append("<td style='border: solid 1px Silver; font-size:11px; font-family:Arial;border-collapse:collapse;font-weight:normal; color:#000000;text-align:left;background:#eaf0f4;'>");

                }

                sb.Append(dt.Rows[rowIndx][colIndx].ToString());
                sb.Append("</td>");
            }
            sb.Append("</tr>");

        }
        sb.Append("</table>");
        return sb.ToString();
    }
}
