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

public partial class Patient_image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //byte[] docSign = null;
        //docSign = (byte[])Session["image"];
        //if (docSign != null)
        //{
        //    Response.Clear();
        //    Response.ContentType = "image/jpeg";
        //    Response.BinaryWrite(docSign);
        //}

        if (Request.QueryString["RxItemID"] != null)
        {
            PatientInfoDAL objPat_Info = new PatientInfoDAL();

            DataTable dtRxDoc = objPat_Info.GetPatRxDocument(int.Parse(Request.QueryString["RxItemID"]));

            if (dtRxDoc.Rows.Count > 0)
            {
                if (dtRxDoc.Rows[0][0] != DBNull.Value)
                {
                    byte[] rxDoc = (byte[])dtRxDoc.Rows[0][0];
                    if (rxDoc != null)
                    {
                        Response.Clear();
                        Response.ContentType = "image/jpeg";
                        Response.BinaryWrite(rxDoc);
                    }
                }
            }

        }
    }
}
