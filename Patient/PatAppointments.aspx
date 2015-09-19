<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="PatAppointments.aspx.cs" Inherits="Patient_PatAppointments" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="demo" runat="server">
<ContentTemplate>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" 
                                 BehaviorID="acCustodianEx1" TargetControlID ="txtDoc" UseContextKey="True" 
                                 runat="server"  MinimumPrefixLength="1"  Enabled="True"  
                                 ServiceMethod="GetDoctorNames" DelimiterCharacters="" ServicePath="" OnClientItemSelected="publishAppInfo"></cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2"
                                 BehaviorID="acCustodianEx2" TargetControlID ="txtFacility" UseContextKey="true" 
                                 runat="server"  MinimumPrefixLength="1"  Enabled="true"  
                                 ServiceMethod="GetClinicFaclityNames" DelimiterCharacters="" ServicePath=""></cc1:AutoCompleteExtender>

<table width="100%">
    <tr>
        <td colspan="2" align="center" class="medication_info_th1">
            Patient Appointments
        </td>
    </tr>
    <tr class="medication_info_tr-odd">
        <td>
            <asp:Label ID="lblPatAccount"  runat="server" Text="Patient Account"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPatAccount1"  runat="server" Text="Patient Account"></asp:Label>
        </td>
    </tr>
    
    <tr class="medication_info_tr-even">
        <td>
            <asp:Label ID="lblDoc" runat="server" Text="Doctor"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDoc" runat="server" ></asp:TextBox>
        </td>
    </tr>
    <tr class="medication_info_tr-odd">
        <td>
            <asp:Label ID="lblFacility" runat="server" Text="Facility"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFacility" runat="server" ></asp:TextBox>
        </td>
    </tr>
    <tr class="medication_info_tr-even">
        <td>
            <asp:Label ID="lblAppPurpose" runat="server" Text="Purpose Of Appointment"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAppPurpose" runat="server" ></asp:TextBox>
            <asp:Button ID="btnPublish" runat="server" style="visibility:hidden" onclick="btnPublish_Click"/>
             </td>
    </tr>
      <tr class="medication_info_tr-odd">
     <td>
                <asp:Label ID="lblAppType" runat="server" Text="Appointment Type"></asp:Label>
        </td>
        <td>
                <asp:RadioButton ID="rbtnCSR" runat="server" Text="CSR" GroupName="AppTyep" Checked="true"/>
                <asp:RadioButton ID="rbtnDoc" runat="server" Text="Doctor" GroupName="AppTyep" />
        </td>
    </tr>
    <tr class="medication_info_tr-even">
        <td>
                    <asp:Label ID="lblDateTime" runat="server" Text="Date And Time"></asp:Label>
        </td>
         <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="60Px"></asp:TextBox> 
                    <cc1:CalendarExtender ID="Getdate" runat="server" TargetControlID="txtDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtDate" 
                        PopupPosition="BottomLeft" OnClientDateSelectionChanged="publishAppInfoByDate"></cc1:CalendarExtender>
                    <asp:ListBox ID="lbappointmentTime" runat="server" Rows="1" ></asp:ListBox>
         </td>
    </tr>
    <tr class="medication_info_tr-odd">
        <td>
            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="5" Width="250px" MaxLength="255" ></asp:TextBox>
        </td>
    </tr>
  
    <tr class="medication_info_tr-even">
        <td colspan="2" align="center">
            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/images/save.gif" 
                onclick="btnSubmit_Click" OnClientClick="return validateAppointment();"/>           
            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/images/cancel.gif" 
                onclick="btnCancel_Click" />    
        </td>
    </tr>
    <tr >
        <td colspan="2">
            <asp:Label ID="lblCurAppDate" runat="server" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gridApointments" runat="server" Width="100%" 
                AutoGenerateColumns="False" AllowPaging="True" 
                onpageindexchanging="gridApointments_PageIndexChanging" PageSize="5" 
                onrowcommand="gridApointments_RowCommand" 
                onrowdeleting="gridApointments_RowDeleting" EmptyDataText="No Appointments" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-HorizontalAlign="Center" >
            <AlternatingRowStyle CssClass="medication_info_tr-even" />
            <Columns>
            <asp:TemplateField>
              <ItemTemplate>
                   <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/images/delete.gif" OnClientClick="if(!confirm('Do you want to delete Appointment')){ return false;}" CommandName="Delete" CommandArgument='<%# Eval("APPT_ID") %>'/>                                   
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" Width="20px" />
              </asp:TemplateField>
            <asp:TemplateField HeaderText="Time">
                            <ItemTemplate>
                                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("APPT_Time")%>'></asp:Label>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Facility">
                  <ItemTemplate>
                                <asp:Label ID="lblFacility" runat="server" Text='<%# Eval("Facility_Name")%>'></asp:Label>                                
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Left" />
           </asp:TemplateField>                          
           <asp:TemplateField HeaderText="Doctor">
                   <ItemTemplate>
                                <asp:Label ID="lblDoc" runat="server" Text='<%# Eval("Doc_FullName")%>'></asp:Label>                                
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Left" />
           </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Patient">
                            <ItemTemplate>
                                <asp:Label ID="lblPatName" runat="server" Text='<%# Eval("Pat_LName")%>'></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text=', '></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Pat_FName")%>'></asp:Label>                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                   </asp:TemplateField> 
            </Columns>
             <HeaderStyle CssClass="medication_info_th1" />
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPrevious" 
                    NextPageText="Next" PreviousPageText="Prev" />
             <RowStyle CssClass="medication_info_tr-odd" />    
            </asp:GridView>
        </td>
    </tr>
</table>
    <asp:HiddenField ID="hidDocID" runat="server" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

