<%@ Page Language="C#" UICulture="id" Culture="id-ID" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WholeSalesWareHouse.aspx.cs" Inherits="ATMOS_SROM.Sales.WholeSalesWareHouse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="dmain" runat="server" visible="true">
        <h2>
            <asp:Label ID="lbJudul" runat="server" Text="Wholesale (Warehouse)"></asp:Label></h2>
        <hr />
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <asp:Button ID="btnInsToShPutus" runat="server" Text="Send SO" OnClick="btnInsToShPutus_Click" />
        <asp:Button ID="btnCloseSO" runat="server" Text="CLOSE SO" OnClick="btnCloseSO_Click" />
        <h2>
            <asp:Label runat="server" ID="Label4">Search List SO </asp:Label></h2>

        <br />
        <asp:TextBox ID="tbBONSearch" runat="server" Width="270px" />&nbsp;
                    <asp:DropDownList ID="ddlBONSearch" runat="server">
                        <asp:ListItem Value="NO_BON" Text="By No Bon"></asp:ListItem>
                        <asp:ListItem Value="NO_SO" Text="By No Order"></asp:ListItem>
                    </asp:DropDownList>
        &nbsp
        <asp:Button ID="btnBONSearch" Text="Search" runat="server" OnClick="btnBONSearch_Click" />
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvBON" runat="server" AllowPaging="true" PageSize="10" DataKeyNames="ID"
                CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false"
                OnRowCommand="gvBON_RowCommand" OnPageIndexChanging="gvBON_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="true" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="SaveRow"
                                    ImageUrl="~/Image/b_ok.png" Text="Save" />
                                <asp:ImageButton ID="imgPdf" runat="server" CausesValidation="False" CommandName="PrintRow"
                                    ImageUrl="~/Image/pdf.png" Text="Print" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                    <asp:BoundField DataField="KODE" HeaderText="Kode" />
                    <asp:BoundField DataField="KODE_CUST" HeaderText="Buyer" />
                    <asp:BoundField DataField="NO_BON" HeaderText="No Bon" />
                    <asp:BoundField DataField="TGL_TRANS" HeaderText="Tanggal Transaksi" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="NET_BAYAR" HeaderText="PRICE" DataFormatString="{0:0,0.00}" Visible="false" />
                    <asp:BoundField DataField="MARGIN" HeaderText="Margin" Visible="false" />
                    <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="STATUS_HEADER" HeaderText="Status" />
                    <asp:BoundField DataField="SEND_DATE" HeaderText="Tanggal Kirim" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="FRETUR" HeaderText="RETUR" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="STATUS_ORDER" HeaderText="Status Order" />
                    <asp:BoundField DataField="NO_SO" HeaderText="No Order SO" />
                    <asp:BoundField DataField="NO_SCAN" HeaderText="No SCAN" />
                </Columns>
            </asp:GridView>
        </div>

    </div>
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
        <div id="dShPutus" runat="server" visible="false">
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
    <!--Pop Up Close SO-->
    <asp:Button ID="btnModalPopupCloseSO" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupCloseSO" runat="server" TargetControlID="btnModalPopupCloseSO"
        Drag="true" PopupControlID="PanelPopupCloseSO" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopupCloseSO" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: none; top: 684px; left: 39px; width: 80%;">
        <br />
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
        <table width="100%" cellspacing="4" >
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="lbIReturJudul">Item List</asp:Label></h2>
                    <hr />
                </td>
                <td align="right">
                    
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <b>NO BUKTI :</b> </td>
                <td>
                    <asp:TextBox ID="tbIReturNoBon" ReadOnly="true" runat="server" Width="100%" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <b>TGL TRANSAKSI :</b> </td>
                <td>
                    <asp:TextBox ID="tbIReturTglTrans" ReadOnly="false" runat="server" Width="100%" ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" Format="dd/MM/yyyy"
                        TargetControlID="tbIReturTglTrans" DefaultView="Days">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
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
                <td>
                </td>
                <td>
                    <b>STATUS :</b> </td>
                <td >
                    <asp:TextBox ID="tbIReturStatus" ReadOnly="true" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <b>BUYER :</b> </td>
                <td>
                    <asp:TextBox ID="tbIReturStore" ReadOnly="true" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <b>KODE :</b> </td>
                <td>
                    <asp:TextBox ID="tbIReturKode" ReadOnly="true" runat="server" ></asp:TextBox>
                    <asp:Label ID="tbIReturMargin" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>  
            <tr>
                <td>
                </td>
                <td>
                    <b>RETUR :</b> </td>
                <td>
                    <asp:TextBox ID="tbIReturRetur" ReadOnly="true" runat="server" ></asp:TextBox>
                </td>
            </tr>  
             <asp:Label ID="lblnoordSO" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblNoScanOrd" runat="server" Visible="false"></asp:Label>
             </table>
                    <asp:Button ID="btnIReturSave" runat="server" Text="Save" OnClick="btnIReturSave_Click" style="font-weight:bold;" />&nbsp;
                    <asp:Button ID="btnIReturPrintDeliveryOrder" runat="server" Text="Print Delivery Order" OnClick="btnIReturPrintDeliveryOrder_Click" />&nbsp;
<%--                    <asp:Button ID="btnIReturPrintPackingList" runat="server" Text="Print Packing List" OnClick="btnIReturPrintPackingListClick" />&nbsp;--%>
                    <asp:Button ID="btnVoid" runat="server" Text="Void" OnClick="btnVoid_Click" />
                    &nbsp;
                    <asp:Button ID="bIReturCloseHide" runat="server" Text="Cancel" Width="100px" Visible="false" />
                    <asp:Button ID="bIReturClose" runat="server" Text="Cancel" Width="100px" OnClick="bIReturClose_Click" />
             
                    <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvIRetur" runat="server" AllowPaging="true" PageSize="20" DataKeyNames="ID"
                        CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false" 
                        OnRowCommand="gvIRetur_RowCommand" OnPageIndexChanging="gvIRetur_PageIndexChanging" OnRowDataBound="gvIRetur_RowDataBound">
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
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="id Kdbrg" SortExpression="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
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
                            <asp:BoundField DataField="TAG_PRICE" HeaderText="@Price" DataFormatString="{0:0,0.00}" Visible="false"/>
                            <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" DataFormatString="{0:0,0.00}" Visible="false"/>                            
                            <asp:BoundField DataField="MARGIN" HeaderText="Margin" Visible="false" />
                            <asp:BoundField DataField="NILAI_BYR" HeaderText="Price" DataFormatString="{0:0,0.00}" Visible="false"/>
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
    </asp:Panel>    

</asp:Content>
