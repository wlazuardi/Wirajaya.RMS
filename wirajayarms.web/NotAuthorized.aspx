<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotAuthorized.aspx.cs" Inherits="WirajayaRMS.Web.NotAuthorized" Title="401 Unauthorized" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="error-page">
        <h2 class="headline text-info"> 401</h2>
        <div class="error-content">
            <h3><i class="fa fa-warning text-yellow"></i> Oops! You're not authorized.</h3>
            <p>
                You are not authorized to see this page content.
                Meanwhile, you may try to go back to the home page or contact your system administrator.
            </p>
            <a href="home.aspx" style="font-size: 25px;">Go Back to Home Page &raquo;</a>
        </div><!-- /.error-content -->
    </div><!-- /.error-page -->
</asp:Content>
