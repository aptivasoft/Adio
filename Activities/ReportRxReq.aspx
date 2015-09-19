
<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="ReportRxReq.aspx.cs" Inherits="ReportRxReq" Title="eCarex Health Care System" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table  align="center"  width="1000px" class="patient_info">
        <tr class="medication_info_th1">
               
                    <td align="right"  colspan="5" style="height:25px;vertical-align:middle;"  > 
                        <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                       <asp:ImageButton ID="btnRClose" runat="server" OnClick="btn_RCloseClick" ImageUrl="../images/close_popup.png" Height="16px" Width="16px" border="0" ToolTip="Close"></asp:ImageButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                   
            </tr>
            <%-- <tr class="medication_info_tr-odd">
            <td align="left" style="width:600px" colspan="2"  >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
                <asp:Label ID="lblOrganization" runat="server" Text="Organization :"></asp:Label>&nbsp;&nbsp;<asp:DropDownList
                    ID="ddlOrganization" runat="server" 
                    OnDataBound="ddlOrganization_DataBound" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrganization_SelectedIndexChanged" style="width:230px"></asp:DropDownList>
                   &nbsp;
                    <asp:Label ID="lblLocation" runat="server" Text="Location     :"></asp:Label>&nbsp;&nbsp;<asp:DropDownList
                    ID="ddlLocation" runat="server" OnDataBound="ddlLocation_DataBound" 
                    onselectedindexchanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True" style="width:150px"></asp:DropDownList>
                 </ContentTemplate>
          </asp:UpdatePanel>
            </td>
             <td align="left"  style="width:50px">
             &nbsp;<asp:LinkButton 
                    runat="server" onclick="btnRxReport_Click" ID="btnRxReport">View</asp:LinkButton></td></tr>--%>
            <tr class="medication_info_tr-odd">
            <td align="center" colspan="5" >
                <rsweb:ReportViewer ID="ReportViewer3" runat="server" 
            Font-Names="Verdana" Font-Size="8pt" Height="380px" Width="1000px" ZoomMode="PageWidth">
          
         </rsweb:ReportViewer>
                 </td>
            </tr>
            </table>
   
</asp:Content>



