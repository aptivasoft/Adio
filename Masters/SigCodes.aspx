<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="SigCodes.aspx.cs" Inherits="Patient_SigCodes" Title="eCarex Health Care System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<asp:UpdatePanel ID="updatePanelProviderInfo" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearchSIG" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetSIGNames" BehaviorID="ModelExtender" CompletionListCssClass="AutoExtender1"  runat="server">
        </cc1:AutoCompleteExtender>
     
<table border="0" width="60%" align = "Center">      
                  <tr class="medication_info_th1">			 
                    <td colspan="4" align="center" width="100%" height="20px"">
                        
                        <asp:Label id="lblTitle" runat="server" Text="SIG CODES" Font-Bold="True" Font-Size="Small"></asp:Label>
	
                    </td>
                  </tr> 
                  
                  <tr class="medication_info_tr-even">
                  <td width="150px" align="left">
                          <asp:Label ID="lblSearch" runat="server" Text="Search SIG Code  " ></asp:Label>
                     </td>
                     <td width="350px" colspan="3">
                          <asp:TextBox ID="txtSearchSIG" runat="server" Width="175px" TabIndex="1"></asp:TextBox>
                          <asp:ImageButton ID="btnSearchSIG" runat="server" 
                              ImageUrl="~/images/search_new.png" border="0" onclick="btnSearchSIG_Click" 
                              Style="height: 24px" TabIndex="2"></asp:ImageButton></asp:ImageButton> 
                     </td>
                  </tr>
                  <tr class="medication_info_tr-odd">
                     <td width="150px" align="left">
                           <asp:Label ID="lblSIGCode" runat="server" Text="SIG Code  "></asp:Label></td>
                     </td>
                     <td width="350px" colspan="3">
                          <asp:TextBox ID="txtSIGCode" runat="server" TabIndex="3" Width="175px"></asp:TextBox>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-even">
                    <td width="150px" align="left">
                           <asp:Label ID="lblSIGName" runat="server" Text="SIG Name  "></asp:Label>
                    </td>
                    <td width="350px" colspan="3">
                           <asp:TextBox ID="txtSIGName" TextMode="MultiLine" Width="175px" runat="server" 
                               TabIndex="4"></asp:TextBox>
                    </td>
                    
                  </tr>
                  <tr class="medication_info_tr-odd">
                    <td width="150px" align="left">
                           <asp:Label ID="lblFactor" runat="server" Text="Factor  "></asp:Label>
                    </td>
                    <td width="350px" colspan="3">
                            <asp:TextBox ID="txtFactor"  Width="175px" runat="server" TabIndex="5"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="filterFactor" FilterMode="ValidChars" ValidChars="0123456789." TargetControlID="txtFactor" runat="server"></cc1:FilteredTextBoxExtender>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-even" align = "Center"> 
                     <td colspan="4" class="medication_info_td-odd"> 
                        <asp:ImageButton ID="btnSIGSave" runat="server"  ImageUrl="~/images/save.gif" border="0" onclick="btnSIGSave_Click" OnClientClick = "return validateSigCodes();" TabIndex="6" ></asp:ImageButton>
                        <asp:ImageButton ID="btnSIGUpdate" runat="server" 
                             ImageUrl="../images/update.png" border="0" Visible="false" Style="height: 24px" 
                             OnClientClick = "return validateSigCodes();" onclick="btnSIGUpdate_Click" 
                             TabIndex="7"  ></asp:ImageButton>
                        <asp:ImageButton ID="btnSIGCancel" runat="server"  
                             ImageUrl="~/images/cancelPopUp.gif" border="0" onclick="btnSIGCancel_Click" 
                             TabIndex="8"></asp:ImageButton>
                        <asp:ImageButton ID="btnSIGDelete" runat="server" 
                             ImageUrl="../images/delete.png" border="0" Visible="false" 
                             onclick="btnSIGDelete_Click" TabIndex="9" ></asp:ImageButton>
                     </td>
                  </tr>
                  <tr class="medication_info_tr-odd">
                    <td colspan="4" align="right">
                     <asp:LinkButton ID="lnkbtnPop" runat="server" OnClientClick = "return openwindow();">Click here </asp:LinkButton>
                        to view Sig Codes
                    </td>
                    </tr>
                  <tr class="medication_info_tr-even"> 
                     <td colspan="4" class="medication_info_td-even"> 
                       <asp:Label ID="lblErrorMsg" runat="server" Visible="false" ForeColor="Red" Font-Size="12px"></asp:Label>
                     </td>
                  </tr>
            </table>
   </ContentTemplate>
</asp:UpdatePanel>
<script type = "text/javascript" language = "javascript">
    function openwindow() 
    {
        window.open("SigCodesList.aspx", "mywindow", "toolbar = no,width =600,height=600,scrollbars = yes");
    }
</script> 
</asp:Content>
