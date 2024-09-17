<%@ Page Language="C#" UICulture="id" Culture="id-ID" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WholeSalesNew.aspx.cs" Inherits="ATMOS_SROM.Sales.WholeSalesNew" %>

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

    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="panelMain">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" runat="server" Width="200px" Height="200px"
                    ImageAlign="Middle" ImageUrl="~/Image/b_loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="panelMain" runat="server" DefaultButton="btnInput">
        <ContentTemplate>
            <div id="dmain" runat="server" visible="true">
                <h2>
                    <asp:Label ID="lbJudul" runat="server" Text="Sales Page (Wholesale)"></asp:Label></h2>
                <div id="DivMessage" runat="server" visible="false">
                </div>
                <asp:Button ID="btnRetur" runat="server" Text="Retur" Visible="false" OnClick="btnReturClick" />
                <asp:Button ID="btnAllWholeSale" runat="server" Text="List Sale Wholesale" OnClick="btnAllWholeSaleClick" />
                <asp:Button ID="btnCloseSO" runat="server" Text="List SO Wholesale" OnClick="btnCloseSO_Click" />
                <asp:Button ID="btnInsToShPutus" runat="server" Text="Send SO" OnClick="btnInsToShPutus_Click" />
                <asp:Button ID="btnManualIns" runat="server" Text="Manual Input" OnClick="btnManualIns_Click" />
                <%--        <a id="bManualIns" visible="true" runat="server" target="_blank" href="WholeSalesManualInput.aspx">Manual Input</a>--%>

                <div id="divStore" runat="server">
                    <asp:HiddenField ID="hdnIDStore" runat="server" />
                    <table>
                    <tr>
                            <td>
                                <b>No PO :</b>
                                <asp:Label ID="lblNoPoWarn" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNoPo" runat="server" MaxLength="25"></asp:TextBox>
                                <asp:Label ID="lblNoPOEdit" runat="server" Visible="false"></asp:Label>
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
                        
                       <%-- <tr>
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
                                <b>Nama Pembeli :</b>
                                <asp:Label ID="lbNamaPembeli" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbKodePembeli" runat="server" Visible="false"></asp:Label>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCustSO" runat = "server" DataTextField="NM_PEMBELI" DataValueField ="KD_PEMBELI"></asp:DropDownList>

                                <%--<asp:TextBox ID="tbNama" runat="server" MaxLength="35"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBNama" runat="server" ControlToValidate="tbNama" ForeColor="Red"
                                    ErrorMessage="Please enter nama pembeli" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        
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
                        <tr>
                            <td colspan="2">
                                <asp:FileUpload ID="flUpload" runat="server" />&nbsp;
                        <asp:Button ID="btnFileUpload" runat="server" Text="Upload" OnClick="btnUploadClick" UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
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
                <asp:TextBox ID="tbBarcode" runat="server" Visible="false"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick" ValidationGroup="Wholesale" Visible="false" />
                <br />
                <asp:Button ID="btnInput" runat="server" Text="Input" OnClick="btnInput_Click" ValidationGroup="Wholesale" Visible="false" />
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        OnRowCommand="gvMain_RowCommand" OnPageIndexChanging="gvMain_PageChanging" PageSize="10">
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
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancelClick" />
            </div>

            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <!--Pop Up Close SO-->
            <asp:Button ID="btnModalPopupCloseSO" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupCloseSO" runat="server" TargetControlID="btnModalPopupCloseSO"
                Drag="true" PopupControlID="PanelPopupCloseSO" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPopupCloseSO" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <div>
                    <asp:Label ID="lblJdulListSO" runat="server" Text="List SO Wholesale" Font-Bold="true" Font-Size="Medium"></asp:Label>
                </div>
                <div id="Div2" runat="server"></div>
                <br />
                 
                <asp:TextBox ID="txtSearchCloseSO" runat="server"></asp:TextBox>
                &nbsp
                <asp:Button ID="btnSearchCloseSO" runat="server" Text="Search" OnClick="btnSearchCloseSO_Click" />
                &nbsp
        <asp:Button ID="btnSearchCloseSOCancel" runat="server" Text="Close" OnClick="btnSearchCloseSOCancel_Click" />
                <br />
               
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvCloseSo" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvCloseSo_PageIndexChanging"
                        OnRowCommand="gvCloseSo_RowCommand" DataKeyNames="NO_SO" OnRowDataBound="gvCloseSo_RowDataBound"
                        CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField ShowHeader="true" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgSearchSave" runat="server" CausesValidation="False" CommandName="CloseSo"
                                            ImageUrl="~/Image/b_ok.png" Text="Save" />
                                        &nbsp;
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditSo"
                                            ImageUrl="~/Image/b_edit.png" Text="Save" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NO_SO" HeaderText="NO_SO" SortExpression="NO_SO" />
                            <asp:BoundField DataField="NO_PO" HeaderText="NO_SCAN" />
                            <asp:BoundField DataField="KODE_CUST" HeaderText="KODE_CUST" />
                            <asp:BoundField DataField="TGL_TRANS" HeaderText="TGL_TRANS" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="SEND_DATE" HeaderText="SEND_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="QTY" HeaderText="QTY" />
                            <asp:BoundField DataField="QTY_REAL_1" HeaderText="QTY_REAL_1" />
                            <asp:BoundField DataField="TGL_KIRIM_1" HeaderText="TGL_KIRIM_1" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="QTY_REAL_2" HeaderText="QTY_REAL_2" />
                            <asp:BoundField DataField="TGL_KIRIM_2" HeaderText="TGL_KIRIM_2" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="FRETUR" HeaderText="RETUR" />
                            <asp:BoundField DataField="NO_BON" HeaderText="NO_BON" />
                            <asp:BoundField DataField="STATUS_HEADER" HeaderText="STATUS" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <!--Pop Up So To SH Putus-->
            <asp:Button ID="btnModalPopupSoToShPutusSrch" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupSoToShPutusSrch" runat="server" TargetControlID="btnModalPopupSoToShPutusSrch"
                Drag="true" PopupControlID="PanelSoToShPutusSrch" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelSoToShPutusSrch" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <div id="Dsearch" runat="server">
                    <div id="dMsgSO" runat="server" visible="false"></div>
                    <br />
                    <asp:TextBox ID="txtSearchSO" runat="server"></asp:TextBox>
                    &nbsp
                    <asp:Button ID="btnSearchSo" runat="server" Text="Search" OnClick="btnSearchSo_Click" />
                    &nbsp
                    <asp:Button ID="btnSearchSoCancel" runat="server" Text="Close" OnClick="btnSearchSoCancel_Click" />
                    <br />
                    <div class="EU_TableScroll" style="display: block">
                        <asp:GridView ID="gvSo" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvSo_PageIndexChanging"
                            OnRowCommand="gvSo_RowCommand" DataKeyNames="ID" OnRowDataBound="gvSo_RowDataBound"
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
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="NO_SO" HeaderText="NO_SO" SortExpression="NO_SO" />
                                <asp:BoundField DataField="NO_SCAN" HeaderText="NO_SCAN" />
                                <asp:BoundField DataField="QTY" HeaderText="QTY" />
                                <asp:BoundField DataField="QTY_REAL_1" HeaderText="QTY_REAL" />
                                <asp:BoundField DataField="TGL_REAL_1" HeaderText="TGL_REAL" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="TGL_KIRIM_1" HeaderText="TGL_KIRIM" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="STATUS_HEADER" HeaderText="STATUS SO" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!--Pop Up So To SH Putus Insert-->
                <%--  <asp:Button ID="btnModalPopupSHPutusIns" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupSHPutusIns" runat="server" TargetControlID="btnModalPopupSHPutusIns"
        Drag="true" PopupControlID="PanelSHPutusIns" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>--%>
                <div id="dShPutus" runat="server" visible="false">
                    <%--    <asp:Panel ID="PanelSHPutusIns" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" 
        Style="display: none; top: 684px; left: 39px; width: 80%;">--%>
                    <br />
                    <table>
                        <tr>
                            <td>NO PO 
                            </td>
                            <td>:
                                <asp:Label ID="lblnopo" runat="server"></asp:Label>
                                <asp:Label ID="lblNoScan" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblIdHeader" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td width="15%"></td>
                            <td>NO SO 
                            </td>
                            <td>:
                                <asp:Label ID="lblnoso" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>KODE CUST 
                            </td>
                            <td>:
                                <asp:Label ID="lblKodeCust" runat="server"></asp:Label>
                            </td>
                            <td width="15%"></td>
                            <td>KODE 
                            </td>
                            <td>:
                                <asp:Label ID="lblKode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL TRANSAKSI 
                            </td>
                            <td>:
                                <asp:Label ID="lblTglTrans" runat="server"></asp:Label>
                            </td>
                            <td width="15%"></td>
                            <td>PERKIRAAN TGL KIRIM 
                            </td>
                            <td>:
                                <asp:Label ID="lblTglKirim" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>MARGIN 
                            </td>
                            <td>:
                                <asp:Label ID="lblMargin" runat="server"></asp:Label>
                            </td>
                            <td width="15%"></td>
                            <td>RETUR 
                            </td>
                            <td>:
                                <asp:Label ID="lblFRetur" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnInsShPutus" runat="server" Text="Kirim SO" OnClick="btnInsShPutus_Click" />
                            </td>
                            <td width="15%"></td>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnInsShPutusCancel" runat="server" Text="Cancel" OnClick="btnInsShPutusCancel_Click" />
                            </td>
                        </tr>
                    </table>
                    <div class="EU_TableScroll" style="display: block">
                        <asp:GridView ID="gvDetailSO" runat="server" AllowPaging="false" PageSize="20" OnPageIndexChanging="gvDetailSO_PageIndexChanging"
                            OnRowCommand="gvDetailSO_RowCommand" DataKeyNames="ID"
                            CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="NO_SO" HeaderText="NO_SO" SortExpression="NO_SO" />
                                <asp:BoundField DataField="QTY" HeaderText="QTY" />
                                <asp:BoundField DataField="BARCODE" HeaderText="BARCODE" />
                                <asp:BoundField DataField="PRICE" HeaderText="PRICE" />
                                <asp:BoundField DataField="QTY_REAL_1" HeaderText="QTY_REAL 1" />
                                <asp:BoundField DataField="TGL_REAL_1" HeaderText="TGL_REAL 1" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="TGL_KIRIM_1" HeaderText="TGL_KIRIM 1" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="QTY_REAL_2" HeaderText="QTY_REAL 2" />
                                <asp:BoundField DataField="TGL_REAL_2" HeaderText="TGL_REAL 2" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="TGL_KIRIM_2" HeaderText="TGL_KIRIM 2" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM_CODE" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
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

            <!--Pop Up Finish dan Change-->
            <asp:Button ID="btnModalChange" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalChange" runat="server" TargetControlID="btnModalChange"
                Drag="true" PopupControlID="PanelDone" CancelControlID="bDONECloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelDone" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="bDONEClose"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="Div1" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnIDDone" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label2">No SO : </asp:Label>
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
                        <td colspan="2">Total Penjualan :
                            <br />
                            <b>RP :
                                <asp:Label ID="lblDONEChange" runat="server"></asp:Label>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="bDONEClose" runat="server" Text="DONE" ValidationGroup="Add_Input"
                                OnClick="bDONEClose_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

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
                                <asp:Label runat="server" ID="Label4">Search</asp:Label></h2>
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
                        <asp:ListItem Value="NO_SO" Text="By No Order"></asp:ListItem>
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
                                    OnRowCommand="gvBONCommand" OnPageIndexChanging="gvBONPageChanging">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Action">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="SaveRow"
                                                        ImageUrl="~/Image/b_ok.png" Text="Save" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                                        <asp:BoundField DataField="KODE" HeaderText="Kode" />
                                        <asp:BoundField DataField="KODE_CUST" HeaderText="Buyer" />
                                        <asp:BoundField DataField="NO_BON" HeaderText="No Bon" />
                                        <asp:BoundField DataField="TGL_TRANS" HeaderText="Tanggal Transaksi" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="NET_BAYAR" HeaderText="PRICE" DataFormatString="{0:0,0.00}" />
                                        <asp:BoundField DataField="MARGIN" HeaderText="Margin" />
                                        <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" />
                                        <asp:BoundField DataField="SEND_DATE" HeaderText="Tanggal Kirim" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="FRETUR" HeaderText="RETUR" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="NO_SO" HeaderText="No Order SO" />
                                        <asp:BoundField DataField="NO_SCAN" HeaderText="No SCAN" />
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
            <asp:ModalPopupExtender ID="ModalReturItem" runat="server" TargetControlID="btnModalItemRetur"
                Drag="true" PopupControlID="PanelIRetur" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelIRetur" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: block; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnIReturIDHeader" runat="server" />
                <asp:HiddenField ID="hdnIReturIDKdbrg" runat="server" />
                <div id="divIReturMessage" runat="server" visible="false" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="lbIReturJudul">Item List</asp:Label></h2>
                            <hr />
                        </td>
                        <td align="right">
                            <asp:Button ID="bIReturCloseHide" runat="server" Text="Cancel" Width="100px" Visible="false" />
                            <asp:Button ID="bIReturClose" runat="server" Text="Cancel" Width="100px" OnClick="bIReturCloseClick" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>NO BUKTI :</b> </td>
                        <td>
                            <asp:TextBox ID="tbIReturNoBon" ReadOnly="true" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>TGL TRANSAKSI :</b> </td>
                        <td>
                            <asp:TextBox ID="tbIReturTglTrans" ReadOnly="false" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" Format="dd/MM/yyyy"
                                TargetControlID="tbIReturTglTrans" DefaultView="Days">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>TGL PENGIRIMAN :</b> </td>
                        <td>
                            <asp:TextBox ID="tbIReturTglKirim" runat="server" Width="100%" ReadOnly="false"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarIReturTglKirim" runat="server" Enabled="true" Format="dd/MM/yyyy"
                                TargetControlID="tbIReturTglKirim" DefaultView="Days">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>STATUS :</b> </td>
                        <td>
                            <asp:TextBox ID="tbIReturStatus" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>BUYER :</b> </td>
                        <td>
                            <asp:TextBox ID="tbIReturStore" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>KODE :</b> </td>
                        <td>
                            <asp:TextBox ID="tbIReturKode" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:Label ID="tbIReturMargin" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>RETUR :</b> </td>
                        <td>
                            <asp:TextBox ID="tbIReturRetur" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnIReturSave" runat="server" Text="Save" OnClick="btnIReturSaveClick" Style="font-weight: bold;" />&nbsp;
                    <asp:Button ID="btnIReturPrintDeliveryOrder" runat="server" Text="Print Delivery Order" OnClick="btnIReturPrintDeliveryOrderClick" />&nbsp;
                    <asp:Button ID="btnIReturPrintPackingList" runat="server" Text="Print Packing List" OnClick="btnIReturPrintPackingListClick" />&nbsp;
                    <asp:Button ID="btnVoid" runat="server" Text="Void" OnClick="btnVoid_Click" />
                        </td>
                    </tr>
                    <asp:Label ID="lblnoordSO" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblNoScanOrd" runat="server" Visible="false"></asp:Label>

                    <tr>
                        <td></td>
                        <td colspan="4">
                            <div class="EU_TableScroll" style="display: block">
                                <asp:GridView ID="gvIRetur" runat="server" AllowPaging="true" PageSize="20" DataKeyNames="ID"
                                    CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false"
                                    OnRowCommand="gvIReturCommand" OnPageIndexChanging="gvIReturPageChanging" OnRowDataBound="gvIReturDataBound">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Action">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="SaveRow"
                                                        ImageUrl="~/Image/b_ok.png" Text="Save" Visible="false" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                                        <asp:BoundField DataField="ID_BAYAR" HeaderText="id Bayar" SortExpression="ID_BAYAR" Visible="false" />
                                        <asp:BoundField DataField="ID_KDBRG" HeaderText="id Kdbrg" SortExpression="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="FBARCODE" HeaderText="Barcode" />
                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                                        <asp:BoundField DataField="FBRAND" HeaderText="Brand" />
                                        <asp:BoundField DataField="FART_DESC" HeaderText="Description" />
                                        <asp:BoundField DataField="FCOL_DESC" HeaderText="Warna" />
                                        <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />

                                        <asp:BoundField DataField="QTY" HeaderText="Qty" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="QTY" HeaderText="Qty" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="FRETUR" HeaderText="RETUR" />
                                        <asp:BoundField DataField="QTY" HeaderText="Qty" />
                                        <asp:BoundField DataField="TAG_PRICE" HeaderText="@Price" DataFormatString="{0:0,0.00}" />
                                        <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" DataFormatString="{0:0,0.00}" />
                                        <asp:BoundField DataField="MARGIN" HeaderText="Margin" />
                                        <asp:BoundField DataField="NILAI_BYR" HeaderText="Price" DataFormatString="{0:0,0.00}" />
                                        <asp:BoundField DataField="QTY_ACTUAL" HeaderText="Qty Actual" />
                                        <asp:TemplateField ShowHeader="true" HeaderText="Input Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="tbIReturQtyActual" runat="server" Visible="false"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilterTBIReturQtyActual" runat="server" Enabled="true"
                                                    TargetControlID="tbIReturQtyActual" FilterType="Numbers">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FSTATUS" HeaderText="FSTATUS" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="QTY_ACTUAL" HeaderText="Qty Actual" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Show Upload-->
            <asp:Button ID="btnModalShowUpload" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalShowUpload" runat="server" TargetControlID="btnModalShowUpload"
                Drag="true" PopupControlID="PanelShowUpload" CancelControlID="bSUCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelShowUpload" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="bSUDONEClose"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="DivSUMessege" runat="server" visible="false"></div>
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label1">Data</asp:Label>
                                <asp:Label runat="server" ID="Label5"></asp:Label></h2>
                            <br />
                            <hr style="width: 100%" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bSUCancel" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px; display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">Data Row : &nbsp;
                    <asp:Label ID="lbSURow" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">Total Qty Row : &nbsp;
                    <asp:Label ID="lbSUTotalQty" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="bSUDONEClose" runat="server" Text="Insert" ValidationGroup="Add_Input" OnClick="bSUDONEClose_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
