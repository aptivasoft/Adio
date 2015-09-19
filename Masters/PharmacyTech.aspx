<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="PharmacyTech.aspx.cs" Inherits="Masters_PharmacyTech" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 eCarex Health Care System</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
    <center>
    <table width="50%" >
    <tr class="medication_info_th1">
       <td colspan="3" align="center" height="20px">ASSIGN PHARMACIES TO PHARMACY TECH.</td>       
    </tr>
         
        
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblPhrmNames" runat="server" Text="Select Pharmacy:"></asp:Label>
                        </td>
                        <td align="left">
                        <asp:DropDownList ID="ddlPharmacyNames" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlPharmacyNames_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        
                        </tr>
        
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblPhrmTechName" runat="server" Text="Select Pharmacy Tech.:"></asp:Label>
                        </td>
                        <td align="left" >
                        <asp:ListBox ID="lstPhrmTechNames" runat="server" SelectionMode="Multiple" Height="125px">
                        </asp:ListBox>
                        </td>
                     
                        </tr>
                    
                       
        <tr class="medication_info_tr-even">
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

