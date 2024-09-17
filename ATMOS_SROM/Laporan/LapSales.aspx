<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapSales.aspx.cs" Inherits="ATMOS_SROM.Laporan.LapSales" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .hidden
        {
             display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="false"
        ScriptMode="Release">
    </asp:ToolkitScriptManager>
    <h2>
        Laporan Penjualan Toko
    </h2>
    <hr />
    <br />
    <div id="DivMessage" runat="server" visible="false" />

    <asp:Panel ID="panelMain" runat="server" DefaultButton="btnRefreshHeader">
    <div style="border-style:solid; border-color:Black;">   
        <h2><b><asp:Label ID="lbIdJudul" runat="server" Text="Laporan Harian"></asp:Label></b></h2>     
        <br />
        <asp:Table ID="tblLaporanHeader" runat="server" style="margin-bottom: 5px">
            <asp:TableRow>
                <asp:TableCell>
                    Tanggal Transaksi
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbTglTrans" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="CalenderExtTglTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbTglTrans" DefaultView="Days">
                    </asp:CalendarExtender>
                    <asp:FilteredTextBoxExtender ID="FilteredTglTrans" runat="server" Enabled="true"
                        TargetControlID="tbTglTrans" FilterType="Custom" ValidChars="1234567890-">
                    </asp:FilteredTextBoxExtender>
                </asp:TableCell>
                <asp:TableCell>
                    s/d
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbTglEndTrans" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="CalenderExtTglEndTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbTglEndTrans" DefaultView="Days">
                    </asp:CalendarExtender>
                    <asp:FilteredTextBoxExtender ID="FilteredTglEndTrans" runat="server" Enabled="true"
                        TargetControlID="tbTglEndTrans" FilterType="Custom" ValidChars="1234567890-">
                    </asp:FilteredTextBoxExtender>
                    &nbsp;                    
                    <asp:Button ID="btnRefreshHeader" runat="server" OnClick="btnRefreshHeaderClick" Text="Refresh" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Showroom
                </asp:TableCell>
                <asp:TableCell ColumnSpan="3">
                    <asp:TextBox ID="tbShowroom" runat="server" ReadOnly="true" Width="350px" Visible="false"></asp:TextBox>
                    <asp:DropDownList ID="ddlShowroom" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="lbKode" runat="server" Visible="false"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <br />
        
        <div id="divPrintScoot" runat="server" visible="false">
            <asp:Button ID="btnPrintHarian" runat="server" Text="Print Penjualan Harian" OnClick="btnPrintHarianClick" /> &nbsp;
            <asp:Button ID="btnPrintNPT" runat="server" Text="Print NPT" />
        </div>
        <br />

        <asp:Table ID="tblLaporanDetail" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    Total Quantity
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbTotalQty" runat="server" ReadOnly="true" ></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Bruto
                </asp:TableCell>
                <asp:TableCell>
                    Nilai Bayar
                </asp:TableCell>
                <asp:TableCell>
                    DPP
                </asp:TableCell>
                <asp:TableCell>
                    PPN
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbBruto" runat="server" ReadOnly="true" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbNilBayar" runat="server" ReadOnly="true" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbDPP" runat="server" ReadOnly="true" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbPPN" runat="server" ReadOnly="true" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Customer Bayar
                </asp:TableCell>
                <asp:TableCell>
                    CASH
                </asp:TableCell>
                <asp:TableCell>
                    CARD
                </asp:TableCell>
                <asp:TableCell>
                    VOUCHER
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbCustBayar" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbCash" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbCard" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbVoucher" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Debit Card
                </asp:TableCell>
                <asp:TableCell>
                    Kredit Card
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbDebit" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbKredit" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Discount
                </asp:TableCell>
                <asp:TableCell>
                    Margin
                </asp:TableCell>
                <asp:TableCell>
                    Other
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbDisc" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbMargin" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbOthers" ReadOnly="true" runat="server" DataFormatString="{0:0,0.00}"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>                    
                </asp:TableCell>
                <asp:TableCell>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Stock Awal
                </asp:TableCell>
                <asp:TableCell>
                    Stock Akhir
                </asp:TableCell>
                <asp:TableCell>
                    Diff
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbStockAwal" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbStockAkhir" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbDiff" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Penjualan
                </asp:TableCell>
                <asp:TableCell>
                    Terima Barang
                </asp:TableCell>
                <asp:TableCell>
                    Keluar Barang
                </asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbPenjualan" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbTerimaBarang" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbKeluarBarang" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    Terima Pinjam
                </asp:TableCell>
                <asp:TableCell>
                    Keluar Pinjam
                </asp:TableCell>
                <asp:TableCell>
                    Adjustment Manual
                </asp:TableCell>
                <asp:TableCell>
                    Adjustment SO
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="tbTerimaPinjam" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbKeluarPinjam" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbAdjManual" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="tbAdjSO" ReadOnly="true" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </div>
    </asp:Panel>
    <br />
    <table>
        <tr>
            <td colspan="2">
                <h2><b><asp:Label ID="lbJudulDetail" runat="server" Text="Laporan Detail Penjualan"></asp:Label></b></h2>
            </td>
        </tr>
        <tr>
            <td>
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
            <td colspan="2" align="right">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnGenerateClick" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
            </td>
        </tr>
    </table>
    <asp:Panel ID="panelView" runat="server" Visible="false">
        <asp:Button ID="btnPrint" runat="server" Text="Print Excel" OnClick="btnPrintClick" />
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID" 
                PageSize="15" OnPageIndexChanging="gvMainPageChanging" OnRowCommand="gvMainCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" CommandName="SaveRow"
                                    ImageUrl="~/Image/b_ok.png" Text="Edit" />
                             </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                    <asp:BoundField DataField="KODE_CUST" HeaderText="Showroom" /> 
                    <asp:BoundField DataField="KODE" HeaderText="Kode" />
                    <asp:BoundField DataField="TGL_TRANS" HeaderText="Tgl Trans" DataFormatString="{0:dd-MM-yyyy}"/>
                    <asp:BoundField DataField="NO_BON" HeaderText="No Bon" />
                    <asp:BoundField DataField="QTY" HeaderText="Qty" /> 
                    <asp:BoundField DataField="NET_BAYAR" HeaderText="Total Price" DataFormatString="{0:0,0.00}"/>
                    <asp:BoundField DataField="JM_UANG" HeaderText="Pembayaran Cash Awal" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="NET_CARD" HeaderText="Pembayaran Kartu" DataFormatString="{0:0,0.00}"/>
                    <asp:BoundField DataField="NET_CASH" HeaderText="Pembayaran Cash" DataFormatString="{0:0,0.00}"/>
                    <asp:BoundField DataField="KEMBALI" HeaderText="Kembali" DataFormatString="{0:0,0.00}" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="JM_VOUCHER" HeaderText="JM_VOUCHER" DataFormatString="{0:0,0.00}" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalViewDetail" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelDetail" CancelControlID="bVClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelDetail" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIDBayarView" runat="server" />
        <div id="divVMessage" runat="server" visible="false">
        </div>
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="Label3">View Detail</asp:Label></h2>
                    <hr/>
                </td>
                <td align="right">
                    <asp:Button ID="bVClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Search </td>
                <td colspan="2">
                    <asp:TextBox ID="tbViewBy" runat="server" Width="270px" />&nbsp;
                    <asp:DropDownList ID="ddlViewBy" runat="server">
                        <asp:ListItem Value="BARCODE" Text="By Barcode"></asp:ListItem>
                    </asp:DropDownList> &nbsp
                    <asp:Button ID="btnSearchSearch" Text="Search" runat="server" OnClick="btnViewSearchClick" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvView" runat="server" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvViewPageIndexChanging"
                        ShowHeaderWhenEmpty="true" DataKeyNames="ID"
                        CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField ShowHeader="true" HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgSearchSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                            ImageUrl="~/Image/b_ok.png" Text="Save" />
                                     </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="ID_BAYAR" HeaderText="ID_BAYAR" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="KODE_CUST" HeaderText="Kode Cust" />
                            <asp:BoundField DataField="TGL_TRANS" HeaderText="TGL_TRANS" />
                            <asp:BoundField DataField="NO_BON" HeaderText="No Bon" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                            <asp:BoundField DataField="TAG_PRICE" HeaderText="Tag Price" />
                            <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" />
                            <asp:BoundField DataField="NILAI_BYR" HeaderText="Bayar" />
                            <asp:BoundField DataField="FBRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="FART_DESC" HeaderText="Article" />
                            <asp:BoundField DataField="FCOL_DESC" HeaderText="Warna" />
                            <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
                       
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
