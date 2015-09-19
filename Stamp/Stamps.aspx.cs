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

public partial class Stamps : System.Web.UI.Page
{

    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null) { Response.Redirect("../Login.aspx"); }

          
            if (!Page.IsPostBack)
            {
                rbtnRegular.Attributes.Add("onclick", "document.getElementById('" + divSelect.ClientID + "').style.display='inline';");
                rbtnSample.Attributes.Add("onclick", "document.getElementById('" + divSelect.ClientID + "').style.display='none';");
                rbtnPAP.Attributes.Add("onclick", "document.getElementById('" + divSelect.ClientID + "').style.display='none';");

                txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                getDrugList();
                
            }
            //if (Request.QueryString["RxItemID"] != null)
                
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void gridRx_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "Generate")
        {
            string rxType = "R";
        if (rbtnPAP.Checked)
            rxType = "P";
        if (rbtnSample.Checked)
            rxType = "S";

        Response.Redirect("GenerateStamp.aspx?Pat_Id=" + e.CommandArgument.ToString() + "&PharmID=" + rbtnSelect.SelectedValue + "&rxType=" + rxType + "&Date=" + txtDate.Text);
        }
    }
   
    protected void btnview_Click(object sender, EventArgs e)
    {

        getDrugList();
    }

    protected void getDrugList()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "sp_getShippingDrugs";// "select Doc_ID from Doctor_Info where Status<>'N' and Doc_LName='" + DocLName + "'";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Connection = sqlCon;
        DateTime dt = DateTime.Parse(txtDate.Text);
        int year = dt.Year * 1000;
        int date = year + dt.DayOfYear;
        string rxType = "R", Query = "";
        if (rbtnPAP.Checked)
            rxType = "P";
        if (rbtnSample.Checked)
            rxType = "S";

        if (rxType != "R")
        {
            string str = "document.getElementById('" + divSelect.ClientID + "').style.display='none';";
            ScriptManager.RegisterStartupScript(btnview, typeof(UpdatePanel), "alert", str, true);
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

        //string tableRx30name = "T_Rx30_Rx", tableRx30Drug = "T_Rx30_Drug", rxPatID = "Rx30PID";
        //if (rbtnSelect.SelectedValue.ToString() == "E")
        //{
        //    tableRx30name = "T_Rx30_ET_Rx";
        //    tableRx30Drug = "T_Rx30_ET_Drug";
        //    rxPatID = "Rx30ETPID";

        //}

        //if (rxType == "R")
        //{
        //    Query = "select (select PInfo.Pat_Lname + ',' + PInfo.Pat_Fname from patient_Info as PInfo where PInfo.Pat_Id=temp.Pat_ID) as Patient,count(temp.Pat_ID) as Drugs,temp.Pat_ID from( "
        //    + "Select Rx30.RxNbr,count(distinct Rx30.RxNbr) as Drugs,Patient_Info.Pat_ID from Patient_Rx," + tableRx30name + " as Rx30,Patient_Info where Patient_Info." + rxPatID + "=Rx30.PatNbrKey and Rx30.FillDate=" + date + "  group by Patient_Info.Pat_ID ,Rx30.RxNbr) as temp group by temp.Pat_ID";
        //}
        //else
        //{
        //    Query = "Select (select PInfo.Pat_Lname + ',' + PInfo.Pat_Fname from patient_Info as PInfo where PInfo.Pat_Id=Patient_Info.Pat_ID) as Patient,count(Patient_Info.Pat_ID) as Drugs,Patient_Info.Pat_ID from Patient_Rx,rx_Drug_Info,Patient_Info where Patient_Info.Pat_ID=Patient_Rx.Pat_ID and Patient_Rx.Rx_ID=rx_Drug_Info.Rx_ID and rx_Drug_Info.Rx_Type='" + rxType + "' and convert(Date,Patient_Rx.Rx_Date,0) = convert(Date,'" + txtDate.Text + "',0) and rx_Drug_Info.Rx_ItemID not in ( select Rx_ItemID from Rx_Delivery_Tracking) group by Patient_Info.Pat_ID ";
        //}
        SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
        DataSet dsDrugs = new DataSet();
        try
        {

            da.Fill(dsDrugs);
            gridRx.DataSource = dsDrugs;
            gridRx.DataBind();



        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
}
