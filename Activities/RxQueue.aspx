<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxQueue.aspx.cs" Inherits="RxQueue" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
 
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
               <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
               </td>
                   
            </tr>
              <tr class="medication_info_tr-odd">
              <td align="left" valign="middle" colspan="3">
                <asp:Label ID="lblRxQueueSearchDate" runat="server" Text="Search by Date"></asp:Label>
                <asp:TextBox ID="txtRxQueueSearchDate" runat="server" Width="100px"></asp:TextBox>
                <cc1:CalendarExtender ID="ceRxQueueSearchDate" PopupButtonID="txtRxQueueSearchDate" TargetControlID="txtRxQueueSearchDate" PopupPosition="BottomLeft" runat="server"></cc1:CalendarExtender>
               <asp:ImageButton ID="btnRxQueueSearchDate" runat="server" border="0" Style="height: 24px" 
                                ImageUrl="~/images/search_new.png"  Width="24px" 
                      onclick="btnRxQueueSearchDate_Click"/>
              </td>
            <td align="right">
                &nbsp;&nbsp;
            <a href="RxQueueList.aspx" >Rx Queue List</a>  
                &nbsp;&nbsp;
            </td>
            </tr>
            <tr class="medication_info_tr-even">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxQueue" runat="server" AutoGenerateColumns="False" 
                     Width="800px"  OnRowCommand="gridRxQueue_RowCommand" EmptyDataText="No Records Found..."  >
                       <Columns>
                            <asp:TemplateField HeaderText="Clinic" ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>'></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                            <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblFacility_Name" runat="server" Text='<%#Eval("Facility_Name")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="New Patients" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblNewPatients" runat="server" Text='<%# Eval("NewPatients")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="New Rx"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblNewRx" runat="server" Text='<%# Eval("NewRx")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Processed"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblprocessed" runat="server" Text='<%# Eval("Processed")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="UnProcessed"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblunprocessed" runat="server" Text='<%# Eval("UnProcessed")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Refills"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRefills" runat="server" Text='<%# Eval("Refills")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Sample"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblsample" runat="server" Text='<%# Eval("Sample")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="PAP"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPAP" runat="server" Text='<%# Eval("PAP")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Other"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="50px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblOther" runat="server" Text='<%# Eval("OP")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField ItemStyle-Width="60px">
                                <ItemTemplate >
                                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Summary" CommandArgument='<%# Eval("Facility_ID") %>' Font-Bold="false">Summary</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="100px">
                                <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Payment" CommandArgument='<%# Eval("Facility_ID") %>' Font-Bold="false">Payment</asp:LinkButton>
                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="100px">
                                <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Sample" CommandArgument='<%# Eval("Facility_ID") %>' Font-Bold="false">Sample</asp:LinkButton>
                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="100px">
                                <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton4" runat="server" CommandName="PAP" CommandArgument='<%# Eval("Facility_ID") %>' Font-Bold="false">PAP</asp:LinkButton>
                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
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

