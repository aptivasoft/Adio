<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="PatientProfile.aspx.cs" Inherits="Patient_PatinetProfile" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>
<cc1:AutoCompleteExtender ID="AutocompletePatients" BehaviorID="acCustodianEx" TargetControlID ="txtDoctor" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true"  ServiceMethod="GetDoctorNames"  CompletionListCssClass="AutoExtender2" ></cc1:AutoCompleteExtender>
 <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" BehaviorID="acCustodianEx1" TargetControlID ="txtClinicFacility" UseContextKey="true" runat="server"  MinimumPrefixLength="1"  Enabled="true"  ServiceMethod="GetClinicFaclityNames"  CompletionListCssClass="AutoExtender2" OnClientItemSelected="AutoCompleteSelected_ClinicFaclityNames"></cc1:AutoCompleteExtender>
<cc1:AutoCompleteExtender ID="AutoCompleteInsurance"  BehaviorID="acCusdfstodianEx7" TargetControlID ="txtPrimInsurance" UseContextKey="True" runat="server"  MinimumPrefixLength="1"  Enabled="True"  ServiceMethod="GetInsuranceNames"   CompletionListCssClass="AutoExtender2" OnClientItemSelected="AutoCompleteSelected_PatIns">
</cc1:AutoCompleteExtender>
<cc1:ModalPopupExtender ID="mpeAddDuplicatePatient" runat="server" 
                        TargetControlID="btnAddDupPatient" 
                        PopupControlID="pnlAddDupPatient" 
                        CancelControlID="btnCancelAddDupPatient" 
                        BackgroundCssClass="modalBackground" 
                        Enabled="True" 
                        PopupDragHandleControlID="pnlAddDupPatient" 
                        DynamicServicePath=""></cc1:ModalPopupExtender>
<cc1:ModalPopupExtender ID="mpeAddPatientStatus" 
                        runat="server" 
                        TargetControlID="btnAddPatStatus"
                        CancelControlID="btnAddPatientStatusOK"
                        PopupControlID="pnlAddPatientStatus" 
                        BackgroundCssClass="modalBackground" 
                        Enabled="True" 
                        PopupDragHandleControlID="pnlAddPatientStatus" 
                        DynamicServicePath=""></cc1:ModalPopupExtender>         
       

<table  align="center"  width="100%" class="patient_info">
<tr class="medication_info_th1">
<td align="center"  colspan="6"><b>PATIENT PROFILE [Edit Mode]</b>
</td>
</tr>
<tr class="medication_info_tr-odd">
<td  ><asp:Label ID="lblPatientName" runat ="server" Text="Patient Name(F,M,L)"></asp:Label>
    &nbsp;<font color="red">*</font></td>
<td>
<asp:TextBox ID="txtPatientFName" runat="server" Width="140px" TabIndex="1"></asp:TextBox>
<asp:TextBox ID="txtPatientMName" runat="server" MaxLength="1" Width="20px" TabIndex="1">
</asp:TextBox><asp:TextBox ID="txtPatientLName" runat="server" Width="140px" TabIndex="1"></asp:TextBox>
</td> 
<td><asp:Label ID="lblDOB" runat="server" Text="Date Of Birth"></asp:Label>
    &nbsp;<font color="red">*</font></td>
<td ><asp:TextBox ID="txtDOB" runat="server" TabIndex="1" Width="140px"></asp:TextBox>
<cc1:CalendarExtender ID="CE_DOB" runat="server" TargetControlID="txtDOB" PopupButtonID="txtDOB" Enabled="True" ></cc1:CalendarExtender>
</td>
<td  ><asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
    &nbsp;<font color="red">*</font></td>
