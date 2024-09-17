<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderTracer.aspx.cs" Inherits="ATMOS_SROM.Master.PurchaseOrderTracer" %>

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
                <asp:ListItem Text="By NO PO" Value="poh.NO_PO"></asp:ListItem>
                <asp:ListItem Text="By GR NO" Value="tb.NO_GR"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /><br />
    </div>
    <div class="EU_TableScroll" style="display: block">
    <div id="TracerHeader" runat="server" visible="false">
        <table>
            <tr>
                <td>PO NO : 
                </td>
                <td>
                    <asp:Label ID="lblPoNo" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <td>GR NO : 
                </td>
                <td>
                    <asp:Label ID="lblGrNo" runat="server"></asp:Label>
                </td>
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
                <td>Total Qty : 
                </td>
                <td>
                    <asp:Label ID="lblTtlQty" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Phone / Email : 
                </td>
                <td>
                    <asp:Label ID="lblPhnEmail" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <td>GR STATUS : 
                </td>
                <td>
                    <asp:Label ID="lblGrStat" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Address : 
                </td>
                <td>
                    <asp:Label ID="lblAddr" runat="server"></asp:Label>
                </td>
                <td width="50px"></td>
                <td>Showroom : 
                </td>
                <td>
                    <asp:Label ID="lblShowroom" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="dGrid" runat="server" visible="false">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ITEM_CODE"
            PageSize="10" OnRowCommand="gvMain_RowCommand" OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound" OnSelectedIndexChanged="gvMain_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" SortExpression="ITEM_CODE" Visible="false" />
                <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                <asp:BoundField DataField="FART_DESC" HeaderText="Description" />
                <asp:BoundField DataField="FCOL_DESC" HeaderText="Colour" />
                <asp:BoundField DataField="FPRODUK" HeaderText="Product" />
                <asp:BoundField DataField="FSize_DESC" HeaderText="Size" />
                <asp:BoundField DataField="QTY" HeaderText="PO Qty" />
                <asp:BoundField DataField="QTY_TIBA" HeaderText="Qty Tiba" />
                <asp:BoundField DataField="Selisih" HeaderText="Selisih" />
                <asp:BoundField DataField="Status_GR" HeaderText="Status GR" />
            </Columns>
        </asp:GridView>

    </div>
        </div>
</asp:Content>
