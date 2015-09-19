<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="CSRHome.aspx.cs" Inherits="CSRHome" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<cc1:ModalPopupExtender ID="popMedIssue" runat="server" TargetControlID="btnMedIssue" PopupControlID="pnlMedIssue" CancelControlID="btnMedIssueCancel" BackgroundCssClass="modalBackground" BehaviorID="MedIssue"></cc1:ModalPopupExtender>
<cc1:ModalPopupExtender ID="popEmailCallLog" runat="server" TargetControlID="btnEmailCallLog" PopupControlID="pnlEmailCallLog" CancelControlID="imgBtnCloseCallLog" BackgroundCssClass="modalBackground" ></cc1:ModalPopupExtender>

<table width="100%" align="center">
<tr>
<td valign="top" width="60%">
<table width="100%" cellspacing="5">
    <tr><td><table width="100%">
<tr class="medication_info_th1">
    <td style="text-align: left; height:20px"><asp:Label ID="lblHeading" runat="server" Text="Scheduled Appointments for Today"></asp:Label></td>
</tr>
<tr>
    <td>
        <asp:GridView ID="gridAppointments" runat="server" 
                      AutoGenerateColumns="False"
                      Width="100%"   
                      AllowPaging="True" 
                      onpageindexchanging="gridAppointments_PageIndexChanging" 
                      PageSize="5"
                      EmptyDataText=" No Appointments Found...">
                         <Columns>
                            <asp:TemplateField HeaderText="Time" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                            <ItemTemplate>
                                <asp:Label ID="lblTime" runat="server" Text='<%#Eval("APPT_Time")%>'></asp:Label>
                            </ItemTemplate>   
                            
                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                         <asp:HyperLinkField HeaderText="Patient" ItemStyle-HorizontalAlign="left" 
                                 DataNavigateUrlFields="Pat_ID" 
                                 DataNavigateUrlFormatString="../Patient/AllPatientProfile.aspx?patID={0}" 
                                 DataTextField="PatientName"  > 
                             <ItemStyle HorizontalAlign="Left" />
                             </asp:HyperLinkField>
                       <asp:TemplateField HeaderText="Doctor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblMed" runat="server" Text='<%# Eval("DoctorName")%>' Font-Bold="false"></asp:Label> 
                           </ItemTemplate>
                         
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                       </asp:TemplateField>
                       
                       </Columns>
                        
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                       <EmptyDataRowStyle Font-Italic="true" HorizontalAlign="Center" BackColor="#eaf0f4" Height="20px"/>
                 </asp:GridView>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table width="100%">
<tr class="medication_info_th1">
<td style="text-align: left; height:20px">
    <asp:Label ID="Label2" runat="server" Text="Actions taken by Doctors"></asp:Label>
</td>
</tr>
<tr class="medication_info_tr-odd"">
<td style="text-align: left;height:20px">
    <asp:RadioButtonList ID="rbtnActions" runat="server" RepeatColumns="2" 
        AutoPostBack="True" onselectedindexchanged="rbtnActions_SelectedIndexChanged" 
        RepeatDirection="Horizontal" Font-Bold="true">
    <asp:ListItem Selected="True" Text="Pending" Value="P"></asp:ListItem>
    <asp:ListItem  Text="Action Taken" Value="A"></asp:ListItem>
    </asp:RadioButtonList>
</td>
</tr>
<tr>
<td valign="top">
<asp:GridView ID="gridActions" 
              runat="server" 
              AutoGenerateColumns="False" 
              Width="100%" AllowPaging="True" 
              PageSize="5"
              onpageindexchanging="gridActions_PageIndexChanging"
              EmptyDataText="No Actions Found..." 
        onrowdatabound="gridActions_RowDataBound">
                         <Columns>
                          <asp:HyperLinkField HeaderText="Patient" ItemStyle-HorizontalAlign="left" 
                                 DataNavigateUrlFields="Pat_ID" 
                                 DataNavigateUrlFormatString="../Patient/AllPatientProfile.aspx?patID={0}" 
                                 DataTextField="PatName"  > 
                             <ItemStyle HorizontalAlign="Left" Width="100px"/>
                             </asp:HyperLinkField>
                        
                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="left" ItemStyle-Width="60px">
                            <ItemTemplate>
                                <asp:Label ID="lblAction" runat="server" Text='<%#Eval("action")%>'></asp:Label>
                            </ItemTemplate>   
<ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblPatient_Name" runat="server" Text='<%#Eval("MedDesc")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        

<ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Doctor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                           <ItemTemplate>
                                <asp:Label ID="lblMed" runat="server" Text='<%# display_link((string) Eval("ApproverName"),(string) Eval("Doctor"))%>' Font-Bold="false"></asp:Label> 
                                                 
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Action Taken" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                           <ItemTemplate>
                                <asp:Label ID="lblMed" runat="server" Text='<%# Eval("Rx_Drug_Request_Status")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                       </asp:TemplateField>
                       </Columns>
                       
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                       <EmptyDataRowStyle Font-Italic="true" HorizontalAlign="Center" BackColor="#eaf0f4" Height="20px"/>
                 </asp:GridView>
