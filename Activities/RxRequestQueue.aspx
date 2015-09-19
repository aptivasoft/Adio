<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxRequestQueue.aspx.cs" Inherits="RxRequestQueue" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updateRxReqQueue" runat="server"  >
<ContentTemplate>

<table  align="center"  width="100%" class="patient_info" >
        <tr class="medication_info_th1">
             <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"> <asp:Label ID="lblHeading" runat="server" Text="Rx Request Queue"></asp:Label>
             </td>
        </tr>

        <tr class="medication_info_tr-odd">
          <td align="left" style="width:448px" ><asp:Label ID="lblsortby" runat="server" Text="Sort By :"></asp:Label>
              &nbsp;&nbsp;
            <asp:RadioButton ID="rbtnShowAll" runat="server" GroupName="pMsg" Text="Date" AutoPostBack="false"  Checked="true"/>
              &nbsp;&nbsp;
            <asp:RadioButton ID="rbtnFacility" runat="server" GroupName="pMsg" Text="Facility" AutoPostBack="false"  />
              &nbsp;&nbsp;
            <asp:RadioButton ID="rbtnDoctor" runat="server" GroupName="pMsg" Text="Doctor" AutoPostBack="false" />
              &nbsp;&nbsp;
            
            </td>
            
            <td align="left" style="width:125px" valign="middle">
            &nbsp;<asp:Label ID = "Label2" runat = "server" Text = "Rx Type :"></asp:Label>&nbsp;
                <asp:DropDownList ID="ddlRxType" runat="server">
                <asp:ListItem Text="ALL" Value="%" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Rx" Value="R"></asp:ListItem>
                <asp:ListItem Text="Sample" Value="S"></asp:ListItem>
                <asp:ListItem Text="PAP" Value="P"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" style="width:150px" >
            &nbsp;<asp:Label ID = "lblStatus" runat = "server" Text = "Status :"></asp:Label>&nbsp;
                <asp:DropDownList ID="ddlStatus" runat="server" style="width:100px" 
                    DataSource="<%# RxStatus.getRxStatus(2)%>" DataTextField="Status_Desc" 
                    DataValueField="Status_Code" >
                              
                </asp:DropDownList>
                 
            </td>
            <td align="left"  style="width:100px">
                 &nbsp;<asp:LinkButton runat="server" onclick="btnQueueList_Click" ID="btnQueueList"><asp:Image ID="imgView" ImageUrl="../Images/search_new.png" runat="server" Height="24px" Width="24px" ToolTip="View" /></asp:LinkButton>&nbsp;&nbsp;
                  <asp:ImageButton ID="btnRefreshQueueList" ImageUrl="../images/action_refresh.gif" runat="server" ToolTip="Refresh Queue List"
                    onclick="btnRefreshQueueList_Click" style="width: 16px;" /> 
                    &nbsp;
                    <a href="RxReqPrint.aspx" >
                    <img src="../Images/print.gif"  alt="Print Rx Requests" border="0"/></a> 
                
             </td>
</tr>
        <tr class="medication_info_tr-even">
          <td align="left" style="width:448px" colspan="7"><asp:Label ID="Label1" runat="server" Text="Filter By :"></asp:Label>
              &nbsp;&nbsp;
              
            <asp:RadioButton ID="rbtnFilterByDate" runat="server" GroupName="rbtnFilter" 
                  Text="Date" oncheckedchanged="rbtnFilterByDate_CheckedChanged" AutoPostBack="true" />
              &nbsp;&nbsp;
            <asp:RadioButton ID="rbtnFilterByFac"  runat="server" GroupName="rbtnFilter" 
                  Text="Facility" oncheckedchanged="rbtnFilterByFac_CheckedChanged" AutoPostBack="true" />
              &nbsp;&nbsp;
            <asp:RadioButton ID="rbtnFilterByDoc"  runat="server" GroupName="rbtnFilter" 
                  Text="Doctor" oncheckedchanged="rbtnFilterByDoc_CheckedChanged" AutoPostBack="true"/>
              &nbsp;&nbsp;
              </td>
            
         
