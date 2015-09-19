using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using NLog;

/// <summary>
/// Summary description for RxStatus
/// </summary>
public  class RxStatus
{
    public RxStatus()
    {    }

    private static NLog.Logger objNLog = NLog.LogManager.GetCurrentClassLogger();

    public static DataTable getRxStatus(int prioriryFlag)
    {
        string conStr = ConfigurationManager.AppSettings["conStr"];
        SqlConnection sqlCon = new SqlConnection(conStr);
        DataSet dsRxStatus = new DataSet();
        try
        {
            SqlCommand sqlCmd = new SqlCommand("SELECT [Status_Code],[Status_Desc] FROM [Rx_Status] where [Status_Priority]=" + prioriryFlag.ToString(), sqlCon);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dsRxStatus, "RxStatus");
        }
        catch (Exception ex)
        {
          objNLog.Error("Error : " + ex.Message);
        }
        return dsRxStatus.Tables[0];
    }
}
