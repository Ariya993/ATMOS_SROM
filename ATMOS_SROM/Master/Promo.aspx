<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Promo.aspx.cs" Inherits="ATMOS_SROM.Master.Promo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function notifdelete() {
            var notif = confirm("Are you sure want to delete?");
            if (notif) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .hidden
        {
             display: none;
        }
        
        .divWaiting{
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center; top: 0; left: 0;
            height: 100%;
            width: 100%;
            padding-top:20%;
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

    <asp:UpdatePanel ID="panelMain" runat="server" >
    <ContentTemplate>
    <div id="divAwal" runat="server">
        <h2>Promo Master</h2>   
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <asp:Button ID="btnDivUploadAll" runat="server" Text="Upload Promo All Showroom" Visible="false" OnClick="btnDivUploadClick" />
        <asp:Button ID="btnDivUploadShr" runat="server" Text="Upload Promo per Showroom" Visible="false" OnClick="btnDivUploadClick" />
        <div id="divUploadPromo" runat="server" style="border: 1px solid black; padding:2px 2px 2px 2px;">
            <b>Upload ALL Article Promo Harga</b><br /><br />
            <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" style="margin-bottom:5px;"/> &nbsp; 
            <asp:HyperLink ID="HyperLinkDownloadPromo" runat="server" Target="_blank" NavigateUrl="~/Upload/Format Upload Promo.xlsx">
                <asp:Label ID="lbFormatUploadPromo" runat="server" Text="Download Format Excel Promo" ></asp:Label>
            </asp:HyperLink>
            <br />
            <asp:Button ID="btnUploadPromo" runat="server" Text="Upload" OnClick="btnUploadPromo_Click" />
        </div>

        <div id="divUploadPromoShowroom" runat="server" visible="false" style="border: 1px solid black; padding:2px 2px 2px 2px;">
            <b>Upload Article Promo Per Showroom</b><br /><br />
            <asp:DropDownList ID="ddlUploadShr" runat="server" AppendDataBoundItems="false" DataTextField="SHOWROOM" DataValueField="VALUE"></asp:DropDownList> <br /><br />
            <asp:FileUpload ID="FileUploadShr" runat="server" BorderColor="Black" BorderWidth="1px" style="margin-bottom:5px;" />&nbsp;
            <asp:HyperLink ID="HyperLinkDownloadPromoShr" runat="server" Target="_blank" NavigateUrl="~/Upload/Format Upload Promo Showroom.xlsx">
                <asp:Label id="lbFormatUploadPromoShr" runat="server" Text="Download Format Excel Promo per Showroom"></asp:Label>
            </asp:HyperLink>
            <br />
            <asp:Button ID="btnUploadPromoShr" runat="server" Text="Upload" OnClick="btnUploadPromoShrClick" />
        </div>
        <div id="divMain" runat="server" visible="true">
            <h2>
                <asp:Label ID="lbJudul" runat="server" Text="Promo All Article"></asp:Label>
            </h2>
            <hr />
            <asp:TextBox ID="tbSearch" runat="server" Width="251px"></asp:TextBox>&nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By Barcode" Value="BARCODE_BRG"></asp:ListItem>
                <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>                
                <asp:ListItem Text="By Description" Value="FART_DESC"></asp:ListItem>              
                <asp:ListItem Text="By Flag" Value="FLAG"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick"/><br />
            <br />
            <asp:Button ID="btnDivAllPromo" runat="server" Text="View All Promo" CommandName="allPromo" Visible="false" OnCommand="divPromoClick"/>
            <asp:Button ID="btnDivPromoShr" runat="server" Text="View Promo per Showroom" CommandName="promoSHR" OnCommand="divPromoClick" />
            <div class="EU_TableScroll" style="display: block">
                <div id="divAllPromo" runat="server">
                    <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                    OnRowCommand="gvMainRowCommand" PageSize="10" OnPageIndexChanging="gvMainPageChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                            ImageUrl="~/Image/b_drop.png" Text="Delete" OnClientClick="return notifdelete();" />
                                     </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="ID KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" /> 
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="BARCODE_BRG" HeaderText="Barcode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                            <asp:BoundField DataField="ITEM_CODE_BRG" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="FBRAND" HeaderText="Brand"  />
                            <asp:BoundField DataField="FART_DESC" HeaderText="Description" />
                            <asp:BoundField DataField="FCOL_DESC" HeaderText="Color" />
                            <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" />
                            <asp:BoundField DataField="FLAG" HeaderText="Price Awal" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="SPCL_PRICE" HeaderText="Special Price" />
                            <asp:BoundField DataField="DISCOUNT" HeaderText="Discount" />
                            <asp:BoundField DataField="FLAG" HeaderText="Flag" />
                            <asp:BoundField DataField="FLAG" HeaderText="Price Akhir" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="START_DATE" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="END_DATE" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="CATATAN" HeaderText="Catatan" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="FLAG" HeaderText="Flag" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="FLAG" HeaderText="Flag" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="FLAG" HeaderText="Flag" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>

                <div id="divPromoShr" runat="server" visible="false">
                    <asp:GridView ID="gvPromoShr" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                    OnRowCommand="gvPromoShrRowCommand" PageSize="10" OnPageIndexChanging="gvPromoShrPageChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                            ImageUrl="~/Image/b_drop.png" Text="Delete" OnClientClick="return notifdelete();" />
                                     </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="ID KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" /> 
                            <asp:BoundField DataField="ID_STORE" HeaderText="ID STORE" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" /> 
                            <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" />
                            <asp:BoundField DataField="KODE" HeaderText="Kode" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="BARCODE_BRG" HeaderText="Barcode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="FBRAND" HeaderText="Brand" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="ART_DESC" HeaderText="DESCRIPTION"  />
                            <asp:BoundField DataField="FART_DESC" HeaderText="Article" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="FCOL_DESC" HeaderText="Color" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="FSIZE_DESC" HeaderText="Size" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="PRICE_BRG" HeaderText="Price" />
                            <asp:BoundField DataField="DISCOUNT" HeaderText="Discount" />
                            <asp:BoundField DataField="START_DATE" HeaderText="Start Date" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="END_DATE" HeaderText="End Date" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="KET" HeaderText="KET" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

    <asp:ModalPopupExtender ID="ModalUpload" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelPopUp" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUp" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnId" runat="server" />
        <h2>Update Promo</h2>
        <div id="DivUploadMessage" runat="server" visible="false">
        </div>
       
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 200px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="lblTitleSubPage">Update Promo</asp:Label></h2>
                    <hr />
                </td>
                <td align="right">
                    <asp:Button ID="bClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Item Code</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUItemCode" runat="server" Width="270px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Price</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUPrice" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Discount
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUDiscount" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Start Date</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUStartDate" runat="server" />
                    <asp:CalendarExtender ID="CalenderExtenderStartDate" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbPUStartDate" DefaultView="Days">
                        </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    End Date</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUEndDate" runat="server" />
                    <asp:CalendarExtender ID="CalenderExtenderEndDate" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbPUEndDate" DefaultView="Days">
                        </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Flag</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUFlag" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Catatan</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUCatatan" runat="server" Width="270px" Height="50px" TextMode="MultiLine"/>
                </td>
            </tr>
          
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                     <div id="divUpload" runat="server">
                        <asp:Button ID="btnPUSave" runat="server" Text="Save" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Button ID="btnModalUploadReady" runat="server" Style="display: none" />

    <asp:ModalPopupExtender ID="ModalUploadReady" runat="server" TargetControlID="btnModalUploadReady"
        Drag="true" PopupControlID="PanelUploadReady" CancelControlID="UPRClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelUploadReady" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="UPRhdnSource" runat="server" />
        <h2>Update Promo</h2>
        <div id="UPRdivMessage" runat="server" visible="false">
        </div>
       
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 200px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="UPRlbJudul">Update Promo</asp:Label></h2>
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

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUploadPromo" />
        <asp:PostBackTrigger ControlID="btnUploadPromoShr" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
