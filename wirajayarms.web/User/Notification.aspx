<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="WirajayaRMS.Web.User.Notification" Title="All Notification" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Repeater ID="rptNotificationList" runat="server" OnItemDataBound="rptNotificationList_ItemDataBound">
                        <HeaderTemplate>
                            <ul class="timeline">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="time-label">
                                <span id="spanMonth" runat="server"></span>
                            </li>
                            <asp:Repeater ID="rptInnerNotificationList" runat="server" OnItemDataBound="rptInnerNotificationList_ItemDataBound">
                                <ItemTemplate>
                                    <li>
                                        <span id="spanIcon" runat="server"></span>
                                        <div id="divItem" runat="server" class="timeline-item">
                                            <span class="time"><i class="fa fa-clock-o"></i> <asp:Literal ID="litDate" runat="server"></asp:Literal></span>
                                            <h3 class="timeline-header no-border"><asp:HyperLink ID="linkMessage" runat="server"></asp:HyperLink></h3>                                                
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <FooterTemplate>
                            <li>
                                <i class="fa fa-clock-o"></i>
                            </li>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>                                             
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
