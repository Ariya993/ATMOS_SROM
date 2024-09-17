<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Whole.aspx.cs" Inherits="ATMOS_SROM.Sales.Whole" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        <div>
        <h2><asp:Label ID="lbJudul" runat="server" Text="Sales Page (Wholesale)"></asp:Label></h2>
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <asp:Button ID="btnRetur" runat="server" Text="Retur" Visible="false" OnClick="btnReturClick" />
        <asp:Button ID="btnAllWholeSale" runat="server" Text="Return" OnClick="btnAllWholeSaleClick" />
        <div id="divStore" runat="server">
            <asp:HiddenField ID="hdnIDStore" runat="server" />
            <table>
                <tr>
                    <td>
                        <b>Tanggal Transaksi :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbDate" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendeExtenderTrans" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbDate" DefaultView="Days">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBDate" runat="server" ControlToValidate="tbDate" ForeColor="Red"
                        ErrorMessage="Please enter tanggal transaksi" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Nama Pembeli :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbNama" runat="server" MaxLength="35"></asp:TextBox>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBNama" runat="server" ControlToValidate="tbNama" ForeColor="Red"
                        ErrorMessage="Please enter nama pembeli" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>          
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Kode Pembeli :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbKode" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBKode" runat="server" ControlToValidate="tbKode" ForeColor="Red"
                        ErrorMessage="Please enter kode pembeli" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Margin :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbMargin" runat="server" MaxLength="3" ValidationGroup="Wholesale"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTBMargin" runat="server" ControlToValidate="tbMargin" ForeColor="Red"
                        ErrorMessage="Please enter margin" ValidationGroup="Wholesale"></asp:RequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="FilterTBMargin" runat="server" Enabled="true"
                            TargetControlID="tbMargin" FilterType="Numbers">
                        </asp:FilteredTextBoxExtender>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div id="divRetur" runat="server" visible="false">
            <b>NO SALES ORDER :</b>&nbsp;
            <asp:TextBox ID="tbReturNoSO" runat="server"></asp:TextBox>
        </div>
        <br />
        <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick" ValidationGroup="Wholesale" />
        <br />
        <asp:Button ID="btnInput" runat="server" Text="Input" OnClick="btnInput_Click" ValidationGroup="Wholesale" />
        <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                OnRowCommand="gvMain_RowCommand" PageSize="10">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                    ImageUrl="~/Image/b_edit.png" Text="Edit" />
                             </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                    <asp:BoundField DataField="BARCODE" HeaderText="Barcode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" /> 
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" /> 
                    <asp:BoundField DataField="ART_DESC" HeaderText="Desc" />
                    <asp:BoundField DataField="WARNA" HeaderText="Color" />
                    <asp:BoundField DataField="SIZE" HeaderText="Size" />
                    <asp:BoundField DataField="QTY" HeaderText="Qty" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                    <asp:BoundField DataField="PRICE" HeaderText="@Price" DataFormatString="{0:0,0.00}" />                    
                    <asp:BoundField DataField="DISCOUNT" HeaderText="Discount(%)" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="NET_PRICE" HeaderText="Net Price" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                    <asp:BoundField DataField="TOTAL_PRICE" HeaderText="TOTAL_PRICE" DataFormatString="{0:0,0.00}" ItemStyle-BackColor="Aquamarine" HeaderStyle-BackColor="CornflowerBlue" />
                                        
                    <asp:BoundField DataField="ART_PRICE" HeaderText="Art Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="SPCL_PRICE" HeaderText="Special Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="JENIS_DISCOUNT" HeaderText="Jenis Discount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="ID_ACARA" HeaderText="ID Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="NET_ACARA" HeaderText="Net Acara" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="ID_KDBRG" HeaderText="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                    <asp:BoundField DataField="RETUR" HeaderText="Retur" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Margin_Input"/>&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </div>    

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

    <!--Pop Up Search-->
    <asp:Button ID="btnModalSearchItemCode" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalSearchItemCode" runat="server" TargetControlID="btnModalSearchItemCode"
        Drag="true" PopupControlID="PanelSearch" CancelControlID="bSearchClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelSearch" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnSearchSave"
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIDSearch" runat="server" />
        <asp:HiddenField ID="hdnIDBayarRetur" runat="server" />
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="Label3">Search</asp:Label></h2>
                    <hr/>
                </td>
                <td align="right">
                    <asp:Button ID="bSearchClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Search </td>
                <td colspan="2">
                    <asp:TextBox ID="tbSearchBy" runat="server" Width="270px" />&nbsp;
                    <asp:DropDownList ID="ddlSearchBy" runat="server">
                        <asp:ListItem Value="BARCODE" Text="By Barcode"></asp:ListItem>
                        <asp:ListItem Value="ITEM_CODE" Text="By Item Code"></asp:ListItem>
                        <asp:ListItem Value="FGROUP" Text="By Group Item"></asp:ListItem>
                        <asp:ListItem Value="PRICE" Text="By Tag Price"></asp:ListItem>
                    </asp:DropDownList> &nbsp
                    <asp:Button ID="btnSearchSearch" Text="Search" runat="server" OnClick="btnSearchSearchClick" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvSearch" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvSearchPageIndexChanging"
                        OnRowCommand="gvSearcRowCommand"
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
                            <asp:BoundField DataField="BARCODE" HeaderText="BARCODE" />
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE" />
                            <asp:BoundField DataField="ART_DESC" HeaderText="ARTICLE DESCRIPTION" />
                            <asp:BoundField DataField="PRICE" HeaderText="PRICE" />
                            <asp:BoundField DataField="STOCK" HeaderText="Stock" />
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
                       
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                    <asp:Button ID="btnSearchSave" runat="server" Text="Save" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <!--Pop Up Edit Quantity-->
    <asp:Button ID="btnModalChangeQty" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalChangeQty" runat="server" TargetControlID="btnModalChangeQty"
        Drag="true" PopupControlID="PanelPopUp" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUp" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnPUSave"
        Style="display: none; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnId" runat="server" />
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="lblTitleSubPage">Quantity</asp:Label></h2>
                    <hr />
                </td>
                <td align="right">
                    <asp:Button ID="bClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr id="trBarcode" runat="server" visible="false">
                <td>
                </td>
                <td>
                    Barcode</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUBarcode" ReadOnly="true" runat="server" Width="270px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Item Code</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUItemCode" ReadOnly="true" runat="server" Width="270px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Description</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUDesc" ReadOnly="true" runat="server" Width="270px" Height="50px" TextMode="MultiLine"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Size
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUSize"  ReadOnly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Price</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUPrice" runat="server" onchange ="this.value=formatCurrencyPAY(this.value);" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Quantity</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUQty" runat="server" MaxLength="2"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="FilteredTextPUQty" runat="server" Enabled="true"
                        TargetControlID="tbPUQty" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>            
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                    <asp:Button ID="btnPUSave" runat="server" Text="Save" 
                        OnClick="btnPUSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <!--Pop Up Finish dan Change-->
    <asp:Button ID="btnModalChange" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalChange" runat="server" TargetControlID="btnModalChange"
        Drag="true" PopupControlID="PanelDone" CancelControlID="bDONECloseHide" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelDone" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="bDONEClose"
        Style="display: none; top: 684px; left: 39px; width: 555px;">
        <br />
        <div id="Div1" runat="server" visible="false"></div>
        <asp:HiddenField ID="hdnIDDone" runat="server" />
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="Label2">No SO : </asp:Label> 
                        <asp:Label runat="server" ID="lbDONEBON"></asp:Label></h2><br />
                    <hr style="width: 100%" />
                </td>
                <td align="right">
                    <asp:Button ID="bDONECloseHide" runat="server" Text="Cancel" Width="100px" 
                       style="height: 26px; display:none;" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="2">
                    Total Penjualan : <br />
                    <b>RP : <asp:Label ID="lblDONEChange" runat="server"></asp:Label>
                    </b>
                </td>
            </tr>         
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                    <asp:Button ID="bDONEClose" runat="server" Text="DONE" ValidationGroup="Add_Input"
                        OnClick="bDONEClose_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <!--Pop Up Search No Bon-->
    <asp:Button ID="btnModalSearchNoBon" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalSearchNoBon" runat="server" TargetControlID="btnModalSearchNoBon"
        Drag="true" PopupControlID="PanelBON" CancelControlID="bBONClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelBON" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" 
        Style="display: block; top: 684px; left: 39px; width: 80%;">
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
                            <asp:BoundField DataField="TGL_TRANS" HeaderText="Tanggal Transaksi" DataFormatString="{0:dd/MM/yyyy}" />
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

        <!--Pop Up Pilih Item untuk Retur-->
    <asp:Button ID="btnModalItemRetur" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalItemRetur" runat="server" TargetControlID="btnModalItemRetur"
        Drag="true" PopupControlID="PanelIRetur" CancelControlID="bIReturCloseHide" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelIRetur" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" 
        Style="display: block; top: 684px; left: 39px; width: 80%;">
        <br />
        <asp:HiddenField ID="hdnIReturIDBayar" runat="server" />
        <asp:HiddenField ID="hdnIReturIDKdbrg" runat="server" />
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="Label5">Item Retur</asp:Label></h2>
                    <hr/>
                </td>
                <td align="right">
                    <asp:Button ID="bIReturCloseHide" runat="server" Text="Cancel" Width="100px" Visible="false" />
                    <asp:Button ID="bIReturClose" runat="server" Text="Cancel" Width="100px" OnClick="bIReturCloseClick" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <b>NO BON :</b> </td>
                <td colspan="2">
                    <asp:TextBox ID="tbIReturNoBon" ReadOnly="true" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <b>STORE :</b> </td>
                <td colspan="2">
                    <asp:TextBox ID="tbIReturStore" ReadOnly="true" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvIRetur" runat="server" AllowPaging="true" PageSize="20" DataKeyNames="ID"
                        CssClass="table table-bordered EU_DataTable" AutoGenerateColumns="false" 
                        OnRowCommand="gvIReturCommand" OnPageIndexChanging="gvIReturPageChanging">
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
                            <asp:BoundField DataField="ID_BAYAR" HeaderText="id Bayar" SortExpression="ID_BAYAR" Visible="false" />
                            <asp:BoundField DataField="ID_KDBRG" HeaderText="id Kdbrg" SortExpression="ID_KDBRG" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                            <asp:BoundField DataField="TAG_PRICE" HeaderText="Tag Price" />
                            <asp:BoundField DataField="BON_PRICE" HeaderText="Bon Price" DataFormatString="{0:0,0.00}" />
                            <asp:BoundField DataField="NILAI_BYR" HeaderText="Price" DataFormatString="{0:0,0.00}" />
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
                       
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                    <asp:Button ID="Button3" runat="server" Text="Save" />
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
