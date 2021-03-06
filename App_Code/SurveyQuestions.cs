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

    public static void saveReportComments(int SurveyID, char FollowupStatus, string SurveyComments) 
    { 
        //sp_Update_SurveyFlag

        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_Update_SurveyFlag", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter Report_Id = sqlCmd.Parameters.Add("@SurveyID", SqlDbType.Int);
        Report_Id.Value = SurveyID;

        SqlParameter Report_FollowupStatus = sqlCmd.Parameters.Add("@FollowupStatus", SqlDbType.Char);
        Report_FollowupStatus.Value = '9';

        SqlParameter Report_SurveyComments = sqlCmd.Parameters.Add("@SurveyComments", SqlDbType.Text);
        Report_SurveyComments.Value = SurveyComments;

        sqlCon.Open();
        sqlCmd.ExecuteNonQuery();
        sqlCon.Close();



    }

    public static void deleteSurvey(int SurveyId) 
    {
        SqlConnection sqlCon = new SqlConnection(conStr);

        SqlCommand sqlCmd = new SqlCommand("sp_Delete_Pat_Survey", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter survey_Id = sqlCmd.Parameters.Add("@SurveyID", SqlDbType.Int);
        survey_Id.Value = SurveyId;
        sqlCon.Open();
        sqlCmd.ExecuteNonQuery();
        sqlCon.Close();
    }


    public static string get_Survey(string patId)
    {
        DataTable dtQuestionSurvey = new DataTable();
        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("sp_getPatSurveys", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Pat_ID", Convert.ToInt32(patId));
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtQuestionSurvey);
            sqlCon.Close();
        }
        catch (Exception e)
        {
        }
        return DataTableToJsonWithJavaScriptSerializer(dtQuestionSurvey);
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

    public static DataTable get_ReportDataTable(int ClinicID, int FacilityID)
    {
        DataTable dtReport = new DataTable();
        string insertCommand = "USE [eCareXDB]" +
                                " DECLARE @return_value int" +
                                " EXEC @return_value = [dbo].[sp_getPatSurveyList]" +
                                  " @Clinic_ID = " + ClinicID + "," +
                                  " @facility_ID = " + FacilityID + "," +
                                  " @FollowupStatus = N'1' " +
                                  " SELECT 'Return Value' = @return_value";


        try
        {
            SqlConnection sqlCon = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(insertCommand, sqlCon);
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtReport);
            sqlCon.Close();
        }
        catch (Exception e)
        {
        }
        return dtReport;
    }




    public static void set_SurveyFeedback(FeedBackSurvey feedback)
    {
        using (SqlConnection openCon = new SqlConnection(conStr))
        {
            string insertCommand = "USE [eCareXDB] DECLARE @return_value int, " +
                                          "@SurveyID int, " +
                                          "@PatSurNo smallint, " +
                                          "@PRtable AS Pst_Survey_Responses " +

                                          " INSERT INTO @PRtable (QID, choice1_selected, choice2_selected, " +
                                          "choice3_selected, choice4_selected,Ques_Comments) " +
                                          "VALUES " + feedback.SurveyQuestionResponse +

                                          " EXEC @return_value = [dbo].[sp_set_PatientSurveys] " +
                                          "@Fac_ID = " + feedback.FacId + ", " +
                                          "@Pat_ID = " + feedback.patientId + ", " +
                                          "@Comments = N'" + feedback.Comments + "', " +
                                          "@User = N'" + feedback.User + "', " +
                                          "@PatResponses = @PRtable, " +
                                          "@SurveyID = @SurveyID OUTPUT " +

                                        "SELECT @SurveyID as N'@SurveyID' " +
                                        "SELECT 'Return Value' = @return_value";

            using (SqlCommand cmd = new SqlCommand(insertCommand))
            {
                cmd.Connection = openCon;
                cmd.Parameters.AddWithValue("@Fac_ID", feedback.FacId);
                cmd.Parameters.AddWithValue("@Pat_ID", feedback.patientId);
                cmd.Parameters.AddWithValue("@Comments", feedback.Comments);
                cmd.Parameters.AddWithValue("@User", feedback.User);
                cmd.Parameters.AddWithValue("@PatResponses", feedback.SurveyQuestionResponse);
                openCon.Open();
                cmd.ExecuteNonQuery();
                openCon.Close();
            }
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