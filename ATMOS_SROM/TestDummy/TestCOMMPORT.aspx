<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCOMMPORT.aspx.cs" Inherits="ATMOS_SROM.TestDummy.TestCOMMPORT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtamount" runat="server" Text="0"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtaddamount" runat="server" Text="0"></asp:TextBox>
        <br />
        <asp:Button ID="btnSendEDC" runat="server" Text="Send To EDC" OnClick="btnSendEDC_Click" />
        <br />
    <asp:Label ID="lblCOMPORT" runat="server" Text="PORT : ">
    </asp:Label>
        <asp:Label ID="lblPORT" runat="server"></asp:Label>
        <br />

        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
