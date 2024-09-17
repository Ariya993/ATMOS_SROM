<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapStock.aspx.cs" Inherits="ATMOS_SROM.Laporan.LapStock" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .hidden
        {
             display: none;
        }
        
        .divWaiting{
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center; top: 0; left: 0;
            height: 100%;
            width: 100%;
            padding-top:20%;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10" 
        runat="server" AssociatedUpdatePanelID="panelMain">
        <ProgressTemplate>
          <div class="divWaiting">
	        <asp:Image ID="imgWait" runat="server" Width="200px" Height="200px"
	        ImageAlign="Middle" ImageUrl="~/Image/b_loading.gif" />
          </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="panelMain" runat="server" DefaultButton="btnInput" >
        <ContentTemplate>            
            <div id="DivMessage" runat="server" visible="false" />
            <div id="divHeader" runat="server">
                <table>
                    <tr>
                        <td colspan="3">                            
                            <h2> Laporan Stock </h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">                            
                            <h1> Filter </h1>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Bulan
                        </td>
                        <td>
                            <asp:TextBox ID="tbBulanStock" runat="server" />
                            <asp:CalendarExtender ID="CalendeExtenderBulanStock" runat="server" Enabled="true" Format="yyMM" ClientIDMode="Static"
                                TargetControlID="tbBulanStock" DefaultView="Months">
                            </asp:CalendarExtender>&nbsp;
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Kode
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlShowroom" runat="server" AppendDataBoundItems="false" DataTextField="SHOWROOM" DataValueField="KODE" />
                        </td>
                        <td>
                            <asp:Button ID="btnSearchStock" runat="server" Text="Search" OnClick="btnSearchStockClick" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="divStock" runat="server" visible="false">
                <table>
                    <tr>
                        <td colspan="7">
                            <b>Stock Header</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Kode
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="tbKode" runat="server" ReadOnly="true" />
                        </td>
                    </tr><tr>
                        <td>
                            Showroom
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="tbShowroom" runat="server" ReadOnly="true" Width="275px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Saldo Awal
                        </td>
                        <td>
                            Jual
                        </td>
                        <td>
                            Beli
                        </td>
                        <td>
                            Terima
                        </td>
                        <td>
                            Kirim
                        </td>
                        <td>
                            Adjustment
                        </td>
                        <td>
                            Saldo Akhir
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="tbAwal" runat="server" ReadOnly="true" Width="75px" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbJual" runat="server" ReadOnly="true" Width="75px" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbBeli" runat="server" ReadOnly="true" Width="75px" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbTerima" runat="server" ReadOnly="true" Width="75px" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbKirim" runat="server" ReadOnly="true" Width="75px" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbAdjustment" runat="server" ReadOnly="true" Width="75px" />
                        </td>
                        <td>
                            <asp:TextBox ID="tbAkhir" runat="server" ReadOnly="true" Width="75px" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnDetail" runat="server" Text="Lihat Detail Stock" OnClick="btnDetailClick" />
            </div>
            <div id="divDetail" runat="server" visible="false">
                <h3 align="center" style="font-weight: bold;">
                    <asp:Label ID="lbJudul" runat="server" Visible="true"></asp:Label>
                </h3>
                <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                    Style="width: 90%;" ShowPrintButton="false" ShowBackButton="false" Visible="false">
                </rsweb:ReportViewer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
