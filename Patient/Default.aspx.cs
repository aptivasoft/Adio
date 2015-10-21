using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Default : System.Web.UI.Page
{
    protected void GetAllSurvey(string patId)
    {
        try
        {
            Response.Write(SurveyQuestions.get_Survey(patId));
            Response.End();
        }
        catch (Exception ex)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params.Get("GetAllSurvey") != null)
        {
            GetAllSurvey(Request.Params.Get("GetAllSurvey"));
        }

        if (Request.Params.Get("SaveSurvey") != null)
        {
            SavePatientFeedBack(Request.QueryString[0], Request.QueryString[1], Request.QueryString[2]);
        }
    }

    public void SavePatientFeedBack(string surveyQuestionResponse, string patId, string fatId)
    {
        FeedBackSurvey feedback = new FeedBackSurvey();
        feedback.User = (string)Session["User"];
        feedback.Comments = "";
        feedback.FacId = Convert.ToInt32(fatId);
        feedback.patientId = Convert.ToInt32(patId); 
        feedback.SurveyQuestionResponse = surveyQuestionResponse;
        SurveyQuestions.set_SurveyFeedback(feedback);

        Response.End();
    }
}