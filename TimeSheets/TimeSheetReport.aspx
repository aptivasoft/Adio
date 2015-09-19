<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="TimeSheetReport.aspx.cs" Inherits="TimeSheetReport" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
                   <cc1:AutoCompleteExtender ID="ACE_EmployeeName"  TargetControlID="txtEmployeeName" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetEmpNames" OnClientItemSelected="AutoCompleteSelectedUser" >
         </cc1:AutoCompleteExtender>
   
 
    <table  align="center"  width="900px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="5" style="height:25px;vertical-align:middle;"  > 
                    <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
             <tr class="medication_info_tr-odd">
             <td style="width:20%">
                 <asp:Label ID="lblEmployee" runat="server" Text="Employee"></asp:Label></td>
             <td style="width:20%">
                 <asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox></td>
             <td style="width:20%">
                 <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label></td>
             <td style="width:20%">
                 <asp:DropDownList ID="ddlStatus" runat="server">
                 <asp:ListItem Text="All" Value="ALL"></asp:ListItem>
                 <asp:ListItem Text="Submitted" Value="S"></asp:ListItem>
                 <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                 <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
                 </asp:DropDownList>
                 </td>
                 <td style="width:20%"><asp:LinkButton ID="btnview" runat="server" onclick="btnView_Click">View</asp:LinkButton>
               </td>
             </tr>
             <tr class="medication_info_tr-even">
             <td>
                 <asp:Label ID="lblStartDate" runat="server" Text="PP Start Date"></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                  <cc1:CalendarExtender ID="Getdate" runat="server" TargetControlID="txtStartDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtStartDate" 
                        PopupPosition="BottomLeft" ></cc1:CalendarExtender>
                        </td>
             <td>
                 <asp:Label ID="lblEndDate" runat="server" Text="PP End Date"></asp:Label></td>
             <td>
                 <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtEndDate" 
                        PopupPosition="BottomLeft" ></cc1:CalendarExtender></td>
                        <td style="width:20%"></td>
             </tr>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="5" >
                <asp:GridView ID="gridTimeSheetList" runat="server" AutoGenerateColumns="False" 
                     Width="900px"  OnRowCommand="gridTimeSheetList_RowCommand" 
                    EmptyDataText="No Records Found..." 
                   onrowdatabound="gridTimeSheetList_RowDataBound" 
>
                       <Columns>
                           <asp:HyperLinkField DataNavigateUrlFields="Emp_ID,PP_ID"  ItemStyle-HorizontalAlign="Left"
                               DataNavigateUrlFormatString="VerifyTimesheet.aspx?empID={0}&PPID={1}" 
                               DataTextField="empName" HeaderText="Employee" />
                        <%-- <asp:BoundField HeaderText="Employee"   ItemStyle-HorizontalAlign="Left" DataField="empName"  ItemStyle-Font-Bold="false"/> --%>  
                        <asp:BoundField HeaderText="PP S-Date" DataFormatString="{0:MM/dd/yyyy}"   
                               ItemStyle-HorizontalAlign="Center" DataField="PP_StartDate" 
                               ItemStyle-Font-Bold="false"  >
                            <ItemStyle Font-Bold="False" HorizontalAlign="Center" />
                           </asp:BoundField>
                         <asp:BoundField HeaderText="Regular Hours"  NullDisplayText="0" 
                               ItemStyle-HorizontalAlign="Right" DataField="RegularHours"  
                               ItemStyle-Font-Bold="false">
                             <ItemStyle Font-Bold="False" HorizontalAlign="Right" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="OT/UT" NullDisplayText="0" 
                               ItemStyle-HorizontalAlign="Right" DataField="OT_UT"  
                               ItemStyle-Font-Bold="false">
                        
                      
                               <ItemStyle Font-Bold="False" HorizontalAlign="Right" />
                           </asp:BoundField>
                        
                      
                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Approved By"  ItemStyle-HorizontalAlign="Left"  >
                          
                            <ItemTemplate>
                               <asp:Label ID="lblApprovedby" runat="server" Text='<%# Eval("TS_ApprovedBy")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                    
                       
                                            
                       
                           
                    
                       
                                            
                       
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

