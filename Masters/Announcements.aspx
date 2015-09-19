<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="Announcements.aspx.cs" Inherits="Patient_Announcements" Title="eCarex Health Care System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="updatePanelEventInfo" runat="server" UpdateMode="Conditional">
     <ContentTemplate>

    <table border="0" width="80%" align="center">   
         
                <tr class="medication_info_th1">
                    <td align = "center" colspan="4" height="20px">
                        <asp:Label id="lblTitle" runat="server" Text="ANNOUNCEMENTS" Font-Bold="True" Font-Size="Small" ></asp:Label>
                    </td>
                </tr>
              <tr class="medication_info_tr-even">
                        <td width = "100px" align = "left">                     
                       <asp:Label ID="lblDate" runat="server" Text="Announcement Date" Width="150px"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                      <asp:TextBox ID="txtDate" runat="server" Width="150px" TabIndex="1"></asp:TextBox>
                        <cc1:CalendarExtender ID="Getdate" runat="server" TargetControlID="txtDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtDate" 
                        PopupPosition="BottomLeft" >
                        </cc1:CalendarExtender>           
                    </td>
                    <td>
                     <asp:Label ID="lblBy" runat="server" Text="By :" Width="40px"></asp:Label>
                     </td>
                     <td>
                    <asp:TextBox ID="txtName" runat="server" Width="150px" TabIndex="2"></asp:TextBox>
                    </td>
               </tr>
               <tr class="medication_info_tr-odd">
                    <td width = "100px" align = "left">                     
                       <asp:Label ID="lblMessage" runat="server" Text="Message"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                      <asp:TextBox ID="txtMessage" runat="server" Width="400px" TabIndex="3" 
                            TextMode="MultiLine" Height="100px"></asp:TextBox>
                    </td> 
                    <td colspan = "2"></td>     
                </tr>
                <tr class="medication_info_tr-even">
                    <td width = "100px" align = "left">                     
                       <asp:Label ID="lblExpiryDate" runat="server" Text="Expiry Date" Width="150px"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                      <asp:TextBox ID="txtExpiryDate" runat="server" Width="150px" TabIndex="4"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExpiryDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtExpiryDate" 
                        PopupPosition="BottomLeft" >
                        </cc1:CalendarExtender>           
                    </td>
                   <td colspan = "2"></td>                
                </tr>
                
                <tr class="medication_info_tr-odd">
                <td width = "100px" align = "left">
                        <asp:Label ID="lblSelectClinic" runat="server" Text="Clinic/Location"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                        <asp:RadioButtonList ID="rblLocation" runat="server" 
                            RepeatDirection="Horizontal" AutoPostBack= "true"
                            onselectedindexchanged="rblLocation_SelectedIndexChanged" TabIndex="5" >
                            <asp:ListItem>Select Clinic</asp:ListItem>
                            <asp:ListItem Selected="True">All Clinics</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>  
                     <td colspan = "2"></td> 
                </tr>

                <tr class="medication_info_tr-even">
                    <td width = "100px" align = "left">
                        <asp:Label ID="lblClinicName" runat="server" Text="Clinic Name"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                        <asp:DropDownList ID="ddlClinicName" runat="server" Width = "180px" AutoPostBack = "true" 
                            TabIndex="6" onselectedindexchanged="ddlClinicName_SelectedIndexChanged" 
                            ondatabound="ddlClinicName_DataBound" Enabled = "false"></asp:DropDownList> 
                    </td>  
                     <td colspan = "2"></td>             
                </tr>
               <tr class="medication_info_tr-odd">
                <td width = "20%" align = "left">
                        <asp:Label ID="lblDrugType_ID" runat="server" Text="Facility"></asp:Label>
                    </td>
                    <td width="55%" class="medication_info_td-even" align="left">
                    <asp:DropDownList ID="ddlFacilityName" runat="server" Width = "180px" AutoPostBack = "true" 
                            TabIndex="7" ondatabound="ddlFacilityName_DataBound" Enabled = "false"></asp:DropDownList> 
                    </td>
                     <td colspan = "2"></td> 
                </tr>

               <tr class="medication_info_tr-even" align = "Center">
                    <td colspan="4">
                  
                     <asp:ImageButton ID="btnAInfoSave" runat="server" ImageUrl="../images/save.gif" border="0"
                                    Style="height: 24px" TabIndex="8" Height="24px" 
                            OnClientClick= "return validateAnnouncements();" onclick="btnAInfoSave_Click" ></asp:ImageButton>
              
                       <%-- <asp:ImageButton ID="btnEInfoUpdate" runat="server" ImageUrl="~/images/update.png" 
                      border="0" Visible="false" Style="height: 24px" 
                      TabIndex="5" onclick="btnEInfoUpdate_Click" ></asp:ImageButton>
                  --%>
                    <asp:ImageButton ID="btnEInfoCancel" runat="server" ImageUrl="../images/cancelPopUp.gif"
                                    border="0" TabIndex="9" onclick="btnAInfoCancel_Click"></asp:ImageButton>                            
                 
                   <%--<asp:ImageButton ID="btnEInfoDelete" runat="server" ImageUrl="~/images/delete.png" border="0"
                    Visible="false" Style="height: 24px" TabIndex="7" onclick="btnEInfoDelete_Click"></asp:ImageButton>--%>
                 </td>
                </tr> 
                      </tr>
                   <tr class="medication_info_tr-odd">
                   <td colspan="4" align="right"> 
                    <asp:LinkButton ID="lnkbtnPop" runat="server" 
                           PostBackUrl="../Masters/AnnouncementsList.aspx">Click here</asp:LinkButton> 
                       &nbsp; to view Announcements
              
                   </td>
                   </tr>
                
       </table>  

     </ContentTemplate>
   

 </asp:UpdatePanel>
<%--<script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("Announcements.aspx", "mywindow", "toolbar = no,width =600,scrollbars = yes");
    }
</script> --%>
</asp:Content>