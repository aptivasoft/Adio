<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:asp="remove">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="/">

    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="90" align="center">
      <tr>
        <td width="100" height="90">
          <img src="images/adio_login_logo.jpg" width="172" height="90" border="0"/>
        </td>
      </tr>
    </table>
    <p align="center">&#160;</p>
    <p align="center">&#160;</p>
    <p align="center">&#160;</p>
    <table border="0" cellpadding="0" cellspacing="0" width="350" height="218" align="center">
      <tr>
        <td width="14">
          <img src="images/adio_login_left.jpg" width="19" height="218" border="0"/>
        </td>
        <td width="92%" background="images/adio_login_bgpanel.jpg" align="center" valign="middle">
          <table class="mainText" border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
            <tr>
              <td width="111">&#160;</td>
              <td width="20">&#160;</td>
              <td width="187">&#160;</td>
            </tr>
            <xsl:for-each select="Pages/Page/Control">
              <tr>
                <xsl:if test="@type='text'">
                  <td width="111">
                    <p align="right">
                      <asp:Label id="{concat('lbl',@id)}" Text="{@caption}" runat="server"></asp:Label>
                    </p>
                  </td>
                  <td width="20" align="center" valign="middle">:</td>
                  <td width="187">
                    <xsl:choose>
                      <xsl:when test="@mode='Password'">
                        <asp:TextBox id="{concat('txt',@id)}" runat="server" TextMode="{@mode}" width="150px"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <asp:TextBox id="{concat('txt',@id)}" runat="server"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </td>
                </xsl:if>
              </tr>
            </xsl:for-each>
            <tr>
              <td width="315" colspan="3" height="32">
                <p align="center">
                  <xsl:for-each select="Pages/Page/LinkControl">
                    <xsl:if test ="@type='hyperlink'">
                      <asp:LinkButton id="{@id}" Text="{@caption}" runat="server"></asp:LinkButton>&#160;&#160;&#160;
                     </xsl:if>
                  </xsl:for-each>
                </p>
              </td>
            </tr>
            <tr>
              <td width="314" height="35" colspan="3" valign="top" align="center">
                <xsl:for-each select="Pages/Page/ButtonControl">
                  <xsl:if test="@type='button'">
                    <p align="center">
                      <asp:UpdatePanel ID="updateLogin" runat="server">
                        <ContentTemplate>
                           <asp:ImageButton ID="{@id}" runat="server"  ImageUrl="images/adio_login_but.jpg" width="103" height="43" onClientClick="return validateUser()"  border="0" ></asp:ImageButton>
                        </ContentTemplate>
                      </asp:UpdatePanel>
                     </p>
                  </xsl:if>
                </xsl:for-each>
              </td>
            </tr>

            <tr>
              <td valign="top" colspan="3" align="center">
                    <p align="center">
                      <asp:UpdatePanel ID="updateStatus" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                      </asp:UpdatePanel>
                     </p>
                </td>
            </tr>
          </table>
        </td>
        <td width="17">
          <img src="images/adio_login_right.jpg" width="16" height="218" border="0"/>
        </td>
      </tr>
    </table>
    <!--<table>
         <tr>
           <td>
             --><!--<asp:Panel ID="pnlForgotPwdPh" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB">
               <table width="300" height="50" border="0" cellpadding="5" cellspacing="5" class="mainText">
                 <tr>
                   <td>
                     <asp:Label id="lblEmailID" Text="Email" runat="server"></asp:Label>
                   </td>
                   <td>
                     <asp:TextBox id="txtEmailID" runat="server"/>
                   </td>
                 </tr>
                 <tr>
                   <td>
                     <asp:Label id="lblPwdPhrase" Text="Password Phrase" runat="server"></asp:Label>
                   </td>
                   <td>
                     <asp:TextBox id="txtPwdPhrase" runat="server"/>
                   </td>
                 </tr>
                 <tr>
                   <td>
                     <asp:UpdatePanel ID="upsendEmail" runat="server" UpdateMode="Conditional">
                       <ContentTemplate>
                         <asp:ImageButton ID="btnSendEmail" runat="server" ImageUrl="images/submit.jpg" border="0"></asp:ImageButton>
                       </ContentTemplate>
                     </asp:UpdatePanel>
                 </td>
                     <td>
                     <asp:ImageButton ID="btnCancel" runat="server"  ImageUrl="images/cancle.JPG" border="0"></asp:ImageButton>
                   </td>
                 </tr>
                 <tr>
                   <td colspan="2" align="center">
                     <asp:Button ID="btnSend" runat="server" style="visibility:hidden;" />
                   </td>
                 </tr>
               </table>
             </asp:Panel>--><!--
           </td>
         </tr>
      <tr>
       <td>
      
       </td>
     </tr>
    </table>-->
  </xsl:template>
</xsl:stylesheet>
