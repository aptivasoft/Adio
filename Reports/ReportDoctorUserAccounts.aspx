<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="ReportDoctorUserAccounts.aspx.cs" Inherits="Reports_ReportDoctorUserAccounts" Title="Untitled Page" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<rsweb:ReportViewer ID="rvUserLoginInfo" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="380px" Width="1000px" ZoomMode="PageWidth">
</rsweb:ReportViewer>
</asp:Content>


