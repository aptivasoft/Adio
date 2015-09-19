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
using System.ComponentModel;
using NLog;
using Adio.UALog;

public partial class RxSample : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
    PatientInfoDAL objPat_Info = new PatientInfoDAL();
    protected void Page_Load(object sender, EventArgs e)
     {
         try
         {
             if (Session["User"] == null || Session["Role"] == null)
                 Response.Redirect("../Login.aspx");

             GridViewHelper helper = new GridViewHelper(this.gridRxSampleList);
             string[] group = { "DoctorName", "PatientName" };
             helper.RegisterGroup(group, true, true);
             helper.GroupHeader += new GroupEvent(helper_GroupHeader);

             Filldata();
         }
         catch (Exception ex)
         {
             objNLog.Error("Error : " + ex.Message);
         }
    }

    protected void gridRxSampleList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objNLog.Info("Event Started...");
        try
        {
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
                hfRXItemID.Value = e.CommandArgument.ToString();
                hfPatId.Value = ds.Tables[0].Rows[0]["Pat_ID"].ToString();
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
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

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

                int flag = 0;
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
            Filldata();
        }
        objNLog.Info("Event Completed...");
    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        row.BackColor = System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
        row.HorizontalAlign = HorizontalAlign.Left;
    }

    protected void Filldata()
    {
        objNLog.Info("Function Started...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getRxSampleList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_FacID = sqlCmd.Parameters.Add("@facilityID", SqlDbType.Int);
            sp_FacID.Value = int.Parse(Request.QueryString["Fac_ID"].ToString());

            SqlParameter sp_RxDate = sqlCmd.Parameters.Add("@RxDate", SqlDbType.DateTime);
            if (Request.QueryString["RxDate"] != null)
                sp_RxDate.Value = Request.QueryString["RxDate"].ToString();
            else
                sp_RxDate.Value = System.DBNull.Value;

            SqlParameter sp_FacName = sqlCmd.Parameters.Add("@facilityName", SqlDbType.VarChar, 50);
            sp_FacName.Direction = ParameterDirection.Output;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxSampleList = new DataSet();

            sqlDa.Fill(dsRxSampleList, "RxSampleList");
            gridRxSampleList.DataSource = dsRxSampleList;
            gridRxSampleList.DataBind();

            if (Request.QueryString["RxDate"] != null)
            {
                string rxDate = Request.QueryString["RxDate"].ToString();

                DateTime dt = (DateTime)(TypeDescriptor.GetConverter(new DateTime(1990, 1, 1)).ConvertFrom(rxDate));

                lblHeading.Text = "Rx Sample - " + dt.ToString("MM/dd/yyyy") + " - " + sp_FacName.Value.ToString();

            }
            else
            lblHeading.Text = "Rx Sample - " + DateTime.Now.ToString("MM/dd/yyyy") + " - " + sp_FacName.Value.ToString();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed...");
    }

    public bool display_link( string status)
    {
        //check username here and return a bool
        if (status != "Hold" && status != "Dispatched" && status != "Denied")
        {
            
                return true;
            
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
            string str = "printWin = window.open('', 'RxPrint', 'scrollbars=1,menubar=1,resizable=1');"


                + "if (printWin != null) "
                + "{"
                + "printWin.document.open();"
                + @"printWin.document.write(""" + getformat() + @""");"
                + "printWin.document.close();"
                + "printWin.print();}";
            ScriptManager.RegisterStartupScript(btnPrint, typeof(Page), "alert", str, true);
            Filldata();
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
        Boolean PageSkipped = true;
        string str = "";
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getRxSampleList", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp_FacID = sqlCmd.Parameters.Add("@facilityID", SqlDbType.Int);
            sp_FacID.Value = int.Parse(Request.QueryString["Fac_ID"].ToString());
            SqlParameter sp_FacName = sqlCmd.Parameters.Add("@facilityName", SqlDbType.VarChar, 50);
            sp_FacName.Direction = ParameterDirection.Output;

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxSampleList = new DataSet();

            sqlDa.Fill(dsRxSampleList, "RxSampleList");

            if (dsRxSampleList.Tables[0].Rows.Count > 0)
            {

                DataView dv = new DataView(dsRxSampleList.Tables[0]);
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


                        str = str + "</table><H5 style='page-break-after:always'></H5>"
                             + "<table width='100%' style='font-size:12px; font-family : Arial, Helvetica, Verdana, sans-serif;'> <tr><td width='50%' align='left'><B>Date:</B>" + dv.Table.Rows[i]["RxDate"].ToString() + "</td><td width='50%' align='right'> <B>Entered by:</B>  " + dv.Table.Rows[i]["User"].ToString() + " </td></tr>";

                        str = str + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                      + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + "<tr><td  align='left' colspan='2'> <B>Clinic/Location:&nbsp;&nbsp;</B> " + dv.Table.Rows[i]["Clinic_Name"].ToString() + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() + "</td></tr>"
                      + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                      + getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString());

                    }
                    j = j + 1;

                    if (PageSkipped == true)
                        str = str + "<tr><td width='50%' align='left'><B> Date: </B>" + dv.Table.Rows[i]["RxDate"].ToString() + "</td><td width='50%' align='right'> <B>Entered by: </B> " + dv.Table.Rows[i]["User"].ToString() + " </td></tr>" 
                                + getPat_Printformat(dv.Table.Rows[i]["Pat_ID"].ToString())
                                + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                                + "<tr><td  align='left' colspan='2'> <B>Clinic/Location:&nbsp;&nbsp;</B> " + dv.Table.Rows[i]["Clinic_Name"].ToString() + "-" + dv.Table.Rows[i]["Facility_Name"].ToString() + "</td></tr>"
                                + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr>"
                                + getDoc_Printformat(dv.Table.Rows[i]["Doc_ID"].ToString());

                    str = str + "<tr><td  align='left' colspan='2'>&nbsp;</td></tr><tr><td width='50%' align='left'> <B> " 
                        + j.ToString() 
                        + ".&nbsp;DRUG NAME :&nbsp;&nbsp; </B>" 
                        + dv.Table.Rows[i]["Rx_DrugName"].ToString() 
                        + " (Sample)</td><td width='50%' align='left'> &nbsp; <B> QTY :&nbsp;&nbsp;</B> " 
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
        string str = "";
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand sqlCmd = new SqlCommand("sp_getPatientInfo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par_Pat_ID = sqlCmd.Parameters.Add("@pat_ID", SqlDbType.Int);
            par_Pat_ID.Value = Pat_ID;
            SqlParameter par_Flag = sqlCmd.Parameters.Add("@printStatus", SqlDbType.Int);
            par_Flag.Value = "1";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsRxQueueList = new DataSet();
           

            sqlDa.Fill(dsRxQueueList, "RxQueueList");

            if (dsRxQueueList.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(dsRxQueueList.Tables[0]);
                string address = dv.Table.Rows[0]["Pat_Ship_Address1"].ToString() + " " + dv.Table.Rows[0]["Pat_Address2"].ToString();
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
        string str = "";
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
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
}
