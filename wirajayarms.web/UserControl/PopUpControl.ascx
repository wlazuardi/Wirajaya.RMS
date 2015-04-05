<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopUpControl.ascx.cs"
    Inherits="WirajayaRMS.Web.UserControl.PopUpControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<act:ModalPopupExtender ID="mpePopUpControl" runat="server" TargetControlID="hidPopUpControl"
    PopupControlID="pnlPopUpControl" CancelControlID="btnClose"
    BackgroundCssClass="modal-backdrop in">
    <Animations>
        <OnShown>            
            <FadeIn Duration="0.1" Fps="15"></FadeIn>            
        </OnShown>
        <OnHiding>
            <FadeOut Duration="0.1" Fps="15"></FadeOut>
        </OnHiding>
    </Animations>
</act:ModalPopupExtender>

<asp:HiddenField ID="hidPopUpControl" runat="server" />

<style>
    .customModal
    {
    	top : 0px !important;
    	left : 0px !important;
    	right : 0px !important;
    	bottom : 0px !important;
    	overflow-y : auto;
    }
    
    .modal-lg
    {
    	min-width: 900px;
    }
</style>

<asp:Panel ID="pnlPopUpControl" runat="server" CssClass="customModal">
    <div ID="divModalDialog" runat="server" class="modal-dialog">
        <div class="modal-content">
            <div id="pnlHeader" runat="server" class="modal-header">
                <button ID="btnClose" runat="server" type="button" class="close" data-dismiss="modal" onclick="javascript:$('html').css('overflow', 'auto');">
                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                </button>
                <h4 class="modal-title">
                    <i id="iIndicator" runat="server"></i> 
                    <asp:Literal ID="litTitle" runat="server"></asp:Literal>
                </h4>
            </div>
            <asp:UpdatePanel ID="uPnlPopUpContent" runat="server">
                <ContentTemplate>
                    <asp:PlaceHolder ID="phPopUpContent" runat="server"></asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Panel>
