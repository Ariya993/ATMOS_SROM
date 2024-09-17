<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="ATMOS_SROM.Master.User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="false"
        ScriptMode="Release">
    </asp:ToolkitScriptManager>
    <div id="divAwal" runat="server">
        <h2><b>Master User</b></h2>   
        <div id="DivMessage" runat="server" visible="false">
        </div>
        <div id="divMain" runat="server" visible="true">
            <hr />
            <asp:TextBox ID="tbSearch" runat="server" Width="251px"></asp:TextBox>&nbsp;
            <asp:DropDownList ID="ddlSearch" runat="server">
                <asp:ListItem Text="By Username" Value="userName"></asp:ListItem>
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearchClick"/><br />
            <asp:Button ID="btnMember" runat="server" Text="Create New User" OnClick="btnMemberClick" ToolTip="Membuat User Baru" />
            <br />
            <div class="EU_TableScroll" style="display: block">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="idUser"
                OnRowCommand="gvMainRowCommand" PageSize="10" OnPageIndexChanging="gvMainPageChanging">
                <Columns>
                    <asp:TemplateField ShowHeader="False" HeaderText="Action" Visible="false">
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
                    <asp:BoundField DataField="idUser" HeaderText="id" SortExpression="id" Visible="false" />
                    <asp:BoundField DataField="idStore" HeaderText="idStore" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" /> 
                    <asp:BoundField DataField="userName" HeaderText="Username" />
                    <asp:BoundField DataField="password" HeaderText="password" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="realName" HeaderText="Nama" />
                    <asp:BoundField DataField="kodeCust" HeaderText="Kode" />
                    <asp:BoundField DataField="store" HeaderText="Store" />
                    <asp:BoundField DataField="userLevel" HeaderText="Level" />                    
                    <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="appraisal" HeaderText="Appraisal" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" /> 
                    <asp:BoundField DataField="online" HeaderText="Online" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="lastLogin" HeaderText="Last Login" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="lastIP" HeaderText="Last IP" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="createdBy" HeaderText="Created BY" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="createdDate" HeaderText="Created Date" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="updatedBy" HeaderText="Update By" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="updatedDate" HeaderText="Update Date" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField DataField="status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
        </div>
    </div>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    
    <!--Pop Up Create Member Baru-->
    <asp:ModalPopupExtender ID="ModalCreateMember" runat="server" TargetControlID="btnShowPopup"
        Drag="true" PopupControlID="PanelCreate" CancelControlID="btnCClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelCreate" runat="server" BackColor="White" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond" DefaultButton="btnCSave"
        Style="display: block; top: 684px; left: 39px; width: 555px;">
        <br />
        <asp:HiddenField ID="hdnIdCreateMember" runat="server" />
        <div id="divCMessage" runat="server" visible="false" />
        <table width="100%" cellspacing="4">
            <tr>
                <td>
                </td>
                <td style="width: 10px;" colspan="2">
                    <h2>
                        <asp:Label runat="server" ID="lblTitleSubPage">Create Member</asp:Label></h2>
                    <hr />
                </td>
                <td align="right">
                    <asp:Button ID="btnCClose" runat="server" Text="Cancel" Width="100px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    No Bon</td>
                <td colspan="2">
                    <asp:TextBox ID="tbCNoBon" runat="server" Width="270px" MaxLength="75" />
                    <asp:RequiredFieldValidator ID="ReqCNoBon" runat="server" ControlToValidate="tbCNoBon" ForeColor="Red"
                        ErrorMessage="Please enter No Bon" ValidationGroup="Member_Input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    First Name</td>
                <td colspan="2">
                    <asp:TextBox ID="tbCFirstName" runat="server" Width="270px" MaxLength="75" />
                    <asp:RequiredFieldValidator ID="ReqCFirstName" runat="server" ControlToValidate="tbCFirstName" ForeColor="Red"
                        ErrorMessage="Please enter first name" ValidationGroup="Member_Input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Last Name</td>
                <td colspan="2">
                    <asp:TextBox ID="tbCLastName" runat="server" Width="270px" MaxLength="75"/>
                    <asp:RequiredFieldValidator ID="ReqCLastName" runat="server" ControlToValidate="tbCLastName" ForeColor="Red"
                        ErrorMessage="Please enter last name" ValidationGroup="Member_Input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Phone
                </td>
                <td colspan="2">
                    +
                    <asp:TextBox ID="tbCPhone1" runat="server" Width="40px" MaxLength="3" />
                    <asp:FilteredTextBoxExtender ID="FilteredtbCPhone1" runat="server" Enabled="true"
                        TargetControlID="tbCPhone1" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>&nbsp;

                    <asp:TextBox ID="tbCPhone" runat="server" Width="210px" MaxLength="22"/>
                    <asp:FilteredTextBoxExtender ID="FilteredtbCPhone" runat="server" Enabled="true"
                        TargetControlID="tbCPhone" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="ReqCPhone" runat="server" ControlToValidate="tbCPhone" ForeColor="Red"
                        ErrorMessage="Please enter phone" ValidationGroup="Member_Input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Email</td>
                <td colspan="2">
                    <asp:TextBox ID="tbCEmail" runat="server" Width="270px" MaxLength="50"/>
                    <asp:RequiredFieldValidator ID="ReqCEmail" runat="server" ControlToValidate="tbCEmail" ForeColor="Red"
                        ErrorMessage="Please enter email" ValidationGroup="Member_Input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Alamat</td>
                <td colspan="2">
                    <asp:TextBox ID="tbCAlamat" runat="server" Width="270px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqCAlamat" runat="server" ControlToValidate="tbCAlamat" ForeColor="Red"
                        ErrorMessage="Please enter alamat" ValidationGroup="Member_Input"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr runat="server" id="trBrand" visible="false">
                <td>
                </td>
                <td>
                    Brand</td>
                <td colspan="2">
                    <asp:TextBox ID="tbCBrand" runat="server"></asp:TextBox>
                </td>
            </tr>       
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                    <asp:Button ID="btnCSave" runat="server" Text="Save" OnClick="btnCSaveClick" ValidationGroup="Member_Input" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="blueHeader">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
