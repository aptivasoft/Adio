<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Patient_setEmployeeInfo" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updatePanelInsuranceInfo" runat="server" UpdateMode="Conditional"><ContentTemplate>
    
    <cc1:AutoCompleteExtender ID="AutoCompleteEmp" BehaviorID="acCustodianEx" TargetControlID ="txtEmpFullName" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true"  ServiceMethod="GetEmpNames"  CompletionListCssClass="AutoExtender2" OnClientItemSelected="publishEmpInfo" ></cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" BehaviorID="acCustodianEx1" TargetControlID ="txtLoc" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true"  ServiceMethod="GetLocationNames"  CompletionListCssClass="AutoExtender2" ></cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" BehaviorID="acCustodianEx2" TargetControlID ="txtTitle" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true"  ServiceMethod="GetTitleNames"  CompletionListCssClass="AutoExtender2" ></cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" BehaviorID="acCustodianEx3" TargetControlID ="txtSup1" UseContextKey="true" runat="server" MinimumPrefixLength="1" Enabled="true" ServiceMethod="GetSup1Names"   CompletionListCssClass="AutoExtender2" ></cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" BehaviorID="acCustodianEx4" TargetControlID ="txtSup2" UseContextKey="true" runat="server" MinimumPrefixLength="1" Enabled="true" ServiceMethod="GetSup2Names"   CompletionListCssClass="AutoExtender2" ></cc1:AutoCompleteExtender>
