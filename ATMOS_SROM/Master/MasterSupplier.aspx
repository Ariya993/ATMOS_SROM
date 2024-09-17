<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterSupplier.aspx.cs" Inherits="ATMOS_SROM.Master.MasterSupplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
    <div>
        <h2>
            <asp:Label ID="lblJudul" runat="server" Text="Master Supplier"></asp:Label>
        </h2>
    </div>
    <div id="dSupplier" runat="server">
        <div id="dSupplierMSG" runat="server" visible="false"></div>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="tbSearchSuppl" runat="server" Width="100px"></asp:TextBox>&nbsp;
                    &nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By Nama Brand" Value="Brand"></asp:ListItem>
                <asp:ListItem Text="By Kode Supplier" Value="KODE"></asp:ListItem>
            </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="Search Supplier" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" OnClick="btnAddSupplier_Click" />
                </td>
                <td></td>
            </tr>
        </table>
        <div id="dGrid" runat="server" visible="true" class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="BRAND"
                PageSize="10" OnRowCommand="gvSupplier_RowCommand" OnPageIndexChanging="gvSupplier_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgSelect" runat="server" CausesValidation="False" CommandName="SelectRow"
                                    ImageUrl="~/Image/b_ok.png" Text="edit Supplier" />&nbsp;
                            <asp:ImageButton ID="imgedit" runat="server" CausesValidation="False" CommandName="EditRow"
                                ImageUrl="~/Image/b_edit.png" Text="edit Discount Showroom" Visible="false"/>&nbsp;    
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                    <asp:BoundField DataField="PHONE" HeaderText="phone" />
                    <asp:BoundField DataField="ALAMAT" HeaderText="Alamat" />
                    <asp:BoundField DataField="KODE" HeaderText="kode" Visible="true" />
                    <asp:BoundField DataField="VENDOR" HeaderText="Vendor" />
                    <%--<asp:BoundField DataField="SHOWROOM" HeaderText="Nama Supplier" />--%>
                </Columns>
            </asp:GridView>
        </div>
        <%-- Pop up Add Supplier --%>
        <asp:Button ID="btnPopUpAddSppl" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="PopUpAddSppl" runat="server" TargetControlID="btnPopUpAddSppl"
            Drag="true" PopupControlID="PanelPopUpAddSppl" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelPopUpAddSppl" runat="server" BackColor="White" CssClass="ModalWindow" BorderStyle="Ridge" BorderColor="BlanchedAlmond" Style="display: block; top: 684px; left: 39px; width: 80%;">
            <h2>
            <asp:Label ID="lblJudulPopUp" runat="server" Text="Add Supplier"></asp:Label>
        </h2>
            <div id="dAddSpplMsg" runat="server" visible="false"></div>
            <br />
            <table>
                <tr>
                    <td>
                        Kode :
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddKode" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>
                        Brand :
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddBrand" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Alamat :
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddAlamat" runat="server"  ></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>
                        Phone :
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddPhone" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Vendor :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAddVendor" runat="server" DataTextField="Nama" DataValueField="Kode"></asp:DropDownList>
                    </td>
                   
                </tr>
            </table>
            <br />
            <asp:Button ID="btnAddSuppl" runat="server" Text="Add" OnClick="btnAddSuppl_Click" />
            &nbsp
            <asp:Button ID="btnAddCancelSuppl" runat="server" Text="Cancel" OnClick="btnAddCancelSuppl_Click" />
        </asp:Panel>

          <%-- Pop up Edit Supplier --%>
        <asp:Button ID="btnPopUpEditSppl" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="PopUpEditSppl" runat="server" TargetControlID="btnPopUpEditSppl"
            Drag="true" PopupControlID="PanelPopUpEditSppl" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelPopUpEditSppl" runat="server" BackColor="White" CssClass="ModalWindow" BorderStyle="Ridge" BorderColor="BlanchedAlmond" Style="display: block; top: 684px; left: 39px; width: 80%;">
            <h2>
            <asp:Label ID="Label1" runat="server" Text="Edit Supplier"></asp:Label>
        </h2>
            <div id="Div1" runat="server" visible="false"></div>
            <br />
            <table>
                <tr>
                    <td>
                        Kode :
                    </td>
                    <td>
                        <asp:TextBox ID="txtEditKode" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>
                        Brand :
                    </td>
                    <td>
                        <asp:TextBox ID="txtEditBrand" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Alamat :
                    </td>
                    <td>
                        <asp:TextBox ID="txtEditAlamat" runat="server"  ></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>
                        Phone :
                    </td>
                    <td>
                        <asp:TextBox ID="txtEditPhone" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Vendor :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEditVendor" runat="server" DataTextField="Nama" DataValueField="Kode"></asp:DropDownList>
                    </td>
                   
                </tr>
            </table>
            <br />
            <asp:Button ID="btnEditSuppl" runat="server" Text="Edit" OnClick="btnEditSuppl_Click" />
            &nbsp
            <asp:Button ID="btnEditCancelSuppl" runat="server" Text="Cancel" OnClick="btnEditCancelSuppl_Click" />
        </asp:Panel>
    </div>
</asp:Content>
