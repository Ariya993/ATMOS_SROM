<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RptMutasiStockPOS.aspx.cs" Inherits="ATMOS_SROM.Report.RptMutasiStockPOS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    <h2>Menu Mutasi Stock Report
    </h2>
    <hr />
    <br />
    <div id="DivMessage" runat="server" visible="false">
    </div>
    <table>
        <tr>
            <td>Showroom
            </td>
            <td>
                <asp:DropDownList ID="ddlStore" runat="server" AppendDataBoundItems="false" DataTextField="SHOWROOM" DataValueField="KODE" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged" ></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Bulan
            </td>
            <td>
                <asp:TextBox ID="tbBulanStock" runat="server" />
                <asp:CalendarExtender ID="CalendeExtenderBulanStock" runat="server" Enabled="true" Format="yyMM" ClientIDMode="Static"
                    TargetControlID="tbBulanStock" DefaultView="Months">
                </asp:CalendarExtender>
                &nbsp;
            </td>
        </tr>
         <tr>
            <td>Last Closing :
            </td>
            <td>
                <asp:Label ID="lbllastclose" runat="server" ></asp:Label>
               
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
                &nbsp
                <asp:Button ID="btnGnr" runat="server" Text="Generate Excel" OnClick="btnGnr_Click" />
                &nbsp
                <asp:Button ID="btnGnrAll" runat="server" Text="Generate Excel All Showroom" OnClick="btnGnrAll_Click" />
                &nbsp
                <asp:Button ID="closing" runat="server" OnClick="closing_Click" Text="closing" visible="false"/>
            </td>
        </tr>
    </table>

    <asp:Label ID="maxdt" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:Label ID="mindt" runat="server" Visible="false"></asp:Label>
    <br />
    <asp:Label ID="lblKode" runat="server" Visible="false"></asp:Label>

    <div class="EU_TableScroll" style="display: block">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="false" DataKeyNames="KODE"
            PageSize="10">
            <Columns>

                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="KODE" HeaderText="KODE" SortExpression="KODE" />
                <asp:BoundField DataField="BARCODE" HeaderText="BARCODE" />
                <asp:BoundField DataField="SLD_AWAl" HeaderText="SLD_AWAl" />
                <asp:BoundField DataField="QTY_BELI" HeaderText="QTY_BELI" />
                <asp:BoundField DataField="QTY_TERIMA" HeaderText="QTY_TERIMA" />
                <asp:BoundField DataField="QTY_RTR_PTS" HeaderText="QTY_RTR_PTS" />
                <asp:BoundField DataField="QTY_IN_PINJAM" HeaderText="QTY_IN_PINJAM" />
                <asp:BoundField DataField="QTY_KIRIM" HeaderText="QTY_KIRIM" />
                <asp:BoundField DataField="QTY_JUAL" HeaderText="QTY_JUAL" />
                <asp:BoundField DataField="QTY_JUAL_PTS" HeaderText="QTY_JUAL_PTS" />
                <asp:BoundField DataField="QTY_OUT_PINJAM" HeaderText="QTY_OUT_PINJAM" />
                <asp:BoundField DataField="QTY_ADJ" HeaderText="QTY_ADJ" />
                <asp:BoundField DataField="QTY_OPNM" HeaderText="QTY_OPNM" />
                <asp:BoundField DataField="SLD_AKHIR" HeaderText="SLD_AKHIR" />
                <asp:BoundField DataField="ADJ_GIT" HeaderText="ADJ_GIT" />
            </Columns>
        </asp:GridView>
    </div>


</asp:Content>
