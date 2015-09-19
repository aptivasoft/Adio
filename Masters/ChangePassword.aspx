<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Masters_ChangePassword" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>eCarex Health Care System</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
    <center>
    <table width="50%" >
    <tr class="medication_info_th1">
       <td colspan="4" align="center" height="20px">CHANGE PASSWORD</td>       
    </tr>
         
        
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblUserID" runat="server" Text="User ID:"></asp:Label>
                        </td>
                        <td align="left">
                                        
                            <asp:TextBox ID="txtUserID" runat="server" TabIndex="6" Width="200px"></asp:TextBox>
                            <%--<span id="spanAvailability"></span>--%>&nbsp;&nbsp;<asp:Label ID = "lblAvailability" runat = "server" Text = "" style="visibility:hidden" ></asp:Label></td>
                            
                        
                        </tr>
        <tr class="medication_info_tr-even">
                        <td align="left">
                            <asp:Label ID="lblOldPwd" runat="server" Text="Old Password: "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOldPwd" runat="server" TabIndex="6" Width="200px" TextMode="Password"></asp:TextBox>
                            <asp:Label ID = "Label2" runat = "server" Text = "" style="visibility:hidden" ></asp:Label></td>
                        </tr>
                         <tr class="medication_info_tr-odd">
                            <td align="left">
                                <asp:Label ID="lblNewPassword" runat="server" Text="New Password: "></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNewPassword" runat="server" TabIndex="7" Width="200px" TextMode="Password" ></asp:TextBox>
                                <asp:Label ID="lblPwdLength" runat="server" Text="(Min. 7 Characters)"></asp:Label>
                            </td>
                         </tr>
                          <tr class="medication_info_tr-even">
                            <td align="left">
                                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: "></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" TabIndex="7" Width="200px" TextMode="Password" ></asp:TextBox>
                            </td>
                         </tr>
        <tr class="medication_info_tr-odd">
            <td colspan="3" align="center">
                <asp:ImageButton ID="btnSaveNewPwd" runat="server" border="0" ImageUrl="~/images/save.gif"  OnClientClick = "return ValidateChangePassword();" TabIndex="9" Style="height: 24px" onclick="btnSaveNewPwd_Click"/>
                 <asp:ImageButton ID="btnChgPwdCancel" runat="server" ImageUrl="../images/cancelPopUp.gif" border="0" TabIndex="11"  Style="height: 24px" onclick="btnChgPwdCancel_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
        <td colspan="2">&nbsp;</td>
        </tr>
    </table>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