<td><asp:RadioButton ID="rbtnMale" runat="server" Text="Male" GroupName="Gender" Checked="true" TabIndex="1"/>
<asp:RadioButton ID="rbtnFeMale" runat="server" Text="FeMale" GroupName="Gender"/>
</td>
</tr>
<tr class="medication_info_tr-even">
<td>
<asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtPhone" runat="server" TabIndex="1" Width="140px"></asp:TextBox></td>
<td><asp:Label ID="lblWorkPhone" runat="server" Text="WorkPhone" ></asp:Label></td>
<td><asp:TextBox ID="txtWorkPhone" runat="server" TabIndex="1" Width="140px"></asp:TextBox></td>
<td><asp:Label ID="lblCellPhone" runat="server" Text="Cell Phone" ></asp:Label></td>
<td><asp:TextBox ID="txtCellPhone" runat="server" TabIndex="1" Width="140px"></asp:TextBox></td>
</tr>
<tr class="medication_info_tr-odd">
<td  ><asp:Label ID="lblecontact" runat="server" Text="Contact(F,L)"></asp:Label>
</td>
<td    >
<asp:TextBox ID="txtecontactFNAME" runat="server" TabIndex="1" Width="140px"></asp:TextBox>
<asp:TextBox ID="txtecontactLNAME" runat="server" TabIndex="1" Width="140px"></asp:TextBox></td>
<td  ><asp:Label ID="lblcontRel" runat="server" Text="Relation?" ></asp:Label></td>
<td ><asp:DropDownList ID="ddlecontactREL" runat="server" TabIndex="1" Width="145px">
    <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="M">Mother</asp:ListItem>
    <asp:ListItem Value="F">Father</asp:ListItem>
    <asp:ListItem Value="P">Spouse</asp:ListItem>
    <asp:ListItem Value="B">Brother</asp:ListItem>
    <asp:ListItem Value="S">Sister</asp:ListItem>
    <asp:ListItem Value="A">GrandFather</asp:ListItem>
    <asp:ListItem Value="G">GrandMother</asp:ListItem>
    <asp:ListItem Value="O">Other</asp:ListItem>
</asp:DropDownList></td>
<td  ><asp:Label ID="lblcontPhone" runat="server" Text="Cont.Phone" ></asp:Label></td>
<td  ><asp:TextBox ID="txtecontactPHONE" runat="server" TabIndex="1" Width="140px"></asp:TextBox></td>

</tr>
<tr class="medication_info_tr-even">
<td  ><asp:Label ID="lblClinicFacility" runat="server" Text="Clinic Facility"></asp:Label>
    &nbsp;<font color="red">*</font></td>
<td><asp:TextBox ID="txtClinicFacility" runat="server" TabIndex="1" Width="300px"></asp:TextBox></td>
<td  ><asp:Label ID="lblDoctor" runat="server" Text="Doctor"></asp:Label>&nbsp;<font color="red">*</font>
</td>
<td> <asp:TextBox ID="txtDoctor" runat="server" TabIndex="1" Width="140px" ></asp:TextBox></td>
	 
<td  ><asp:Label ID="lblPrimInsurance" runat="server" Text="Primary Insurance" TabIndex="1"></asp:Label></td>
<td    >
<asp:DropDownList ID="ddlPrimInsurance" runat="server" OnDataBound="ddlPrimInsurance_DataBound" Width="145px">
</asp:DropDownList>
<asp:TextBox ID="txtPrimInsurance" runat="server" TabIndex="1" Visible="false" Width="155px"></asp:TextBox></td>
</tr>
<tr class="medication_info_tr-odd">
<td  ><asp:Label ID="lblbalance" runat="server" Text="SSN"></asp:Label>&nbsp;<font color="red">*</font></td>
<td    ><asp:TextBox ID="txtSSN" runat="server" TabIndex="1" Width="140px" MaxLength="9"></asp:TextBox>
<cc1:FilteredTextBoxExtender TargetControlID="txtSSN"  FilterType="Numbers" runat="server"></cc1:FilteredTextBoxExtender>
</td>
<td>
<asp:Label ID="lblMRN" runat="server" Text="MRN"></asp:Label>&nbsp;
</td>
<td >
<asp:TextBox ID="txtMRN" runat="server" TabIndex="1" Width="140px"></asp:TextBox>
</td>
<td colspan="2" valign="middle">
<asp:Label ID="lblAutoRefillOption" runat="server" Text="Auto Refill Option"></asp:Label>
<asp:CheckBox ID="chkAutoreFillOption" runat="server" TabIndex="1"/>
</td>
</tr>  
<tr class="medication_info_tr-odd">
<td><asp:Label ID="lblAddress" runat="server" Text="Billing Address"></asp:Label></td>
<td>&nbsp;</td>
<td colspan="4"><asp:Label ID="Label1" runat="server" Text="Mailing Address"></asp:Label>
              <asp:Checkbox ID="chkboxMAddress" runat="server"  
                  Text="Same as Billing" oncheckedchanged="chkboxMAddress_CheckedChanged" AutoPostBack="true" />
