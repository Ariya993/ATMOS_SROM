<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Config.aspx.cs" Inherits="ATMOS_SROM.Configuration.Config" %>
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
    <asp:Panel ID="panelMain" runat="server">
        <div id="divUploadPromo" runat="server" style="border: 1px solid black; padding:2px 2px 2px 2px;">
            <b>Configuration</b><br /><br />
            <asp:Table ID="tblMain" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <b>Lock Movement</b>
                    </asp:TableCell>                    
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:TextBox ID="tbLock" runat="server" Enabled="false" />
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="tbLockDate" runat="server" />
                        <asp:CalendarExtender ID="calLockDate" runat="server" TargetControlID="tbLockDate" DefaultView="Months" 
                            Enabled="true" Format="dd-MM-yyyy" />
                    </asp:TableCell><asp:TableCell>
                        <asp:Button ID="btnLock" runat="server" Text="Lock Table" OnClick="btnLockClick" />
                    </asp:TableCell></asp:TableRow></asp:Table></div></asp:Panel></asp:Content>