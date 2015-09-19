<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Drug.aspx.cs" Inherits="Drug" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updatePanelDrugInfo" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
         <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearchDrug" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetDrugNames" BehaviorID="ModelExtender" runat="server" OnClientItemSelected="publishDrugInfo">
         </cc1:AutoCompleteExtender>
         <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" TargetControlID="txtSearchFormulary" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetFormularyNames" runat="server">
         </cc1:AutoCompleteExtender>--%>
    <table border="0" width="600px" align="center">   
         
                <tr class="medication_info_th1">
                    <td align = "center" colspan="4" height="20px">
                        <asp:Label id="lblTitle" runat="server"  Text="DRUG INFORMATION" Font-Bold="True" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
              <tr class="medication_info_tr-even">
                        <td align = "left">
                        <asp:Label ID="lblSearch" runat="server" Text="Search Drug" Width="150px" ></asp:Label>
                       </td>
                    <td class="medication_info_td-even" align="left">
                        <asp:TextBox ID="txtSearchDrug" runat="server" Width="150px" TabIndex="8"></asp:TextBox>
                        <asp:ImageButton ID="btnSearchDrug" runat="server" border="0" Style="height: 24px" 
                                ImageUrl="~/images/search_new.png" onclick="btnSearchDrug_Click" TabIndex="9" Width="24px"/>
                        </td>
               </tr>
               <tr class="medication_info_tr-odd">
                    <td width = "100px" align = "left">
                        <asp:Label ID="lblDrugName" runat="server" Text="Drug Name"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                        <asp:TextBox ID="txtDrugName" runat="server" Width="150px" 
                            Style="margin-left: 0px" TabIndex="1"></asp:TextBox>
                    </td>                    
                </tr>
               <tr class="medication_info_tr-even">
                <td width = "100px" align = "left">
                        <asp:Label ID="lblDrugType_ID" runat="server" Text="DrugType"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                    <asp:DropDownList ID="ddlDrugType_ID" runat="server" Width = "155px" AutoPostBack = "true" 
                            TabIndex="2"></asp:DropDownList> 
                    </td>
                    
                </tr>
                <tr class="medication_info_tr-odd">
                     <td width = "100px" align = "left">                     
                       <asp:Label ID="lblDrugCostIndex" runat="server" Text="Cost Index"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                      <asp:TextBox ID="txtDrugCostIndex" runat="server" Width="150px" TabIndex="3" 
                            MaxLength="1"></asp:TextBox>
                    </td>
                </tr>

               <tr class="medication_info_tr-even" align = "Center">
                    <td colspan="2">
                  
                     <asp:ImageButton ID="btnDInfoSave" runat="server" ImageUrl="../images/save.gif" border="0"
                                    Style="height: 24px" OnClientClick = "return validateDrugInfo();" 
                                                onclick="btnDInfoSave_Click" TabIndex="4" 
                            Height="24px"></asp:ImageButton>
              
                        <asp:ImageButton ID="btnDInfoUpdate" runat="server" ImageUrl="~/images/update.png" 
                      border="0" Visible="false" Style="height: 24px" OnClientClick = "return validateDrugInfo();" 
                      TabIndex="5" onclick="btnDInfoUpdate_Click" ></asp:ImageButton>
                  
                    <asp:ImageButton ID="btnInfoCancel" runat="server" ImageUrl="../images/cancelPopUp.gif"
                                    border="0" onclick="btnInfoCancel_Click" TabIndex="6"></asp:ImageButton>                            
                 
                   <asp:ImageButton ID="btnDInfoDelete" runat="server" ImageUrl="~/images/delete.png" border="0"
                    Visible="false" Style="height: 24px" TabIndex="7" onclick="btnDInfoDelete_Click" OnClientClick = "return validateDrugInfo();"></asp:ImageButton>
                 </td>
                </tr> 
        <tr class="medication_info_tr-odd">
        <td colspan="2">
         <asp:CheckBox ID="chk_DType" runat="server" Text = "Check this to add Drug Type" 
                align = "left" oncheckedchanged="chk_DType_CheckedChanged" AutoPostBack = "true" />
        </td>
        </tr>
        <tr class="medication_info_tr-even">
        <td colspan="2" align="right">
         <asp:LinkButton ID="lnkbtnPop" runat="server" OnClientClick = "return openwindow();">Click here </asp:LinkButton> to view Drug List
        </td>
        </tr>
   </table> 
        
   
   <asp:Panel ID="PnlDType" runat="server" Visible = "false">
        
        <table border="0" width="600px" align="center">  
               <tr class="medication_info_th1">
                    <td  colspan="2">
                        <asp:Label id="lblTitle2" runat="server" Text="DRUG TYPE"  Font-Bold="True" Font-Size="Small"></asp:Label>
                    </td>
               </tr>
               <tr class="medication_info_tr-even">
                    <td  align = "left" width = "100px">
                        <asp:Label ID="lblDTInfo" runat="server" Text="Drug Type" Width = "75px" Height="16px"></asp:Label>
                    </td>
                    <td class="medication_info_td-even" align="left" width = "250px">
                        <asp:TextBox ID="txtDTInfo" width ="150px" runat="server" TabIndex="19"></asp:TextBox>                                                     
                        <asp:ImageButton ID="btnDTInfoSave" runat="server"  ImageUrl="../images/save.gif"
                        border="0"  OnClientClick = "return validateDrugType();" onclick="btnDTInfoSave_Click" TabIndex="9" ></asp:ImageButton>
              
                   </td>
                </tr>
           </table>  
      </asp:Panel>
     </ContentTemplate>
   

 </asp:UpdatePanel>
<script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("DrugList.aspx", "mywindow", "toolbar = no,width =600,height=600,scrollbars = yes");
    }
</script> 
</asp:Content>