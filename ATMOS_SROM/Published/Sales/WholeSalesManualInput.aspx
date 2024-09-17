<%@ Page Language="C#" UICulture="id" Culture="id-ID" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WholeSalesManualInput.aspx.cs" Inherits="ATMOS_SROM.Sales.WholeSalesManualInput" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                    num.substring(num.length - (4 * i + 3));
            return (num);
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
    <%--    <asp:UpdatePanel ID="panelMain" runat="server" DefaultButton="btnInput">
        <ContentTemplate>--%>
    <div id="dmain" runat="server" visible="true">
    </div>

    <h2>
        <asp:Label ID="lbJudul" runat="server" Text="Sales Page (Wholesale) Manual Input"></asp:Label></h2>
    <div id="Div1" runat="server" visible="true">
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <asp:Button ID="btnFileUploadIns" runat="server" Text="Wholesale (Upload File)" OnClick="btnFileUploadIns_Click" />

        <div id="divStore" runat="server">
            <asp:HiddenField ID="hdnIDStore" runat="server" />
            <table>
                <tr>
                    <td>
                        <b>No PO :</b>
                        <asp:Label ID="lblNoPoWarn" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblNoPOEdit" runat="server" Visible="false"></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtNoPo" runat="server" MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoPo" ForeColor="Red"
                            ErrorMessage="Please enter no PO" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>
                    </td>
                    <td id="tdNoSO" runat="server" visible="false">
                        <b>No SO :</b>
                        <asp:Label ID="lblNoSOHide" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td id="tdNoSOValue" runat="server" visible="false">
                        <asp:TextBox ID="txtNoSO" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Tanggal Transaksi :</b>
                        <asp:Label ID="lbTglTrans" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="idSOHeaderEdit" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDate" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendeExtenderTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                            TargetControlID="tbDate" DefaultView="Days">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBDate" runat="server" ControlToValidate="tbDate" ForeColor="Red"
                            ErrorMessage="Please enter tanggal transaksi" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Tanggal Pengiriman :</b>
                        <asp:Label ID="lbTglKirim" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDateKirim" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalenderExtenderKirim" runat="server" Enabled="true" Format="dd-MM-yyyy"
                            TargetControlID="tbDateKirim" DefaultView="Days">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBDateKirim" runat="server" ControlToValidate="tbDateKirim" ForeColor="Red"
                            ErrorMessage="Please enter estimasi tanggal pengiriman barang" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Nama Pembeli :</b>
                        <asp:Label ID="lbNamaPembeli" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbKodePembeli" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <%--                                <asp:TextBox ID="tbNama" runat="server" MaxLength="35"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBNama" runat="server" ControlToValidate="tbNama" ForeColor="Red"
                                    ErrorMessage="Please enter nama pembeli" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlCustSO" runat="server" DataTextField="NM_PEMBELI" DataValueField="KD_PEMBELI"></asp:DropDownList>

                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <b>Kode Pembeli :</b>
                        <asp:Label ID="lbKodePembeli" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbKode" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBKode" runat="server" ControlToValidate="tbKode" ForeColor="Red"
                            ErrorMessage="Please enter kode pembeli" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <b>Margin :</b>
                        <asp:Label ID="lbMargin" runat="server" Visible="false"></asp:Label>
                    </td>
                     <td>
                                <asp:TextBox ID="tbMargin" runat="server" MaxLength="5" ValidationGroup="Wholesale"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBMargin" runat="server" ControlToValidate="tbMargin" ForeColor="Red"
                                    ErrorMessage="Please enter margin" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>
                                <asp:FilteredTextBoxExtender ID="FilterTBMargin" runat="server" Enabled="true"
                                    TargetControlID="tbMargin" FilterType="Custom, Numbers" ValidChars=".,">
                                </asp:FilteredTextBoxExtender>
                            </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="cbRetur" runat="server" Text="Retur" />
                    </td>
                </tr>

            </table>
        </div>
        <br />
        <div id="divRetur" runat="server" visible="false">
            <b>NO SALES ORDER :</b>&nbsp;
            <asp:TextBox ID="tbReturNoSO" runat="server"></asp:TextBox>
        </div>
        <br />
        <asp:Label ID="lblSumQty" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="Wholesale" />
        <br />
        <asp:Button ID="btnInput" runat="server" Text="Input" OnClick="btnInput_Click" ValidationGroup="Wholesale" />
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="false" DataKeyNames="ID"
                OnRowCommand="gvMain_RowCommand" OnPageIndexChanging="gvMain_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                    ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                    ImageUrl="~/Image/b_drop.png" Text="Delete" />&nbsp;
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
                    <asp:BoundField DataField="ART_DESC" HeaderText="Desc" />
                    <asp:BoundField DataField="WARNA" HeaderText="Color" />
                    <asp:BoundField DataField="SIZE" HeaderText="Size" />
                    <asp:BoundField DataField="QTY" HeaderText="Qty" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                    <asp:BoundField DataField="PRICE" HeaderText="@Price" DataFormatString="{0:0,0.00}" />
                    <asp:BoundField DataField="DISCOUNT" HeaderText="Discount(%)" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="NET_PRICE" HeaderText="Net Price" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                    <asp:BoundField DataField="TOTAL_PRICE" HeaderText="TOTAL_PRICE" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />

                    <asp:BoundField DataField="ART_PRICE" HeaderText="Art Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="SPCL_PRICE" HeaderText="Special Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="JENIS_DISCOUNT" HeaderText="Jenis Discount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="ID_ACARA" HeaderText="ID Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="NET_ACARA" HeaderText="Net Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="ID_KDBRG" HeaderText="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="RETUR" HeaderText="Retur" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Margin_Input" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </div>

    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
    <!--Pop Up Search-->
    <asp:Button ID="btnModalSearchItemCode" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalSearchItemCode" runat="server" TargetControlID="btnModalSearchItemCode"
        Drag="true" PopupControlID="PanelSearch" CancelControlID="bSearchClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelSearch" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnSearchSave"
        Style="display: none; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIDSearch" runat="server" />
        <asp:HiddenField ID="hdnIDBayarRetur" runat="server" />
        <table width="100%" cellspacing="4">
            <tr>
                <td></td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="Label3">Search</asp:Label></h2>
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
                    </asp:DropDownList>
                    &nbsp
                    <asp:Button ID="btnSearchSearch" Text="Search" runat="server" OnClick="btnSearchSearch_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <div class="EU_TableScroll" style="display: block">
                        <asp:GridView ID="gvSearch" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvSearch_PageIndexChanging"
                            OnRowCommand="gvSearch_RowCommand"
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
                                <asp:BoundField DataField="ART_DESC" HeaderText="ARTICLE DESCRIPTION" />
                                <asp:BoundField DataField="PRICE" HeaderText="PRICE" />
                                <asp:BoundField DataField="STOCK" HeaderText="Stock" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>

            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSearchSave" runat="server" Text="Save" />
                </td>
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
                    <asp:TextBox ID="tbPUPrice" runat="server" onchange="this.value=formatCurrencyPAY(this.value);" />
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

</asp:Content>
