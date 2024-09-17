<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POTracer.aspx.cs" Inherits="ATMOS_SROM.Master.POTracer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
    <div>
        <h2>
            <asp:Label ID="lblJudul" runat="server">PO TRACER </asp:Label>
        </h2>
    </div>
    <div id="dbrandSearch" runat="server">
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="tbSearch" runat="server" Width="100px"></asp:TextBox>&nbsp;
                    &nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By NO PO" Value="NO_PO"></asp:ListItem>
                <asp:ListItem Text="By NO GR" Value="NO_GR"></asp:ListItem>
            </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </td>

                <td></td>
            </tr>
        </table>
        <div id="dGrid" runat="server" visible="true" class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                PageSize="10" OnRowCommand="gvMain_RowCommand" OnPageIndexChanging="gvMain_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgSelect" runat="server" CausesValidation="False" CommandName="SelectRow"
                                    ImageUrl="~/Image/b_ok.png" Text="edit Showroom" />&nbsp;
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false" />
                    <asp:BoundField DataField="NO_PO" HeaderText="NO_PO" />
                    <asp:BoundField DataField="NO_GR" HeaderText="NO_GR" />
                    <asp:BoundField DataField="PO_REFF" HeaderText="Description" />
                    <asp:BoundField DataField="BRAND" HeaderText="BRAND" />
                    <asp:BoundField DataField="CONTACT" HeaderText="CONTACT" />
                    <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                    <asp:BoundField DataField="PHONE" HeaderText="PHONE" />
                    <asp:BoundField DataField="TotalPOQty" HeaderText="Total QTY" />
                    <asp:BoundField DataField="Status_GR" HeaderText="Status_GR" />
                </Columns>
            </asp:GridView>
        </div>

        <asp:Button ID="btnPopUpPOTracerD" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupPOTracerD" runat="server" TargetControlID="btnPopUpPOTracerD"
            Drag="true" PopupControlID="PanelPOTracerD" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelPOTracerD" runat="server" BackColor="White" CssClass="ModalWindow" BorderStyle="Ridge"
            BorderColor="BlanchedAlmond" Style="display: block; top: 684px; left: 39px; width: 80%;">
            <div id="Div4" runat="server" visible="false"></div>
            <h1>
                <asp:Label ID="lblJudulDetail" runat="server" Text="Detail" Font-Bold="true"></asp:Label>
            </h1>
            <table>
                <tr>
                    <td>PO NO  
                    </td>
                    <td>:
                        <asp:Label ID="lblPoNo" runat="server"></asp:Label>
                        <asp:Label ID="lblPOID" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td width="50px"></td>
                    <td>PO REFF 
                    </td>
                    <td>:
                        <asp:Label ID="lblPoReff" runat="server"></asp:Label>
                    </td>
                    <td width="50px"></td>
                    <td>PO Date 
                    </td>
                    <td>:
                        <asp:Label ID="lblPoDt" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Brand  
                    </td>
                    <td>:<asp:Label ID="lblBrand" runat="server"></asp:Label>
                    </td>
                    <td width="50px"></td>
                    <td>Supplier  
                    </td>
                    <td>:<asp:Label ID="lblSuppl" runat="server"></asp:Label>
                    </td>
                    <td width="50px"></td>
                    <td>Showroom 
                    </td>
                    <td>:
                        <asp:Label ID="lblShowroom" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>GR NO 
                    </td>
                    <td>:
                        <asp:Label ID="lblGrNo" runat="server"></asp:Label>
                    </td>
                    <td width="50px"></td>
                    <td>GR STATUS 
                    </td>
                    <td>:
                        <asp:Label ID="lblGrStat" runat="server"></asp:Label>
                    </td>
                    <td width="50px"></td>
                    <td>Total Qty 
                    </td>
                    <td>:
                        <asp:Label ID="lblTtlQty" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>Contact 
                    </td>
                    <td colspan="6">:
                        <asp:Label ID="lblContact" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Phone / Email 
                    </td>
                    <td colspan="6">:
                        <asp:Label ID="lblPhnEmail" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Address 
                    </td>
                    <td colspan="6">:
                        <asp:Label ID="lblAddr" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td colspan="7" align="right">
                        
                    </td>

                </tr>
                <tr>
                    <td colspan="7" align="left">
                        <asp:TextBox ID="txtDetailSrch" runat="server" Width="100px"></asp:TextBox>&nbsp;
                    &nbsp;
                    <asp:DropDownList ID="ddlDetailSrch" runat="server">
                        <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                        <%--<asp:ListItem Text="By NO GR" Value="NO_GR"></asp:ListItem>--%>
                    </asp:DropDownList>
                        <asp:Button ID="btnDetailSrch" runat="server" Text="Search" OnClick="btnDetailSrch_Click" />
                        &nbsp; <asp:Button ID="btnclose" runat="server" Text="Close Detail" OnClick="btnclose_Click" />
                    </td>

                </tr>
            </table>
            <asp:GridView ID="gvPoTracerD" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID_DETAIL_PO"
                PageSize="10" OnPageIndexChanging="gvPoTracerD_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_DETAIL_PO" HeaderText="ID_DETAIL_PO" Visible="false" />
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false" />
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
        </asp:Panel>

    </div>
</asp:Content>
