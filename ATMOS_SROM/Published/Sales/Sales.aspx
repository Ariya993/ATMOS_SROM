<%@ Page Title="Sales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="ATMOS_SROM.Sales.Sales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function formatCurrency(num) {
            var value = '<% = tbPAYPrice.ClientID %>';
            var bayar = document.getElementById(value).value;
            num = num.toString().replace(',', '');
            bayar = bayar.toString().replace(',', '');
            bayar = bayar.toString().replace('.', '');

            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));


            return (num);
        }

        function numberWithCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
        }

        function thousandSep(val) {
            var value = val.toString().replace(',', '');
            return String(value).split("").reverse().join("")
                          .replace(/(\d{3}\B)/g, "$1,")
                          .split("").reverse().join("");
        }

        function CheckForPastDate(sender, args) {
            var selectedDate = new Date();
            selectedDate = sender._selectedDate;
            var todayDate = new Date();
            if (selectedDate.getDateOnly() > todayDate.getDateOnly()) {

                sender._selectedDate = todayDate;
                sender._textbox.set_Value(sender._selectedDate.format(sender._format));

                alert("Cannot Select Date in the Future");

            }
        }

        function formatCurrencyPAY(num) {
            num = num.toString().replace(',', '');
            num = num.toString().replace(-/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));
            return (num);
        }

        function disableSubmit(obj) {
            obj.disabled = true;
        }

        function notifVoid() {
            var notif = confirm("Are you sure want to void this no Bon?");
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="300"></asp:ScriptManager>

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
            <asp:Panel ID="panelAwal" runat="server" DefaultButton="btnInput">
                <iframe id="iframePDF" visible="false" runat="server" style="display: none;">
    </iframe>
                <div>

                    <h2>
                        <asp:Label ID="lbJudul" runat="server"></asp:Label></h2>
                    <div id="DivMessage" runat="server" visible="false">
                    </div>
                    <asp:HiddenField ID="hdnNoUrutBon" runat="server" />
                    <asp:Button ID="btnRetur" runat="server" Text="Retur / Penukaran barang penjualan" OnClick="btnReturClick" />
                    &nbsp;
                    <asp:Button ID="btnVoid" runat="server" Text="Void" OnClick="btnVoidClick" />
                    &nbsp;
                    <asp:Button ID="btnNoSale" runat="server" Text="NO SALE" ToolTip="Tidak ada penjualan barang hari ini" OnClick="btnNoSaleClick" />
                    &nbsp;
                    <asp:Button ID="btnReprintStruck" runat="server" Text="Reprint Bon" OnClick="btnReprintStruck_Click" />

                    <div id="divStore" runat="server">
                        <asp:HiddenField ID="hdnIDStore" runat="server" />
                        <table>
                            <tr>
                                <td>Tanggal Transaksi :
                                </td>
                                <td>
                                    <asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendeExtenderTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                        TargetControlID="tbDate" DefaultView="Days">
                                    </asp:CalendarExtender>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStore" runat="server" AppendDataBoundItems="false" DataTextField="SHOWROOM" DataValueField="KODE">
                                    </asp:DropDownList>
                                    &nbsp;
                        <asp:CheckBox ID="cbStoreChange" runat="server" ToolTip="Centang agar Store tidak diganti" Text="Don't Change Store" />
                                    &nbsp
                                    <asp:Button ID="btnFindStore" runat="server" Text="Search Store" OnClick="btnFindStore_Click" />
                                </td>
                            </tr>
                            <tr id="trMarginSIS" runat="server">
                                <td>
                                    <b>Discount Margin :&nbsp;</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbMarginSIS" runat="server" MaxLength="6" Width="30px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftbMarginSIS" runat="server" TargetControlID="tbMarginSIS" FilterType="Custom" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lbWarningSIS" Visible="false" runat="server" Text="Pilih Showroom, Margin dan Tanggal Transaksinya dengan benar!" Font-Size="Small" Style="color: red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div id="divRetur" runat="server" visible="false">
                        <b>NO BON :</b>&nbsp;
            <asp:TextBox ID="tbReturNoBon" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                    <div id="divVoid" runat="server" visible="false">
                        <b>NO Bon :</b> &nbsp;
            <asp:TextBox ID="tbVoidNoBon" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick" />
                    <br />
                    <asp:Button ID="btnInput" runat="server" Text="Input" OnClick="btnInput_Click" />
                    <asp:Label ID="lbOtherIncome" runat="server" Visible="false"></asp:Label>
                    <div class="EU_TableScroll" style="display: block">
                        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="false" DataKeyNames="ID"
                            OnRowCommand="gvMain_RowCommand" OnRowDataBound="gvMain_DataBound">
                            <Columns>
                                <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                    <ItemTemplate>
                                        <div>
                                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                                ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                    ImageUrl="~/Image/b_drop.png" Text="Delete" />&nbsp;
                                <asp:ImageButton ID="imgCommand" runat="server" CausesValidation="False" CommandName="CommandRow"
                                    ImageUrl="~/Image/b_comment.png" Text="Command" Visible="false" />
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
                                <asp:BoundField DataField="ART_DESC" HeaderText="Desc" />
                                <asp:BoundField DataField="WARNA" HeaderText="Color" />
                                <asp:BoundField DataField="SIZE" HeaderText="Size" />
                                <asp:BoundField DataField="QTY" HeaderText="Qty" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                                <asp:BoundField DataField="PRICE" HeaderText="@Price" DataFormatString="{0:0,0.00}" />
                                <asp:BoundField DataField="DISCOUNT" HeaderText="Discount(%)" />
                                <asp:BoundField DataField="NET_DISCOUNT" HeaderText="Net Discount" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                                <asp:BoundField DataField="NET_PRICE" HeaderText="Net Price" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="TOTAL_PRICE" HeaderText="TOTAL_PRICE" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                                <asp:BoundField DataField="BARCODE" HeaderText="Barcode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                <asp:BoundField DataField="ART_PRICE" HeaderText="Art Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="SPCL_PRICE" HeaderText="Special Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="JENIS_DISCOUNT" HeaderText="Jenis Discount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ID_ACARA" HeaderText="ID Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="NET_ACARA" HeaderText="Net Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ID_KDBRG" HeaderText="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="RETUR" HeaderText="Retur" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:BoundField DataField="MEMBER" HeaderText="Member" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField HeaderText="Change Price" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbChangePrice" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancelClick" />
                </div>
            </asp:Panel>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

            <!--Pop Up Search No Bon-->
            <asp:Button ID="btnModalSearchNoBon" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalSearchNoBon" runat="server" TargetControlID="btnModalSearchNoBon"
                Drag="true" PopupControlID="PanelBON" CancelControlID="bBONClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelBON" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnBONID" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label4">Search</asp:Label>
                                <asp:Label runat="server" ID="lblFlag" Visible="false"></asp:Label>
                                <%--<asp:Label runat="server" ID="lblVoidNoVoid" Visible="false" Text="-"></asp:Label>--%>
                            </h2>
                            <hr />
                        </td>
                        <td align="right">
                            <asp:Button ID="bBONClose" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Search </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbBONSearch" runat="server" Width="270px" />&nbsp;
                    <asp:DropDownList ID="ddlBONSearch" runat="server">
                        <asp:ListItem Value="NO_BON" Text="By No Bon"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp
                    <asp:Button ID="btnBONSearch" Text="Search" runat="server" OnClick="btnBONSearchClick" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <div class="EU_TableScroll" style="display: block">
                                <asp:GridView ID="gvBON" runat="server" AllowPaging="true" PageSize="20" DataKeyNames="ID"
                                    CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false"
                                    OnRowCommand="gvBONCommand" OnPageIndexChanging="gvBONPageChanging" OnRowDataBound="gvBON_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Action">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:ImageButton ID="imgSearchSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                                        ImageUrl="~/Image/b_ok.png" Text="Save" />
                                                     <asp:ImageButton ID="imgReprint" runat="server" CausesValidation="False" CommandName="Reprint"
                                                        ImageUrl="~/Image/pdf.png" Text="Save" Visible="false"/>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                                        <asp:BoundField DataField="KODE_CUST" HeaderText="Store" />
                                        <asp:BoundField DataField="NO_BON" HeaderText="No Bon" />
                                        <asp:BoundField DataField="TGL_TRANS" HeaderText="Tanggal Transaksi" DataFormatString="{0:dd/MM/yyyy - HH:mm:ss}" />
                                        <asp:BoundField DataField="NET_BAYAR" HeaderText="PRICE" DataFormatString="{0:0,0.00}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnBONSave" runat="server" Text="Save" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Pilih Item untuk Retur-->
            <asp:Button ID="btnModalItemRetur" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalItemRetur" runat="server" TargetControlID="btnModalItemRetur"
                Drag="true" PopupControlID="PanelIRetur" CancelControlID="bIReturClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelIRetur" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; width: 100%;">
                <br />
                <asp:HiddenField ID="hdnIReturIDBayar" runat="server" />
                <asp:HiddenField ID="hdnIReturIDKdbrg" runat="server" />
                <asp:HiddenField ID="hdnIReturTgl" runat="server" />

                <div id="divIReturMessage" runat="server" visible="false"></div>
               
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label5">Item Retur</asp:Label></h2>
                            <hr />
                        </td>
                        <td align="right">
                            <asp:Button ID="bIReturClose" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                             <asp:Label runat="server" ID="lblInfoRetur" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>NO BON :</b> </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbIReturNoBon" ReadOnly="true" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:Button ID="btnIReturVoid" runat="server" Text="Void" OnClick="btnIReturVoidClick" OnClientClick="return notifVoid();" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>STORE :</b> </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbIReturStore" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="lbIReturKode" ReadOnly="true" runat="server" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Tanggal Transaksi :</b> </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtIReturTglTrans" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <div class="EU_TableScroll" style="display: block">
                                <asp:GridView ID="gvIRetur" runat="server" AllowPaging="true" PageSize="50" DataKeyNames="ID"
                                    CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false" OnRowCommand="gvIReturCommand" OnRowDataBound="gvIRetur_RowDataBound" OnPageIndexChanging="gvIRetur_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Action">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:ImageButton ID="imgSearchSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                                        ImageUrl="~/Image/b_ok.png" Text="Save" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                                        <asp:BoundField DataField="ID_BAYAR" HeaderText="id Bayar" SortExpression="ID_BAYAR" Visible="false" />
                                        <asp:BoundField DataField="ID_KDBRG" HeaderText="id Kdbrg" SortExpression="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                                        <asp:BoundField DataField="FBRAND" HeaderText="Brand" />
                                        <asp:BoundField DataField="FART_DESC" HeaderText="Description" />
                                        <asp:BoundField DataField="FCOL_DESC" HeaderText="Color" />
                                        <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />
                                        <asp:BoundField DataField="TAG_PRICE" HeaderText="Tag Price" />
                                        <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" DataFormatString="{0:0,0.00}" />
                                        <asp:BoundField DataField="NILAI_BYR" HeaderText="Price" DataFormatString="{0:0,0.00}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="Button3" runat="server" Text="Save" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Search-->
            <asp:Button ID="btnModalSearchItemCode" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalSearchItemCode" runat="server" TargetControlID="btnModalSearchItemCode"
                Drag="true" PopupControlID="PanelSearch" CancelControlID="bSearchClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelSearch" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnSearchSearch"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnIDSearch" runat="server" />
                <asp:HiddenField ID="hdnIDBayarRetur" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <b>
                                    <asp:Label runat="server" ID="Label3">Search</asp:Label></b></h2>
                            <hr />
                        </td>
                        <td align="right">
                            <asp:Button ID="bSearchClose" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Search </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbSearchBy" runat="server" Width="270px" />&nbsp;
                    <asp:DropDownList ID="ddlSearchBy" runat="server">
                        <asp:ListItem Value="BARCODE" Text="By Barcode"></asp:ListItem>
                        <asp:ListItem Value="ITEM_CODE" Text="By Item Code"></asp:ListItem>
                        <asp:ListItem Value="FGROUP" Text="By Group Item"></asp:ListItem>
                        <asp:ListItem Value="PRICE" Text="By Tag Price"></asp:ListItem>
                        <asp:ListItem Value="FART_DESC" Text="By Description"></asp:ListItem>
                        <asp:ListItem Value="FCOL_DESC" Text="By Description"></asp:ListItem>
                        <asp:ListItem Value="FSIZE_DESC" Text="By Description"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp
                    <asp:Button ID="btnSearchSearch" Text="Search" runat="server" OnClick="btnSearchSearchClick" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <div class="EU_TableScroll" style="display: block">
                                <asp:GridView ID="gvSearch" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvSearchPageIndexChanging"
                                    OnRowCommand="gvSearcRowCommand"
                                    CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Action">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:ImageButton ID="imgSearchSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                                        ImageUrl="~/Image/b_ok.png" Text="Save" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                                        <asp:BoundField DataField="BARCODE" HeaderText="BARCODE" />
                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE" />
                                        <asp:BoundField DataField="FART_DESC" HeaderText="ARTICLE DESCRIPTION" />
                                        <asp:BoundField DataField="FCOL_DESC" HeaderText="COLOR" />
                                        <asp:BoundField DataField="FSIZE_DESC" HeaderText="SIZE" />
                                        <asp:BoundField DataField="PRICE" HeaderText="PRICE" />
                                        <asp:BoundField DataField="BARCODE" HeaderText="BARCODE" Visible="false" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" align="left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Edit Quantity-->
            <asp:Button ID="btnModalChangeQty" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalChangeQty" runat="server" TargetControlID="btnModalChangeQty"
                Drag="true" PopupControlID="PanelPopUp" CancelControlID="bClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPopUp" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPUSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <asp:HiddenField ID="hdnId" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="lblTitleSubPage">Quantity</asp:Label></h2>
                            <hr />
                        </td>
                        <td align="right">
                            <asp:Button ID="bClose" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                    <tr id="trBarcode" runat="server" visible="false">
                        <td></td>
                        <td>Barcode</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPUBarcode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Item Code</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPUItemCode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Description</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPUDesc" ReadOnly="true" runat="server" Width="270px" Height="50px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Size
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPUSize" ReadOnly="true" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Price</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPUPrice" ReadOnly="true" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Quantity</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPUQty" runat="server" MaxLength="2"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextPUQty" runat="server" Enabled="true"
                                TargetControlID="tbPUQty" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnPUSave" runat="server" Text="Save"
                                OnClick="btnPUSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Service-->
            <asp:Button ID="btnModalService" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalService" runat="server" TargetControlID="btnModalService"
                Drag="true" PopupControlID="PanelService" CancelControlID="bSClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelService" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnSSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <asp:HiddenField ID="hdnServiceID" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2><b>
                                <asp:Label runat="server" ID="Label9">Service</asp:Label></b></h2>

                        </td>
                        <td align="right">
                            <asp:Button ID="bSClose" runat="server" Text="Close" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Barcode</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbSBarcode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Description</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbSDesc" MaxLength="100" runat="server" Width="270px" Height="50px" TextMode="MultiLine" />
                            <asp:RequiredFieldValidator ID="reqValidTbSDesc" runat="server" ControlToValidate="tbSDesc" ForeColor="Red"
                                ErrorMessage="Please description" ValidationGroup="Service_Input"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Price</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbSPrice" runat="server" onchange="this.value=formatCurrencyPAY(this.value);" />
                            <asp:FilteredTextBoxExtender ID="Filter" runat="server" Enabled="true"
                                TargetControlID="tbSPrice" FilterType="Custom" ValidChars="0123456789,">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="reqValidTbSPrice" runat="server" ControlToValidate="tbSPrice" ForeColor="Red"
                                ErrorMessage="Please insert price" ValidationGroup="Service_Input"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:CheckBox ID="cbSMinus" runat="server" Text="Minus" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnSSave" runat="server" Text="Save" ValidationGroup="Service_Input" OnClick="btnSSaveClick" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Pilih Cara Pembayaran dan Acara-->
            <asp:Button ID="btnModalPaymentMethodAndAcara" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPaymentMethodAndAcara" runat="server" TargetControlID="btnModalPaymentMethodAndAcara"
                Drag="true" PopupControlID="PanelBYR" CancelControlID="bBYRCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelBYR" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnBYRSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="DivRule" runat="server" class="info" visible="true">
                    <b>Aturan Event</b><br />
                    -Pembayaran Menggunakan Discount <b>Karyawan</b> akan menghilangkan discount promo<br />
                </div>
                <div id="DivBYRMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnIdBYR" runat="server" />
                <asp:HiddenField ID="hdnNoUrut" runat="server" />

                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="lbBYRJudul">Bayar</asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bBYRClose" runat="server" Text="Cancel" Width="100px"
                                OnClick="bBYRClose_Click" Style="height: 26px" />
                            <asp:Button ID="bBYRCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Total Price</b></td>
                        <td colspan="2">
                            <asp:TextBox ID="tbBYRPrice" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Quantity</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbBYRQty" ReadOnly="true" runat="server" Width="50px" />
                        </td>
                    </tr>
                    <tr id="trPayMethod" runat="server" visible="false">
                        <td></td>
                        <td>Payment Method</td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rbBYRPay" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Card" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="Cash" Value="No" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Event Program</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlBYRAcara" runat="server" AppendDataBoundItems="false" DataTextField="NAMA_ACARA" DataValueField="ID_ACARA"
                                OnSelectedIndexChanged="ddlBYRAcara_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trBYRACaraNama" runat="server" visible="false">
                        <td></td>
                        <td>Nama</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbBYRAcaraNama" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trBYRNoID" runat="server" visible="false">
                        <td></td>
                        <td>No ID</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbBYRNoID" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnBYRSave" runat="server" Text="Save" ValidationGroup="Add_Input"
                                UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'"
                                OnClick="btnBYRSave_Click" />&nbsp;
                    <asp:Button ID="btnBYRMember" runat="server" Text="Member" OnClick="btnBYRMemberClick" Visible="false" />
                            <asp:Button ID="btnBYRMemberNew" runat="server" Text="Member" OnClick="btnBYRMemberClick" Enabled="false"/>&nbsp;
                    <asp:Button ID="btnBYREmployee" runat="server" Text="Employee" OnClick="btnBYREmployeeClick" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Pembayaran New Input Cash and Voucher-->
            <asp:Button ID="btnModalNewInputPayment" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalNewInputPayment" runat="server" TargetControlID="btnModalNewInputPayment"
                Drag="true" PopupControlID="PanelNPay" CancelControlID="bNPayCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelNPay" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnNPaySave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="DivNPayMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnNPayNilaiVoucher" runat="server" />
                <asp:HiddenField ID="hdnNPayIDBayar" runat="server" />
                <asp:HiddenField ID="HiddenField3" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label6">PAY</asp:Label></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bNPayClose" runat="server" Text="Cancel" Width="100px"
                                OnClick="bPAYClose_Click" Style="height: 26px" />
                            <asp:Button ID="bNPayCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Total Price</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNPayTotalPrice" ReadOnly="true" runat="server" Width="270px" />
                            <asp:Label ID="lblOriTotalPrice" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Quantity</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNPayQty" ReadOnly="true" runat="server" Width="50px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>No Bon Manual
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNoBonManual" runat="server" MaxLength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Remark
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="TxtFRemark" runat="server" MaxLength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>No Voucher
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNPayNoVou" runat="server" MaxLength="20" />
                            <asp:Label ID="lblNoVoucher" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblSelisihPayVoucher" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblNilaiVoucher" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nilai Voucher
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNPayNilaiVou" runat="server" MaxLength="12" onchange="this.value=formatCurrency(this.value);" Enabled="false"/> 
                            <asp:FilteredTextBoxExtender ID="filterTBNPayNilaiVou" runat="server" Enabled="true"
                                TargetControlID="tbNPayNilaiVou" FilterType="Custom" ValidChars="0123456789,">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>

                    <tr id="trNPayOther" runat="server" visible="false">
                        <td></td>
                        <td>
                            <b>OTHER PAYMENT</b>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNPayOther" runat="server" onchange="this.value=formatCurrency(this.value);" />
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="true"
                                TargetControlID="tbPAYOther" FilterType="Custom" ValidChars="0123456789,">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Ongkos Kirim :</b>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="CbOngkir" runat="server" Text="Ongkos Kirim" OnCheckedChanged="CbOngkir_CheckedChanged" AutoPostBack="true" />
                            <asp:HiddenField ID="HdnOngkir" runat="server" />
                        </td>
                    </tr>
                    <asp:Panel ID="PnlOngkir" runat="server" Visible="false">
                        <tr>
                            <td></td>
                            <td>
                                <b>Biaya Ongkir :</b>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtBiayaOngkir" runat="server" onchange="this.value=thousandSep(this.value);" />
                                <asp:FilteredTextBoxExtender ID="FltrBiayaOngkir" runat="server" Enabled="true"
                                    TargetControlID="txtBiayaOngkir" FilterType="Custom" ValidChars="0123456789,.">
                                </asp:FilteredTextBoxExtender>
                                <asp:HiddenField ID="HdnBiayaOngkir" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <b>Free Ongkir :</b>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtFreeOngkir" runat="server" onchange="this.value=thousandSep(this.value);" />
                                <asp:FilteredTextBoxExtender ID="fltrFreeOngkir" runat="server" Enabled="true"
                                    TargetControlID="txtFreeOngkir" FilterType="Custom" ValidChars="0123456789,.">
                                </asp:FilteredTextBoxExtender>
                                <asp:HiddenField ID="HdnFreeOngkir" runat="server" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td></td>
                        <td>
                            <b>CASH PAY</b>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNPayPay" runat="server" onchange="this.value=thousandSep(this.value);" />
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="true"
                                TargetControlID="tbNPayPay" FilterType="Custom" ValidChars="0123456789,.">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="tbNPayPay" ForeColor="Red"
                                ErrorMessage="Please enter money" ValidationGroup="NPay_Input"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Pay By Card :</b>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="cbNPayCard" runat="server" Text="Pay By Card" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnNPaySave" runat="server" Text="Save" ValidationGroup="NPay_Input" OnClick="btnNPaySaveClick"
                                UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                            &nbsp;
                    <asp:Button ID="btnNPayVoucher" runat="server" Text="Add Voucher" OnClick="btnNPayVoucherClick"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Member-->
            <asp:Button ID="btnModalMember" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalMember" runat="server" TargetControlID="btnModalMember"
                Drag="true" PopupControlID="PanelMember" CancelControlID="bMemberCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelMember" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnMemberSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="divMember" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnMemberID" runat="server" />
                <asp:HiddenField ID="hdnMemberDisc" runat="server" />
                <asp:HiddenField ID="hdnMemberNilai" runat="server" />
                <asp:HiddenField ID="hdnMemberStatus" runat="server" />
                <asp:HiddenField ID="hdnMemberNumber" runat="server" />
                <asp:HiddenField ID="hdncountwrongpin" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label8">MEMBER</asp:Label></h2>
                            <br />
                        </td>
                        <td align="right">
                            <asp:Button ID="bMemberClose" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" OnClick="bMemberCloseClick" />
                            <asp:Button ID="bMemberCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>No Membership :</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbMemberNumber" runat="server" OnTextChanged="tbMemberNumber_TextChanged" AutoPostBack="true" />
                            <%--<asp:FilteredTextBoxExtender ID="FiltertbMemberNumber" runat="server" Enabled="true"
                        TargetControlID="tbMemberNumber" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>--%>
                            <asp:RegularExpressionValidator ID="regexpvald1" runat="server" ControlToValidate="tbMemberNumber" ValidationExpression="^\d{3}-\d{2}-\d{8}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>TOTAL POINT :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTotalPoint" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>DEPOSIT POINT :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblPointDeposit" runat="server" Text="25.000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>REDEEMABLE POINT :
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblredeemablePoint" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>POIN :
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbMemberPoin" runat="server" />
                            <asp:FilteredTextBoxExtender ID="FiltertbMemberPoin" runat="server" Enabled="true"
                                TargetControlID="tbMemberPoin" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>PIN :</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbMemberPIN" runat="server" TextMode="Password" MaxLength="4"/>
                            <asp:FilteredTextBoxExtender ID="FiltertbMemberPIN" runat="server" Enabled="true"
                                TargetControlID="tbMemberPIN" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:Label ID="lblWrongPin" Visible="false" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnMemberSave" runat="server" Text="Save" OnClick="btnMemberSaveClick"
                                UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Staff-->
            <asp:Button ID="btnModalEmployee" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalEmployee" runat="server" TargetControlID="btnModalEmployee"
                Drag="true" PopupControlID="PanelEmployee" CancelControlID="bEmpCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelEmployee" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="DivEmpMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnIDEmp" runat="server" />
                <asp:HiddenField ID="hdnEmpDisc" runat="server" />
                <asp:HiddenField ID="hdnEmpStatus" runat="server" />
                <asp:HiddenField ID="hdnEmpEpc" runat="server" />
                <asp:HiddenField ID="hdnEmpSisaLimit" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2><b>
                                <asp:Label runat="server" ID="lbEmpJudul">EMPLOYEE</asp:Label></b></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bEmpClose" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" OnClick="bEmpCloseClick" />
                            <asp:Button ID="bEmpCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>No Kartu :</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbEmpNoCard" runat="server" OnTextChanged="tbEmpNoCard_TextChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nama Employee :</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbEmpName" runat="server" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Position :</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbEmpPosition" runat="server" Enabled="false" />
                            <asp:TextBox ID="tbEmpTipe" runat="server" Enabled="false" Style="display: none" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td></td>
                        <td>Discount :
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbEmpDisc" runat="server" MaxLength="3" />
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="true"
                                TargetControlID="tbEmpDisc" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnEmpSave" runat="server" Text="Save" OnClick="btnEmpSaveClick"
                                UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Pembayaran New Input Card-->
            <asp:Button ID="btnModalNewCard" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalNewCard" runat="server" TargetControlID="btnModalNewCard"
                Drag="true" PopupControlID="PanelNCard" CancelControlID="bNCardCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelNCard" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPAYSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
               <div id="Div3" runat="server" class="info" visible="true">
                    <b>Aturan Pembayaran Menggunakan Kartu</b><br />
                    - Pembayaran dengan Kartu tambahan, Kartu pertama harus lebih kecil dari total pembayaran!<br />
                </div>
                <div id="DivNCardMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnNCardValueVoucher" runat="server" />
                <asp:HiddenField ID="hdnNCardValueCash" runat="server" />
                <asp:HiddenField ID="HiddenField6" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label7">PAY CARD</asp:Label></h2>
                        </td>
                        <td align="right">
                            <asp:Button ID="bNCardClose" runat="server" Text="Cancel" Width="100px"
                                OnClick="bPAYClose_Click" Style="height: 26px" />
                            <asp:Button ID="bNCardCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Total Price</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNCardPrice" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Quantity</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNCardQty" ReadOnly="true" runat="server" Width="50px" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>Card Number
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNCardNoCard" runat="server" MaxLength="20" />
                            <%--<asp:FilteredTextBoxExtender ID="filterTBNCardNoCard" runat="server" Enabled="true"
                                TargetControlID="tbNCardNoCard" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbNCardNoCard" ForeColor="Red"
                                ErrorMessage="Please enter Card Number" ValidationGroup="NCard_Input"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Kode Card
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlNCardKodeCard" runat="server">
                                <asp:ListItem Text="Debit" Value="Debit"></asp:ListItem>
                                <asp:ListItem Text="Visa" Value="Visa"></asp:ListItem>
                                <asp:ListItem Text="Master" Value="Master"></asp:ListItem>
                                <asp:ListItem Text="Membership" Value="Membership"></asp:ListItem>
                                <asp:ListItem Text="Transfer Bank" Value="TRF_OL"></asp:ListItem>
                                <asp:ListItem Text="Xendit" Value="xendit"></asp:ListItem>
                                <asp:ListItem Text="Midtrans" Value="midtrans"></asp:ListItem>
                                <asp:ListItem Text="QR BCA" Value="QR_BCA"></asp:ListItem>
                                <asp:ListItem Text="QR Mandiri" Value="QR_Mandiri"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Reff Number
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNCardVLCard" runat="server" MaxLength="20" />
                            <%--<asp:FilteredTextBoxExtender ID="filterTBNCardVLCard" runat="server" Enabled="true"
                                TargetControlID="tbNCardVLCard" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbNCardVLCard" ForeColor="Red"
                                ErrorMessage="Please enter Confirmation Code" ValidationGroup="NCard_Input"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Bank Card
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlCardName" runat="server" DataTextField="Nama" DataValueField="Kode"></asp:DropDownList>
                            <%--<asp:TextBox ID="tbNCardBank" runat="server" MaxLength="10" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tbNCardBank" ForeColor="Red"
                                ErrorMessage="Please enter Name Bank Card" ValidationGroup="NCard_Input"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>EDC 
                        </td>
                        <%--<td colspan="2">--%>
                        <td>
                            <asp:DropDownList ID="ddlNCardEdc" runat="server">
                                <%--<asp:ListItem Text="Other" Value="Other" />--%>
                                <asp:ListItem Text="BCA" Value="BCA"></asp:ListItem>
                                <asp:ListItem Text="Mandiri" Value="Mandiri" />
                                <asp:ListItem Text="Virtual Account" Value="VA" />
                                <asp:ListItem Text="Membership" Value="Member" />
                                <asp:ListItem Text="Transfer" Value="TRF" />
                                <asp:ListItem Text="QR" Value="QR" />
                                <asp:ListItem Text="BRI" Value="BRI" />

                                <%--<asp:ListItem Text="Niaga" Value="Niaga" />
                                <asp:ListItem Text="Danamon" Value="Danamon" />
                                <asp:ListItem Text="Permata" Value="Permate" />--%>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSendToEDC" runat="server" Text="To EDC" OnClick="btnSendToEDC_Click" />
                            &nbsp;
                            <asp:Button ID="btnReceiveFromEDC" runat="server" Text="Receive EDC" OnClick="btnReceiveFromEDC_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <b>PRICE CARD</b>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbNCardPay" runat="server" onchange="this.value=thousandSep(this.value);" />
                            <asp:FilteredTextBoxExtender ID="filterTBNCardPay" runat="server" Enabled="true"
                                TargetControlID="tbNCardPay" FilterType="Custom" ValidChars="0123456789,.">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="tbNCardPay" ForeColor="Red"
                                ErrorMessage="Please enter money" ValidationGroup="NCard_Input"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnNCardSave" runat="server" Text="Save" ValidationGroup="NCard_Input"
                                UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'"
                                OnClick="btnNCardSaveClick" />
                            &nbsp;
                    <asp:Button ID="btnNCardCard" runat="server" Text="Add Another Card" OnClick="btnNCardCardClick" ValidationGroup="NCard_Input"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Pembayaran (Input Card Number if Card, Input Pay if Cash)-->
            <asp:Button ID="btnModalInputPayment" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalInputPayment" runat="server" TargetControlID="btnModalInputPayment"
                Drag="true" PopupControlID="PanelPay" CancelControlID="bPAYCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPay" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPAYSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="DivPAYMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnIdPAY" runat="server" />
                <asp:HiddenField ID="hdnShowRoom" runat="server" />
                <asp:HiddenField ID="hdnPAYNilaiVoucher" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label1">PAY</asp:Label></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bPAYClose" runat="server" Text="Cancel" Width="100px"
                                OnClick="bPAYClose_Click" Style="height: 26px" />
                            <asp:Button ID="bPAYCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Total Price</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPAYPrice" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Quantity</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPAYQuantity" ReadOnly="true" runat="server" Width="50px" />
                        </td>
                    </tr>

                    <div id="divPAYCard" runat="server" visible="false">
                        <tr>
                            <td></td>
                            <td>Card Number
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="tbPAYNoCard" runat="server" MaxLength="20" />
                                <asp:FilteredTextBoxExtender ID="tbTextBoxPAYNoCard" runat="server" Enabled="true"
                                    TargetControlID="tbPAYNoCard" FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbPAYNoCard" ForeColor="Red"
                                    ErrorMessage="Please enter Card Number" ValidationGroup="PAY_Input"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Kode Card
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="tbPAYKDCard" runat="server" MaxLength="9" />
                                <asp:FilteredTextBoxExtender ID="tbTextBoxPAYKDCard" runat="server" Enabled="true"
                                    TargetControlID="tbPAYKDCard" FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbPAYKDCard" ForeColor="Red"
                                    ErrorMessage="Please enter Kode Number" ValidationGroup="PAY_Input"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Code Confirmation Card
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="tbPAYVLCard" runat="server" MaxLength="20" />
                                <asp:FilteredTextBoxExtender ID="tbTextBoxPAYVLCard" runat="server" Enabled="true"
                                    TargetControlID="tbPAYVLCard" FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbPAYVLCard" ForeColor="Red"
                                    ErrorMessage="Please enter Confirmation Code" ValidationGroup="PAY_Input"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Bank Card
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="tbPAYBank" runat="server" MaxLength="10" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbPAYBank" ForeColor="Red"
                                    ErrorMessage="Please enter Name Bank Card" ValidationGroup="PAY_Input"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>EDC Number
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="tbPAYEdc" runat="server" MaxLength="7" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPAYEdc" ForeColor="Red"
                                    ErrorMessage="Please enter EDC Number" ValidationGroup="PAY_Input"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </div>
                    <tr>
                        <td></td>
                        <td>No Voucher
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPAYNoVoucher" runat="server" MaxLength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nilai Voucher
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPAYNilaiVoucher" runat="server" MaxLength="12" onchange="this.value=formatCurrency(this.value);" />
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxPAYNilaiVoucher" runat="server" Enabled="true"
                                TargetControlID="tbPAYNilaiVoucher" FilterType="Custom" ValidChars="0123456789,">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>

                    <tr id="trRetur" runat="server" visible="false">
                        <td></td>
                        <td>
                            <b>OTHER PAYMENT</b>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPAYOther" runat="server" onchange="this.value=formatCurrency(this.value);" />
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="true"
                                TargetControlID="tbPAYOther" FilterType="Custom" ValidChars="0123456789,">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>PAY</b>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPAYPAY" runat="server" onchange="this.value=formatCurrency(this.value);" />
                            <asp:FilteredTextBoxExtender ID="tbTextBoxPAYPAY" runat="server" Enabled="true"
                                TargetControlID="tbPAYPAY" FilterType="Custom" ValidChars="0123456789,.">
                            </asp:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPAYPAY" ForeColor="Red"
                                ErrorMessage="Please enter money" ValidationGroup="PAY_Input"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trChange" runat="server" visible="false">
                        <td></td>
                        <td>
                            <b>CHANGE</b>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbPAYChange" runat="server" ReadOnly="true" onchange="this.value=formatCurrency(this.value);" />
                            <asp:FilteredTextBoxExtender ID="tbTextBoxPAYChange" runat="server" Enabled="true"
                                TargetControlID="tbPAYChange" FilterType="Custom" ValidChars="0123456789,-">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnPAYSave" runat="server" Text="Save" ValidationGroup="PAY_Input"
                                OnClick="btnPAYSave_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                            &nbsp;
                    <asp:Button ID="btnPAYVoucher" runat="server" Text="Add Another Voucher" OnClick="btnPAYVoucherClick" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Finish dan Change-->
            <asp:Button ID="btnModalChange" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalChange" runat="server" TargetControlID="btnModalChange"
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
                                <asp:Label runat="server" ID="Label2">No Bon : </asp:Label>
                                <asp:Label runat="server" ID="lbDONEBON"></asp:Label></h2>
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
                        <td colspan="2">Terima Kasih Telah Berbelanja, Kembalian anda :
                            <br />
                            <b>RP :
                                <asp:Label ID="lblDONEChange" runat="server"></asp:Label>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <div>
                                <asp:Button ID="bDONEClose" runat="server" Text="DONE" ValidationGroup="Add_Input"
                                    OnClick="bDONEClose_Click" />
                                <asp:Button ID="bDoneGiftRecipt" runat="server" Text="Gift Receipt" OnClick="bDoneGiftReciptClick" />
                                <a id="bDoneLinkReprint" visible="false" runat="server" target="_blank" href="http://your_url_here.html">Reprint Struck</a>
                                <a id="bDoneLinkStruck" runat="server" target="_blank" href="http://your_url_here.html">Print Struck</a>
                                <a id="bDoneLinkGift" runat="server" target="_blank" href="http://your_url_here.html" style="display: none;">Print Gift Struck</a>
                            </div>
                        </td>
                    </tr>
                    <td colspan="4" class="blueHeader">&nbsp;
                    </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Search Store-->
            <asp:Button ID="btnModalSearchStore" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalSearchStore" runat="server" TargetControlID="btnModalSearchStore"
                Drag="true" PopupControlID="PSearchStore" CancelControlID="bDONECloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PSearchStore" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="bDONEClose"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <div id="Div2" runat="server" visible="false"></div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td colspan="1" align="right">Search Showroom :</td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="tbSearchShowRoom" runat="server"></asp:TextBox>
                            &nbsp
                        <asp:Button ID="btnsearchStore" runat="server" OnClick="btnsearchStore_Click" Text="Search" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsearchStoreClose" runat="server" OnClick="btnsearchStoreClose_Click" Text="Close" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <%--                </table>
                <div id="dGrid" runat="server" visible="true">--%>
                             <div class="EU_TableScroll" style="display: block">
                            <asp:GridView ID="gvShowroom" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="SHOWROOM"
                                PageSize="10" OnRowCommand="gvShowroom_RowCommand" OnPageIndexChanging="gvShowroom_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                        <ItemTemplate>
                                            <div>
                                                <asp:ImageButton ID="imgSelect" runat="server" CausesValidation="False" CommandName="SelectRow"
                                                    ImageUrl="~/Image/b_ok.png" Text="Select" />&nbsp;
                                
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" SortExpression="SHOWROOM" Visible="true" />
                                    <asp:BoundField DataField="KODE" HeaderText="kode" Visible="true" />
                                    <asp:BoundField DataField="STORE" HeaderText="Store" />
                                    <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                                    <asp:BoundField DataField="PHONE" HeaderText="phone" />
                                    <asp:BoundField DataField="ALAMAT" HeaderText="Alamat" />
                                </Columns>
                            </asp:GridView>
                                 </div>
                        </td>
                    </tr>
                    <%--</div>--%>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
