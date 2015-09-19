<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxPayment.aspx.cs" Inherits="RxPayment" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
 
    <table  align="center"  width="800px"  >
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4"  style="height:25px;vertical-align:middle;"   > 
                        <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxPayment" runat="server" AutoGenerateColumns="False" 
                     Width="800px" OnRowDataBound="gridRxPayment_RowDataBound" ShowFooter="true" EmptyDataText="No Records Found..." >
                       <Columns>
                            
                        <asp:TemplateField HeaderText="Patient" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Label ID="lblFacility_Name" runat="server" Text='<%#Eval("PatientName")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>   
                            <FooterTemplate >
                                <asp:Label ID="Label1" runat="server" Text="Grand Total"></asp:Label></FooterTemplate>                     
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblNewPatients" runat="server" Text='<%# Eval("PayDate")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Amount ($)"  ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblNewRx" runat="server" Text='<%# Eval("Payment_Amount")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                             <FooterTemplate>
                                <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label></FooterTemplate> 
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Description"  ItemStyle-HorizontalAlign="Left">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRefills" runat="server" Text='<%# Eval("Payment_Notes")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       
                       
                       
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                        <FooterStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 </td>
            </tr>
            </table>
    
</asp:Content>

