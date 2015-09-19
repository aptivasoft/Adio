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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Mail;
using NLog;
public partial class Patient_Timesheet : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    TimeSheetDAL TS_Data = new TimeSheetDAL();
    TimeSheetDetails TS_Det = new TimeSheetDetails();
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null || Session["Role"] == null)
                Response.Redirect("../Login.aspx");

            if (!IsPostBack)
            {
                //BindDateAndDay();  
                Filldata();
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }

    protected void Filldata()
    {
        objNLog.Info("Function Started..");
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);

            //string sqlQuery = "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

            SqlCommand sqlCmd = new SqlCommand("sp_getTimeSheet", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_PPID = sqlCmd.Parameters.Add("@PPID", SqlDbType.Int);
            par_PPID.Value = Request.QueryString["PPID"];
            SqlParameter par_EmpID = sqlCmd.Parameters.Add("@empId", SqlDbType.Int);
            par_EmpID.Value = getEmpID((string)Session["User"]);
            SqlParameter par_THours = sqlCmd.Parameters.Add("@THours", SqlDbType.Int);
            par_THours.Direction = ParameterDirection.Output;
            //SqlParameter par_FacilityID = sqlCmd.Parameters.Add("@facility_ID", SqlDbType.Int);
            //par_FacilityID.Value = FacilityID;
            //SqlParameter par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
            //par_Date.Value = DateTime.Parse(date);
            //SqlParameter par_RxType = sqlCmd.Parameters.Add("@RxType", SqlDbType.Char, 1);
            //par_RxType.Value = ddlRxType.SelectedValue;



            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsTimesheet = new DataSet();

            sqlDa.Fill(dsTimesheet);
            gridTimeSheetList.DataSource = dsTimesheet.Tables[0];
            gridTimeSheetList.DataBind();
            TextBox tb;
            tb = (TextBox)gridTimeSheetList.FooterRow.FindControl("txtTHours");
            tb.Text = par_THours.Value.ToString();

            if (dsTimesheet.Tables[1].Rows.Count > 0)
            {

                lblempName1.Text = dsTimesheet.Tables[1].Rows[0]["empName"].ToString();
                lblPhone1.Text = dsTimesheet.Tables[1].Rows[0]["Emp_Phone"].ToString();
                lblMailId1.Text = dsTimesheet.Tables[1].Rows[0]["Emp_Email"].ToString();
                lblSupervisor1.Text = dsTimesheet.Tables[1].Rows[0]["Sup1"].ToString();
                AltlblSupervisor1.Text = dsTimesheet.Tables[1].Rows[0]["Sup2"].ToString();
                //lblempName1.Text = dsTimesheet.Tables[1].Rows[0][""].ToString();
                //lblempName1.Text = dsTimesheet.Tables[1].Rows[0][""].ToString();
                //lblempName1.Text = dsTimesheet.Tables[1].Rows[0][""].ToString();



            }
            string mode = (string)Request.QueryString["mode"];
            if (mode == "view")
            {
                btnCancel.Visible = false;
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                tb.Visible = false;
                Label lb;
                lb = (Label)gridTimeSheetList.FooterRow.FindControl("lblTHours");
                lb.Text = par_THours.Value.ToString();

                if (dsTimesheet.Tables[2].Rows.Count > 0)
                {
                    if (dsTimesheet.Tables[2].Rows[0][0].ToString() != "C" && dsTimesheet.Tables[2].Rows[0][0].ToString() != "")
                    {
                        lblSubmittedby.Text = "<b>Submitted By:</b> " + lblempName1.Text;
                        lblSubmittedDate.Text = "<b>Date/Time :</b> " + dsTimesheet.Tables[2].Rows[0][1].ToString();
                        if (dsTimesheet.Tables[2].Rows[0][0].ToString() == "A")
                        {

                            lblApprovedby.Text = "<b>Approved/Rejected By:</B> " + dsTimesheet.Tables[2].Rows[0][2].ToString();
                            lblApprovedDate.Text = "<b>Date/Time :</B> " + dsTimesheet.Tables[2].Rows[0][3].ToString();
                        }

                    }
                }
                //gridTimeSheetList.FooterRow.Visible = false;

            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetACodes(string prefixText, int count)
    {
        List<string> User_List = new List<string>();
        RegisterUserDAL reg_Usr_DAL = new RegisterUserDAL();

        User_List.Clear();

        DataTable dtFac = reg_Usr_DAL.get_ACodes(prefixText, count);
        foreach (DataRow dr in dtFac.Rows)
        {
            User_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString()+ "-" + dr[1].ToString(), dr[0].ToString()));
        }

        return User_List.ToArray();
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetABCodes(string prefixText, int count)
    {
        List<string> User_List = new List<string>();
        RegisterUserDAL reg_Usr_DAL = new RegisterUserDAL();

        User_List.Clear();

        DataTable dtFac = reg_Usr_DAL.get_ABCodes(prefixText, count);
        foreach (DataRow dr in dtFac.Rows)
        {
            User_List.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString() + "-" + dr[1].ToString(), dr[0].ToString()));
        }

        return User_List.ToArray();
    }

    public void BindDateAndDay()
    {
        objNLog.Info("Function Started..");
        try
        {
            TS_Det.User_ID = (string)Session["User"];
            TS_Det.EMP_ID = TS_Data.GetEmpID(TS_Det);

            DataTable dt_EmpData = TS_Data.GetEmpDetails(TS_Det);

            lblempName1.Text = dt_EmpData.Rows[0]["Emp_LName"].ToString() + "," + dt_EmpData.Rows[0]["Emp_FName"].ToString();
            lblPhone1.Text = dt_EmpData.Rows[0]["Emp_Phone"].ToString();
            lblMailId1.Text = dt_EmpData.Rows[0]["Emp_Email"].ToString();
            TimeSheetDetails TS_Det1 = new TimeSheetDetails();
            TS_Det1.EMP_ID = int.Parse(dt_EmpData.Rows[0]["Emp_SupID1"].ToString());
            lblSupervisor1.Text = (TS_Data.GetEmpDetails(TS_Det)).Rows[0]["Emp_LName"].ToString() + "," + dt_EmpData.Rows[0]["Emp_FName"].ToString();
            TS_Det.LocID = int.Parse(dt_EmpData.Rows[0]["Emp_LocID"].ToString());
            lblLocation1.Text = TS_Data.GetLocation(TS_Det);

            TextBox txthrs, txtout, txtAsbCode, txtActvCode, txtCmnts;
            txthrs = new TextBox();
            txtout = new TextBox();
            txtActvCode = new TextBox();
            txtCmnts = new TextBox();
            txtAsbCode = new TextBox();

            if (Request.QueryString["PPID"] != null)// emp is saved his info prev.
            {
                TS_Det.PayPeriodID = Int32.Parse(Request.QueryString["PPID"].ToString());

                DataTable dt_DateDur = TS_Data.GetDateDuration(TS_Det);

                DateTime dt_StartDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_StartDate"].ToString());
                DateTime dt_EndDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_EndDate"].ToString());

                TS_Det.TimeSheetID = int.Parse(TS_Data.Get_EmpTimeSheetID(TS_Det).Rows[0]["TS_Id"].ToString());

                DataTable dt_EMPTSDetails = TS_Data.Get_EmpTimeSheetDetails(TS_Det);
                //test
                int dur = (dt_EndDate.Day - dt_StartDate.Day) + 1;
                int rel = dt_EMPTSDetails.Rows.Count;

                DateTime Cur_Date = dt_StartDate;
                for (int i = 0, j = 1; i < (dt_EndDate.Day - dt_StartDate.Day) + 1; i++, j++)
                {
                    Label lblDatesetter = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("lblDate" + j) as Label;
                    lblDatesetter.Visible = true;
                    Label lblDaysetter = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("lblDay" + j) as Label;
                    lblDaysetter.Visible = true;

                    txthrs = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Hours") as TextBox;
                    txtout = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "OTUT") as TextBox;
                    txtAsbCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "AbsentCode") as TextBox;
                    txtActvCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "ActivetyCode") as TextBox;
                    txtCmnts = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Coments") as TextBox;

                    txthrs.Visible = true;
                    txtout.Visible = true;
                    txtAsbCode.Visible = true;
                    txtActvCode.Visible = true;
                    txtCmnts.Visible = true;
                    if (txthrs.Text == "")
                        txthrs.Text = "0";
                    if (txtout.Text == "")
                        txtout.Text = "0";
                    if (TS_Data.getStatus(TS_Det) == "C" || TS_Data.getStatus(TS_Det) == "c")
                    {
                        txthrs.ReadOnly = true;
                        txtout.ReadOnly = true;
                        txtAsbCode.ReadOnly = true;
                        txtActvCode.ReadOnly = true;
                        txtCmnts.ReadOnly = true;
                    }
                    if (dt_EMPTSDetails.Rows.Count > 0)
                    {
                        if (dt_EMPTSDetails.Rows[i]["TD_Hours"].ToString() != String.Empty)
                            txthrs.Text = dt_EMPTSDetails.Rows[i]["TD_Hours"].ToString();
                        if (dt_EMPTSDetails.Rows[i]["TD_OTUT"].ToString() != String.Empty)
                            txtout.Text = dt_EMPTSDetails.Rows[i]["TD_OTUT"].ToString();
                        if (dt_EMPTSDetails.Rows[i]["TD_AbsentCode"].ToString() != String.Empty)
                            txtAsbCode.Text = TS_Data.GetAbsentName(int.Parse(dt_EMPTSDetails.Rows[i]["TD_AbsentCode"].ToString()));
                        if (dt_EMPTSDetails.Rows[i]["TD_ActivityCode"].ToString() != String.Empty)
                            txtActvCode.Text = TS_Data.GetActivityName(int.Parse(dt_EMPTSDetails.Rows[i]["TD_ActivityCode"].ToString())); ;
                        if (dt_EMPTSDetails.Rows[i]["TD_Comment"].ToString() != String.Empty)
                            txtCmnts.Text = dt_EMPTSDetails.Rows[i]["TD_Comment"].ToString();
                    }

                    if (lblDatesetter != null)
                    {
                        lblDatesetter.Text = Cur_Date.AddDays(i).ToShortDateString();
                    }
                    if (lblDaysetter != null)
                    {
                        lblDaysetter.Text = Cur_Date.AddDays(i).DayOfWeek.ToString();
                    }
                }

            }

            if (!TS_Data.IsSubmit(TS_Det))
            {


            }
            else
            {
                txthrs.ReadOnly = true;
                txtActvCode.ReadOnly = true;
                txtAsbCode.ReadOnly = true;
                txtout.ReadOnly = true;
                txtCmnts.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
    public void ClearTextBoxes()
    {
        objNLog.Info("Function Started..");
        try
        {
            TS_Det.User_ID = (string)Session["User"];
            TS_Det.EMP_ID = TS_Data.GetEmpID(TS_Det);

            TS_Det.PayPeriodID = Int32.Parse(Request.QueryString["PPID"].ToString());

            DataTable dt_DateDur = TS_Data.GetDateDuration(TS_Det);

            DateTime dt_StartDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_StartDate"].ToString());
            DateTime dt_EndDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_EndDate"].ToString());

            TS_Det.PayPeriodID = Int32.Parse(dt_DateDur.Rows[0]["PP_ID"].ToString());
            TS_Det.TSStartDate = dt_StartDate;
            TS_Det.TSEndDate = dt_EndDate;
            TS_Det.Status = 'C';

            // sets timesheet data

            if (int.Parse(TS_Data.Get_EmpTimeSheetID(TS_Det).Rows[0]["TS_Id"].ToString()) != 0)
                TS_Det.TimeSheetID = int.Parse(TS_Data.Get_EmpTimeSheetID(TS_Det).Rows[0]["TS_Id"].ToString());// gets the Time sheet id


            string[] date = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] Hrs = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] TSout = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] AsbCode = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] ActvCode = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] Cmnts = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];

            for (int i = 0, j = 1; i < (dt_EndDate.Day - dt_StartDate.Day) + 1; i++, j++)
            {

                TextBox txthrs = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Hours") as TextBox;
                TextBox txtout = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "OTUT") as TextBox;
                TextBox txtAsbCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "AbsentCode") as TextBox;
                TextBox txtActvCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "ActivetyCode") as TextBox;
                TextBox txtCmnts = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Coments") as TextBox;

                txthrs.Text = "0";
                txtout.Text = "0";
                txtAsbCode.Text = "";
                txtActvCode.Text = "";
                txtCmnts.Text = "";
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
    
    public void RenderTextBoxes()
    {
        
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Function Started..");
        try
        {
            TS_Det.User_ID = (string)Session["User"];
            TS_Det.EMP_ID = TS_Data.GetEmpID(TS_Det);

            TS_Det.PayPeriodID = Int32.Parse(Request.QueryString["PPID"].ToString());

            DataTable dt_DateDur = TS_Data.GetDateDuration(TS_Det);

            DateTime dt_StartDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_StartDate"].ToString());
            DateTime dt_EndDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_EndDate"].ToString());

            TS_Det.PayPeriodID = Int32.Parse(dt_DateDur.Rows[0]["PP_ID"].ToString());
            TS_Det.TSStartDate = dt_StartDate;
            TS_Det.TSEndDate = dt_EndDate;
            TS_Det.Status = 'C';

            // sets timesheet data

            if (int.Parse(TS_Data.Get_EmpTimeSheetID(TS_Det).Rows[0]["TS_Id"].ToString()) != 0)
                TS_Det.TimeSheetID = int.Parse(TS_Data.Get_EmpTimeSheetID(TS_Det).Rows[0]["TS_Id"].ToString());// gets the Time sheet id


            string[] date = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] Hrs = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] TSout = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] AsbCode = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] ActvCode = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] Cmnts = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];

            for (int i = 0, j = 1; i < (dt_EndDate.Day - dt_StartDate.Day) + 1; i++, j++)
            {

                TextBox txthrs = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Hours") as TextBox;
                TextBox txtout = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "OTUT") as TextBox;
                TextBox txtAsbCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "AbsentCode") as TextBox;
                TextBox txtActvCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "ActivetyCode") as TextBox;
                TextBox txtCmnts = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Coments") as TextBox;
                Label lblDatesetter = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("lblDate" + j) as Label;

                if (txthrs.Text != String.Empty)
                    Hrs[i] = txthrs.Text;
                else
                    Hrs[i] = "0";

                if (txtout.Text != String.Empty)
                    TSout[i] = txtout.Text;
                else
                    TSout[i] = "0";

                if (txtAsbCode.Text != String.Empty)
                    AsbCode[i] = txtAsbCode.Text;

                if (txtActvCode.Text != String.Empty)
                    ActvCode[i] = txtActvCode.Text;

                if (txtCmnts.Text != String.Empty)
                    Cmnts[i] = txtCmnts.Text;

                date[i] = lblDatesetter.Text;
            }

            if (Request.QueryString["PPID"] != null)// emp is saved his info prev.
            {
                if (TS_Data.getStatus(TS_Det) == String.Empty)
                {
                    string stat = TS_Data.saveTimeSheetDetails(date, Hrs, TSout, AsbCode, ActvCode, Cmnts, TS_Det);

                    lblComments1.Text = stat;
                }
                else if (TS_Data.getStatus(TS_Det) == "S" || TS_Data.getStatus(TS_Det) == "s")
                {
                    // string Status = TS_Data.set_EMPTimeSheets(TS_Det);
                    string stat = TS_Data.updateTimeSheetDetails(date, Hrs, TSout, AsbCode, ActvCode, Cmnts, TS_Det);

                    lblComments1.Text = stat;
                }
                else if (TS_Data.getStatus(TS_Det) == "C" || TS_Data.getStatus(TS_Det) == "c")
                {
                    TS_Det.User_ID = (string)Session["User"];
                    TS_Det.EMP_ID = TS_Data.GetEmpID(TS_Det);
                    TS_Det.PayPeriodID = Int32.Parse(Request.QueryString["PPID"].ToString());
                    TS_Det.PayPeriodID = Int32.Parse(dt_DateDur.Rows[0]["PP_ID"].ToString());
                    TS_Det.TSStartDate = dt_StartDate;
                    TS_Det.TSEndDate = dt_EndDate;
                    TS_Det.Status = 'C';
                    if (int.Parse(TS_Data.Get_EmpTimeSheetID(TS_Det).Rows[0]["TS_Id"].ToString()) != 0)
                        TS_Det.TimeSheetID = int.Parse(TS_Data.Get_EmpTimeSheetID(TS_Det).Rows[0]["TS_Id"].ToString());// gets the Time sheet id
                    for (int i = 0, j = 1; i < (dt_EndDate.Day - dt_StartDate.Day) + 1; i++, j++)
                    {

                        TextBox txthrs = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Hours") as TextBox;
                        TextBox txtout = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "OTUT") as TextBox;
                        TextBox txtAsbCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "AbsentCode") as TextBox;
                        TextBox txtActvCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "ActivetyCode") as TextBox;
                        TextBox txtCmnts = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Coments") as TextBox;

                        txthrs.ReadOnly = true;
                        txtout.ReadOnly = true;
                        txtAsbCode.ReadOnly = true;
                        txtActvCode.ReadOnly = true;
                        txtCmnts.ReadOnly = true;
                    }
                }
            }
            ClearTextBoxes();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        objNLog.Info("Event Started..");
        try
        {
            TS_Det.User_ID = (string)Session["User"];
            TS_Det.EMP_ID = TS_Data.GetEmpID(TS_Det);
            TS_Det.EMP_TSID = TS_Data.GetEmployeeTSID(TS_Det);
            TS_Det.PayPeriodID = Int32.Parse(Request.QueryString["PPID"].ToString());

            DataTable dt_DateDur = TS_Data.GetDateDuration(TS_Det);

            DateTime dt_StartDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_StartDate"].ToString());
            DateTime dt_EndDate = DateTime.Parse(dt_DateDur.Rows[0]["PP_EndDate"].ToString());


            //DateTime dt_StartDate = DateTime.Parse(dt_DateDur.Rows[0][0].ToString());
            //DateTime dt_EndDate = DateTime.Parse(dt_DateDur.Rows[0][1].ToString());

            string[] date = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] Hrs = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] TSout = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] AsbCode = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] ActvCode = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];
            string[] Cmnts = new string[(dt_EndDate.Day - dt_StartDate.Day) + 1];

            for (int i = 0, j = 1; i < (dt_EndDate.Day - dt_StartDate.Day) + 1; i++, j++)
            {

                TextBox txthrs = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Hours") as TextBox;
                TextBox txtout = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "OTUT") as TextBox;
                TextBox txtAsbCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "AbsentCode") as TextBox;
                TextBox txtActvCode = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "ActivetyCode") as TextBox;
                TextBox txtCmnts = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("txtDay" + j + "Coments") as TextBox;
                Label lblDatesetter = (this.Master.FindControl("ContentPlaceHolder1")).FindControl("lblDate" + j) as Label;

                if (txthrs.Text != String.Empty)
                    Hrs[i] = txthrs.Text;
                else
                    Hrs[i] = "0";

                if (txtout.Text != String.Empty)
                    TSout[i] = txtout.Text;
                else
                    TSout[i] = "0";

                if (txtAsbCode.Text != String.Empty)
                    AsbCode[i] = txtAsbCode.Text;

                if (txtActvCode.Text != String.Empty)
                    ActvCode[i] = txtActvCode.Text;

                if (txtCmnts.Text != String.Empty)
                    Cmnts[i] = txtCmnts.Text;

                date[i] = lblDatesetter.Text;

            }
            string Status = TS_Data.SubmitTimeSheetDetails(date, Hrs, TSout, AsbCode, ActvCode, Cmnts, TS_Det);
            lblComments1.Text = Status;
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Event Completed..");
    }


    public bool display_link(object Date)
    {
        
        //check username here and return a bool
        string mode = (string) Request.QueryString["mode"];
        if (mode == "view")
            return false;
        DateTime dt1,dt2;
        dt1 =DateTime.Parse( DateTime.Now.ToString("MM/dd/yyyy"));
        dt2 = (DateTime)Date;
        dt2 = DateTime.Parse(dt2.ToString("MM/dd/yyyy"));
        int i = dt1.Subtract(dt2).Days;
        if (dt1.Subtract(dt2).Days >= 0)
            return true;
        else
            return false;


    }
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        objNLog.Info("Function Started..");
        try
        {
            string date1 = "", date2 = "";
            SqlConnection sqlCon = new SqlConnection(conStr);

            SqlCommand sqlCmd = new SqlCommand("sp_SetTimeSheet", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_status = sqlCmd.Parameters.Add("@Status", SqlDbType.Char, 1);
            par_status.Value = "S";
            SqlParameter par_PPID = sqlCmd.Parameters.Add("@PPID", SqlDbType.Int);
            par_PPID.Value = Request.QueryString["PPID"];
            SqlParameter par_EmpID = sqlCmd.Parameters.Add("@empId", SqlDbType.Int);
            par_EmpID.Value = getEmpID((string)Session["User"]);
            SqlParameter par_SDate = sqlCmd.Parameters.Add("@SubmitDate", SqlDbType.Date);
            par_SDate.Value = DateTime.Now;
            SqlParameter par_uID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar,20);
            par_uID.Value = (string)Session["User"];
            SqlParameter par_TS_ID = sqlCmd.Parameters.Add("@TSID", SqlDbType.Int);
            par_TS_ID.Direction = ParameterDirection.Output;
          
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            
            string tsid = par_TS_ID.Value.ToString();
            SqlParameter par_TSID;

            SqlParameter par_Date;

            SqlParameter par_AbsCode;
            SqlParameter par_ActvCode;
            SqlParameter par_Hrs;
            SqlParameter par_OTUT;
            SqlParameter par_Comment;
            SqlParameter par_TDID;
            SqlParameter par_userID;
            DateTime dt1, dt2;

            foreach (GridViewRow row in gridTimeSheetList.Rows)
            {
                sqlCmd = new SqlCommand("sp_SetTimeSheetDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                par_TSID = sqlCmd.Parameters.Add("@TSID", SqlDbType.Int);
                par_TSID.Value = tsid;
                par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
                if (((Label)row.FindControl("lblTDID")).Text.Trim() != "")
                {
                    par_TDID = sqlCmd.Parameters.Add("@TDID", SqlDbType.Int);
                    par_TDID.Value = ((Label)row.FindControl("lblTDID")).Text;
                }
                par_AbsCode = sqlCmd.Parameters.Add("@AbsCode", SqlDbType.VarChar, 50);
                par_ActvCode = sqlCmd.Parameters.Add("@ActvCode", SqlDbType.VarChar, 50);
                par_Hrs = sqlCmd.Parameters.Add("@Hrs", SqlDbType.Float);
                par_OTUT = sqlCmd.Parameters.Add("@OTUT", SqlDbType.Float);
                par_Comment = sqlCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 255);
                par_userID  = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
                par_userID.Value = (string)Session["User"];
                dt1 = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                dt2 = DateTime.Parse(row.Cells[1].Text);
                dt2 = DateTime.Parse(dt2.ToString("MM/dd/yyyy"));

                if (dt1.Subtract(dt2).Days >= 0)
                {
                    if (date1 == "")
                        date1 = row.Cells[1].Text;
                    if (row.RowIndex == gridTimeSheetList.Rows.Count - 1)
                        date2 = row.Cells[1].Text;
                    par_Date.Value = row.Cells[1].Text;
                    if (((TextBox)row.FindControl("txtABC")).Text.Trim() != "")
                        par_AbsCode.Value = ((TextBox)row.FindControl("txtABC")).Text.Trim();
                    if (((TextBox)row.FindControl("txtAC")).Text.Trim() != "")
                        par_ActvCode.Value = ((TextBox)row.FindControl("txtAC")).Text.Trim();
                    if (((TextBox)row.FindControl("txtHours")).Text.Trim() == "")
                        par_Hrs.Value = 0;
                    else
                        par_Hrs.Value = ((TextBox)row.FindControl("txtHours")).Text;

                    if (((TextBox)row.FindControl("txtOTUT")).Text.Trim() == "")
                        par_OTUT.Value = 0;
                    else
                        par_OTUT.Value = ((TextBox)row.FindControl("txtOTUT")).Text;

                    par_Comment.Value = ((TextBox)row.FindControl("txtComments")).Text;
                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            sendMail(lblempName1.Text, date1, date2, getEmpID((string)Session["User"]));
            Response.Redirect("TimesheetList.aspx");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void sendMail(string UserName,string date1,string date2,string  empID)
    {
        objNLog.Info("Function Started..");
        try
        {
            MailMessage mm = new MailMessage();
            SmtpClient sc = new SmtpClient();

            SqlConnection sqlCon = new SqlConnection(conStr);
            string emailId = "";

            SqlCommand sqlCmd = new SqlCommand("SELECT Emp_Email from Employee where Emp_ID=(select Emp_SupID1 from Employee where Emp_ID='" + empID + "')", sqlCon);

            sqlCon.Open();
            emailId = sqlCmd.ExecuteScalar().ToString();

            sqlCon.Close();
            objNLog.Info("Email.."+emailId);
            if (emailId != "")
            {
                MailAddress ma = new MailAddress(ConfigurationManager.AppSettings["SentMail"].ToString());

                mm.From = ma;
                mm.To.Add(new MailAddress(emailId));
                mm.Subject = "Timesheet Review and Approval";
                mm.Body = "Employee Name :" + UserName + "\nStatus : Timesheet submitted \n Pay Period : " + date1 + " - " + date2;
                sc.Host = ConfigurationManager.AppSettings["smtpMailserver"].ToString();
                sc.Port = int.Parse(ConfigurationManager.AppSettings["smtpport"].ToString());
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential("BhaskerN", "Bn627315","mail.adiopharmacy.com");
                sc.EnableSsl = true;
                objNLog.Info("SMTP.." + sc.Host+";"+sc.Port.ToString()+";");
                sc.Send(mm);
            }
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        objNLog.Info("Function Completed..");
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        objNLog.Info("Event Started..");
        btnSave.Visible = false;
        btnSubmit.Visible = false;
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand("sp_SetTimeSheet", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_status = sqlCmd.Parameters.Add("@Status", SqlDbType.Char, 1);
            par_status.Value = "C";
            SqlParameter par_PPID = sqlCmd.Parameters.Add("@PPID", SqlDbType.Int);
            par_PPID.Value = Request.QueryString["PPID"];
            SqlParameter par_EmpID = sqlCmd.Parameters.Add("@empId", SqlDbType.Int);
            par_EmpID.Value = getEmpID((string)Session["User"]);
            SqlParameter par_UserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.Int);
            par_UserID.Value = getEmpID((string)Session["User"]);
            SqlParameter par_TS_ID = sqlCmd.Parameters.Add("@TSID", SqlDbType.Int);
            par_TS_ID.Direction = ParameterDirection.Output;

                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            string tsid = par_TS_ID.Value.ToString();
            SqlParameter par_TSID;

            SqlParameter par_Date;

            SqlParameter par_AbsCode;
            SqlParameter par_ActvCode;
            SqlParameter par_Hrs;
            SqlParameter par_OTUT;
            SqlParameter par_Comment;
            SqlParameter par_TDID;
            SqlParameter par_userID;
            DateTime dt1, dt2;

            foreach (GridViewRow row in gridTimeSheetList.Rows)
            {
                sqlCmd = new SqlCommand("sp_SetTimeSheetDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                par_TSID = sqlCmd.Parameters.Add("@TSID", SqlDbType.Int);
                par_TSID.Value = tsid;
                par_Date = sqlCmd.Parameters.Add("@Date", SqlDbType.Date);
                if (((Label)row.FindControl("lblTDID")).Text.Trim() != "")
                {

                    par_TDID = sqlCmd.Parameters.Add("@TDID", SqlDbType.Int);
                    par_TDID.Value = ((Label)row.FindControl("lblTDID")).Text;
                }
                par_AbsCode = sqlCmd.Parameters.Add("@AbsCode", SqlDbType.VarChar, 50);
                par_ActvCode = sqlCmd.Parameters.Add("@ActvCode", SqlDbType.VarChar, 50);
                par_userID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
                par_userID.Value = (string)Session["User"];
                par_Hrs = sqlCmd.Parameters.Add("@Hrs", SqlDbType.Float);
                par_OTUT = sqlCmd.Parameters.Add("@OTUT", SqlDbType.Float);
                par_Comment = sqlCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 255);

                dt1 = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                dt2 = DateTime.Parse(row.Cells[1].Text);
                dt2 = DateTime.Parse(dt2.ToString("MM/dd/yyyy"));

                if (dt1.Subtract(dt2).Days >= 0)
                {
                    par_Date.Value = row.Cells[1].Text;
                    if (((TextBox)row.FindControl("txtABC")).Text.Trim() != "")
                        par_AbsCode.Value = ((TextBox)row.FindControl("txtABC")).Text.Trim();
                    if (((TextBox)row.FindControl("txtAC")).Text.Trim() != "")
                        //par_Hrs.Value = null;

                        par_ActvCode.Value = ((TextBox)row.FindControl("txtAC")).Text.Trim();
                    if (((TextBox)row.FindControl("txtHours")).Text.Trim() == "")
                        par_Hrs.Value = 0;
                    else

                        par_Hrs.Value = ((TextBox)row.FindControl("txtHours")).Text;

                    if (((TextBox)row.FindControl("txtOTUT")).Text.Trim() == "")
                        par_OTUT.Value = 0;
                    else
                        par_OTUT.Value = ((TextBox)row.FindControl("txtOTUT")).Text;

                    par_Comment.Value = ((TextBox)row.FindControl("txtComments")).Text;

                    sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
            Response.Redirect("TimesheetList.aspx");
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
 
        objNLog.Info("Event Completed..");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TimesheetList.aspx");
    }

    protected string getEmpID(string UserName)
    {
        objNLog.Info("Function Started..");
        string empID="";

        SqlConnection sqlCon = new SqlConnection(conStr);
        SqlCommand sqlCmd = new SqlCommand("SELECT [Emp_ID] from [Users] where [User_Id]='" + UserName + "'", sqlCon);

        try
        {
            sqlCon.Open();
            empID = sqlCmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
        objNLog.Info("Function Complted..");
        return empID;
    }
}
