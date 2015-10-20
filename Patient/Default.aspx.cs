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
    }
}