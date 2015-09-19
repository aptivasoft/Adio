<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="ActivityCodes.aspx.cs" Inherits="ActivityCodes" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <title>eCarex Health Care System</title>    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updatePanelRegister" runat="server" 
        UpdateMode = "Conditional" RenderMode="Inline">
    <ContentTemplate> 
      
    <center>
    <table width="600px" >
    <tr class="medication_info_th1">
       <td colspan="4" align="center" height="20px">ACTIVITY CODES</td>       
    </tr>
 
       
        <tr class="medication_info_tr-odd">
                        <td align="left" >
                            <asp:Label ID="lblActivityCode" runat="server" Text="Activity Code"></asp:Label>
                        </td>
                        <td align="left" >
                                        
                            <asp:TextBox ID="txtActivityCode" runat="server" TabIndex="1" Width="50px" 
                                ></asp:TextBox>
                                <cc1:FilteredTextBoxExtender  ID="FTAC" TargetControlID="txtActivityCode" runat="server" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                            
                        </td>
                        </tr>
        <tr class="medication_info_tr-even">
       
                            <td align="left" >
                                <asp:Label ID="lblActivityName" runat="server" Text="Activity Name"></asp:Label>
                            </td>
                            <td class="medication_info_td-even" align="left">
                                <asp:TextBox ID="txtActivityName" runat="server" TabIndex="2" Width="150px" ></asp:TextBox>
                            </td>
                            </tr>
        <tr class="medication_info_tr-odd">
     
                                <td align="left" >
                                    <asp:Label ID="lblActivityDesc" runat="server" Text="Activity Description"></asp:Label>
                                </td>
                                <td class="medication_info_td-even" align="left">
                                    <asp:TextBox ID="txtActivityDesc" runat="server" TabIndex="4" Width="250px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </td>
                            </tr>
        <tr class="medication_info_tr-even">
       
            <td colspan="3" align="center" >
                <asp:ImageButton ID="btnSave" runat="server" border="0" 
                    ImageUrl="~/images/save.gif" onclick="btnSave_Click" 
                   TabIndex="8" />
            
            
           
            <asp:ImageButton ID="btnUserCancel" runat="server" 
                    ImageUrl="../images/cancelPopUp.gif" border="0" 
                     TabIndex="10" onclick="btnUserCancel_Click"></asp:ImageButton>
           
           </td>
        </tr>
        <tr class="medication_info_tr-odd">
     
            <td align="right" colspan = "2" >
              <asp:LinkButton ID="lnkbtnPop" runat="server" OnClientClick = "return openwindow();">Click here</asp:LinkButton> &nbsp; to view ActivityCodes list
              
            </td>
            
         </tr>        
 
        
        <tr class="medication_info_tr-even">
        <td colspan="2">
           <asp:GridView ID="gridActivityList" runat="server" AutoGenerateColumns="False" 
                     Width="600px"  EmptyDataText="No Records Found..."  >
                       <Columns>
                        <asp:BoundField HeaderText="Activity Code"   ItemStyle-HorizontalAlign="Left" DataField="Activity_Code"  ItemStyle-Font-Bold="false"/>   
                        <asp:BoundField HeaderText="Activity Name"   ItemStyle-HorizontalAlign="Left" DataField="Activity_Name"  ItemStyle-Font-Bold="false"/>   
                                         
                       
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
  <script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("ActivityCodesList.aspx", "mywindow", "toolbar = no,width =600,height = 600,scrollbars = yes");
    }
</script> 
 </asp:Content>

