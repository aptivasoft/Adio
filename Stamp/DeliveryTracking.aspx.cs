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

public partial class DeliveryTracking : System.Web.UI.Page
{

    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null) { Response.Redirect("../Login.aspx"); }

            if (!Page.IsPostBack)
            {   }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        fillData();
    }
    
    protected void btnTrack_Click(object sender, EventArgs e)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "";// "select Doc_ID from Doctor_Info where Status<>'N' and Doc_LName='" + DocLName + "'";
        sqlCmd.Connection = sqlCon;
        DateTime dt = DateTime.Parse(txtDate.Text);
        int year = dt.Year * 1000;
        int date = year + dt.DayOfYear;
        string rxType = "R", Query = "";
        if (rbtnPAP.Checked)
            rxType = "P";
        if (rbtnSample.Checked)
            rxType = "S";
        string tableRx30name = "T_Rx30_Rx", tableRx30Drug = "T_Rx30_Drug", rxPatID = "Rx30PID";
        //if ("E" == "E")
        //{
        //    tableRx30name = "T_Rx30_ET_Rx";
        //    tableRx30Drug = "T_Rx30_ET_Drug";
        //    rxPatID = "Rx30ETPID";
        //}

        if (rxType == "R")
        {
            Query = "Select distinct RxTracking.TrackingNum as TrackingNumber from RxTracking ";//where RxTracking.RxNbr = Rx30.RxNbr ";
        }
        else
        {
            Query = "Select distinct Rx_Delivery_Tracking.Tracking_Number as TrackingNumber from Rx_Delivery_Tracking";// where Rx_Delivery_Tracking.Rx_ItemID";
        }

        SqlDataAdapter da = new SqlDataAdapter(Query, sqlCon);
        DataSet dsTracking = new DataSet();
        
        try
        {
            da.Fill(dsTracking);
            foreach (DataRow dr in dsTracking.Tables[0].Rows)
            {
                UpdateTrackingStatus(dr["TrackingNumber"].ToString());
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }


    protected void UpdateTrackingStatus(string trackingNum)
    {
        try
        {
            TrackingEvent[] tEvent;
            Swsim.SwsimV6 swsimobj = new SwsimV6();
            swsimobj.TrackShipment((object)getCredentialObj(), (object)trackingNum, out tEvent);
            string status = tEvent[0].Event.ToString(); 
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message); 
        }
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

    protected void fillData()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "";// "select Doc_ID from Doctor_Info where Status<>'N' and Doc_LName='" + DocLName + "'";
        sqlCmd.Connection = sqlCon;
        DateTime dt = DateTime.Parse(txtDate.Text);
        int year = dt.Year * 1000;
        int date = year + dt.DayOfYear;
        string rxType = "R", Query = "";
        if (rbtnPAP.Checked)
            rxType = "P";
        if (rbtnSample.Checked)
            rxType = "S";
        string tableRx30name = "T_Rx30_Rx", tableRx30Drug = "T_Rx30_Drug", rxPatID = "Rx30PID";
        //if ("E" == "E")
        //{
        //    tableRx30name = "T_Rx30_ET_Rx";
        //    tableRx30Drug = "T_Rx30_ET_Drug";
        //    rxPatID = "Rx30ETPID";

        //}

        if (rxType == "R")
        {

            Query = "Select (select PInfo.Pat_Lname + ',' + PInfo.Pat_Fname from patient_Info as PInfo where PInfo.Pat_Id=Patient_Info.Pat_ID) as Patient,RxTracking.Status,RxTracking.ShipDate,Rx30Drug.Name as Drugs,Rx30.RxQty as Qty,RxTracking.RxTable from Patient_Rx," + tableRx30name + " as Rx30," + tableRx30Drug + " as Rx30Drug,Patient_Info,RxTracking where RxTracking.RxNbr = Rx30.RxNbr and RxTracking.RxTable='" + tableRx30name + "' and Patient_Info." + rxPatID + "=Rx30.PatNbrKey and convert(Date,RxTracking.ShipDate,0) = convert(Date,'" + txtDate.Text + "',0)  and Rx30Drug.DrugNbrKey=Rx30.DispensedDrugKey and  RxTracking.RxTable='" + tableRx30name + "'";

            tableRx30name = "T_Rx30_ET_Rx";
            tableRx30Drug = "T_Rx30_ET_Drug";
            rxPatID = "Rx30ETPID";

            Query = Query + "UNION Select (select PInfo.Pat_Lname + ',' + PInfo.Pat_Fname from patient_Info as PInfo where PInfo.Pat_Id=Patient_Info.Pat_ID) as Patient,RxTracking.Status,RxTracking.ShipDate,Rx30Drug.Name as Drugs,Rx30.RxQty as Qty,RxTracking.RxTable from Patient_Rx," + tableRx30name + " as Rx30," + tableRx30Drug + " as Rx30Drug,Patient_Info,RxTracking where RxTracking.RxNbr = Rx30.RxNbr and RxTracking.RxTable='" + tableRx30name + "' and Patient_Info." + rxPatID + "=Rx30.PatNbrKey and convert(Date,RxTracking.ShipDate,0) = convert(Date,'" + txtDate.Text + "',0)  and Rx30Drug.DrugNbrKey=Rx30.DispensedDrugKey and  RxTracking.RxTable='" + tableRx30name + "'";
        }
        else
        {
            Query = "Select (select PInfo.Pat_Lname + ',' + PInfo.Pat_Fname from patient_Info as PInfo where PInfo.Pat_Id=Patient_Info.Pat_ID) as Patient,Rx_Delivery_Tracking.Delivery_Status as Status,Rx_Delivery_Tracking.Date_Shipped as ShipDate,rx_Drug_Info.Rx_DrugName as Drugs,rx_Drug_Info.Rx_Qty as Qty from Patient_Rx,rx_Drug_Info,Patient_Info,Rx_Delivery_Tracking where Rx_Delivery_Tracking.Rx_ItemID=rx_Drug_Info.Rx_ItemID and  Patient_Info.Pat_ID=Patient_Rx.Pat_ID and Patient_Rx.Rx_ID=rx_Drug_Info.Rx_ID and rx_Drug_Info.Rx_Type='" + rxType + "' and convert(Date,Rx_Delivery_Tracking.Date_Shipped,0) = convert(Date,'" + txtDate.Text + "',0) ";
        }
        SqlDataAdapter da = new SqlDataAdapter(Query, sqlCon);
        DataSet dsTracking = new DataSet();
        try
        {

            da.Fill(dsTracking);
            gridTracking.DataSource = dsTracking;
            gridTracking.DataBind();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
}
