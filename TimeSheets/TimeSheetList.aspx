<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="TimeSheetList.aspx.cs" Inherits="TimeSheetList" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
          
   
 
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="5" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
             
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="5" >
                <asp:GridView ID="gridTimeSheetList" runat="server" AutoGenerateColumns="False" 
                     Width="800px"  OnRowCommand="gridTimeSheetList_RowCommand" EmptyDataText="No Records Found..."  >
                       <Columns>
                            
                        <asp:BoundField HeaderText="Pay Period Ending" DataFormatString="{0:MM/dd/yyyy}"   ItemStyle-HorizontalAlign="Center" DataField="PP_EndDate" ItemStyle-Font-Bold="false"  />
                         <asp:BoundField HeaderText="Regular Hours"  NullDisplayText="0" ItemStyle-HorizontalAlign="Right" DataField="RegularHours"  ItemStyle-Font-Bold="false"/>
                           <asp:BoundField HeaderText="OT/UT" NullDisplayText="0" ItemStyle-HorizontalAlign="Right" DataField="OT_UT"  ItemStyle-Font-Bold="false"/>
                        
                       <asp:TemplateField HeaderText="Total Hours" ItemStyle-HorizontalAlign="Right"  >
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotalHours" runat="server" Text='<%# display_link1(Eval("RegularHours"),Eval("OT_UT"))%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="left" >
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Action"  ItemStyle-HorizontalAlign="Left"  >
                          
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("PP_ID")%>' CommandName='Edit' Visible='<%# display_link((string) Eval("status"),0)%>'>Create</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("PP_ID")%>' CommandName='Edit' Visible='<%# display_link((string) Eval("status"),1)%>'>Modify</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Eval("PP_ID")%>' CommandName='View' Visible='<%# display_link((string) Eval("status"),2)%>'>view</asp:LinkButton>
                                
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
    </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

