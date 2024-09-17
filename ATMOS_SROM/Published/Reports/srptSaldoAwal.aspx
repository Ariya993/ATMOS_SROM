<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="srptSaldoAwal.aspx.cs" Inherits="ATMOS_SROM.Reports.srptSaldoAwal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <h2>
        Menu Movement Report
    </h2>
    <hr />
    <br />
    <div id="DivMessage" runat="server" visible="false">
    </div>

    <table>
        <tr>
            <td>
                Showroom
            </td>
            <td>
                <asp:DropDownList ID="ddlShowroom" runat="server">
                    <asp:ListItem Text="--ALL--" Value=""></asp:ListItem>
                    <asp:ListItem Text="Fred Perry Plaza Senayan - LWIFSS01" Value="LWIFSS01"></asp:ListItem>
                    <asp:ListItem Text="Fred Perry Kota Kasablanka - LWIFSS02" Value="LWIFSS02"></asp:ListItem>
                    <asp:ListItem Text="Fred Perry Plaza Indonesia - LWIFSS03" Value="LWIFSS03"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Periode
            </td>
            <td>
                <asp:TextBox ID="tbStartDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalenderExtenderStart" runat="server" Enabled="true" Format="dd-MM-yyyy"
                TargetControlID="tbStartDate" DefaultView="Days">
                </asp:CalendarExtender>
                between
                <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalenderExtenderEnd" runat="server" Enabled="true" Format="dd-MM-yyyy"
                TargetControlID="tbEndDate" DefaultView="Days">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                Search
            </td>
            <td>
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>&nbsp;
                <asp:DropDownList ID="ddlSearch" runat="server">
                    <asp:ListItem Text="Product" Value="FPRODUK"></asp:ListItem>
                    <asp:ListItem Text="Description" Value="FART_DESC"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnGenerateClick" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
            </td>
        </tr>
    </table>
    <div id="divReport" runat="server" visible="false">
    <h3 align="center" style="font-weight: bold;">
    <asp:Label ID="lbJudul" runat="server" Visible="true"></asp:Label>
    </h3>
        <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            Style="width: 90%;" ShowPrintButton="false" ShowBackButton="false" Visible="false">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