</tr>
<tr class="medication_info_tr-odd" style="height:30px">
 <td  align="left" style="width:548px" colspan="3">

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
 <div id="pnlDateSelect"   style="height:40px; position:absolute;" runat="server" visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Start Date : &nbsp;&nbsp;
        <asp:TextBox ID="txtStartDate" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
        <cc1:CalendarExtender ID="ceStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="txtStartDate" Enabled="True" ></cc1:CalendarExtender>
        &nbsp;(MM/DD/YYYY)&nbsp;&nbsp;&nbsp;End Date : &nbsp;&nbsp;
        <asp:TextBox ID="txtEndDate" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
         <cc1:CalendarExtender ID="cdEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="txtEndDate" Enabled="True" ></cc1:CalendarExtender>
        &nbsp;&nbsp;&nbsp;(MM/DD/YYYY)&nbsp; &nbsp;<asp:Label ID="lblerror" runat="server" Text="Enter Proper Date Formats" Visible="false" ForeColor="Red"></asp:Label></div>
 <div id="pnlFacilitySelect" style ="height:40px; position:absolute;" runat="server" visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select Clinic: &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlFilterClinic" runat="server" 
            AutoPostBack="true" ondatabound="ddlFilterClinic_DataBound" 
            onselectedindexchanged="ddlFilterClinic_SelectedIndexChanged"></asp:DropDownList>
        &nbsp;Select Facility: &nbsp;&nbsp;
        <asp:DropDownList ID="ddlFilterFacility" runat="server" AutoPostBack="true" 
            ondatabound="ddlFilterFacility_DataBound" 
            onselectedindexchanged="ddlFilterFacility_SelectedIndexChanged"></asp:DropDownList>
    </div>
 <div id="pnlDoctorSelect" style="height:40px; position:absolute;" runat="server" visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Select Doctor: &nbsp;&nbsp;
        <asp:TextBox ID="txtFilterDoctor" runat="server" 
            ></asp:TextBox>
            
    </div> 
</td>
           <td align="left"  style="width:75px">
                 &nbsp;<asp:Label ID="lblcount" runat="server"></asp:Label></td>
</tr>
            <tr class="medication_info_tr-even">
            <td align="center" colspan="4">
<asp:UpdatePanel ID="updateRxQueue" runat="server" UpdateMode="Conditional">
<ContentTemplate>
      <asp:GridView   ID="gridRxRequestQueue" 
                                runat="server" 
                                AutoGenerateColumns="False" 
                                Width="100%" 
                                AllowSorting="true"  
                                OnRowCommand="gridRxRequestQueue_RowCommand" 
                                EmptyDataText="No Records Found..." 
                                AllowPaging="True" 
                                onpageindexchanging="gridRxRequestQueue_PageIndexChanging" 
                                PageSize="25" 
                                onrowdatabound="gridRxRequestQueue_RowDataBound" >
                       <Columns>
                            <asp:TemplateField HeaderText="Clinic" ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>'></asp:Label>
                            </ItemTemplate>                        

                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Left"  Visible="false" >
                            <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblFacility_Name" runat="server" Text='<%#Eval("Facility_Name")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        

                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                       </asp:TemplateField>
                       <%--<asp:TemplateField HeaderText="Rx Date" ItemStyle-HorizontalAlign="center"  >
                            <ItemTemplate>
                                <asp:Label ID="lblRxDate" runat="server" Text='<%#Eval("Rx_Date")%>'   Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        

                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                       </asp:TemplateField>--%>
                       <asp:BoundField DataField="Rx_Date" HeaderText="Rx Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HtmlEncode="False" />
                       
                       <asp:HyperLinkField  HeaderText="Patient" 
                                ItemStyle-HorizontalAlign="left" DataNavigateUrlFields="Pat_ID" 
                                DataNavigateUrlFormatString="../Patient/AllPatientProfile.aspx?patID={0}" 
                                DataTextField="PatientName"  >
                        <ControlStyle ></ControlStyle>

                        <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                            </asp:HyperLinkField>
                       <asp:TemplateField HeaderText="Doctor"  ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:Label ID="lblDoctor" runat="server" Text='<%# Eval("doctorName")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                       </asp:TemplateField>
             
                                 <asp:TemplateField HeaderText="Request By"  ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRxReqBy" runat="server" Text='<%# Eval("Rx_ApprovedBy")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="RxType"  ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Label ID="lblRxType" runat="server" Text='<%# Eval("Rx_Type")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                       </asp:TemplateField>

                       <%-- <asp:HyperLinkField  HeaderText="DrugName" 
                                ItemStyle-HorizontalAlign="left" DataNavigateUrlFields="Rx_Req_ID" 
                                DataNavigateUrlFormatString="RxApproval.aspx?RxRequestID={0}" 
                                DataTextField="Rx_Drugname"  
                                Target="_blank">
                        <ControlStyle ></ControlStyle>
                        
                        <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                            </asp:HyperLinkField> --%>
                            
                        <asp:TemplateField HeaderText="DrugName"  ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                            <asp:LinkButton ID="hlDrugName" runat="server" Text='<%# Eval("Rx_DrugName")%>' CommandArgument='<%# Eval("Rx_Req_ID")%>' CommandName="RXApproval"></asp:LinkButton>
                             </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                       </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Comments"  ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:Label ID="lblcomments" runat="server" Text='<%# Eval("Rx_Request_Comments")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left" Width="225px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <%--<asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status")%> ' Font-Bold="false"></asp:Label>  --%>                         
                                <asp:LinkButton ID="hlstatus" runat="server" Text='<%# Eval("status")%>' CommandArgument='<%# Eval("Rx_Req_ID")+"-"+Eval("Status")%>' CommandName="ReportRxReq"> </asp:LinkButton>
                            </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center"  Width="50px"></ItemStyle>
                       </asp:TemplateField>
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <PagerSettings PageButtonCount="20" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                  
 </ContentTemplate>
