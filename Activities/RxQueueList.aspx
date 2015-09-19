<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxQueueList.aspx.cs" Inherits="RxQueueList" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <cc1:ModalPopupExtender ID="MPE_SamplePAPProcessing" runat="server" TargetControlID="btnSampleProcessing"  PopupControlID="pnlSamplePAPProcessing" CancelControlID="btnProcessingCancel" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>

 <%--            <asp:UpdatePanel ID="UpdatePanelRxQueueList" runat="server">
            <ContentTemplate>
--%> 
     <table  align="center"  width="1000px" class="patient_info">
    
        <tr class="medication_info_th1">
            <td align="center"  colspan="6" style="height:25px;vertical-align:middle;">
             <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="medication_info_tr-odd">
            <td align="left" style="width:270px" >

                <asp:Label ID="lblOrganization" runat="server" Text="Organization :"></asp:Label>
                &nbsp;&nbsp;<asp:DropDownList
                    ID="ddlOrganization" runat="server" 
                    OnDataBound="ddlOrganization_DataBound" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrganization_SelectedIndexChanged" style="width:180px"></asp:DropDownList>
                    </td>
            <td align="left" style="width:240px"  >&nbsp;
                    <asp:Label ID="lblLocation" runat="server" Text="Location :"></asp:Label>&nbsp;&nbsp;<asp:DropDownList
                    ID="ddlLocation" runat="server" OnDataBound="ddlLocation_DataBound" 
                    onselectedindexchanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True" style="width:160px"></asp:DropDownList>
                
            </td>
            <td align="left" style="width:80px" >&nbsp;
                <asp:DropDownList ID="ddlRxType" runat="server">
                <asp:ListItem Text="Rx" Value="R" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Sample" Value="S"></asp:ListItem>
                <asp:ListItem Text="PAP" Value="P"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" style="width:150px" >&nbsp;<asp:Label ID = "lblStatus" runat = "server" Text = "Status :"></asp:Label>&nbsp;
                <asp:DropDownList ID="ddlStatus" runat="server" style="width:100px" 
                    DataSource="<%# RxStatus.getRxStatus(1)%>" DataTextField="Status_Desc" 
                    DataValueField="Status_Code" ondatabound="ddlStatus_DataBound">
                              
                </asp:DropDownList>
                 
            </td>
            
           <td align="left" style="width:180px" >
               &nbsp;<asp:Label ID="lblDate" runat="server" Text="Select Date :"></asp:Label>&nbsp;&nbsp;<asp:TextBox
                ID="txtDate" runat="server" Width="60px" ></asp:TextBox><asp:Image ID="Image1" runat="server" ImageUrl="../images/Calendar.png" />
               &nbsp;
<cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" PopupButtonID="Image1"
    TargetControlID="txtDate">
