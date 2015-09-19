<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="EventsCalendar.aspx.cs" Inherits="Patient_EventsCalendar" Title="eCarex Health Care System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="updatePanelEventInfo" runat="server" UpdateMode="Conditional">
     <ContentTemplate>

    <table border="0" width="600px" align="center">   
         
                <tr class="medication_info_th1">
                    <td align = "center" colspan="4" height="20px">
                        <asp:Label id="lblTitle" runat="server" Text="EVENT CALENDAR" 
                            Font-Bold="True" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
              <tr class="medication_info_tr-even">
                        <td width = "100px" align = "left">
                        <asp:Label ID="lblSelectClinic" runat="server" Text="Clinic/Location"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                        <asp:RadioButtonList ID="rblLocation" runat="server" 
                            RepeatDirection="Horizontal" AutoPostBack= "true"
                            onselectedindexchanged="rblLocation_SelectedIndexChanged" >
                            <asp:ListItem>Select Clinic</asp:ListItem>
                            <asp:ListItem Selected="True">All Clinics</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>  
               </tr>
               <tr class="medication_info_tr-odd">
                    <td width = "100px" align = "left">
                        <asp:Label ID="lblClinicName" runat="server" Text="Clinic Name"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                        <asp:DropDownList ID="ddlClinicName" runat="server" Width = "180px" AutoPostBack = "true" 
                            TabIndex="1" onselectedindexchanged="ddlClinicName_SelectedIndexChanged" 
                            ondatabound="ddlClinicName_DataBound" Enabled = "false"></asp:DropDownList> 
                    </td>                    
                </tr>
               <tr class="medication_info_tr-even">
                <td width = "100px" align = "left">
                        <asp:Label ID="lblDrugType_ID" runat="server" Text="Facility"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                    <asp:DropDownList ID="ddlFacilityName" runat="server" Width = "180px" AutoPostBack = "true" 
                            TabIndex="2" ondatabound="ddlFacilityName_DataBound" Enabled = "false"></asp:DropDownList> 
                    </td>
                    
                </tr>
                <tr class="medication_info_tr-odd">
                     <td width = "100px" align = "left">                     
                       <asp:Label ID="lblDate" runat="server" Text="Event Date"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                      <asp:TextBox ID="txtDate" runat="server" Width="175px" TabIndex="3"></asp:TextBox>
                        <cc1:CalendarExtender ID="Getdate" runat="server" TargetControlID="txtDate" 
                        Animated="true" FirstDayOfWeek="Default" PopupButtonID="txtDate" 
                        PopupPosition="BottomLeft" >
                        </cc1:CalendarExtender>           
                    </td>
                </tr>
                <tr class="medication_info_tr-even">
                     <td width = "100px" align = "left">                     
                       <asp:Label ID="lblTime" runat="server" Text="Event Time  (hh:mm)"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                        <asp:DropDownList ID="ddlHrs" runat="server" Width="50px" TabIndex="4">
                            <asp:ListItem>00</asp:ListItem><asp:ListItem>01</asp:ListItem><asp:ListItem>02</asp:ListItem><asp:ListItem>03</asp:ListItem><asp:ListItem>04</asp:ListItem><asp:ListItem>05</asp:ListItem><asp:ListItem>06</asp:ListItem><asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem><asp:ListItem>09</asp:ListItem><asp:ListItem>10</asp:ListItem><asp:ListItem>11</asp:ListItem><asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList> 
                         <asp:DropDownList ID="ddlMins" runat="server" Width="50px" TabIndex="6">
                            <asp:ListItem>00</asp:ListItem><asp:ListItem>15</asp:ListItem><asp:ListItem>30</asp:ListItem><asp:ListItem>45</asp:ListItem>
                        </asp:DropDownList> 
                         <asp:DropDownList ID="ddlAmPm" runat="server" Width="50px" TabIndex="6">
                            <asp:ListItem>AM</asp:ListItem><asp:ListItem>PM</asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                </tr>
                <tr class="medication_info_tr-odd">
                     <td width = "100px" align = "left">                     
                       <asp:Label ID="lblDuration" runat="server" Text="Event Duration"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                       <%--<asp:DropDownList ID="ddlDuration" runat="server" Width="150px">
                            <asp:ListItem>2 Hours</asp:ListItem><asp:ListItem>Full Day</asp:ListItem><asp:ListItem>3 Days</asp:ListItem>
                        </asp:DropDownList> --%>
                         <asp:TextBox ID="txtDuration" runat="server" TabIndex="7" 
                            Width="175px"></asp:TextBox>
                    </td>
                </tr>
                
                
                <tr class="medication_info_tr-even">
                     <td width = "100px" align = "left">                     
                       <asp:Label ID="lblEventDesc" runat="server" Text="Event Description"></asp:Label>
                    </td>
                    <td width="250px" class="medication_info_td-even" align="left">
                      <asp:TextBox ID="txtEventDesc" runat="server" Width="250px" TabIndex="8" 
                            TextMode="MultiLine" Height="100px"></asp:TextBox>
                    </td>
                </tr>

               <tr class="medication_info_tr-odd" align = "Center">
                    <td colspan="2">
                  
                     <asp:ImageButton ID="btnEInfoSave" runat="server" ImageUrl="../images/save.gif" border="0"
                                    Style="height: 24px" TabIndex="9" 
                            Height="24px" OnClientClick= "return validateEvent();" 
                            onclick="btnEInfoSave_Click"></asp:ImageButton>
              
                       <%-- <asp:ImageButton ID="btnEInfoUpdate" runat="server" ImageUrl="~/images/update.png" 
                      border="0" Visible="false" Style="height: 24px" 
                      TabIndex="5" onclick="btnEInfoUpdate_Click" ></asp:ImageButton>
                  --%>
                    <asp:ImageButton ID="btnEInfoCancel" runat="server" ImageUrl="../images/cancelPopUp.gif"
                                    border="0" TabIndex="10" onclick="btnEInfoCancel_Click"></asp:ImageButton>                            
                 
                   <%--<asp:ImageButton ID="btnEInfoDelete" runat="server" ImageUrl="~/images/delete.png" border="0"
                    Visible="false" Style="height: 24px" TabIndex="7" onclick="btnEInfoDelete_Click"></asp:ImageButton>--%>
                 </td>
                </tr> 
                      </tr>
                   <tr class="medication_info_tr-even">
                   <td colspan="2" align="right"> 
                    <asp:LinkButton ID="lnkbtnPop" runat="server" 
                           PostBackUrl="../Masters/EventsList.aspx" TabIndex="11">Click here</asp:LinkButton> 
                       &nbsp; to view Events list
              
                   </td>
                   </tr>
                
       </table>  

     </ContentTemplate>
   

 </asp:UpdatePanel>
<%--<script type = "text/javascript" language = "javascript">
    function openwindow() {
        window.open("EventsList.aspx", "mywindow", "toolbar = no,width =600,scrollbars = yes");
    }
</script> --%>
</asp:Content>