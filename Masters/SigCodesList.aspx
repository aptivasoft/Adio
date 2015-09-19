<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SigCodesList.aspx.cs" Inherits="Patient_SigCodesList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SIG Codes</title>
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
              <asp:Label ID="lblHeading" runat="server" Text="SIG Codes"></asp:Label>
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
                           <asp:BoundField HeaderText="SIG Code" DataField = "SIG_Code" ItemStyle-HorizontalAlign="Left">
                           </asp:BoundField>
                           <asp:BoundField HeaderText="SIG Name" DataField = "SIG_Name" ItemStyle-HorizontalAlign="Left">
                           </asp:BoundField>  
                           <asp:BoundField HeaderText="SIG Factor" DataField = "SIG_Factor" ItemStyle-HorizontalAlign="Left">
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