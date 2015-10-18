﻿using System;
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


using System.Web.Script.Serialization;

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
    public static string surveryQuestion()
    {
        return DataTableToJsonWithJavaScriptSerializer(get_SurveyQuestionsDataTable());
    }

    public static DataTable get_SurveyQuestionsDataTable()
    {
        DataTable dtQuestionSurvey = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("select * from Survey_Questions", sqlCon);
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtQuestionSurvey);
            sqlCon.Close();
        }
        catch (Exception e)
        {
        }
        return dtQuestionSurvey;
    }


    public static void set_SurveyFeedback(FeedBackSurvey feedback)
    {
        using (SqlConnection connection = new SqlConnection(conStr))
        {

            SqlCommand cmd = new SqlCommand("DECLARE @return_value int, " +
                                          "@SurveyID int, " +
                                          "@PatSurNo smallint, "+
                                          "@PRtable AS Pst_Survey_Responses "+
  
                                          " INSERT INTO @PRtable (QID, choice1_selected, choice2_selected, " +
                                          "choice3_selected, choice4_selected,Ques_Comments) "+
                                          "VALUES "+ feedback.SurveyQuestionResponse +

                                          " EXEC @return_value = [dbo].[sp_set_PatientSurveys] "+
                                          "@Fac_ID = " + feedback.FacId +", "+
                                          "@Pat_ID = "+ feedback.patientId +", "+
                                          "@Comments = N'" + feedback.Comments +"', "+
                                          "@User = N'" + feedback.User +"', "+
                                          "@PatResponses = @PRtable, "+
                                          "@SurveyID = @SurveyID OUTPUT "+

                                        "SELECT @SurveyID as N'@SurveyID' "+

                                        "SELECT 'Return Value' = @return_value");
            cmd.CommandType = CommandType.Text;

            //SqlCommand com = new SqlCommand( "sp_set_PatientSurveys", connection);  //creating  SqlCommand  object
            //com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.AddWithValue("@Pat_ID", feedback.patientId);
            //com.Parameters.AddWithValue("@QID ", feedback.QID);
            //com.Parameters.AddWithValue("@choice1_selected ", feedback.choice1_selected);
            //com.Parameters.AddWithValue("@choice2_selected ", feedback.choice2_selected);
            //com.Parameters.AddWithValue("@choice3_selected ", feedback.choice3_selected);
            //com.Parameters.AddWithValue("@choice4_selected ", feedback.choice4_selected);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public static string DataTableToJsonWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

}