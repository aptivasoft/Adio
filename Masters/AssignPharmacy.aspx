<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="AssignPharmacy.aspx.cs" Inherits="Masters_AssignPharmacy" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>eCarex Health Care System</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
    <center>
    <table width="800">
    <tr class="medication_info_th1">
       <td width="100%" colspan="2" align="center" height="25px">ASSIGN PHARMACY</td>       
    </tr>
    <tr class="medication_info_tr-odd">
    <td width="100%" colspan="2" align="left">
    <asp:RadioButton ID="rbtnPhrmTech" runat="server"  Text="Pharmacy Tech" AutoPostBack="true" 
            GroupName="rbPhrm" oncheckedchanged="rbtnPhrmTech_CheckedChanged" Checked="true" Font-Bold="true" />
    <asp:RadioButton ID="rbtnFacility" runat="server"  Text="Facility" AutoPostBack="true" 
            GroupName="rbPhrm" oncheckedchanged="rbtnFacility_CheckedChanged" Font-Bold="true" />
    </td>
    </tr>
    
    <tr class="medication_info_tr-even">
                        <td width="20%" align="left" >
                            <asp:Label ID="lblPhrmNames1" runat="server" Text="Select Pharmacy:"></asp:Label>
                        </td>
                        <td width="80%" align="left" >
                            <asp:DropDownList ID="ddlPharmacyNames" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlPharmacyNames_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        
                        </tr>
    <tr >
    <td colspan="2">
    <asp:Panel ID="pnlPhrmTech" runat="server">
   
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblPhrmTechName" runat="server" Text="Select Pharmacy Tech.:"></asp:Label>
                        </td>
                        <td align="left">
                        <asp:ListBox ID="lstPhrmTechNames" runat="server" SelectionMode="Multiple" Height="200px">
                        </asp:ListBox>
                        </td>
                     
                        </tr>
                    
                       
      
   
    </asp:Panel>
    <asp:Panel ID="pnlPhrmFacility" runat="server" Visible="false">
 
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblClinicNames" runat="server" Text="Select Clinic:"></asp:Label>
                        </td>
                        <td align="left">
                        <asp:DropDownList ID="ddlClinicNames" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlClinicNames_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        
                        </tr>
        <tr class="medication_info_tr-even">
                        <td align="left">
                            <asp:Label ID="lblFacilityName" runat="server" Text="Select Facilities:"></asp:Label>
                        </td>
                        <td align="left" >
                        <asp:ListBox ID="lstFacilityNames" runat="server" SelectionMode="Multiple" Height="200px">
                        </asp:ListBox>
                        </td>
                     
                        </tr>
    </asp:Panel>
    </td>
    </tr>
      <tr class="medication_info_th1">
            <td colspan="2" align="center">
                <asp:ImageButton ID="btnAssignPharmacy" runat="server" border="0" 
                    ImageUrl="~/images/save.gif" Style="height: 24px" onclick="btnAssignPharmacy_Click" 
                     />
                 <asp:ImageButton ID="btnCancelAssignment" runat="server" 
                    ImageUrl="../images/cancelPopUp.gif" border="0"   Style="height: 24px" onclick="btnCancelAssignment_Click" 
                      ></asp:ImageButton>
            </td>
        </tr>
    </table>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

