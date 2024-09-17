<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DummyRpt.aspx.cs" Inherits="ATMOS_SROM.TestDummy.DummyRpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <table>
         <tr>
            <td>
                Periode
            </td>
            <td>
                <asp:TextBox ID="tbStartDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalenderExtenderStart" runat="server" Enabled="true" Format="dd-MM-yyyy"
                TargetControlID="tbStartDate" DefaultView="Days">
                </asp:CalendarExtender>
                &nbsp;
                <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" />
            </td>
        </tr>
    </table>
      <div id="divReport" runat="server" visible="false">
    <h3 align="center" style="font-weight: bold;">
    <asp:Label ID="lbJudul" runat="server" Visible="true"></asp:Label>
    </h3>
        <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            Style="width: 90%;" ShowPrintButton="false" ShowBackButton="false" Visible="false">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
