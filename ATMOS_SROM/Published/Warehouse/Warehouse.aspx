<%@ Page Title="" Language="C#" UICulture="id" Culture="id-ID" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Warehouse.aspx.cs" Inherits="ATMOS_SROM.Warehouse.Warehouse" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="panelMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" runat="server" Width="200px" Height="200px"
                    ImageAlign="Middle" ImageUrl="~/Image/b_loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="panelMain" runat="server">
        <ContentTemplate>

            <h2>
                <asp:Label ID="lbIdJudul" runat="server" Text="Warehouse"></asp:Label></h2>
            <div id="DivMessage" runat="server" visible="false">
            </div>
            <div>

                <br />
                <div id="divBtnMain" runat="server" visible="true">
                    <asp:Button ID="btnCreateTrfStock" runat="server" Text="Create Transfer Stock" OnClick="btnCreateTrfStockClick"
                        ToolTip="Input pemindahan barang ke toko lain atau warehouse"
                        Style="color: Blue;" Height="44px" Width="181px" />

                    <asp:Button ID="btnCreatePinjam" runat="server" Text="Peminjaman" OnClick="btnCreatePinjamClick"
                        ToolTip="Input peminjaman dari toko ke vendor"
                        Style="color: Blue;" Height="44px" Width="181px" />

                    <asp:Button ID="btnCreateReturSing" runat="server" Text="Returan Singapura"
                        ToolTip="Input returan dari toko ke singapura" OnClick="btnCreateReturSingClick"
                        Style="color: Blue; display: none;" Height="44px" Width="181px" />

                    <asp:Button ID="btnStockOpname" runat="server" Text="Stock Opname"
                        Style="color: Blue; display: none;" Height="44px" Width="181px" />
                </div>
                <asp:Button ID="btnViewTrfStock" runat="server" Text="View Stock" OnClick="btnCreateTrfStockClick" Visible="false"
                    Style="color: Blue;" Height="44px" Width="181px" /><%-- OnClientClick="return notifdelete();"/>--%>
            </div>
            <br />

            <br />
            <div id="divStock" runat="server" visible="true">
                <b>Download Data :&nbsp;</b>
                <asp:TextBox ID="tbDownloadDate" runat="server" Enabled="true"></asp:TextBox>
                <asp:DropDownList ID="ddlDownloadStore" runat="server" DataTextField="SHOWROOM" DataValueField="KODE" AppendDataBoundItems="false"></asp:DropDownList>&nbsp;
    <asp:Button ID="btnDownloadStock" runat="server" Text="Download" OnClick="btnDownloadStockClickNew" ValidationGroup="Download_Stock" />
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" Format="dd-MM-yyyy"
                    TargetControlID="tbDownloadDate" DefaultView="Days">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbDownloadDate" ForeColor="Red"
                    ErrorMessage="Mohon masukan tanggal peminjaman" ValidationGroup="Download_Stock"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="btnAddAdjustment" runat="server" Text="Add Adjustment" OnClick="btnAddAdjustmentClick" ToolTip="Penambahan Stock Adjust Bila Tidak Ada di Master Stock" Visible="false" />&nbsp;  
                <asp:Button ID="btnAdjustment" runat="server" Text="Adjusment Manual" OnClick="btnAdjustmentClick" ToolTip="Adjustment Manual yang dilakukan oleh Inventory Control" Visible="false" />&nbsp;
                <asp:Button ID="btnUploadAdjustment" runat="server" Text="Upload Adjustment" OnClick="btnUploadAdjustmentClick" ToolTip="Upload Adjustment yang dilakukan oleh Inventory Control" Visible="false" />&nbsp;
                <asp:Button ID="btnTrfStock" runat="server" Text="Penerimaan Barang" OnClick="btnTrfStockClick" ToolTip="Menandai kiriman barang dari toko lain atau warehouse" />
                &nbsp;      
                <asp:Button ID="btnListPeminjaman" runat="server" Text="Peminjaman Barang" OnClick="btnListPeminjamanClick" ToolTip="Menandai peminjaman barang dari toko atau warehouse" />
                <br />
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                &nbsp;
    <asp:DropDownList ID="ddlSearch" runat="server">
        <asp:ListItem Text="by Warehouse" Value="WAREHOUSE"></asp:ListItem>
        <asp:ListItem Text="by Barcode" Value="BARCODE"></asp:ListItem>
        <asp:ListItem Text="by Item Code" Value="ITEM_CODE"></asp:ListItem>
        <asp:ListItem Text="by Description" Value="ART_DESC"></asp:ListItem>
    </asp:DropDownList>&nbsp;
    <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearchClick" /><br />
                <div id="divTotal" runat="server">
                    <b>Total Stock : </b>&nbsp;
        <asp:TextBox ID="tbTotalStock" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                <div>
                    <div class="EU_TableScroll" style="display: block">
                        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                            PageSize="10" OnRowCommand="gvMainCommand" OnPageIndexChanging="gvMainPageIndexChanging">
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderText="Action" Visible="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                                ImageUrl="~/Image/b_edit.png" Text="Edit" />
                                            &nbsp;
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow" Visible="false"
                                    ImageUrl="~/Image/b_drop.png" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                                <asp:BoundField DataField="WAREHOUSE" HeaderText="Warehouse" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="STOCK" HeaderText="Stock" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                <asp:BoundField DataField="BARCODE" HeaderText="barcode" />
                                <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                                <asp:BoundField DataField="PRODUK" HeaderText="Produk" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="FGROUP" HeaderText="Group" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ART_DESC" HeaderText="Article Name" />
                                <asp:BoundField DataField="WARNA" HeaderText="Color" />
                                <asp:BoundField DataField="SIZE" HeaderText="Size" />
                                <asp:BoundField DataField="STOCK" HeaderText="Stock" />
                                <asp:BoundField DataField="ID_KDBRG" HeaderText="ID Kdbrg" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="KODE" HeaderText="Kode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div id="divCreateStock" runat="server" style="display: none">
                <asp:HiddenField ID="hdnCreateStock" runat="server" />
                <asp:HiddenField ID="hdnKodeMove" runat="server" />
                <asp:Panel ID="panelCStockHeader" runat="server">
                    <asp:HiddenField ID="hdnIDHeader" runat="server" />
                    <asp:HiddenField ID="hdnIDShowroomKirim" runat="server" />
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnStockGenerate" runat="server" Text="Generate No Bukti" OnClick="btnStockGenerateClick" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>No Bukti :
                            </td>
                            <td>
                                <asp:TextBox ID="tbStockNoBukti" runat="server" ReadOnly="true" Width="190px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Kirim Dari :
                            </td>
                            <td>
                                <asp:TextBox ID="tbStockDari" runat="server" ReadOnly="true" Width="190px"></asp:TextBox>
                                <asp:Label ID="lbStockKodeDari" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbStockIdDari" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>Ke : &nbsp;
                    <asp:TextBox ID="tbStockKe" runat="server" ReadOnly="true" Width="190px"></asp:TextBox>
                                <asp:TextBox ID="tbStockKodeKe" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Waktu Kirim :
                            </td>
                            <td>
                                <asp:TextBox ID="tbStockWaktuKirim" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Upload Transfer Stock :
                            </td>
                            <td>
                                <asp:FileUpload ID="UpdStock" runat="server" Enabled="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnUpdStock" runat="server" Text="Upload Stock" OnClick="btnUpdStockClick" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:HyperLink ID="hplUpdStockExcel" runat="server" Target="_blank" NavigateUrl="~/Upload/Format_Upload_Transfer_Stocks.xlsx">
                                    <asp:Label ID="lbUpdStockExcel" runat="server" Text="Download Format Upload Excel"></asp:Label>
                                </asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%-- Total Qty In Excel : --%><asp:Label ID="lblTtlQtyExcel" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <%-- Total Qty Uploaded :--%>
                                <asp:Label ID="lblTtlQtyUpd" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="panelPinjamHeader" runat="server" Visible="false">
                    <asp:Table ID="tblPinjam" runat="server" BorderStyle="Dashed">
                        <asp:TableHeaderRow BorderStyle="Dashed">
                            <asp:TableHeaderCell ColumnSpan="2" BorderStyle="Dashed">
                        <b>Peminjaman</b>
                            </asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        DARI
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamDari" runat="server" ReadOnly="true" Width="270px"></asp:TextBox>
                                <asp:TextBox ID="tbPinjamDariKode" runat="server" ReadOnly="true" Width="270px" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="tbPinjamDariID" runat="server" ReadOnly="true" Width="270px" Style="display: none;"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        KE
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamKe" runat="server" Width="270px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValidTbPinjamKe" runat="server" ControlToValidate="tbPinjamKe" ForeColor="Red"
                                    ErrorMessage="Mohon masukan tempat yang meminjam" ValidationGroup="Pinjam_Save"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        KODE
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamKode" runat="server" Width="270px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValidTbPinjamKode" runat="server" ControlToValidate="tbPinjamKode" ForeColor="Red"
                                    ErrorMessage="Mohon masukan kode tempat yang meminjam" ValidationGroup="Pinjam_Save"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        NAMA
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamNama" runat="server" Width="270px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValidTbPinjamNama" runat="server" ControlToValidate="tbPinjamNama" ForeColor="Red"
                                    ErrorMessage="Mohon masukan nama orang yang bertanggung jawab" ValidationGroup="Pinjam_Save"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        TELEPHONE
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamPhone" runat="server" Width="270px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValidTbPinjamPhone" runat="server" ControlToValidate="tbPinjamPhone" ForeColor="Red"
                                    ErrorMessage="Mohon masukan no telephone yang bertanggung jawab" ValidationGroup="Pinjam_Save"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        EMAIL
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamEmail" runat="server" Width="270px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValidTbPinjamEmail" runat="server" ControlToValidate="tbPinjamEmail" ForeColor="Red"
                                    ErrorMessage="Mohon masukan email yang bertanggung jawab" ValidationGroup="Pinjam_Save"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        TANGGAL AMBIL
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamAmbil" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarPinjamAmbil" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                    TargetControlID="tbPinjamAmbil" DefaultView="Days">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="reqValidTbPinjamAmbil" runat="server" ControlToValidate="tbPinjamAmbil" ForeColor="Red"
                                    ErrorMessage="Mohon masukan tanggal peminjaman" ValidationGroup="Pinjam_Save"></asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        TANGGAL KEMBALI
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="tbPinjamKembali" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalenderPinjamKembali" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                    TargetControlID="tbPinjamKembali" DefaultView="Days">
                                </asp:CalendarExtender>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                        Status
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="ddlPinjamStatus" runat="server">
                                    <asp:ListItem Text="Kembali" Value="kembali"></asp:ListItem>
                                    <asp:ListItem Text="Tidak Kembali" Value="tidak kembali" />
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>

                <asp:Panel ID="panelReturHeader" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>Waktu Kirim :
                            </td>
                            <td>
                                <asp:TextBox ID="tbReturWaktuKirim" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="ctbReturWaktuKirim" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                    TargetControlID="tbReturWaktuKirim" DefaultView="Days">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="ReqtbReturWaktuKirim" runat="server" ControlToValidate="tbReturWaktuKirim" ForeColor="Red"
                                    ErrorMessage="Mohon masukan tanggal returan" ValidationGroup="Retur_Save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <td>
                            <asp:Button ID="btnReturGetNoBukti" runat="server" Text="Get from No Bukti" />
                        </td>
                        <tr>
                            <td colspan="3">
                                <asp:HyperLink ID="hplReturExcel" runat="server" Target="_blank" NavigateUrl="~/Upload/Format_Upload_Transfer_Stock.xlsx">
                                    <asp:Label ID="lbReturExcel" runat="server" Text="Download Format Upload Excel"></asp:Label>
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;<br />

                <asp:Button ID="btnStockGetItemCode" runat="server" ToolTip="Pilih barang yang akan dikirim" Text="Get Item Code" OnClick="btnStockGetItemCodeClick" />
                <asp:Button ID="btnPinjamGetItemCode" runat="server" ToolTip="Pilih barang yang akan dipinjam" Text="Get Item Code"
                    OnClick="btnStockGetItemCodeClick" ValidationGroup="Pinjam_Save" Visible="false" />
                <asp:Button ID="btnReturGetItemCode" runat="server" ToolTip="Pilih barang yang akan diretur" Text="Get Item Code"
                    OnClick="btnStockGetItemCodeClick" ValidationGroup="Retur_Save" Visible="false" />
                <div>
                    <div class="EU_TableScroll" style="display: block">
                        <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                            PageSize="10" OnRowCommand="gvStockCommand" OnPageIndexChanging="gvStockPageChanging">
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderText="Action" Visible="false">
                                    <ItemTemplate>
                                        <div>
                                            <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                                ImageUrl="~/Image/b_drop.png" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                                <asp:BoundField DataField="ID_KDBRG" HeaderText="id_kdbrg" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ID_HEADER" HeaderText="id_header" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ID_SHOWROOM" HeaderText="id_showroom" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="KODE" HeaderText="Kode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="CREATED_BY" HeaderText="CREATED_BY" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="CREATED_DATE" HeaderText="CREATED_DATE" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="FLAG" HeaderText="Flag" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="STAT" HeaderText="Stat" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                                <asp:BoundField DataField="ART_DESC" HeaderText="Description" />
                                <asp:BoundField DataField="FCOLOR" HeaderText="Color" />
                                <asp:BoundField DataField="FSIZE" HeaderText="Size" />
                                <asp:BoundField DataField="QTY" HeaderText="Qty Kirim" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Button ID="btnStockSave" ToolTip="Save Transfer Stock" runat="server" Text="Save" OnClick="btnStockSaveClick" />
                    <asp:Button ID="btnPinjamSave" ToolTip="Save Peminjaman" runat="server" Text="Save" Visible="false" ValidationGroup="Pinjam_Save" OnClick="btnPinjamSaveClick" />
                    <asp:Button ID="btnReturSave" ToolTip="Save Returan" runat="server" Text="Save" Visible="false" ValidationGroup="Retur_Save" />
                </div>
            </div>

            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

            <!--Pop Up View Transfer Header-->
            <asp:Button ID="btnModalPopupTrfHeader" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupTrfHeader" runat="server" TargetControlID="btnModalPopupTrfHeader"
                Drag="true" PopupControlID="PanelPU" CancelControlID="bPUClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPU" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnIDPU" runat="server" />
                <div id="divPUMessage" runat="server" visible="false" />
                <h2>Detail Transfer Stock</h2>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="tbPUSearch" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlPUSearch" runat="server">
                        <asp:ListItem Text="By No Bukti" Value="NO_BUKTI"></asp:ListItem>
                        <asp:ListItem Text="By Status" Value="STATUS"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnPUSearch" runat="server" Text="Search" Width="100px" OnClick="btnTrfStockClick"
                        Style="height: 26px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bPUClose" runat="server" Text="Close" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvPU" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnRowCommand="gvPUCommand" OnPageIndexChanging="gvPUPageChanging" OnRowDataBound="gvPUDataBound">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                <asp:ImageButton ID="imgPdf" runat="server" CausesValidation="False" CommandName="PrintRow"
                                    ImageUrl="~/Image/pdf.png" Text="Print" />
                                        <asp:ImageButton ID="imgPdfPL" runat="server" CausesValidation="False" CommandName="PrintRowPL"
                                            ImageUrl="~/Image/pdf.png" Text="Print Packing List" ToolTip="Print Packing List" />
                                        <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                            ImageUrl="~/Image/b_drop.png" Text="Delete" ToolTip="Delete Delivery yang salah" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="NO_BUKTI" HeaderText="No Bukti" />
                            <asp:BoundField DataField="DARI" HeaderText="Dari" />
                            <asp:BoundField DataField="KE" HeaderText="Ke" />
                            <asp:BoundField DataField="WAKTU_KIRIM" HeaderText="Waktu Kirim" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="WAKTU_TERIMA" HeaderText="Waktu Terima" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="STATUS" HeaderText="Status" />
                            <asp:BoundField DataField="KODE_DARI" HeaderText="Kode Dari" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KODE_KE" HeaderText="Kode Ke" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!--Pop Up View Item Kirim-->
            <asp:Button ID="btnModalItemKirim" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalItemKirim" runat="server" TargetControlID="btnModalItemKirim"
                Drag="true" PopupControlID="PanelIKirim" CancelControlID="bIKirimCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelIKirim" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: block; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnIKirimIDHeader" runat="server" />
                <asp:HiddenField ID="hdnIKirimStatus" runat="server" />
                <h2>Detail Transfer Stock</h2>
                <div id="divIKirimMessage" runat="server">
                </div>
                <table width="100%">
                    <tr>
                        <td>
                            <b>NO BUKTI :</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbIKirimNoBukti" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>DARI :</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbIKirimDari" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:TextBox ID="tbIKirimKodeDari" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>KE :</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbIKirimKe" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:TextBox ID="tbIKirimKodeKe" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>WAKTU KIRIM :</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbIKirimWaktuKirim" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>WAKTU TERIMA :</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbIKirimWaktuTerima" runat="server" ReadOnly="false"></asp:TextBox>
                            &nbsp;
                    <asp:CalendarExtender ID="CalendarWaktuTerima" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbIKirimWaktuTerima" DefaultView="Days">
                    </asp:CalendarExtender>
                            <asp:FilteredTextBoxExtender ID="filtertbIKirimWaktuTerima" runat="server" TargetControlID="tbIKirimWaktuTerima" FilterType="Custom"
                                ValidChars="0123456789-/" />
                            <asp:Label ID="lbIKirimWaktuTerima" runat="server" Text="Mohon masukan waktu terima" Font-Size="Small" Visible="false"></asp:Label>
                            <asp:Button ID="btnIKirimGenerateWaktuTerima" runat="server" Text="Generate Waktu Terima" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Search :</b>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbIKirimSearch" runat="server" Width="170px"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlIKirimSearch" runat="server">
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnIKirimSearch" runat="server" Text="Search" Width="100px"
                        Style="height: 26px" />&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnIKirimReceiveAll" runat="server" Text="Receive All" Width="100px" OnClick="btnIKirimReceiveAllClick"
                                Style="height: 26px" />&nbsp;&nbsp;
                    <asp:Label ID="lblinfoNoBukti" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="bIKirimClose" runat="server" Text="Close" Width="100px"
                                Style="height: 26px" OnClick="bIKirimCloseClick" />
                            <asp:Button ID="bIKirimCloseHide" runat="server" Text="Close" Width="100px"
                                Style="height: 26px; display: none" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvIKirim" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnRowCommand="gvIKirimRowCommand" OnPageIndexChanging="gvIKirimPageChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="QTY_KIRIM" HeaderText="Qty Kirim" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="QTY_TERIMA" HeaderText="Qty Terima" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="USER_KIRIM" HeaderText="User Kirim" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="REFF" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                            <asp:BoundField DataField="FBRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="FART_DESC" HeaderText="Description" />
                            <asp:BoundField DataField="FCOL_DESC" HeaderText="Color" />
                            <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />

                            <asp:BoundField DataField="QTY_KIRIM" HeaderText="Qty Kirim" />
                            <asp:BoundField DataField="QTY_TERIMA" HeaderText="Qty Terima" />
                            <asp:BoundField DataField="USER_KIRIM" HeaderText="User Kirim" />
                            <asp:BoundField DataField="REFF" HeaderText="Status" />
                            <asp:BoundField DataField="NO_BUKTI" HeaderText="No Bukti" />
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Button ID="btnIKirimDone" Text="Done" runat="server" />
            </asp:Panel>

            <!--Pop Up Generate No Bukti-->
            <asp:Button ID="btnModalGenerateNoBukti" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalGenerateNoBukti" runat="server" TargetControlID="btnModalGenerateNoBukti"
                Drag="true" PopupControlID="PanelQTY" CancelControlID="bQTYCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelQTY" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="DivQTYMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnIdQTY" runat="server" />
                <asp:HiddenField ID="hdnStock" runat="server" />
                <asp:HiddenField ID="hdnIdTujuan" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label1">ALOKASI</asp:Label></h2>
                            <br />
                            <hr style="width: 100%" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bQTYClose" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" />
                            <asp:Button ID="bQTYCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>WAKTU KIRIM
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbQTYWaktu" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendeExtenderQtyWaktu" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                TargetControlID="tbQTYWaktu" DefaultView="Days">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="reqValidQtyWaktu" runat="server" ControlToValidate="tbQTYWaktu" ForeColor="Red"
                                ErrorMessage="Mohon masukan waktu kirim" ValidationGroup="QTY_KIRIM"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>DARI</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbQTYDari" Width="200px" ReadOnly="true" runat="server" />
                            <asp:DropDownList ID="ddlQTYDari" runat="server" DataTextField="SHOWROOM" DataValueField="VALUE" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>KE
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlQTYKE" runat="server" AppendDataBoundItems="false">
                            </asp:DropDownList>
                        </td>
                    </tr>


                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnQTYSave" runat="server" Text="Save" OnClick="btnQTYSaveClick" ValidationGroup="QTY_KIRIM" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Terima-->
            <asp:Button ID="btnModalInputQtyTerima" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalInputQtyTerima" runat="server" TargetControlID="btnModalInputQtyTerima"
                Drag="true" PopupControlID="PanelTRM" CancelControlID="bTRMCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelTRM" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnTRMSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="divTRMMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnTRMIdTrfStock" runat="server" />
                <asp:HiddenField ID="hdnTRMStock" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label2">Transfer Stock</asp:Label></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bTRMClose" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" OnClick="bTRMCloseClick" />
                            <asp:Button ID="bTRMCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>NO BUKTI</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMNoBukti" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ITEM CODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMItemCode" ReadOnly="true" runat="server" Width="270px" />
                            <asp:TextBox ID="tbTRMBarcode" ReadOnly="true" runat="server" Width="270px" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DARI</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMDari" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>KE
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMKe" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>JUMLAH KIRIM
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMQTYKirim" runat="server" ReadOnly="true" Width="75px" />
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="true"
                                TargetControlID="tbTRMQTYKirim" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>JUMLAH TERIMA
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMQTYTerima" runat="server" Width="75px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbTRMQTYTerima" ForeColor="Red"
                                ErrorMessage="Please input quantity" ValidationGroup="Trm_Input"></asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="true"
                                TargetControlID="tbTRMQTYTerima" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Alasan
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMAlasan" runat="server" Width="275px" Height="100px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnTRMSave" runat="server" Text="Save" OnClick="btnTRMSaveClick" ValidationGroup="Trm_Input"
                                UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up View Item Code-->
            <asp:Button ID="btnModalPopupItemCode" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalPopupItemCode" runat="server" TargetControlID="btnModalPopupItemCode"
                Drag="true" PopupControlID="PanelItem" CancelControlID="bItemClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelItem" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <h2>Detail Item Code</h2>
                <div id="divItemMessage" runat="server">
                </div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="tbItemSearch" runat="server" OnTextChanged="tbItemSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlItemSearch" runat="server">
                        <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                        <asp:ListItem Text="By Description" Value="ART_DESC"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnItemSearch" runat="server" Text="Search" Width="100px" OnClick="btnItemSearchClick"
                        Style="height: 26px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bItemClose" runat="server" Text="Close" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnRowCommand="gvItemCommand" OnPageIndexChanging="gvItemPageIndexChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                            ImageUrl="~/Image/b_ok.png" Text="Save" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="ART_DESC" HeaderText="Description" />
                            <asp:BoundField DataField="WARNA" HeaderText="Warna" />
                            <asp:BoundField DataField="SIZE" HeaderText="Size" />
                            <asp:BoundField DataField="WAREHOUSE" HeaderText="Warehouse" />
                            <asp:BoundField DataField="STOCK" HeaderText="Stock" />
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!--Pop Up Input Qty Kirim-->
            <asp:Button ID="btnModalInputQtyKirim" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalInputQtyKirim" runat="server" TargetControlID="btnModalInputQtyKirim"
                Drag="true" PopupControlID="PanelKRM" CancelControlID="bKRMCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelKRM" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnKRMSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="divKRMMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnKRMIdKdbrg" runat="server" />
                <asp:HiddenField ID="hdnKRMStat" runat="server" />
                <asp:HiddenField ID="hdnKRMIdTemp" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label3">Stock Kirim</asp:Label></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bKRMCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" />

                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td></td>
                        <td>NO BUKTI</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMNoBukti" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>BARCODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMBarcode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ITEM CODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMItemCode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DESCRIPTION</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMDesc" ReadOnly="true" runat="server" Width="350px" />
                            <asp:Label ID="lbKRMBrand" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbKRMDesc" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbKRMColor" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbKRMSize" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DARI</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMDari" ReadOnly="true" runat="server" Width="270px" />
                            <asp:Label ID="lbKRMKodeDari" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbKRMIDDari" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>KE
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMKe" ReadOnly="true" runat="server" Width="270px" />
                            <asp:Label ID="lbKRMKodeKe" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>STOCK
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMStock" ReadOnly="true" runat="server" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>JUMLAH KIRIM
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbKRMQTYKirim" runat="server" Width="75px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbKRMQTYKirim" ForeColor="Red"
                                ErrorMessage="Please input quantity" ValidationGroup="Krm_Input"></asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredtbKRMQTYKirim" runat="server" Enabled="true"
                                TargetControlID="tbKRMQTYKirim" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnKRMSave" runat="server" Text="Save" OnClick="btnKRMSaveClick" ValidationGroup="Krm_Input" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Adjustment-->
            <asp:Button ID="btnModalAdjustment" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalAdjustment" runat="server" TargetControlID="btnModalAdjustment"
                Drag="true" PopupControlID="PanelAdj" CancelControlID="bAdjCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelAdj" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnAdjSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="divMessageAdj" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnAdjIdStock" runat="server" />
                <asp:HiddenField ID="hdnAdjIdKdbrg" runat="server" />
                <asp:HiddenField ID="hdnAdjStock" runat="server" />
                <asp:HiddenField ID="hdnAdjType" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="lbAdjJudul">Stock Adjustment</asp:Label></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bAdjCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>WAREHOUSE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAdjWarehouse" ReadOnly="true" runat="server" Width="270px" />
                            <asp:Label ID="lbAdjKode" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>BARCODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAdjBarcode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ITEM CODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAdjItemCode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DESCRIPTION</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAdjDesc" ReadOnly="true" runat="server" Width="350px" Height="50px" TextMode="MultiLine" />
                            <asp:Label ID="lbAdjBrand" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbAdjDesc" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbAdjColor" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbAdjSize" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ALASAN</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAdjAlasan" runat="server" Width="350px" Height="50px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>TANGGAL</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAdjTanggal" runat="server" Width="100px" />
                            <asp:CalendarExtender ID="calendarAdjTanggal" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                TargetControlID="tbAdjTanggal" DefaultView="Days">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>PERBEDAAN
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAdjPerbedaan" runat="server" Width="100px" />
                            <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" ValidChars="0123456789" FilterType="Custom" TargetControlID="tbAdjPerbedaan" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="2">
                            <asp:CheckBox ID="cbAdjMinus" Text="Minus" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnAdjSave" runat="server" Text="Save" OnClick="btnAdjSaveClick" ValidationGroup="Adj_Input" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Adjustment Manual Backdate-->
            <asp:Button ID="btnModalAdjustmentManual" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalAdjustmentManual" runat="server" TargetControlID="btnModalAdjustmentManual"
                Drag="true" PopupControlID="PanelAM" CancelControlID="bAMCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelAM" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnAdjSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="div2" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnAMID" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="3">
                            <h2>
                                <asp:Label runat="server" ID="Label5">Stock Adjustment Manual</asp:Label></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bAMCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>WAREHOUSE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAMWarehouse" ReadOnly="true" runat="server" Width="270px" />
                            <asp:Label ID="lbAMWarehouse" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>BARCODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAMBarcode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ITEM CODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAMItemCode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DESCRIPTION</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAMDesc" ReadOnly="true" runat="server" Width="350px" Height="50px" TextMode="MultiLine" />
                            <asp:Label ID="Label7" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Label8" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Label9" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Label10" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ALASAN</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAMAlasan" runat="server" Width="350px" Height="50px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Adjustment
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbAMAdjustment" runat="server" Width="100px" ValidationGroup="Adj_Input" />
                            <asp:FilteredTextBoxExtender runat="server" ID="filterTbAMAdjustment" ValidChars="0123456789-" FilterType="Custom" TargetControlID="tbAMAdjustment" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnAMSave" runat="server" Text="Save" OnClick="btnAMSave_Click" ValidationGroup="Adj_Input" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up View Adjustment List-->
            <asp:Button ID="btnModalListAdj" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="ModalListAdj" runat="server" TargetControlID="btnModalListAdj"
                Drag="true" PopupControlID="PanelLAdj" CancelControlID="bLAdjClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelLAdj" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <h2>Detail List Adjustment Manual</h2>
                <div id="divLAdj" runat="server">
                </div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="tbLAdjSearch" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlLAdjSearch" runat="server">
                        <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                        <asp:ListItem Text="By Description" Value="ART_DESC"></asp:ListItem>
                        <asp:ListItem Text="By Kode Showroom" Value="KODE"></asp:ListItem>
                        <asp:ListItem Text="By Showroom" Value="SHOWROOM"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnLAdjSearch" runat="server" Text="Search" Width="100px" OnClick="btnAdjustmentClick"
                        Style="height: 26px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bLAdjClose" runat="server" Text="Close" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvLAdj" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnPageIndexChanging="gvLAdjPageIndexChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                            ImageUrl="~/Image/b_ok.png" Text="Save" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="Id KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="ID_WAREHOUSE" HeaderText="Id Warehouse" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KODE" HeaderText="Kode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="ART_DESC" HeaderText="Description" />
                            <asp:BoundField DataField="COLOR" HeaderText="Warna" />


                            <asp:BoundField DataField="SIZE" HeaderText="Size" />
                            <asp:BoundField DataField="STOCK_AWAL" HeaderText="Stock Awal" />
                            <asp:BoundField DataField="STOCK_AKHIR" HeaderText="Stock Akhir" />
                            <asp:BoundField DataField="ADJUSTMENT" HeaderText="Adjusment" />
                            <asp:BoundField DataField="ALASAN" HeaderText="Alasan" />
                            <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!--Pop Up View List Peminjaman Header-->
            <asp:Button ID="btnModalListPeminjaman" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalListPeminjaman" runat="server" TargetControlID="btnModalListPeminjaman"
                Drag="true" PopupControlID="PanelLP" CancelControlID="bLPClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelLP" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <h2>Detail Peminjaman Barang</h2>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="tbLPSearch" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlLPSearch" runat="server">
                        <asp:ListItem Text="By No Bukti" Value="NO_BUKTI"></asp:ListItem>
                        <asp:ListItem Text="By Status" Value="STATUS"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnLPSearch" runat="server" Text="Search" Width="100px" OnClick="btnListPeminjamanClick"
                        Style="height: 26px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bLPClose" runat="server" Text="Close" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvLPeminjaman" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnRowCommand="gvLPeminjamanCommand" OnPageIndexChanging="gvLPeminjamanPageChanging" OnRowDataBound="gvLPeminjaman_RowDataBound">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                            ImageUrl="~/Image/b_ok.png" Text="Save" />
                                        <asp:ImageButton ID="imgPdf" runat="server" CausesValidation="False" CommandName="PrintRow"
                                    ImageUrl="~/Image/pdf.png" Text="Print" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="NO_BUKTI" HeaderText="No Bukti" />
                            <asp:BoundField DataField="DARI" HeaderText="Dari" />
                            <asp:BoundField DataField="KE" HeaderText="Ke" />
                            <asp:BoundField DataField="KODE_DARI" HeaderText="Kode Dari" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KODE_KE" HeaderText="Kode Ke" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                            <asp:BoundField DataField="NAMA" HeaderText="Nama" />
                            <asp:BoundField DataField="PHONE" HeaderText="Phone" />
                            <asp:BoundField DataField="EMAIL" HeaderText="E-Mail" />
                            <asp:BoundField DataField="WAKTU_KIRIM" HeaderText="Waktu Ambil" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="WAKTU_KEMBALI" HeaderText="Waktu Kembali" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="WAKTU_SELESAI" HeaderText="Waktu Actual Kembali" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="STATUS" HeaderText="Status" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!--Pop Up View List Peminjaman Detail-->
            <asp:Button ID="btnModalPeminjamanDetail" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPeminjamanDetail" runat="server" TargetControlID="btnModalPeminjamanDetail"
                Drag="true" PopupControlID="PanelPD" CancelControlID="bPDCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPD" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnIdHeaderPinjam" runat="server" />
                <h2>Detail List Barang Peminjaman</h2>
                <div id="divDetailMessage" runat="server" visible="false" />
                <table>
                    <tr>
                        <td>NO BUKTI
                        </td>
                        <td>
                            <asp:TextBox ID="tbPDNoBukti" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>DARI
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbPDDari" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                            <asp:Label ID="lbPDKodeDari" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>KE
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbPDKe" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>NAMA & KODE PEMINJAM
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbPDNama" runat="server" ReadOnly="true"></asp:TextBox>
                            -
                    <asp:TextBox ID="tbPDKodeKe" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>TELEPON & EMAIL
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbPDPhone" runat="server" ReadOnly="true"></asp:TextBox>
                            -
                    <asp:TextBox ID="tbPDEmail" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>STATUS PEMINJAMAN
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbPDStatus" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>WAKTU PEMINJAMAN
                        </td>
                        <td>
                            <asp:TextBox ID="tbPDWaktuKirim" runat="server" ReadOnly="true"></asp:TextBox>
                            s/d                    
                    <asp:TextBox ID="tbPDWaktuKembali" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>WAKTU PENGEMBALIAN
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbPDWaktuSelesai" runat="server"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="filtertbPDWaktuSelesai" runat="server" Enabled="true"
                                TargetControlID="tbPDWaktuSelesai" FilterType="Custom" ValidChars="1234567890-">
                            </asp:FilteredTextBoxExtender>
                            <asp:CalendarExtender ID="CalendartbPDWaktuSelesai" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                TargetControlID="tbPDWaktuSelesai" DefaultView="Days">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="reqtbPDWaktuSelesai" runat="server" ControlToValidate="tbPDWaktuSelesai" ForeColor="Red"
                                ErrorMessage="Mohon masukan tanggal pengembalian" ValidationGroup="Pinjam_Detail"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>

                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="tbPDSearch" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlPDSeach" runat="server">
                        <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnPDSearch" runat="server" Text="Search" Width="100px" OnClick="btnPDSearchClick"
                        Style="height: 26px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bPDCloseHide" runat="server" Text="Close" Width="100px"
                                Style="height: 26px; display: none" />
                            <asp:Button ID="bPDClose" runat="server" Text="Close" Width="100px" OnClick="btnListPeminjamanClick"
                                Style="height: 26px" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvPinjamDetail" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnRowCommand="gvPinjamDetailCommand" OnPageIndexChanging="gvPinjamDetailPageChanging" OnRowDataBound="gvPinjamDetailDataBound">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow" Visible="false"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" ValidationGroup="Pinjam_Detail" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="ID KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="FART_DESC" HeaderText="Description" />

                            <asp:BoundField DataField="COLOR" HeaderText="Warna" />
                            <asp:BoundField DataField="SIZE" HeaderText="Size" />
                            <asp:BoundField DataField="QTY_KIRIM" HeaderText="Qty Pinjam" />
                            <asp:BoundField DataField="QTY_TERIMA" HeaderText="Qty Kembali" />
                            <asp:BoundField DataField="FLAG" HeaderText="Status Barang" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!--Pop Up Pengembalian-->
            <asp:Button ID="btnModalPengembalianPinjam" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPengembalianPinjam" runat="server" TargetControlID="btnModalPengembalianPinjam"
                Drag="true" PopupControlID="PanelPP" CancelControlID="bPPCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPP" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPPSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="divMessagePP" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnIdPPHeader" runat="server" />
                <asp:HiddenField ID="hdnIdPPDetail" runat="server" />
                <asp:HiddenField ID="hdnIdPPKdbrg" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="3">
                            <h2><b>
                                <asp:Label runat="server" ID="lbPPJudul">Pengembalian Barang Peminjaman</asp:Label></b></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bPPCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                            <asp:Button ID="bPPClose" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" OnClick="btnPDSearchClick" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DARI</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPDari" ReadOnly="true" runat="server" Width="270px" />
                            <asp:Label ID="lbPPDariKode" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>KE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPKe" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>BARCODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPBarcode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ITEM CODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPItemCode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DESCRIPTION</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPDesc" ReadOnly="true" runat="server" Width="350px" Height="50px" TextMode="MultiLine" />
                            <asp:Label ID="lbPPBrand" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbPPDesc" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbPPColor" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lbPPSize" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ALASAN</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPAlasan" runat="server" Width="350px" Height="50px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>QTY KIRIM
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPQtyKirim" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>QTY KEMBALI
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPPQtyKembali" runat="server" Width="100px" />
                            <asp:FilteredTextBoxExtender ID="FiltertbPPQtyKembali" runat="server" Enabled="true"
                                TargetControlID="tbPPQtyKembali" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="reqtbPPQtyKembali" runat="server" ControlToValidate="tbPPQtyKembali" ForeColor="Red"
                                ErrorMessage="Please input qty kembali" ValidationGroup="Stock_Pinjam"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnPPSave" runat="server" Text="Save" OnClick="btnPPSaveClick" ValidationGroup="Stock_Pinjam" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Retur Upload-->
            <asp:Button ID="btnModalUploadRetur" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalUploadRetur" runat="server" TargetControlID="btnModalUploadRetur"
                Drag="true" PopupControlID="PanelUpdRetur" CancelControlID="bUpdRClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelUpdRetur" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPPSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="divUploadRetur" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnSource" runat="server" />
                <asp:HiddenField ID="hdnNamaSource" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="3">
                            <h2><b>
                                <asp:Label runat="server" ID="lbUpdRetJudul">Upload Data</asp:Label></b></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bUpdRClose" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nama File :&nbsp;
                    <asp:Label ID="lbUpdRNamaFile" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnUpdRSave" runat="server" Text="Upload" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Finish-->
            <asp:Button ID="btnModalFinish" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalFinish" runat="server" TargetControlID="btnModalFinish"
                Drag="true" PopupControlID="PanelDone" CancelControlID="bDONECloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelDone" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="bDONEClose"
                Style="display: block; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="Div1" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnIDDone" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="lbTestPrint"></asp:Label>
                                <asp:Label runat="server" ID="Label4" Text="No Bon : ">&nbsp;</asp:Label>
                                <asp:Label runat="server" ID="lblDONEChange"></asp:Label></h2>
                            <br />
                            <hr style="width: 100%" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bDONECloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <div>
                                <asp:Button ID="bDONEClose" runat="server" Text="DONE" ValidationGroup="Add_Input" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                    Style="width: 100%;" ShowPrintButton="false" ShowBackButton="false" Visible="false" ShowRefreshButton="false" ShowFindControls="false" ShowPageNavigationControls="false">
                </rsweb:ReportViewer>
            </div>
            <div>
                <rsweb:ReportViewer ID="ReportViewerPackingList" runat="server" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                    Style="width: 100%;" ShowPrintButton="false" ShowBackButton="false" Visible="false" ShowRefreshButton="false" ShowFindControls="false" ShowPageNavigationControls="false">
                </rsweb:ReportViewer>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdStock" />
        </Triggers>
    </asp:UpdatePanel>

    <!--Pop Up Upload Adjustment-->
    <asp:Button ID="btnModalUploadAdj" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalUploadAdjust" runat="server" TargetControlID="btnModalUploadAdj"
        Drag="true" PopupControlID="PanelUAdj" CancelControlID="bUAdjClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelUAdj" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <div id="dMsgUpAdjust" runat="server" visible="false" />
        <table>
            <tr>
                <td colspan="3">
                    <h1>Upload Stock Adjust</h1>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:HyperLink ID="HyperLink2" Text="Upload Stock Adjust Template" runat="server" Target="_blank" NavigateUrl="~/Upload/STOCK ADJUST.xlsx"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>Store :
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlStoreUpload" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Tanggal :</td>
                <td colspan="2">
                    <asp:TextBox ID="txTanggalAdj" runat="server" Width="100px" />
                    <asp:CalendarExtender ID="CalendarTanggalAdj" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="txTanggalAdj" DefaultView="Days">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>Upload :
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;       
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload Stock Adjust" OnClick="btnFupAdjStock_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    &nbsp
                    <asp:Button ID="bUAdjClose" runat="server" Text="Close" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
