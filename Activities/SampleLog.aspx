<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="SampleLog.aspx.cs" Inherits="SampleLog" Title="eCarex Health Care System" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
 
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text="Sample/PAP LOG"></asp:Label>
                    </td>
                   
            </tr>
             <tr class="medication_info_tr-odd">
                <td align="left" style="width:300px">
                    <asp:Label ID="lblOrganization" runat="server" Text="Organization :"></asp:Label>
                    &nbsp;&nbsp;<asp:DropDownList
                        ID="ddlOrganization" runat="server" 
                        OnDataBound="ddlOrganization_DataBound" AutoPostBack="True" 
                        onselectedindexchanged="ddlOrganization_SelectedIndexChanged" style="width:200px"></asp:DropDownList>
                        </td>
                <td align="left" style="width:225px"  >&nbsp;
                        <asp:Label ID="lblLocation" runat="server" Text="Location :"></asp:Label>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlLocation" runat="server" OnDataBound="ddlLocation_DataBound" 
                        onselectedindexchanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True" style="width:150px"></asp:DropDownList>
                    
                </td>
                
                <td align="left" width ="50px" colspan="2">
                    &nbsp;
                   
                </td>
          
            </tr>   
            <tr class="medication_info_tr-odd">
            <td align="right" colspan="4">
                <asp:label id="SortInformationLabel" forecolor="Navy" runat="server"/>

             </td>
            
            </tr>
            <tr class="medication_info_tr-even">
            <td align="center" colspan="4" >
                <asp:GridView ID="gridRxQueue" runat="server" 
                     AutoGenerateColumns="False" 
                     allowsorting="true"
                     onsorting="gridRxQueue_Sorting"
                     onsorted="gridRxQueue_Sorted" 
                     Width="800px"  
                     OnRowCommand="gridRxQueue_RowCommand" EmptyDataText="No Records Found..."  >
                       <Columns>
                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbldrugID" runat="server" Text='<%#Eval("Drug_ID")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("Inv_Group_Desc")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Drug" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px">
                            <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblDrug" runat="server" Text='<%#Eval("Drug_Name")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Form" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                           
                           <ItemTemplate>
                                <asp:Label ID="lblForm" runat="server" Text='<%# Eval("Drug_Form")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Qty in Stock"  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                          
                            <ItemTemplate>
                            <asp:HyperLink ID="hlQty" runat="server" NavigateUrl='<%# Eval("url")%>' Text='<%# Eval("Qty")%>' Font-Bold="false"></asp:HyperLink>
                               
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="LastUpdated"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblLastUpdated" runat="server" Text='<%# Eval("Inv_Date")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>
                       <%--<asp:TemplateField HeaderText="Comments"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Inv_Desc")%>' Font-Bold="false"></asp:Label>                            
                            </ItemTemplate>
                            
                       </asp:TemplateField>--%>
                 
                       
                       
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <HeaderStyle CssClass="medication_info_th1" />
                       <RowStyle CssClass="medication_info_tr-odd" />
                 </asp:GridView>
                 </td>
            </tr>
            </table>
    
</asp:Content>

