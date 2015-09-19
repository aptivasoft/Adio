<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eCarex User LogIn</title>
    <link rel="stylesheet" type="text/css" href="css/login_style.css" />
    <link rel="stylesheet" type="text/css" href="css/ModalPopUp.css" />
    <script src="javascript/eCareX.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmang" EnablePageMethods="true"  EnablePartialRendering="true" runat="server"/>
        <asp:PlaceHolder ID="phLogin" runat="server"></asp:PlaceHolder>   
    </form>
</body>
</html>
