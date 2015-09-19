<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="NurseHome.aspx.cs" Inherits="NurseHome" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<cc1:ModalPopupExtender ID="popRxApproval" runat="server" TargetControlID="btnRxApproval" PopupControlID="pnlRxApproval" CancelControlID="btnRxCancel" BackgroundCssClass="modalBackground" BehaviorID="RxApproval"></cc1:ModalPopupExtender>
<cc1:ModalPopupExtender ID="popMedIssue" runat="server" TargetControlID="btnMedIssue" PopupControlID="pnlMedIssue" CancelControlID="btnMedIssueCancel" BackgroundCssClass="modalBackground" BehaviorID="MedIssue"></cc1:ModalPopupExtender>
<cc1:ModalPopupExtender ID="popEmailCallLog" runat="server" TargetControlID="btnEmailCallLog" PopupControlID="pnlEmailCallLog" CancelControlID="imgBtnCloseCallLog" BackgroundCssClass="modalBackground" ></cc1:ModalPopupExtender>

<table width="100%" align="center">
<tr>
<td width="70%" valign="top">
<table width="100%">
<tr>
<td align="left" class="medication_info_th1" colspan="2"><asp:Label ID="lblHeading" runat="server" Text="Pending Med Requests"></asp:Label></td>
</tr>
<tr>
<td align="left" class="medication_info_tr-even">
<asp:Label ID="Label1" runat="server" Text="Filter By :"></asp:Label>
&nbsp;&nbsp;
<asp:RadioButton ID="rbtnFilterByDate" runat="server" GroupName="rbtnFilter" 
        Text="Date" AutoPostBack="true" 
        oncheckedchanged="rbtnFilterByDate_CheckedChanged" />
&nbsp;&nbsp;
<asp:RadioButton ID="rbtnFilterByFac"  runat="server" GroupName="rbtnFilter" 
        Text="Facility" AutoPostBack="true" 
        oncheckedchanged="rbtnFilterByFac_CheckedChanged" />
&nbsp;&nbsp;
<asp:RadioButton ID="rbtnFilterByDoc"  runat="server" GroupName="rbtnFilter" 
        Text="Doctor" AutoPostBack="true" 
        oncheckedchanged="rbtnFilterByDoc_CheckedChanged"/>
&nbsp;&nbsp;
</td>
<td class="medication_info_tr-even">
  <asp:LinkButton runat="server" onclick="btnQueueList_Click" ID="btnQueueList"><asp:Image ID="imgView" ImageUrl="../Images/search_new.png" runat="server" Height="24px" Width="24px" ToolTip="View" /></asp:LinkButton>&nbsp;&nbsp;
 </td>
</tr>
<tr>
<td  align="left"  colspan="2" style="background-color:#d6e3ec;height:25px;font-size: 11px; border: 1px solid #FFFFFF;vertical-align: middle;" >
 <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" 
                           BehaviorID="acCustodianEx4" 
                           TargetControlID ="txtFilterDoctor" 
                           UseContextKey="True" 
                           runat="server"  
                           MinimumPrefixLength="1"  
                           Enabled="True"  
                           ServiceMethod="GetDoctorNames"
                           CompletionListCssClass="AutoExtender2" 
                           DelimiterCharacters="" 
                           OnClientItemSelected="AutoCompleteSelected_Doctor"
                           ServicePath="" 
                           ></cc1:AutoCompleteExtender> 
 <div id="pnlDateSelect"   style="position:absolute;" runat="server" visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Start Date : &nbsp;&nbsp;
        <asp:TextBox ID="txtStartDate" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
        <cc1:CalendarExtender ID="ceStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="txtStartDate" Enabled="True" ></cc1:CalendarExtender>
        &nbsp;(MM/DD/YYYY)&nbsp;&nbsp;&nbsp;End Date : &nbsp;&nbsp;
        <asp:TextBox ID="txtEndDate" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
         <cc1:CalendarExtender ID="cdEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="txtEndDate" Enabled="True" ></cc1:CalendarExtender>
        &nbsp;&nbsp;&nbsp;(MM/DD/YYYY)&nbsp;
        &nbsp;<asp:Label ID="lblerror" runat="server" Text="Enter Proper Date Formats" Visible="false" ForeColor="Red"></asp:Label></div>
 <div id="pnlFacilitySelect" style ="position:absolute;" runat="server" visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select Clinic: &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlFilterClinic" runat="server" 
            AutoPostBack="true" ondatabound="ddlFilterClinic_DataBound" 
            onselectedindexchanged="ddlFilterClinic_SelectedIndexChanged"></asp:DropDownList>
        &nbsp;Select Facility: &nbsp;&nbsp;
        <asp:DropDownList ID="ddlFilterFacility" runat="server" AutoPostBack="true" 
            ondatabound="ddlFilterFacility_DataBound" 
            onselectedindexchanged="ddlFilterFacility_SelectedIndexChanged"></asp:DropDownList>
    </div>
 <div id="pnlDoctorSelect" style="position:absolute;" runat="server" visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select Doctor: &nbsp;&nbsp;
        <asp:TextBox ID="txtFilterDoctor" runat="server" 
            ></asp:TextBox>
            <input type="hidden" id="hidFilterDocID" runat="server" />
    </div> 
