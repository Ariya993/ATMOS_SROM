<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ATMOS_SROM._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <%--<img src="Image/all.jpg" width="100%" />--%>
            </td>
        </tr>
    </table>

    <asp:Button ID="btnDouble" runat="server" Visible="false" OnClick="btnDoubleClick" Text="Click Me!!!" />
    <asp:Button ID="Button1" runat="server" Visible="false" OnClick="btnDoubleClick" Text="Click Me!!!" />

    
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    </script>
</asp:Content>
