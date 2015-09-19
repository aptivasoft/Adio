<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="DeliveryTracking.aspx.cs" Inherits="DeliveryTracking" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
      <table align="center" width="600px" >
       <tr class="medication_info_th1">
       <td colspan="4" align="center" valign="middle" height="25px" width="100%">Tracking</td>
       </tr>
       
       <tr class="medication_info_tr-odd" >
             
       <td  align="left" width="200px">
           <asp:RadioButton ID="rbtnRegular" runat="server" Text="Regular" GroupName="Rxtype" Checked="true" />&nbsp;
           <asp:RadioButton ID="rbtnSample" runat="server" Text="Sample" GroupName="Rxtype" />&nbsp;
           <asp:RadioButton ID="rbtnPAP" runat="server" Text="PAP" GroupName="Rxtype" />&nbsp;</td>
           
           <td align="left" >&nbsp;<asp:Label ID="lblDate" runat="server" Text="Date :"></asp:Label>&nbsp;<asp:TextBox ID="txtDate" runat="server" ></asp:TextBox><cc1:CalendarExtender ID="CE_ExpiryDate" runat="server" TargetControlID="txtDate" 
                                  PopupButtonID="txtDate" Enabled="True"></cc1:CalendarExtender>
                                  </td> 
           
           <td width="25px">
           <asp:LinkButton ID="btnview" runat="server" onclick="btnview_Click">View</asp:LinkButton></td>
           
           <td >
       <asp:LinkButton ID="btnTrack" runat="server" onclick="btnTrack_Click">Update Tracking</asp:LinkButton>
       </td>
       
       </tr>
       
       <tr class="medication_info_tr-even">
       <td colspan="4" align="center">
           <asp:GridView ID="gridTracking" runat="server"
           
           AutoGenerateColumns="False"  
                       EmptyDataText="No Records Found..."
                    Width="100%" >
           <Columns>
                        <asp:BoundField HeaderText="Patient" ItemStyle-HorizontalAlign="Left"   DataField='patient' />
                       <asp:BoundField HeaderText="Drugs" ItemStyle-HorizontalAlign="Left"   DataField='Drugs' />
                       <asp:BoundField HeaderText="Qty" ItemStyle-HorizontalAlign="Right"   DataField='qty' />
                       <asp:BoundField HeaderText="ShipDate" ItemStyle-HorizontalAlign="Center"  DataField='ShipDate' DataFormatString="{0:MM/dd/yyyy}" />
                       <asp:BoundField HeaderText="Delivery Status" ItemStyle-HorizontalAlign="Center"   DataField='status' />
                                             
                       
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

