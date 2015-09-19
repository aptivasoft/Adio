<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="AssignReports.aspx.cs" Inherits="Masters_AssignReports" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>eCarex Health Care System</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
    <center>
    <table width="50%" >
    <tr class="medication_info_th1">
       <td colspan="3" align="center" height="20px">ASSIGN REPORTS TO USERS</td>       
    </tr>
         
        
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblReportName" runat="server" Text="Select Report:"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                        <asp:DropDownList ID="ddlReportNames" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlReportNames_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                        
                        </tr>
                        <tr class="medication_info_tr-even">
                        <td align="left">
                            <asp:Label ID="lblUserRole" runat="server" Text="Select User Role:"></asp:Label>
                        </td>
                        <td align="left" >
                         <asp:DropDownList ID="ddlUserRoles" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlUserRoles_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                           <td>
                        <asp:CheckBox ID="chkAllUsers" runat="server" Text="Select All Users" 
                                   oncheckedchanged="chkAllUsers_CheckedChanged" AutoPostBack="true"/>
                        </td>
                        </tr>
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblUserName" runat="server" Text="Select Users:"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                        <asp:ListBox ID="lstUserNames" runat="server" SelectionMode="Multiple" Height="125px">
                        </asp:ListBox>
                        </td>
                     
                        </tr>
                    
                       
        <tr class="medication_info_tr-even">
            <td colspan="3" align="center">
                <asp:ImageButton ID="btnAssignReport" runat="server" border="0" 
                    ImageUrl="~/images/save.gif" Style="height: 24px" 
                    onclick="btnAssignReport_Click" />
                 <asp:ImageButton ID="btnCancelAssignment" runat="server" 
                    ImageUrl="../images/cancelPopUp.gif" border="0"   Style="height: 24px" 
                    onclick="btnCancelAssignment_Click"  ></asp:ImageButton>
            </td>
        </tr>
 
    </table>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

