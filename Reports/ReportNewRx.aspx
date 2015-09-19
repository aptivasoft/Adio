
<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="ReportNewRx.aspx.cs" Inherits="ReportNewRx" Title="eCarex Health Care System" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
          
   
 
    <table  align="center"  width="1000px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="5" style="height:25px;vertical-align:middle;"  > <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                   
            </tr>
             <tr class="medication_info_tr-odd">
                         <td align="left" style="width:500px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>

             <table width="100%">
             <tr>
             <td align="left" style="width:50%"  >
                <asp:Label ID="lblOrganization" runat="server" Text="Organization :"></asp:Label><br />
                <asp:DropDownList ID="ddlOrganization" runat="server" 
                    OnDataBound="ddlOrganization_DataBound" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrganization_SelectedIndexChanged" style="width:200px"></asp:DropDownList>
                         
                
            </td>
             <td align="left" style="width:50%"  >
                
                    <asp:Label ID="lblLocation" runat="server" Text="Location     :"></asp:Label><br /><asp:DropDownList
                    ID="ddlLocation" runat="server" OnDataBound="ddlLocation_DataBound" 
                    onselectedindexchanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True" style="width:200px"></asp:DropDownList>
            </td>
             </tr></table>
                    </ContentTemplate>
                    </asp:UpdatePanel>

             </td>
            
            <td align="left" style="width:125px" >
            <asp:Label ID = "lblStatus" runat = "server" Text = "Status :"></asp:Label><br />
                <asp:DropDownList ID="ddlStatus" runat="server" style="width:125px" 
                    DataSource="<%# RxStatus.getRxStatus(1)%>" DataTextField="Status_Desc" 
                    DataValueField="Status_Code" ondatabound="ddlStatus_DataBound">
                              
                </asp:DropDownList>
            </td>
           <td align="left" style="width:150px" >
            &nbsp;<asp:Label ID="lblDate" runat="server" Text="From Date :"></asp:Label><br /><asp:TextBox
                ID="txtDate1" runat="server" Width="60px" ></asp:TextBox><asp:Image ID="Image1" runat="server" ImageUrl="../images/Calendar.png" />&nbsp;
<cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="Right" PopupButtonID="Image1"
    TargetControlID="txtDate1">
</cc1:CalendarExtender></td>    <td align="left" style="width:150px" >
&nbsp;<asp:Label ID="lblDate2" runat="server" Text="To Date :"></asp:Label><br /><asp:TextBox
                ID="txtDate2" runat="server" Width="60px" ></asp:TextBox><asp:Image ID="Image2" runat="server" ImageUrl="../images/Calendar.png" />&nbsp;
<cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="Right" PopupButtonID="Image2"
    TargetControlID="txtDate2">
</cc1:CalendarExtender>
            </td>
             <td align="left"  style="width:50px">
             &nbsp;<asp:LinkButton 
                    runat="server" onclick="btnRxReport_Click" ID="btnRxReport" OnClientClick="return compareDates();">view</asp:LinkButton></td></tr>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="5" >
                <rsweb:ReportViewer ID="ReportViewer2" runat="server" 
            Font-Names="Verdana" Font-Size="8pt" Height="380px" Width="1000px" ZoomMode="PageWidth">
          
         </rsweb:ReportViewer>
                 </td>
            </tr>
            </table>
   
</asp:Content>



