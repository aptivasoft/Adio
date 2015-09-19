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
        byte[] docSign = null;
        docSign = (byte[])Session["image"];
        if (docSign != null)
        {
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(docSign);
        }
    }
}
