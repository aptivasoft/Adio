<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <title>eCarex Health Care System</title>    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
      <cc1:AutoCompleteExtender ID="ACEUserID"  TargetControlID="txtSearchUser" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetUserNames"  >
         </cc1:AutoCompleteExtender>
    <center>
    <table width="600px" >
    <tr class="medication_info_th1">
       <td colspan="4" align="center" height="20px">REGISTER USER</td>       
    </tr>
        <tr class="medication_info_tr-odd">
                    <td align = "left" width="40%">
                    <asp:Label ID="lblSearch" runat="server" Text="Search User" Width="150px" ></asp:Label>
                    
               </td><td align="left">
                    <asp:TextBox ID="txtSearchUser" runat="server" Width="200px" TabIndex="1"></asp:TextBox>
                   <asp:ImageButton ID="btnSearchUser" runat="server" ImageUrl="~/images/search_new.png" border="0"  
                          Style="height: 24px" TabIndex="2"  onclick="btnSearchUser_Click"></asp:ImageButton>                      
                </td>
        </tr>   
        <tr class="medication_info_tr-even">
       
                        <td align="left">
                            <asp:Label ID="lblURole" runat="server" Text="User Role"></asp:Label>
                        </td>
                        <td align="left">   
                         <asp:UpdatePanel ID="updatePanel1" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate>
    <cc1:AutoCompleteExtender ID="ACE_DoctorName"  TargetControlID="txtDocName" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetDoctorNames" OnClientItemSelected="AutoCompleteSelectedUser" >
         </cc1:AutoCompleteExtender>
         <cc1:AutoCompleteExtender ID="ACE_EmployeeName"  TargetControlID="txtEmployeeName" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetEmpNames" OnClientItemSelected="AutoCompleteSelectedUser" >
         </cc1:AutoCompleteExtender>
                            <asp:DropDownList ID="ddlUserRole" runat="server" OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged" 
                                 Width="150px" AutoPostBack="true" TabIndex="3">
                            </asp:DropDownList>&nbsp;&nbsp;<asp:TextBox ID="txtEmployeeName" 
            runat="server" Visible="true" TabIndex="4"></asp:TextBox><asp:TextBox 
            ID="txtDocName" runat="server"  Visible="false" TabIndex="5"></asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                           
                        </td>
                    </tr>
        <tr class="medication_info_tr-odd">
                        <td align="left">
                            <asp:Label ID="lblUserID" runat="server" Text="User ID "></asp:Label>
                        </td>
                        <td align="left">
                                        
                            <asp:TextBox ID="txtUserID" runat="server" TabIndex="6" Width="200px"></asp:TextBox>
                            <%--<span id="spanAvailability"></span>--%>&nbsp;&nbsp;<asp:Label ID = "lblAvailability" runat = "server" Text = "" style="visibility:hidden" ></asp:Label></td>
                            
                        
                        </tr>
         
        <tr class="medication_info_tr-even">
       
                            <td align="left">
                                <asp:Label ID="lblPassword" runat="server" Text="Password "></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassword" runat="server" TabIndex="7" Width="200px" TextMode="Password" ></asp:TextBox>
                                <asp:Label ID="lblPwdLength" runat="server" Text="(Min. 7 Characters)"></asp:Label>
                            </td>
                            </tr>
        <tr class="medication_info_tr-odd">
                        <td align="left">
                         <asp:CheckBox ID="chkStamps" runat="server" Text="Generate Stamps" AutoPostBack="true" oncheckedchanged="chkStamps_CheckedChanged" />
                       </td>
                       <td align="left">
                         <asp:Label ID="lblStampLocID" runat="server" Text="Location"></asp:Label>
                         <asp:DropDownList ID="ddlStampLocation" runat="server" AutoPostBack="true" Visible="false"></asp:DropDownList>
                        </td>
         </tr>              

        <tr class="medication_info_tr-even">
     
                                <td align="left">
                                    <asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtComments" runat="server" TabIndex="8" Width="200px" 
                                        TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
        <tr class="medication_info_tr-odd">
       
            <td colspan="3" align="center">
                <asp:ImageButton ID="btnRegister" runat="server" border="0" 
                    ImageUrl="~/images/save.gif" onclick="btnRegister_Click" 
                    OnClientClick = "return validateAddUser();" TabIndex="9" 
                    Style="height: 24px"/>
            
            <asp:ImageButton ID="btnUserUpdate" runat="server" ImageUrl="~/images/update.png" 
                    border="0" Visible="false" 
                Style="height: 24px" OnClientClick = "return validateAddUser();" 
                    TabIndex="10" onclick="btnUserUpdate_Click" ></asp:ImageButton>
           
            <asp:ImageButton ID="btnUserCancel" runat="server" 
                    ImageUrl="../images/cancelPopUp.gif" border="0" 
                     TabIndex="11" onclick="btnUserCancel_Click" Style="height: 24px"></asp:ImageButton>
           
            <asp:ImageButton ID="btnUserDelete" runat="server" ImageUrl="~/images/delete.png" border="0"
                    Visible="false" Style="height: 24px" TabIndex="12" 
                    onclick="btnUserDelete_Click" OnClientClick="return validateAddUser();"></asp:ImageButton>
             </td>
        </tr>
        <tr>
        <td colspan="2"><asp:HiddenField ID="empId" runat="server" /></td>
        </tr>
       
    </table>
    </ContentTemplate>
  </asp:UpdatePanel>
 </asp:Content>