</td>
</tr>
</table>
</td>
</tr>
    <tr>
    <td>
     <table style="width:100%">
  <tr class="medication_info_th1">
    <td align="left" height="25px" width="50%"><asp:Label ID="Label3" runat="server" Text="Pending Med-Line Issues"></asp:Label>
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
                                  onpageindexchanging="gridRxMedIssue_PageIndexChanging" 
                                  PagerSettings-Visible="false" 
                                  AllowPaging="true" 
                                  PageSize="5"  
                                  EmptyDataText="No Med Issues Found..."
                                  width="100%" 
                                  onrowcommand="gridRxMedIssue_RowCommand">
                         <Columns>
                                <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Rx_Date")%>'></asp:Label>
                                </ItemTemplate>                        
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
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
                               <ItemStyle HorizontalAlign="Left" Width="200px" />
                           </asp:TemplateField>
                           
                           <asp:TemplateField ItemStyle-Width="50px" HeaderText="Action" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate >
                                     <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Action" CommandArgument='<%# Eval("Call_ID")%>' Font-Bold="false">Action</asp:LinkButton>
                                </ItemTemplate>
                                 
                            </asp:TemplateField>
                               
                           </Columns>
                           <AlternatingRowStyle CssClass="medication_info_tr-even" />
                           <HeaderStyle CssClass="medication_info_th1" />
                           <PagerSettings Visible="False" />
                           <RowStyle CssClass="medication_info_tr-odd" />
                           <EmptyDataRowStyle Font-Italic="true" BackColor="#eaf0f4" HorizontalAlign="Center" Width="100%" Height="20px"/>
                     </asp:GridView>
     </td>
  </tr>
  <tr>
  <td>
  <table width="100%">
  <tr>
  <td><asp:LinkButton ID="btnPMI" runat="server" CommandArgument="0" onclick="btnPMI_Click" CommandName="Previous" >Previous</asp:LinkButton>
  </td>
  <td><asp:LinkButton ID="btnNMI" runat="server" CommandArgument="1" CommandName="Next" onclick="btnPMI_Click">Next</asp:LinkButton>
  </td>
  <td align="right"><a id="btnAllMI" runat="server" href="../Patient/MedIssueQueue.aspx">
      ALL</a>
</td>
</tr>
</table>
</td>
</tr>
</table>
    </td>
    </tr>
</table>
</td>
<td valign="top" width="40%">
<table width="100%" cellspacing="5">
    <tr><td><table width="100%">
        <tr class="medication_info_th1">
            <td style="text-align: left; height:20px">
                <asp:Label ID="Label1" runat="server" Text="Upcoming Events"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:GridView ID="gridEvents" 
                              runat="server" 
                              AutoGenerateColumns="False" 
                              onrowcreated="gridEvents_RowCreated" 
                              onrowdatabound="gridEvents_RowDataBound" 
                              ShowHeader="false" 
                              Width="100%" 
                              AllowPaging="True" 
                              onpageindexchanging="gridEvents_PageIndexChanging" 
                              PageSize="5"
                              EmptyDataText="No Events Found..." >
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="0px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEventID" runat="server" Font-Bold="false" 
                                    Text='<%#Eval("EventID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="0px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="EventDate" HeaderText="Date" 
                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%" >
                           </asp:BoundField>
                        <asp:TemplateField HeaderText="Event" ItemStyle-HorizontalAlign="Left" 
                            ItemStyle-Width="50%">
                            <ItemTemplate>
                                <asp:Label ID="lblEvent" runat="server" Font-Bold="false" 
                                    Text='<%#Eval("EventInfo")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="65%" />
                        </asp:TemplateField>
                    </Columns>
                  
                    <AlternatingRowStyle CssClass="medication_info_tr-even" />
                    <HeaderStyle CssClass="medication_info_th1" />
                    <RowStyle CssClass="medication_info_tr-odd" />
                    <EmptyDataRowStyle Font-Italic="true" HorizontalAlign="Center" BackColor="#eaf0f4" Height="20px"/>
                </asp:GridView>
            </td>
        </tr>
    </table>
