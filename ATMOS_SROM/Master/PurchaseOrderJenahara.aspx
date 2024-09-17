<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderJenahara.aspx.cs" Inherits="ATMOS_SROM.Master.PurchaseOrderJenahara" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>

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
            <div>
                <h2>Purchase Order</h2>
            </div>
            <div id="DivMessage" runat="server" visible="false">
            </div>
            <div>
                <br />
                <asp:Label ID="lblPPNPO" runat="server" Font-Bold="true" Text="PPN Percentage : "></asp:Label>
                <asp:TextBox ID="txtPPN" runat="server" MaxLength="6" ValidationGroup="PPNPRCNT"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPPN" runat="server" ControlToValidate="txtPPN" ForeColor="Red"
                    ErrorMessage="Please enter PPN Percentage" ValidationGroup="PPNPRCNT"></asp:RequiredFieldValidator>
                <asp:FilteredTextBoxExtender ID="FiltertxtPPN" runat="server" Enabled="true"
                    TargetControlID="txtPPN" FilterType="Custom, Numbers" ValidChars=".,">
                </asp:FilteredTextBoxExtender>
                <br />
                <br />
                <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;
            <asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/format PO_upload.xlsx">
                <asp:Label ID="lbDownload" runat="server" Text="Download Format Upload Excel"></asp:Label><br />
            </asp:HyperLink>
                <asp:RadioButtonList ID="rblFlag" runat="server">
                    <asp:ListItem Text="Reguler" Value="Reg" Selected="True" />
                    <asp:ListItem Text="Value Only" Value="Val" />
                </asp:RadioButtonList>
                <asp:CheckBox ID="ChkReturPO" runat="server" Text="PO Retur" />
                <br />
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"
                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
            </div>
            <div id="divMain" runat="server" visible="true">
                <hr />
                <asp:TextBox ID="tbSearch" runat="server" Width="251px"></asp:TextBox>&nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By PO Refference" Value="PO_REFF"></asp:ListItem>
                <asp:ListItem Text="By NO PO" Value="NO_PO"></asp:ListItem>
                <asp:ListItem Text="By Status" Value="STATUS"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /><br />
                <asp:Button ID="btnJalan" runat="server" Text="Create Surat Jalan" Visible="false" />
                <br />
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnRowCommand="gvMainCommand" OnPageIndexChanging="gvMainPageChanging" OnRowDataBound="gvMainDataBound" OnSelectedIndexChanged="gvMain_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                    ImageUrl="~/Image/b_drop.png" Text="Delete" />&nbsp;
                                <asp:ImageButton ID="imgPdf" runat="server" CausesValidation="False" CommandName="PrintRow"
                                    ImageUrl="~/Image/pdf.png" Text="Print" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="NO_PO" HeaderText="No PO" />
                            <asp:BoundField DataField="PO_REFF" HeaderText="PO Refference" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="QTY" HeaderText="Qty " />
                            <asp:BoundField DataField="STATUS" HeaderText="Status" />
                            <asp:BoundField DataField="QTY_NOW" HeaderText="Qty now" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }
            </script>

            <asp:Button ID="btnPanelPU" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender0" runat="server" TargetControlID="btnPanelPU"
                Drag="true" PopupControlID="PanelPU" CancelControlID="bPUClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPU" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: block; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnIDPU" runat="server" />
                <asp:HiddenField ID="hdnPUNoPO" runat="server" />
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <b>Detail Purchase Order</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>NO Purchase Order :</b> &nbsp;
                    <asp:TextBox ID="tbPUNoPO" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="tbPUSearch" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlPUSearch" runat="server">
                        <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnPUSearch" runat="server" Text="Search" OnClick="btnPUSearch_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnPUClose" runat="server" Text="Close" OnClick="btnPUClose_Click" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvPU" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                        PageSize="10" OnRowCommand="gvPUCommand" OnPageIndexChanging="gvPUPageChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                    ImageUrl="~/Image/b_drop.png" Text="Delete" />
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
                            <asp:BoundField DataField="COGS" HeaderText="COGS" />
                            <asp:BoundField DataField="PRICE" HeaderText="Retail Price" />
                            <asp:BoundField DataField="QTY" HeaderText="Qty" />
                            <asp:BoundField DataField="QTY_TIBA" HeaderText="Qty Tiba" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <asp:Button ID="btnModalTRM" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnModalTRM"
                Drag="true" PopupControlID="PanelTRM" CancelControlID="bTRMCloseHide" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelTRM" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnTRMSave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="Div1" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnTRMIdTrfStock" runat="server" />
                <asp:HiddenField ID="hdnTRMQty" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label2">Update Detail PO</asp:Label></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bTRMCloseHide" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>NO PO</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMNoPO" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>ITEM CODE</td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMItemCode" ReadOnly="true" runat="server" Width="270px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>JUMLAH BELI
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbTRMQTYBeli" runat="server" Width="75px" />
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="true"
                                TargetControlID="tbTRMQTYBeli" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnTRMSave" runat="server" Text="Save" OnClick="btnTRMSaveClick" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--Pop Up Upload-->
            <asp:Button ID="btnModalUploadReady" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalUploadReady" runat="server" TargetControlID="btnModalUploadReady"
                Drag="true" PopupControlID="PanelUploadReady" CancelControlID="UPRClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelUploadReady" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: block; top: 684px; left: 39px; width: 555px;">
                <br />
                <asp:HiddenField ID="UPRhdnSource" runat="server" />
                <asp:HiddenField ID="UPRhdnFileType" runat="server" />
                <h2>Update Promo</h2>
                <div id="UPRdivMessage" runat="server" visible="false">
                </div>

                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 200px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="UPRlbJudul">Update PO</asp:Label></h2>
                            <hr />
                        </td>
                        <td align="right">
                            <asp:Button ID="UPRClose" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <b>File anda siap di Upload :&nbsp; 
                        <asp:Label ID="UPRlbFileName" runat="server"></asp:Label>
                            </b>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:Button ID="UPRbtnUpload" Text="Upload Now" runat="server" OnClick="UPRbtnUploadClick" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="btnModalPopUpPrintPO" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopUpPrintPO" runat="server" TargetControlID="btnModalPopUpPrintPO"
                Drag="true" PopupControlID="PanelPopUpPrintPO" CancelControlID="UPRClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPopUpPrintPO" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: block; top: 684px; left: 39px; width: 755px;">
                <br />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <h2>Print PO</h2>
                <div id="Div2" runat="server" visible="false">
                </div>

                <table width="100%" cellspacing="4">
                    <tr>
                        <td style="text-align: right">
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="ReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                Style="width: 100%;" ShowPrintButton="false" ShowBackButton="false" Visible="true" ShowRefreshButton="false" ShowFindControls="false" ShowPageNavigationControls="false">
                            </rsweb:ReportViewer>

                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="gvMain" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
