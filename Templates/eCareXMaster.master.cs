using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Adio.UALog;
public partial class eCareXMaster : System.Web.UI.MasterPage
{
    private static UserActivityLog objUALog = new UserActivityLog();
    
    string conStr = ConfigurationManager.AppSettings["conStr"];

    protected void Page_Load(object sender, EventArgs e)
    {
        //lblUsername.Attributes.Add("onmouseover", "highlightUserON();");
        //lblUsername.Attributes.Add("onmouseout", "highlightUserOFF();");

        if (Session["User"] == null || Session["Role"] == null)
            Response.Redirect("../Login.aspx");

        //if (Session["User"] != null)
        //    lblUsername.Text = DefaultValues.GetUserDisplayName((string)Session["User"]);
        //else
        //    Response.Redirect("../Login.aspx");

        //if(Session["RoleName"]!=null)
        //    lblUsername.ToolTip = "Logged in as " + (string)Session["RoleName"];

        if ((string)Session["Role"] == "A")
        {
            hrefUALog.Visible = true;
            hrefSmtp.Visible = true;
            hrefEmpUserAccounts.Visible = true;
            hrefAS.Visible = true;
            hrefAppointments.Visible = true;
            hrefBill.Visible = true;
            hrefDocUserAccounts.Visible = true;
            hrefEmpUserAccounts.Visible = true;
            hrefNewPat.Visible = true;
            hrefPay.Visible = true;
            hrefPresHistory.Visible = true;
            hrefSamples.Visible = true;
            hrefTS.Visible = true;
            hrefAssignPhrm.Visible = true;
            hrefAssignUserReports.Visible = true;
            hrefRx30RxLog.Visible = true;
            //hrefDelPatient.Visible = true;
        }
        else
        {
            hrefUALog.Visible = false;
            hrefSmtp.Visible = false;          
            hrefAssignUserReports.Visible = false;

            UserReportsDAL objUserRpts = new UserReportsDAL();

            DataTable dtUserReports = objUserRpts.GetUserReports((string)Session["User"]);

            hrefUALog.Visible = false;
            hrefSmtp.Visible = false;
            hrefEmpUserAccounts.Visible = false;
            hrefAS.Visible = false;
            hrefAppointments.Visible = false;
            hrefBill.Visible = false;
            hrefDocUserAccounts.Visible = false;
            hrefEmpUserAccounts.Visible = false;
            hrefNewPat.Visible = false;
            hrefPay.Visible = false;
            hrefPresHistory.Visible = false;
            hrefRx30RxLog.Visible = false;
            hrefSamples.Visible = false;
            hrefTS.Visible = false;
            //hrefDelPatient.Visible = false;

            if (dtUserReports.Rows.Count > 0)
            {
                for (int i = 0; i < dtUserReports.Rows.Count; i++)
                {
                    switch (dtUserReports.Rows[i][0].ToString())
                    {
                        case "1":
                            {
                                hrefRpt.Visible = true;
                                hrefAS.Visible = true;
                                break;
                            }
                        case "2":
                            {
                                hrefRpt.Visible = true; 
                                hrefAppointments.Visible = true;
                                break;
                            }
                        case "3":
                            {
                                hrefRpt.Visible = true;
                                hrefBill.Visible = true;
                                break;
                            }
                        case "4":
                            {
                                hrefRpt.Visible = true;
                                hrefDocUserAccounts.Visible = true;
                                break;
                            }
                        case "5":
                            {
                                hrefRpt.Visible = true;
                                hrefEmpUserAccounts.Visible = true;
                                break;
                            }
                        case "6":
                            {
                                hrefRpt.Visible = true;
                                hrefNewPat.Visible = true;
                                break;
                            }
                        case "7":
                            {
                                hrefRpt.Visible = true;
                                hrefPay.Visible = true;
                                break;
                            }
                        case "8":
                            {
                                hrefRpt.Visible = true;
                                hrefPresHistory.Visible = true;
                                break;
                            }
                        case "9":
                            {
                                hrefRpt.Visible = true;
                                hrefSamples.Visible = true;
                                break;
                            }
                        case "10":
                            {
                                hrefRpt.Visible = true;
                                hrefTS.Visible = true;
                                break;
                            }
                        case "11":
                            {
                                hrefRpt.Visible = true;
                                hrefRx30RxLog.Visible = true;
                                break;
                            }
                       
                    }
                }
            }
        }


        

        if ((string)Session["Role"] == "D" || (string)Session["Role"] == "P" || (string)Session["Role"] == "T" || (string)Session["Role"] == "C" ||  (string)Session["Role"] == "N")
        {
            if ((string)Session["Role"] == "D")
            {
                //hlHome.NavigateUrl = "../Home/DoctorHome.aspx";

                hrefMaster.Visible = true;
                hrefChgPwd.Visible = true;
                hrefActivity.Visible = false;
                hrefAnnouncments.Visible = false;
                hrefClinic.Visible = false;
                hrefDataInt.Visible = false;
                hrefDoctor.Visible = false;
                hrefDrug.Visible = false;
                hrefEmp.Visible = false;
                hrefEvent.Visible = false;
                hrefIns.Visible = false;
                hrefLoc.Visible = false;
                hrefSIG.Visible = false;
                hrefuser.Visible = false;
                hrefTimesheet.Visible = false;
                hrefUALog.Visible = false;
                hrefRxQueue.Visible = false;
                hrefGenStamp.Visible = false;
                hrefTrackStamp.Visible = false;
                hrefAssignPhrm.Visible = false;
                //hrefPay.Visible = false;
                //hrefBill.Visible = false;
                //hrefTS.Visible = false;
                //hrefAS.Visible = false;
               
                
            }
            else if ((string)Session["Role"] == "N")
            {
                //hlHome.NavigateUrl = "../Home/NurseHome.aspx";

                hrefMaster.Visible = true;
                hrefChgPwd.Visible = true;
                hrefActivity.Visible = false;
                hrefAnnouncments.Visible = false;
                hrefClinic.Visible = false;
                hrefDataInt.Visible = false;
                hrefDoctor.Visible = false;
                hrefDrug.Visible = false;
                hrefEmp.Visible = false;
                hrefEvent.Visible = false;
                hrefIns.Visible = false;
                hrefLoc.Visible = false;
                hrefSIG.Visible = false;
                hrefuser.Visible = false;
                hrefTimesheet.Visible = false;
                hrefUALog.Visible = false;
                hrefRxQueue.Visible = false;
                hrefGenStamp.Visible=false;
                hrefTrackStamp.Visible = false;
                hrefAssignPhrm.Visible = false;
                //hrefPay.Visible = false;
                //hrefBill.Visible = false;
                //hrefTS.Visible = false;
                //hrefAS.Visible = false;
            }
            else if ((string)Session["Role"] == "C")
            {
                //hlHome.NavigateUrl = "../Home/CSRHome.aspx";

                hrefMaster.Visible = true ;
                hrefActivity.Visible = false;
                hrefAnnouncments.Visible = true;
                hrefClinic.Visible = false;
                hrefDataInt.Visible = false;
                hrefDoctor.Visible = true;
                hrefDrug.Visible = true;
                hrefEmp.Visible = false;
                hrefEvent.Visible = false ;
                hrefIns.Visible = true;
                hrefLoc.Visible = false;
                hrefSIG.Visible = true;
                hrefuser.Visible = false;
                hrefTimesheet.Visible = true ;
                hrefUALog.Visible = false;
                hrefAssignPhrm.Visible = false;
                if (Session["LocID"] != null)
                {
                    hrefGenStamp.Visible = true;
                    hrefTrackStamp.Visible = true;
                }
                else
                {
                    hrefGenStamp.Visible = false;
                    hrefTrackStamp.Visible = false;
                }
            }
            else
            {
                //hlHome.NavigateUrl = "../Home/PharmacistHome.aspx";
                
                hrefMaster.Visible = true;
                hrefActivity.Visible = true;
                hrefAnnouncments.Visible = true;
                hrefClinic.Visible = true;
                hrefDataInt.Visible = true;
                hrefDoctor.Visible = true;
                hrefDrug.Visible = true;
                hrefEmp.Visible = false ;
                hrefEvent.Visible = true;
                hrefIns.Visible = true;
                hrefLoc.Visible = true;
                hrefSIG.Visible = true;
                hrefuser.Visible = false ;
                hrefTimesheet.Visible = true;
                hrefUALog.Visible = false;
                hrefAssignPhrm.Visible = false;
                if (Session["LocID"] != null)
                {
                    hrefGenStamp.Visible = true;
                    hrefTrackStamp.Visible = true;
                }
                else
                {
                    hrefGenStamp.Visible = false;
                    hrefTrackStamp.Visible = false;
                }
            }
            //hlHome.Visible = true;
            //lblSeprator.Visible = true;
        }
        else
        {
            hrefMaster.Visible = true ;
            hrefActivity.Visible = true;
            hrefAnnouncments.Visible = true;
            hrefClinic.Visible = true;
            hrefDataInt.Visible = true;
            hrefDoctor.Visible = true;
            hrefDrug.Visible = true;
            hrefEmp.Visible = true;
            hrefEvent.Visible = true;
            hrefIns.Visible = true;
            hrefLoc.Visible = true;
            hrefSIG.Visible = true;
            hrefuser.Visible = true;
            hrefTimesheet.Visible = true;
            hrefAssignPhrm.Visible = true;
            //hlHome.Visible = false;
            //lblSeprator.Visible = false;
            if (Session["LocID"] != null)
            {
                hrefGenStamp.Visible = true;
                hrefTrackStamp.Visible = true;
            }
            else
            {
                hrefGenStamp.Visible = false;
                hrefTrackStamp.Visible = false;
            }
        }
    }

    //protected void lnkLogout_Click(object sender, EventArgs e)
    //{
    //    Session.Abandon();
    //    objUALog.LogUserActivity(conStr,(string)Session["User"],"Logged Out.","",0);
    //    Response.Redirect("../Login.aspx");
    //}
}