</td>
</tr>
<tr>
<td colspan="2">
<asp:GridView ID="gridRxMedRequest" runat="server" AutoGenerateColumns="False" width="100%"
                        OnRowCommand="gridRxMedRequest_RowCommand"   onpageindexchanging="gridRxMedRequest_PageIndexChanging" PagerSettings-Visible="false" AllowPaging="true" PageSize="5"  EmptyDataText="No Records Found..."  >
                         <Columns>
                                            
                            <asp:TemplateField HeaderText="DoctorID" ItemStyle-HorizontalAlign="Left" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_ID" runat="server" Text='<%#Eval("Doc_ID")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>  
                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRxDate" runat="server" Text='<%#Eval("Rx_Date")%>'></asp:Label>
                            </ItemTemplate>   
                            
                        <ItemStyle HorizontalAlign="center" Width="20px"></ItemStyle>
                        </asp:TemplateField>
                        
                       <asp:HyperLinkField HeaderText="Patient" ItemStyle-HorizontalAlign="left" ItemStyle-Width="125px"
                                DataNavigateUrlFields="Pat_ID" 
                                DataNavigateUrlFormatString="../Patient/AllPatientProfile.aspx?patID={0}" 
                                DataTextField="PatientName"  >
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:HyperLinkField>
                        
                       <asp:TemplateField HeaderText="Medication" ItemStyle-HorizontalAlign="Left" >
                           
                           <ItemTemplate>
                                <asp:Label ID="lblMed" runat="server" Text='<%# Eval("Med")%>' Font-Bold="false"></asp:Label> 
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("rxType")%>' Font-Bold="false"></asp:Label>                      
                           </ItemTemplate>
                         
                        <ItemStyle HorizontalAlign="Left" Width="225px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Doctor" ItemStyle-HorizontalAlign="Left" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDoc_Name" runat="server" Text='<%#Eval("doctorName")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>   
                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                        </asp:TemplateField>  
                        
                       <asp:TemplateField  HeaderText="Action">
                                <ItemTemplate>
                                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Approve" CommandArgument='<%# Eval("Rx_Req_ID")%>'  Font-Bold="false" >Approve</asp:LinkButton>
                                     &nbsp;&nbsp;|&nbsp;&nbsp;
                                      <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Denied" CommandArgument='<%# Eval("Rx_Req_ID")%>' Font-Bold="false" >Deny</asp:LinkButton>
                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px"   />
                            </asp:TemplateField>
                           
                       </Columns>
                        
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                         <PagerSettings Mode="NextPrevious" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                       <EmptyDataRowStyle Font-Italic="true" BackColor="#eaf0f4" HorizontalAlign="Center" Width="100%"/>
                 </asp:GridView>
                
</td>
</tr>
<tr>
<td width="70%" valign="top">
<table width="100%">
<tr>
<td><asp:LinkButton ID="btnPMR" runat="server" CommandArgument="0" onclick="btnPMR_Click" CommandName="Previous" >Previous</asp:LinkButton></td>
<td><asp:LinkButton ID="btnNMR" runat="server" CommandArgument="1" CommandName="Next" onclick="btnPMR_Click">Next</asp:LinkButton></td>
<td align="right"><a id="btnALLMR"  runat="server" href="../Activities/RxRequestQueue.aspx">
    ALL</a>
