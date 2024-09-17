<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RptStock.aspx.cs" Inherits="ATMOS_SROM.Report.RptStock" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <h2>
        Menu Report Stock
    </h2>
    <hr />
    <br />
    <div id="DivMessage" runat="server" visible="false">
    </div>

    <div id="divReport" runat="server" visible="false" >
    <h3 align="center" style="font-weight: bold">
    <asp:Label ID="lbJudul" runat="server" Visible="true"></asp:Label>
    </h3>
        <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            Style="width: 100%;" ShowPrintButton="false" ShowBackButton="false" Visible="false">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
