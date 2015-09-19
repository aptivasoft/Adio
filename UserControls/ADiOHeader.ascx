<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ADiOHeader.ascx.cs" Inherits="UserControls_ADiOHeader" %>
<link rel="stylesheet" type="text/css" href="../css/style.css" />  
<script language="javascript" type="text/javascript">
    function updateClock() {
        var currentTime = new Date();
        var todayDate = currentTime.format("MM/dd/yyyy");
        var currentHours = currentTime.getHours();
        var currentMinutes = currentTime.getMinutes();
        var currentSeconds = currentTime.getSeconds();
        currentMinutes = (currentMinutes < 10 ? "0" : "") + currentMinutes;
        currentSeconds = (currentSeconds < 10 ? "0" : "") + currentSeconds;
        var timeOfDay = (currentHours < 12) ? "AM" : "PM";
        currentHours = (currentHours > 12) ? currentHours - 12 : currentHours;
        currentHours = (currentHours == 0) ? 12 : currentHours;
        var currentDateString = todayDate;
        var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + " " + timeOfDay;
        document.getElementById("spnDate").innerHTML = currentDateString;
        document.getElementById("spnTime").innerHTML = currentTimeString;
    }
</script>
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" height="63">
            <tr>
              <td width="5">
                <img src="../images/adio_0101.jpg" border="0" alt=""/>
              </td>
              <td width="160" background="../Images/adio_0202.jpg"  align="right">
                <img src="../Images/logo_audio_new.png" width="123" height="55" alt="" class="mouse" onclick="GotoPage();"/>
              </td>
              <td width="74">
                <img src="../Images/adio_0303.jpg" width="98" height="63" alt=""/>
              </td>
              <td width="90">
                <img src="../Images/adio_0404.jpg" width="90" height="63" alt=""/>
              </td>
              <td width="125">
                <img src="../Images/adio_0505.jpg" width="125" height="63" alt=""/>
              </td>
              <td width="82">
                <img src="../Images/adio_0606.jpg" width="82" height="63" alt=""/>
              </td>
              <td width="19">
                <img src="../Images/adio_0707.jpg" width="19" height="63" alt=""/>
              </td>
              <td width="50%" background="../Images/adio_0808.jpg" align="center" valign="top">
                <table align="center" border="0" cellpadding="0" cellspacing="1" width="100%" height="27">
                  <tr>
                    <td  valign="middle" height="25" class="mainText" align="right">
                     <font Color="#ff3300">Welcome </font>&nbsp;<asp:Label ID="lblUsername" runat="server" ForeColor="#000066" Font-Italic="true"></asp:Label></td>
                     
                    <td width="30" valign="middle" height="25" class="linkTitle" align="center">
                      <asp:HyperLink ID="hlHome"  runat="server" CssClass="linkTitle" Text="Home" NavigateUrl="Default.aspx"></asp:HyperLink>
                    </td>
                     <td width="5" valign="middle" height="10" class="linkTitle" align="center"><asp:Label ID="lblSeprator" runat="server" Text="|&nbsp;"></asp:Label></td>
                    <td width="30" valign="middle" height="25" class="linkTitle" align="center">
                      <asp:LinkButton ID="lnkLogout"  runat="server" Text="Logout" onclick="lnkLogout_Click" OnClientClick ="return Logout();"></asp:LinkButton>
                    </td>
                     <td width="5" valign="middle" height="10" class="linkTitle" align="center">|</td>
                    <td width="140" valign="middle" height="25" class="linkTitle" align="center"> 
                     <span id="spnDate" style="color:#800080"></span>&nbsp;|
                     <span id="spnTime" style="color:#800080"></span>
                     </td> 
                     <td width="30" valign="middle" height="25"><asp:ImageButton ID="Help" ImageUrl="../Images/help.gif" runat="server" ToolTip="Help"  OnClientClick = "OpenHelp(); return false;" /></td> 
                  </tr>
                </table>
              </td>
              <td width="12">
                <img src="../Images/adio_1010.jpg" width="11" height="63" alt=""/>
              </td>
            </tr>
          </table>
          <script>
              updateClock();
              setInterval('updateClock()', 1000);
          </script>