</td>
</tr>
</table>
</td>
</tr>
</table>
</td>
<td width="30%" valign="top">
<table width="100%">
            <tr >
               <td align="left" colspan="2" height="20px" class="medication_info_th1">
                <asp:Label ID="lblHeading2" runat="server" Text=""></asp:Label>
               </td>
            </tr>
            <tr>
               
                   <td align="left" class="medication_info_tr-odd"><asp:Label ID="lblNOP" runat="server" Text="No. of Prescriptions:"></asp:Label>
                    </td>
                    <td align="right" class="medication_info_tr-odd">&nbsp;<asp:Label ID="lblNOP1" runat="server" Text=""  Font-Bold="false"></asp:Label></td>
                    
                   
            </tr>
            <tr >
               
                    <td align="left" class="medication_info_tr-even">&nbsp;&nbsp;<asp:Label ID="lblNOR" runat="server" Text="No. of Refills:"></asp:Label></td>
                    <td align="right" class="medication_info_tr-even">&nbsp;<asp:Label ID="lblNOR1" runat="server" Text=""  Font-Bold="false"></asp:Label></td>
            </tr>
            <tr>
               
                    <td align="left" class="medication_info_tr-odd"><asp:Label ID="lblNOMR" runat="server" Text="No. of Med Requests:"></asp:Label>
                    </td>
                    <td align="right" class="medication_info_tr-odd">&nbsp;<asp:Label ID="lblNOMR1" runat="server" Text=""  Font-Bold="false"></asp:Label></td>
                    
                   
            </tr>
            <tr>
               
                    <td align="left" class="medication_info_tr-even">&nbsp;&nbsp;<asp:Label ID="lblNOMI" runat="server" Text="No. of Med-Line Issues:"></asp:Label></td>
                    <td align="right" class="medication_info_tr-even">&nbsp;<asp:Label ID="lblNOMI1" runat="server" Text="" Font-Bold="false"></asp:Label></td>
                    
                   
            </tr>
            
            </table>
</td>
</tr>
<tr>
<td valign="top" width="70%">
<table width="100%">
<tr class="medication_info_th1">
<td align="left" >
<asp:Label ID="lblHeading1" runat="server" Text="Pending Med-Line Issues"></asp:Label>
</td>
<td colspan="2">
    <asp:ImageButton ID="imgBtnSendCallLog" runat="server" 
            ImageUrl="../Images/email.gif"  ToolTip="E-Mail Call Log" runat="server" 
            onclick="imgBtnSendCallLog_Click" style="height: 16px; width: 16px" 
            Width="16px" />
            &nbsp;&nbsp;&nbsp;
         <asp:ImageButton ID="imgBtnPrintCallLog" runat="server" 
            ToolTip="Print Call Log" ImageUrl="../Images/print.gif" 
            onclick="imgBtnPrintCallLog_Click" />
</td>
</tr>
<tr>
<td colspan="3">
<asp:GridView ID="gridRxMedIssue" 
                              runat="server" 
                              AutoGenerateColumns="False" 
                              OnRowCommand="gridRxMedIssue_RowCommand"
                              onpageindexchanging="gridRxMedIssue_PageIndexChanging" 
                              PagerSettings-Visible="false" 
                              AllowPaging="true" 
                              PageSize="5"
                              EmptyDataText="No Records Found..." width="100%" >
                     <Columns>
                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Rx_Date")%>'></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                        <asp:HyperLinkField HeaderText="Patient" ItemStyle-HorizontalAlign="left" ItemStyle-Width="150px"
                                DataNavigateUrlFields="Pat_ID" 
                                DataNavigateUrlFormatString="../Patient/AllPatientProfile.aspx?patID={0}" 
                                DataTextField="PatientName"  >
                           <ItemStyle HorizontalAlign="Left" />
                       </asp:HyperLinkField>
                        
                       <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Note")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="Doctor" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDoc1" runat="server" Text='<%#Eval("doctorName")%>'></asp:Label>
                            </ItemTemplate>   
                            
                        <ItemStyle Width="150px"></ItemStyle>
                        </asp:TemplateField>
                        
                       <asp:TemplateField  HeaderText="Action">
                                <ItemTemplate >
                                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Action" CommandArgument='<%# Eval("Call_ID")%>' Font-Bold="false">Action</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                       <EmptyDataRowStyle Font-Italic="true" BackColor="#eaf0f4" HorizontalAlign="Center" Width="100%"/>
                 </asp:GridView>
