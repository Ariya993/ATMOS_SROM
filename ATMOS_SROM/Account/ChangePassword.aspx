<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="ATMOS_SROM.Account.ChangePassword" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>


        Change Password2
    </h2>    
    <div id="DivMessage" runat="server" visible="false">
    </div>
    <p>
        Use the form below to change your password.
    </p>
    <p>
        New passwords are required to be a minimum of 8 characters in length.
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
