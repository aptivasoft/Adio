<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="VerifyTimesheet.aspx.cs" Inherits="VerifyTimesheet" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatepanelTimeSheet" runat="server">
<ContentTemplate>
  <table width="100%">
       <tr class="medication_info_th1">
        <td colspan="4" align="center">Time Sheet</td>
       </tr>
       <tr class="medication_info_tr-even">
           <td>
                <asp:Label ID="lblempName" runat="server" Text="Employee Name"></asp:Label>
           </td>
           <td>
                <asp:Label ID="lblempName1" runat="server" Text=""></asp:Label>
           </td> 
           <td>
                <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
           </td>
           <td>
                <asp:Label ID="lblPhone1" runat="server" Text=""></asp:Label>
           </td> 
       </tr>
        <tr class ="medication_info_tr-odd">
           <td>
                <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
           </td>
           <td>
                <asp:Label ID="lblLocation1" runat="server" Text=""></asp:Label>
           </td> 
           <td>
                <asp:Label ID="lblEmail" runat="server" Text="E-Mail"></asp:Label>
           </td>
           <td>
                <asp:Label ID="lblMailId1" runat="server"></asp:Label>
           </td> 
       </tr>
        <tr class="medication_info_tr-even">
           <td>
                <asp:Label ID="lblSupervisor" runat="server" Text="Supervisor"></asp:Label>
           </td>
           <td>
                <asp:Label ID="lblSupervisor1" runat="server" Text=""></asp:Label>
           </td> 
           <td>
                <asp:Label ID="AltlblSupervisor" runat="server" Text="Alt. Supervisor"></asp:Label>
           </td>
           <td>
                <asp:Label ID="AltlblSupervisor1" runat="server" Text="--"></asp:Label>
           </td> 
       </tr>
       <tr class ="medication_info_tr-odd">
           <td >
                <asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label>
           </td>
           <td colspan="3">
           
                <asp:TextBox ID="txtComments" runat="server" Text="" TextMode="MultiLine" Rows="3" Width="250px"></asp:TextBox>
           </td> 
       </tr>
       <tr class="medication_info_tr-even">
           <td colspan="4">
           
                   
                     <asp:GridView ID="gridTimeSheetList" runat="server" AutoGenerateColumns="False" 
                     Width="1000px"   EmptyDataText="No Records Found..."  ShowFooter="true"  >
                       <Columns>
                        <asp:TemplateField HeaderText="Hours" ItemStyle-HorizontalAlign="Right" Visible="false" >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblTDID" runat="server"  Text='<%# Eval("TD_ID")%>' Font-Bold="false"></asp:Label>                       
                              
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                         <asp:BoundField HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}"   
                               ItemStyle-HorizontalAlign="Center" DataField="Date" 
                               ItemStyle-Font-Bold="false"  >
                             <ItemStyle Font-Bold="False" HorizontalAlign="Center" />
                           </asp:BoundField>
                         <asp:BoundField HeaderText="Day" ItemStyle-HorizontalAlign="Left" 
                               DataField="Day"  ItemStyle-Font-Bold="false" FooterText="Total">
                          
                             <ItemStyle Font-Bold="False" HorizontalAlign="Left" />
                           </asp:BoundField>
                          
                       <asp:TemplateField HeaderText="Hours" ItemStyle-HorizontalAlign="Right"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblHours" runat="server"  Text='<%# Eval("TD_Hours")%>'  Font-Bold="false"></asp:Label>                       
                             
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                           <FooterTemplate>
                           <asp:Label ID="lblTHours" runat="server"    Font-Bold="true"></asp:Label>                       
                        
                           </FooterTemplate>
                           <FooterStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="OT/UT" ItemStyle-HorizontalAlign="Right"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblOTUT" runat="server"  Text='<%# Eval("TD_OTUT")%>' Font-Bold="false"></asp:Label>                       
                                
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Absent Code" ItemStyle-HorizontalAlign="Left"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblABC" runat="server"  Text='<%# Eval("TD_AbsentCode")%>' Font-Bold="false"></asp:Label>                       
                               
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Activity Code" ItemStyle-HorizontalAlign="Left"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblAC" runat="server"  Text='<%# Eval("TD_ActivityCode")%>' Font-Bold="false"></asp:Label>                       
                                
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="left" >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server"  Text='<%# Eval("TD_Comment")%>' Font-Bold="false"></asp:Label>                       
                                
                           </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <FooterStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                   
                   
         
           </td>
       </tr>
       <tr class="medication_info_tr-odd">
       <td style="width:25%">
           <asp:Label ID="lblSubmittedby" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
            <td style="width:25%">
           <asp:Label ID="lblSubmittedDate" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
            <td style="width:25%">
           <asp:Label ID="lblApprovedby" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
            <td style="width:25%">
           <asp:Label ID="lblApprovedDate" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
       </tr>
       <tr class="medication_info_tr-even">
       <td style="width:25%">
           <asp:Label ID="Label1" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
            <td style="width:25%">
           <asp:Label ID="Label2" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
            <td style="width:25%">
           <asp:Label ID="Label3" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
            <td style="width:25%">
           <asp:Label ID="Label4" runat="server" Text="" Font-Bold="false"></asp:Label>
               
            </td>
       </tr>
       <tr class="medication_info_tr-odd">
           <td colspan="4" align="center">
               <asp:LinkButton ID="btnApprove" runat="server" onclick="btnApprove_Click">Approve</asp:LinkButton>
               &nbsp;&nbsp;
               <asp:LinkButton ID="btnReject" runat="server" onclick="btnReject_Click">Reject</asp:LinkButton>
               &nbsp;&nbsp;
               <asp:LinkButton ID="btnCancel" runat="server" onclick="btnCancel_Click">Cancel</asp:LinkButton>
       
            </td>
       </tr>
    </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

