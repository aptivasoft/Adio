<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/eCareXMaster.master" CodeFile="UserActivityLog.aspx.cs" Inherits="Masters_UserActivityLog" Title="User Activity Log" EnableEventValidation="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content1" runat="server">
<table align="center" border="0" width="95%">
<tr class="medication_info_th1" >
<td colspan="4" align="center" height="20px">
<asp:Label ID="Label1" runat="server" Text="USER ACTIVITY LOG"  Font-Bold="True" Font-Size="Small"></asp:Label>
</td>
</tr>
<tr  class="medication_info_tr-odd">
<td align="left" colspan="2"> 
<asp:UpdatePanel ID="upUALogRbtn" runat="server">
<ContentTemplate>
    <asp:RadioButton ID="rbtnUAToday" Text="Today" runat="server" AutoPostBack="true"  Checked="true" GroupName="rbtnUALog" oncheckedchanged="rbtnUAToday_CheckedChanged"  />
    <asp:RadioButton ID="rbtnUADate" Text="Date" runat="server" AutoPostBack="true" GroupName="rbtnUALog" oncheckedchanged="rbtnUADate_CheckedChanged" />
    <asp:RadioButton ID="rbtnUADateRange" Text="Date Range" runat="server" AutoPostBack="true" GroupName="rbtnUALog" oncheckedchanged="rbtnUADateRange_CheckedChanged" />
    <asp:RadioButton ID="rbtnUAUserID" Text="User ID" runat="server" AutoPostBack="true" GroupName="rbtnUALog" oncheckedchanged="rbtnUAUserID_CheckedChanged" />
</ContentTemplate>
</asp:UpdatePanel>
</td> 
<td   colspan="2">
<asp:UpdatePanel ID="upEmailUserActivity" runat="server">
<ContentTemplate>
<asp:ImageButton ID="imgEmail" ImageUrl="../Images/email.gif"  ToolTip="Report User Activity E-Mail" runat="server" onclick="imgEmail_Click" style="width: 16px" />
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>

<tr  class="medication_info_tr-even" >
<td align="left"   height="30px" valign="middle" colspan="4">
    <asp:UpdatePanel id="up1" runat="server" RenderMode="Inline">
    <ContentTemplate>
    <div ID="pnlUserID" runat="server"  Visible="false" style = "position:absolute">
    <table align="left">
    <tr>
    <td align="left">
        <asp:Label ID="lblUserID" runat="server" Text="Enter User ID: " Font-Bold="true"  ></asp:Label>
    </td>
     
    <td align="left">
        <asp:TextBox ID="txtUserID" runat="server" TabIndex="1" Width="175px"  ></asp:TextBox>
    </td>
    
    <td align="left">
     <asp:UpdatePanel ID="upGetUALog" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnGetUALogUserID" runat="server" Text = "Get Activity" BackColor="OldLace" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" TabIndex="2" onclick="btnGetUALogUserID_Click"  /> 
        </ContentTemplate>
        </asp:UpdatePanel>
    </td>
    </tr>
    </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel id="up2" runat="server" RenderMode="Inline">
    <ContentTemplate>
    <div ID="pnlDate" runat="server" Visible="false"  style="position:absolute">
    <table>
    <tr>
    <td>
    <asp:Label ID="lblDate" runat="server" Text="Enter Date: " Font-Bold="true"  ></asp:Label>
    </td>
    
    <td>
     <asp:TextBox ID="txtDate" runat="server" TabIndex="1" Width="175px"  ></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate" Animated="true" PopupButtonID="txtDate"></cc1:CalendarExtender>
    </td>
     
    <td>
        <asp:UpdatePanel ID="upGetDateUALog" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnGetUALogByDate" runat="server" Text = "Get Activity" 
                BackColor="OldLace" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  
                style="height: 24px" TabIndex="2" onclick="btnGetUALogByDate_Click"  /> 
        </ContentTemplate>
        </asp:UpdatePanel>
    </td>
    </tr>
    </table>
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>    
    
    <asp:UpdatePanel id="up3" runat="server" RenderMode="Inline">
    <ContentTemplate>
    <div ID="pnlDateRange" runat="server" Visible="false"  style="position:absolute">
    <table>
    <tr>
    <td>
    <asp:Label ID="lblFromDate" runat="server" Text="Enter From Date: " Font-Bold="true"  ></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="txtDate1" runat="server" TabIndex="1" Width="100px"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate1" Animated="true" PopupButtonID="txtDate1"></cc1:CalendarExtender>
    </td>
      <td>
    <asp:Label ID="lblToDate" runat="server" Text="Enter To Date: " Font-Bold="true"  ></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="txtDate2" runat="server" TabIndex="1" Width="100px"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate2" Animated="true" PopupButtonID="txtDate2"></cc1:CalendarExtender>
    </td>
    <td>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnGetUALogByDateRange" runat="server" Text = "Get Activity" 
                BackColor="OldLace" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  
                style="height: 24px" TabIndex="2" 
                onclick="btnGetUALogByDateRange_Click" OnClientClick="return compareDates();"  /> 
        </ContentTemplate>
        </asp:UpdatePanel>
    </td>
    </tr>
    </table>
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel id="up4" runat="server" RenderMode="Inline">
    <ContentTemplate>
    <div ID="pnlToday" runat="server" Visible="false"  style="vertical-align:middle;">
    <table>
    <tr>
    <td valign="middle">
        <asp:Label ID="lblUATodayTitle" runat="server" Text="Today's User Activities" Font-Bold="True" Font-Size="12px"></asp:Label>
    </td>
    </tr>
    </table>
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</td>
</tr>
</table>