</td>
</tr>
<tr>
<td>
<table width="100%">
<tr>
<td><asp:LinkButton ID="btnPMI" runat="server" CommandArgument="0" onclick="btnPMI_Click" CommandName="Previous" >Previous</asp:LinkButton></td>
<td><asp:LinkButton ID="btnNMI" runat="server" CommandArgument="1" CommandName="Next" onclick="btnPMI_Click">Next</asp:LinkButton></td>
<td align="right"><a id="btnAllMI" runat="server" href="../Patient/MedIssueQueue.aspx">
    ALL</a></td>
</tr>
</table>
</td>
</tr>
</table>
</td>
<td valign="top" width="30%">
</td>
</tr>
</table>

 <asp:Panel ID="pnlRxApproval" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
                <table width="350"   border="0" cellpadding="0" cellspacing="0">
                  <tr class="medication_info_th1" width="100%">
                    <td  height="30px" align="left">
                        RxApproval
                    </td>
                    <td align="right">
                      <asp:ImageButton ID="btnRxCancel" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
                    </td>
                  </tr>
                  <tr>
                    <td width="350" colspan="2" align="center">
                      <table aligin="center" class="medication_info_popup" cellpadding="0" cellspacing="2">
                        <tr >
                          <td align="left">
                            <asp:Label ID = "Label2" Text = "Doctor Name:" runat = "server"  Font-Bold="true"></asp:Label>
                          </td>
                          <td>
                            
                             <cc1:AutoCompleteExtender ID="autoSrchDoc" 
                                                       BehaviorID="acCustodianEx4" 
                                                       TargetControlID ="txtDocName" 
                                                       UseContextKey="True" 
                                                       runat="server"  
                                                       MinimumPrefixLength="1"  
                                                       Enabled="True"  
                                                       ServiceMethod="SearchDoctorNames"   
                                                       ServicePath="" OnClientItemSelected="AutoCompleteSelected_Doctor"
                                                       CompletionListCssClass="AutoExtender2">
                                                       </cc1:AutoCompleteExtender>
                             <asp:TextBox ID="txtDocName" runat="server" Width="175px"></asp:TextBox>
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "Label12" Text = "Medication Name:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td>
                            
                                <asp:Label ID = "lblMedicationName"  runat = "server" ></asp:Label>
                             
                          </td>
                        </tr>
                       
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "Label14" Text = "Quantity:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left" height="25px">
                            <asp:TextBox ID="txtQuantity"  runat="server" Width="175px"></asp:TextBox>
                           
                          </td>
                           
                        </tr>
                        <tr>
                          <td  align="left" >
                            <asp:Label ID = "Label3" Text = "Refills:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left" height="25px">
                            <asp:DropDownList ID="ddlRefills" runat="server" Width="155px">
                                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                </asp:DropDownList>
                          </td>
                        </tr>
                        <tr>
                          <td  align="left" >
                            <asp:Label ID = "Label4" Text = "Pharmacy:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left" height="25px">
                               <asp:RadioButtonList ID="rbtnPhrm" runat="server"  RepeatDirection="Horizontal" AutoPostBack="false"
                                    onselectedindexchanged="rbtnPhrm_SelectedIndexChanged">
                                    <asp:ListItem Text="ADiO" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="1" ></asp:ListItem>
                                </asp:RadioButtonList> 
                          </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="txtPharmacy" runat="server" Width="150px" Visible=true></asp:TextBox></td>
                        </tr>
                        <tr>
                          <td  align="left" >
                            <asp:Label ID = "Label15" Text = "SIG:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left" height="25px">
                            <asp:TextBox ID="txtSIG" runat="server" Width="175px"></asp:TextBox>
                          </td>
                        </tr>
                       
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "Label17" Text = "Note to Pharmacist:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left">
                            <asp:TextBox ID="txtNote" runat="server" TextMode="multiLine" Width="175px"></asp:TextBox>
                          </td>
                        </tr>
                        
                        
                        <tr>
                          <td  align="center" colspan="2">
                              <asp:HiddenField ID="hfID" runat="server" />
                              <asp:HiddenField ID="hfStatus" runat="server" />
                              <asp:HiddenField ID="hidDocID" runat="server" />
                                <asp:ImageButton ID="btnRxApprovalSave" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btnRxApprovalSave_Click"></asp:ImageButton>
                             
                          </td>
                          
                        </tr>
                      </table>
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>
             <asp:Panel ID="pnlMedIssue" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
                <table width="350"   border="0" cellpadding="0" cellspacing="0">
                  <tr class="medication_info_th1" width="100%">
                    <td  height="30px" align="left">
                        Med Issue
                    </td>
                    <td align="right">
                      <asp:ImageButton ID="btnMedIssueCancel" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
                    </td>
                  </tr>
                  <tr>
                    <td width="350" colspan="2" align="center">
                      <table aligin="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblMedIssue" Text = "Medication Issue:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td>
                            
                                <asp:Label ID = "lblMedIssue1"  runat = "server" ></asp:Label>
                             
                          </td>
                        </tr>
                       
                        
                       
                       
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblMedIssueComment" Text = "Comment:" runat = "server" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left">
                            <asp:TextBox ID="txtMedIssueComment" runat="server" TextMode="multiLine" Width="180px"></asp:TextBox>
                          </td>
                        </tr>
                        
                        
                        <tr>
                          <td  align="center" colspan="2">
                           <asp:HiddenField ID="hfCallID" runat="server" />
                            
                                <asp:ImageButton ID="btnMedIssueSave" runat="server"  
                                  ImageUrl="../images/save.gif" border="0" onclick="btnMedIssueSave_Click1"  ></asp:ImageButton>
                             
                          </td>
                          
                        </tr>
                      </table>
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>
             <asp:Button ID="btnMedIssue" runat="server" Text="Button" Style="visibility:hidden" />
             <asp:Button ID="btnRxApproval" runat="server" Text="Button" Style="visibility:hidden" />
               <asp:Panel ID="pnlEmailCallLog" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
                <table width="350"   border="0" cellpadding="0" cellspacing="0">
                  <tr class="medication_info_th1" width="100%">
                    <td  height="30px" align="left">
                        E-Mail Call Log
                    </td>
                    <td align="right">
                      <asp:ImageButton ID="imgBtnCloseCallLog" runat="server"   ImageUrl="../images/close_popup.png" Height="16px" Width="16px" border="0" ToolTip="Close"></asp:ImageButton>
                    </td>
                  </tr>
                  <tr>
                    <td width="350" colspan="2" align="left">
                      <table width="100%" aligin="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                       <tr>
                          <td align="left">
                            <asp:Label ID = "Label5" Text = "From:" runat = "server" ></asp:Label>
                          </td>
                          <td> 
                          <asp:TextBox ID="txtEmailFrom" runat="server" Width="250"></asp:TextBox>
                          </td>
                        <tr>
                          <td align="left">
                            <asp:Label ID = "Label6" Text = "To:" runat = "server" ></asp:Label>
                          </td>
                          <td> 
                          <asp:TextBox ID="txtEmailTo" runat="server" Width="250"></asp:TextBox>
                          </td>
                        </tr>
                         <tr>
                          <td align="left">
                            <asp:Label ID = "Label7" Text = "Subject:" runat = "server" ></asp:Label>
                          </td>
                          <td> 
                          <asp:TextBox ID="txtEmailSubject" runat="server" Width="250"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left">
                            <asp:Label ID = "Label8" Text = "Message:" runat = "server" ></asp:Label>
                          </td>
                          <td> 
                          <asp:TextBox ID="txtEmailMessage" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                        <td colspan="2" align="center">
                        <asp:ImageButton ID="imgBtnSendEmail" runat="server" ImageUrl="../Images/ok.jpg" 
                                onclick="imgBtnSendEmail_Click" />
                        </td>
                        </tr>
                      
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>  
               <asp:Button ID="btnEmailCallLog" runat="server" Text="Call Log" Style="visibility:hidden" />
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