</cc1:CalendarExtender>
            </td>
             <td align="left"  style="width:75px">
                 &nbsp;<asp:LinkButton 
                    runat="server" onclick="btnQueueList_Click" ID="btnQueueList"><asp:Image ID="imgView" ImageUrl="../Images/search_new.png" runat="server" Height="24px" Width="24px" ToolTip="View" /></asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton 
                    runat="server" onclick="btnPrint_Click" ID="btnPrint" OnClientClick="return PrintRxList();"><asp:Image ID="imgPrint" ImageUrl="../Images/print.gif" runat="server" Height="16px" Width="16px" ToolTip="Print"/></asp:LinkButton>
                    <asp:LinkButton 
                    runat="server" onclick="btnPrint_Click" ID="btnAdminPrint" ><asp:Image ID="imgAdminPrint" ImageUrl="../Images/print.gif" runat="server" Height="16px" Width="16px" ToolTip="Print"/></asp:LinkButton>
                      
                    </td>
              </tr>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="6" >
                <asp:GridView ID="gridRxQueueList" runat="server" AutoGenerateColumns="False" 
                     Width="100%"  OnRowCommand="gridRxQueueList_RowCommand" EmptyDataText="No Records Found..." >
                       <Columns>
                       <asp:TemplateField HeaderText="Test" Visible="false">
                       <ItemTemplate>
                        </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clinic" ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>'></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Left"  Visible="false">
                            <ItemTemplate>
                                &nbsp;&nbsp;<asp:Label ID="lblFacility_Name" runat="server" Text='<%#Eval("Facility_Name")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Right"  Visible="false">
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDoctorName" runat="server" Text='<%# Eval("DoctorName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Patient Name" ItemStyle-HorizontalAlign="Left">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblPatientName" runat="server" Text='<%# Eval("PatientName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Drug Name" ItemStyle-HorizontalAlign="left" >
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;<asp:Label ID="lblDrugName" runat="server" Text='<%# Eval("Rx_DrugName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Rx Type"  ItemStyle-HorizontalAlign="Left" >
                          
                            <ItemTemplate>
                                <asp:Label ID="lblRxType" runat="server" Text='<%# Eval("RxType")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Quantity"  ItemStyle-HorizontalAlign="Right" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Rx_Qty")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="User"  ItemStyle-HorizontalAlign="center" ItemStyle-Width="150px" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblUser" runat="server" Text='<%# Eval("User")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Time"  ItemStyle-HorizontalAlign="center" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRxDate" runat="server" Text='<%# Eval("Rx_Date")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Left" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRxStatus" runat="server" Text='<%# Eval("Status")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Process">
                           
                            <ItemTemplate> 
                                <asp:LinkButton ID="btnprocess" runat="server" CommandName="Processing"  CommandArgument='<%# Eval("Rx_ItemID")%>' Visible='<%# display_link((string) Eval("RxType"),(string) Eval("Status"))%>'   Font-Bold="false" >Process</asp:LinkButton>
                                  
                            </ItemTemplate>
                            
                            <ItemStyle HorizontalAlign="Left" />
                            
                       </asp:TemplateField>
                                         
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 </td>
            </tr>
            </table>
             <asp:label ID="txtPrintTime" runat="server" visible="false"/>
            <input id="btnConfirm" type="hidden" value="button"  onclick="needConfirm();" runat="server"/>  
                  <asp:Button id="hidConfirm" runat="server"  Style="visibility:hidden" onclick="hidConfirm_Click"/>  
                   
 <%--                            
</ContentTemplate>
</asp:UpdatePanel> 

