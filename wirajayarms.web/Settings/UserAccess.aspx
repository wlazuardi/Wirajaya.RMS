﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAccess.aspx.cs"
    Inherits="WirajayaRMS.Web.Settings.UserAccess" Title="User Access" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../UserControl/AlertControl.ascx" TagName="AlertControl" TagPrefix="uc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <style>
                .myCheckbox label
                {
                    margin-left: 10px;
                }
            </style>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                User Access List</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>
                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Division
                                </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlDivisi" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlDivisi_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Organizational Structure
                                </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlStrukturOrganisasi" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlStrukturOrganisasi_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Level Approval
                                </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlLevelApproval" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlLevelApproval_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group" style="margin-top:50px;">
                                <div class="col-sm-12">
                                    <asp:Repeater ID="rptUserAccess" runat="server" OnItemDataBound="rptUserAccess_ItemDataBound"
                                        OnItemCommand="rptUserAccess_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-hover table-striped" id="tblUserAccess">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            No.
                                                        </th>
                                                        <th>
                                                            Username
                                                        </th>
                                                        <th>
                                                            Division
                                                        </th>
                                                        <th>
                                                            Organizational Structure
                                                        </th>                                                        
                                                        <th>
                                                            Level Approval
                                                        </th>
                                                        <th width="90">
                                                            Option
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.ItemIndex + 1 %>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litUsername" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litDivisi" runat="server"></asp:Literal>
                                                </td>                                                
                                                <td>
                                                    <asp:Literal ID="litStrukturOrganisasi" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litLevelApproval" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" Width="35" CssClass="btn btn-sm btn-primary">
                                                        <span class="fa fa-pencil"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                                        <span class="fa fa-trash-o"></span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:LinkButton ID="btnAddNewAccess" runat="server" OnClick="btnAddNewAccess_Click"
                                        CssClass="btn btn-primary pull-right">
                                        <i class="fa fa-plus"></i> Add New User Access
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                var prm = Sys.WebForms.PageRequestManager.getInstance();

                function pageLoad() {
                    var tblUserAccess = $('#tblUserAccess').dataTable({
                        "bDestroy": true,
                        "bPaginate": true,
                        "bLengthChange": true,
                        "bStateSave": true,
                        "bFilter": true,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false
                    });

                    var tblSearchUser = $('#tblSearchUser').dataTable({
                        "bDestroy": true,
                        "bPaginate": true,
                        "bLengthChange": false,
                        "bStateSave": true,
                        "bFilter": false,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false
                    });
                }

                prm.add_beginRequest(function(s, e) {
                    
                });

                prm.add_endRequest(function(s, e) {
                    pageLoad();
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" 
        BehaviorID="popUpConfirm" PopupControlID="pnlConfirm" Type="Warning"/>
        
    <uc:PopUpControl ID="popUpAddEditUserAccess" runat="server" HeaderText="Add / Edit User Access"
        BehaviorID="popUpAddEditUserAccess" PopupControlID="pnlAddEditUserAccess" Type="Default" />
    
    <uc:PopUpControl ID="popUpSearchUser" runat="server" HeaderText="Search User" BehaviorID="popUpSearchUser"
        PopupControlID="pnlSearchUser" Type="Default" EnableCloseBtn="false" />
        
    <asp:Panel ID="pnlSearchUser" runat="server" DefaultButton="btnSearch">
        <div class="modal-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-2">
                        Username</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtUsernameSearch" runat="server" CssClass="form-control" Placeholder="Search by username"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2">
                        Full Name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtFullNameSearch" runat="server" CssClass="form-control" Placeholder="Search by full name"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2">
                        E-mail</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtEmailSearch" runat="server" CssClass="form-control" Placeholder="Search by e-mail"></asp:TextBox>
                    </div>

                    <script>
                        // javascript to add to your aspx page
                        function CheckAllParameter() {
                            var txtUsernameSearch = document.getElementById('<%= txtUsernameSearch.ClientID %>');
                            var txtFullNameSearch = document.getElementById('<%= txtFullNameSearch.ClientID %>');
                            var txtEmailSearch = document.getElementById('<%= txtEmailSearch.ClientID %>');

                            if (txtUsernameSearch.value.length < 3 && txtFullNameSearch.value.length < 3 && txtEmailSearch.value.length < 3) {
                                return false;
                            }

                            return true;
                        }

                        function ValidateSearch(source, args) {
                            if (CheckAllParameter() == false)
                                args.IsValid = false;
                            else
                                args.IsValid = true;
                        }
                    </script>

                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:CustomValidator runat="server" ID="cvSearchUser" ClientValidationFunction="ValidateSearch"
                            ErrorMessage="Please provide at least one search field with minimum 3 characters"
                            Display="Dynamic" ValidationGroup="SearchUser"></asp:CustomValidator>
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary pull-right"
                            ValidationGroup="SearchUser" OnClick="btnSearch_Click">
                            <i class="fa fa-search"></i> Search
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row" style="margin-top: 20px;">
                    <div class="col-sm-12">
                        <asp:Repeater ID="rptUserSearch" runat="server" OnItemDataBound="rptUserSearch_ItemDataBound" OnItemCommand="rptUserSearch_OnItemCommand">
                            <HeaderTemplate>
                                <table class="table table-striped table-hover" id="tblSearchUser">
                                    <thead>
                                        <tr>
                                            <th>
                                                Username
                                            </th>
                                            <th>
                                                Full Name
                                            </th>
                                            <th>
                                                E-mail
                                            </th>
                                            <th>
                                                Choose
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>                                
                                <tr>
                                    <td>
                                        <asp:Literal ID="litUsername" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litFullName" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnChoose" runat="server" Text="Choose" CssClass="btn btn-sm btn-success">
                                            <i class="fa fa-check"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <%--<div class="form-group">
                    <label class="control-label">
                        Hak Akses Menu</label>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" CssClass="myCheckbox">
                        <asp:ListItem Text="Pendataan" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Setting" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Request" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Kandidat" Value="4"></asp:ListItem>
                    </asp:CheckBoxList>
                   
                </div>--%>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnCancelSearch" runat="server" CssClass="btn btn-danger pull-right"
                Text="Cancel" OnClick="btnCancelSearch_Click" />
        </div>
    </asp:Panel>
        
    <asp:Panel ID="pnlAddEditUserAccess" runat="server" DefaultButton="btnSaveAddUserAcces">
        <div class="modal-body">
            <div class="form">
                <div class="form-group">
                    <label class="control-label">
                        Username</label>
                    <div class="input-group">
                        <asp:HiddenField ID="hidArgument" runat="server"/>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="No space, at least 5 characters, e.g., johndoe" Enabled="false"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="btnShowSearchUser" runat="server" CssClass="btn btn-info btn-flat"
                                OnClick="btnShowSearchUser_Click">
                                <i class="fa fa-search"></i>
                            </asp:LinkButton>
                        </span>
                    </div>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ControlToValidate="txtUsername"
                        ErrorMessage="Field tidak boleh kosong" Display="Dynamic" ValidationGroup="AddEditUserAccess">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regUsername" runat="server" ControlToValidate="txtUsername"
                        ValidationExpression="^[\w\d]{5,}$" ErrorMessage="Username must be at least 5 characters, allowed characters: a-z, 0-9, atau _"
                        Display="Dynamic" ValidationGroup="AddEditUserAccess">
                    </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Division</label>
                    <asp:DropDownList ID="ddlDivisiAddEdit" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisiAddEdit_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqDivisiAddEdit" runat="server" ControlToValidate="ddlDivisiAddEdit"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditUserAccess"
                        InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Organizational Structure</label>
                    <asp:DropDownList ID="ddlStrukturOrganisasiAddEdit" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqStrukturOrganisasi" runat="server" ControlToValidate="ddlStrukturOrganisasiAddEdit"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditUserAccess"
                        InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Level Approval</label>
                    <asp:DropDownList ID="ddlLevelApprovalAddEdit" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqLevelApprovalAddEdit" runat="server" ControlToValidate="ddlLevelApprovalAddEdit"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditUserAccess"
                        InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <%--<div class="form-group">
                    <label class="control-label">
                        Hak Akses Menu</label>
                    <asp:CheckBoxList ID="chkListMenuAccess" runat="server" CssClass="myCheckbox">
                        <asp:ListItem Text="Pendataan" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Setting" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Request" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Kandidat" Value="4"></asp:ListItem>
                    </asp:CheckBoxList>
                    <asp:CustomValidator runat="server" ID="cvListMenuAccess" ClientValidationFunction="ValidateMenuAccess"
                        ErrorMessage="Pilih salah satu menu" Display="Dynamic" ValidationGroup="AddEditUserAccess"></asp:CustomValidator>

                    <script>
                        // javascript to add to your aspx page
                        function ValidateMenuAccess(source, args) {
                            var chkListModules = document.getElementById('<%= chkListMenuAccess.ClientID %>');
                            var chkListinputs = chkListModules.getElementsByTagName("input");
                            for (var i = 0; i < chkListinputs.length; i++) {
                                if (chkListinputs[i].checked) {
                                    args.IsValid = true;
                                    return;
                                }
                            }
                            args.IsValid = false;
                        }
                    </script>

                </div>--%>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSaveAddUserAcces" runat="server" CssClass="btn btn-primary pull-right"
                Text="Add" ValidationGroup="AddEditUserAccess" OnClick="btnSaveAddUserAcces_Click" />
        </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlConfirm" runat="server">
        <div class="modal-body">
            <asp:Label ID="lblConfirm" runat="server"></asp:Label>
            <asp:HiddenField ID="hfConfirmArgument" runat="server"></asp:HiddenField>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnYesConfirm" runat="server" CssClass="btn btn-warning" Text="Yes"
                OnClick="btnYesConfirm_Click" />
            <asp:Button ID="btnNoConfirm" runat="server" CssClass="btn btn-primary" Text="Cancel"
                OnClick="btnNoConfirm_Click" />
        </div>
    </asp:Panel>
</asp:Content>
