<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestReprint.aspx.cs" Inherits="ATMOS_SROM.TestDummy.TestReprint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .hidden {
            display: none;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="300"></asp:ScriptManager>
    <iframe id="iframePDF" visible="false" runat="server" style="display: none;">
    </iframe>
    <div>
        <asp:Label ID="lblBonSale" runat="server" Text="No Bon Sale : "> </asp:Label>&nbsp; 
        <asp:TextBox ID="txtBonSale" runat="server"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btnPrintBonSales" runat="server" Text="Print" OnClick="btnPrintBonSales_Click" />
        <a id="bDoneLinkStruck" runat="server" target="_blank" href="http://your_url_here.html">Print Struck</a>
        <a id="bDoneLinkReprint" runat="server" target="_blank" href="http://your_url_here.html" style="display: none;">Print Gift Struck</a>
        <br />
    </div>
    <div>
        <asp:FileUpload ID="fileUpload" runat="server" ToolTip="Upload File Master Voucher" />
        <asp:Button ID="btnAdd" runat="server" Text="Upload Voucher"
            UseSubmitBehavior="false" OnClientClick="this.disabled = 'true';this.value = 'Please Wait...'" OnClick="btnAdd_Click" />
        <br />
        <asp:Label ID="lblinfo" runat="server" Text ="Result"></asp:Label>
    </div>
</asp:Content>
