<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="MedRequest.aspx.cs" Inherits="MedRequest" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode = "Conditional" RenderMode="Inline">
       <ContentTemplate>
         <cc1:AutoCompleteExtender ID="AutocompletePatients" BehaviorID="acCustodianEx" TargetControlID ="txtPatname" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true"  ServiceMethod="GetPatinetNames" OnClientItemSelected="AutoCompleteSelected_Patient"></cc1:AutoCompleteExtender>
         <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" BehaviorID="acCustodianEx1" 
                TargetControlID="txtDocName" UseContextKey="true" runat="server"  
                MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetDoctorNames" 
                OnClientItemSelected="AutoCompleteSelected_Doctor" >
         </cc1:AutoCompleteExtender>
         <cc1:AutoCompleteExtender   ID="AutoCompleteExtender1" 
                                     BehaviorID="acCustodianEx2" TargetControlID ="txtMedicationName" 
                                     UseContextKey="True" runat="server"  MinimumPrefixLength="1"  Enabled="True"  
                                     ServiceMethod="GetMedicationNames" DelimiterCharacters="" ServicePath=""></cc1:AutoCompleteExtender>
        <!-- Add Auto Complete Extender for SIG CODES -->
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" TargetControlID="txtSIG" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetSIGNames" BehaviorID="ModelExtender" CompletionListCssClass="AutoExtender1"  runat="server">
        </cc1:AutoCompleteExtender>
       <table align="center" width="100%" >
       <tr class="medication_info_th1">
       <td colspan="3" align="center" valign="middle" height="25px">New Med Request</td>
       </tr>
       
       <tr class="medication_info_tr-odd">
       <td ><asp:Label ID="lblPatName" runat="server" Text="PatientName"></asp:Label></td>       
       <td colspan="2"><asp:TextBox ID="txtPatname" runat="server" Width="250px"></asp:TextBox></td>
       </tr>
       <tr class="medication_info_tr-even">
       <td ><asp:Label ID="lblDocname" runat="server" Text="Doctor"></asp:Label></td>       
       <td colspan="2"><asp:TextBox ID="txtDocName" runat="server" Width="250px"></asp:TextBox></td>
       </tr>
       <tr class="medication_info_tr-odd">
       <td><asp:Label ID="lblDrugName" runat="server" Text="Drug Name"></asp:Label></td>       
       <td colspan="2"><asp:TextBox ID="txtMedicationName" runat="server" Width="250px"></asp:TextBox></td>
       </tr>
       <tr class="medication_info_tr-even">
       <td ><asp:Label ID="lblQty" runat="server" Text="Qty" ></asp:Label></td>       
       <td colspan="2"><asp:TextBox ID="txtQty" runat="server" Width="250px"></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="ftQty" runat="server" FilterType="Numbers" TargetControlID="txtQty"></cc1:FilteredTextBoxExtender>
       </td>
       </tr>
       <tr class="medication_info_tr-odd">
       <td><asp:Label ID="lblSIG" runat="server" Text="SIG"></asp:Label></td>       
       <td colspan="2"><asp:TextBox ID="txtSIG" runat="server" Width="250px"></asp:TextBox></td>
       </tr>
       <tr class="medication_info_tr-even">
           <td ><asp:Label ID="lblPharmacy" runat="server" Text="Pharmacy"></asp:Label></td>       
           <td colspan="2">
               <asp:RadioButtonList ID="rbtnPhrm" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true"
                   onselectedindexchanged="rbtnPhrm_SelectedIndexChanged">
               <asp:ListItem Text="ADiO" Value="0" Selected="True"></asp:ListItem>
               <asp:ListItem Text="Other" Value="1" ></asp:ListItem>
               </asp:RadioButtonList> 
             
               <asp:TextBox ID="txtPharmacy" runat="server" Width="250px" Visible="false"></asp:TextBox>
            </td>       
       </tr>
       <tr class="medication_info_tr-odd">
       <td><asp:Label ID="txtDelivery" runat="server" Text="Delivery" ></asp:Label></td>       
       <td colspan="2">
           <asp:RadioButtonList ID="rbtnDelivery" runat="server" RepeatColumns="4">
           </asp:RadioButtonList>
           
       </td>
       </tr>
       <tr class="medication_info_tr-even">
       <td ><asp:Label ID="lblRxType" runat="server" Text="Rx Type"></asp:Label></td>       
       <td colspan="2">
       <asp:RadioButtonList ID="rbtnRequestType" runat="server" RepeatColumns="4">
           </asp:RadioButtonList>
           
       </td>
       </tr>
       <tr class="medication_info_tr-odd">
       <td><asp:Label ID="lblReqType" runat="server" Text="Request Type"></asp:Label></td>       
        <td colspan="2">
           <asp:RadioButton ID="rbtnPatCalled" runat="server" Text="Patient Called" Checked="true" GroupName="RequestType" />
           <asp:RadioButton ID="rbtnFacPharmCall" runat="server" Text="Facility/Pharma Called" GroupName="RequestType" />
        </td>
        </tr>
        <tr class="medication_info_tr-even">
        <td ><asp:Label ID="lblPatContact" runat="server" Text="Patient Contact"></asp:Label></td>       
        <td colspan="2"><asp:TextBox ID="txtPatContact" runat="server" Width="250px"></asp:TextBox></td>        
        </tr>
        <tr class="medication_info_tr-odd">
        <td><asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label></td>       
        <td colspan="2"><asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" 
                Rows="3" Width="400px"></asp:TextBox></td>        
        </tr>
        <tr class="medication_info_tr-even">
        <td colspan="4" align="center">
        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/images/save.gif" onclick="btnSubmit_Click"  OnClientClick="return ValidateMedRequest();"/>
        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/images/cancelPopUp.gif"  onclick="btnCancel_Click" />
       </td>
        </tr>
        <tr class="medication_info_tr-odd">
          <td colspan="3" align="center">&nbsp;&nbsp;<asp:Label ID="lblMsg" Text="" runat="server" Visible="false" Font-Bold="true" ></asp:Label></td>
       </tr>
       </table>
           <asp:HiddenField ID="hidPatID" runat="server" />
           <asp:HiddenField ID="hidDocID" runat="server" />
       </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

