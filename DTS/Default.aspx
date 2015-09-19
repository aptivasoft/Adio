<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <asp:Button ID="btnBulkCopy" Runat="server" Text="Execute DTS" 
             onclick="btnBulkCopy_Click" />&nbsp;<asp:Button ID="btnImportData" runat="server" Text="Import Data" 
             onclick="btnImportData_Click"  />
        <br />
        <br />
        <asp:Label ID="lblResult" Runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblCounter" Runat="server"></asp:Label>    
         
    </div>
    </form>
</body>
</html>
