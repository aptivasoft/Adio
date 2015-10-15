using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using NLog;
using Adio.UALog;

/// <summary>
/// Summary description for SurveyQuestions
/// </summary>
public class SurveyQuestions
{
    static string conStr = ConfigurationManager.AppSettings["conStr"];
    private NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();
    private UserActivityLog objUALog = new UserActivityLog();
    public SurveyQuestions()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void get_SurveyQuestions()
    {
      //  objNLog.Info("Function Started with prefixText,count as arguments...");
        DataSet dsPatient_Names = new DataSet();
        bool successFlag = false;

        string pat_Fname;
        string pat_Lname;
        string sqlQuery;
        try
        {

            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("select * from Survey_Questions", sqlCon);
            
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsPatient_Names);
            sqlCon.Close();
        }
        catch(Exception e)
        {
        }

    }
}