--%> 

         <asp:Button ID="btnSampleProcessing" runat="server" Text="Button" Style="visibility:hidden" />

         <asp:Panel ID="pnlSamplePAPProcessing" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" ><table width="350"   border="0" cellpadding="0" cellspacing="0">
                                                  <tr class="medication_info_th1" width="100%">
                                                      <td colspan="2"  height="30px" align="left">
                                                          &nbsp;&nbsp;Sample/PAP Rx Processing </td><td align="right"><asp:ImageButton ID="btnProcessingCancel" runat="server"  ImageUrl="../images/close_popup.png" border="0" Width="16px" Height="16px"></asp:ImageButton>
        </td></tr><tr  width="100%">
                                                      <td colspan="2"  height="0px" align="left"></td>
                                                      <td 
                                                  align="left" colspan="2" height="0px"></td></tr>
                                <tr  width="100%">
                                <td colspan="2"  height="0px" align="left">
                                        </td>
                                <td align="left">
                                    <asp:Label ID = "lblProcesingAlert" Text = "more than 3 Samples issued" runat = "server"  ForeColor="Red" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                    <tr><td width="350" colspan="3" align="center"><table align="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                                            <tr ><td align="left">
                                                <asp:Label ID = "lblProcessingType" Text = "Type:" runat = "server" ></asp:Label>
        </td><td colspan="2" align="left"><asp:RadioButtonList ID="rbtnProcessingType" runat="server" RepeatColumns="2"><asp:ListItem Text="PAP" Value="P"></asp:ListItem>
        <asp:ListItem Text="Sample" Value="S"></asp:ListItem>
                                                    </asp:RadioButtonList>
        </td></tr>
                                    <tr ><td align="left">
                                        <asp:Label ID = "lblProcessingDrug" Text = "Drug Name & Strength:" runat = "server" ></asp:Label>

        </td><td colspan="2" align="left"><asp:Label ID = "lblProcessingDrug1"  runat = "server" ></asp:Label>

        </td></tr><tr ><td align="left"><asp:Label ID = "lblQtyinStock" Text = "Qty in Stock:" runat = "server" ></asp:Label>

        </td><td colspan="2" align="left"><asp:Label ID = "lblQtyinStock1"  runat = "server" ></asp:Label>

        </td></tr>
                                    <tr><td align="left" >
                                        <asp:Label ID = "lblQtyProcessed" Text = "Qty Processed:" runat = "server" ></asp:Label>

        </td><td align="left" height="25px" colspan="2"><asp:TextBox ID="txtQtyProcessed" runat="server" Width="50px"></asp:TextBox>

        </td></tr>
                                    <tr><td align="left" >
                                        <asp:Label ID = "lblLotNum" Text = "Lot #:" runat = "server" ></asp:Label>

        </td><td align="left" height="25px" colspan="2"><asp:TextBox ID="txtLotNum" runat="server" Width="100px"></asp:TextBox>

        </td></tr>
                                    <tr><td align="left" >
                                        <asp:Label ID = "lblExpiryDate" Text = "Expiry Date:" runat = "server" ></asp:Label>

        </td><td align="left" height="25px" colspan="2"><asp:TextBox ID="txtExpiryDate" runat="server" Width="100px"></asp:TextBox>
                                            <asp:Label ID="lblExpFormat" Text="(MM/DD/YYYY)" runat="server" Visible="False"></asp:Label>

         <cc1:CalendarExtender ID="CE_ExpiryDate" runat="server" TargetControlID="txtExpiryDate" 
                                          PopupButtonID="txtExpiryDate" Enabled="True"></cc1:CalendarExtender>

        </td></tr>
                                    
                                    <tr><td align="left"  >
                                        <asp:Label ID = "lblProcessingStatus" Text = "Status:" runat = "server" ></asp:Label>

        </td><td align="left" height="25px"  colspan="2"><asp:DropDownList ID="ddlProcessingStatus" runat="server">
                                            <asp:ListItem Text="Filled" Value="F"></asp:ListItem>
        <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
        <asp:ListItem Text="Partial" Value="P"></asp:ListItem>
                                            </asp:DropDownList>

        </td></tr>
                                    <tr><td align="left" >
                                        <asp:Label ID = "lblProcessingComments" Text = "Comments:" runat = "server" ></asp:Label>

        </td><td align="left" colspan="2"><asp:TextBox ID="txtProcessingComments" runat="server" TextMode="MultiLine" 
                                          Width="180px"></asp:TextBox>

        </td>
        </tr>
        <tr>
          <td colspan="2" align="center">
             <asp:ImageButton ID="btn_Process_Save" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btn_Process_Save_Click" OnClientClick="return AlertProcess();" ></asp:ImageButton>
          </td>
        </tr>
        <tr>
        <td colspan="2" align="center">

        <asp:HiddenField ID="hfPatId" runat="server" />

        <asp:HiddenField ID="hidDocID" runat="server" />

        <asp:HiddenField ID="hfRXItemID" runat="server" />

        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table></asp:Panel>
  
<%-- <div id="div1"  style="position:absolute;border:solid black 0px;top:25%;right:25%;bottom:25%;left:25%;padding:25px; margin:25px;">
	 <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelRxQueueList" DisplayAfter="0">
       <ProgressTemplate>
          <div class="modalBackground"> 
           <table>
           <tr>
           <td class="label" valign="middle" align="center">
                 Processing...Please Wait..!
           </td>
           </tr>
           </table>
           </div>
       </ProgressTemplate>
   </asp:UpdateProgress>
</div> --%>

</asp:Content>

