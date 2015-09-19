<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Clinic.aspx.cs" Inherits="Clinics" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    <center>
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
         <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtClinicName" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetClinic" BehaviorID="ModelExtender"  runat="server">
        </cc1:AutoCompleteExtender>
        <div style="vertical-align:middle">
         <table>   
                <tr>
                    <td colspan="2" align="center" class="medication_info_th1" height="20px">
                          CLINIC ORGANIZATION
                    </td>
                </tr>       
                <tr class="medication_info_tr-odd">
                    <td class="medication_info_td-odd">
                        <asp:Label ID="lblClinc" runat="server" Text="Clinic Name"></asp:Label>
                    </td>
                    <td class="medication_info_td-odd">
                        <asp:TextBox ID="txtClinicName" runat="server" TabIndex="1" Width="175px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="medication_info_tr-even">
                    <td colspan="2" class="medication_info_td-even">
                        <asp:ImageButton ID="btnClinicSave" runat="server" Style="height: 23px" 
                            ImageUrl="~/images/save.gif" border="0" onclick="btnClinicSave_Click" 
                            OnClientClick="return validateClinic();" TabIndex="2"></asp:ImageButton>
                        <asp:ImageButton  ID="btnClinicDelete" runat="server" Style="height: 23px"
                            ImageUrl="../images/delete.png" border="0" onclick="btnClinicDelete_Click" 
                            OnClientClick="return validateClinic();" TabIndex="3"></asp:ImageButton>
                    </td>            
                </tr>
         </table>
     </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </center>
</asp:Content>