</td>
</tr>    
<tr class="medication_info_tr-even">
<td><asp:Label ID="Label2" runat="server" Text="Address1"></asp:Label></td>
<td><asp:TextBox ID="txtAddress1" runat="server" Width="315px" TabIndex="1" ></asp:TextBox></td>
<td  colspan="4"><asp:TextBox ID="txtSAddr1" runat="server" Width="300px" TabIndex="6"></asp:TextBox></td>
</tr>     
<tr class="medication_info_tr-odd">
<td>
<asp:Label ID="Label3" runat="server" Text="Address2"></asp:Label>
</td>
<td  >
<asp:TextBox ID="txtAddress2" runat="server" Width="315px" TabIndex="2"></asp:TextBox></td>
<td colspan="4"> 
<asp:TextBox ID="txtSAddr2" runat="server" Width="300px" TabIndex="7"></asp:TextBox></td>
</tr>
<tr class="medication_info_tr-even">
<td><asp:Label ID="lblCSZ" runat="server" Text="City, State & Zip"></asp:Label></td>
<td><asp:TextBox ID="txtCity" runat="server" TabIndex="3" Width="140px"></asp:TextBox>
<asp:TextBox ID="txtState" runat="server" Width="20px" MaxLength="2" TabIndex="4"></asp:TextBox>
<asp:TextBox ID="txtZip" runat="server"   TabIndex="5" Width="135px"></asp:TextBox></td>
<td  colspan="4">
<asp:TextBox ID="txtSCity1" runat="server" TabIndex="8" Width="140px"></asp:TextBox>
<asp:TextBox ID="txtSState" runat="server" Width="20px" MaxLength="2" TabIndex="9"></asp:TextBox>
<asp:TextBox ID="txtSZIP" runat="server" Width="120px" TabIndex="10"></asp:TextBox></td>
</tr> 
<tr class="medication_info_tr-odd">
<td>
<asp:Label ID="lblDiagnosis" runat="server" Text="Diagnosis Codes"></asp:Label>
</td>
<td>
<asp:TextBox ID="txtDiagn1" runat="server" Width="55px" TabIndex="11"></asp:TextBox>            
<asp:TextBox ID="txtDiagn2" runat="server" Width="55px" TabIndex="11"></asp:TextBox>            
<asp:TextBox ID="txtDiagn3" runat="server" Width="55px" TabIndex="11"></asp:TextBox>            
<asp:TextBox ID="txtDiagn4" runat="server" Width="55px" TabIndex="11"></asp:TextBox>
<asp:TextBox ID="txtDiagn5" runat="server" Width="55px" TabIndex="11"></asp:TextBox>
</td>
<td colspan="2" valign="middle">
<asp:CheckBox ID="chkHippa" runat="server" TabIndex="11" Text="HIPAA Notice" TextAlign="Left" /> 
<asp:Label ID="lblHippaDate" Text="Date" runat="server" />

<asp:TextBox ID="txtHippaDate" runat="server" Width="90px" TabIndex="11"></asp:TextBox>
<cc1:CalendarExtender ID="ceHippaDate" runat="server"  TargetControlID="txtHippaDate" PopupButtonID="txtHippaDate"  Enabled="True" ></cc1:CalendarExtender>  
</td>
<td>
<asp:Label ID="lblPatStatus" Text="Patient Status" runat="server"></asp:Label>
</td>
<td>
<asp:DropDownList ID="ddlPatStatus" runat="server">
<asp:ListItem Text="Active" Value="Y" Selected="True"></asp:ListItem>
<asp:ListItem Text="InActive" Value="N"></asp:ListItem>
</asp:DropDownList>
</td>
</tr>    
<tr class="medication_info_tr-even">
<td colspan="6" align="center">

<asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/images/save.gif" 
                    onclick="btnSave_Click" OnClientClick="return  ValidatePatientProf();" TabIndex="11"/>
<asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/images/update.png" 
                        Height="27px"  Visible="False" onclick="btnUpdate_Click"  OnClientClick="return  ValidatePatientProf();" TabIndex="11"/>
<asp:ImageButton ID="btnCancel" runat="server" 
                        ImageUrl="~/images/cancelPopUp.gif"   onclick="btnCancel_Click" 
                        TabIndex="11"/>    
</td>
</tr>
<tr class="medication_info_tr-odd"><td colspan="6"> &nbsp;<font color="red">Note : * 
    indicates fields are mandatory</font></td></tr>
<%--<tr class="medication_info_tr-even"><td colspan="6" align="center">
<asp:Label ID="lblStatus" runat="server" Visible="false" Font-Bold="true" ForeColor="Red" Font-Size="11px"></asp:Label>
</td>
</tr>--%>

</table>
 <input type="button" id="btnAddDupPatient" style="visibility:hidden" text="Add Dup Patient" runat="server" />
 <asp:Panel ID="pnlAddDupPatient" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
 <table width="325"   border="0" cellpadding="0" cellspacing="0">
 <tr class="medication_info_th1" width="100%">
  <td   colspan="2" height="30px"  align="left"> &nbsp;&nbsp;Add Duplicate Patient </td>
  </tr>
 <tr>
 <td   valign="middle" align="center">
  &nbsp;<asp:Image ID="imgWarnDupPat" runat="server" ImageUrl="~/images/accdeny.gif" /> 
</td>
  <td>
 <asp:Label ID="lblAddDupPatient" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
 </td>
 </tr>
 <tr>
 <td colspan="2" align="center">
 <asp:ImageButton ID="btnAddDuplicatePatient" runat="server" 
                        ImageUrl="~/images/add.gif" onclick="btnAddDuplicatePatient_Click"   
                       />
                       &nbsp;&nbsp;
 <asp:ImageButton ID="btnCancelAddDupPatient" runat="server" 
                        ImageUrl="~/images/cancelPopUp.gif"   
                       />
 </td>
 </tr>
 </table>
 </asp:Panel>
 
 <input type="button" id="btnAddPatStatus" runat="server" style="visibility:hidden" value="" />
 <asp:Panel ID="pnlAddPatientStatus" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
 <table width="350"   border="0" cellpadding="0" cellspacing="0">
 <tr class="medication_info_th1" width="100%">
 <td colspan="2" height="30px"  align="left"> &nbsp;&nbsp;Add Patient Status</td>
 
 </tr>
 <tr>
 <td align="center">
            <div id="divPatWarning" runat="server" visible="false">
             <asp:Image ID="imgWarn" ImageUrl="~/images/accdeny.gif" runat="server" />
             </div>
             <div id="divPatError" runat="server" visible="false">
             <asp:Image ID="imgError" ImageUrl="~/images/red-error.gif" runat="server" />
             </div>
             <div id="divPatSuccess" runat="server" visible="false">
             <asp:Image ID="imgOK" ImageUrl="~/images/check.gif" runat="server" />
             </div>
            </td>
            <td>
            <asp:Label ID="lblAddPatientStatus" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
            </td>
 </tr>
 <tr>
 <td colspan="2" align="center">
 <asp:ImageButton ID="btnAddPatientStatusOK" runat="server" 
                        ImageUrl="~/images/ok.jpg" />
 
 </td>
 </tr>
 </table>
 </asp:Panel>
 <asp:HiddenField ID="hidPat_ID" runat="server" />
    <asp:HiddenField ID="hidPat_INSID" runat="server" />
    <asp:Label ID="lbl" runat="server"></asp:Label>