<table border="0" width="80%" align="center">
  
        <tr class="medication_info_th1">
           <td align="center"  colspan="4" height="20px"><b>EMPLOYEE PROFILE</b></td>
        </tr>
        <tr class="medication_info_tr-even">
                <td  align="left">
                    <asp:Label ID="lblEmpSearch" runat="server" Text="Search Employee"></asp:Label> 
                </td>
                <td colspan="3">
                <asp:TextBox ID="txtEmpFullName" runat="server" width="280px"
                        onChange="$find('AutoCompleteEmp').set_contextKey($get('<%= txtEmpFullName.ClientID %>').value);" 
                        TabIndex="32" ></asp:TextBox> 
                 
                     <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/images/search_new.png" ToolTip="Search" onclick="btnSearch_Click" TabIndex="33" Width="24px" Height="24px" />
                </td>                
        </tr>
            <tr class="medication_info_tr-odd">
                <td>
                <asp:Label ID="lblempName" runat ="server" Text="Employee Name(F,M,L)"></asp:Label>&nbsp;<font color="red">*</font>
                </td>
                <td>
                  <asp:TextBox ID="txtempFName" runat="server" Width="120px" TabIndex="1"></asp:TextBox>
                  <asp:TextBox ID="txtempMName" runat="server" MaxLength="1" Width="20px" 
                        TabIndex="2" ></asp:TextBox>
                  <asp:TextBox ID="txtempLName" runat="server" Width="120px" TabIndex="3"></asp:TextBox>
                </td>                 
                <td>
                <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txtPhone" runat="server" TabIndex="14" Width="175px"></asp:TextBox></td>
            </tr>         
            
            <tr class="medication_info_tr-even">
                
                <td><asp:Label ID="lblemail" runat="server" Text="Email"></asp:Label></td>
                <td><asp:TextBox ID="txtemail" runat="server" TabIndex="4" Width="280px"></asp:TextBox></td>
                <td><asp:Label ID="lblHomePhone" runat="server" Text="Home Phone"></asp:Label></td>
                <td><asp:TextBox ID="txtHomePhone" runat="server" TabIndex="15" Width="175px"></asp:TextBox></td>
            </tr>            
            
            <tr class="medication_info_tr-odd">
               <td><asp:Label ID="lblAddress1" runat="server" Text="First Address"></asp:Label></td>
               <td><asp:TextBox ID="txtAddress1" runat="server" Width="280px" TabIndex="5"></asp:TextBox></td>
                 <td><asp:Label ID="lblSup1" runat="server" Text="Supervisor 1"></asp:Label></td>
               <td><asp:TextBox ID="txtSup1" runat="server" Width="175px" onChange="$find('AutoCompleteExtender3').set_contextKey($get('<%= txtSup1.ClientID %>').value);" TabIndex="16"></asp:TextBox></td>
            </tr>     
            <tr class="medication_info_tr-even">
               <td>
               <asp:Label ID="lblAddress2" runat="server" Text="Second Address"></asp:Label></td>
               <td>
               <asp:TextBox ID="txtAddress2" runat="server" Width="280px" TabIndex="6"></asp:TextBox></td>
               <td><asp:Label ID="lblSup2" runat="server" Text="Supervisor 2"></asp:Label></td>
               <td><asp:TextBox ID="txtSup2" runat="server" Width="175px" AutoPostBack = "true" 
                       TabIndex="17" ></asp:TextBox></td>
            </tr>
            <tr class="medication_info_tr-odd">
                <td><asp:Label ID="lblCSZ" runat="server" Text="City, State & Zip"></asp:Label></td>
                <td><asp:TextBox ID="txtCity" runat="server" TabIndex="7" Width="120px"></asp:TextBox>
                    <asp:TextBox ID="txtState" runat="server" Width="20px" MaxLength="2" 
                        TabIndex="8"></asp:TextBox>
                    <asp:TextBox ID="txtZip" runat="server" Width="120px" TabIndex="9"></asp:TextBox></td>
                   
             <td>
            <asp:Label ID="lblTitle" runat="server" Text="Search Title"></asp:Label>&nbsp;<font color="red">*</font>
            </td>
            <td>
            <asp:TextBox ID="txtTitle" runat="server" TabIndex="18" Width="175px"></asp:TextBox>
            
            </td>
            </tr> 
            <tr class="medication_info_tr-even">
            <td>
            <asp:Label ID="lblLocation" runat="server" Text="Search Location"></asp:Label>&nbsp;<font color="red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtLoc" runat="server" TabIndex="10" Width="280px"></asp:TextBox>
            
            </td>
             <td colspan = "2"> 
                 <asp:CheckBox ID="chkTitle" runat="server" Text = "Check this to add New Title" 
                     oncheckedchanged="chkTitle_CheckedChanged" AutoPostBack = "true" 
                     TabIndex="19" />
            </td>
            </tr>  
            
            
                                  
            <tr class="medication_info_tr-odd">
            <td>
            <asp:Label ID="lblApprov" runat="server" Text="Approval?"></asp:Label>&nbsp;<font color="red">*</font>
            </td>
            <td>
           <asp:DropDownList ID="ddlApprov" runat="server" 
                                 Width="130px" TabIndex="11">
               <asp:ListItem Value="Y">Yes</asp:ListItem>
               <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList>
            </td>
             <td>
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>&nbsp;<font color="red">*</font>
            </td>
            <td>
           <asp:DropDownList ID="ddlStatus" runat="server" 
                                 Width="130px" TabIndex="20">
               <asp:ListItem Value="Y">Active</asp:ListItem>
               <asp:ListItem Value="N">In-Active</asp:ListItem>
                            </asp:DropDownList>
            </td>
            </tr>  
           <tr class="medication_info_tr-even">
                <td><asp:Label ID="lblHireDate" runat="server" Text="Hire Date"></asp:Label>&nbsp;<font color="red">*</font></td>
                <td><asp:TextBox ID="txtHireDate" runat="server" TabIndex="12" Width="125px"></asp:TextBox>
                <cc1:CalendarExtender ID="Getdate" runat="server" TargetControlID="txtHireDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtHireDate" 
                        PopupPosition="BottomLeft" ></cc1:CalendarExtender>
                
                
                </td>
                <td><asp:Label ID="lblTDate" runat="server" Text="Terminated Date"></asp:Label>
                   </td>
                <td><asp:TextBox ID="txtTDate" runat="server" TabIndex="21" Width="125px"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtTDate" 
                        PopupPosition="BottomLeft" ></cc1:CalendarExtender>
                
                
                </td>
            </tr>
            
            <tr class="medication_info_tr-odd">
            <td><asp:Label ID="lblcmts" runat="server" Text="Comments"></asp:Label></td>
            <td><asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" TabIndex="13" Width="280px"></asp:TextBox>                          
            </td>
            <td>
            <asp:Label ID="lblEmpRole" runat="server" Text="Employee Role"></asp:Label>&nbsp;<font color="red">*</font>
            </td>
            <td>   
            <asp:DropDownList ID="ddlempRole" runat="server" Width="130px" TabIndex="22"></asp:DropDownList>
            </td>
            
            </tr>  
            
            <tr class="medication_info_tr-even">
            
            <td colspan="4" align="center">
           <asp:HiddenField ID="hdnEmpID" runat="server" />
           <asp:ImageButton ID="btnSave" runat="server" Height="24" 
                    ImageUrl="~/images/save.gif" onclick="btnSave_Click" 
                    OnClientClick="return validateEmployee();" TabIndex="23"/>
           <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/images/update.png" 
                    Height="24px"  Visible="False" onclick="btnUpdate_Click" 
                    OnClientClick="return validateEmployee();" TabIndex="24"/>
           <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/images/cancel.gif" 
                    Height="24" onclick="btnCancel_Click" TabIndex="25" />  
           <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" 
                    Height="24" onclick="btnDelete_Click" Visible="False" TabIndex="26" />  
                                 
            </td>
            </tr>
            
            <tr class="medication_info_tr-odd">
            <td colspan="4"> &nbsp;<font color="red">Note : * indicates fields are mandatory</font>
            </td>
            </tr>
            <tr class="medication_info_tr-even">
                   <td colspan="4" align="right"> 
                    <asp:LinkButton ID="lnkbtnPop" runat="server" 
                           onclick="lnkbtnPop_Click">Click here</asp:LinkButton> 
                       &nbsp; to view Employee list
              
                   </td>
             </tr>
            </table>
   
    <asp:Panel ID="PnlTitle" runat="server" Visible = "false">
        
        <table border="0" width="80%" align="center">  
               <tr class="medication_info_th1">
                    <td  colspan="2">
                        <asp:Label id="lblTitle2" runat="server" Text="Title" 
                            Font-Bold="True" Font-Size="Medium"></asp:Label>
                    </td>
               </tr>
               <tr class="medication_info_tr-even" align = "center">
                    <td align = "right">
                        <asp:Label ID="lblNewTitle" runat="server" Text="Title" width ="120px" Height="16px"></asp:Label>
                    </td>
                    <td align = "left">
                        <asp:TextBox ID="txtNewTitle" width ="200px" runat="server" TabIndex="27"></asp:TextBox>
                    </td>
                    </tr>
               <tr class="medication_info_tr-odd">
                    <td align = "right">
                        <asp:Label ID="lblDesc" runat="server" Text="Description" width ="120px" Height="16px"></asp:Label>
                    </td>
                    <td align = "left">
                        <asp:TextBox ID="txtDesc" width ="200px" runat="server" TabIndex="28"></asp:TextBox>
                    </td>
               </tr>
              <tr class="medication_info_tr-even">
                    <td align = "right">
                        <asp:Label ID="lblTimeEntry" runat="server" Text="TimeEntryRequired" width ="120px" Height="16px"></asp:Label>
                    </td>
                    <td align = "left">
                        <asp:DropDownList ID="ddlTimeEntry" runat="server" Width = "200px" 
                            TabIndex="29">
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
               </tr>
               <tr class="medication_info_tr-odd">
                    <td align = "center" colspan = "2">                                                         
                        <asp:ImageButton ID="btnTitleSave" runat="server"  ImageUrl="../images/save.gif"
                        border="0"  OnClientClick = "return validateTitle();" TabIndex="30" 
                            onclick="btnTitleSave_Click" ></asp:ImageButton>
                        <asp:ImageButton ID="btnTitleCancel" runat="server"  ImageUrl="../images/cancel.gif"
                        border="0" TabIndex="31" 
                            onclick="btnTitleCancel_Click" ></asp:ImageButton>
              
                   </td>
               </tr>
           </table>  
      </asp:Panel>         
           <asp:HiddenField ID="hdnSup1" runat="server" />
           <asp:HiddenField ID="hdnSup2" runat="server" />
   </ContentTemplate></asp:UpdatePanel>
<script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("EmployeeList.aspx", "mywindow", "toolbar = no,width =650,scrollbars = yes");
    }
</script> 
</asp:Content>
