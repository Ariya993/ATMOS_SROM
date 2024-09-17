<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SKU.aspx.cs" Inherits="ATMOS_SROM.Master.SKU" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
  
    <div>
        <h2>SKU</h2>
    </div>
    <div id="dUpload" runat="server">
        <table>
            <tr>
                <td>Upload :
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;       
                </td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload SKU" OnClick="btnUpload_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    <%--<asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/format PO_upload.xlsx" Visible="false">
                        <asp:Label ID="lbDownload" runat="server" Text="Download Format Upload Excel" Visible="false"></asp:Label><br />
                    </asp:HyperLink>--%>
                </td>
            </tr>

            <tr>
                <td>SEARCH :  
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlSearch" runat="server">
                        <asp:ListItem Text="By KODE SKU" Value="KD_SKU"></asp:ListItem>
                        <asp:ListItem Text="By KODE BRAND" Value="KD_BRAND"></asp:ListItem>
                        <asp:ListItem Text="By GRUP" Value="GRUP"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblInfo" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <script type="text/javascript">

            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
            function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

        </script>
    </div>
    <%--<div class="EU_TableScroll" style="display: none" id="dupdata">--%>
    <asp:GridView ID="gvPU" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="MS_SKU_ID"
        PageSize="10" OnPageIndexChanging="gvPUPageChanging">
        <Columns>
            <asp:TemplateField HeaderText="No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MS_SKU_ID" HeaderText="id" SortExpression="MS_SKU_ID" Visible="false" />
            <asp:BoundField DataField="KD_SKU" HeaderText="Kode SKU" />
            <asp:BoundField DataField="KD_BRAND" HeaderText="Brand" />
            <asp:BoundField DataField="GRUP" HeaderText="Grup" />
            <asp:BoundField DataField="DISC_P" HeaderText="Disc P (%)" />
            <asp:BoundField DataField="MARGIN" HeaderText="Margin (%)" />
            <asp:BoundField DataField="BBN_DEPT" HeaderText="Beban Dept (%)" />
            <asp:BoundField DataField="KETERANGAN" HeaderText="Keterangan" />
            <asp:BoundField DataField="FILE_UP" HeaderText="File" />
        </Columns>
    </asp:GridView>
    <%--</div>--%>

    <%--<div class="EU_TableScroll" style="display: none">--%>
    <asp:GridView ID="gvPUSearch" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="MS_SKU_ID"
        PageSize="10" OnPageIndexChanging="gvPUSearch_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MS_SKU_ID" HeaderText="id" SortExpression="MS_SKU_ID" Visible="false" />
            <asp:BoundField DataField="KD_SKU" HeaderText="Kode SKU" />
            <asp:BoundField DataField="KD_BRAND" HeaderText="Brand" />
            <asp:BoundField DataField="GRUP" HeaderText="Grup" />
            <asp:BoundField DataField="DISC_P" HeaderText="Disc P (%)" />
            <asp:BoundField DataField="MARGIN" HeaderText="Margin (%)" />
            <asp:BoundField DataField="BBN_DEPT" HeaderText="Beban Dept (%)" />
            <asp:BoundField DataField="KETERANGAN" HeaderText="Keterangan" />
            <asp:BoundField DataField="FILE_UP" HeaderText="File" />
        </Columns>
    </asp:GridView>
    <%--</div>--%>
</asp:Content>
