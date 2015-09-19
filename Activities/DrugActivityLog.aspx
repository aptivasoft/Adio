<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="DrugActivityLog.aspx.cs" Inherits="DrugActivityLog" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
 
    <table  align="center"  width="100%" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text="Drug Activity Log"></asp:Label>
                    </td>
                   
            </tr>
         
            <tr class="medication_info_tr-even">
            <td align="left"  >
             <asp:Label ID="lblDrug" runat="server" Text="Drug :" Font-Bold ="true"></asp:Label>&nbsp;&nbsp; <asp:Label ID="lblDrug1" runat="server" Text=""></asp:Label>
            </td>
            <td align="left"  >
             <asp:Label ID="lblStrength" runat="server" Text="Strength/Form :" Font-Bold ="true"></asp:Label>&nbsp;&nbsp; <asp:Label ID="lblStrength1" runat="server" Text=""></asp:Label>
            </td>
            <td align="left" >
             <asp:Label ID="lblType" runat="server" Text="Type :" Font-Bold ="true"></asp:Label>&nbsp;&nbsp; <asp:Label ID="lblType1" runat="server" Text=""></asp:Label>
          
            </td>
             <td align="left"  >
             <asp:Label ID="lblFacility" runat="server" Text="Location :" Font-Bold ="true"></asp:Label>&nbsp;&nbsp; <asp:Label ID="lblFacility1" runat="server" Text=""></asp:Label>
          
            </td>
            </tr>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxQueue" runat="server" AutoGenerateColumns="False" Width="100%"  OnRowCommand="gridRxQueue_RowCommand" EmptyDataText="No Records Found..."  >
                       <Columns>
                            <asp:TemplateField HeaderText="Date/Time" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px"  >
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Inv_Date")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Trans Type" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                            <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblType" runat="server" Text='<%#Eval("Inv_Trans_Desc")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Patient" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                           <ItemTemplate>
                                <asp:Label ID="lblPatient" runat="server" Text='<%# Eval("PatientName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Qty"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lot"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="75px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblLotNum" runat="server" Text='<%# Eval("Lot_Num")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exp Date"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="75px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblExpiryDate" runat="server" Text='<%# Eval("Expiry_Date")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                      
                       <asp:TemplateField HeaderText="Comments"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Inv_Desc")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 </td>
            </tr>
            </table>
    
</asp:Content>

