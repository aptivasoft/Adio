<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Stamps.aspx.cs" Inherits="Stamps" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
      <table align="center" width="600px" >
       <tr class="medication_info_th1">
       <td colspan="3" align="center" valign="middle" height="25px" width="100%">Stamps</td>
       </tr>
       
       <tr class="medication_info_tr-odd" >
             
       <td  align="left" width="200px">
           <asp:RadioButton ID="rbtnRegular" runat="server" Text="Regular" GroupName="Rxtype" Checked="true" />&nbsp;
           <asp:RadioButton ID="rbtnSample" runat="server" Text="Sample" GroupName="Rxtype" />&nbsp;
           <asp:RadioButton ID="rbtnPAP" runat="server" Text="PAP" GroupName="Rxtype" />&nbsp;</td>
           
           <td align="left" >&nbsp;<asp:Label ID="lblDate" runat="server" Text="Date :"></asp:Label>&nbsp;<asp:TextBox ID="txtDate" runat="server" ></asp:TextBox>
           
            <cc1:CalendarExtender ID="CE_ExpiryDate" runat="server" TargetControlID="txtDate" 
                                  PopupButtonID="txtDate" Enabled="True"></cc1:CalendarExtender>
                                  </td> 
           
           <td width="25px">
           <asp:LinkButton ID="btnview" runat="server" onclick="btnview_Click">View</asp:LinkButton></td>
           
       </tr>
       <tr class="medication_info_tr-even">
            <td ><div id="divSelect" style="display:inline" runat="server">
           <asp:RadioButtonList ID="rbtnSelect" runat="server" RepeatColumns="2">
           <asp:ListItem Text="Paducah" Value="P" Selected="True"></asp:ListItem>
           <asp:ListItem Text="ETown" Value="E"></asp:ListItem>
           </asp:RadioButtonList>
       
       </div></td>   
       <td colspan="2">
           </td>
       
       </tr>
       <tr class="medication_info_tr-odd">
       <td colspan="3">
           <asp:GridView ID="gridRx" runat="server"
           
           AutoGenerateColumns="False"  
                      OnRowCommand="gridRx_RowCommand" EmptyDataText="No Records Found..."
                     >
           <Columns>
           
            <asp:TemplateField HeaderText="Patient"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="350px">
                           
                            <ItemTemplate>
                                <asp:LinkButton ID="btnGenrateStamp" runat="server" Text='<%# Eval("Patient")%>' CommandArgument='<%# Eval("Pat_id")%>' CommandName="Generate"></asp:LinkButton>
                                                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:BoundField HeaderText="Drugs"   DataField='Drugs' />
                                             
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
           </asp:GridView>
           </td>
       </tr>
       
       
       </table>
       
       </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

