<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Insurance.aspx.cs" Inherits="Patient_setINsuranceInfo" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updatePanelInsuranceInfo" runat="server" UpdateMode="Conditional"><ContentTemplate>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearchIns" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetInsuranceNames" BehaviorID="ModelExtender" runat="server" OnClientItemSelected="publishInsInfo">
    </cc1:AutoCompleteExtender>
   
    <table border="0" width="80%" align="center">
      
            <tr class="medication_info_th1">
              <td colspan="4" align="center" height="20px"> <asp:Label id="lblTitle" runat="server" Text="INSURANCE INFORMATION" 
                            Font-Bold="True" Font-Size="Small"></asp:Label>
	       </td>
             </tr>
              <tr align = "right" class="medication_info_tr-even">
                    <td align = "left"><asp:Label ID="lblSearch" runat="server" Text="Search Insurance" Width="150px" ></asp:Label></td>
                    <td align = "left" colspan = "3">
                     
                     <asp:TextBox ID="txtSearchIns" runat="server" Width="150px" TabIndex="15"></asp:TextBox>
                     <asp:ImageButton ID="btnSearchIns" runat="server" ImageUrl="~/images/search_new.png" border="0" Width="24px" Height="24px"
                     onclick="btnSearchIns_Click" TabIndex="16"> </asp:ImageButton>
                     </td>
              </tr>
              <tr class="medication_info_tr-odd">
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsName" runat="server" Text="Insurance Name"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsName" runat="server" Width="150px" TabIndex="1"></asp:TextBox>
                </td>
               
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsCity" runat="server" Text="City"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsCity" runat="server" Width="150px" TabIndex="6"></asp:TextBox>
                </td>
              </tr>
              <tr class="medication_info_tr-even">
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsNumber" runat="server" Text="Insurance Number"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsNumber" runat="server" Width="150px" TabIndex="2"></asp:TextBox>
                </td>
              
                <td width="150px" align = "left">    
                  <asp:Label ID="lblInsState" runat="server" Text="State"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsState" runat="server" Width="150px" TabIndex="7" 
                        MaxLength="2"></asp:TextBox>
                </td>
              </tr>
              <tr class="medication_info_tr-odd">
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsCompany" runat="server" Text="Insurance Company"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsCompany" runat="server" Width="150px" TabIndex="3"></asp:TextBox>
                </td>
                
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsZip" runat="server" Text="ZIP"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsZip" runat="server" Width="150px" 
                         TabIndex="8"></asp:TextBox>
                </td>
              </tr>
              <tr class="medication_info_tr-even">
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsAddress1" runat="server" Text="Address 1"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsAddress1" runat="server" Width="150px" TabIndex="4"></asp:TextBox>
                </td>
               
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsPhone" runat="server" Text="Phone"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsPhone" runat="server" Width="150px" 
                         TabIndex="9"></asp:TextBox>
                </td>
              </tr>
              <tr class="medication_info_tr-odd">
                <td width="150px" align = "left">
                  <asp:Label ID="lblInsAddress2" runat="server" Text="Address 2"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsAddress2" runat="server" Width="150px" TabIndex="5"></asp:TextBox>
                </td>
               <td width="150px" align = "left">
                  <asp:Label ID="lblInsFax" runat="server" Text="Fax"></asp:Label>
                </td>
                <td width="350px">
                  <asp:TextBox ID="txtInsFax" runat="server" Width="150px" 
                         TabIndex="10"></asp:TextBox>
                </td>
              </tr>
              <tr class="medication_info_tr-even"> 
              <td align="center" colspan="4">
                      <asp:ImageButton ID="btnInsSave" runat="server"  ImageUrl="../images/save.gif"
                              border="0"  Style="height: 23px" onclick="btnInsSave_Click" 
                          OnClientClick = "return validateInsuranceInfo();" TabIndex="11"  ></asp:ImageButton>
                 
              
               <asp:ImageButton ID="btnInsUpdate" runat="server" ImageUrl="~/images/update.png" 
                          border="0" Visible="false" 
                 Style="height: 23px" OnClientClick = "return validateInsuranceInfo();" 
                                          onclick="btnInsUpdate_Click" TabIndex="12" ></asp:ImageButton>
              
            <asp:ImageButton ID="btnInsCancel" runat="server"  
                    ImageUrl="../images/cancelPopUp.gif" border="0"  Style="height: 23px" 
                    onclick="btnInsCancel_Click" TabIndex="13"></asp:ImageButton>
           
                  <asp:ImageButton ID="btnInsDelete" runat="server"  ImageUrl="~/images/delete.png" border="0"
            Visible="false" onclick="btnInsDelete_Click"  Style="height: 23px" TabIndex="14" ></asp:ImageButton>
           </td></tr>   
         </tr>
                   <tr class="medication_info_tr-odd">
                   <td colspan="4"  align="right"> 
                    <asp:LinkButton ID="lnkbtnPop" runat="server" OnClientClick = "return openwindow();">Click here</asp:LinkButton> &nbsp; to view Insurance list
              
                   </td>
                   </tr>
            </table>
            
       
   </ContentTemplate></asp:UpdatePanel>
<script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("InsuranceList.aspx", "mywindow", "toolbar = no,width =600,scrollbars = yes");
    }
</script> 
</asp:Content>