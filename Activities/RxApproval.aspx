<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxApproval.aspx.cs" Inherits="RxApproval" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="EXPIRES" content="0" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" BehaviorID="acCustodianEx1" 
                TargetControlID="txtDocName" UseContextKey="true" runat="server"  
                MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetDoctorNames" 
                OnClientItemSelected="AutoCompleteSelected_Doctor" >
           </cc1:AutoCompleteExtender>
     <cc1:AutoCompleteExtender   ID="AutoCompleteExtender1" 
         BehaviorID="acCustodianEx2" TargetControlID ="txtDrug" 
         UseContextKey="True" runat="server"  MinimumPrefixLength="1"  Enabled="True"  
         ServiceMethod="GetMedicationNames" DelimiterCharacters="" ServicePath=""></cc1:AutoCompleteExtender>

    <table width="100%">
    <tr class="medication_info_th1">
       <td colspan="4" align="center" height="25px">Rx Approval</td>
       </tr>
      
        <tr class="medication_info_tr-odd">
         <td align = "left" >
                    <asp:Label ID="Label2" runat="server" Text="Patient Name : " ></asp:Label>
               </td>
               <td align = "left" >
                    <asp:Label ID="lblpatientName" runat="server"></asp:Label>
               </td>
        </tr>  
        <tr class="medication_info_tr-even">
            <td align = "left">
            <asp:Label ID="Label1" runat="server" Text="Patient Contact : " ></asp:Label>
            </td>
            <td align = "left">
            <asp:Label ID="lblPatContact" runat="server" ></asp:Label>
            </td>
        </tr> 
        <tr class="medication_info_tr-odd">
       
            <td align="left" >
                <asp:Label ID="lblDoctor" runat="server" Text="Doctor Name"></asp:Label>
            </td>
            <td  align="left" align="left">   
                
               <asp:TextBox ID="txtDocName" runat="server" Text="" Width="250px"></asp:TextBox>
            </td>
          </tr> 
        <tr class="medication_info_tr-even">
       
            <td align="left" >
                <asp:Label ID="lblDrug" runat="server" Text="Drug Name"></asp:Label>
            </td>
            <td  align="left" align="left">   
                
               <asp:TextBox ID="txtDrug" runat="server" Text="" Width="250px"></asp:TextBox>
            </td>
          </tr>
        <tr class="medication_info_tr-odd">
                        <td align="left" >
                            <asp:Label ID="lblQty" runat="server" Text="Qty"></asp:Label>
                        </td>
                        <td align="left" >
                           <asp:TextBox ID="txtQuantity" runat="server" TabIndex="1" Width="50px"></asp:TextBox>
                        </td>
                        </tr>
        <tr class="medication_info_tr-even">
          <td align="left" >
                                <asp:Label ID="lblsig" runat="server" Text="SIG"></asp:Label>
                            </td>
                            <td  align="left">
                                <asp:TextBox ID="txtSIG" runat="server" TabIndex="2" Width="250px" 
                                   ></asp:TextBox>
                            </td>
                            </tr>
     <tr class="medication_info_tr-odd">
           <td ><asp:Label ID="lblPharmacy" runat="server" Text="Pharmacy"></asp:Label></td>       
           <td colspan="2">
               <asp:RadioButtonList ID="rbtnPhrm" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true"
                   onselectedindexchanged="rbtnPhrm_SelectedIndexChanged">
               <asp:ListItem Text="ADiO" Value="0"></asp:ListItem>
               <asp:ListItem Text="Other" Value="1" ></asp:ListItem>
               </asp:RadioButtonList> 
             
               <asp:TextBox ID="txtPharmacy" runat="server" Width="250px" Visible="false"></asp:TextBox>
            </td>       
       </tr>
        <tr class="medication_info_tr-even">
     
                                <td align="left" >
                                    <asp:Label ID="lblRefills" runat="server" Text="Refills"></asp:Label>
                                </td>
                                <td  align="left">
                                    <asp:DropDownList ID="ddlRefills" runat="server">
                                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                            </tr>
       
             <tr class="medication_info_tr-odd">
       <td ><asp:Label ID="lblRxType" runat="server" Text="Type"></asp:Label></td>       
       <td >
       <asp:RadioButtonList ID="rbtnRequestType" runat="server" RepeatColumns="4">
           </asp:RadioButtonList>
           
       </td>
       </tr>
       <tr class="medication_info_tr-even">
       <td ><asp:Label ID="lblDecision" runat="server" Text="Decision"></asp:Label></td>       
       <td >
       <asp:RadioButtonList ID="rbtnDecision" runat="server" RepeatColumns="4">
       <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
       <asp:ListItem Text="Denied" Value="R"></asp:ListItem>
       <asp:ListItem Text="Pending" Value="N"></asp:ListItem>
           </asp:RadioButtonList>
           
       </td>
       </tr>
        <tr class="medication_info_tr-odd">
        <td>
      <asp:Label ID="lblRxDoc" runat="server" Text="Attach Document :"></asp:Label>
      </td>
      <td >
      <asp:UpdatePanel ID="updateRxDoc" UpdateMode="Conditional" runat="server" >
      <ContentTemplate>
      <asp:FileUpload ID="FileUpRxDoc" runat="server"/> <b>(For Approved Rx only)</b>
          </ContentTemplate>
      <Triggers>
      <asp:PostBackTrigger ControlID="btnSave"/>
      </Triggers>
      </asp:UpdatePanel> 
      </td>
      </tr>

       <tr class="medication_info_tr-even">
        <td ><asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label></td>       
        <td ><asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" Width="250px"></asp:TextBox></td>        
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
                  <tr>
       <td colspan="2" align="center">&nbsp;&nbsp;<asp:Label ID="lblMsg" Text="" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label></td>
       </tr>
        </tr>
        
    </table>
     <asp:HiddenField ID="hidDocID" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
 </asp:Content>

