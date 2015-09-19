<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxSummary.aspx.cs" Inherits="RxSummary" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
 
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"   > <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxSummary" runat="server" AutoGenerateColumns="False" 
                     Width="800px"  EmptyDataText="No Records Found...">
                       <Columns>
                            
                        <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblFacility_Name" runat="server" Text='<%#Eval("RxDate")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>    
                                                
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="New Patients" ItemStyle-HorizontalAlign="Right">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblNewPatients" runat="server" Text='<%# Eval("NewPatients")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="New Rx"  ItemStyle-HorizontalAlign="Right">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblNewRx" runat="server" Text='<%# Eval("NewRx")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Processed"  ItemStyle-HorizontalAlign="Right">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblprocessed" runat="server" Text='<%# Eval("Processed")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="UnProcessed"  ItemStyle-HorizontalAlign="Right">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblunprocessed" runat="server" Text='<%# Eval("UnProcessed")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Refills"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRefills" runat="server" Text='<%# Eval("Refills")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Other"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblOther" runat="server" Text='<%# Eval("OP")%>' Font-Bold="false"></asp:Label>                            
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

