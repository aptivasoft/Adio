<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="RxReqPrint.aspx.cs" Inherits="RxReqPrint" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
 
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
            <%-- 
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
            --%>
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
                <asp:GridView ID="gridRxReqList" runat="server" AutoGenerateColumns="False" 
                     Width="100%"  OnRowCommand="gridRxReqList_RowCommand" EmptyDataText="No Records Found..." >
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
                       <asp:BoundField DataField="Rx_Date" HeaderText="Rx Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" HtmlEncode="False" />
                       
                        <asp:TemplateField HeaderText="Patient Name" ItemStyle-HorizontalAlign="Left">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblPatientName" runat="server" Text='<%# Eval("PatientName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Doctor"  ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:Label ID="lblDoctor" runat="server" Text='<%# Eval("doctorName")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                           

                        <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                       </asp:TemplateField>
                       
                       
             
                        <asp:TemplateField HeaderText="DrugName"  ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                            <asp:Label ID="lblDrugName" runat="server" Text='<%# Eval("Rx_DrugName")%>' Font-Bold="false"></asp:Label>
                             </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Quantity"  ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Rx_Qty")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left" Width="115px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Rx Type"  ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:Label ID="lblrxtype" runat="server" Text='<%# Eval("RxType")%> ' Font-Bold="false"></asp:Label>                        
                                <%-- <asp:LinkButton ID="hlstatus" runat="server" Text='<%# Eval("status")%>' CommandArgument='<%# Eval("Rx_Req_ID")+"-"+Eval("Status")%>' CommandName="ReportRxReq"> </asp:LinkButton> --%>
                            </ItemTemplate>

                        <ItemStyle HorizontalAlign="Left"  Width="50px"></ItemStyle>
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
    </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

