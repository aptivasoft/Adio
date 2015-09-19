<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="DeletePatient.aspx.cs" Inherits="Masters_DeletePatient" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <title>
    eCarex Health Care System
 </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode = "Conditional">
          <ContentTemplate>         
 
    <table  align="center"  width="800px" class="patient_info">
        <tr class="medication_info_th1">
         <td align="center"  colspan="6" style="height:25px;vertical-align:middle;"> <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
         </td>
        </tr>
             <tr class="medication_info_tr-odd">
                <td align="left" colspan="2">
                    <asp:Label ID="lblOrganization" runat="server" Text="Organization :"></asp:Label>
                    <asp:DropDownList ID="ddlOrganization" runat="server" OnDataBound="ddlOrganization_DataBound" AutoPostBack="True" onselectedindexchanged="ddlOrganization_SelectedIndexChanged"></asp:DropDownList>
               </td>
                <td align="left"  colspan="2">
                    <asp:Label ID="lblLocation" runat="server" Text="Location :"></asp:Label>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlLocation" runat="server" OnDataBound="ddlLocation_DataBound" onselectedindexchanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True" style="width:150px"></asp:DropDownList>
                </td>
                
                <td align="left" width ="50px"  colspan="2">
                      <asp:LinkButton ID = "lnkbtnView" Text = "View" runat = "server"   onclick="lnkbtnView_Click"></asp:LinkButton>   
                </td>
            </tr> 
            <tr><td colspan="6" align="right"><asp:Label ID="lblPageCount" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label></td></tr>           
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="6">
                <asp:GridView ID="gridPatientList" runat="server" AutoGenerateColumns="False" 
                     Width="800px"  OnRowCommand="gridPatientList_RowCommand" 
                     
                    EmptyDataText="No Records Found..." AllowPaging="True" 
                    onpageindexchanging="gridPatientList_PageIndexChanging" PageSize="50" 
                    PagerSettings-Mode="NextPreviousFirstLast" 
                    PagerSettings-FirstPageText="First  " 
                    PagerSettings-LastPageText="Last " 
                    PagerSettings-NextPageText="Next " 
                    PagerSettings-PreviousPageText="Previous" 
                    PagerStyle-BackColor="#4f81bc" 
                    PagerStyle-ForeColor="#FFFFFF"
                    ShowHeader="true"
                     OnRowCreated="gridPatientList_RowCreated"> 
                    
                       <Columns>
                            <asp:TemplateField HeaderText="Clinic" ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblClinic_Name" runat="server" Text='<%#Eval("Clinic_Name")%>'></asp:Label>
                            </ItemTemplate>                        
                                <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Left"  Visible="false">
                            <ItemTemplate>
                                &nbsp;&nbsp;<asp:Label ID="lblFacility_Name" runat="server" Text='<%#Eval("Facility_Name")%>' Font-Bold="false"></asp:Label>
                            </ItemTemplate>                        
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                       <asp:HyperLinkField HeaderText="Patient Name" ItemStyle-HorizontalAlign="left" 
                                DataNavigateUrlFields="Pat_ID" 
                                DataNavigateUrlFormatString="../Patient/AllPatientProfile.aspx?patID={0}" 
                                DataTextField="PatientName"  >
                           <ItemStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                        <asp:TemplateField HeaderText="Patient Name" ItemStyle-HorizontalAlign="left" Visible="false">
                           
                           <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPatName" runat="server" Text='<%# Eval("PatientName")%>' Font-Bold="false"></asp:Label>                       
                           </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="DOB"  ItemStyle-HorizontalAlign="center" >
                          
                            <ItemTemplate>
                                <asp:Label ID="lblPatDOB" runat="server" Text='<%# Eval("Pat_DOB") %>' 
                                    Font-Bold="False"></asp:Label>                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center" />
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="SSN"  ItemStyle-HorizontalAlign="center" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPatSSN" runat="server" Text='<%# Eval("Pat_SSN") %>' 
                                    Font-Bold="False"></asp:Label>                            
                            </ItemTemplate>
                            
                            <ItemStyle HorizontalAlign="center" />
                            
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Gender"  ItemStyle-HorizontalAlign="center" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblPatGender" runat="server" Text='<%# Eval("Pat_Gender") %>' 
                                    Font-Bold="False"></asp:Label>                            
                            </ItemTemplate>
                            
                            <ItemStyle HorizontalAlign="center" />
                            
                       </asp:TemplateField>
                       </Columns>
                       <AlternatingRowStyle CssClass="medication_info_tr-even" />
                       <PagerStyle BackColor="#4F81BC" ForeColor="White" />
                       <HeaderStyle CssClass="medication_info_th1"  />
                       <PagerSettings FirstPageImageUrl="~/Images/MoveFirst.gif" 
                                      FirstPageText="" 
                                      LastPageImageUrl="~/Images/MoveLast.gif" 
                                      LastPageText="" 
                                      Mode="NextPreviousFirstLast" 
                                      NextPageImageUrl="~/Images/MoveNext.gif" 
                                      NextPageText="" Position="TopAndBottom" 
                                      PreviousPageImageUrl="~/Images/MovePrevious.gif" 
                                      PreviousPageText=""/>
                           
                       <RowStyle CssClass="medication_info_tr-odd"  />
                 </asp:GridView>
                 </td>
              </tr>
             
            </table>
    </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

