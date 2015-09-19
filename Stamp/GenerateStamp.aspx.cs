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
using Swsim;

public partial class GenerateStamp : System.Web.UI.Page
{

    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    string conStr = ConfigurationManager.AppSettings["conStr"];

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null) 
            {
                Response.Redirect("../Login.aspx"); 
            }

            if (!Page.IsPostBack)
            {
                 
                    rbtnRegular.Attributes.Add("onclick", "document.getElementById('" + divSelect.ClientID + "').style.display='inline';");
                    rbtnSample.Attributes.Add("onclick", "document.getElementById('" + divSelect.ClientID + "').style.display='none';");
                    rbtnPAP.Attributes.Add("onclick", "document.getElementById('" + divSelect.ClientID + "').style.display='none';");

                    txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    //getDrugList();

                    txtShipDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    lblPostalBalance1.Text =String.Format("{0:c}", getPostalBalance());
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
 
    protected void btnTest_Click(object sender, EventArgs e)
    {
        try
        {

            Address toAddress = new Address();
            toAddress.Address1 = txtToAddress1.Text;
            toAddress.Address2 = txtToAddress2.Text;
            toAddress.City = txtToCity.Text;
            toAddress.FirstName = lblPatientFName.Text;
            toAddress.LastName = lblPatientLName.Text;
            toAddress.MiddleName = lblPatientMName.Text;
            toAddress.State = txtToState.Text;
            toAddress.ZIPCode = txtToZip.Text;

            if (checkAddress(ref toAddress))
            {
                Swsim.SwsimV6 swsimobj = new SwsimV6();
                Address fromAddress = new Address();

                fromAddress.Address1 = txtFromAddress1.Text;
                fromAddress.Address2 = txtFromAddress2.Text;
                fromAddress.City = txtFromCity.Text;
                fromAddress.FirstName = txtFromFName.Text;
                fromAddress.LastName = txtFromLName.Text;
                fromAddress.MiddleName = txtFromMName.Text;
                fromAddress.State = txtFromState.Text;
                fromAddress.ZIPCode = txtFromZip.Text;
                string URL;
                RateV3 rate = getRateobj();
                swsimobj.CreateTestIndicium((object)getCredentialObj(), ref rate, fromAddress, toAddress, null, image_type.jpg, EltronPrinterDPIType.Default, out URL);

                string str = "window.open('" + URL + "','SampleStamp');";
                ScriptManager.RegisterStartupScript(btnTest, typeof(UpdatePanel), "alert", str, true);

            }
            else
            {
                string str = "alert('Address InValid! Please Check..');";
                ScriptManager.RegisterStartupScript(imgBtnChkAddr, typeof(UpdatePanel), "alert", str, true);
            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            hplStamp.NavigateUrl = "";
            Address toAddress = new Address();
            toAddress.Address1 = txtToAddress1.Text;
            toAddress.Address2 = txtToAddress2.Text;
            toAddress.City = txtToCity.Text;
            toAddress.FirstName = lblPatientFName.Text;
            toAddress.LastName = lblPatientLName.Text;
            toAddress.MiddleName = lblPatientMName.Text;
            toAddress.State = txtToState.Text;
            toAddress.ZIPCode = txtToZip.Text;

            if (checkAddress(ref toAddress))
            {
                Swsim.SwsimV6 swsimobj = new SwsimV6();
                Address fromAddress = new Address();

                fromAddress.Address1 = txtFromAddress1.Text;
                fromAddress.Address2 = txtFromAddress2.Text;
                fromAddress.City = txtFromCity.Text;
                fromAddress.FirstName = txtFromFName.Text;
                fromAddress.LastName = txtFromLName.Text;
                fromAddress.MiddleName = txtFromMName.Text;
                fromAddress.State = txtFromState.Text;
                fromAddress.ZIPCode = txtFromZip.Text;

                string intgrtxID = ConfigurationManager.AppSettings["stampsIntegrationID"].ToString(), URL, trackNum = "";
                Swsim.PostageBalance postageBalance;
                Guid stampsTxID;
                RateV3 rate = getRateobj();
                
                swsimobj.CreateIndicium((object)getCredentialObj(), ref intgrtxID, ref trackNum, ref rate, fromAddress, toAddress, ConfigurationManager.AppSettings["stampsIntegrationID"].ToString(), null, true, image_type.jpg, EltronPrinterDPIType.Default, txtMessage.Text, out stampsTxID, out URL, out postageBalance);
                hplStamp.NavigateUrl = URL;
                lblPostalBalance1.Text =String.Format("{0:c}",double.Parse( postageBalance.AvailablePostage.ToString()));
                txtRate.Text = String.Format("{0:c}", double.Parse(rate.Amount.ToString()));
                updateTracking(trackNum, URL, txtToAddress.Text, rate.ShipDate.ToString());
                 
                string str = "alert('Stamp Generated Successfully...');"
                                    + "ClientSidePrint('" + URL + "'); ";
                ScriptManager.RegisterStartupScript(btnGenerate , typeof(UpdatePanel), "alert", str, true);

                getDrugList("");
                FillGrid();
                RefreshControls();
            }
            else
            {
                string str = "alert('InValid Address..! Please Check..');";
                ScriptManager.RegisterStartupScript(imgBtnChkAddr , typeof(UpdatePanel), "alert", str, true);
            }
        } 
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            string str = "alert('Error occured..! Please Check Address/Stamp Options.');";
            ScriptManager.RegisterStartupScript(imgBtnChkAddr, typeof(UpdatePanel), "alert", str, true);
        }
    }

    protected void btn_Process_Save_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Function Started with patID as parameter.");
        SqlConnection sqlCon = new SqlConnection(conStr);
        string Query;
        Query = "Update Patient_Info SET [Pat_Ship_Address1]='" + txtToAddress1.Text + "',[Pat_Ship_Address2]='" + txtToAddress2.Text + "',[Pat_Ship_City]='" + txtToCity.Text + "',[Pat_Ship_State]='" + txtToState.Text + "',[Pat_Ship_Zip]='" + txtToZip.Text + "' where Pat_ID=" + hidPatID.Value ;
        SqlCommand sqlcomm = new SqlCommand(Query,sqlCon );

        try
        {
            sqlCon.Open();
            sqlcomm.ExecuteNonQuery();
            sqlCon.Close();
            txtToAddress.Text = lblPatientFName.Text + " " + lblPatientLName.Text + " " + lblPatientMName.Text
                    + "\n" + txtToAddress1.Text + " " + txtToAddress2.Text
                    + "\n" + txtToCity.Text + " " + txtToState.Text + " " + txtToZip.Text;
                
        
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed.");
    }

    protected void btnUpdateFromAddress_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Function Started with patID as parameter.");
        SqlConnection sqlCon = new SqlConnection(conStr);
        string Query;
        Query = "Update [StampAddress] SET [Address1]='" + txtFromAddress1.Text + "',[Address2]='" + txtFromAddress2.Text + "',[City]='" + txtFromCity.Text + "',[State]='" + txtFromState.Text + "',[Zip]='" + txtFromZip.Text + "',[FirstName]='" + txtFromFName.Text + "',[MiddleName]='" + txtFromMName.Text + "',[LastName]='" + txtFromLName.Text + "' where [UserID]='" + (string)Session["User"] + "'";
        SqlCommand sqlcomm = new SqlCommand(Query, sqlCon);

        try
        {
            sqlCon.Open();
            sqlcomm.ExecuteNonQuery();
            sqlCon.Close();
            txtFromAddress.Text = txtFromFName.Text + " " + txtFromLName.Text + " " + txtFromMName.Text
                 + "\n" + txtFromAddress1.Text + " " + txtFromAddress2.Text
                 + "\n" + txtFromCity.Text + " " + txtFromState.Text + " " + txtFromZip.Text;


        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed.");
    }

    protected RateV3 getRateobj()
    {
        RateV3 rate = new RateV3();
        try
        {
           
            rate.FromZIPCode = txtFromZip.Text ;
            rate.ToZIPCode = txtToZip.Text;
            if (txtozWeight.Text.Trim() == "")
                txtozWeight.Text = "0.0";
            if (txtlbsWeight.Text.Trim() == "")
                txtlbsWeight.Text = "0.0";
            rate.WeightOz = double.Parse(txtozWeight.Text);
            rate.WeightLb = double.Parse(txtlbsWeight.Text);
            rate.ShipDate = DateTime.Parse(txtShipDate.Text);
            rate.PackageType = (PackageTypeV2)int.Parse(ddlPackageType.SelectedValue);
            rate.ServiceType = (ServiceType)int.Parse(rbtnServiceType.SelectedValue);
            AddOn[] addon = new AddOn[1];
            addon[0] = new AddOn();
            addon[0].AddOnType = (AddOnType)int.Parse(ddlAddOnType.SelectedValue);
            rate.AddOns = addon;
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return rate;
    }

    protected Credentials getCredentialObj()
    {
        Swsim.Credentials credential = new Swsim.Credentials();
        try
        {

            credential.IntegrationID = new Guid(ConfigurationManager.AppSettings["stampsIntegrationID"].ToString());
            credential.Username = ConfigurationManager.AppSettings["stampsUsername"].ToString();
            credential.Password = ConfigurationManager.AppSettings["stampsPassword"].ToString();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return credential;
    }

    protected double getPostalBalance()
    {
        try
        {
            Swsim.SwsimV6 swsimobj = new SwsimV6();
            AccountInfo accInfo;
            swsimobj.GetAccountInfo((object) getCredentialObj(),out  accInfo);
            return double.Parse(accInfo.PostageBalance.AvailablePostage.ToString());
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        return 0.0d;
    }

    protected Boolean checkAddress(ref Address toAddress)
    {
        try
        {
            Address address = toAddress;

            Boolean AddressMatch, CItyZipOK;
            Swsim.SwsimV6 swsimobj = new SwsimV6();
            swsimobj.CleanseAddress((object)getCredentialObj(), ref address, out AddressMatch, out CItyZipOK);
            if (AddressMatch)
            {
                toAddress.CleanseHash = address.CleanseHash;
                toAddress.OverrideHash = address.OverrideHash;
            }
            else if (CItyZipOK)
            {
                toAddress.OverrideHash = address.OverrideHash;
            }
            if (!CItyZipOK)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            return false ;
        }
        return true;
    }

    protected void gridRx2_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkdrug = (CheckBox)e.Row.Cells[0].FindControl("chkRxNum");
                chkdrug.Attributes.Add("onclick", "addMessage('" + txtMessage.ClientID + "','" + chkdrug.ToolTip.ToString() + "','" + chkdrug.ClientID  + "');");
                upGridRx2.Update();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void updateTracking(string trackingNum, string URL, string shipAddress, string shipDate)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        //string rxType = Request.QueryString["rxType"].ToString();
        string rxType = hidRxType.Value;
        string tableRx30name = "T_Rx30_Rx";
        if (hidPharmID.Value  == "E")
        {
            tableRx30name = "T_Rx30_ET_Rx";
        }
        CheckBox chk;
        string Query = "";
        foreach (GridViewRow gr in gridRx2.Rows)
        {
            chk = gr.FindControl("chkRxNum") as CheckBox;
            if (chk.Checked)
            {
                if (rxType == "R")
                {
                    Query = Query + "INSERT INTO [RxTracking]([RxNbr],[RxTable],[TrackingNum],[URL],[Status],[CreatedBy],[CreatedDate],[ShipDate],ShipAddress,Pat_ID)     VALUES"
                    + "(" + chk.ToolTip.ToString() + ",'" + tableRx30name + "','" + trackingNum + "','" + URL + "','Shipped','" + (string)Session["User"] + "',getdate(),'" + shipDate + "','" + shipAddress  + "','" + hidPatID.Value  + "') ";
                }
                else
                {
                    Query = Query + "INSERT INTO [Rx_Delivery_Tracking] ([Rx_ItemID],[Date_Shipped],[Tracking_Number],[Delivery_Mode],[Shipping_Details],[Ship2Address],[Delivery_Confirmation],[Delivery_Status])     VALUES"
                    + "(" + chk.ToolTip.ToString() + ",'" + shipDate + "','" + trackingNum + "','S','Shipped','" + shipAddress  + "','Shipped','Shipped')";
                }
            }
        }
        
        try
        {
            SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            
            
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void getDrugList(string searchText)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "sp_getShippingDrugs";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;
        DateTime dt = DateTime.Parse(txtDate.Text);
        int year = dt.Year * 1000;
        int date = year + dt.DayOfYear;
        
        string rxType = "R";
        
        if (rbtnPAP.Checked)
            rxType = "P";
        
        if (rbtnSample.Checked)
            rxType = "S";

        if (rxType != "R")
        {
            string str = "document.getElementById('" + divSelect.ClientID + "').style.display='none';";
            ScriptManager.RegisterStartupScript(imgSrcDrugs, typeof(UpdatePanel), "alert", str, true);
        }

        SqlParameter sp_rxType = sqlCmd.Parameters.Add("@rxType", SqlDbType.Char, 1);
        sp_rxType.Value = rxType;
        
        if (rxType == "R")
        {
            SqlParameter sp_Filldate = sqlCmd.Parameters.Add("@Filldate", SqlDbType.Int);
            sp_Filldate.Value = date;
            SqlParameter sp_PharmID = sqlCmd.Parameters.Add("@PharmID", SqlDbType.Char, 1);
            sp_PharmID.Value = rbtnSelect.SelectedValue.ToString();

        }
        else
        {
            SqlParameter sp_date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            sp_date.Value = txtDate.Text;
        }
        if (searchText != "")
        {
            SqlParameter sp_serText = sqlCmd.Parameters.Add("@SearchName", SqlDbType.VarChar, 50);
            sp_serText.Value = searchText;
        }
        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
        DataSet dsDrugs = new DataSet();
        
        try
        {
            da.Fill(dsDrugs);
            if (dsDrugs.Tables.Count > 0)
            {
                if (dsDrugs.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = dsDrugs.Tables[0].NewRow();
                    dr[0] = "No Patients Found..!";
                    dsDrugs.Tables[0].Rows.Add(dr);
                }
            }
            gridRx1.DataSource = dsDrugs;
            gridRx1.DataBind();
            upGridRx1.Update();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void imgSrcDrugs_Click(object sender, ImageClickEventArgs e)
    {
        getDrugList("");
    }

    protected void ddlAddOnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtozWeight.Text == "0.0" && txtlbsWeight.Text == "0.0")
            {
                string  str ="alert('Please Specify Weight..');";
                ScriptManager.RegisterStartupScript(btnGenerate, typeof(UpdatePanel), "alert", str, true);
            }
            else
            {
                GetStampRate();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
            string str = "alert('Error occured..! Please check Address/Weight/Stamp Options');";
            ScriptManager.RegisterStartupScript(btnGenerate, typeof(UpdatePanel), "alert", str, true);
        }
    }

    protected void imgBtnChkAddr_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Address toAddress = new Address();
            toAddress.Address1 = txtToAddress1.Text;
            toAddress.Address2 = txtToAddress2.Text;
            toAddress.City = txtToCity.Text;
            toAddress.FirstName = lblPatientFName.Text;
            toAddress.LastName = lblPatientLName.Text;
            toAddress.MiddleName = lblPatientMName.Text;
            toAddress.State = txtToState.Text;
            toAddress.ZIPCode = txtToZip.Text;

            string str;
            if (checkAddress(ref toAddress))
            {
                str = "alert('Address Valid!');";
                ScriptManager.RegisterStartupScript(imgBtnChkAddr , typeof(Button), "alert", str, true);
            }
            else
            {
                str = "alert('Address InValid! Please Check..');";
                ScriptManager.RegisterStartupScript(imgBtnChkAddr, typeof(Button), "alert", str, true);
            }

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void gridRx1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Generate")
        {
            string rxType = "R";
            
            if (rbtnPAP.Checked) rxType = "P";
            if (rbtnSample.Checked) rxType = "S";
            
            hidPatID.Value = e.CommandArgument.ToString();
            hidRxType.Value = rxType;
            hidPharmID.Value = rbtnSelect.SelectedValue;
            hidRxDate.Value = txtDate.Text;

            FillRxData(hidPatID.Value);
             
            //Response.Redirect("GenerateStamp.aspx?Pat_Id=" + e.CommandArgument.ToString() + "&PharmID=" + rbtnSelect.SelectedValue + "&rxType=" + rxType + "&Date=" + txtDate.Text);
        }
        if (e.CommandName.Equals("AlphaPaging"))
        {

            getDrugList(e.CommandArgument.ToString());
        }
    }

    protected void FillRxData(string patID)
    {
        objNLog.Info("Function Started with patID as parameter.");
        SqlConnection sqlCon = new SqlConnection(conStr);

        try
        {
            RefreshControls();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT [Pat_Ship_Address1],[Pat_FName],[Pat_MName],[Pat_LName],[Pat_Ship_Address2],[Pat_Ship_City],[Pat_Ship_State],[Pat_Ship_Zip]  FROM [Patient_Info] where Pat_ID=" + patID
            //    + " SELECT [FirstName],[MiddleName],[LastName],[Address1],[Address2],[City],[State],[Zip] FROM [StampAddress] where [LocID]= '" + (string)Session["User"] + "'", sqlCon);
            SqlCommand sqlCmd = new SqlCommand("sp_GetStampsLocAddress", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sparm_PatID = sqlCmd.Parameters.Add("@PatID", SqlDbType.Int);
            if (patID != "")
                sparm_PatID.Value = Int32.Parse(patID);
            else
                sparm_PatID.Value = Convert.DBNull;

            SqlParameter sparm_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar,50);
            sparm_UserID.Value = (string)Session["User"];


            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            DataSet dsAddress = new DataSet();
            da.Fill(dsAddress);
            if (dsAddress.Tables[0].Rows.Count > 0)
            {
                lblPatientFName.Text = dsAddress.Tables[0].Rows[0]["Pat_FName"].ToString();
                lblPatientLName.Text = dsAddress.Tables[0].Rows[0]["Pat_LName"].ToString();
                lblPatientMName.Text = dsAddress.Tables[0].Rows[0]["Pat_MName"].ToString();
                txtToAddress1.Text = dsAddress.Tables[0].Rows[0]["Pat_Ship_Address1"].ToString();
                txtToAddress2.Text = dsAddress.Tables[0].Rows[0]["Pat_Ship_Address2"].ToString();
                txtToCity.Text = dsAddress.Tables[0].Rows[0]["Pat_Ship_City"].ToString();
                txtToState.Text = dsAddress.Tables[0].Rows[0]["Pat_Ship_State"].ToString();
                txtToZip.Text = dsAddress.Tables[0].Rows[0]["Pat_Ship_Zip"].ToString();

                txtToAddress.Text = lblPatientFName.Text + " " 
                                    + lblPatientLName.Text + " " 
                                    + lblPatientMName.Text
                    + "\n" + txtToAddress1.Text + " " + txtToAddress2.Text
                    + "\n" + txtToCity.Text + " " + txtToState.Text + " " + txtToZip.Text;
                upAddress.Update();
            }

            if (dsAddress.Tables[1].Rows.Count > 0)
            {
                txtFromFName.Text = dsAddress.Tables[1].Rows[0]["FirstName"].ToString();
                txtFromLName.Text = dsAddress.Tables[1].Rows[0]["LastName"].ToString();
                txtFromMName.Text = dsAddress.Tables[1].Rows[0]["MiddleName"].ToString();
                txtFromAddress1.Text = dsAddress.Tables[1].Rows[0]["Address1"].ToString();
                txtFromAddress2.Text = dsAddress.Tables[1].Rows[0]["Address2"].ToString();
                txtFromCity.Text = dsAddress.Tables[1].Rows[0]["City"].ToString();
                txtFromState.Text = dsAddress.Tables[1].Rows[0]["State"].ToString();
                txtFromZip.Text = dsAddress.Tables[1].Rows[0]["Zip"].ToString();

                txtFromAddress.Text = txtFromFName.Text + " " + txtFromLName.Text + " " + txtFromMName.Text
                  + "\n" + txtFromAddress1.Text + " " + txtFromAddress2.Text
                  + "\n" + txtFromCity.Text + " " + txtFromState.Text + " " + txtFromZip.Text;
                upAddress.Update();
            }
            
            FillGrid();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function completed.");
    }
   
    protected void gridRx1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            TableCell cell = e.Row.Cells[0];
            cell.ColumnSpan = 2;

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

    protected void FillGrid()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "sp_getShippingDrugs";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;
        DateTime dt = DateTime.Parse(hidRxDate.Value);
        int year = dt.Year * 1000;
        int date = year + dt.DayOfYear;
        string rxType = "R";

        rxType = hidRxType.Value;

        SqlParameter sp_PatID = sqlCmd.Parameters.Add("@Pat_Id", SqlDbType.Int);
        sp_PatID.Value = hidPatID.Value;

        SqlParameter sp_rxType = sqlCmd.Parameters.Add("@rxType", SqlDbType.Char, 1);
        sp_rxType.Value = rxType;

        if (rxType == "R")
        {
            SqlParameter sp_Filldate = sqlCmd.Parameters.Add("@Filldate", SqlDbType.Int);
            sp_Filldate.Value = date;
            SqlParameter sp_PharmID = sqlCmd.Parameters.Add("@PharmID", SqlDbType.Char, 1);
            sp_PharmID.Value = hidPharmID.Value;
        }
        else
        {
            SqlParameter sp_date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            sp_date.Value = hidRxDate.Value;
        }

        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
        DataSet dsDrugs = new DataSet();
        try
        {
            da.Fill(dsDrugs);
            gridRx2.DataSource = dsDrugs;
            gridRx2.DataBind();
            upGridRx2.Update();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    private void GetStampRate()
    {

        RateV3 rate = getRateobj();
        RateV3[] Rate;
        Swsim.SwsimV6 swsimobj = new SwsimV6();
        swsimobj.GetRates((object)getCredentialObj(), rate, out Rate);
        if (Rate.Length > 0)
        {
            txtRate.Text = String.Format("{0:c}", double.Parse(Rate[0].Amount.ToString()));

        }

    }

    private void RefreshControls()
    {
        txtMessage.Text = "";
        ddlAddOnType.SelectedIndex = 0;
        txtRate.Text = "";
        ddlPackageType.SelectedIndex = 0;
        txtlbsWeight.Text = "0.0";
        txtozWeight.Text = "0.0";
        rbtnServiceType.SelectedValue = "1";
        txtHeight.Text = "0";
        txtWidth.Text = "0";
        txtLength.Text = "0";
        if (gridRx2.Rows.Count == 0)
        {
            txtFromAddress.Text = "";
            txtToAddress.Text = "";
        }

    }
    
}
