<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderTracerNew.aspx.cs" Inherits="ATMOS_SROM.Master.PurchaseOrderTracerNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="false"
        ScriptMode="Release">
    </asp:ToolkitScriptManager>

    <div id="idSearch" runat="server">
        <asp:TextBox ID="tbSearch" runat="server" Width="251px"></asp:TextBox>&nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By NO PO" Value="NO_PO"></asp:ListItem>
                <asp:ListItem Text="By GR NO" Value="NO_GR"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /><br />
    </div>
    <div class="EU_TableScroll" style="display: block">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%" Height="100%"
            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID_PO"
            PageSize="10" OnRowCommand="gvMain_RowCommand" OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound" OnSelectedIndexChanged="gvMain_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField ShowHeader="False" HeaderText="Action">
                    <ItemTemplate>
                        <div>
                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID_PO" HeaderText="ID_PO" SortExpression="ITEM_CODE" Visible="false" />
                <asp:BoundField DataField="NO_PO" HeaderText="NO_PO" />
                <asp:BoundField DataField="NO_GR" HeaderText="NO_GR" />
                <asp:BoundField DataField="DATE" HeaderText="DATE" />
                <%--<asp:BoundField DataField="TGL_GR" HeaderText="TGL_GR" />--%>
                <asp:BoundField DataField="BARCODE" HeaderText="BARCODE" Visible="false" />
                <asp:BoundField DataField="QTY" HeaderText="QTY" />
                <asp:BoundField DataField="QTY_TIBA" HeaderText="QTY_TIBA" />
                <asp:BoundField DataField="Selisih" HeaderText="Selisih" />
                <asp:BoundField DataField="STATUS_PO" HeaderText="STATUS_PO" />
                <asp:BoundField DataField="PO_REFF" HeaderText="PO_REFF" />
                <asp:BoundField DataField="BRAND" HeaderText="BRAND" />
                <asp:BoundField DataField="CONTACT" HeaderText="CONTACT" />
                <asp:BoundField DataField="PHONE" HeaderText="PHONE" />
                <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                <asp:BoundField DataField="SUPPLIER" HeaderText="SUPPLIER" />

            </Columns>
        </asp:GridView>

    </div>
    <asp:Button ID="btnShowDtlTracer" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalShowDtlTracer" runat="server" TargetControlID="btnShowDtlTracer"
        Drag="true" PopupControlID="PanelShowDtlTracer" CancelControlID="bVIClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelShowDtlTracer" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" class="EU_TableScroll"
        Style="display: none; top: 684px; left: 39px; width: 80%;">
        <br />

        <div id="dmsgUpdItem" runat="server" visible="false"></div>

        <asp:HiddenField ID="hdnIDVI" runat="server" />
        <asp:HiddenField ID="hdnVINoPO" runat="server" />

        <table>
            <tr>
                <td colspan="2">
                    <asp:Button ID="bVIClose" runat="server" Text="Close" OnClick="bVIClose_Click" />
                </td>
            </tr>
            <tr>
                <td>PO NO : 
                </td>
                <td>
                    <asp:Label ID="lblPoNo" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <%-- <td>GR NO : 
                </td>
                <td>
                    <asp:Label ID="lblGrNo" runat="server"></asp:Label>
                </td>--%>
            </tr>
            <tr>
                <td>PO REFF : 
                </td>
                <td>
                    <asp:Label ID="lblPoReff" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <td>PO Date : 
                </td>
                <td>
                    <asp:Label ID="lblPoDt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Brand : 
                </td>
                <td>
                    <asp:Label ID="lblBrand" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <td>Supplier : 
                </td>
                <td>
                    <asp:Label ID="lblSuppl" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Contact : 
                </td>
                <td>
                    <asp:Label ID="lblContact" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <%-- <td>Total Qty : 
                </td>
                <td>
                    <asp:Label ID="lblTtlQty" runat="server"></asp:Label>
                </td>--%>
            </tr>
            <tr>
                <td>Phone / Email : 
                </td>
                <td>
                    <asp:Label ID="lblPhnEmail" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <%--<td>GR STATUS : 
                </td>
                <td>
                    <asp:Label ID="lblGrStat" runat="server"></asp:Label>
                </td>--%>
            </tr>
            <tr>
                <td>Address : 
                </td>
                <td>
                    <asp:Label ID="lblAddr" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <%-- <td>Showroom : 
                </td>
                <td>
                    <asp:Label ID="lblShowroom" runat="server"></asp:Label>
                </td>--%>
            </tr>
        </table>
        <br />
         <div class="EU_TableScroll" style="display: block">
        <h1>Summary</h1>
         <br />
        <asp:GridView ID="GvDtlDiff" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%" Height="100%"
            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID_PO"
            PageSize="10"  OnPageIndexChanging="GvDtlDiff_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID_PO" HeaderText="ID_PO" Visible="false"/>
                <asp:BoundField DataField="NO_PO" HeaderText="NO_PO" />
                <asp:BoundField DataField="BARCODE" HeaderText="BARCODE" />
                <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM_CODE" />
                <asp:BoundField DataField="FSIZE_DESC" HeaderText="FSIZE_DESC" />
                <asp:BoundField DataField="FART_DESC" HeaderText="FART_DESC" />
                <asp:BoundField DataField="FCOL_DESC" HeaderText="FCOL_DESC" />
                <asp:BoundField DataField="FPRODUK" HeaderText="FPRODUK" />
                <asp:BoundField DataField="QTY" HeaderText="QTY" />
                <asp:BoundField DataField="QTY_TIBA" HeaderText="QTY_TIBA" />

            </Columns>
        </asp:GridView>
        <br />
        <h1>Detail</h1>
         <br />
        <asp:GridView ID="GvDetailTracer" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%" Height="100%"
            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ITEM_CODE"
            PageSize="10" OnRowCommand="GvDetailTracer_RowCommand" OnPageIndexChanging="GvDetailTracer_PageIndexChanging">

            <Columns>
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" SortExpression="ITEM_CODE" Visible="false" />
                <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                <asp:BoundField DataField="FART_DESC" HeaderText="Description" />
                <asp:BoundField DataField="FCOL_DESC" HeaderText="Colour" />
                <asp:BoundField DataField="FPRODUK" HeaderText="Product" />
                <asp:BoundField DataField="FSize_DESC" HeaderText="Size" />
                <asp:BoundField DataField="QTY" HeaderText="PO Qty" />
                <asp:BoundField DataField="QTY_TIBA" HeaderText="Qty Tiba" />
                <asp:BoundField DataField="Selisih" HeaderText="Selisih" />
                <asp:BoundField DataField="NO_GR" HeaderText="No GR" />
                <asp:BoundField DataField="TGL_GR" HeaderText="Tgl GR" />
            </Columns>
        </asp:GridView>
        </div>
    </asp:Panel>

</asp:Content>
