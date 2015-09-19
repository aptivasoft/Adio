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
using System.Text;

public partial class Autocomplete_AutoCompleteDoctors : System.Web.UI.Page
{
    private const string jasonPacketFormater = "[{0}]";
    private const string keyValueFormater = "{2}\"{0}\":\"{1}\"{3}";
    private const string keyValueFormaterIntermediatElement = ",{2}\"{0}\":\"{1}\"{3}";

    protected void Page_Load(object sender, EventArgs e)
    {
        string likeElementForSearchingDB = string.Empty;

        if (Request.QueryString.Count == 3)
        {
            likeElementForSearchingDB = Request.QueryString[2];

        }


        PatientInfoDAL objPat_Info = new PatientInfoDAL();
        
        objPat_Info = new PatientInfoDAL();
      
        DataTable Doc_Names = objPat_Info.get_DoctorNames(likeElementForSearchingDB);


        var sb = new StringBuilder();
        for (int i = 0; i < Doc_Names.Rows.Count; i++)
        {
            switch (i)
            {
                case 0:
                    sb.AppendFormat(keyValueFormater, Doc_Names.Rows[i][2], string.Format("{1}, {0} - {2}", Doc_Names.Rows[i][1], Doc_Names.Rows[i][0], Doc_Names.Rows[i][3]), "{", "}");
                    break;
                default:
                    sb.AppendFormat(keyValueFormaterIntermediatElement, Doc_Names.Rows[i][2], string.Format("{1}, {0} - {2}", Doc_Names.Rows[i][1], Doc_Names.Rows[i][0], Doc_Names.Rows[i][3]), "{", "}");
                    break;
            }
        }
        Response.Write(string.Format(jasonPacketFormater, sb.ToString()));




    }
}
