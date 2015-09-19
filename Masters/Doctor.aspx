<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Doctor.aspx.cs" Inherits="Doctor" Title="eCarex Health Care System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<asp:UpdatePanel ID="updatePanelProviderInfo" runat="server" UpdateMode="Conditional">
<Triggers>
<asp:PostBackTrigger ControlID="btnSave"/>
<asp:PostBackTrigger ControlID="btnUpdate"/>
</Triggers>
<ContentTemplate>

<cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearchProvider" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetProviderNames" BehaviorID="ModelExtender" runat="server" OnClientItemSelected="publishDocInfo">
</cc1:AutoCompleteExtender>

<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" TargetControlID="txtClinic" UseContextKey="True"  ContextKey="0" MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetClinic" runat="server">
</cc1:AutoCompleteExtender>
     
<table border="0" width="80%" align = "Center">      
      
                  <tr class="medication_info_th1">			 
                    <td colspan="4" align="center" width="100%" height="20px">
                        <asp:Label id="lblTitle" runat="server" Text="DOCTOR INFORMATION"  Font-Bold="True" Font-Size="Small" ></asp:Label>
                    </td>
                  </tr> 
                  
                  <tr class="medication_info_tr-even">
                    <td>
                    <asp:Label ID="lblUserType" runat="server" Text="User Type"></asp:Label>
                    </td>
                    <td>
                    <asp:RadioButtonList ID="rbtnUserType" runat="server"  RepeatDirection="Horizontal">
                    <asp:ListItem Value='D' Text="Doctor" Selected="True"></asp:ListItem>
                    <asp:ListItem Value='N' Text="Nurse"></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                    <td align = "left">
                            <asp:Label ID="lblSearchProvider" runat="server" Text="Search Provider" Width="150px" ></asp:Label>
                    </td>
                    <td align = "left">
                            <asp:TextBox ID="txtSearchProvider" runat="server" Width="150px" TabIndex="26"></asp:TextBox>
                            <asp:ImageButton ID="btnSearchProvider" runat="server" ImageUrl="~/images/search_new.png" border="0" onclick="btnSearchProvider_Click" TabIndex="26" Width="24px" Height="24px"></asp:ImageButton> 
                     </td>
                  </tr>
                  <tr class="medication_info_tr-odd">
                     <td width="150px" align="left">
                           <asp:Label ID="lblFName" runat="server" Text="First Name"></asp:Label>
                     </td>
                     <td width="350px">
                          <asp:TextBox ID="txtFName" runat="server" Width="150px" style="margin-left: 0px" TabIndex="1"></asp:TextBox>
                    </td>
                    <td width="150px" align="left">
                          <asp:Label ID="lblAdd1" runat="server" Text="Address 1"></asp:Label>
                    </td>
                    <td width="350px">
                          <asp:TextBox ID="txtAdd1" runat="server" Width="150px" TabIndex="12"></asp:TextBox>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-even">
                    <td width="150px" align="left">
                            <asp:Label ID="lblMName" runat="server" Text="Middle Name"></asp:Label>
                    </td>
                    <td width="350px">
                            <asp:TextBox ID="txtMName" runat="server" Width="150px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td width="150px" align = "left">
                            <asp:Label ID="lblAdd2" runat="server" Text="Address 2"></asp:Label>
                    </td>
                    <td width="350px">
                            <asp:TextBox ID="txtAdd2" runat="server" Width="150px" TabIndex="13"></asp:TextBox>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-odd">
                    <td width="150px" align="left">
                            <asp:Label ID="lblLName" runat="server" Text="Last Name"></asp:Label>
                    </td>
                    <td width="350px">
                            <asp:TextBox ID="txtLName" runat="server" Width="150px" TabIndex="3"></asp:TextBox>
                    </td>
                    <td width="150px" align="left">
                            <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                    </td>
                    <td width="350px">
                            <asp:TextBox ID="txtCity" runat="server" Width="150px" TabIndex="14"></asp:TextBox>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-even">
                    <td width="150px" align="left">
                             <asp:Label ID="lblDegree" runat="server" Text="Degree"></asp:Label>
                    </td>
                    <td width="350px">
                             <asp:TextBox ID="txtDegree" runat="server" Width="150px" TabIndex="4"></asp:TextBox>
                    </td>
                    <td width="150px" align="left">
                             <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                    </td>
                    <td width="350px">
                             <asp:TextBox ID="txtState" runat="server" Width="150px" TabIndex="15" MaxLength="2"></asp:TextBox>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-odd">
                    <td width="150px" align="left">
                             <asp:Label ID="lblSpeciality" runat="server" Text="Speciality"></asp:Label>
                    </td>
                    <td width="350px">
                             <asp:TextBox ID="txtSpeciality" runat="server" Width="150px" TabIndex="5"></asp:TextBox>
                    </td>
                    <td width="150px" align="left">
                             <asp:Label ID="lblZip" runat="server" Text="Zip"></asp:Label>
                    </td>
                    <td width="350px">
                             <asp:TextBox ID="txtZip" runat="server" Width="150px" TabIndex="16"></asp:TextBox>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-even" >
                    <td width="150px" align="left">
                              <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                    </td>
                    <td width="350px">
                              <asp:TextBox ID="txtLocation" runat="server" Width="150px" TabIndex="6" ></asp:TextBox>
                    </td>
                    <td width="150px" align="left">
                              <asp:Label ID="lbleMail" runat="server" Text="E-Mail"></asp:Label>
                    </td>
                    <td width="350px">
                              <asp:TextBox ID="txtEMail" runat="server" Height="20px" TabIndex="17" Width="150px"></asp:TextBox>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-odd">
                    <td width="150px" align="left">
                              <asp:Label ID="lblResPhoneNo" runat="server" Text="Residence Phone No"></asp:Label>
                    </td>
                    <td width="350px">
                              <asp:TextBox ID="txtResPhoneNo" runat="server" Width="150px" TabIndex="7"></asp:TextBox>
                    </td>
                    <td width="150px" align="left">
                              <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label>
                    </td>
                    <td width="350px">
                              <asp:TextBox ID="txtFax" runat="server" Width="150px" TabIndex="18"></asp:TextBox>
                    </td>
                  </tr>
                  <tr  class="medication_info_tr-even">
                    <td width="150px" align="left"> 
                              <asp:Label ID="lblCPNo" runat="server" Text="Cell Phone No"></asp:Label>
                    </td>
                    <td width="350px">
                              <asp:TextBox ID="txtCPNo" runat="server" TabIndex="8" Width="150px"></asp:TextBox>
                    </td>
                   
                    <td width="150px" align="left">
                              <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td width="350px">
                              <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="19" Width="150px">
                              <asp:ListItem Text="Active" Value="0" Enabled="true"></asp:ListItem>
                              <asp:ListItem Text="Inactive" Value="1"></asp:ListItem>
                              </asp:DropDownList>
                    </td>
                  </tr>
                  <tr class="medication_info_tr-odd">
                    <td width="150px" align="left">
                             <asp:Label ID="lblClinicName" runat="server" Text="Search Clinic"></asp:Label>
                    </td>
                    <td width="350px">
                             <asp:TextBox ID="txtClinic" runat="server" Width="150px" TabIndex="9" ></asp:TextBox>
                    </td>
                    <td width="150px" align="left">
                              <asp:Label ID="lblNPI" runat="server" Text="NPI#"></asp:Label></td>
                    <td width="350px">
                                                 <asp:TextBox ID="txtNPI" runat="server" Width="150px" TabIndex="20" ></asp:TextBox>
                        
                              </td>
                  </tr>
                   <tr class="medication_info_tr-even">
                    <td width="150px" align="left">
                             <asp:Label ID="lblDEANo" runat="server" Text="DEA#"></asp:Label>
                    </td>
                    <td width="350px">
                             <asp:TextBox ID="txtDEANo" runat="server" Width="150px" TabIndex="10" ></asp:TextBox>
                    </td>
                    <%--Added Doctor Signature Upload - START.--%>
                    <td width="150px" align="left">
                              <asp:Label ID="Label1" runat="server" Text="Upload Signature"></asp:Label>
                    </td>
                    <td width="350px">
                              <asp:FileUpload ID="FileUpload1" EnableViewState="true" runat="server"  Width="217px" TabIndex="21" /></td>
                   <%--Added Doctor Signature Upload - END.--%>
                  </tr>
                  <tr class="medication_info_tr-odd">
                    <td width="150px" align="left">
                             <asp:Label ID="lblLicNo" runat="server" Text="License#"></asp:Label>
                    </td>
                    <td width="350px">
                             <asp:TextBox ID="txtLicNo" runat="server" Width="150px" TabIndex="11" ></asp:TextBox>
                    </td>
                     <%--Added Doctor Signature image - START.--%>
                    <td width="150px" align="left">
                          <asp:Label ID="lblDocSign" runat="server" Text="Signature" Visible="false"></asp:Label>
                    </td>
                    <td width="350px">
                        <asp:Image ID="imgDocSign" runat="server" Width="150px" Height="40px" Visible="False" />
                        </td>
                        <%--Added Doctor Signature image - END.--%>
                  </tr>
                  <tr class="medication_info_tr-even" align = "center"> 
                     <td colspan="4" class="medication_info_td-odd"> 
                             <asp:ImageButton ID="btnSave" runat="server"  ImageUrl="../images/save.gif" 
                                 border="0" onclick="btnSave_Click" 
                              TabIndex="22" OnClientClick ="return validateNewDoctor();" ></asp:ImageButton>
                        
                             <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="../images/update.png" 
                                 border="0" Visible="false" 
                              Style="height: 24px" onclick="btnUpdate_Click" TabIndex="23" 
                                 OnClientClick = "return validateNewDoctor();"></asp:ImageButton>
                      
                             <asp:ImageButton ID="btnCancel" runat="server" 
                                 ImageUrl="../images/cancelPopUp.gif" border="0" onclick="btnCancel_Click" 
                              TabIndex="24"></asp:ImageButton>
                       
                             <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../images/delete.png" 
                                 border="0" Visible="false" 
                              Style="height: 24px" onclick="btnDelete_Click" TabIndex="25" 
                                 OnClientClick="if(!confirm('Are you sure, Do you want to delete Provider')){ return false;}">
                             </asp:ImageButton>
                   </td>
                   </tr>
                   <tr class="medication_info_tr-odd">
                   <td colspan="4" align="right"> 
                    <asp:LinkButton ID="lnkbtnPop" runat="server" OnClientClick = "return openwindow();">Click here</asp:LinkButton> 
                       &nbsp; to view Doctors list
              
                   </td>
                   </tr>
            </table>
            
       
   </ContentTemplate></asp:UpdatePanel>
<script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("ProvidersList.aspx", "mywindow", "toolbar = no,width =600,scrollbars = yes");
    }
</script> 
</asp:Content>
