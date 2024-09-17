<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="ATMOS_SROM.Configuration.Configuration" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="300"></asp:ScriptManager>
    <asp:Panel ID="panelMain" runat="server">
        <div id="DivMessage" runat="server" visible="false" />
        <div>
            <b>Configuration</b><br /><br />
            <table>
                <tr>
                    <td colspan="2">
                        <b>Lock Movement Store</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="tbLock" runat="server" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tbLockDate" runat="server" ></asp:TextBox>
                        <asp:CalendarExtender ID="CalendeExtenderLockDate" runat="server" Enabled="true" Format="yyMM"
                            TargetControlID="tbLockDate" DefaultView="Months">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>                    
                    <td colspan="2">
                        <asp:Button ID="btnLock" runat="server" Text="Lock Table" CommandName ="FSS" OnCommand="btnLockClick"
                            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        <asp:Button ID="btnUnLock" runat="server" Text="Unlock Table" CommandName="FSS" OnCommand="btnUnlockClick"
                            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <b>Lock Movement Dept Store</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="tbLockSIS" runat="server" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tbLockDateSIS" runat="server" ></asp:TextBox>
                        <asp:CalendarExtender ID="CalendeExtenderLockDateSIS" runat="server" Enabled="true" Format="yyMM"
                            TargetControlID="tbLockDateSIS" DefaultView="Months">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnLockSIS" runat="server" Text="Lock Table" CommandName ="SIS" OnCommand="btnLockClick"
                            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        <asp:Button ID="btnUnLockSIS" runat="server" Text="Unlock Table" CommandName ="SIS" OnCommand="btnUnlockClick"
                            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <b>Lock Movement Warehouse</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="tbLockHO" runat="server" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tbLockDateHO" runat="server" ></asp:TextBox>
                        <asp:CalendarExtender ID="CalendeExtenderLockDateHO" runat="server" Enabled="true" Format="yyMM"
                            TargetControlID="tbLockDateHO" DefaultView="Months">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnLockHO" runat="server" Text="Lock Table" CommandName ="WHO" OnCommand="btnLockClick"
                            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                        <asp:Button ID="btnUnLockHO" runat="server" Text="Unlock Table" CommandName="WHO" OnCommand="btnUnlockClick"
                            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <b>LOCK SLD PERIODE</b>
                        &nbsp
                        <asp:Label ID="lblinfo" runat="server" Text="* Dapat Di Lock Setelah semua Config Diatas Di lock di bulan yang sama" ForeColor="Red" Font-Italic="true" Font-Size="Smaller"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblmaxfbln" runat="server" Visible="false"></asp:Label>
                        <asp:Button ID="btnLockSldPeriode" runat="server" OnClick="btnLockSldPeriode_Click" Enabled="false" Text="Lock Sld Periode"/>
                    </td>
                </tr>
            </table>
        </div>    
    </asp:Panel>
</asp:Content>
