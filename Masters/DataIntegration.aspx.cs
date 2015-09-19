using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Dts.Runtime;

public partial class Patient_dataIntegration : System.Web.UI.Page
{
    string conStr = ConfigurationManager.AppSettings["conStr"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        if(!Page.IsPostBack)
        {
            string role = (string)Session["Role"];
        if ( role== "A" || role== "M" )
        {
            
        }
        else
        {
            btnBulkCopy.Visible = false;
            btnImportData.Visible = false;
            lblResult.Visible = true;
            lblResult.Text = "Acess Denied";

            Server.Transfer("../Patient/AccessDenied.aspx");

            
        }
        }
    }

    private void OnRowsCopied(object sender, SqlRowsCopiedEventArgs args)
    {
        //lblCounter.Text += args.RowsCopied.ToString() + " rows are copied<Br>";
    }
    protected void btnBulkCopy_Click(object sender, EventArgs e)
    {
        try
        {
            string pkgLocation;
            Package pkg;
            Application app;
            DTSExecResult pkgResults;

            MyEventListener eventListener = new MyEventListener();


            //pkgLocation = "D:\\dts\\TestDataPKG.dtsx";
            app = new Application();

            //Load Package from File system.
            //pkg = app.LoadPackage(pkgLocation, eventListener);
            //app.SaveToXml("D:\\test.xml", pkg, eventListener);

            //pkgResults = pkg.Validate(null, null, null, null);
            //pkgResults = pkg.Execute(null, null, eventListener, null, null);


            string pkgName = String.Empty;
            string sqlServer = String.Empty;
            string userID = String.Empty;
            string pwd = String.Empty;

            pkgName = ConfigurationManager.AppSettings["PackageName"];             // Give the Name of the DTS Package in SQL Server.
            sqlServer = ConfigurationManager.AppSettings["ServerName"];  // Name of the SQL Server.
            userID = ConfigurationManager.AppSettings["UserName"];            // Sql Server User ID.
            pwd = ConfigurationManager.AppSettings["Password"];       // Sql Server Password.



            AdioDTS.AdioDTS adiodts = new AdioDTS.AdioDTS();
            string retval = adiodts.RunDTS(sqlServer, userID, pwd, pkgName);
            //Load Package from sql server.
            //pkg = app.LoadFromDtsServer(pkgName, sqlServer, null);
            //pkg = app.LoadFromSqlServer(pkgName, sqlServer, userID, pwd, null);
            //pkgResults = pkg.Execute();//(null, null, eventListener, null, null);

            lblResult.Text = "DTS Package Executed Successfully...";
            btnBulkCopy.Enabled  = false;
            btnImportData.Enabled = true;
        }
        catch (Exception ex)
        {
            lblResult.Text = "Exception: " + ex.Message;
        }

    }

    protected void btnImportData_Click(object sender, EventArgs e)
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        string sqlQuery = "";// "select p.pat_FName, p.pat_LName, p.pat_DOB, p.pat_Gender, p.LastModified,pin.PI_PolicyID,pin.PI_GroupNo,pin.PI_BINNo,PI_InsdName,PI_InsdRel,ins.Ins_Name,p.Doc_ID,ph.Phrm_ID,pa.PA_Desc,ph.Phrm_Name,ph.Phrm_Address1,ph.Phrm_Address2,ph.Phrm_City,ph.Phrm_State,ph.Phrm_Zip,ph.Phrm_Phone,ph.Phrm_Fax from Patient_Info p, Patient_Ins pin, Patient_Allergies pa,Pharmacy_Info ph,Insurance_Info ins where p.pat_ID =" + Int32.Parse(patID) + " and pin.pat_ID=" + Int32.Parse(patID) + " and pa.pat_ID=" + Int32.Parse(patID) + "and p.Phrm_ID=ph.Phrm_ID and pin.Ins_ID=ins.Ins_ID";

        sqlQuery = "importFacility";

        SqlCommand sqlCmd = new SqlCommand();
        try
        {
            sqlCon.Open();
            sqlCmd.CommandText = "importFacility";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.ExecuteNonQuery();
            sqlCmd.CommandText = "importDoctor";
            sqlCmd.ExecuteNonQuery();
            sqlCmd.CommandText = "importPatient";
            sqlCmd.ExecuteNonQuery();
            sqlCmd.CommandText = "importRx";
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            lblResult1.Text = "Imported Successfully...";

        }
        catch (Exception ex)
        {
            lblResult1.Text = "Importing Failed...";
        }

    }
}

class MyEventListener : DefaultEvents
{
    public override bool OnError(DtsObject source, int errorCode, string subComponent,
      string description, string helpFile, int helpContext, string idofInterfaceWithError)
    {
        // Add application-specific diagnostics here.
        string str = source + "<br>" + subComponent + "<br>" + description;
        return false;
    }
}
