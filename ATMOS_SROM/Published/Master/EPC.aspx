<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EPC.aspx.cs" Inherits="ATMOS_SROM.Master.EPC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function changeTextBox() {
            var tbPUNIK = document.getElementById("<%= tbSearch.ClientID %>");
            tbPUNIK.textContent = "07-";
        }

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
    <asp:Panel ID="panelMain" runat="server">
    <div id="divAwal" runat="server">
        <h2>EPC</h2>   
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <div id="divMain" runat="server" visible="true">
            <hr />
            <asp:Button ID="btnAddEPC" runat="server" Text="ADD New EPC" OnClick="btnAddEPCClick" />
            <%-- Upload EPC (Terhubung DCARD) & VIP Member --%>
            &nbsp
            <asp:Button ID="btnUploadEPC" runat="server" Text="UPLOAD New EPC" OnClick="btnUploadEPC_Click" />
            &nbsp
            <asp:Button ID="btnAddVipMember" runat="server" Text="Add New VIP MEMBER" OnClick="btnAddVipMember_Click" Visible="false"/>
            &nbsp
            <asp:Button ID="btnUploadVIPMEMBER" runat="server" Text="UPLOAD New VIP MEMBER" OnClick="btnUploadVIPMEMBER_Click" Visible="true"/>
            <%-- End Upload EPC (Terhubung DCARD) & VIP Member --%>
            <br />
            <asp:TextBox ID="tbSearch" runat="server" Width="251px">
            </asp:TextBox>
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By NIK" Value="NIK"></asp:ListItem>
                <asp:ListItem Text="By Nama" Value="NAMA"></asp:ListItem>                
                <asp:ListItem Text="By Limit" Value="LIMIT"></asp:ListItem>              
                <asp:ListItem Text="By Jabatan" Value="JABATAN"></asp:ListItem>          
                <asp:ListItem Text="By Status EPC" Value="STATUS_EPC"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick ="btnSearchClick"/><br />
            <br />
            <div class="EU_TableScroll" style="display: block">
                <div id="divPromoShr" runat="server" visible="true">
                    <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="ID"
                     PageSize="10" OnPageIndexChanging="gvMainPageIndexChanging" OnRowCommand="gvMainRowCommand" >
                        <Columns>
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="EditRow"
                                            ImageUrl="~/Image/b_edit.png" Text="Edit" />&nbsp;
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
                            <asp:BoundField DataField="NIK" HeaderText="NIK"  /> 
                            <asp:BoundField DataField="NAMA" HeaderText="NAMA" /> 
                            <asp:BoundField DataField="JABATAN" HeaderText="JABATAN" />
                            <asp:BoundField DataField="JOIN_DATE" HeaderText="JOIN_DATE" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="LIMIT" HeaderText="LIMIT" />
                            <asp:BoundField DataField="LIMIT_DELAMI" HeaderText="LIMIT_DELAMI" />
                            <asp:BoundField DataField="STATUS_EMPLOYEE" HeaderText="STAT" />
                            <asp:BoundField DataField="NIK" HeaderText="Item Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" Visible="false" />
                            <asp:BoundField DataField="NIK" HeaderText="Brand" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" Visible="false" />
                            <asp:BoundField DataField="NIK" HeaderText="DESCRIPTION" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" Visible="false" />
                            <asp:BoundField DataField="NIK" HeaderText="Article" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" Visible="false" />
                            <asp:BoundField DataField="NIK" HeaderText="Color" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" Visible="false" />
                            <asp:BoundField DataField="STATUS_CARD" HeaderText="Size" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" Visible="false" />
                            <asp:BoundField DataField="TIPE" HeaderText="TIPE" />
                            <asp:BoundField DataField="STATUS_EPC" HeaderText="STATUS_EPC" />
                            <asp:BoundField DataField="SISA_LIMIT" HeaderText="SISA_LIMIT" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <asp:Button ID="btnGenerateEPC" runat="server" Text="Generate EPC" OnClick="btnGenerateEPCClick" />
    </div>
    </asp:Panel>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalAddEdit" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelPopUp" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUp" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnId" runat="server" />
        <div id="DivPopUpMessage" runat="server" visible="false">
        </div>
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 200px;" colspan="3">
                    <h2>
                        <asp:Label runat="server" ID="lbPUJudul" Text="Add New EPC"></asp:Label></h2>
                        
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    NIK</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUNIK" runat="server" Width="90%" ReadOnly="true" />
                    <asp:Label ID="lbPUNoUrut" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Nama</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUNama" runat="server" Width="90%"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Jabatan
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUJabatan" runat="server" Width="50%" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Join Date</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPUDateJoin" runat="server" ReadOnly="false" OnTextChanged="tbPUDateJoinChange" AutoPostBack="true" />
                    <asp:CalendarExtender ID="CalenderExtenderStartDate" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="tbPUDateJoin" DefaultView="Days">
                        </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Limit EPC
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbPULimitSOS" runat="server" Width="90%" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Limit Delami</td>
                <td colspan="2">
                    <asp:TextBox ID="tbPULimitDelami" runat="server" Width="90%" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Relasi</td>
                <td colspan="2">
                    <asp:CheckBox ID="cbPURelasi" runat="server" Text="Ada Relasi" />
                </td>
            </tr>
          <tr>
                <td>
                </td>
                <td>
                    </td>
                <td colspan="2">
                    <asp:CheckBox ID="ChkMRA" runat="server" Text="Diskon MRA " OnCheckedChanged="ChkMRA_CheckedChanged" AutoPostBack="true"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                     <div id="divUpload" runat="server">
                        <asp:Button ID="btnPUSave" runat="server" Text="Save" Width="100px" OnClick="btnPUSaveClick" />&nbsp;
                        <asp:Button ID="bClose" runat="server" Text="Cancel" Width="100px"/>
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
    <!-- Pop Up Upload VIP MEMBER -->
     <asp:Button ID="btnPopUpUploadVIPMember" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modalUploadVipMember" runat="server" TargetControlID="btnPopUpUploadVIPMember"
        Drag="true" PopupControlID="PanelPopUpUploadMember" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUpUploadMember" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <div id="dMsgVipMember" runat="server" visible="false">
        </div>
         <table>
            <tr>
                <td>Upload :
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;       
                </td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    &nbsp
                    <asp:Button ID ="btnupVipMemberClose" runat="server" OnClick="btnupVipMemberClose_Click" Text="Close"/>
                </td>
            </tr>
             <tr>
                 <td colspan="3">
                      <asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/FORMAT Upload_VIP_MEMBER.xlsx" Visible="true">
                        <asp:Label ID="lbDownload" runat="server" Text="Download Format Upload Excel" Visible="true"></asp:Label><br />
                    </asp:HyperLink>
                 </td>
             </tr>
             </table>
        </asp:Panel>

    <!-- Pop Up Add New VIP MEMBER -->
     <asp:Button ID="btnPopupAddVIP" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupAddVip" runat="server" TargetControlID="btnPopupAddVIP"
        Drag="true" PopupControlID="PanelPopupAddVip" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopupAddVip" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnVipId" runat="server" />
        <div id="Div1" runat="server" visible="false">
        </div>
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 200px;" colspan="3">
                    <h2>
                        <asp:Label runat="server" ID="Label1" Text="Add New SOS VIP MEMBER"></asp:Label></h2>
                        
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    NIK</td>
                <td colspan="2">
                    <asp:Label ID ="lblvipnoH" runat="server" Text="707-02-"></asp:Label>
                    <asp:TextBox ID="txtvipNo" runat="server" Width="80%" />
                    <asp:Label ID="Label2" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Nama</td>
                <td colspan="2">
                    <asp:TextBox ID="txtNamaVip" runat="server" Width="90%"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Jabatan
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtJabatanVIP" runat="server" Width="50%" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Join Date</td>
                <td colspan="2">
                    <asp:TextBox ID="txtjoindtvip" runat="server" ReadOnly="false" />
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" Format="dd-MM-yyyy"
                        TargetControlID="txtjoindtvip" DefaultView="Days">
                        </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Limit ATMOS</td>
                <td colspan="2">
                    <asp:TextBox ID="txtlimitsosVip" runat="server" Width="90%" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Limit Delami</td>
                <td colspan="2">
                    <asp:TextBox ID="txtlimitdelamivip" runat="server" Width="90%" text="0" Enabled="false"/>
                </td>
            </tr>
            <%--<tr>
                <td>
                </td>
                <td>
                    Relasi</td>
                <td colspan="2">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Ada Relasi" />
                </td>
            </tr>--%>
          
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                     <div id="div2" runat="server">
                        <asp:Button ID="btnVIPMemberSave" runat="server" Text="Save" Width="100px" OnClick="btnVIPMemberSave_Click" />&nbsp;
                        <asp:Button ID="Button3" runat="server" Text="Cancel" Width="100px"/>
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

        <!-- Pop Up Upload EPC -->
     <asp:Button ID="btnUpEPC" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modalUpEPC" runat="server" TargetControlID="btnUpEPC"
        Drag="true" PopupControlID="PanelPopUpUpEPC" CancelControlID="bClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelPopUpUpEPC" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <div id="dMsgUpEPC" runat="server" visible="false">
        </div>
         <table>
              <tr>
                 <td colspan="3">
                 <h1>Upload EPC</h1>    
                 </td>
                 </tr>
             <tr>
                 <td colspan="3">
                 <asp:HyperLink ID="HyperLink1" Text="Download EPC Template" runat="server" Target="_blank" NavigateUrl="~/Upload/DISCOUNT EPC.xlsx"></asp:HyperLink>
                 </td>
             </tr>
            <tr>
                <td>Upload :
                </td>
                <td>
                    <asp:FileUpload ID="FupEPC" runat="server" BorderColor="Black" BorderWidth="1px" Style="margin-bottom: 5px;" />&nbsp;       
                </td>
                <td>
                    <asp:Button ID="btnFupEPC" runat="server" Text="Upload" OnClick="btnFupEPC_Click"
                        UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    &nbsp
                    <asp:Button ID ="btnFupEPCClose" runat="server" OnClick="btnFupEPCClose_Click" Text="Close"/>
                    <%--<asp:HyperLink ID="HyperLinkDownload" runat="server" Target="_blank" NavigateUrl="~/Upload/format PO_upload.xlsx" Visible="false">
                        <asp:Label ID="lbDownload" runat="server" Text="Download Format Upload Excel" Visible="false"></asp:Label><br />
                    </asp:HyperLink>--%>
                </td>
            </tr>
             </table>
        </asp:Panel>

</asp:Content>
