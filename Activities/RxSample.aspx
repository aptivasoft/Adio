<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxSample.aspx.cs" Inherits="RxSample" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <cc1:ModalPopupExtender ID="MPE_SamplePAPProcessing" runat="server" TargetControlID="btnSampleProcessing"  PopupControlID="pnlSamplePAPProcessing" CancelControlID="btnProcessingCancel" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
                
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
                 <td align="center"  colspan="3" style="height:25px;vertical-align:middle;" class="medication_info_th1" > <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label></td>
                 <td align="center"  colspan="1" style="height:25px;vertical-align:middle;"  class="medication_info_th1">
                    <asp:LinkButton runat="server" onclick="btnPrint_Click" ID="btnPrint" ><asp:Image ID="imgAdminPrint" ImageUrl="../Images/print.gif" runat="server" Height="16px" Width="16px" ToolTip="Print"/></asp:LinkButton>
                 </td>
            </tr>
           
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxSampleList" runat="server" AutoGenerateColumns="False" 
                     Width="800px" EmptyDataText="No Records Found..."  OnRowCommand="gridRxSampleList_RowCommand" >
                       <Columns>
                            <%--<asp:TemplateField HeaderText="Clinic" ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>'></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Left"  Visible="false">
                            <ItemTemplate>
                                &nbsp;&nbsp;<asp:Label ID="lblFacility_Name" runat="server" Text='<%#Eval("Facility_Name")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Right"  Visible="false">
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDoctorName" runat="server" Text='<%# Eval("DoctorName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Patient Name" ItemStyle-HorizontalAlign="Right"  Visible="false">
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPatientName" runat="server" Text='<%# Eval("PatientName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Patient Name" ItemStyle-HorizontalAlign="left" >
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDrugName" runat="server" Text='<%# Eval("Rx_DrugName")%>' Font-Bold="false"></asp:Label>                       
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
                       <asp:TemplateField HeaderText="User"  ItemStyle-HorizontalAlign="left" ItemStyle-Width="150px" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblUser" runat="server" Text='<%# Eval("User")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Time"  ItemStyle-HorizontalAlign="center" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRxDate" runat="server" Text='<%# Eval("Rx_Date")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Order Status"  ItemStyle-HorizontalAlign="Left" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                         <asp:TemplateField >
                           
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Processing" CommandArgument='<%# Eval("Rx_ItemID")%>' Visible='<%# display_link((string) Eval("Status"))%>'  Font-Bold="false">Process</asp:LinkButton>
                               
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
            <asp:Button ID="btnSampleProcessing" runat="server" Text="Button" Style="visibility:hidden" />
               <asp:Panel ID="pnlSamplePAPProcessing" runat="server" BorderColor="Black" BorderWidth="1px"  BackColor="#FBFBFB" >
                <table width="350"   border="0" cellpadding="0" cellspacing="0">
                  <tr class="medication_info_th1" width="100%">
                    <td colspan="2"  height="30px" align="left">
                        &nbsp;&nbsp;Sample/PAP Rx Processing
                    </td>
                    <td align="right">
                      <asp:ImageButton ID="btnProcessingCancel" runat="server"  ImageUrl="../images/CloseRx.gif" border="0"></asp:ImageButton>
                    </td>
                  </tr>
                   <tr  width="100%">
                    <td colspan="2"  height="0px" align="left">
                        </td>
                    <td align="left">
                      <asp:Label ID = "lblProcesingAlert" Text = "more than 3 Samples issued" runat = "server"  ForeColor="Red" Visible="False"></asp:Label>
                    
                    </td>
                  </tr>
                  <tr>
                    <td width="350" colspan="3" align="center">
                      <table align="center" class="medication_info_popup" cellpadding="0" cellspacing="1">
                      <tr >
                          <td align="left">
                            <asp:Label ID = "lblProcessingType" Text = "Type:" runat = "server" ></asp:Label>
                          </td>
                          <td colspan="2" align="left">
                            
                              <asp:RadioButtonList ID="rbtnProcessingType" runat="server" RepeatColumns="2">
                              <asp:ListItem Text="PAP" Value="P"></asp:ListItem>
                              <asp:ListItem Text="Sample" Value="S"></asp:ListItem>
                              </asp:RadioButtonList>
                             
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblProcessingDrug" Text = "Drug Name & Strength:" runat = "server" ></asp:Label>
                          </td>
                          <td colspan="2" align="left">
                                <asp:Label ID = "lblProcessingDrug1"  runat = "server" ></asp:Label>
                          </td>
                        </tr>
                        <tr >
                          <td align="left">
                            <asp:Label ID = "lblQtyinStock" Text = "Qty in Stock:" runat = "server" ></asp:Label>
                          </td>
                          <td colspan="2" align="left">
                                <asp:Label ID = "lblQtyinStock1"  runat = "server" ></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblQtyProcessed" Text = "Qty Processed:" runat = "server" ></asp:Label>
                          </td>
                          <td align="left" height="25px" colspan="2">
                              <asp:TextBox ID="txtQtyProcessed" runat="server" Width="50px"></asp:TextBox>
                          </td>
                        </tr>
                         <tr><td align="left" ><asp:Label ID = "lblLotNum" Text = "Lot #:" runat = "server" ></asp:Label></td><td align="left" height="25px" colspan="2"><asp:TextBox ID="txtLotNum" runat="server" Width="100px"></asp:TextBox></td></tr>
                            <tr><td align="left" ><asp:Label ID = "lblExpiryDate" Text = "Expiry Date:" runat = "server" ></asp:Label></td><td align="left" height="25px" colspan="2"><asp:TextBox ID="txtExpiryDate" runat="server" Width="100px"></asp:TextBox> <cc1:CalendarExtender ID="CE_ExpiryDate" runat="server" TargetControlID="txtExpiryDate" 
                                  PopupButtonID="txtExpiryDate" Enabled="True"></cc1:CalendarExtender></td></tr>
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblProcessingStatus" Text = "Status:" runat = "server" ></asp:Label>
                          </td>
                          <td align="left" height="25px"  colspan="2">
                              <asp:DropDownList ID="ddlProcessingStatus" runat="server">
                              <asp:ListItem Text="Filled" Value="F"></asp:ListItem>
                              <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
                              <asp:ListItem Text="Partial" Value="P"></asp:ListItem>
                              </asp:DropDownList>
                          </td>
                        </tr>
                       
                        <tr>
                          <td align="left" >
                            <asp:Label ID = "lblProcessingComments" Text = "Comments:" runat = "server" ></asp:Label>
                          </td>
                          <td align="left" colspan="2">
                            <asp:TextBox ID="txtProcessingComments" runat="server" TextMode="multiLine" Width="180px"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                        <td colspan="2" align="center">
                        <asp:HiddenField ID="hfRXItemID" runat="server" />
                        <asp:HiddenField ID="hfPatId" runat="server" />
                         <asp:ImageButton ID="btn_Process_Save" runat="server"  ImageUrl="../images/save.gif" border="0" OnClick="btn_Process_Save_Click" ></asp:ImageButton>
                         </td>
                        </tr>
                        
                      </table>
                    </td>
                   
                  </tr>
                </table>
              </asp:Panel>
</asp:Content>

