<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GoodReceiveJenahara.aspx.cs" Inherits="ATMOS_SROM.Warehouse.GoodReceiveJenahara" %>
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
    </script>
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
        <h2>Good Receive</h2>
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <asp:Button ID="btnListGR" runat="server" Text="List Good Receive" OnClick="btnListGRClick" />
        <br /><br />
        <asp:Button ID="btnSearch" runat="server" Text="Search Purchase Order" OnClick="btnSearchClick" />
        <br />
        <table>
            <tr>
                <td>
                    <b>NO Good Receive :&nbsp;</b>
                </td>
                <td>
                    <asp:TextBox ID="tbNoGR" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Kode Supplier :</b>
                </td>
                <td>
                    <asp:TextBox ID="tbKodeSupplier" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Nama Supplier :</b>
                </td>
                <td>
                    <asp:TextBox ID="tbNamaSupplier" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Tanggal Transaksi :</b>
                </td>
                <td>
                    <asp:TextBox ID="tbTglTrans" runat="server" Width="170px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendeExtenderTbTglTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbTglTrans" DefaultView="Days">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="reqValidTbTglTrans" runat="server" ControlToValidate="tbTglTrans" ForeColor="Red"
                        ErrorMessage="Mohon masukan waktu penerimaan" ValidationGroup="TGL_KIRIM"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>        
        <br />
    <asp:Panel ID="panelMain" runat="server">
    <div>
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                OnRowCommand="gvMain_RowCommand" PageSize="10">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
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
                    <asp:BoundField DataField="NO_PO" HeaderText="NO Purchase Order" /> 
                    <asp:BoundField DataField="PO_REFF" HeaderText="PO Refference" />
                    <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSaveClick" ValidationGroup="TGL_KIRIM" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>
    </asp:Panel>

    <asp:Panel ID="panelDetail" runat="server" Visible="false">
        <div>
        <asp:HiddenField ID="hdnDetail" runat="server" />
            <table width="100%">
            <tr>
                <td colspan="2">
                    <b>Detail Purchase Order</b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbDetailSearch" runat="server"></asp:TextBox> &nbsp;
                    <asp:DropDownList ID="ddlDetailSearch" runat="server">
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                    </asp:DropDownList> &nbsp;
                    <asp:Button ID="btnDetailSearch" runat="server" Text="Search" OnClick="btnDetailSearchClick" />
                </td>
            </tr>
        </table>
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                PageSize="10" OnPageIndexChanging="gvDetailPageChanging" PagerSettings-Mode="NumericFirstLast" OnRowDataBound="gvDetail_RowDataBound">
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
                    <asp:BoundField DataField="NO_PO" HeaderText="No Purchase Order" /> 
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" /> 
                    <asp:BoundField DataField="BARCODE" HeaderText="Barcode" /> 
                    <asp:BoundField DataField="COGS" HeaderText="COGS" Visible="false"/> 
                    <asp:BoundField DataField="PRICE" HeaderText="Retail Price" Visible="false" />
                    <asp:BoundField DataField="QTY" HeaderText="Qty" />
                    <asp:BoundField DataField="QTY_WAIT" HeaderText="Qty Wait" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                    <asp:TemplateField ShowHeader="true" HeaderText="Input Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="tbInputQty" runat="server" Width="20px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="true"
                                TargetControlID="tbInputQty" >
                            </asp:FilteredTextBoxExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QTY_TIBA" HeaderText="QTY TIBA" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:TemplateField ShowHeader="true" HeaderText="Warehouse">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlWarehouse" runat="server">
                                <asp:ListItem Text="Main Warehouse" Value="WARE-001"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_KDBRG" HeaderText="idKdbrg" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="FART_DESC" HeaderText="Article" />
                    <asp:BoundField DataField="FCOL_DESC" HeaderText="Warna" />
                    <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="btnDetailSave" runat="server" OnClick="btnDetailSaveClick" Text="Save" />
        </div>
    </asp:Panel>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

    <asp:ModalPopupExtender ID="ModalPopupExtender0" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelSearch" CancelControlID="bSearchClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelSearch" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIDSearch" runat="server" />
        <div id="divSearchMessage" runat="server" visible="false">
        </div>
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="Label3">Search</asp:Label></h2>
                    <hr/>
                </td>
                <td align="right">
                    <asp:Button ID="bSearchClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Search </td>
                <td colspan="2">
                    <asp:TextBox ID="tbSearchBy" runat="server" Width="270px" />&nbsp;
                    <asp:DropDownList ID="ddlSearchBy" runat="server">
                        <asp:ListItem Value="PO_REFF" Text="By PO Refference"></asp:ListItem>
                        <asp:ListItem Value="NO_PO" Text="By NO Purchase Order"></asp:ListItem>
                        <asp:ListItem Text="By Status" Value="STATUS"></asp:ListItem>
                    </asp:DropDownList> &nbsp
                    <asp:Button ID="btnSearchSearch" Text="Search" runat="server" OnClick="btnSearchSearchClick" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvSearch" runat="server" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvSearchPageIndexChanging"
                        OnRowCommand="gvSearcRowCommand" ShowHeaderWhenEmpty="true" DataKeyNames="ID"
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
                            <asp:BoundField DataField="NO_PO" HeaderText="NO Purchase Order" />
                            <asp:BoundField DataField="PO_REFF" HeaderText="PO Refference" />
                            <asp:BoundField DataField="DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="KODE_SUPPLIER" HeaderText="Kode Supplier" />
                            <asp:BoundField DataField="SUPPLIER" HeaderText="Supplier" />
                            <asp:BoundField DataField="CONTACT" HeaderText="Contact" />
                            <asp:BoundField DataField="ADDRESS" HeaderText="Address" />
                            <asp:BoundField DataField="EMAIL" HeaderText="E-mail" />
                            <asp:BoundField DataField="PHONE" HeaderText="Phone" />
                            <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                            <asp:BoundField DataField="STATUS" HeaderText="Status" />
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

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelPU" CancelControlID="bPUClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPU" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIDPU" runat="server" />
        <asp:HiddenField ID="hdnPUNoPO" runat="server" />
        <table width="100%">
            <tr>
                <td colspan="2">
                    <b>Detail Purchase Order</b>
                </td>
            </tr>
            <tr>
                <td>
                    <b>NO Purchase Order :</b> &nbsp;
                    <asp:TextBox ID="tbPUNoPO" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbPUSearch" runat="server"></asp:TextBox> &nbsp;
                    <asp:DropDownList ID="ddlPUSearch" runat="server">
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                    </asp:DropDownList> &nbsp;
                    <asp:Button ID="btnPUSearch" runat="server" Text="Search" />
                </td>
                <td align="right">
                    <asp:Button ID="btnPUClose" runat="server" Text="Close" />
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
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" /> 
                    <asp:BoundField DataField="COGS" HeaderText="COGS" /> 
                    <asp:BoundField DataField="PRICE" HeaderText="Retail Price" />
                    <asp:BoundField DataField="QTY" HeaderText="Qty" />
                    <asp:BoundField DataField="QTY_WAIT" HeaderText="Qty Wait" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                    <asp:TemplateField ShowHeader="true" HeaderText="Input Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="tbInputQty" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <!--View List Good Receive-->
    <asp:Button ID="btnModalListGRHeader" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalListGRHeader" runat="server" TargetControlID="btnModalListGRHeader"
        Drag="true" PopupControlID="PanelGRHeader" CancelControlID="bGRHClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelGRHeader" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <div id="divMessageGRHeader" runat="server" visible="false"></div>
        <table width="100%">
            <tr>
                <td colspan="2">
                    <b>List Good Receive</b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbGRHSearch" runat="server"></asp:TextBox> &nbsp;
                    <asp:DropDownList ID="ddlGRHSearch" runat="server">
                        <asp:ListItem Text="By No Good Receive" Value="NO_GR"></asp:ListItem>
                        <asp:ListItem Text="By Kode Supplier" Value="KODE_SUPPLIER"></asp:ListItem>
                        <asp:ListItem Text="By Tanggal Transaksi" Value="TGL_TRANS"></asp:ListItem>
                    </asp:DropDownList> &nbsp;
                    <asp:Button ID="btnGRHSearch" runat="server" Text="Search" OnClick="btnListGRClick" />
                </td>
                <td align="right">
                    <asp:Button ID="bGRHClose" runat="server" Text="Close" />
                </td>
            </tr>
        </table>
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvGRH" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="NO_GR"
                PageSize="10" OnRowCommand="gvGRHCommand" OnPageIndexChanging="gvGRHPageChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgSave" runat="server" CausesValidation="False" CommandName="SaveRow"
                                    ImageUrl="~/Image/b_ok.png" Text="Save" />&nbsp;                                    
                                <asp:ImageButton ID="imgPdf" runat="server" CausesValidation="False" CommandName="PrintRow"
                                    ImageUrl="~/Image/pdf.png" Text="Print Packing List" />
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow" OnClientClick="return notifdelete()" Visible ="false"
                                    ImageUrl="~/Image/b_drop.png" Text="Delete" />
                             </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NO_GR" HeaderText="No Good Receive" />
                    <asp:BoundField DataField="KODE_SUPPLIER" HeaderText="Kode Supplier" />
                    <asp:BoundField DataField="TGL_TRANS" HeaderText="Tanggal" DataFormatString="{0:dd/MM/yyyy}" /> 
                    <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <!--View List Item Good Receive-->
    <asp:Button ID="btnModalListGRDetail" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalListGRDetail" runat="server" TargetControlID="btnModalListGRDetail"
        Drag="true" PopupControlID="PanelGRDetail" CancelControlID="bGRDCloseHide" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelGRDetail" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnGRDNoGR" runat="server" />
        <div id="divMessageGRDetail" runat="server" visible="false"></div>
        <table width="100%">
            <tr>
                <td colspan="2">
                    <b>List Item Good Receive</b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbGRDSearch" runat="server"></asp:TextBox> &nbsp;
                    <asp:DropDownList ID="ddlGRDSearch" runat="server">
                        <asp:ListItem Text="By No Good Receive" Value="NO_GR"></asp:ListItem>
                        <asp:ListItem Text="By Kode Supplier" Value="KODE_SUPPLIER"></asp:ListItem>
                        <asp:ListItem Text="By Tanggal Transaksi" Value="TGL_TRANS"></asp:ListItem>
                    </asp:DropDownList> &nbsp;
                    <asp:Button ID="btnGRDSearch" runat="server" Text="Search" />
                </td>
                <td align="right">
                    <asp:Button ID="bGRDCloseHide" runat="server" Text="Close" style="display:none;" />
                    <asp:Button ID="bGRDClose" runat="server" Text="Close" OnClick="btnListGRClick" />
                </td>
            </tr>
        </table>
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvGRD" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                PageSize="10" OnRowCommand="gvGRDCommand" OnPageIndexChanging="gvGRDPageChanging">
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
                    <asp:BoundField DataField="ID_KDBRG" HeaderText="id_Kdbrg" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="NO_GR" HeaderText="No Good Receive" />
                    <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                    <asp:BoundField DataField="FBRAND" HeaderText="Brand" />
                    <asp:BoundField DataField="FART_DESC" HeaderText="Description" />
                    <asp:BoundField DataField="FCOL_DESC" HeaderText="Color" />
                    <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />
                    <asp:BoundField DataField="FSIZE_DESC" HeaderText="id_Kdbrg" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="FSIZE_DESC" HeaderText="id_Kdbrg" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="QTY_TIBA" HeaderText="Qty" /> 
                    <asp:BoundField DataField="COGS" HeaderText="COGS" /> 
                    <asp:BoundField DataField="PRICE" HeaderText="Retail Price" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
