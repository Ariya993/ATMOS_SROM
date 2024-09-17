<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ShowRoomEdit.aspx.cs" Inherits="ATMOS_SROM.Master.ShowRoomEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
  <script src="http://www.decorplanit.com/plugin/autoNumeric-1.9.18.js"></script>
  <script type='text/javascript'>
      $(function ($) {
          // Defines your numeric masking for any elements with the class numericOnly and sets the maximum to 10
          $('.numericOnly').autoNumeric('init', { vMax: 9999999999 });
      });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release"></asp:ScriptManager>
    <div>
        <h2>
            <asp:Label ID="lblJudul" runat="server"></asp:Label>
        </h2>
    </div>
    <div id="dSelection" runat="server">
        <asp:Button ID="btndShowroomShow" runat="server" OnClick="dShowroomShow_Click" Text="Showroom" />
        &nbsp
        <asp:Button ID="btndBrandShow" runat="server" OnClick="dBrandShow_Click" Text="Brand" />
    </div>
    <br />
    <div id="dShowroom" runat="server" visible="true">
        <div id="dShowroomMSG" runat="server" visible="false"></div>
        <div id="dbrandSearch" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="tbSearchShowRoom" runat="server" Width="100px"></asp:TextBox>&nbsp;
                    &nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By Showroom" Value="SHOWROOM"></asp:ListItem>
                <asp:ListItem Text="By Kode Showroom" Value="KODE"></asp:ListItem>
            </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" Text="Search Showroom" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnAddShowroom" runat="server" Text="Add Showroom" OnClick="btnAddShowroom_Click" />
                        <asp:Button ID="btnSetDisc" runat="server" Text="Set Discount EPC" OnClick="btnSetDiscEPC_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>

        </div>
        <div id="dGrid" runat="server" visible="true" class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvShowroom" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="SHOWROOM"
                PageSize="10" OnRowCommand="gvShowroom_RowCommand" OnPageIndexChanging="gvShowroom_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgSelect" runat="server" CausesValidation="False" CommandName="SelectRow"
                                    ImageUrl="~/Image/b_ok.png" Text="edit Showroom" />&nbsp;
                            <asp:ImageButton ID="imgedit" runat="server" CausesValidation="False" CommandName="EditRow"
                                ImageUrl="~/Image/b_edit.png" Text="edit Discount Showroom" />&nbsp;    
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" SortExpression="SHOWROOM" Visible="true" />
                    <asp:BoundField DataField="STORE" HeaderText="Store" />
                    <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                    <asp:BoundField DataField="PHONE" HeaderText="phone" />
                    <asp:BoundField DataField="ALAMAT" HeaderText="Alamat" />
                    <asp:BoundField DataField="KODE" HeaderText="kode" Visible="true" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
        <%-- Pop Up Set Disc EPC --%>
        <asp:Button ID="btnPopUpSetDiscEPC" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="PopUpSetDiscEPC" runat="server" TargetControlID="btnPopUpSetDiscEPC"
            Drag="true" PopupControlID="PanelPopUpSetDiscEPC" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelPopUpSetDiscEPC" runat="server" BackColor="White" CssClass="ModalWindow" BorderStyle="Ridge" BorderColor="BlanchedAlmond" Style="display: block; top: 684px; left: 39px; width: 80%;">
          <div id="dSetDiscMsg" runat="server" visible="false"></div>
            <h1>
                Set Discount EPC
            </h1>
            <br />
            <table>
                <tr>
                    <td colspan="3">
                        <div style="margin-bottom: 20px">
                            <asp:RadioButton ID="optDiscKaryawanEPC" runat="server" GroupName="Disc" Text="Disc Karyawan" AutoPostBack="true" Checked="true" OnCheckedChanged="optDiscKaryawanEPC_OnCheckedChanged"/>
                            &nbsp
                            <asp:RadioButton ID="optDiscRelasiEPC" runat="server" GroupName="Disc" Text="Disc Relasi" AutoPostBack="true" Visible="true" OnCheckedChanged="optDiscRelasiEPC_OnCheckedChanged"/>
                            &nbsp
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>BRAND</td>
                    <td>
                        <asp:DropDownList ID="ddlBrandEPC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBrandEPC_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>TIPE DISCOUNT</td>
                    <td>
                        <asp:DropDownList ID="ddlTipeDiscEPC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipeDiscEPC_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>DISCOUNT</td>
                    <td>
                        <asp:TextBox ID="txDiscount" runat="server"></asp:TextBox>
                        <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBox" ValidChars="0123456789" FilterType="Custom" TargetControlID="txDiscount" />
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txDiscount" ErrorMessage="Please Enter Only Numbers" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">   
                        <div style="margin-top: 20px">
                            <asp:Button ID="btnUpdateEPC" runat="server" Text="Update" OnClick="btnUpdateEPC_Click" />
                            <asp:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnUpdateEPC">
                            </asp:ConfirmButtonExtender>
                            &nbsp
                            <asp:Button ID="btnCloseEPC" runat="server" Text="Close" OnClick="btnCloseEPC_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%-- Pop Up Update Confirmation --%>
        <asp:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnUpdateEPC" OkControlID = "btnYes"
                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Confirmation
            </div>
            <div class="body">
                Do you want to update EPC discount?
            </div>
            <div class="footer">
                <asp:Button ID="btnYes" runat="server" Text="Yes" />
                <asp:Button ID="btnNo" runat="server" Text="No" />
            </div>
        </asp:Panel>
        <%-- Pop Up Edit Disc Showroom --%>
        <asp:Button ID="Button1" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnPopUpAddSHR"
            Drag="true" PopupControlID="Panel1" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" BackColor="White" CssClass="ModalWindow" BorderStyle="Ridge" BorderColor="BlanchedAlmond" Style="display: block; top: 684px; left: 39px; width: 80%;">
          <div id="Div4" runat="server" visible="false"></div>
            <h1>
                Edit Discount Showroom
            </h1>
            <br />
            <asp:Label ID="lblinfo" runat="server" ForeColor="Red" Text="* Centang Untuk Menambahkan Diskon"></asp:Label>
            <br />
            <asp:Label ID="lblKdShrEditDisc" runat="server" Visible="false"></asp:Label>
            <table>
                <tr>
                    <td>Showroom : </td>
                    <td>
                        <asp:Label ID ="lblNamaSHR" runat="server"></asp:Label>
                        <asp:Label ID ="libIDSHR" Visible="false" runat="server"></asp:Label>
                    </td>
                    <td width="50px"></td>
                     <td>Kode Showroom : </td>
                    <td>
                        <asp:Label ID ="lblKodeSHR" runat="server"></asp:Label>
                        <asp:Label ID ="lblBrandSHR" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:CheckBox ID="ChckDiscKaryawanEdit" runat="server" Text="Disc Karyawan" />
                          &nbsp
                           <asp:CheckBox ID="ChckDiscRelasiEdit" runat="server" Text="Disc Relasi" Visible="true"/>
                          &nbsp
                           <asp:CheckBox ID="ChckDiscVIPMemberEdit" runat="server" Text="Disc VIP Member" Visible="true"/>
                          &nbsp</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btneditDiscSHR" runat="server" Text="Update" OnClick="btneditDiscSHR_Click" />
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btneditDiscSHRClose" runat="server" Text="Close" OnClick="btneditDiscSHRClose_Click" />
                    </td>
                </tr>
            </table>
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                PageSize="10" >
                 <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="true" />
                    <asp:BoundField DataField="KODE" HeaderText="KODE" />
                    <asp:BoundField DataField="SHOWROOM" HeaderText="SHOWROOM" />
                    <asp:BoundField DataField="DISCOUNT" HeaderText="DISCOUNT" />
                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                   <asp:BoundField DataField="TIPE" HeaderText="TIPE" />
                     <asp:BoundField DataField="TIPE_DISCOUNT" HeaderText="TIPE_DISCOUNT" />
                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                 </Columns>
                 </asp:GridView>
            </asp:Panel>
        <%-- Pop up Add Showroom --%>
        <asp:Button ID="btnPopUpAddSHR" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="PopUpAddSHR" runat="server" TargetControlID="btnPopUpAddSHR"
            Drag="true" PopupControlID="PanelPopUpAddSHR" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelPopUpAddSHR" runat="server" BackColor="White" CssClass="ModalWindow" BorderStyle="Ridge" BorderColor="BlanchedAlmond" Style="display: block; top: 684px; left: 39px; width: 80%;">
          <div id="dAddShrMsg" runat="server" visible="false"></div>
              <table>
                <tr>
                    <td>Brand </td>
                    <td>
                        <asp:DropDownList ID="ddlBrandShr" runat="server" DataTextField="Nama" DataValueField="Kode"></asp:DropDownList>
                    </td>
                    <td width="50px"></td>
                    <td>Status </td>
                    <td>
                        <asp:DropDownList ID="ddlStatusBrandShr" runat="server" DataTextField="Nama" DataValueField="Kode" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusBrandShr_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td width="50px"></td>

                </tr>
                <tr>
                    <td>Kode Showroom </td>
                    <td>
                        <asp:TextBox ID="txtkdSHR" runat="server" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="lblNoUrutSHR" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td width="50px"></td>
                    <td>Showroom : 
                    </td>
                    <td>
                        <asp:TextBox ID="txtShrName" runat="server" OnTextChanged="txtShrName_TextChanged" AutoPostBack="true"></asp:TextBox>

                    </td>
                    <td width="50px"></td>
                        <td style="display:none">Store : 
                        </td>
                        <td style="display:none">
                            <asp:TextBox ID="txtStoreShr" runat="server" Text=""></asp:TextBox>
                        </td>
                </tr>
                 <tr>
                        <td>Brand Jual : 
                        </td>
                        <td>
                             <%--<asp:DropDownList ID="ddlBrandJual" runat="server" DataTextField="Nama" DataValueField="Kode"></asp:DropDownList>--%>
                            <asp:TextBox ID="TxtBrndJualShr" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="lblKDBrandJualSHR" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td width="50px"></td>
                        <td>
                            <asp:Button ID="btnSrchBrandJual" runat="server" Text="Search Brand Jual" OnClick="btnSrchBrandJual_Click" />
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                        </td>

                        <%-- <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    <tr>
                        <td>Alamat : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtAlamatShr" runat="server"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Phone : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneShr" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>PT : 
                        </td>
                        <td>
                            <asp:TextBox ID="TxtPTShr" runat="server" MaxLength="5" Text="SOS"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Luas : 
                        </td>
                        <td>
                            <asp:TextBox ID="TxtLuasSHR" runat="server"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Jumlah SPG : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtJmlSpgSHR" runat="server" ></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Salary : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalarySHR" runat="server"  class="numericOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Internet : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtInternetSHR" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Listrik : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtListrikSHR" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Telepon : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtTeleponSHR" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Sewa : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtSewaSHR" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Susut : 
                        </td>
                        <td>
                            <asp:TextBox ID="TxtSusutSHR" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Biaya Lain : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtBiayalainSHR" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Service : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtServiceSHR" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Logo Img : 
                        </td>
                        <td>
                            <asp:TextBox ID="TxtLogoImg" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Status : 
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatOpenClose" runat="server" DataTextField="Nama" DataValueField="Kode" ></asp:DropDownList>
                            <%--<asp:TextBox ID="txtStatSHR" runat="server" MaxLength="15"></asp:TextBox>--%>
                        </td>
                        <td width="50px"></td>
                        <td>Status Showroom : 
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlstatusShowroom" runat="server" DataTextField="Nama" DataValueField="Kode" ></asp:DropDownList>
                            <%--<asp:TextBox ID="txtStatusShowroomSHR" runat="server" Visible="false"></asp:TextBox>--%>
                        </td>
                        <td width="50px"></td>
                        <td style="display:none">Status Awal : 
                        </td>
                        <td style="display:none">
                            <asp:TextBox ID="txtStatusAwalSHR" runat="server" ReadOnly="true" Text="N"></asp:TextBox>
                        </td>
                    </tr>
                  <tr>
                      <td colspan="7">
                          <asp:CheckBox ID="chkDiscKaryawan" runat="server" Text="Disc Karyawan" Visible="true"/>
                          &nbsp
                           <asp:CheckBox ID="chkDiscRelasi" runat="server" Text="Disc Relasi" Visible="true"/>
                          &nbsp
                           <asp:CheckBox ID="chkDiscVIPMember" runat="server" Text="Disc VIP Member" Visible="true"/>
                          &nbsp
                          </td>
                  </tr>
                <tr>
                    <td colspan="7">
                        <asp:Button ID="btnaddSHR" runat="server" Text="Add" OnClick="btnaddSHR_Click" />
                        &nbsp
                        <asp:Button ID="btnCloseAddShr" runat="server" Text="Cancel" OnClick="btnCloseAddShr_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Button ID="Button2" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnPanelPU"
            Drag="true" PopupControlID="PanelBrandJualSHR" CancelControlID="bPUClose" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelBrandJualSHR" runat="server" BackColor="White" CssClass="ModalWindow"
            BorderStyle="Ridge" BorderColor="BlanchedAlmond"
            Style="display: block; top: 684px; left: 39px; width: auto;">
            <br />
            <asp:TextBox ID="txtbrandjualSHRsearch" runat="server" Width="100"></asp:TextBox>&nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Text="By Brand" Value="BRAND"></asp:ListItem>
                <asp:ListItem Text="By Consignment" Value="Consignment"></asp:ListItem>
                <asp:ListItem Text="By Super Brand" Value="SUPER_BRAND"></asp:ListItem>
            </asp:DropDownList>&nbsp;
    <asp:Button ID="btnbrandjualSHRsearch" runat="server" Text="Search Brand" OnClick="btnbrandjualSHRsearch_Click" />
            <div id="Div3" runat="server" visible="true" class="EU_TableScroll" style="display: block">

                <asp:GridView ID="GVBrandJual" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Visible="true"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="FBRAND"
                    PageSize="10" OnRowCommand="GVBrandJual_RowCommand" OnPageIndexChanging="GVBrandJual_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgSelectbrand" runat="server" CausesValidation="False" CommandName="SelectBrand"
                                        ImageUrl="~/Image/b_ok.png" Text="Select" />&nbsp;
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="FBRAND" HeaderText="BRAND" SortExpression="FBRAND" Visible="true" />
                        <asp:BoundField DataField="FKD_BRN" HeaderText="Kode Brand" />
                        <asp:BoundField DataField="Consignment" HeaderText="Consignment" />--%>
                        <asp:BoundField DataField="SUPER_BRAND" HeaderText="SUPER_BRAND" />

                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="Label3" runat="server" Text="Brand : "> </asp:Label>
            <asp:Label ID="lblBrndJualSHR" runat="server"> </asp:Label>
            <asp:Label ID="lblKDBrndJualSHR" runat="server" Visible="false"> </asp:Label>
            <asp:Button ID="btnSaveBrndJualSHR" runat="server" Text="Select" OnClick="btnSaveBrndJualSHR_Click" />&nbsp
            <asp:Button ID="btnCancelBrndJualSHR" runat="server" Text="Cancel Selection" OnClick="btnCancelBrndJualSHR_Click" />
            &nbsp
            <asp:Button ID="btnCloseBrndJualSHR" runat="server" Text="Close" OnClick="btnCloseBrndJualSHR_Click" />
            <br />
        </asp:Panel>

        <%-- Popup Edit Showroom --%>
        <asp:Button ID="btnPanelPU" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender0" runat="server" TargetControlID="btnPanelPU"
            Drag="true" PopupControlID="PanelPU" CancelControlID="bPUClose" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelPU" runat="server" BackColor="White" CssClass="ModalWindow"
            BorderStyle="Ridge" BorderColor="BlanchedAlmond"
            Style="display: block; top: 684px; left: 39px; width: 80%;">
            <div id="dShowRoomData" runat="server" >
                <table>
                    <tr>
                        <td>Showroom : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtshowroom" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:Label ID="lblIDShowroom" runat="server" Visible="false"></asp:Label>
                            <%--<asp:Label ID="lblShowroom" runat="server"></asp:Label>
                    <asp:Label ID="lblShowroomkode" runat="server" Visible="false"></asp:Label>--%>
                        </td>
                        <td width="50px"></td>
                        <td>Kode Showroom : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtKdShowroom" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td style="display:none">Store : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtstore" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                            <%--<asp:Label ID="lblBrand" runat="server"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>Brand : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtBrand" runat="server" MaxLength="20" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Brand Jual : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtBrandjual" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>
                            <asp:Button ID="btnSrchBrand" runat="server" Text="Search Brand Jual" OnClick="btnSrchBrand_Click1"/>
                        </td>
                        <td>
                            <asp:Label ID="lblbrandjualkd" runat="server" Visible="false"></asp:Label>
                        </td>

                        <%-- <td>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>--%>
                    </tr>
                    <tr>
                        <td>Alamat : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtAlamat" runat="server"></asp:TextBox>
                            <%--<asp:Label ID="lblAlamat" runat="server"></asp:Label>--%>
                        </td>
                        <td width="50px"></td>
                        <td>Phone : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="20"></asp:TextBox>
                            <%--<asp:Label ID="lblPhone" runat="server"></asp:Label>--%>
                        </td>
                        <td width="50px"></td>
                        <td>PT : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtPT" runat="server" MaxLength="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Luas : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtluas" runat="server"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Jumlah SPG : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtJmSpg" runat="server"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Salary : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalary" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Internet : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtinternet" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Listrik : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtlistrik" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Telepon : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtbiayatelp" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Sewa : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtsewa" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Susut : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtsusut" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                        <td width="50px"></td>
                        <td>Biaya Lain : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtbiayalain" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Service : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtservice" runat="server" class="numericOnly"></asp:TextBox>
                        </td>
                         <td width="50px"></td>
                        <td>Logo Img : 
                        </td>
                        <td>
                            <asp:TextBox ID="txtLogoImgUpd" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Status : 
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatOpenCloseEdit" runat="server" DataTextField="Nama" DataValueField="Kode" ></asp:DropDownList>
                            <%--<asp:TextBox ID="txtstatus" runat="server" MaxLength="15"></asp:TextBox>--%>
                        </td>
                        <td width="50px"></td>
                        <td>Status Showroom : 
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatusShr" runat="server" DataTextField="Nama" DataValueField="Kode" Enabled="false"></asp:DropDownList>
                            <%--<asp:TextBox ID="txtStatusShr" runat="server" MaxLength="15"></asp:TextBox>--%>
                        </td>
                        <td width="50px"></td>
                        <td style="display:none">Status Awal : 
                        </td>
                        <td style="display:none">
                            <asp:TextBox ID="txtStatusAwal" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>

                </table>
            </div>
            <div id="dsave" runat="server" visible="true">

                <asp:Button ID="btnupd" runat="server" Text="Update" OnClick="btnupd_Click" /> &nbsp
                <asp:Button ID="btnCancelclose" runat="server" Text="Cancel" OnClick="btnCancelclose_Click" />
            </div>
        </asp:Panel>

        <asp:Button ID="bBrandClose" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnPanelPU"
            Drag="true" PopupControlID="PanelBrand" CancelControlID="bPUClose" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelBrand" runat="server" BackColor="White" CssClass="ModalWindow"
            BorderStyle="Ridge" BorderColor="BlanchedAlmond"
            Style="display: block; top: 684px; left: 39px; width: auto;">
            <br />
            <asp:TextBox ID="txtBrandsrch" runat="server" Width="100"></asp:TextBox>&nbsp;
             <asp:DropDownList ID="DropDownList3" runat="server">
              <%--  <asp:ListItem Text="By Brand" Value="BRAND"></asp:ListItem>
                <asp:ListItem Text="By Consignment" Value="Consignment"></asp:ListItem>--%>
                <asp:ListItem Text="By Super Brand" Value="SUPER_BRAND"></asp:ListItem>
            </asp:DropDownList>&nbsp;
    <asp:Button ID="btnSearchBrand" runat="server" Text="Search Brand" OnClick="btnSearchBrand_Click" />
            <div id="Div1" runat="server" visible="true" class="EU_TableScroll" style="display: block">

                <asp:GridView ID="GVBrand" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Visible="true"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="FBRAND"
                    PageSize="10" OnRowCommand="GVBrand_RowCommand" OnPageIndexChanging="GVBrand_PageIndexChanging" OnRowDataBound="GVBrand_RowDataBound" OnSelectedIndexChanged="GVBrand_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgSelectbrand" runat="server" CausesValidation="False" CommandName="SelectBrand"
                                        ImageUrl="~/Image/b_ok.png" Text="Select" />&nbsp;
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
<%--                        <asp:BoundField DataField="FBRAND" HeaderText="BRAND" SortExpression="FBRAND" Visible="true" />
                        <asp:BoundField DataField="FKD_BRN" HeaderText="Kode Brand" />
                        <asp:BoundField DataField="Consignment" HeaderText="Consignment" />--%>
                        <asp:BoundField DataField="SUPER_BRAND" HeaderText="SUPER_BRAND" />

                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="Label1" runat="server" Text="Brand : "> </asp:Label>
            <asp:Label ID="lblselectedBrand" runat="server"> </asp:Label>
            <asp:Label ID="lblselectedKDBrand" runat="server" Visible="false"> </asp:Label>
            <asp:Button ID="btnsaveselected" runat="server" Text="Select" OnClick="btnsaveselected_Click" />&nbsp
            <asp:Button ID="btnCancel" runat="server" Text="Cancel Selection" OnClick="btnCancel_Click" />
            &nbsp
            <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" />
            <br />
        </asp:Panel>
    </div>

    <div id="dBrand" runat="server" visible="false">
          <div id="dBrandMSG" runat="server" visible="false"></div>

        <table>
            <tr>
                <td>Upload :
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;       
                </td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload Brand" OnClick="btnUpload_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                </td>
            </tr>

            <tr>
                <td colspan="2">
                     <asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/UPLOAD_BRAND.xlsx">
                <asp:Label ID="lbDownload" runat="server" Text="Download Format Upload Brand"></asp:Label><br />
            </asp:HyperLink>
                </td>
                <td>
                    <asp:Button ID="btnAddBrandManual" runat="server" Text="Add New Brand" OnClick="btnAddBrandManual_Click"/>
                </td>
            </tr>

        </table>
        <br />
        <asp:TextBox ID="txtSearchBrnd" runat="server" Width="100"></asp:TextBox>&nbsp;
         <asp:DropDownList ID="DropDownList2" runat="server">
                <asp:ListItem Text="By Brand" Value="FBRAND"></asp:ListItem>
                <asp:ListItem Text="By Consignment" Value="Consignment"></asp:ListItem>
                <asp:ListItem Text="By Super Brand" Value="SUPER_BRAND"></asp:ListItem>
            </asp:DropDownList>&nbsp;
    <asp:Button ID="btnSrcBrand" runat="server" Text="Search Brand" OnClick="btnSrcBrand_Click" />
        <div id="Div2" runat="server" class="EU_TableScroll" style="display: block">

            <asp:GridView ID="GvBrnd" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Visible="true"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="FKD_BRN"
                PageSize="10" OnRowCommand="GvBrnd_RowCommand" OnPageIndexChanging="GvBrnd_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                        <ItemTemplate>
                            <div>
                                <asp:ImageButton ID="imgSelectbrand" runat="server" CausesValidation="False" CommandName="EditBrand"
                                    ImageUrl="~/Image/b_edit.png" Text="Edit Brand" />&nbsp;
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FBRAND" HeaderText="BRAND" SortExpression="FBRAND" Visible="true" />
                    <asp:BoundField DataField="FKD_BRN" HeaderText="Kode Brand" />
                    <asp:BoundField DataField="Consignment" HeaderText="Consignment" />
                    <asp:BoundField DataField="SUPER_BRAND" HeaderText="Super Brand" />
                    <asp:BoundField DataField="NOMOR" HeaderText="Nomor Brand" />
                </Columns>
            </asp:GridView>
        </div>

        <asp:Button ID="btnditBrnd" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="PopUpEditBrnd" runat="server" TargetControlID="btnditBrnd"
            Drag="true" PopupControlID="PanelEditBrnd" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="PanelEditBrnd" runat="server" BackColor="White" CssClass="ModalWindow"
            BorderStyle="Ridge" BorderColor="BlanchedAlmond"
            Style="display: block; top: 684px; left: 39px; width: 80%;">
            <br />
            <asp:HiddenField ID="hfAddEditBrand" runat="server" />

            <table>
                <tr>
                    <td>Brand :
                    </td>
                    <td>
                        <asp:TextBox ID="txtFbrand" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>No Brand :
                    </td>
                    <td>
                        <asp:TextBox ID="txtFNoBrand" runat="server" ReadOnly="true"  ></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>Kode Brand :
                    </td>
                    <td>
                        <asp:TextBox ID="txtFKdBrand" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Consignment :
                    </td>
                    <td>
                        <asp:TextBox ID="txtConsignment" runat="server"></asp:TextBox>
                    </td>
                    <td width="50px"></td>
                    <td>Super Brand :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSuperBrand" runat="server" DataTextField="Nama" DataValueField="Kode"></asp:DropDownList>
                        <%--<asp:TextBox ID="TxtSuperBrand" runat="server"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnUpdBrnd" runat="server" Text="Update" OnClick="btnUpdBrnd_Click" />
                        <asp:Button ID="btnAddBrand" runat="server" Text="Add" OnClick="btnAddBrand_Click" visible="false"/>

                        &nbsp
                        <asp:Button ID="btnClosePopUpEditBrnd" runat="server" Text="Cancel" OnClick="btnClosePopUpEditBrnd_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
