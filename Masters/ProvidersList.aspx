<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProvidersList.aspx.cs" Inherits="ProvidersList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Providers List</title>
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
              <asp:Label ID="lblHeading" runat="server" Text="Providers List"></asp:Label>
              </td>                   
        </tr>
        <tr class="medication_info_tr-odd">          
        <td align="left">
            <input style="background-color:#FDF5E6;color:Black;  border-width: 1px;  border-style: solid; width:60px" type="button" value="Print" id="btnPrint" runat="Server" onclick="CallPrint('divPrint')" />
        </td>
        </tr>
    
        <tr class="medication_info_tr-even">
            <td align="center">
                <asp:Panel ID="PnlGV" runat="server">
            
            <div id="divPrint">
            <asp:GridView ID="GVDocList" runat="server" Width="100%" 
                    AutoGenerateColumns="False" ondatabound="GVDocList_DataBound" 
                    AllowPaging = "true" onpageindexchanging="GVDocList_PageIndexChanging" PageSize="25" >
                       <EditRowStyle Font-Bold="False" />
                       <AlternatingRowStyle CssClass="medication_info_tr-even" HorizontalAlign = "Left"/>
                       <Columns>
                           <asp:BoundField HeaderText="Provider Name" DataField = "ProviderName" SortExpression = "ProviderName" ItemStyle-Width = "150px" >
                               
                               <ItemStyle Width="150px" />
                               
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Address" DataField = "ProviderAddress" SortExpression = "ProviderAddress" ItemStyle-Width = "250px">
                               
                               <ItemStyle Width="250px" />
                               
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Clinic" DataField = "Location" SortExpression = "Location" ItemStyle-Width = "100px">
                               
                               <ItemStyle Width="100px" />
                               
                           </asp:BoundField>
                    <%--       <asp:BoundField HeaderText="Home Phone" DataField = "HomePhone" SortExpression = "HomePhone" ItemStyle-Width = "100px">
                               
                               <ItemStyle Width="100px" />
                               
                           </asp:BoundField>--%>
                           <asp:BoundField HeaderText="Cell Phone" DataField = "CellPhone" SortExpression = "CellPhone" ItemStyle-Width = "100px">
                               
                               <ItemStyle Width="100px" />
                               
                           </asp:BoundField>
                    <%--       <asp:BoundField HeaderText="Email" DataField = "Email" SortExpression = "Email" ItemStyle-Width = "100px">
                              
                               <ItemStyle Width="100px" />
                              
                           </asp:BoundField>--%>
                          <%-- <asp:BoundField HeaderText="Fax" DataField = "Fax" SortExpression = "Fax" ItemStyle-Width = "100px">
                            
                               <ItemStyle Width="100px" />
                            
                           </asp:BoundField>--%>
                           <asp:BoundField HeaderText="Speciality" DataField = "Speciality" SortExpression = "Speciality" ItemStyle-Width = "100px">
                 
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
