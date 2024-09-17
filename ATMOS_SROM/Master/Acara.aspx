<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Acara.aspx.cs" Inherits="ATMOS_SROM.Master.Acara" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.10.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $body = $("body");

        $(document).on({
            ajaxStart: function () { $body.addClass("loading"); },
            ajaxStop: function () { $body.removeClass("loading"); }
        });
    </script>
    <script type="text/javascript">
        function changeTextBox() {
            var ddlPUGroup = document.getElementById("<%= ddlPUGroup.ClientID %>");
            var tbPUGroupDesc = document.getElementById("<%= tbPUGroupDesc.ClientID %>");
            var ddlPUNamaGroup = document.getElementById("<%= ddlPUNamaGroup.ClientID %>");

            var value = ddlPUGroup.options[ddlPUGroup.selectedIndex].value;

            ddlPUNamaGroup.options[ddlPUGroup.selectedIndex].selected = true;

            var desc = ddlPUNamaGroup.options[ddlPUNamaGroup.selectedIndex].text;
            tbPUGroupDesc.textContent = desc;

            var tbMinPurch = document.getElementById("<%= tbMinPurch.ClientID %>"); GridVGridView1iew1
            var tbPUDisc = document.getElementById("<%= tbPUDisc.ClientID %>");
            var rblPUDisc = document.getElementById("<%= rblPUDisc.ClientID %>");
            if (ddlPUGroup.options[ddlPUGroup.selectedIndex].text == "AS023" || ddlPUGroup.options[ddlPUGroup.selectedIndex].text == "AS025" || ddlPUGroup.options[ddlPUGroup.selectedIndex].text == "AS026") {
                tbMinPurch.disabled = false;
                tbPUDisc.disabled = false;
            }
            else if (ddlPUGroup.options[ddlPUGroup.selectedIndex].text == "AS024") {
                tbMinPurch.disabled = false;
                tbPUDisc.disabled = false;
                //tbPUDisc.textContent = "0";
            }
            else {
                tbMinPurch.disabled = true;
                tbPUDisc.disabled = false;
            }
        }

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        var spinnerVisible = false;
        function showProgress() {
            if (!spinnerVisible) {
                $("div#spinner").fadeIn("fast");
                spinnerVisible = true;
            }
        };
        function hideProgress() {
            if (spinnerVisible) {
                var spinner = $("div#spinner");
                spinner.stop();
                spinner.fadeOut("fast");
                spinnerVisible = false;
            }
        };
    </script>
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
        .hidden {
            display: none;
        }

        .modal {
            display: none;
            position: fixed;
            z-index: 1000;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            background: rgba( 255, 255, 255, .8 ) url('http://i.stack.imgur.com/FhHRx.gif') 50% 50% no-repeat;
        }

        body.loading {
            overflow: hidden;
        }

            body.loading .modal {
                display: block;
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

    <asp:UpdatePanel ID="panelMain" runat="server">
        <ContentTemplate>
            <div id="divAwal" runat="server">
                <h2>Article Master</h2>
                <div id="DivMessage" runat="server" visible="false">
                </div>
                <div id="divUploadPromo" runat="server" visible="false">
                    Upload Promo Harga<br />
                    <asp:FileUpload ID="FileUploadPromo" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />
                    &nbsp; 
            <asp:HyperLink ID="HyperLinkDownloadPromo" runat="server" Target="_blank" NavigateUrl="~/Upload/Format Upload Promo.xlsx">
                <asp:Label ID="lbFormatUploadPromo" runat="server" Text="Download Format Excel Promo"></asp:Label>
            </asp:HyperLink>
                    <br />
                    <asp:Button ID="btnUploadPromo" runat="server" Text="Upload" />
                </div>

                <asp:Button ID="btnNewAcara" runat="server" Text="Create New Acara" OnClick="btnNewAcaraClick"
                    UseSubmitBehavior="false" OnClientClick="" />
                &nbsp;
        

        <div id="divMain" runat="server" visible="true">
            <hr />
            <asp:TextBox ID="tbSearch" runat="server" Width="251px"></asp:TextBox>&nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By Kode Acara" Value="ACARA_VALUE"></asp:ListItem>
                <asp:ListItem Text="By Nama Acara" Value="NAMA_ACARA"></asp:ListItem>
                <asp:ListItem Text="By Group Acara" Value="ACARA_STATUS"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick" /><br />
            <br />
            <div class="EU_TableScroll" style="display: block">
                <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                    OnRowCommand="gvMainRowCommand" PageSize="10" OnPageIndexChanging="gvMainPageChanging">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                        ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
                                <asp:ImageButton ID="imgDel" runat="server" CausesValidation="False" CommandName="DeleteRow"
                                    ImageUrl="~/Image/b_drop.png" Text="Delete" OnClientClick="return notifdelete();" />
                                                              &nbsp;
                                  <asp:ImageButton ID="imgDuplicate" runat="server" CausesValidation="False" Height="20" Width="20" CommandName="Duplicate"
                                    ImageUrl="~/Image/b_Duplicate.png" Text="Duplicate" />
  </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                        <asp:BoundField DataField="ID_ACARA" HeaderText="idAcara" />
                        <asp:BoundField DataField="ID_ACARA_STATUS" Visible="false" />
                        <asp:BoundField DataField="NO_URUT" HeaderText="NO_URUT" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ACARA_VALUE" HeaderText="KODE ACARA" />
                        <asp:BoundField DataField="ACARA_STATUS" HeaderText="GROUP ACARA" />
                        <asp:BoundField DataField="DISC" HeaderText="DISC" />
                        <asp:BoundField DataField="NAMA_ACARA" HeaderText="NAMA ACARA" />
                        <asp:BoundField DataField="ACARA_DESC" HeaderText="DESC" />
                        <asp:BoundField DataField="START_DATE" HeaderText="START DATE" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="END_DATE" HeaderText="END DATE" DataFormatString="{0:dd-MM-yyyy}" />

                        <asp:BoundField DataField="STATUS_ACARA" HeaderText="STATUS" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ARTICLE" HeaderText="ARTICLE" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="KODE" HeaderText="KODE" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="SHOWROOM" HeaderText="SHOWROOM" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="DESC_DISC" HeaderText="DESC DISC" />
                        <asp:BoundField DataField="SPCL_PRICE" HeaderText="SPCL_PRICE" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
            </div>

            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalUpload" runat="server" TargetControlID="btnShowPopup"
                Drag="true" PopupControlID="PanelPopUp" CancelControlID="bClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelPopUp" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: block; top: 684px; left: 39px; width: 755px;">
                <br />
                <asp:HiddenField ID="HfIdToDuplicate" runat="server" />
                <asp:HiddenField ID="hdnId" runat="server" />
                <%--<h2>Create New Event</h2>--%>
                <h2><asp:Label ID="lblJdlPopUp" runat="server"></asp:Label></h2>
                <div id="DivUploadMessage" runat="server" visible="false">
                </div>

                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td>
                            <h2>
                                <asp:Label runat="server" ID="lblTitleSubPage">New Event</asp:Label></h2>
                            <hr />
                        </td>
                        <td align="right">
                            <asp:Button ID="bClose" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <b>Showroom :</b>
                        </td>
                        <td>
                            <asp:Button ID="btnPUShowroom" runat="server" Text="Pilih Showroom" OnClick="btnPUShowroomClick" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="tbPUShowroom" runat="server" Width="300px" Height="50px" TextMode="MultiLine" ReadOnly="true" Text="" />
                            <asp:TextBox ID="tbPUIDShow" runat="server" Width="300px" Height="50px" TextMode="MultiLine" ReadOnly="true" Text="" Style="display: none" />
                            <asp:TextBox ID="tbPUShow" runat="server" Width="300px" Height="50px" TextMode="MultiLine" ReadOnly="true" Text="" Style="display: none" />
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <b>Group Acara : </b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPUGroup" runat="server" DataTextField="ACARA_STATUS" DataValueField="ID" onchange="changeTextBox();" ><%--OnSelectedIndexChanged="ddlPUGroup_SelectedIndexChanged" AutoPostBack="true">--%>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPUNamaGroup" runat="server" DataValueField="ID" DataTextField="DESC_DISC" onchange="changeTextBox();" Style="display: none">
                            </asp:DropDownList>
                            &nbsp
                            <asp:Button ID="btnSrchGrpAcara" runat="server" Text="Search" OnClick="btnSrchGrpAcara_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="tbPUGroupDesc" runat="server" Width="300px" Height="50px" TextMode="MultiLine" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>No Surat Acara : </b>
                            <%--<b>Nama Acara : </b>--%>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPUNamaAcara" runat="server" Width="300px" MaxLength="75" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Kode Acara : </b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPUAcaraValue" runat="server" MaxLength="15" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Description :</b></td>
                        <td>
                            <asp:TextBox ID="tbPUDesc" runat="server" Width="300px" Height="50px" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Discount / Special Price / Potongan Harga: </b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPUDisc" runat="server" MaxLength="17" text="0"/>
                            <%--<asp:FilteredTextBoxExtender ID="FilterTbPUDisc" runat="server" TargetControlID="tbPUDisc" FilterType="Numbers" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Minimum Purchase : </b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbMinPurch" runat="server" MaxLength="7" Text="0" Enabled="false"/> 
                            <%--<asp:FilteredTextBoxExtender ID="FilterTbPUDisc" runat="server" TargetControlID="tbPUDisc" FilterType="Numbers" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:RadioButtonList ID="rblPUDisc" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Discount" Value="D" Selected="True" />
                                <asp:ListItem Text="Special Price / Potongan Harga" Value="P" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Waktu Acara : </b></td>
                        <td>
                            <asp:TextBox ID="tbPUStartAcara" runat="server" />
                            <asp:CalendarExtender ID="CalenderExtenderStartDate" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                TargetControlID="tbPUStartAcara" DefaultView="Days">
                            </asp:CalendarExtender>
                            &nbsp;s/d&nbsp;
                    <asp:TextBox ID="tbPUEndAcara" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalenderExtenderEndDate" runat="server" Enabled="true" Format="dd-MM-yyyy"
                                TargetControlID="tbPUEndAcara" DefaultView="Days">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td></td>
                        <td>
                            <b>File :</b></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <b>Item :</b>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblPUAllIem" runat="server">
                                <%--<asp:ListItem Text="All Item" Value="ALL" />--%>
                                <asp:ListItem Text="All Item Normal" Value="N" />
                                <asp:ListItem Text="Selected Item" Value="S" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2" align="left">
                            <div id="divUpload" runat="server">
                                <asp:Button ID="btnUpload" runat="server" Text="Save" OnClick="btnUpload_Click" />
                                <asp:Button ID="btnDuplicate" runat="server" Text="Duplicate" OnClick="btnDuplicate_Click" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Button ID="btnShowViewItem" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalViewItem" runat="server" TargetControlID="btnShowViewItem"
                Drag="true" PopupControlID="PanelVI" CancelControlID="bVIClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelVI" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" class="EU_TableScroll"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <div id="dmsgUpdItem" runat="server" visible="false"></div>
                <div class="EU_TableScroll" style="display: block;max-height: 700px; overflow: auto;"">
                <asp:HiddenField ID="hdnIDVI" runat="server" />
                <asp:HiddenField ID="hdnVINoPO" runat="server" />
                 <asp:HiddenField ID="HfKodeAcara" runat ="server" />
                 <table width="100%">
                    <tr>
                        <td colspan="2">
                            <h2><b>Detail Item Acara</b></h2>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17%">
                            <b>ID Acara :</b> &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="tbVIIdAcara" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17%">
                            <b>Kode Acara :</b> &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="tbVIKodeAcara" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Disc / Special Price :</b> &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="tbVIDisc" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Status Article :</b> &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="cbVIStatusArticle" runat="server" Enabled="false" Text="All Article" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Upload Article :</b> &nbsp;
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;
                    <asp:Button ID="btnFileUpload" runat="server" Text="Upload Now" OnClick="btnVIUploadClick"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        </td>
                    </tr>
                     <tr id="trhide" runat="server" visible="true">
                        <td>
                            <b>Upload Article GWP:</b> &nbsp;
                        </td>
                        <td>
                            <asp:FileUpload ID="FlUp2" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;
                        <asp:Button ID="btnFileUploadGWP" runat="server" Text="Upload GWP Now" OnClick="btnFileUploadGWP_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        </td>
                    </tr>
                    <tr>
                        <td>Format Excel
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/Format_Upload_Acara.xlsx">
                                <asp:Label ID="lbFormatUpload" runat="server" Text="Download Format Excel"></asp:Label>
                            </asp:HyperLink>
                            &nbsp
                            <asp:HyperLink ID="HyperLinkDownloadGWP" runat="server" Target="_blank" NavigateUrl="~/Upload/Format_Upload_Item_GWP_PWP.xlsx">
                                <asp:Label ID="lbFormatUploadGWP" runat="server" Text="Download Format Excel GWP"></asp:Label>
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnDiscShowroom" runat="server" Text="Add Showroom" OnClick="btnDiscShowroom_Click" /></td>
                    </tr>
                    <tr>
                        <td>
                            Showroom : 
                        </td>
                        <td>
                            <asp:Label ID="lblacaraval" runat="server" Visible="false"></asp:Label>
                             <asp:TextBox ID="txtShrBerjalan" runat="server" Width="300px" Height="50px" TextMode="MultiLine" ReadOnly="true" Text=""></asp:TextBox>
                            <asp:TextBox ID="txtIDShrBerjalan" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Search</b></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="3">
                            <asp:Button ID="bVIClose" runat="server" Text="Close" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <h2>ITEM ACARA</h2>
                    <br />
                    <table>
                        <tr>
                        <td colspan="2">
                            <asp:TextBox ID="tbVISearch" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="ddlVISearch" runat="server">
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                        <asp:ListItem Text="By Barcode" Value="BARCODE"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="btnVISearch" runat="server" Text="Search" OnClick="btnVISearchClick" />
                        </td>
                            </tr>
                    </table>
                     <br />
                    <asp:GridView ID="gvVI" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" 
                        DataKeyNames="ID" OnPageIndexChanging="gvVI_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
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
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="ITEM_DESC" HeaderText="Description" />
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div class="EU_TableScroll" style="display: block" id="divItemGWP" runat="server">
                    <h2>ITEM GWP /PWP</h2>
                    <br />
                    <asp:GridView ID="Gv_GWP" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%" 
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true"
                         DataKeyNames="ID" OnPageIndexChanging="Gv_GWP_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
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
                            <asp:BoundField DataField="BARCODE" HeaderText="Barcode" />
                            <asp:BoundField DataField="ITEM_DESC" HeaderText="Description" />
                            <asp:BoundField DataField="ITEM_PRICE_ACARA" HeaderText="PRICE"  Visible="false"/>
                        </Columns>
                    </asp:GridView>
                </div>
                    </div>
            </asp:Panel>

            <asp:Button ID="btnShowViewShowroom" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalViewShowroom" runat="server" TargetControlID="btnShowViewShowroom"
                Drag="true" PopupControlID="PanelViewShowroom" CancelControlID="bVSClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelViewShowroom" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <h2>Showroom</h2>
                <table width="100%">
                    <tr>
                        <td style="display: block">
                            <asp:Button ID="btnVSCheckAll" runat="server" Text="All Showroom" OnClick="btnVSCheckAllClick" />
                            <asp:TextBox ID="tbVSSearch" runat="server" Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlVSSearch" runat="server" Visible="false">
                                <asp:ListItem Text="By Kode" Value="KODE"></asp:ListItem>
                                <asp:ListItem Text="By Showroom" Value="SHOWROOM"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnVSSearch" runat="server" Text="Search" Visible="false" OnClick="btnPUShowroomClick" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bVSClose" runat="server" Text="Close" Style="display: none" />
                            <asp:Button ID="bVSCloseShow" runat="server" Text="Close" OnClick="bVSCloseShowClick" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="gvVS" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="false" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:CheckBox ID="cbShowroom" runat="server" Text="" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="KODE" HeaderText="Kode" />
                            <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="STATUS_SHOWROOM" HeaderText="Status Showroom" />
                        </Columns>
                    </asp:GridView>
                  
                </div>
                  <asp:Button ID="btnVSSave" runat="server" Text="Save" OnClick="btnVSSaveClick" />
            </asp:Panel>

            <!--Pop Up Upload Now-->
            <asp:Button ID="btnModalUploadItem" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalUploadItem" runat="server" TargetControlID="btnModalUploadItem"
                Drag="true" PopupControlID="PanelUpdItem" CancelControlID="bUIClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelUpdItem" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnUISave"
                Style="display: none; top: 684px; left: 39px; width: 555px;">
                <br />
                <div id="divUIMessage" runat="server" visible="false"></div>
                <asp:HiddenField ID="hdnSource" runat="server" />
                <asp:HiddenField ID="hdnNamaSource" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td></td>
                        <td style="width: 10px;" colspan="3">
                            <h2><b>
                                <asp:Label runat="server" ID="lbUIJudul">Upload Data</asp:Label></b></h2>
                            <br />
                            <hr style="width: 62px" />
                        </td>
                        <td align="right">
                            <asp:Button ID="bUIClose" runat="server" Text="Cancel" Width="100px"
                                Style="height: 26px" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nama File :&nbsp;
                    <asp:Label ID="lbUINamaFile" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" align="left">
                            <asp:Button ID="btnUISave" runat="server" Text="Upload" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="blueHeader">&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                Drag="true" PopupControlID="Panel1" CancelControlID="Button4" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="HiddenField3" runat="server" />
                <asp:HiddenField ID="HiddenField4" runat="server" />
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <b>Detail Item Acara</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17%">
                            <b>ID Acara :</b> &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="tbDIAIdAcara" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 17%">
                            <b>Kode Acara :</b> &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Disc / Special Price :</b> &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Status Article :</b> &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Text="All Article" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Upload Article :</b> &nbsp;
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Upload Now" OnClick="btnVIUploadClick"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td><b>Search</b></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            &nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="By Item Code" Value="ITEM_CODE"></asp:ListItem>
                    </asp:DropDownList>
                            &nbsp;
                    <asp:Button ID="Button3" runat="server" Text="Search" OnClick="btnVISearchClick" />
                        </td>
                        <td align="right">
                            <asp:Button ID="Button4" runat="server" Text="Close" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
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
                            <asp:BoundField DataField="ITEM_DESC" HeaderText="Description" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

           <asp:Button ID="Button5" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button5"
                Drag="true" PopupControlID="Panel2" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel2" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <asp:HiddenField ID="HiddenField5" runat="server" />
                <asp:HiddenField ID="HiddenField6" runat="server" />
                <h2>Showroom</h2>
                <table width="100%">
                    <tr>
                        <td style="display: block">
                            <asp:Button ID="Button6" runat="server" Text="All Showroom" OnClick="btnVSCheckAllClick" Visible="false"/>
                            <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="DropDownList2" runat="server" Visible="false">
                                <asp:ListItem Text="By Kode" Value="KODE"></asp:ListItem>
                                <asp:ListItem Text="By Showroom" Value="SHOWROOM"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="Button7" runat="server" Text="Search" Visible="false" OnClick="btnPUShowroomClick" />
                        </td>
                        <td align="right">
                            <asp:Button ID="Button8" runat="server" Text="Close" Style="display: none" />
                            <asp:Button ID="Button9" runat="server" Text="Close" OnClick="Button9_Click" />
                        </td>
                    </tr>
                </table>
                <div class="EU_TableScroll" style="display: block">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="false" DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:CheckBox ID="cbShowroom" runat="server" Text="" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="id" SortExpression="id" Visible="false" />
                            <asp:BoundField DataField="KODE" HeaderText="Kode" />
                            <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" />
                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                            <asp:BoundField DataField="STATUS_SHOWROOM" HeaderText="Status Showroom" />
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnShowroomAddSave" runat="server" Text="Save" OnClick="btnShowroomAddSave_Click" />
                </div>
            </asp:Panel>

             <!--Pop Up Search Group Acara -->
            <asp:Button ID="btnModalSearchGroupAcara" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalSearchGroupAcara" runat="server" TargetControlID="btnModalSearchGroupAcara"
                Drag="true" PopupControlID="PSearchGroupAcara"  BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PSearchGroupAcara" runat="server" BackColor="White" CssClass="ModalWindow"
                BorderStyle="Ridge" BorderColor="BlanchedAlmond"
                Style="display: none; top: 684px; left: 39px; width: 80%;">
                <br />
                <div id="Div2" runat="server" visible="false"></div>
                <asp:HiddenField ID="HiddenField7" runat="server" />
                <table width="100%" cellspacing="4">
                    <tr>
                        <td colspan="1" align="right">Search Group Acara :</td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="tbSearchGrp" runat="server"></asp:TextBox>
                            &nbsp
                        <asp:Button ID="btnsearchGrp" runat="server" OnClick="btnsearchGrp_Click" Text="Search" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsearcGrpClose" runat="server" OnClick="btnsearcGrpClose_Click" Text="Close" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                          
                             <div class="EU_TableScroll" style="display: block">
                            <asp:GridView ID="gvGrp" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                                PageSize="10" OnRowCommand="gvGrp_RowCommand" OnPageIndexChanging="gvGrp_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                        <ItemTemplate>
                                            <div>
                                                <asp:ImageButton ID="imgSelect" runat="server" CausesValidation="False" CommandName="SelectRow"
                                                    ImageUrl="~/Image/b_ok.png" Text="Select" />&nbsp;
                                
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false" />
                                    <asp:BoundField DataField="ACARA_STATUS" HeaderText="Group Acara"/>
                                    <asp:BoundField DataField="DESC_DISC" HeaderText="Description" />
                                </Columns>
                            </asp:GridView>
                                 </div>
                        </td>
                    </tr>
                    </table>
                    <%--</div>--%>
            </asp:Panel>
            <div class="modal">
                <!-- Place at bottom of page -->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileUpload" />
            <asp:PostBackTrigger ControlID="btnFileUploadGWP" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
