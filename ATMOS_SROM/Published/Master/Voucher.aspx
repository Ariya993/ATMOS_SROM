<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="ATMOS_SROM.Master.Voucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <asp:Panel ID="PanelMain" runat="server">
        <div>
            <h1>MASTER VOUCHER</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>
            <div>
                <asp:FileUpload ID="fileUpload" runat="server" ToolTip="Upload File Master Voucher" />
                <asp:Button ID="btnAdd" runat="server" Text="Upload Voucher"
                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" OnClick="btnAddClick" />
                <br />
                <asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/Format_Upload_Voucher.xlsx">
                    <asp:Label ID="lbFormatUpload" runat="server" Text="Download Format Excel"></asp:Label>
                </asp:HyperLink>
            </div>
            <br />
            <div>
                <asp:Button ID="btnAddVoucher" runat="server" Text="Add New Voucher"
                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" OnClick="btnAddVoucherClick" />
            </div>
            <br />
            <i>*no voucher</i><br />
            <asp:TextBox ID="tbFilterText" runat="server" Width="300px" />
            &nbsp;
    <asp:DropDownList ID="ddlFilter" runat="server" Style="font-size: 1.2em;">
        <asp:ListItem Text="No Voucher" Value="NO_VOUCHER" />
        <asp:ListItem Text="Status Voucher" Value="STATUS_VOUCHER" />
    </asp:DropDownList>&nbsp;
    <asp:Button ID="btnFilter" runat="server" Text="filter" OnClick="btnFilter_Click"
        Width="80px" />
            <br />
            <br />
            <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                    DataKeyNames="ID" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMainPageChanging" OnRowCommand="gvMainCommand">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false" />
                        <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" ToolTip="Edit Data Voucher"
                                        CommandName="EditRow" Style="width: 10px; height: 10px"
                                        ImageUrl="~/Image/b_edit.png" Text="View" />

                                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" ToolTip="View Data Voucher" Visible="false"
                                        CommandName="ViewRow" Style="width: 10px; height: 10px"
                                        ImageUrl="~/Image/checkmark.png" Text="View" />
                                    <asp:ImageButton ID="imgTransaction" runat="server" CausesValidation="False" ToolTip="View Data Voucher" Visible="false"
                                        CommandName="TransRow" Style="width: 10px; height: 10px"
                                        ImageUrl="~/Image/b_memo1.png" Text="View" />
                                    &nbsp;<asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" ToolTip="Delete Data Voucher" Visible="false"
                                        CommandName="DeleteRow" Style="width: 10px; height: 10px"
                                        ImageUrl="~/Image/b_drop.png" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NO_VOUCHER" HeaderText="No Voucher" />
                        <asp:BoundField DataField="NILAI" HeaderText="Nilai" DataFormatString="{0:0,0}" />
                        <asp:BoundField DataField="JENIS" HeaderText="Jenis" />
                        <asp:BoundField DataField="VALID_FROM" HeaderText="From" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="VALID_UNTIL" HeaderText="Until" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="STATUS_VOUCHER" HeaderText="Status Voucher" />

                        <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="UPDATED_BY" HeaderText="Updated By" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="UPDATED_DATE" HeaderText="Updated Date" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:Button ID="btnExcel" Text="Generate Excel File" runat="server" OnClick="btnExcelClick" />
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        </div>
    </asp:Panel>

    <!--Pop EditData-->
    <asp:Button ID="btnViewData" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalViewData" runat="server" TargetControlID="btnViewData"
        Drag="true" PopupControlID="PanelViewData" CancelControlID="bVDClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelViewData" runat="server" BackColor="WhiteSmoke" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: none; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnVDID" runat="server" />
        <div id="divVDMessage" runat="server" visible="false" />
        <table style="width: 80%">
            <tr>
                <td style="width: 10px;"></td>
                <td colspan="3">
                    <h2>
                        <asp:Label runat="server" ID="lbVDJudul">View Data Voucher</asp:Label></h2>
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="width: 120px;">
                    <asp:Label ID="lbVDNoVoucher" runat="server" Style="font-size: 1.2em;" Text="No Voucher"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbVDNoVoucher" runat="server" Width="200px" />&nbsp;
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lbVDNilai" runat="server" Style="font-size: 1.2em;" Text="Nilai"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbVDNilai" runat="server" Width="400px" MaxLength="75" />&nbsp;
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lbVDJenis" runat="server" Style="font-size: 1.2em;" Text="Jenis"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlVDJenis" runat="server">
                        <asp:ListItem Text="Value" Value="V" />
                        <asp:ListItem Text="Percentage" Value="P" />
                    </asp:DropDownList>
                </td>
            </tr>
            <%--            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="lbVDBrand" runat="server" style="font-size:1.2em;" Text="Brand" Visible="false"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlVDBrand" runat="server" DataValueField="BRAND_ID" DataTextField="BRAND" AppendDataBoundItems="false" Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="lbVDEvent" runat="server" style="font-size:1.2em;" Text="Event" Visible="false"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbVDEvent" runat="server" Width="400px" MaxLength="250"  Visible="false"/>&nbsp;&nbsp;
                </td>
            </tr>--%>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lbVDStartDate" runat="server" Style="font-size: 1.2em;" Text="Valid From"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbVDStartDates" runat="server" Width="400px" MaxLength="20" ReadOnly="true" />&nbsp;&nbsp;                    
                    <asp:CalendarExtender runat="server" ID="CalendarStartDate" TargetControlID="tbVDStartDates" FirstDayOfWeek="Monday"
                        DefaultView="Days" Format="dd-MM-yyyy">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lbVDEndDate" runat="server" Style="font-size: 1.2em;" Text="Valid Until"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbVDEndDates" runat="server" Width="400px" MaxLength="20" ReadOnly="true" />&nbsp;&nbsp;
                    <asp:CalendarExtender runat="server" ID="CalendarEndDate" TargetControlID="tbVDEndDates" FirstDayOfWeek="Monday"
                        DefaultView="Days" Format="dd-MM-yyyy">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Label ID="lbVDStatus" runat="server" Style="font-size: 1.2em;" Text="Status"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlVDStatus" runat="server" Style="font-size: 1.2em;">
                        <asp:ListItem Text="ACTIVE" Value="ACTIVE" />
                        <asp:ListItem Text="DEACTIVE" Value="DEACTIVE" />
                        <asp:ListItem Text="DONE" Value="DONE" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none">
                <td></td>
                <td colspan="3">
                    <div class="EU_TableScroll" style="display: block">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="20" DataKeyNames="ID"
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
                                <asp:BoundField DataField="KODE_CUST" HeaderText="Store" />
                                <asp:BoundField DataField="NO_BON" HeaderText="No Bon" />
                                <asp:BoundField DataField="TGL_TRANS" HeaderText="Tanggal Transaksi" DataFormatString="{0:dd/MM/yyyy - HH:mm:ss}" />
                                <asp:BoundField DataField="NET_BAYAR" HeaderText="PRICE" DataFormatString="{0:0,0.00}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>

            <tr>
                <td></td>
                <td colspan="3" align="left">
                    <asp:Button ID="btnVDSave" runat="server" Text="Save"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" OnClick="btnVDSaveClick" />&nbsp;
                    <asp:Button ID="bVDClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
