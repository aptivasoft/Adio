<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="AnnouncementsList.aspx.cs" Inherits="AnnouncementsList" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>         
   <table align="center" width="100%">
        <tr class="medication_info_th1">
              <td align="center" style="height:25px;vertical-align:middle;"> 
              <asp:Label ID="lblHeading" runat="server" Text="Announcements"></asp:Label>
              </td>                   
        </tr>
        <tr class="medication_info_tr-odd">          
            <td align="left">
            <input type="button" value="Print" id="btnPrint" runat="Server" onclick="CallPrint('divPrint')" style=" background-color:#FDF5E6; border-color:Black; border-style:Solid; width:50px; border-width:1px"/>
            </td>
        </tr>
    
        <tr class="medication_info_tr-even">
            <td align="center">
                <asp:Panel ID="PnlGV" runat="server">
            
            <div id="divPrint">
            <asp:GridView ID="GVList" runat="server" Width="100%" 
                    AutoGenerateColumns="False" ondatabound="GVList_DataBound" 
                    AllowPaging = "True" onpageindexchanging="GVList_PageIndexChanging" 
                    PageSize="25" onrowediting="GVList_RowEditing" 
                    onrowupdating="GVList_RowUpdating" onrowdeleting="GVList_RowDeleting" 
                    onrowcancelingedit="GVList_RowCancelingEdit" 
                    onrowdatabound="GVList_RowDataBound" >
                       <EditRowStyle Font-Bold="False" />
                       <AlternatingRowStyle CssClass="medication_info_tr-even" HorizontalAlign = "Left"/>
                       <Columns>
                        <asp:CommandField ShowEditButton="True">
                            
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                           </asp:CommandField>
                           
                           <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/delete.gif" 
                               ShowDeleteButton="True"><ItemStyle HorizontalAlign="Center" Width="20px" />
                           </asp:CommandField>
                       <asp:TemplateField HeaderText="AnnouncementID" Visible = "false">
                           <ItemTemplate><asp:Label ID="lblAnnouncementID" runat="server" Text='<%# Eval("Announcement_ID")%>'></asp:Label> 
                            </ItemTemplate>                             
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Announcement Date" ItemStyle-Width = "150px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label ID="lblAnnDate" runat="server" Text='<%# Eval("Announcement_Date")%>'></asp:Label> 
                            </ItemTemplate> 
                            <EditItemTemplate><asp:TextBox ID="txtAnnDate" runat="server" Text='<%# Eval("Announcement_Date")%>'></asp:TextBox>
                            <cc1:CalendarExtender ID="Getdate" runat="server" TargetControlID="txtAnnDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtAnnDate" 
                        PopupPosition="BottomLeft" >
                        </cc1:CalendarExtender>      
                            </EditItemTemplate> 
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Message" ItemStyle-Width = "200px">
                           <ItemTemplate><asp:Label ID="lblMessage" runat="server" Text='<%# Eval("Message")%>'></asp:Label> 
                            </ItemTemplate> 
                            <EditItemTemplate><asp:TextBox ID="txtMessage" runat="server" Text='<%# Eval("Message")%>'></asp:TextBox> 
                            </EditItemTemplate> 
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Expiry Date" ItemStyle-Width = "150px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label ID="lblExpiryDate" runat="server" Text='<%# Eval("Expiry_Date")%>'></asp:Label> 
                            </ItemTemplate> 
                            <EditItemTemplate><asp:TextBox ID="txtExpiryDate" runat="server" Text='<%# Eval("Expiry_Date")%>'></asp:TextBox>
                            <cc1:CalendarExtender ID="GetExpirydate" runat="server" TargetControlID="txtExpiryDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtExpiryDate" 
                        PopupPosition="BottomLeft" >
                        </cc1:CalendarExtender>      
                            </EditItemTemplate> 
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Location" ItemStyle-Width = "150px">
                           <ItemTemplate><asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityName")%>'></asp:Label> 
                            </ItemTemplate>                             
                           </asp:TemplateField> 
                                                    
                           <asp:TemplateField HeaderText="Posted By" ItemStyle-Width = "150px">
                           <ItemTemplate><asp:Label ID="lblPostedBy" runat="server" Text='<%# Eval("PostedBy")%>'></asp:Label> 
                           </ItemTemplate> 
                           </asp:TemplateField>
                           

                
                       </Columns>
                       <HeaderStyle CssClass="medication_info_th1" />
                       <PagerSettings FirstPageText="First" LastPageText="Last" 
                           Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Previous"  />
                       <RowStyle CssClass="medication_info_tr-odd" HorizontalAlign = "Left" />
            </asp:GridView>
            </div>
                </asp:Panel>
            </td>
            
        </tr>
     </table>
 
    
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