<table align="center"  width="80%" class="patient_info">
<tr>
<td>

<cc1:TabContainer ID="tabCntPatient" Visible="false" runat="server" Width="100%"  
        ActiveTabIndex="0"  OnActiveTabChanged="tabCntPatient_ActiveTabChanged"  
        AutoPostBack="true" ScrollBars="Auto" BorderWidth="0" CssClass="blue"   >
        <cc1:TabPanel ID="Patient_Ins" runat="server" Height="85"><HeaderTemplate>Insurance</HeaderTemplate>
    <ContentTemplate>
    <cc1:ModalPopupExtender ID="AddInsurance" runat="server" TargetControlID="btnAddIns" PopupControlID="pnlAddInsurance" CancelControlID="btnIClose" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></cc1:ModalPopupExtender>
    <asp:GridView ID="gridPatInsurance" runat="server" AutoGenerateColumns="False" Width="100%" >
    <AlternatingRowStyle CssClass="medication_info_tr-even" />
    <Columns>
    <asp:TemplateField HeaderText="S.No">
    <ItemTemplate>
    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
    </ItemTemplate>                        
        <ItemStyle HorizontalAlign="Center" Width="50px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Insurance Name">
    <ItemTemplate>
    <asp:Label ID="lblpatIns" runat="server" Text='<%# Eval("Ins_Name")%>'></asp:Label>                                
    </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Policy ID">
    <ItemTemplate>
    <asp:Label ID="lblpatInsNo" runat="server" Text='<%# Eval("PI_PolicyID")%>'></asp:Label>                                
    </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:TemplateField>  
    <asp:TemplateField HeaderText="BIN #">
    <ItemTemplate>
    <asp:Label ID="lblpatBINNo" runat="server" Text='<%# Eval("PI_BINNo")%>'></asp:Label>                                
    </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:TemplateField> 
    <asp:TemplateField HeaderText="GROUP NO">
    <ItemTemplate>
    <asp:Label ID="lblpatGRPNo" runat="server" Text='<%# Eval("PI_GroupNo")%>'></asp:Label>                                
    </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:TemplateField>    
    <asp:TemplateField HeaderText="Phone #">
    <ItemTemplate>
    <asp:Label ID="lblpatPhNo" runat="server" Text='<%# Eval("PI_Phone")%>'></asp:Label>                                
    </ItemTemplate>
    </asp:TemplateField>  
    <asp:TemplateField HeaderText="Rel.To Insd">
    <ItemTemplate>
    <asp:Label ID="lblrelINSD" runat="server" Text='<%# Eval("PI_InsdRel")%>'></asp:Label>                                
    </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField> 
    <asp:TemplateField HeaderText="Insd Name">
    <ItemTemplate>
    <asp:Label ID="lblINSDName" runat="server" Text='<%# Eval("PI_InsdName")%>'></asp:Label>                                
    </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>                                      
    </Columns>
    <HeaderStyle CssClass="medication_info_th1" />
    <RowStyle CssClass="medication_info_tr-odd" />
    </asp:GridView>
    <asp:LinkButton ID="btnAddIns" ForeColor="#000066"  runat="server" OnClientClick="clearInsurancePanel();">Add Insurance</asp:LinkButton>
    <asp:Panel ID="pnlAddInsurance" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" BehaviorID="acCusdfstodianEx7" 
            TargetControlID ="txtInsName" UseContextKey="True" runat="server"  
            MinimumPrefixLength="1"  Enabled="True"  ServiceMethod="GetInsuranceNames"  
            CompletionListCssClass="AutoExtender2" DelimiterCharacters="" ServicePath="">
    </cc1:AutoCompleteExtender>
    <table width="350"   border="0" cellpadding="0" cellspacing="0">
    <tr class="medication_info_th1" width="100%">
    <td colspan="2"  height="30px"  align="left">&nbsp;&nbsp;Insurance Details</td>
    <td align="right">
    <asp:ImageButton ID="btnIClose" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
    </td>
    </tr>
    <tr>
    <td width="350" colspan="3" align="center">
    <table aligin="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
    <tr>
    <td align="left" width="150px">
    <asp:Label ID = "Label4" Text = "Insurance Name :" runat = "server" Font-Bold="True"></asp:Label>
    </td>
    <td colspan="2" width="195px">
    <asp:TextBox ID="txtInsName" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left" width="150px">
    <asp:Label ID = "Label5" Text = "Insurance Phone :" runat = "server" Font-Bold="True"></asp:Label>
    </td>
    <td colspan="2" width="195px">
    <asp:TextBox ID="txtInsPhone" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left" width="150px">
    <asp:Label ID = "lblPID" Text = "Policy ID :" runat = "server"  Font-Bold="True"></asp:Label>
    </td>
    <td align="left" colspan="2" width="195px">
    <asp:TextBox ID="txtPID" runat="server" ></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left" width="150px">
    <asp:Label ID = "lblGNo" Text = "Group No :" runat = "server"  Font-Bold="True"></asp:Label>
    </td>
                          <td align="left" colspan="2" width="195px">
                              <asp:TextBox ID="txtGNO" runat="server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" width="150px">
                            <asp:Label ID = "lblBNO" Text = "BIN No :" runat = "server"  Font-Bold="True"></asp:Label>
                          </td>
                          <td align="left" colspan="2" width="195px">
                              <asp:TextBox ID="txtBNO" runat="server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" width="150px">
                            <asp:Label ID = "lblIName" Text = "Insured Name :" runat = "server"  Font-Bold="True"></asp:Label>
                          </td>
                          <td align="left" colspan="2" width="195px">
                              <asp:TextBox ID="txtIName" runat="server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" width="150px">
                            <asp:Label ID = "lblIDOB" Text = "Insured DOB :" runat = "server"  
                                  Font-Bold="True"></asp:Label>
                          </td>
                          <td align="left" colspan="2" width="195px">
                              <asp:TextBox ID="txtIDOB" runat="server" ></asp:TextBox>
                              <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIDOB" PopupButtonID="txtIDOB" Enabled="True"></cc1:CalendarExtender>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" width="150px">
                            <asp:Label ID = "lblISSN" Text = "Insured SSN :" runat = "server"  
                                  Font-Bold="True"></asp:Label>
                          </td>
                          <td align="left" colspan="2" width="195px">
                              <asp:TextBox ID="txtISSN" runat="server" ></asp:TextBox>  
                          </td>
                        </tr>
                        <tr>
                          <td align="left" width="150px">
                            <asp:Label ID = "lblIRel" Text = "Insured Relation :" runat = "server"  
                                  Font-Bold="True"></asp:Label>
                          </td>
                          <td align="left" colspan="2" width="195px">
                              <asp:TextBox ID="txtIRel" runat="server" ></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" colspan="3" width="345px">
                          <asp:Label ID="lblIsPrimary" runat = "server" Text = "Is Primary Insurance:" 
                                  Font-Bold="True"></asp:Label>
                          <asp:CheckBox ID="chkIsPrimary" runat ="server"/>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" colspan="3" width="195px">
                              <asp:RadioButton ID="rbtnIActive" Text="Active" GroupName="InsActive" runat="server" />
                              &nbsp;<asp:RadioButton ID="rbtnIinActive" Text="InActive" GroupName="InsActive" runat="server" />
                          </td>
                        </tr>
                        <tr>
                          <td  align="center" colspan="4">
                                <asp:ImageButton ID="btnSaveInsurance" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btnSaveInsurance_Click" OnClientClick="return ValidatePatientInsurance2();"></asp:ImageButton>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </asp:Panel>
             </ContentTemplate>
             </cc1:TabPanel>
