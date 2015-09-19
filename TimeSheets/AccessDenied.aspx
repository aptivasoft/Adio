<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true"  Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="vertical-align:middle;height:100%">
<center>
 <table  align="center"  width="800px"  style="height:100%;vertical-align:middle;">
 <tr>
                    <td align="center"  colspan="4"  style="height:25px;vertical-align:middle;"   > 
                        <table  align="center"  width="800px">
                        <tr>
                        <td>
                            
                        </td>
                        </tr>
                        <tr>
                            <td align="center"  colspan="4"  style="height:25px;vertical-align:middle;"   > 
                                <asp:Image ID="imgAccessDenied" runat="server" ImageUrl="../images/accdeny.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center"  colspan="4"  style="height:25px;vertical-align:middle;"   > 
                            
                                <asp:Label  ID="lblHeading" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="13px" ForeColor="Red" Text="Access Denied" BackColor="#eaf0f4" Width="110px"></asp:Label>
                           
                            </td>
                        </tr>
                        </table>
                    </td>
  </tr>
 </table>
</center>
</div>
</asp:Content>

