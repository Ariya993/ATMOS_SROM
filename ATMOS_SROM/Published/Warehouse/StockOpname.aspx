<%@ Page Title="Stock Opname" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockOpname.aspx.cs" Inherits="ATMOS_SROM.Warehouse.StockOpname" MaintainScrollPositionOnPostback="true" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="false"
        ScriptMode="Release">
    </asp:ToolkitScriptManager>
    <asp:Panel ID="panelMain" runat="server">
        <h2>Stock Opname</h2>
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <br />
        <asp:Button ID="btnViewAllSO" runat="server" Text="Lihat Hasil Stock Opname" OnClick="btnTrfStockClick" />
        <div id="divUploadSO" runat="server">
            <table>
                <tr>
                    <td>Upload :
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;       
                    </td>
                    <td>
                        <asp:LinkButton ID="DownloadFormatSO" runat="server" Text="Download Format Excel Stock Opname" OnClick="DownloadFormatSO_Click"></asp:LinkButton>
                        <asp:HyperLink ID="HyperLinkDownloadPromo" runat="server" Target="_blank" NavigateUrl="~/Upload/Format_Stock_Opname.txt" Visible="false">
                <asp:Label ID="lbFormatUploadPromo" runat="server" Text="Download Format Excel Promo" Visible="false"></asp:Label>
            </asp:HyperLink>
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
                    <td>Tanggal SO :
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendeExtenderTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                            TargetControlID="tbDate" DefaultView="Days">
                        </asp:CalendarExtender>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload Stock Opname" OnClick="btnUploadClick"
                            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="panelCompare" runat="server" Visible="false" BorderColor="Black" BorderWidth="1px">
            <table>
                <tr>
                    <td>Total Stock :
                    </td>
                    <td>
                        <asp:TextBox ID="tbAllStock" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Nama Warehouse/Showroom :
                    </td>
                    <td>
                        <asp:TextBox ID="tbNamaWarehouse" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Kode Warehouse/Showroom :
                    </td>
                    <td>
                        <asp:TextBox ID="tbKodeWarehouse" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>No Bukti :
                    </td>
                    <td>
                        <asp:TextBox ID="tbNoBukti" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Tanggal SO :
                    </td>
                    <td>
                        <asp:TextBox ID="tbTglSO" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Search :
                    </td>
                    <td>
                        <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                        &nbsp;
                        <asp:DropDownList ID="ddlSearch" runat="server">
                            <asp:ListItem Text="by Warehouse" Value="WAREHOUSE"></asp:ListItem>
                            <asp:ListItem Text="by Barcode" Value="BARCODE"></asp:ListItem>
                            <asp:ListItem Text="by Rak" Value="RAK"></asp:ListItem>
                        </asp:DropDownList>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Order by :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderBy" runat="server">
                            <asp:ListItem Text="by Difference" Value="1" />
                            <asp:ListItem Text="by Article" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearchClick" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnViewTempSODetail" Text="Edit Uploaded SO" runat="server" OnClick="btnViewTempSODetailClick" /><%--Text="View dan Edit Hasil SO"--%>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnPrintAll" Text="Print All Rak" ToolTip="Print Hasil Stock Opname All Rak" runat="server" OnCommand="printStock" />
            <br />
            <asp:DropDownList ID="ddlRakSO" runat="server" DataTextField="RAK" DataValueField="RAK">
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnPrintByRak" Text="Print By Rak" runat="server" OnClick="btnPrintByRakClick" />
            <br />
            <asp:Button ID="btnAdd" Text="Add New Stock SO" runat="server" OnClick="btnAddClick" />
            <asp:HiddenField ID="hdnIdHeader" runat="server" />

            <div>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true"
                        PageSize="10" OnRowCommand="gvMainCommand" OnPageIndexChanging="gvMainPageIndexChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" />
                                        &nbsp;
                                        <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow" OnClientClick="return notifdelete()"
                                            ImageUrl="~/Image/b_drop.png" Text="Delete" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID_HEADER" HeaderText="ID_HEADER" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode SO" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand SO" />
                            <asp:BoundField DataField="ART_DESC" HeaderText="Article SO" />
                            <asp:BoundField DataField="WARNA" HeaderText="Warna SO" />
                            <asp:BoundField DataField="SIZE" HeaderText="Size SO" />
                            <asp:BoundField DataField="STOCK" HeaderText="Stock SO" />

                            <asp:BoundField DataField="IDSTOCK" HeaderText="IDSTOCK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="QTY_STOCK" HeaderText="Stock" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                            <asp:BoundField DataField="DIFF_STOCK" HeaderText="Difference" ItemStyle-BackColor="GreenYellow" HeaderStyle-BackColor="#75c705" />
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <asp:Button ID="btnDone" runat="server" Text="Done" Height="37px" OnClick="btnDoneClick"
                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'"
                    Width="96px" />
            </div>
        </asp:Panel>
    </asp:Panel>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

    <!--Pop Up Add New Item-->
    <asp:ModalPopupExtender ID="ModalUpdate" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelUpdate" CancelControlID="bUpdClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelUpdate" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnUpdSave"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnId" runat="server" />
        <asp:HiddenField ID="hdnIdKdbrg" runat="server" />
        <asp:HiddenField ID="hdnIdStock" runat="server" />
        <div id="DivUpdMessage" runat="server" visible="false">
        </div>
        <br />
        <table width="100%" cellspacing="4">
            <tr>
                <td></td>
                <td colspan="3">
                    <h2>
                        <asp:Label runat="server" ID="lblTitleSubPage" Text="Update Stock/Barcode"
                            Width="100%"></asp:Label>
                    </h2>
                    <hr style="width: 100%;" />
                </td>
                <td align="right">
                    <asp:Button ID="bUpdClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Barcode</td>
                <td colspan="2">
                    <asp:TextBox ID="tbUpdBarcode" runat="server" Width="270px" />
                    <asp:FilteredTextBoxExtender ID="FilteredTextUpdBarcode" runat="server" Enabled="true"
                        TargetControlID="tbUpdBarcode" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>
                    <asp:Label ID="lbUpdBarcode" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Item Code</td>
                <td colspan="2">
                    <asp:TextBox ID="tbUpdItemCode" ReadOnly="true" runat="server" Width="270px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Brand</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlUpdBrand" runat="server" DataTextField="FBRAND" DataValueField="FBRAND"
                        OnTextChanged="ddlUpdBrandChange" AutoPostBack="true" AppendDataBoundItems="false">
                        <asp:ListItem Text="-Pilih Brand-" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Produk</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlUpdProduk" runat="server" DataTextField="FPRODUK" DataValueField="FPRODUK"
                        OnTextChanged="ddlUpdProdukChange" AutoPostBack="true" AppendDataBoundItems="false">
                        <asp:ListItem Text="-Pilih Produk-" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Article</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlUpdArticle" runat="server" DataTextField="FART_DESC" DataValueField="FART_DESC"
                        OnTextChanged="ddlUpdArticleChange" AutoPostBack="true" AppendDataBoundItems="false">
                        <asp:ListItem Text="-Pilih Article-" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lbUpdArticle" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Warna</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlUpdColor" runat="server" DataTextField="FCOL_DESC" DataValueField="FCOL_DESC"
                        OnTextChanged="ddlUpdColorChange" AutoPostBack="true" AppendDataBoundItems="false">
                        <asp:ListItem Text="-Pilih Color-" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lbUpdColor" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Size</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlUpdSize" runat="server" DataTextField="FSIZE_DESC" DataValueField="FSIZE_DESC"
                        OnTextChanged="ddlUpdSizeChange" AutoPostBack="true" AppendDataBoundItems="false">
                        <asp:ListItem Text="-Pilih Size-" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lbUpdSize" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Rak</td>
                <td colspan="2">
                    <asp:TextBox ID="tbUpdRak" runat="server" Width="270px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Stock</td>
                <td colspan="2">
                    <asp:TextBox ID="tbUpdStock" runat="server"></asp:TextBox>
                    <asp:Label ID="lbUpdStock" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbUpdDiffStock" runat="server" Visible="false"></asp:Label>
                    <asp:FilteredTextBoxExtender ID="FilteredTextUpdQty" runat="server" Enabled="true"
                        TargetControlID="tbUpdStock" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3" align="left">
                    <asp:Button ID="btnUpdSave" runat="server" Text="Save"
                        OnClick="btnUpdSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <!--Pop Up View Transfer Header-->
    <asp:ModalPopupExtender ID="ModalPopupTrfHeader" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelPU" CancelControlID="bPUClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPU" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: none; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIDPU" runat="server" />
        <h2>Stock Opname</h2>
        <div id="DivMessageHeader" runat="server" visible="false">
        </div>
        <table width="100%">
            <tr>
                <td>
                    <asp:TextBox ID="tbPUSearch" runat="server"></asp:TextBox>
                    &nbsp;
                    <asp:DropDownList ID="ddlPUSearch" runat="server">
                        <asp:ListItem Text="By No Bukti" Value="NO_BUKTI"></asp:ListItem>
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
                PageSize="10" OnRowCommand="gvPUCommand" OnPageIndexChanging="gvPUPageChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
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
                    <asp:BoundField DataField="NO_BUKTI" HeaderText="No Bukti" />
                    <asp:BoundField DataField="DARI" HeaderText="Dari" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="KE" HeaderText="Tempat" />
                    <asp:BoundField DataField="WAKTU_KIRIM" HeaderText="Tanggal Cut Off" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="WAKTU_TERIMA" HeaderText="Waktu Terima" DataFormatString="{0:dd/MM/yyyy - HH:mm:ss}" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="KODE_DARI" HeaderText="Kode Dari" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="KODE_KE" HeaderText="Kode Ke" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <!--Pop Up View Transfer Detail-->
    <asp:ModalPopupExtender ID="ModalItemKirim" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelIKirim" CancelControlID="bIKirimCloseHide" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelIKirim" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIKirimIDHeader" runat="server" />
        <h2>View Stock Opname Detail</h2>
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
                    <b>TEMPAT :</b>
                </td>
                <td>
                    <asp:TextBox ID="tbIKirimKe" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="tbIKirimKodeKe" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>TANGGAL CUT OFF :</b>
                </td>
                <td>
                    <asp:TextBox ID="tbIKirimWaktuKirim" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td></td>
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
                    <asp:Button ID="btnIKirimSearch" runat="server" Text="Search" Width="100px" OnClick="btnIKirimSearchClick"
                        Style="height: 26px" />
                </td>
                <td align="right">
                    <asp:Button ID="bIKirimClose" runat="server" Text="Close" Width="100px"
                        Style="height: 26px" OnClick="btnTrfStockClick" />
                    <asp:Button ID="bIKirimCloseHide" runat="server" Text="Close" Width="100px"
                        Style="height: 26px" Visible="false" />
                </td>
            </tr>
        </table>
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvIKirim" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                PageSize="10" OnPageIndexChanging="gvIKirimPageChanging">
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
                    <asp:BoundField DataField="ID_KDBRG" HeaderText="ID KDBRG" />
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                    <asp:BoundField DataField="QTY_KIRIM" HeaderText="Qty Kirim" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="QTY_TERIMA" HeaderText="Perbedaan Qty" />
                    <asp:BoundField DataField="STOCK_AKHIR_KIRIM" HeaderText="Stock Sebelum SO" />
                    <asp:BoundField DataField="STOCK_AKHIR_TERIMA" HeaderText="Stock Setelah SO" />
                    <asp:BoundField DataField="USER_KIRIM" HeaderText="User" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="btnIKirimDone" Text="Done" runat="server" />
    </asp:Panel>

    <!--Pop Up View Temp Transfer Detail-->
    <asp:ModalPopupExtender ID="ModalTempDetail" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelTempKirim" CancelControlID="bTempCloseHide" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelTempKirim" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <h2>View Stock Opname Detail</h2>
        <div id="div1" runat="server">
        </div>
        <table width="100%">
            <tr>
                <td>
                    <b>Search :</b>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbTempSearch" runat="server" Width="170px"></asp:TextBox>
                    &nbsp;
                    <asp:DropDownList ID="ddlTempSearch" runat="server">
                        <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                        <asp:ListItem Text="By Rak" Value="RAK"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnTempSearch" runat="server" Text="Search" Width="100px" OnClick="btnViewTempSODetailClick"
                        Style="height: 26px" />
                </td>
                <td align="right">
                    <asp:Button ID="bTempClose" runat="server" Text="Close" Width="100px" OnClick="btnSearchClick"
                        Style="height: 26px" />
                    <asp:Button ID="bTempCloseHide" runat="server" Text="Close" Width="100px"
                        Style="height: 26px" Visible="false" />
                </td>
            </tr>
        </table>
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvTemp" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                PageSize="10" OnRowCommand="gvTempCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                    ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow" OnClientClick="return notifdelete()"
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
                    <asp:BoundField DataField="ID_HEADER" HeaderText="ID Header" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="ID_KDBRG" HeaderText="ID KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RAK" HeaderText="Rak" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item COde" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                    <asp:BoundField DataField="FGROUP" HeaderText="Group" />
                    <asp:BoundField DataField="ART_DESC" HeaderText="Article Desc" />
                    <asp:BoundField DataField="WARNA" HeaderText="Warna" />
                    <asp:BoundField DataField="SIZE" HeaderText="Size" />
                    <asp:BoundField DataField="STOCK" HeaderText="Qty" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <!--Pop Up Edit Temp Item-->
    <asp:ModalPopupExtender ID="ModalTempEdit" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelTEdit" CancelControlID="bTEditClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelTEdit" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnUpdSave"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnTEditID" runat="server" />
        <asp:HiddenField ID="hdnTEditIDKdbrg" runat="server" />
        <asp:HiddenField ID="hdnTEditIDStock" runat="server" />
        <div id="Div2" runat="server" visible="false">
        </div>
        <br />
        <table width="100%" cellspacing="4">
            <tr>
                <td></td>
                <td colspan="3">
                    <h2>
                        <asp:Label runat="server" ID="lbTEditJudul" Text="Update Qty/Barcode"
                            Width="100%"></asp:Label>
                    </h2>
                    <hr style="width: 100%;" />
                </td>
                <td align="right">
                    <asp:Button ID="bTEditClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Barcode</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditBarcode" runat="server" Width="270px" />
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="true"
                        TargetControlID="tbTEditBarcode" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>
                    <asp:Label ID="lbTEditBarcode" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbTEditItemCode" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>

            <tr>
                <td></td>
                <td>Brand</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditBrand" runat="server" Width="270px" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Group</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditGroup" runat="server" Width="270px" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Article</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditArticle" runat="server" Width="270px" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Warna</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditWarna" runat="server" Width="270px" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Size</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditSize" runat="server" Width="270px" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Rak</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditRak" runat="server" Width="270px" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Stock</td>
                <td colspan="2">
                    <asp:TextBox ID="tbTEditStock" runat="server"></asp:TextBox>
                    <asp:Label ID="lbTEditStock" runat="server" Visible="false"></asp:Label>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="true"
                        TargetControlID="tbTEditStock" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3" align="left">
                    <asp:Button ID="btnTEditSave" runat="server" Text="Save"
                        OnClick="btnTEditSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <%-- Tambahan Untuk Haspus Data Stock Opname --%>

    <%--<table>
        <tr>
            <td>
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlSearchStockOp" runat="server">
                    <asp:ListItem Text="By No Bukti" Value="h.NO_BUKTI"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btSearch" runat="server" Value="Search" />
            </td>
            <td>
                <asp:Button ID="btDelete" runat="server" Value="Delete" />
            </td>
        </tr>
    </table>--%>

    <div id="dGvStockOPData" runat="server">
        <%--<div class="EU_TableScroll" style="display: block">--%>
        <asp:GridView ID="gvSockOPData" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
            PageSize="10" OnRowCommand="gvSockOPData_RowCommand" OnPageIndexChanging="gvSockOPData_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                <asp:BoundField DataField="ID_HEADER" HeaderText="ID Header" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                <asp:BoundField DataField="ID_KDBRG" HeaderText="ID KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                <asp:BoundField DataField="RAK" HeaderText="Rak" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item COde" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                <asp:BoundField DataField="FGROUP" HeaderText="Group" />
                <asp:BoundField DataField="ART_DESC" HeaderText="Article Desc" />
                <asp:BoundField DataField="WARNA" HeaderText="Warna" />
                <asp:BoundField DataField="SIZE" HeaderText="Size" />
                <asp:BoundField DataField="STOCK" HeaderText="Qty" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
