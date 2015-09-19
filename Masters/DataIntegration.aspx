<%@ Page Language="C#" MasterPageFile="~/Templates/eCareXMaster.master" AutoEventWireup="true" CodeFile="DataIntegration.aspx.cs" Inherits="Patient_dataIntegration" Title="eCarex Health Care System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <br />
    <center>
 <table  align="center"  width="800px"  >
        <tr class="medication_info_th1">
               
                    <td align="center"  colspan="4"  style="height:25px;vertical-align:middle;"   > 
                        <asp:Label ID="lblHeading" runat="server" Text="DATA INTEGRATION"></asp:Label>
                    </td>
                   
            </tr>
            <tr class="medication_info_tr-odd">
 <td align="left">
         <asp:Button ID="btnBulkCopy" Runat="server" Text="Execute DTS" 
             onclick="btnBulkCopy_Click" style=" background-color:#FDF5E6; border-color:Black; border-style:Solid; width:100px; border-width:1px"/>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblResult" Runat="server" ></asp:Label></td>
 </tr>
 <tr class="medication_info_tr-even">
 <td align="left">
 <asp:Button ID="btnImportData" runat="server" Text="Import Data" 
             onclick="btnImportData_Click" Enabled="true"  style=" background-color:#FDF5E6; border-color:Black; border-style:Solid; width:100px; border-width:1px"/>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblResult1" Runat="server" ></asp:Label></td>
 </tr>
 
 </table>

       </center>
        <asp:Label ID="lblCounter" Runat="server"></asp:Label>    
         
    </div>
</asp:Content>

