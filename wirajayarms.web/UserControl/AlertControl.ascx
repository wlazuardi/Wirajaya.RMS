<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlertControl.ascx.cs"
    Inherits="WirajayaRMS.Web.UserControl.AlertControl" %>
<asp:Panel ID="pnlAlert" runat="server" CssClass="alert alert-info" Style="margin-top: 20px">
    <i id="iIndicator" runat="server"></i>
    <button id="btnDismissAlert" runat="server" class="close" data-dismiss="alert" aria-hidden="true" onserverclick="btnDismissAlert_Click" style="display:none;">×</button>
    <asp:Label ID="lblError" runat="server"></asp:Label>
</asp:Panel>