<cc1:TabPanel ID="Patient_Allergies" runat="server" Height="85">
    <HeaderTemplate>Allergies</HeaderTemplate>
    <ContentTemplate>
    <cc1:ModalPopupExtender ID="AddAllergy" runat="server" TargetControlID="btnAddAllergy" PopupControlID="pnlAddAllergy" CancelControlID="btnAClose" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></cc1:ModalPopupExtender>
    <asp:GridView ID="gridPatAllergies" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCancelingEdit="gridPatAllergies_RowCancelingEdit" OnRowEditing="gridPatAllergies_RowEditing" OnRowUpdating="gridPatAllergies_RowUpdating" OnRowCommand="gridPatAllergies_RowCommand" AllowPaging="True" OnRowDeleting="gridPatAllergies_RowDeleting" PageSize="5">                                         <AlternatingRowStyle CssClass="medication_info_tr-even" />
    <Columns>
    <asp:CommandField ShowEditButton="True">
    <ItemStyle HorizontalAlign="Center" Width="50px"/>
    </asp:CommandField>
    <asp:TemplateField>
    <ItemTemplate>
    <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/images/delete.gif" OnClientClick="if(!confirm('Do you want to delete Allergy')){ return false;}" CommandName="Delete" CommandArgument='<%# Eval("PA_ID") %>'/>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" Width="20px" />
    </asp:TemplateField>
    <asp:TemplateField Visible="False">
    <ItemTemplate>
    <asp:Label ID="lblPA_ID" runat="server" Text='<%#Eval("PA_ID")%>'></asp:Label>
    </ItemTemplate>                        
    </asp:TemplateField>
    <asp:TemplateField HeaderText="S.No">
    <ItemTemplate>
    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
    </ItemTemplate>                        
    <ItemStyle HorizontalAlign="Center" Width="50px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Allergic To">
    <ItemTemplate>
    <asp:Label ID="lblAllergic" runat="server" Text='<%# Eval("Pat_Allergic_To")%>'></asp:Label>                                
    </ItemTemplate>
    <EditItemTemplate>
    <asp:TextBox ID="txtAlleryTO" runat="server" Text='<%# Eval("Pat_Allergic_To")%>'></asp:TextBox>
    </EditItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Allergic Condition">
    <ItemTemplate>
    <asp:Label ID="lblAlleCond" runat="server" Text='<%# Eval("Pat_Allergy_Desc")%>'></asp:Label>                                
    </ItemTemplate>
    <EditItemTemplate>
    <asp:TextBox ID="txtAllergyDesc" runat="server" Text='<%# Eval("Pat_Allergy_Desc")%>'></asp:TextBox>
    </EditItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    </Columns>                
    <HeaderStyle CssClass="medication_info_th1" />
    <RowStyle CssClass="medication_info_tr-odd" />
    </asp:GridView>
    <asp:LinkButton ID="btnAddAllergy" runat="server" OnClientClick="clearAllergyPanel();" ForeColor="#000066" >Add Allergy</asp:LinkButton>
    <asp:Panel ID="pnlAddAllergy" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
    <table width="350"   border="0" cellpadding="0" cellspacing="0">
    <tr class="medication_info_th1" width="100%">
    <td  height="30px"  align="left">
        &nbsp;&nbsp;Allergy Details
    </td>
    <td align="right">
    <asp:ImageButton ID="btnAClose" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
    </td>
    </tr>
    <tr>
    <td width="350" colspan="2" align="center">
    <table aligin="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
    <tr >
    <td align="left">
    <asp:Label ID = "lblAllergyTo" Text = "Allergic To :" runat = "server"  Font-Bold="True"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="txtAllergyTo" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left" >
    <asp:Label ID = "lblAllergyDesc" Text = "Allergy Description :" runat = "server"  Font-Bold="True"></asp:Label>
    </td>
    <td align="right" height="25px">
    <asp:TextBox ID="txtAllergyDesc" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td  align="center" colspan="2">
    <asp:ImageButton ID="btnSaveAllergy" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btnSaveAllergy_Click" OnClientClick="return ValidatePatientAllergy();"></asp:ImageButton>
    </td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
    </asp:Panel>
    </ContentTemplate>
    </cc1:TabPanel>     
    
</cc1:TabContainer> 
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
</table>
    

</asp:Content>

