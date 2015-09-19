<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Timesheet.aspx.cs" Inherits="Patient_Timesheet" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatepanelTimeSheet" runat="server">
<ContentTemplate>
  <table align="center"  width="900px" class="patient_info" >
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
                <asp:Label ID="lblComments1" runat="server" Text=""></asp:Label>
           </td> 
       </tr>
       <tr class="medication_info_tr-even">
           <td colspan="4">
           
                   
                     <asp:GridView ID="gridTimeSheetList" runat="server" AutoGenerateColumns="False" 
                     Width="900px"   EmptyDataText="No Records Found..."  ShowFooter="True" >
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
                          
                       <asp:TemplateField HeaderText="Hours" ItemStyle-HorizontalAlign="Center"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblHours" runat="server" Visible='<%# !(bool) display_link( Eval("Date")) %>' Text='<%# Eval("TD_Hours")%>'  Font-Bold="false"></asp:Label>                       
                               <asp:TextBox ID="txtHours" runat="server" Visible='<%# display_link( Eval("Date")) %>' Text='<%# Eval("TD_Hours")%>' onblur="CalculateTotal();" Width="50px"></asp:TextBox>
                               <cc1:FilteredTextBoxExtender ID="FTHours" runat="server" FilterType="Numbers" TargetControlID="txtHours"></cc1:FilteredTextBoxExtender>
         
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                           <FooterStyle HorizontalAlign="Right" />
                           <FooterTemplate>
                           <asp:TextBox ID="txtTHours" runat="server"   Width="50px" ReadOnly="true"></asp:TextBox>
                           <asp:Label ID="lblTHours" runat="server"   Width="50px" ReadOnly="true"></asp:Label>
                         
                           </FooterTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="OT/UT" ItemStyle-HorizontalAlign="Right"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblOTUT" runat="server" Visible='<%# !(bool) display_link( Eval("Date")) %>' Text='<%# Eval("TD_OTUT")%>' Font-Bold="false"></asp:Label>                       
                                <asp:TextBox ID="txtOTUT" runat="server" Visible='<%# display_link( Eval("Date")) %>' Text='<%# Eval("TD_OTUT")%>' Width="50px"></asp:TextBox>
                                 <cc1:FilteredTextBoxExtender ID="FTOTUT" runat="server" FilterType="Numbers" TargetControlID="txtOTUT"></cc1:FilteredTextBoxExtender>
         
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Absent Code" ItemStyle-HorizontalAlign="Left"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblABC" runat="server" Visible='<%# !(bool) display_link( Eval("Date")) %>' Text='<%# Eval("TD_AbsentCode")%>' Font-Bold="false"></asp:Label>                       
                                <asp:TextBox ID="txtABC" runat="server" Visible='<%# display_link( Eval("Date")) %>' Text='<%# Eval("TD_AbsentCode")%>' Width="150px"></asp:TextBox>
                                
                                   <cc1:AutoCompleteExtender ID="ACE_ABC"  
                                 TargetControlID="txtABC" UseContextKey="true" 
                                 runat="server"  MinimumPrefixLength="1"  Enabled="true" 
                                 ServiceMethod="GetABCodes"  ></cc1:AutoCompleteExtender>
                                <%-- <cc1:FilteredTextBoxExtender ID="FTABC" runat="server" FilterType="Numbers" TargetControlID="txtABC"></cc1:FilteredTextBoxExtender>--%>
         
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Activity Code" ItemStyle-HorizontalAlign="Left"  >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblAC" runat="server" Visible='<%# !(bool) display_link( Eval("Date")) %>' Text='<%# Eval("TD_ActivityCode")%>' Font-Bold="false"></asp:Label>                       
                                <asp:TextBox ID="txtAC" runat="server" Visible='<%# display_link( Eval("Date")) %>' Text='<%# Eval("TD_ActivityCode")%>' Width="150px"></asp:TextBox>
                                
                                 <cc1:AutoCompleteExtender ID="ACE_EmployeeName"  
                                 TargetControlID="txtAC" UseContextKey="true" 
                                 runat="server"  MinimumPrefixLength="1"  Enabled="true" 
                                 ServiceMethod="GetACodes"  ></cc1:AutoCompleteExtender>
                                 <%--<cc1:FilteredTextBoxExtender ID="FTAC" runat="server" FilterType="Numbers" TargetControlID="txtAC"></cc1:FilteredTextBoxExtender>--%>
         
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="left" >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Visible='<%# !(bool) display_link( Eval("Date")) %>' Text='<%# Eval("TD_Comment")%>' Font-Bold="false"></asp:Label>                       
                                <asp:TextBox ID="txtComments" runat="server" Visible='<%# display_link( Eval("Date")) %>' Text='<%# Eval("TD_Comment")%>'></asp:TextBox>
                           </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                        <FooterStyle CssClass="medication_info_th1" />
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
               <asp:LinkButton ID="btnSubmit" runat="server" onclick="btnSubmit_Click1">Submit</asp:LinkButton>
               &nbsp;&nbsp;
               <asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click1">Save &amp; Continue</asp:LinkButton>
               &nbsp;&nbsp;
               <asp:LinkButton ID="btnCancel" runat="server" onclick="btnCancel_Click">Cancel</asp:LinkButton>
       
            </td>
       </tr>
    </table>
        <div id="div2"  style="position:absolute;border:solid black 0px;top:25%;right:25%;bottom:25%;left:25%;padding:25px; margin:25px;">
	 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanelTimeSheet" DisplayAfter="0">
       <ProgressTemplate>
          <div class="modalBackground"> 
           <table>
           <tr>
           <td class="label" valign="middle" align="center">
                 Processing...Please Wait..!
           </td>
           </tr>
           </table>
           </div>
       </ProgressTemplate>
   </asp:UpdateProgress>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

