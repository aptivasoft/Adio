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
using System.Data.SqlClient;
using System.ComponentModel;
using NLog;

public partial class RxSummary : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            Filldata();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void Filldata()
    {
        objNLog.Info("Function Started...");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_getRxSummary", sqlCon);
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
            DataSet dsRxSummary = new DataSet();

            sqlDa.Fill(dsRxSummary, "RxSummary");
            gridRxSummary.DataSource = dsRxSummary;
            gridRxSummary.DataBind();

            if (Request.QueryString["RxDate"] != null)
            {
            string rxDate = Request.QueryString["RxDate"].ToString();

            DateTime dt = (DateTime)(TypeDescriptor.GetConverter(new DateTime(1990, 5, 6)).ConvertFrom(rxDate));
           
            lblHeading.Text = "Rx Summary for the month of " +  dt.ToString("MMMM yyyy") + " - " + sp_FacName.Value.ToString();
            
            }
            else
              lblHeading.Text = "Rx Summary for the month of " + DateTime.Now.ToString("MMMM yyyy") + " - " + sp_FacName.Value.ToString();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed...");
    }

 

    
    protected void gridPrisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
 
        }
    }
     
}
