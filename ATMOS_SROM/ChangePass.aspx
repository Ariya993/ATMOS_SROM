<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="ATMOS_SROM.ChangePass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Change Password
    </h2>    
    <div id="DivMessage" runat="server" visible="false">
    </div>
    <p>
        Use the form below to change your password.
    </p>
    <asp:Panel ID="panelChange" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="2">
                        <b>Change Password</b>   
                    </td>
                </tr>
                <tr>
                    <td>
                        Username :
                    </td>
                    <td>
                        <asp:TextBox ID="tbUserName" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Old Password :
                    </td>
                    <td>
                        <asp:TextBox ID="tbOldPass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        New Password :
                    </td>
                    <td>
                        <asp:TextBox ID="tbNewPass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm Password :
                    </td>
                    <td>
                        <asp:TextBox ID="tbConfirmPass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSaveClick" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
