<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" MasterPageFile="~/Templates/eCareXMaster.master"Inherits="Patient_EmployeeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" width="100%">
        <tr class="medication_info_th1">
              <td align="center" style="height:25px;vertical-align:middle;"> 
              <asp:Label ID="lblHeading" runat="server" Text="Employee List"></asp:Label>
              </td>                   
        </tr>
        <tr class="medication_info_tr-odd">          
            <td align="left">
            <input type="button" value="Print" id="btnPrint" runat="Server" onclick="CallPrint('divPrint')" style=" background-color:#FDF5E6; border-color:Black; border-style:Solid; width:50px; border-width:1px"/>
           <%-- <input type="button" value="Print this page " id="btnPrint" runat="Server" onclick="return Button1_onclick()" />
            <asp:Button ID="btnPrintCtrl" runat="server" Text="Print" 
                    onclick="btnPrintCtrl_Click" />--%>
            </td>
        </tr>
    
        <tr class="medication_info_tr-even">
            <td align="center">
                <asp:Panel ID="PnlGV" runat="server">
            
            <div id="divPrint">
            <asp:GridView ID="GVList" runat="server" Width="100%" 
                    AutoGenerateColumns="False" ondatabound="GVList_DataBound" 
                    AllowPaging = "true" onpageindexchanging="GVList_PageIndexChanging" PageSize="25" >
                       <EditRowStyle Font-Bold="False" />
                       <AlternatingRowStyle CssClass="medication_info_tr-even" HorizontalAlign = "Left"/>
                       <Columns>
                        <asp:HyperLinkField HeaderText="Employee Name" ItemStyle-HorizontalAlign="left" 
                                DataNavigateUrlFields="Emp_ID" 
                                DataNavigateUrlFormatString="Employee.aspx?EmpID={0}" 
                                DataTextField="EmpName"  >
                           <ItemStyle HorizontalAlign="Left" Width="15%"/>
                            </asp:HyperLinkField>
                            
                               
                          
                           <asp:BoundField HeaderText="Address" DataField = "EmpAddress">
                               
                               <ItemStyle  Width="30%"  />
                               
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Phone" DataField = "Phone">
                                <ItemStyle Width="10%" HorizontalAlign="Center"/>
                               
                           </asp:BoundField>
                           
                           <asp:BoundField HeaderText="Role" DataField = "EmpRole">
                               
                               <ItemStyle Width="10%" />
                               
                           </asp:BoundField>

                           <asp:BoundField HeaderText="Location" DataField = "Location">
                               
                               <ItemStyle Width="30%" />
                               
                           </asp:BoundField>
                           
                           <asp:BoundField HeaderText="Title" DataField = "Title">
                               
                               <ItemStyle Width="5%" />
                               
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Status" DataField = "Status">
                               
                               <ItemStyle Width="10%" />
                               
                           </asp:BoundField>


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
 </asp:Content>