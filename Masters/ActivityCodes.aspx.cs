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
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NLog;

public partial class ActivityCodes : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");
        if ((string)Session["Role"] == "C")
        {
            Server.Transfer("../Patient/AccessDenied.aspx");
        }
        if (!Page.IsPostBack)
        {
            //Filldata();
        }
    }

    protected void Filldata()
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            SqlCommand sqlCmd = new SqlCommand("Select Activity_Code,Activity_Name from Activities", sqlCon);

            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            DataSet dsActivityList = new DataSet();

            sqlDa.Fill(dsActivityList);
            gridActivityList.DataSource = dsActivityList.Tables[0];
            gridActivityList.DataBind();

        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
    }
    
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);
        try
        {
            string userID = (string)Session["User"];

            SqlCommand sqlCmd = new SqlCommand("sp_SetActivities", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par_ACode = sqlCmd.Parameters.Add("@ACode", SqlDbType.Int);
            par_ACode.Value = txtActivityCode.Text;

            SqlParameter par_AName = sqlCmd.Parameters.Add("@AName", SqlDbType.VarChar, 30);
            par_AName.Value = txtActivityName.Text;

            SqlParameter par_ADesc = sqlCmd.Parameters.Add("@ADesc", SqlDbType.VarChar, 255);
            par_ADesc.Value = txtActivityDesc.Text;

            SqlParameter pUserID = sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20);
            pUserID.Value = userID;

            SqlParameter par_error = sqlCmd.Parameters.Add("@error", SqlDbType.VarChar, 255);
            par_error.Direction = ParameterDirection.Output;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();

            string str = "alert('" + par_error.Value.ToString() + "');";
            ScriptManager.RegisterStartupScript(btnSave, typeof(Page), "alert", str, true);
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
        finally
        {
            sqlCon.Close();
        }
    }
   
    protected void btnUserCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtActivityCode.Text = "";
            txtActivityDesc.Text = "";
            txtActivityName.Text = "";
        }
        catch (Exception ex)
        {
            objNLog.Error("Error : " + ex.Message);
        }
    }
   
}