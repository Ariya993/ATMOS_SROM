<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSearchShowroomForSalesPage.aspx.cs" Inherits="ATMOS_SROM.TestDummy.TestSearchShowroomForSalesPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Search Showroom :
                    </td>
                    <td>
                        <asp:TextBox ID="tbSearchShowRoom" runat="server"></asp:TextBox>
                        &nbsp
                        <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" Text="Search"/>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStore" runat="server" AppendDataBoundItems="false" DataTextField="SHOWROOM" DataValueField="KODE" Enabled="false">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:CheckBox ID="cbStoreChange" runat="server" ToolTip="Centang agar Store tidak diganti" Text="Don't Change Store" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="dGrid" runat="server" visible="false">
            <asp:GridView ID="gvShowroom" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%"
                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AllowPaging="true" DataKeyNames="SHOWROOM"
                PageSize="10" OnRowCommand="gvShowroom_RowCommand" OnPageIndexChanging="gvShowroom_PageIndexChanging" OnRowDataBound="gvShowroom_RowDataBound" OnSelectedIndexChanged="gvShowroom_SelectedIndexChanged">
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
                    <asp:BoundField DataField="SHOWROOM" HeaderText="Showroom" SortExpression="SHOWROOM" Visible="true" />
                    <asp:BoundField DataField="KODE" HeaderText="kode" Visible="true" />
                    <asp:BoundField DataField="STORE" HeaderText="Store" />
                    <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                    <asp:BoundField DataField="PHONE" HeaderText="phone" />
                    <asp:BoundField DataField="ALAMAT" HeaderText="Alamat" />
                    

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
