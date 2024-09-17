<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPrintDummy.aspx.cs" Inherits="ATMOS_SROM.TestDummy.ReportPrintDummy" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
            </asp:ScriptManager>

           <asp:TextBox ID="txtNoPO" runat="server"></asp:TextBox>
              
            &nbsp 
            <asp:Button ID="btnPrint" runat="server" OnClick="btnDirectPrint_Click" Text="Print" />
            <br />

            <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                Style="width: 90%;" ShowPrintButton="false" ShowBackButton="false" Visible="true">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
