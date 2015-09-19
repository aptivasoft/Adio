<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxInventory.aspx.cs" Inherits="RxInventory" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
      <cc1:AutoCompleteExtender ID="AutocompletePatients" BehaviorID="acCustodianEx" TargetControlID ="txtPatname" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true"  ServiceMethod="GetPatinetNames" OnClientItemSelected="AutoCompleteSelected_Patient"></cc1:AutoCompleteExtender>

      <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" 
                                 BehaviorID="acCustodianEx2" TargetControlID ="txtDrug" 
                                 UseContextKey="True" runat="server"  MinimumPrefixLength="1"  Enabled="True"  
                                 ServiceMethod="GetMedicationNames" DelimiterCharacters="" ServicePath=""  OnClientItemSelected="ACS_Drug"></cc1:AutoCompleteExtender>
 <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" 
                                 BehaviorID="acCustodianEx3" TargetControlID ="txtFacility" 
                                 UseContextKey="True" runat="server"  MinimumPrefixLength="1"  Enabled="True"  
                                 ServiceMethod="GetFacilityNames" DelimiterCharacters="" ServicePath=""  OnClientItemSelected="ACS_Facility"></cc1:AutoCompleteExtender>

 
        

        <div>
    <table width="100%"  >
    <tr class="medication_info_th1">
       <td colspan="4" align="center" height="25px">Update Inventory</td>
       </tr>
       <tr>
       <td colspan="2" align="left">&nbsp;&nbsp;<asp:Label ID="lblMsg" Text="" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label></td>
       </tr>
       <tr class="medication_info_tr-odd">
                        <td align="left" >
                            <asp:Label ID="lblFacility" runat="server" Text="Facility"></asp:Label>
                        </td>
                        <td align="left" >
                                        
                            <asp:TextBox ID="txtFacility" runat="server" TabIndex="1"  
                                ></asp:TextBox>
                            
                        </td>
                        </tr>
        <tr class="medication_info_tr-even">
       
                        <td align="left" >
                            <asp:Label ID="lblDrug" runat="server" Text="Drug Name & Strength"></asp:Label>
                        </td>
                        <td  align="left">   
                            
                           <asp:TextBox ID="txtDrug" runat="server" Text="" TabIndex="2"></asp:TextBox>
                        </td>
          </tr>
       
        <%--<tr class="medication_info_tr-even">
       
                        <td align="left" >
                            <asp:Label ID="lblForm" runat="server" Text="Form"></asp:Label>
                        </td>
                        <td  align="left" align="left">   
                            
                            <asp:DropDownList ID="ddlForm" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlForm_SelectedIndexChanged">
                            <asp:ListItem Text="Tablet" Value="Tablet"></asp:ListItem>
                            <asp:ListItem Text="Injection" Value="Injection"></asp:ListItem>
                            <asp:ListItem Text="Capsule" Value="Capsule"></asp:ListItem>
                            <asp:ListItem Text="Solution" Value="Solution"></asp:ListItem>
                            
                            </asp:DropDownList>
                        </td>
          </tr>--%>
        <tr class="medication_info_tr-odd">
                        <td align="left" >
                            <asp:Label ID="lblNDC" runat="server" Text="NDC#"></asp:Label>
                        </td>
                        <td align="left" >
                                        
                            <asp:TextBox ID="txtNDC" runat="server" TabIndex="3"  
                                ></asp:TextBox>
                            
                        </td>
                        </tr>
        <tr class="medication_info_tr-even">
       
                            <td align="left" >
                                <asp:Label ID="lblIType" runat="server" Text="Inventory Type"></asp:Label>
                            </td>
                            <td  align="left">
                                <asp:RadioButtonList ID="rbtnIType" runat="server" TabIndex="4" RepeatColumns="2" 
                                    onselectedindexchanged="rbtnIType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="PAP" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Sample" Value="S"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            </tr>
       <tr class="medication_info_tr-odd">
        
                                <td align="left" >
                                    <asp:Label ID="lblStockQty" runat="server" Text="Qty in Stock"></asp:Label>
                                </td>
                                <td  align="left">
                                    <asp:Label ID="lblStockQty1" runat="server" ></asp:Label> 
                                 
                                    
                                </td>
                            </tr>

        <tr class="medication_info_tr-even">
        
                                <td align="left" >
                                    <asp:Label ID="lblTType" runat="server" Text="Transaction Type"></asp:Label>
                                </td>
                                <td  align="left">
                                    <asp:RadioButtonList ID="rbtnTType" TabIndex="5" runat="server"  AutoPostBack="true"
                                         OnSelectedIndexChanged="rbtnTType_SelectedIndexChanged" RepeatColumns="2">
                                <asp:ListItem Text="Add" Value="A"></asp:ListItem>
                                <asp:ListItem Text="Remove" Value="R"></asp:ListItem>
                                </asp:RadioButtonList>
                                    
                                </td>
                            </tr>
        <tr class="medication_info_tr-odd">
     
                                <td align="left" >
                                    <asp:Label ID="lblPat" runat="server" Text="Patient:"></asp:Label>
                                </td>
                                <td  align="left">
                                   <asp:TextBox ID="txtPatName" runat="server"  Visible="false" TabIndex="6"  Text="" Width="250px"></asp:TextBox>
                        
                                </td>
                            </tr>
        <tr class="medication_info_tr-even">
     
                                <td align="left" >
                                    <asp:Label ID="lblQty" runat="server" Text="Qty updated"></asp:Label>
                                </td>
                                <td  align="left">
                                   <asp:TextBox ID="txtQty" runat="server" TabIndex="7" Text="" Width="50px"></asp:TextBox>
                        
                                </td>
                            </tr>
               <tr class="medication_info_tr-odd">
        
                                <td align="left" >
                                    <asp:Label ID="lblLotNum" runat="server" Text="Lot/Bin #"></asp:Label>
                                </td>
                                <td  align="left">
                                   <asp:TextBox ID="txtLotNum" runat="server" TabIndex="8" Text="" Width="100px"></asp:TextBox> 
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblExpiryDt" runat="server" Text="Expiry Date :"></asp:Label>
                                   <asp:TextBox ID="txtExpiryDate" runat="server" TabIndex="8" Text="" Width="100px"></asp:TextBox>
                                   <cc1:CalendarExtender ID="CE_DOE" runat="server" TargetControlID="txtExpiryDate" PopupButtonID="txtDOE" Enabled="True" ></cc1:CalendarExtender> 
                                </td>
                            </tr>
             <tr class="medication_info_tr-even">
       <td ><asp:Label ID="lblRReason" runat="server" Text="Reason (if removed)"></asp:Label></td>       
       <td >
       <asp:DropDownList ID="ddlRReason" TabIndex="9" runat="server">
                            <asp:ListItem Text="Expired" Value="Expired"></asp:ListItem>
                            <asp:ListItem Text="Transfer" Value="Transfer"></asp:ListItem>
                            <asp:ListItem Text="Recall" Value="Recall"></asp:ListItem>
                            <asp:ListItem Text="Banned" Value="Banned"></asp:ListItem>
                            
                            </asp:DropDownList>
           
       </td>
       </tr>
   
       <tr class="medication_info_tr-odd">
        <td ><asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label></td>       
        <td ><asp:TextBox ID="txtComments" runat="server" TabIndex="9" TextMode="MultiLine" Rows="3" Width="250px"></asp:TextBox></td>        
        </tr>
        <tr class="medication_info_tr-odd">
       
            <td colspan="3" align="center" >
                <asp:ImageButton ID="btnSave" runat="server" border="0" 
                    ImageUrl="~/images/save.gif" onclick="btnSave_Click" 
                     />
            
           
           
            <asp:ImageButton ID="btnUserCancel" runat="server" 
                    ImageUrl="../images/cancelPopUp.gif" border="0" 
                     TabIndex="10" onclick="btnUserCancel_Click"></asp:ImageButton>
           
                 </td>
        </tr>
        
    </table>
    <asp:HiddenField ID="hidPatID" runat="server" />
    <asp:HiddenField ID="hidFacID" runat="server" />
     <asp:HiddenField ID="hidClinID" runat="server" />
     <asp:HiddenField ID="hidDrugID" runat="server" 
                onvaluechanged="hidDrugID_ValueChanged" />
     <asp:Button ID="btngetStock" runat="server" Text="getStock"  style="display:none;"/>
    </ContentTemplate>
  </asp:UpdatePanel>
 </asp:Content>