</td></tr>
    <tr><td><table width="100%">
        <tr class="medication_info_th1">
            <td colspan="2" style="text-align: left; height:20px">
                <asp:Label ID="lblHeading2" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="medication_info_tr-odd">
            <td align="left" valign="top" width="50%">
                <asp:Label ID="lblNONP" runat="server" Text="No. of New Patients:"></asp:Label>
            </td>
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNONP1" runat="server" Font-Bold="false" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="medication_info_tr-even">
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNOP" runat="server" Text="No. of Prescriptions:"></asp:Label>
            </td>
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNOP1" runat="server" Font-Bold="false" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="medication_info_tr-odd">
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNOS" runat="server" Text="No. of Sample Rx:"></asp:Label>
            </td>
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNOS1" runat="server" Font-Bold="false" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="medication_info_tr-even">
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNOMR" runat="server" Text="No. of Med Requests:"></asp:Label>
            </td>
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNOMR1" runat="server" Font-Bold="false" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="medication_info_tr-odd">
            <td align="left" valign="top" width="50%">
                 
                <asp:Label ID="lblNOPC" runat="server" Text="No. of Patient Calls:"></asp:Label>
            </td>
            <td align="left" valign="top" width="50%">
                <asp:Label ID="lblNOPC1" runat="server" Font-Bold="false" Text=""></asp:Label>
            </td>
        </tr>
    </table></td></tr>
    <tr>
    <td>
    <div id="divEvt" runat="server"></div>
    </td>
    </tr>
    <tr>
    <td>
     <table width="100%">
        <tr class="medication_info_th1">
            <td style="text-align: left; height:20px">
                <asp:Label ID="lblHeading1" runat="server" Text="Announcements/Messages"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:GridView ID="gridMessages" 
                              runat="server" 
                              AutoGenerateColumns="False" 
                              ShowHeader="true" 
                              Width="100%" 
                              AllowPaging="True" 
                              HorizontalAlign="Left" 
                              onpageindexchanging="gridMessages_PageIndexChanging" 
                              PageSize="5"
                              EmptyDataText="No Announcements Found..." 
                    onrowdatabound="gridMessages_RowDataBound">
                    <Columns>
                     <asp:TemplateField HeaderText="Sr.No."  >
                            <ItemTemplate>
                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Eval("SrNo")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:Label ID="lblAnnDate" runat="server" Text='<%#Eval("Announcement_Date")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Message" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblMessages" runat="server" Text='<%#Eval("Message")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="65%"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ANNC. By" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblBy" runat="server" Text='<%#Eval("AnnouncementBy")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="20%"/>
                        </asp:TemplateField>
                    </Columns>
                     
                    <AlternatingRowStyle CssClass="medication_info_tr-even" />
                    <HeaderStyle CssClass="medication_info_th1" />
                    <RowStyle CssClass="medication_info_tr-odd" />
                    <EmptyDataRowStyle Font-Italic="true" HorizontalAlign="Center" BackColor="#eaf0f4" Height="20px"/>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </td>
    </tr>
</table>
</td>
</tr>
</table>
<asp:Panel ID="pnlMedIssue" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
                <table width="350"   border="0" cellpadding="0" cellspacing="0">
                  <tr class="medication_info_th1" width="100%">
                    <td  height="30px" align="left">
                        Med Issue
                    </td>
                    <td align="right">
                      <asp:ImageButton ID="btnMedIssueCancel" runat="server"  ImageUrl="../images/close_popup.png" Height="16px" Width="16px" border="0" ToolTip="Close"></asp:ImageButton>
                    </td>
                  </tr>
                  <tr>
                    <td width="350" colspan="2" align="center">
                      <table aligin="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblMedIssue" Text = "Medication Issue:" runat = "server" ></asp:Label>
                          </td>
                          <td>
                            
                                <asp:Label ID = "lblMedIssue1"  runat = "server" ></asp:Label>
                             
                          </td>
                        </tr>
                       
                        
                       
                       
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblMedIssueComment" Text = "Comment:" runat = "server" ></asp:Label>
                          </td>
                          <td align="left">
                            <asp:TextBox ID="txtMedIssueComment" runat="server" TextMode="multiLine" Width="180px"></asp:TextBox>
                          </td>
                        </tr>
                        
                        
                        <tr>
                          <td  align="center" colspan="2">
                           <asp:HiddenField ID="hfCallID" runat="server" />
                            
                                <asp:ImageButton ID="btnMedIssueSave" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btnMedIssueSave_Click"></asp:ImageButton>
                             
                          </td>
                          
                        </tr>
                      </table>
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>
                  <asp:Button ID="btnMedIssue" runat="server" Text="Button" Style="visibility:hidden" />
                  <asp:label ID="txtPrintTime" runat="server" visible="false"/>
                  
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
                            <asp:Label ID = "Label4" Text = "To:" runat = "server" ></asp:Label>
                          </td>
                          <td> 
                          <asp:TextBox ID="txtEmailTo" runat="server" Width="250"></asp:TextBox>
                          </td>
                        </tr>
                         <tr>
                          <td align="left">
                            <asp:Label ID = "Label6" Text = "Subject:" runat = "server" ></asp:Label>
                          </td>
                          <td> 
                          <asp:TextBox ID="txtEmailSubject" runat="server" Width="250"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td align="left">
                            <asp:Label ID = "Label7" Text = "Message:" runat = "server" ></asp:Label>
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

