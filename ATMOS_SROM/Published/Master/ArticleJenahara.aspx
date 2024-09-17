<%@ Page Title="Article" Language="C#" UICulture="id" Culture="id-ID" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticleJenahara.aspx.cs" Inherits="ATMOS_SROM.Master.ArticleJenahara" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="http://www.decorplanit.com/plugin/autoNumeric-1.9.18.js"></script>
    <script type='text/javascript'>
        $(function ($) {
            // Defines your numeric masking for any elements with the class numericOnly and sets the maximum to 10
            $('.numericOnly').autoNumeric('init', { vMax: 9999999999 });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="false"
        ScriptMode="Release">
    </asp:ToolkitScriptManager>
    <div id="divAwal" runat="server">
        <h2>Article Master</h2>
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <div id="divUpload" runat="server">
            <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;
            <asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/Format_Master_Article.xlsx">
                <asp:Label ID="lbDownload" runat="server" Text="Download Format Upload Excel"></asp:Label>
            </asp:HyperLink><br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblSrc" runat="server"></asp:Label>
        </div>
        <div id="divMain" runat="server" visible="true">
            <hr />
            <asp:TextBox ID="tbSearch" runat="server" Width="251px"></asp:TextBox>&nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                <asp:ListItem Text="By Description" Value="FART_DESC"></asp:ListItem>
                <asp:ListItem Text="By Status Block" Value="STAT_KDBRG"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick" /><br />
            <br />
            <div class="EU_TableScroll" style="display: block">
                <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                    OnRowCommand="gvMainRowCommand" PageSize="10" OnPageIndexChanging="gvMainPageChanging" OnRowDataBound="gvMain_RowDataBound">
                    <Columns>
                        <asp:TemplateField ShowHeader="false" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                        ImageUrl="~/Image/b_edit.png" Text="Edit" />
                                    <asp:ImageButton ID="imgBlock" runat="server" CausesValidation="False" CommandName="BlockRow"
                                        ImageUrl="~/Image/b_drop.png" Text="Block Item" />
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
                        <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                        <asp:BoundField DataField="FART_DESC" HeaderText="Desc" />
                        <asp:BoundField DataField="FCOL_DESC" HeaderText="Color" />
                        <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />
                        <asp:BoundField DataField="FPRODUK" HeaderText="Produk" />
                        <asp:BoundField DataField="PRICE" HeaderText="Price" />
                        <asp:BoundField DataField="STAT_KDBRG" HeaderText="BLOCKED" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <!-- Pop Up Add Edit Price -->
    <asp:Button ID="btnShowPopupPrice" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtenderPrice" runat="server" TargetControlID="btnShowPopupPrice"
        Drag="true" PopupControlID="PanelPopUpPrice" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUpPrice" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPUSave"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <div id="div1" runat="server">
            <h2>Article Price</h2>
            <div id="DivPriceMsg" runat="server" visible="false">
            </div>
            <br />
            <table>
                <tr>
                    <td colspan="4">
                        <h1>Article Info</h1>
                    </td>
                    <td>
                        <asp:Button ID="btncloesprice" runat="server" Text="Close" OnClick="btncloesprice_Click" />
                    </td>
                </tr>
                <tr>
                    <td>Barcode </td>
                    <td>:
                        <asp:Label ID="lblinfobarcode" runat="server"></asp:Label><asp:Label ID="lblinfoIdkdbrg" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td width="10%"></td>
                    <td>Item Code </td>
                    <td>:
                        <asp:Label ID="lblInfoItemCode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Desc </td>
                    <td>:
                        <asp:Label ID="lblInfoDesc" runat="server"></asp:Label>
                    </td>
                    <td width="10%"></td>
                    <td>Product </td>
                    <td>:
                        <asp:Label ID="lblInfoProduct" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Color </td>
                    <td>: 
                        <asp:Label ID="lblInfoColor" runat="server"></asp:Label>
                    </td>
                    <td width="10%"></td>
                    <td>Size </td>
                    <td>:
                        <asp:Label ID="lblInfoSize" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>New Article Price </td>
                    <td>
                        <asp:TextBox ID="txtpriceArticle" runat="server" class="numericOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Start Date </td>
                    <td>
                        <asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendeExtenderTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                            TargetControlID="tbDate" DefaultView="Days">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button ID="btnaddprice" runat="server" Text="Add New Price" OnClick="btnaddprice_Click" />
                    </td>
                </tr>

            </table>
            <br />
            <div class="EU_TableScroll" style="display: block">
                <asp:GridView ID="gvPrice" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                    OnRowCommand="gvPrice_RowCommand" PageSize="10" OnPageIndexChanging="gvPrice_PageIndexChanging" OnRowDataBound="gvPrice_RowDataBound">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditPrice"
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
                        <asp:BoundField DataField="PRICE" HeaderText="Price" />
                        <asp:BoundField DataField="START_DATE" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="END_DATE" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Info" HeaderText="Info" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

    <asp:Button ID="btnShowPopupEditPrice" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtenderEditPrice" runat="server" TargetControlID="btnShowPopupEditPrice"
        Drag="true" PopupControlID="PanelPopUpEditPrice" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUpEditPrice" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPUSave"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <div id="div2" runat="server">
            <h2>Article Price Edit</h2>
            <div id="Div3" runat="server" visible="false">
            </div>
            <br />
            <table>
                <tr>
                    <td colspan="5">
                        <asp:Button id="btneditpriceclose" runat="server" Text="Close" OnClick="btneditpriceclose_Click" />
                    </td>
                </tr>
                <tr>
                    <td>Harga Saat Ini 
                    </td>
                    <td>
                        : <asp:Label ID="lblpriceNow" runat="server"> </asp:Label>
                        <asp:Label ID="lblidprice" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td width="10%"></td>
                    <td>Berlaku Mulai Tanggal
                    </td>
                    <td>
                        : <asp:Label ID="lblTglStart" runat="server"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Harga Baru 
                    </td>
                    <td>
                        <asp:TextBox ID="txtPriceNew" runat="server" class="numericOnly"></asp:TextBox>
                    </td>
                    <td width="10%"></td>
                    <td colspan="2">
                        <asp:Button ID="btneditPrice" runat="server" Text="edit" OnClick="btneditPrice_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelPopUp" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUp" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPUSave"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnId" runat="server" />
        <table width="100%" cellspacing="4">
            <tr>
                <td></td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="lblTitleSubPage">Article</asp:Label></h2>
                    <hr />
                </td>
                <td align="right">
                    <asp:Button ID="bClose" runat="server" Text="Cancel" Width="100px" />
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
                    <asp:TextBox ID="tbPUQty" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3" align="left">
                    <asp:Button ID="btnPUSave" runat="server" Text="Save" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
