<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sale.aspx.cs" Inherits="ATMOS_SROM.Sales.Sale" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function formatCurrency(num) {
            var value = num;
            var bayar = document.getElementById(value).value;
            num = num.toString().replace(',', '');
            bayar = bayar.toString().replace(',', '');
            bayar = bayar.toString().replace('.', '');

            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));


            return (num);
        }

        function numberWithCommas(x) {
            var parts = x.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
        }

        function thousandSep(val) {
            var value = val.toString().replace(',', '');
            return String(value).split("").reverse().join("")
                          .replace(/(\d{3}\B)/g, "$1,")
                          .split("").reverse().join("");
        }

        function CheckForPastDate(sender, args) {
            var selectedDate = new Date();
            selectedDate = sender._selectedDate;
            var todayDate = new Date();
            if (selectedDate.getDateOnly() > todayDate.getDateOnly()) {

                sender._selectedDate = todayDate;
                sender._textbox.set_Value(sender._selectedDate.format(sender._format));

                alert("Cannot Select Date in the Future");

            }
        }

        function formatCurrencyPAY(num) {
            num = num.toString().replace(',', '');
            num = num.toString().replace(-/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));
            return (num);
        }

        function disableSubmit(obj) {
            obj.disabled = true;
        }

        function notifVoid() {
            var notif = confirm("Are you sure want to void this no Bon?");
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

    <asp:UpdatePanel ID="panelMain" runat="server" DefaultButton="btnInput" >
        <ContentTemplate>
            <iframe id="iframePDF" Visible="false" runat="server" style="display:none;">
            </iframe>
            <div>
                <h2><asp:Label ID="lbJudul" runat="server" ></asp:Label></h2>
                <div id="DivMessage" runat="server" visible="false" />
                <asp:HiddenField ID="hdnNoUrutBon" runat="server" />
                <asp:Button ID="btnRetur" runat="server" Text="Retur / Penukaran barang penjualan" OnClick="btnReturClick" /> &nbsp;
                <asp:Button ID="btnVoid" runat="server" Text="Void" OnClick="btnVoidClick" /> &nbsp;
                <asp:Button ID="btnNoSale" runat="server" Text="NO SALE" ToolTip="Tidak ada penjualan barang hari ini" OnClick="btnNoSaleClick" />
        
                <div id="divStore" runat="server">
                    <asp:HiddenField ID="hdnIDStore" runat="server" />
                    <table>
                        <tr>
                            <td>
                                Tanggal Transaksi :
                            </td>
                            <td>
                                <asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendeExtenderTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                    TargetControlID="tbDate" DefaultView="Days">
                                </asp:CalendarExtender>&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStore" runat="server" AppendDataBoundItems="false" DataTextField="SHOWROOM" DataValueField="KODE">
                                </asp:DropDownList> &nbsp;
                                <asp:CheckBox ID="cbStoreChange" runat="server" ToolTip="Centang agar Store tidak diganti" Text="Don't Change Store" />
                            </td>
                        </tr>
                        <tr id="trMarginSIS" runat="server">
                            <td>
                                <b>Discount Margin :&nbsp;</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbMarginSIS" runat="server" MaxLength="2" Width="30px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftbMarginSIS" runat="server" TargetControlID="tbMarginSIS" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbWarningSIS" Visible="false" runat="server" Text="Pilih Showroom, Margin dan Tanggal Transaksinya dengan benar!" Font-Size="Small" style="color:red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div id="divRetur" runat="server" visible="false">
                    <b>NO BON :</b>&nbsp;
                    <asp:TextBox ID="tbReturNoBon" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                <div id="divVoid" runat="server" visible="false">
                    <b>NO Bon :</b> &nbsp;
                    <asp:TextBox ID="tbVoidNoBon" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                <br />
                <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick" />
                <br />
                <asp:Button ID="btnInput" runat="server" Text="Input" OnClick="btnInput_Click" />
                <asp:Label ID="lbOtherIncome" runat="server" Visible="false" ></asp:Label>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="false" DataKeyNames="ID"
                        OnRowCommand="gvMain_RowCommand" OnRowDataBound="gvMain_DataBound">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                        <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                            ImageUrl="~/Image/b_drop.png" Text="Delete"/>&nbsp;
                                        <asp:ImageButton ID="imgCommand" runat="server" CausesValidation="False" CommandName="CommandRow"
                                            ImageUrl="~/Image/b_comment.png" Text="Command" Visible="false" />
                                     </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" /> 
                            <asp:BoundField DataField="ART_DESC" HeaderText="Desc" />
                            <asp:BoundField DataField="WARNA" HeaderText="Color" />
                            <asp:BoundField DataField="SIZE" HeaderText="Size" />
                            <asp:BoundField DataField="QTY" HeaderText="Qty" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                            <asp:BoundField DataField="PRICE" HeaderText="@Price" DataFormatString="{0:0,0.00}" />                    
                            <asp:BoundField DataField="DISCOUNT" HeaderText="Discount(%)" />
                            <asp:BoundField DataField="NET_DISCOUNT" HeaderText="Net Discount" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                            <asp:BoundField DataField="NET_PRICE" HeaderText="Net Price" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="TOTAL_PRICE" HeaderText="TOTAL_PRICE" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" /> 
                    
                            <asp:BoundField DataField="ART_PRICE" HeaderText="Art Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="SPCL_PRICE" HeaderText="Special Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="JENIS_DISCOUNT" HeaderText="Jenis Discount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="ID_ACARA" HeaderText="ID Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="NET_ACARA" HeaderText="Net Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="RETUR" HeaderText="Retur" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="MEMBER" HeaderText="Member" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:TemplateField HeaderText="Change Price" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbChangePrice" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancelClick" />
            </div>

            <!--Pop Up Search No Bon-->
            <asp:Button ID="btnModalSearchNoBon" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalSearchNoBon" runat="server" TargetControlID="btnModalSearchNoBon"
                Drag="true" PopupControlID="PanelBON" CancelControlID="bBONClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelBON" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" 
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="hdnBONID" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td>
                        </td>
                        <td style="width: 10px;" colspan="2">
                            <h2>
                                <asp:Label runat="server" ID="Label4">Search</asp:Label></h2>
                            <hr/>
                        </td>
                        <td align="right">
                            <asp:Button ID="bBONClose" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            Search </td>
                        <td colspan="2">
                            <asp:TextBox ID="tbBONSearch" runat="server" Width="270px" />&nbsp;
                            <asp:DropDownList ID="ddlBONSearch" runat="server">
                                <asp:ListItem Value="NO_BON" Text="By No Bon"></asp:ListItem>
                            </asp:DropDownList> &nbsp
                            <asp:Button ID="btnBONSearch" Text="Search" runat="server" OnClick="btnBONSearchClick" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <div class="EU_TableScroll" style="display: block">
                            <asp:GridView ID="gvBON" runat="server" AllowPaging="true" PageSize="20" DataKeyNames="ID"
                                CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false"
                                OnRowCommand="gvBONCommand" OnPageIndexChanging="gvBONPageChanging">
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
                        <td>
                        </td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnBONSave" runat="server" Text="Save" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
