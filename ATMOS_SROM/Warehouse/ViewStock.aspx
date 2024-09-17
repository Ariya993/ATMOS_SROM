<%@ Page Language="C#"  UICulture="id" Culture="id-ID" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStock.aspx.cs" Inherits="ATMOS_SROM.Warehouse.ViewStock" %>

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

    <asp:UpdatePanel ID="panelMain" runat="server">
        <ContentTemplate>

            <h2>
                <asp:Label ID="lbIdJudul" runat="server" Text="Warehouse"></asp:Label></h2>
            <div id="DivMessage" runat="server" visible="false">
            </div>
            <br />
            <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
            &nbsp;
    <asp:DropDownList ID="ddlSearch" runat="server">
        <asp:ListItem Text="by Barcode" Value="BARCODE"></asp:ListItem>
        <asp:ListItem Text="by Item Code" Value="ITEM_CODE"></asp:ListItem>
        <asp:ListItem Text="by Description" Value="ART_DESC"></asp:ListItem>
    </asp:DropDownList>&nbsp;
    <asp:DropDownList ID="ddlStoreSrchStore" runat="server" DataTextField="SHOWROOM" DataValueField="KODE" AppendDataBoundItems="false"></asp:DropDownList>&nbsp;
&nbsp;
    <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" />
            <br />
            <div class="EU_TableScroll" style="display: block">
                <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                    PageSize="10" OnPageIndexChanging="gvMain_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                        <asp:BoundField DataField="WAREHOUSE" HeaderText="Warehouse" />
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="STOCK" HeaderText="Stock" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                        <asp:BoundField DataField="BARCODE" HeaderText="barcode" />
                        <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                        <asp:BoundField DataField="PRODUK" HeaderText="Produk" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="FGROUP" HeaderText="Group" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ART_DESC" HeaderText="Article Name" />
                        <asp:BoundField DataField="WARNA" HeaderText="Color" />
                        <asp:BoundField DataField="SIZE" HeaderText="Size" />
                        <asp:BoundField DataField="STOCK" HeaderText="Stock" />
                        <asp:BoundField DataField="ID_KDBRG" HeaderText="ID Kdbrg" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="KODE" HeaderText="Kode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    </Columns>
                </asp:GridView>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

