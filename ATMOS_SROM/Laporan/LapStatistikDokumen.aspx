<%@ Page Language="C#" UICulture="id" Culture="id-ID" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="LapStatistikDokumen.aspx.cs" Inherits="ATMOS_SROM.Laporan.LapStatistikDokumen" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function notifdelete() {
            var notif = confirm("Are you sure you have finish?");
            if (notif) {
                return true;
            }
            else {
                return false;
            }
        }

        function notifNewNoBukti() {
            var notif = confirm("Are you sure want to create New No Bukti?");
            if (notif) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }

        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h2>
        <asp:Label ID="lbIdJudul" runat="server" Text="Statistik Dokumen"></asp:Label></h2>
    <div id="DivMessage" runat="server" visible="false">
    </div>
    <table>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="lblShr" runat="server" Text="Showroom "></asp:Label>
            </td>

            <td>
                <asp:DropDownList ID="ddlStore" runat="server" AppendDataBoundItems="false" DataTextField="SHOWROOM" DataValueField="KODE">
                </asp:DropDownList>
            </td>
            <td rowspan="3" style="width: 30px"></td>
            <td rowspan="3">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtPO_GR" runat="server" GroupName="DocType" Text="Beli/terima dr supplier (GR)" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtTRF_TERIMA" runat="server" GroupName="DocType" Text="Terima" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtRTR_PUTUS" runat="server" GroupName="DocType" Text="Retur putus" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtIN_PINJAM" runat="server" GroupName="DocType" Text="In pinjam" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtADJUSTMENT" runat="server" GroupName="DocType" Text="Adjustment" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbtSALES" runat="server" GroupName="DocType" Text="Jual" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtTRF_KIRIM" runat="server" GroupName="DocType" Text="Kirim" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtJL_PUTUS" runat="server" GroupName="DocType" Text="Jual putus" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtOUT_PINJAM" runat="server" GroupName="DocType" Text="Out pinjam" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtSTOCK_OP" runat="server" GroupName="DocType" Text="Opname" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblBln" runat="server" Text="Bulan "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendeExtenderLockDate" runat="server" Enabled="true" Format="yyMM"
                    TargetControlID="tbDate" DefaultView="Months">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLastClosing" runat="server" Text="last closing "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastClosingValue" runat="server"></asp:Label>

            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnProses" runat="server" Text="Proses" OnClick="btnProses_Click" />
                &nbsp;
                        <asp:Button ID="ExportExcel" runat="server" Text="Export ke Excel" OnClick="ExportExcel_Click" />
                &nbsp;
        <asp:Button ID="btnPopUpAddSHR" runat="server" Visible="false" />

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblttlqty" runat="server" Text="Total Qty: "></asp:Label>
                &nbsp; 
                        <asp:Label ID="lblttlqtyValue" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblttldok" runat="server" Text="Total Dokumen: "></asp:Label>
                &nbsp; 
                        <asp:Label ID="lblttldokvalue" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div class="EU_TableScroll" style="display: block">
        <div id="divPromoShr" runat="server" visible="true">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="id"
                PageSize="10" OnPageIndexChanging="gvMain_PageIndexChanging" OnRowCommand="gvMain_RowCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgSelect" runat="server" CausesValidation="False" CommandName="SelectRow"
                                    ImageUrl="~/Image/b_ok.png" Text="View Detail" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" Visible="false" />
                    <asp:BoundField DataField="No_Dok" HeaderText="No Dokumen" />
                    <asp:BoundField DataField="TGL_Trans" HeaderText="Tanggal Transaksi" DataFormatString="{0:dd-MMMM-yyyy}" />
                    <asp:BoundField DataField="TTL_QTY" HeaderText="Total Qty" />
                    <asp:BoundField DataField="TTL_DOK" HeaderText="Dokumen" />
                    <asp:BoundField DataField="TimeStamp" HeaderText="TIMESTAMP" DataFormatString="{0:dd-MMMM-yyyy HH:mm:ss}" />
                    <asp:BoundField DataField="ID_USER" HeaderText="ID USER" />
                    <asp:BoundField DataField="KD_SHR" HeaderText="KD_SHR" HeaderStyle-CssClass="hidden" Visible="false" />

                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:LinkButton Text="" ID="lnkFake" runat="server" />
    <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" Style="display: block; top: 450px; left: 39px; width: 800px;">
        <%--<div class="header">
            Details
        </div>--%>
        <h1>Lihat Detail
        </h1>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnPrintDetail" runat="server" Text="export ke excel" OnClick="btnPrintDetail_Click" CausesValidation="false"/>
                </td>
                <td style="text-align:right">
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="EU_TableScroll" style="display: block; top: 450px; left: 39px; width: 780px;">
                        <asp:GridView ID="GvDetail" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="barcode"
                            PageSize="10" OnPageIndexChanging="GvDetail_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="barcode" HeaderText="Barcode" />
                                <asp:BoundField DataField="Art_Desc" HeaderText="Art Desc" />
                                <asp:BoundField DataField="Warna" HeaderText="Warna" />
                                <asp:BoundField DataField="Size" HeaderText="Size" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                <asp:BoundField DataField="TGL_Trans" HeaderText="TIMESTAMP" DataFormatString="{0:dd-MM-yyyy HH:mm:ss}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>

        <div class="footer">
        </div>
    </asp:Panel>
    <asp:HiddenField ID="rbtvalue" runat="server" />
    <asp:HiddenField ID="hdnDtStart" runat="server" />
    <asp:HiddenField ID="hdnDtEnd" runat="server" />
    <asp:HiddenField ID="hdnKodeShr" runat="server" />
    <asp:HiddenField ID="hdnIdHeader" runat="server" />
    <asp:HiddenField ID="hdnExcelName" runat="server" />
</asp:Content>
