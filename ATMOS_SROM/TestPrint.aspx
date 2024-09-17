<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPrint.aspx.cs" Inherits="_707SROM.TestPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lbIP" runat="server" Text="IP Address"></asp:Label>
        <asp:TextBox ID="tbIP" runat="server"></asp:TextBox>
        <asp:Button ID="btnPrint" Text="Print" runat="server" OnClick="btnPrintClick" />
    </div>
    </form>
</body>
</html>