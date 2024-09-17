<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterCustWholesale.aspx.cs" Inherits="ATMOS_SROM.Master.MasterCustWholesale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
    <div>
        <h2>
            <asp:Label ID="lblJudul" runat="server" Text="Master Wholesale Customer"></asp:Label>
        </h2>
    </div>
    <div id="dSupplier" runat="server">
        <div id="dCustMSG" runat="server" visible="false"></div>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="tbSearch" runat="server" Width="100px"></asp:TextBox>&nbsp;
                    &nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By Nama Customer" Value="NM_PEMBELI"></asp:ListItem>
                <asp:ListItem Text="By Kode Customer" Value="KD_PEMBELI"></asp:ListItem>
            </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAddNewCust" runat="server" Text="Add Customer" OnClick="btnAddNewCust_Click" />
                </td>
                <td></td>
            </tr>
        </table>
        <div id="dGrid" runat="server" visible="true" class="EU_TableScroll" style="display: block">
            <asp:GridView ID="GvCustWholesale" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                PageSize="10" OnPageIndexChanging="GvCustWholesale_PageIndexChanging" OnRowCommand="GvCustWholesale_RowCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgSelect" runat="server" CausesValidation="False" CommandName="EditRow"
                                    ImageUrl="~/Image/b_ok.png" Text="edit" />&nbsp;
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                    <asp:BoundField DataField="KD_PEMBELI" HeaderText="Kode" />
                    <asp:BoundField DataField="NM_PEMBELI" HeaderText="Nama" />
                    <asp:BoundField DataField="HAS_SO_ORDER" HeaderText="Has Wholesale" />
                    <asp:BoundField DataField="BLOCK" HeaderText="Blocked" />
                </Columns>
            </asp:GridView>
        </div>
        <%-- Pop up Add Supplier --%>
        <asp:Button ID="btnPopUpAddCst" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="PopUpAddCst" runat="server" TargetControlID="btnPopUpAddCst"
            Drag="true" PopupControlID="PanelPopUpAddCst" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelPopUpAddCst" runat="server" BackColor="White" CssClass="ModalWindow" BorderStyle="Ridge" BorderColor="BlanchedAlmond" Style="display: block; top: 684px; left: 39px; width: 80%;">
            <h2>
                <asp:Label ID="lblJudulPopUp" runat="server"></asp:Label>
            </h2>
            <div id="dAddCustMsg" runat="server" visible="false"></div>
            <br />
            <table>
                <tr>
                    <td>Kode Customer:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddKode" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>
                        <asp:CheckBox ID="CBBlockStat" runat="server" Text="Blocked" TextAlign="Right" />
                    </td>
                </tr>
                <tr>
                    <td>Nama Customer :
                    </td>
                    <td>
                        <asp:TextBox ID="txtNmCust" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIDCustEdit" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>

            </table>
            <br />
            <asp:Button ID="btnAddCust" runat="server" OnClick="btnAddCust_Click" />
            &nbsp
            <asp:Button ID="btnAddCancelCust" runat="server" Text="Cancel" OnClick="btnAddCancelCust_Click" />
        </asp:Panel>

    </div>
</asp:Content>
