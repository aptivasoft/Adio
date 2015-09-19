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


public partial class ActivitySummary : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");
 
        if (!Page.IsPostBack)
        {
            ddlYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", };
            for (int i = 1; i <= DateTime.Now.Month; i++)
            {
                ddlMonth.Items.Add(new ListItem(months[i-1],i.ToString()));
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            Filldata(DateTime.Now.ToString());

            lblHeading.Text = "Activity Summary - " + DateTime.Now.ToString("MMMM, yyyy");
            lblRX30Heading.Text = "Rx30 Activity Summary - " + DateTime.Now.ToString("MMMM, yyyy");
        }
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Clinic_Name")
        {
            row.BackColor =System.Drawing.ColorTranslator.FromHtml("#b5d1e8");
            row.HorizontalAlign = HorizontalAlign.Left;
        }
        
    }
    
    protected void Filldata(string date)
    {
        objNLog.Info("Function Started with date as parameter...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_getActivitySummary", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        if (Session["Role"].ToString() == "D")
        {
            SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 15);
            par_UserID.Value = (string)Session["User"];
        }
        SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
        par_Date.Value = date;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataSet dsRxSummary = new DataSet();
        DataSet dsRx30Summary = new DataSet();
        try
        {
            sqlDa.Fill(dsRxSummary, "RxQueue");

            gridRxQueue.DataSource = dsRxSummary;
            gridRxQueue.DataBind();

            sqlCmd.CommandText = "sp_get_RX30_ActivitySummary";
            sqlDa = new SqlDataAdapter(sqlCmd);

            sqlDa.Fill(dsRx30Summary, "Rx30Queue");

            gridRx30Summary.DataSource = dsRx30Summary;
            gridRx30Summary.DataBind();
            successFlag = true;
        }
        catch (SqlException SqlEx)
        {
            objNLog.Error("SQLException : " + SqlEx.Message);
             
        }
        catch (Exception ex)
        {
            objNLog.Error("Exception : " + ex.Message);
          
        }
        finally
        {
            sqlCon.Close();
            objNLog.Info("Finally Block: " + successFlag);
        }
        objNLog.Info("Function Completed...");
    }

    protected void gridRxQueue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Summary")
        {
            Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Payment")
        {
            Response.Redirect("RxPayment.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Sample")
        {
            Response.Redirect("RxSample.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "PAP")
        {
            Response.Redirect("RxPAP.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
    }

    protected void gridRxQueue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        bool successFlag = false;
        if (e.Row.RowIndex != -1)
        {
            int index = e.Row.RowIndex;

            string sCatId = gridRxQueue.DataKeys[index].Values["Clinic_ID"].ToString();
            if (sCatId != "0")
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("sp_getActivitySummary", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                if (Session["Role"].ToString() == "D")
                {
                    SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 15);
                    par_UserID.Value = (string)Session["User"];
                }
                SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
                par_ClinicID.Value = sCatId;

                SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
                DateTime dt;
                if (ddlMonth.SelectedValue.ToString() == "12")
                    dt = new DateTime(int.Parse(ddlYear.SelectedValue) + 1, 1, 1);
                else
                    dt = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue.ToString()) + 1, 1);
                dt = dt.AddDays(-1);
                par_Date.Value = dt.ToString();

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet dsRxQueue = new DataSet();
                try
                {
                    sqlDa.Fill(dsRxQueue, "RxQueue");
                    DataGrid NewDg = new DataGrid();
                    NewDg.AutoGenerateColumns = false;
                    NewDg.HeaderStyle.CssClass = "medication_info_th1";
                    NewDg.ItemStyle.CssClass = "medication_info_tr-odd";
                    NewDg.ItemStyle.Font.Bold = false;
                    NewDg.AlternatingItemStyle.Font.Bold = false;
                    NewDg.AlternatingItemStyle.CssClass = "medication_info_tr-even";
                    NewDg.ShowHeader = false;
                    
                    BoundColumn nameColumn = new BoundColumn();
                    nameColumn.HeaderText = "Location";
                    nameColumn.DataField = "Facility_Name";
                    nameColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    nameColumn.ItemStyle.Width = 250;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();
                    nameColumn.HeaderText = "New Patients";
                    nameColumn.DataField = "NewPatients";
                    nameColumn.ItemStyle.Width = 100;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "New Rx";

                    nameColumn.DataField = "NewRx";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "Refills";

                    nameColumn.DataField = "Refills";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "Sample";

                    nameColumn.DataField = "Sample";
                    nameColumn.ItemStyle.Width = 100;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "PAP";

                    nameColumn.DataField = "PAP";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "Payments";

                    nameColumn.DataField = "Payments";
                    nameColumn.ItemStyle.Width = 100;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "S/P Billing";

                    nameColumn.DataField = "SPBilling";
                    nameColumn.ItemStyle.Width = 100;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();
                    nameColumn.HeaderText = "Med Reqs";

                    nameColumn.DataField = "MedRequests";
                    nameColumn.ItemStyle.Width = 61;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();
                    nameColumn.HeaderText = "Other";

                    nameColumn.DataField = "Other";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);


                    NewDg.Width = Unit.Percentage(100.00);
                    NewDg.DataSource = dsRxQueue;
                    NewDg.DataBind();

                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                    NewDg.RenderControl(htw);


                    string DivStart = "<DIV id='uniquename" + e.Row.RowIndex.ToString() + "' style='DISPLAY: none; HEIGHT: 1px;'>";
                    string DivBody = sw.ToString();
                    string DivEnd = "</DIV>";

                    string FullDIV = DivStart + DivBody + DivEnd;


                    int LastCellPosition = e.Row.Cells.Count - 1;
                    int NewCellPosition = e.Row.Cells.Count;

                    e.Row.Cells[0].ID = "CellInfo" + e.Row.RowIndex.ToString();

                     e.Row.Cells[LastCellPosition].Text = e.Row.Cells[LastCellPosition].Text +
                                      "</td><tr><td ></td><td colspan='" +
                                      NewCellPosition + "'>" + FullDIV;
                    
                    e.Row.Cells[0].Text = @"&nbsp;&nbsp;<A onclick=""togglePannelStatus('uniquename" +
                                    e.Row.RowIndex.ToString() + "',this);" + @""" onmouseover=""this.style.cursor='hand'"" onmouseout=""this.style.cursor='hand'"" ><B>+</B></A>&nbsp;";
                    successFlag = true;
                }
                catch (SqlException SqlEx)
                {
                    objNLog.Error("SQLException : " + SqlEx.Message);
                    throw new Exception("Exception re-Raised from DL with SQLError# " + SqlEx.Number + " ", SqlEx);
                }
                catch (Exception ex)
                {
                    objNLog.Error("Exception : " + ex.Message);
                    throw new Exception("**Error occured while  Binding Activity Summary.", ex);
                }
                finally
                {
                    sqlCon.Close();
                    objNLog.Info("Finally Block: " + successFlag);
                }
            }
            else
            {
                
                e.Row.Cells[0].Text = "";
            }
            Label lb=(Label) e.Row.Cells[7].FindControl("lblPayments");
            if (lb.Text == "")
                lb.Text = "0.00";
            Label lb1 = (Label)e.Row.Cells[8].FindControl("lblBilling");
            if (lb1.Text == "")
                lb1.Text = "0.00";
        }
    }
   
    public void SetProps(System.Web.UI.WebControls.DataGrid DG) 
{
    DG.Font.Size = 8; 
    DG.Font.Bold = false; 
    DG.Font.Name = "tahoma";
    
    /*******************Professional 2********************/
    //Border Props 
    DG.GridLines = GridLines.Both; 
    DG.CellPadding = 3; 
    DG.CellSpacing = 0; 
    DG.BorderColor = System.Drawing.Color.FromName("#CCCCCC"); 
    DG.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1); 

 
    //Header Props 
    DG.HeaderStyle.BackColor = System.Drawing.Color.SteelBlue; 
    DG.HeaderStyle.ForeColor = System.Drawing.Color.White; 
    DG.HeaderStyle.Font.Bold = true; 
    DG.HeaderStyle.Font.Size = 8; 
    DG.HeaderStyle.Font.Name = "tahoma"; 
    DG.ItemStyle.BackColor = System.Drawing.Color.LightSteelBlue; 
}
  
    protected void gridRx30Summary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Summary")
        {
            Response.Redirect("RxSummary.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Payment")
        {
            Response.Redirect("RxPayment.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Sample")
        {
            Response.Redirect("RxSample.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "PAP")
        {
            Response.Redirect("RxPAP.aspx?Fac_ID=" + e.CommandArgument.ToString());
        }
    }

    protected void gridRx30Summary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            int index = e.Row.RowIndex;

            string sCatId = gridRx30Summary.DataKeys[index].Values["Clinic_ID"].ToString();
            if (sCatId != "0")
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("sp_get_RX30_ActivitySummary", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                if (Session["Role"].ToString() == "D")
                {
                    SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 15);
                    par_UserID.Value = (string)Session["User"];
                }
                SqlParameter par_ClinicID = sqlCmd.Parameters.Add("@Clinic_ID", SqlDbType.Int);
                par_ClinicID.Value = sCatId;

                SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
                DateTime dt;
                if (ddlMonth.SelectedValue.ToString() == "12")
                    dt = new DateTime(int.Parse(ddlYear.SelectedValue) + 1, 1, 1);
                else
                    dt = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue.ToString()) + 1, 1);
                dt = dt.AddDays(-1);
                par_Date.Value = dt.ToString();

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                DataSet dsRxQueue = new DataSet();
                try
                {
                    sqlDa.Fill(dsRxQueue, "RxQueue");
                    DataGrid NewDg = new DataGrid();
                    NewDg.AutoGenerateColumns = false;
                    NewDg.HeaderStyle.CssClass = "medication_info_th1";
                    NewDg.ItemStyle.CssClass = "medication_info_tr-odd";
                    NewDg.ItemStyle.Font.Bold = false;
                    NewDg.AlternatingItemStyle.Font.Bold = false;
                    NewDg.AlternatingItemStyle.CssClass = "medication_info_tr-even";
                    NewDg.ShowHeader = false;

                    BoundColumn nameColumn = new BoundColumn();
                    nameColumn.HeaderText = "Location";
                    nameColumn.DataField = "Facility_Name";
                    nameColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    nameColumn.ItemStyle.Width = 250;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();
                    nameColumn.HeaderText = "New Patients";
                    nameColumn.DataField = "NewPatients";
                    nameColumn.ItemStyle.Width = 100;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "New Rx";

                    nameColumn.DataField = "NewRx";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "Refills";

                    nameColumn.DataField = "Refills";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "Sample";

                    nameColumn.DataField = "Sample";
                    nameColumn.ItemStyle.Width = 100;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "PAP";

                    nameColumn.DataField = "PAP";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "Payments";

                    nameColumn.DataField = "Payments";
                    nameColumn.ItemStyle.Width = 100;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();
                    nameColumn.HeaderText = "Med Reqs";

                    nameColumn.DataField = "MedRequests";
                    nameColumn.ItemStyle.Width = 101;
                    NewDg.Columns.Add(nameColumn);


                    NewDg.Width = Unit.Percentage(100.00);
                    NewDg.DataSource = dsRxQueue;
                    NewDg.DataBind();
                    
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                    NewDg.RenderControl(htw);


                    string DivStart = "<DIV id='uniquenameRX" + e.Row.RowIndex.ToString() + "' style='DISPLAY: none; HEIGHT: 1px;'>";
                    string DivBody = sw.ToString();
                    string DivEnd = "</DIV>";

                    string FullDIV = DivStart + DivBody + DivEnd;


                    int LastCellPosition = e.Row.Cells.Count - 1;
                    int NewCellPosition = e.Row.Cells.Count;

                    e.Row.Cells[0].ID = "CellInfo" + e.Row.RowIndex.ToString();

                        e.Row.Cells[LastCellPosition].Text = e.Row.Cells[LastCellPosition].Text +
                                      "</td><tr><td ></td><td colspan='" +
                                      NewCellPosition + "'>" + FullDIV;
                    e.Row.Cells[0].Text = @"&nbsp;&nbsp;<A onclick=""togglePannelStatus('uniquenameRX" +
                                    e.Row.RowIndex.ToString() + "',this);" + @""" onmouseover=""this.style.cursor='hand'"" onmouseout=""this.style.cursor='hand'"" ><B>+</B></A>&nbsp;";
                }
                catch (Exception ex)
                {
                    objNLog.Error("Error : " + ex.Message);
                }
            }
            else
            {

                e.Row.Cells[0].Text = "";
            }
            Label lb = (Label)e.Row.Cells[7].FindControl("lblPayments");
            if (lb.Text == "")
                lb.Text = "0.00";
        }
    }
   
    protected void btnView_Click(object sender, EventArgs e)
    {
        DateTime dt;
        if(ddlMonth.SelectedValue.ToString()=="12")
           dt =new DateTime(int.Parse(ddlYear.SelectedValue)+1,1,1);
        else
            dt = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue.ToString()) + 1, 1);

        dt = dt.AddDays(-1);
        Filldata( dt.ToString());
        lblHeading.Text = "Activity Summary - " + dt.ToString("MMMM, yyyy");
        lblRX30Heading.Text = "Rx30 Activity Summary - " + dt.ToString("MMMM, yyyy");
       
    }
    
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", };
        int maxMonth = DateTime.Now.Month;
        if (ddlYear.SelectedIndex > 0)
            maxMonth = 12;
        ddlMonth.Items.Clear();
        for (int i = 1; i <= maxMonth; i++)
        {
            ddlMonth.Items.Add(new ListItem(months[i - 1], i.ToString()));
        }
    }
}
