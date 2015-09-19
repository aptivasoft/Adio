<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="EMailSettings.aspx.cs" Inherits="Masters_EMailSettings" Title="Untitled Page" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updateSmtp" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table align="center" width="95%">
<tr class="medication_info_th1" >
<td align="center" height="20px" colspan="4">
<asp:Label ID="Label1" runat="server" Text="SMTP E-MAIL SETTINGS"  Font-Bold="True" Font-Size="Small"></asp:Label>
</td>
</tr>

<tr  class="medication_info_tr-odd">
<td align="left" > 
    <asp:Label ID="lblSmtpServer" runat="server" Text="SMTP Server"></asp:Label>
</td> 
<td align="left"> 
    <asp:TextBox ID="txtSmtpServer" runat="server" Width="200px"></asp:TextBox>
</td> 
<td align="left" > 
    <asp:Label ID="Label2" runat="server" Text="SMTP Port"></asp:Label>
</td> 
<td align="left"> 
    <asp:TextBox ID="txtSmtpPort" runat="server" Width="75px"></asp:TextBox>
</td>

</tr>

<tr  class="medication_info_tr-even" >
<td align="left" > 
    <asp:Label ID="Label3" runat="server" Text="SMTP User ID"></asp:Label>
</td> 
<td align="left"> 
    <asp:TextBox ID="txtSmtpUserID" runat="server" Width="200px"></asp:TextBox>
</td>
<td align="left" > 
    <asp:Label ID="Label4" runat="server" Text="SMTP Password"></asp:Label>
</td> 
<td align="left"> 
    <asp:TextBox ID="txtSmtpPassword" runat="server" Width="200px"></asp:TextBox>
</td>
</tr>

<tr  class="medication_info_tr-odd">
<td align="left" > 
    <asp:Label ID="Label5" runat="server" Text="Mail From" ></asp:Label>
</td> 
<td align="left"> 
    <asp:TextBox ID="txtMailFrom" runat="server" Width="200px"></asp:TextBox>
</td> 
<td align="left" > 
    <asp:Label ID="Label6" runat="server" Text="Mail To"></asp:Label>
</td> 
<td align="left"> 
    <asp:TextBox ID="txtMailTo" runat="server" Width="200px"></asp:TextBox>
</td>
</tr>

<tr  class="medication_info_tr-even" >
<td align="left"> 
   <asp:Label ID="Label7" runat="server" Text="Mail Priority"></asp:Label>
</td> 
<td align="left"> 
   <asp:DropDownList ID="ddlMailPriority" runat="server">
       <asp:ListItem Text="Normal" Selected="True" Value="N"></asp:ListItem>
       <asp:ListItem Text="Low" Value="L"></asp:ListItem>
       <asp:ListItem Text="High" Value="H"></asp:ListItem>
   </asp:DropDownList>
</td> 
<td align="left"> 
   <asp:CheckBox ID="chkIsSSL" runat="server" Text="Is SSL" />
</td>
<td align="left">
   <asp:CheckBox ID="chkIsHTML" runat="server" Text="Is HTML" />
</td> 

</tr>

<tr  class="medication_info_tr-odd">
<td align="center" colspan="4"> 
   <asp:ImageButton ID="btnSmtpSubmit" runat="server" ImageUrl="../images/save.gif" border="0" onclick="btnSmtpSubmit_Click" Visible="false" OnClientClick ="return ValidateSmtpSettings();"/>
   <asp:ImageButton ID="btnSmtpUpdate" runat="server"  
        ImageUrl="../images/update.png" border="0" Visible="false" Style="height: 24px" 
        onclick="btnSmtpUpdate_Click" OnClientClick ="return ValidateSmtpSettings();"/>
</td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