<table align="center" width="95%">

<tr>
<td>
<asp:UpdatePanel ID="updateUAGrid" runat="server">
<ContentTemplate>
<cc1:ModalPopupExtender ID="mpeEmailActivityLog" 
                        runat="server" 
                        TargetControlID="btnEmailActivityLogAlias" 
                        PopupControlID="pnlEmailActivityLog" 
                        BackgroundCssClass="modalBackground" 
                        CancelControlID="btnCancelEmailActivityLog">
</cc1:ModalPopupExtender>
<cc1:ModalPopupExtender ID="mpeEmailActivityLogStatus" 
                        runat="server" 
                        TargetControlID="btnEmailActivityLogStatusAlias" 
                        PopupControlID="pnlEmailActivityLogStatus" 
                        BackgroundCssClass="modalBackground" 
                        CancelControlID="btnCancelEmailActivityLogStatus">
</cc1:ModalPopupExtender>
    <asp:GridView ID="grdUserActivity" AllowPaging="true" PageSize="15" 
                    runat="server" AutoGenerateColumns="false" 
                    CssClass="medication_info"
                    RowStyle-CssClass="medication_info_tr-odd" 
                    AlternatingRowStyle-CssClass="medication_info_tr-even" 
                    onpageindexchanging="grdUserActivity_PageIndexChanging"
                    PagerSettings-Mode="NextPreviousFirstLast" 
                    PagerSettings-FirstPageText="First | " 
                    PagerSettings-LastPageText="Last" 
                    PagerSettings-NextPageText="Next | " 
                    PagerSettings-PreviousPageText="Previous" 
                    PagerStyle-BackColor="#4f81bc" 
                    PagerStyle-ForeColor="#FFFFFF" 
        onrowdatabound="grdUserActivity_RowDataBound">
        <PagerStyle HorizontalAlign="Right"/>
        <Columns>
            <asp:TemplateField HeaderText="S.No." HeaderStyle-CssClass="medication_info_th1" HeaderStyle-Height="25px"  ItemStyle-HorizontalAlign="Right">
               <ItemTemplate>
               <asp:Label ID="lblSno" runat="server" Text='<%# Bind("SrNo")%>'></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User ID" HeaderStyle-CssClass="medication_info_th1" HeaderStyle-Height="25px"  ItemStyle-HorizontalAlign="Left">
               <ItemTemplate>
               <asp:Label ID="lblUID" runat="server" Text='<%# Bind("UserID")%>'></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText = "User Name" HeaderStyle-CssClass="medication_info_th1"   ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
            <ItemTemplate>
            <asp:Label ID="lblUName" runat = "server" Text='<%# Bind("UserName")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Activity"  HeaderStyle-CssClass="medication_info_th1"  ItemStyle-Width="50%">
               <ItemTemplate>
               <asp:Label ID="lblUActivity" runat="server" Text='<%# Bind("UserActivity")%>' Width="100%"></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DB Table Name" HeaderStyle-CssClass="medication_info_th1"  ItemStyle-Width="15%">
               <ItemTemplate>
               <asp:Label ID="lblDBTableName" runat="server" Text='<%# Bind("DBTableName")%>'></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time" HeaderStyle-CssClass="medication_info_th1"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
               <ItemTemplate>
               <asp:Label ID="lblActivityTime" runat="server" Text='<%# Bind("Time")%>'></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="TempID" HeaderStyle-CssClass="medication_info_th1"  ItemStyle-HorizontalAlign="Center"  Visible="false">
               <ItemTemplate>
               <asp:Label ID="lblTempID" runat="server" Text='<%# Bind("TempID")%>'></asp:Label>
               </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Panel ID="pnlEmailActivityLog" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
    <table width="325px"   border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td align="left"  height="25px"  class="medication_info_th1" width="95%">
        <asp:Label ID="lblEmailActivityLog" runat="server" Text="E-Mail User Activity Log"></asp:Label>
    </td>
    <td  height="25px" align="right" class="medication_info_th1" width="5%">
       <asp:ImageButton ID="btnCancelEmailActivityLog" runat="server"  ImageUrl= "../images/close_popup.png" Height="16px" Width="16px" border="0" ToolTip="Close"></asp:ImageButton>
    </td>
    </tr>
    <tr>
    <td>
    <table>
     <tr>
    <td>
        <asp:Label ID="lblEmailID" runat="server" Text="To" Font-Bold="true" Font-Size="12px"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtEmailID" runat="server" Width="250px"></asp:TextBox>
    </td>
    </tr>
   
     <tr>
      <td align="left">
       <asp:Label ID = "Label7" Text = "Message:" runat = "server" Font-Bold="true" Font-Size="12px"></asp:Label>
       </td>
        <td> 
        <asp:TextBox ID="txtEmailMessage" runat="server" Width="250" TextMode="MultiLine"  Height="50px"></asp:TextBox>
          </td>
       </tr>
    <tr>
    <td colspan="2" align="center">
    <asp:ImageButton ID="btnEmailActivityLog" runat="server" 
            ImageUrl="../Images/send.png" onclick="btnEmailActivityLog_Click" />
    </td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlEmailActivityLogStatus" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
    <table width="300px"   border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td align="left"  height="25px"  class="medication_info_th1" width="95%">
        <asp:Label ID="Label2" runat="server" Text="E-Mail User Activity Log"></asp:Label>
    </td>
    <td  height="25px" align="right" class="medication_info_th1" width="5%">
       <asp:ImageButton ID="btnCancelEmailActivityLogStatus" runat="server"  ImageUrl= "../images/close_popup.png" Height="16px" Width="16px" border="0" ToolTip="Close"></asp:ImageButton>
    </td>
    </tr>
    <tr>
    <td align="center" height="50px">
    <asp:Image ID="imgEmailActivityLogStatus" runat="server" ImageUrl="../images/check.gif"/>&nbsp;&nbsp;
    <asp:Label ID="lblEmailActivityLogStatus" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
    </td>
    </tr>
  
    </table>
    </asp:Panel>
    <asp:Button ID="btnEmailActivityLogAlias" runat="server" style="visibility:hidden" Text="Email Activity Log" />
    <asp:Button ID="btnEmailActivityLogStatusAlias" runat="server" style="visibility:hidden" Text="Email Activity Log Status" />
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
</table>
</asp:Content>