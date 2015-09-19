<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FacilityList.aspx.cs" Inherits="Patient_FacilityList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Facility List</title>
     <link rel="stylesheet" type="text/css" href="../css/style.css" />   
     <link rel="stylesheet" type="text/css" href="../css/medication.css" />
     <script type="text/javascript" src="../javascript/eCareX.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center" width="100%">
        <tr class="medication_info_th1">
              <td align="center" style="height:25px;vertical-align:middle;"> 
              <asp:Label ID="lblHeading" runat="server" Text="Facility List"></asp:Label>
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
                           <asp:BoundField HeaderText="Facility Name" DataField = "FacilityName">
                               
                               <ItemStyle Width="100px" />
                               
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Facility Code" DataField = "FacilityCode">
                               
                               <ItemStyle Width="100px" />
                               
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Address" DataField = "Address">
                               
                               <ItemStyle Width="250px" />
                               
                           </asp:BoundField>

                           <asp:BoundField HeaderText="Phone" DataField = "Phone">
                               
                               <ItemStyle Width="100px" />
                               
                           </asp:BoundField>
                    <%--       <asp:BoundField HeaderText="Email" DataField = "Email" SortExpression = "Email" ItemStyle-Width = "100px">
                              
                               <ItemStyle Width="100px" />
                              
                           </asp:BoundField>--%>
                          <%-- <asp:BoundField HeaderText="Fax" DataField = "Fax" SortExpression = "Fax" ItemStyle-Width = "100px">
                            
                               <ItemStyle Width="100px" />
                            
                           </asp:BoundField>--%>
                           <asp:BoundField HeaderText="Clinic" DataField = "Clinic">
                 
                               <ItemStyle Width="100px" />
                 
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
    </div>
    </form>
</body>
</html>