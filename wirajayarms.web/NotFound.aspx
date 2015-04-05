<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="WirajayaRMS.Web.NotFound" Title="404 Not Found Page" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="error-page">
        <h2 class="headline text-info"> 404</h2>
        <div class="error-content">
            <h3><i class="fa fa-warning text-yellow"></i> Oops! Page not found.</h3>
            <p>
                We could not find the page you were looking for.
                Meanwhile, you may return to home page or try refreshing this page later.
            </p>
            <a href="home.aspx" style="font-size: 25px;">Go Back to Home Page &raquo;</a>
        </div><!-- /.error-content -->
    </div><!-- /.error-page -->
</asp:Content>
