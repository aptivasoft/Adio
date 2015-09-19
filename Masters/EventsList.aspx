<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="EventsList.aspx.cs" Inherits="Events_List" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>         
   <table align="center" width="100%">
        <tr class="medication_info_th1">
              <td align="center" style="height:25px;vertical-align:middle;"> 
              <asp:Label ID="lblHeading" runat="server" Text="Events List"></asp:Label>
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
                       <asp:TemplateField HeaderText="EventID" Visible = "false">
                           <ItemTemplate><asp:Label ID="lblEventID" runat="server" Text='<%# Eval("EventID")%>'></asp:Label> 
                            </ItemTemplate>                             
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Facility Name">
                           <ItemTemplate><asp:Label ID="lblFacility" runat="server" Text='<%# Eval("FacilityName")%>'></asp:Label> 
                            </ItemTemplate>                             
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Event Date" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label ID="lblEventDate" runat="server" Text='<%# Eval("EventDate")%>'></asp:Label> 
                            </ItemTemplate> 
                            <EditItemTemplate><asp:TextBox ID="txtEventDate" runat="server" Text='<%# Eval("EventDate")%>'></asp:TextBox>
                            <cc1:CalendarExtender ID="Getdate" runat="server" TargetControlID="txtEventDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtEventDate" 
                        PopupPosition="BottomLeft" >
                        </cc1:CalendarExtender>      
                            </EditItemTemplate> 
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Event Time" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:Label ID="lblEventTime" runat="server" Text='<%# Eval("EventTime")%>'></asp:Label> 
                            </ItemTemplate> 
                            <EditItemTemplate>
                            <asp:DropDownList ID="ddlHrs" runat="server" Width="50px" Text='<%# Eval("hTime")%>' >
                            <asp:ListItem>0</asp:ListItem><asp:ListItem>1</asp:ListItem><asp:ListItem>2</asp:ListItem><asp:ListItem>3</asp:ListItem><asp:ListItem>4</asp:ListItem><asp:ListItem>5</asp:ListItem><asp:ListItem>6</asp:ListItem><asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem><asp:ListItem>9</asp:ListItem><asp:ListItem>10</asp:ListItem><asp:ListItem>11</asp:ListItem><asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList> 
                         <asp:DropDownList ID="ddlMins" runat="server" Width="50px" Text='<%# Eval("mTime")%>' >
                            <asp:ListItem>0</asp:ListItem><asp:ListItem>15</asp:ListItem><asp:ListItem>30</asp:ListItem><asp:ListItem>45</asp:ListItem>
                         </asp:DropDownList> 
                           <asp:DropDownList ID="ddlAmPm" runat="server" Width="50px" Text='<%# Eval("AmPmTime")%>'>
                            <asp:ListItem>AM</asp:ListItem><asp:ListItem>PM</asp:ListItem>
                        </asp:DropDownList> 
                            </EditItemTemplate> 
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Event Duration" ItemStyle-HorizontalAlign="Center">
                           <ItemTemplate><asp:Label ID="lblEDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label> 
                            </ItemTemplate> 
                            <EditItemTemplate>
                            <asp:TextBox ID="txtDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:TextBox> 
                            
                            </EditItemTemplate> 
                           </asp:TemplateField> 
                           
                           <asp:TemplateField HeaderText="Event Description">
                           <ItemTemplate><asp:Label ID="lblEventDesc" runat="server" Text='<%# Eval("EventDesc")%>'></asp:Label> 
                            </ItemTemplate> 
                            <EditItemTemplate><asp:TextBox ID="txtEventDesc" runat="server" Text='<%# Eval("EventDesc")%>'></asp:TextBox> 
                            </EditItemTemplate> 
                           </asp:TemplateField>                           
                           <asp:TemplateField HeaderText="Posted By">
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