</asp:UpdatePanel>


                 </td>
            </tr>
            </table>
 <cc1:ModalPopupExtender ID="mpeRxApproval" runat="server"  TargetControlID="btnRxApproveAlias" PopupControlID="pnlApproveMedReuest" 
        CancelControlID="imgRxApproveClose" BackgroundCssClass="modalBackground" 
          Enabled="True" PopupDragHandleControlID="pnlEditRx" ></cc1:ModalPopupExtender>
          
<input type="button"  ID="btnRxApproveAlias" runat="server" style="visibility:hidden"/>
            
<asp:Panel ID="pnlApproveMedReuest" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB"  >
  <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" BehaviorID="acCustodianEx1" 
                TargetControlID="txtDocName" UseContextKey="true" runat="server"  
                MinimumPrefixLength="1"  Enabled="true" ServiceMethod="GetDoctorNames" 
                OnClientItemSelected="AutoCompleteSelected_Doctor" >
           </cc1:AutoCompleteExtender>
  <cc1:AutoCompleteExtender   ID="AutoCompleteExtender1" 
         BehaviorID="acCustodianEx2" TargetControlID ="txtSubDrugName" 
         UseContextKey="True" runat="server"  MinimumPrefixLength="1"  Enabled="True"  
         ServiceMethod="GetMedicationNames" DelimiterCharacters="" ServicePath=""></cc1:AutoCompleteExtender>

    <table width="400" border="0" cellpadding="0" cellspacing="0">
    <tr class="medication_info_th1">
       <td align="left" height="25px">Rx Approval</td>
       <td align="right" height="25px" ><asp:ImageButton ID="imgRxApproveClose" runat="server"  ImageUrl="../images/close_popup.png" Height="16px" Width="16px" border="0" ToolTip="Close"></asp:ImageButton></td>
    </tr>
    <tr>
    <td width="400"  colspan="2"  align="center">
    <table align="center" class="medication_info_popup" cellpadding="0" cellspacing="1" width="100%">
  
        <tr>
         <td align = "left" >
                    <asp:Label ID="lblPatName" runat="server" Text="Patient Name :" Font-Bold="true"></asp:Label>
               </td>
               <td align = "left" >
                    <asp:Label ID="lblpatientName" runat="server"></asp:Label>
               </td>
        </tr>  
        <tr>
            <td align = "left">
            <asp:Label ID="lblPatcon" runat="server" Text="Patient Contact :" Font-Bold="true"></asp:Label>
            </td>
            <td align = "left">
            <asp:Label ID="lblPatContact" runat="server" ></asp:Label>
            </td>
        </tr> 
        <tr>
       
            <td align="left" >
                <asp:Label ID="lblDoctor" runat="server" Text="Doctor Name :" Font-Bold="true"></asp:Label>
            </td>
            <td  align="left" >   
                
               <asp:TextBox ID="txtDocName" runat="server" Text="" Width="200px"></asp:TextBox>
            </td>
          </tr> 
        <tr>
       
            <td align="left" >
                <asp:Label ID="lblDrug" runat="server" Text="Drug Name :" Font-Bold="true"></asp:Label>
            </td>
            <td  align="left" align="left">   
                
               <asp:TextBox ID="txtDrug" runat="server" Text="" Width="200px"></asp:TextBox>
            </td>
          </tr>
       
   
          <tr>
                        <td align="left" >
                            <asp:Label ID="lblQty" runat="server" Text="Qty :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                           <asp:TextBox ID="txtQuantity" runat="server" TabIndex="1" Width="50px"></asp:TextBox>
                        </td>
                        </tr>
        <tr>
          <td align="left" >
                                <asp:Label ID="lblsig" runat="server" Text="SIG :" Font-Bold="true"></asp:Label>
                            </td>
                            <td  align="left">
                                <asp:TextBox ID="txtSIG" runat="server" TabIndex="2" Width="200px" 
                                   ></asp:TextBox>
                            </td>
                            </tr>
        <tr>
           <td ><asp:Label ID="lblPharmacy" runat="server" Text="Pharmacy :" Font-Bold="true"></asp:Label></td>       
           <td colspan="2">
               <asp:RadioButtonList ID="rbtnPhrm" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true"
                   onselectedindexchanged="rbtnPhrm_SelectedIndexChanged">
               <asp:ListItem Text="ADiO" Value="0"></asp:ListItem>
               <asp:ListItem Text="Other" Value="1" ></asp:ListItem>
               </asp:RadioButtonList> 
             
               <asp:TextBox ID="txtPharmacy" runat="server" Width="200px" Visible="false"></asp:TextBox>
            </td>       
       </tr>
        <tr>
     
                                <td align="left" >
                                    <asp:Label ID="lblRefills" runat="server" Text="Refills :" Font-Bold="true"></asp:Label>
                                </td>
                                <td  align="left">
                                    <asp:DropDownList ID="ddlRefills" runat="server">
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

                                    </asp:DropDownList>
                                </td>
                            </tr>
        <tr>
       <td ><asp:Label ID="lblRxType" runat="server" Text="Type :" Font-Bold="true"></asp:Label></td>       
       <td >
       <asp:RadioButtonList ID="rbtnRequestType" runat="server" RepeatColumns="4">
           </asp:RadioButtonList>
           
       </td>
       </tr>
        <tr>
       <td ><asp:Label ID="lblDecision" runat="server" Text="Decision :" Font-Bold="true"></asp:Label></td>       
       <td >
       <asp:RadioButtonList ID="rbtnDecision" runat="server" RepeatColumns="4" 
               onselectedindexchanged="rbtnDecision_SelectedIndexChanged" AutoPostBack="true">
           <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
           <asp:ListItem Text="Denied" Value="R"></asp:ListItem>
           <asp:ListItem Text="Pending" Value="N"></asp:ListItem>
       </asp:RadioButtonList>
           
       </td>
       </tr>
           <tr>
                <td align="left" colspan="2">
                   <asp:CheckBox ID="chkSubDrugName" runat="server" AutoPostBack="true" 
                        Text="Substitute Drug Name? &nbsp;&nbsp;&nbsp;(For Approved Rx only)"  
                        Enabled="false" Font-Bold="true" 
                        oncheckedchanged="chkSubDrugName_CheckedChanged"/> 
                </td>
           </tr>
                   <tr>
          <td align="left" colspan="2">
          <asp:Panel ID="pnlSubDrugName" runat="server" Visible="false">
          <table>
          <td>
           <asp:Label ID="lblSubDrugName" runat="server" Text="Substitute Drug Name:" Font-Bold="true"></asp:Label>
          </td>
          <td align="left" >
             <asp:TextBox ID="txtSubDrugName" runat="server" TabIndex="1" Width="200px"></asp:TextBox>
          </td>       
          </table>
          </asp:Panel>
           </td>
           </tr>
        <tr>
        <td>
      <asp:Label ID="lblRxDoc" runat="server" Text="Attach Document :" Font-Bold="true"></asp:Label>
      </td>
      <td >
 
      <asp:FileUpload ID="FileUpRxDoc" runat="server"/><br /> <b>(For Approved Rx only)</b>
 
      </td>
      </tr>

        <tr>
        <td><asp:Label ID="lblComments" runat="server" Text="Comments :" Font-Bold="true"></asp:Label>
        </td>       
        <td><asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" Width="200px"></asp:TextBox>
        </td>        
        </tr>
 
        <tr>
          <td colspan="4" align="center" >
          <asp:ImageButton ID="btnSave" runat="server" border="0" 
                  ImageUrl="~/images/save.gif" onclick="btnSave_Click" style="height: 24px"/> 
          <asp:HiddenField id="hidRxReqID" runat="server" />
           <input type="hidden" id="hidDocID" runat="server" />
          </td>
        </tr>
        <tr>
        <td colspan="2" align="center">&nbsp;&nbsp;<asp:Label ID="lblMsg" Text="" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    </td>
    </tr>   
    </table>
      
  </asp:Panel>
  
 </ContentTemplate>
  <Triggers>
      <asp:PostBackTrigger ControlID="btnSave"/>
      </Triggers>
</asp:UpdatePanel>       
 
</asp:Content>