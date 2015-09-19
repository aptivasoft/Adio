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
using System.Drawing;


public partial class Rx30RxLog : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    //Decimal rxCount = 0;

    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        GridViewHelper helper = new GridViewHelper(this.gridRx30Summary);
        helper.RegisterGroup("Region", true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);

        if (!Page.IsPostBack)
        {
            
            //helper.RegisterSummary("NewRx", SummaryOperation.Sum, "Region");
            //helper.RegisterSummary("GoalT", SummaryOperation.Sum, "Region");
            //helper.GroupSummary += new GroupEvent(helper_GroupSummary);

            ddlYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            string[] months = { "January", 
                                "February", 
                                "March", 
                                "April", 
                                "May", 
                                "June", 
                                "July", 
                                "August", 
                                "September", 
                                "October", 
                                "November", 
                                "December",};
            for (int i = 1; i <= DateTime.Now.Month; i++)
            {
                ddlMonth.Items.Add(new ListItem(months[i-1],i.ToString()));
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            Filldata(DateTime.Now.ToString());

            //lblHeading.Text = "Activity Summary - " + DateTime.Now.ToString("MMMM, yyyy");
            lblRX30Heading.Text = "Rx30 Rx LOG - " + DateTime.Now.ToString("MMMM, yyyy");



        }
    }

    void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
    {
        row.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
        row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        row.Cells[1].Text = "Region TOTAL";
    }

    
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Region")
        {
            row.BackColor = System.Drawing.ColorTranslator.FromHtml("#81BEF7");
            row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            row.HorizontalAlign = HorizontalAlign.Left;
            row.Font.Bold = true;
        }
        
        
    }
    
    protected void Filldata(string date)
    {
        objNLog.Info("Function Started with date as parameter...");
        bool successFlag = false;
        SqlConnection sqlCon = new SqlConnection(conStr);

        //SqlCommand sqlCmd = new SqlCommand("sp_getActivitySummary", sqlCon);
        SqlCommand sqlCmd = new SqlCommand();
        
        if (Session["Role"].ToString() == "D")
        {
            SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 15);
            par_UserID.Value = (string)Session["User"];
        }
        SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
        par_Date.Value = date;
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        //DataSet dsRxSummary = new DataSet();
        DataSet dsRx30Summary = new DataSet();
        try
        {
            //sqlDa.Fill(dsRxSummary, "RxQueue");
            //gridRxQueue.DataSource = dsRxSummary;
            //gridRxQueue.DataBind();

            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandText = "sp_get_RX30_RxLog";
            sqlCmd.CommandType = CommandType.StoredProcedure;

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

   
    protected void btnView_Click(object sender, EventArgs e)
    {
        DateTime dt;
        if(ddlMonth.SelectedValue.ToString()=="12")
           dt =new DateTime(int.Parse(ddlYear.SelectedValue)+1,1,1);
        else
            dt = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue.ToString()) + 1, 1);

        dt = dt.AddDays(-1);
        Filldata( dt.ToString());
        //lblHeading.Text = "Activity Summary - " + dt.ToString("MMMM, yyyy");
        lblRX30Heading.Text = "Rx30 Rx LOG - " + dt.ToString("MMMM, yyyy");
       
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
    // To keep track of the previous row Group Identifier
    string strPreviousRowID = string.Empty;
    // To keep track the Index of Group Total
    int intSubTotalIndex = 1;

    // To temporarily store Sub Total
    int intSubTotalNewRx = 0;
    int intSubTotalRefills = 0;
    int intSubTotalGoal = 0;

    int intTenPercentMonthGoalSubTotal = 0;
    int intScripsNeededTenPercentMonthGoalSubTotal = 0;

    int intTwentyFivePercentMonthGoalSubTotal = 0;
    int intScripsNeededTwentyFivePercentMonthGoalSubTotal = 0;
    

    // To temporarily store Grand Total
    int intGrandTotalNewRx = 0;
    int intGrandTotalRefills = 0;
    int intGrandTotalGoal = 0;

    int intTenPercentMonthGoalGrandTotal = 0;
    int intScripsNeededTenPercentMonthGoalGrandTotal = 0;

    int intTwentyFivePercentMonthGoalGrandTotal = 0;
    int intScripsNeededTwentyFivePercentMonthGrandTotal = 0;

    protected void gridRx30Summary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            
            int index = e.Row.RowIndex;
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Decimal rxtotal = Convert.ToDecimal
                //(DataBinder.Eval(e.Row.DataItem, "NewRx"));
                //rxCount = rxCount + rxtotal;

                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "Region").ToString();

                int intNewRx = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NewRx").ToString());
                intSubTotalNewRx += intNewRx;
                intGrandTotalNewRx += intNewRx;

                int intGoalT = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "GoalT").ToString());
                
                double dblAvgMonthlyGoal = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem,"Goal").ToString());
               
                //10% Month Goal Calculations.
                int tenPercentMonthGoal =Convert.ToInt32(1.1 * dblAvgMonthlyGoal);                
                e.Row.Cells[5].Text = tenPercentMonthGoal.ToString();

                int percentageOfTenPercentGoal =0;
                if (tenPercentMonthGoal != 0)
                {
                    percentageOfTenPercentGoal = Convert.ToInt32( (Convert.ToDouble(intNewRx) / Convert.ToDouble(tenPercentMonthGoal)) * 100);
                    e.Row.Cells[6].Text = percentageOfTenPercentGoal.ToString();
                }

                int scripsNeededtenPercentGoal = tenPercentMonthGoal - intNewRx;
                e.Row.Cells[7].Text = scripsNeededtenPercentGoal.ToString();


                //Ten Percent Month Goal Totals
                intTenPercentMonthGoalSubTotal += tenPercentMonthGoal;
                intTenPercentMonthGoalGrandTotal += tenPercentMonthGoal;

                //Scrips Needed 10% Goal Totals
                intScripsNeededTenPercentMonthGoalSubTotal += scripsNeededtenPercentGoal;
                intScripsNeededTenPercentMonthGoalGrandTotal += scripsNeededtenPercentGoal;

                //Ten Percent Achieved Goal Highlighting.                 

                if (percentageOfTenPercentGoal < intGoalT)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.FromName("#FAAFBE");
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.FromName("#000000");
                }
                if (percentageOfTenPercentGoal > intGoalT)
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.FromName("#54C571");
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.FromName("#000000");
                }

                //25% Month Goal Calculations.
                int twentyFivePercentMonthGoal = Convert.ToInt32(1.25 * dblAvgMonthlyGoal);
                e.Row.Cells[8].Text = twentyFivePercentMonthGoal.ToString();

                int percentageOfTwentyFivePercentGoal=0;
                if (twentyFivePercentMonthGoal != 0)
                {
                    percentageOfTwentyFivePercentGoal = Convert.ToInt32((Convert.ToDouble(intNewRx) / Convert.ToDouble(twentyFivePercentMonthGoal)) * 100);
                    e.Row.Cells[9].Text = percentageOfTwentyFivePercentGoal.ToString();
                }

                int scripsNeededtwentyFivePercentGoal = twentyFivePercentMonthGoal - intNewRx;
                e.Row.Cells[10].Text = scripsNeededtwentyFivePercentGoal.ToString();


                //Ten Percent Month Goal Totals
                intTwentyFivePercentMonthGoalSubTotal += twentyFivePercentMonthGoal;
                intTwentyFivePercentMonthGoalGrandTotal += twentyFivePercentMonthGoal;

                //Scrips Needed 10% Goal Totals
                intScripsNeededTwentyFivePercentMonthGoalSubTotal += scripsNeededtwentyFivePercentGoal;
                intScripsNeededTwentyFivePercentMonthGrandTotal += scripsNeededtwentyFivePercentGoal;

                //Twenty Five Percent Achieved Goal Highlighting.                 
                
                if (percentageOfTwentyFivePercentGoal < intGoalT)
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.FromName("#FAAFBE");
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.FromName("#000000");
                }
                if (percentageOfTwentyFivePercentGoal > intGoalT)
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.FromName("#54C571");
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.FromName("#000000");
                }


                //int intRefills = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Refills").ToString());
                //intSubTotalRefills += intRefills;
                //intGrandTotalRefills += intRefills;

                //int intGoal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Goal").ToString());
                //intSubTotalGoal += intGoal;
                //intGrandTotalGoal += intGoal;

                ////Achieved Goal Highlighting.
                //int intGoalP = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "GoalP").ToString());
                //int intGoalT = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "GoalT").ToString());
                //if (intGoalP < intGoalT)
                //{
                //    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                //    e.Row.Cells[5].ForeColor = System.Drawing.Color.FromName("#FFFFFF");
                //}
                //if (intGoalP > intGoalT)
                //{
                //    e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                //    e.Row.Cells[5].ForeColor = System.Drawing.Color.FromName("#FFFFFF");
                //}
            }
            

            string sCatId = gridRx30Summary.DataKeys[index].Values["Clinic_ID"].ToString();
            if (sCatId != "0")
            {
                SqlConnection sqlCon = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand("sp_get_RX30_RxLog", sqlCon);
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

                    nameColumn.HeaderText = "New Rx";

                    nameColumn.DataField = "NewRx";
                    nameColumn.ItemStyle.Width = 50;
                    NewDg.Columns.Add(nameColumn);

                    nameColumn = new BoundColumn();

                    nameColumn.HeaderText = "Refills";

                    nameColumn.DataField = "Refills";
                    nameColumn.ItemStyle.Width = 50;
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

        }

    }

    protected void gridRx30Summary_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Region") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "Region").ToString())
                    IsSubTotalRowNeedToAdd = true;

            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Region") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }

            if (IsSubTotalRowNeedToAdd)
            {
                GridView gridRx30Summary = (GridView)sender;

                // Creating a Row
                GridViewRow SubTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Sub Total";
                HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                HeaderCell.ColumnSpan = 0; // For merging first, second row cells to one
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

                //Adding Subtotal New Rx Column
                HeaderCell = new TableCell();
                HeaderCell.Text = intSubTotalNewRx.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

                //Adding Empty Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

                //Adding Subtotal Ten Percent (10%) Month Goal Column
                HeaderCell = new TableCell();
                HeaderCell.Text = intTenPercentMonthGoalSubTotal.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

                //Adding Empty Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);



                //Adding Subtotal Scrips Needed 10% Goal  
                HeaderCell = new TableCell();
                HeaderCell.Text =  intScripsNeededTenPercentMonthGoalSubTotal.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

                //Adding Subtotal Twenty Five Percent Month Goal 
                HeaderCell = new TableCell();
                HeaderCell.Text = intTwentyFivePercentMonthGoalSubTotal.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

               
                //Adding Empty Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

                //Adding Subtotal Scrips Needed Twenty Five Percent Month Goal 
                HeaderCell = new TableCell();
                HeaderCell.Text = intScripsNeededTwentyFivePercentMonthGoalSubTotal.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "SubTotalRowStyle";
                SubTotalRow.Cells.Add(HeaderCell);

                ////Adding Subtotal Refills Column
                //HeaderCell = new TableCell();
                //HeaderCell.Text = intSubTotalRefills.ToString();
                //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                //HeaderCell.CssClass = "SubTotalRowStyle";
                //SubTotalRow.Cells.Add(HeaderCell);

                ////Adding Subtotal Goal Column
                //HeaderCell = new TableCell();
                //HeaderCell.Text = intSubTotalGoal.ToString();
                //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                //HeaderCell.CssClass = "SubTotalRowStyle";
                //SubTotalRow.Cells.Add(HeaderCell);

                



                //Adding the Row at the RowIndex position in the Grid
                if (e.Row.RowIndex != -1)
                {
                    gridRx30Summary.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex+1, SubTotalRow);
                    intSubTotalIndex++;

                    intSubTotalNewRx = 0;

                    intTenPercentMonthGoalSubTotal = 0;

                    intScripsNeededTenPercentMonthGoalSubTotal = 0;

                    intTwentyFivePercentMonthGoalSubTotal = 0;

                    intScripsNeededTwentyFivePercentMonthGoalSubTotal = 0;

                    intSubTotalGoal = 0;
                }
                else
                {
                    gridRx30Summary.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, SubTotalRow);
                    intSubTotalIndex++;

                }
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                GridView gridRx30Summary = (GridView)sender;

                // Creating a Row
                GridViewRow GrandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Grand Total Cell
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Grand Total";
                HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                HeaderCell.ColumnSpan = 0; // For merging first, second row cells to one
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Grandtotal of New Rx Column
                HeaderCell = new TableCell();
                HeaderCell.Text = intGrandTotalNewRx.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Empty Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Grandtotal of Ten Percent (10%) Month Goal Column
                HeaderCell = new TableCell();
                HeaderCell.Text = intTenPercentMonthGoalGrandTotal.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Empty Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Empty Column
                HeaderCell = new TableCell();
                HeaderCell.Text = intScripsNeededTenPercentMonthGoalGrandTotal.ToString(); 
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Twenty Five Percent Month Goal Grand Total
                HeaderCell = new TableCell();
                HeaderCell.Text = intTwentyFivePercentMonthGoalGrandTotal.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Empty Column
                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                //Adding Scrips Needed Twenty Five Percent Month Grand Total
                HeaderCell = new TableCell();
                HeaderCell.Text = intScripsNeededTwentyFivePercentMonthGrandTotal.ToString();
                HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell.CssClass = "GrandTotalRowStyle";
                GrandTotalRow.Cells.Add(HeaderCell);

                ////Adding Grandtotal of Refills Column
                //HeaderCell = new TableCell();
                //HeaderCell.Text = intGrandTotalRefills.ToString();
                //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                //HeaderCell.CssClass = "GrandTotalRowStyle";
                //GrandTotalRow.Cells.Add(HeaderCell);

                ////Adding Grandtotal of Goal Column
                //HeaderCell = new TableCell();
                //HeaderCell.Text = intGrandTotalGoal.ToString();
                //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                //HeaderCell.CssClass = "GrandTotalRowStyle";
                //GrandTotalRow.Cells.Add(HeaderCell);

                 

                //Adding the Row at the RowIndex position in the Grid
                gridRx30Summary.Controls[0].Controls.AddAt(e.Row.RowIndex, GrandTotalRow);
            }
        }
   
}
