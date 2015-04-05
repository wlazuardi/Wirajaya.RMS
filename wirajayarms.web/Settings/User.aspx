﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs"
    Inherits="WirajayaRMS.Web.Settings.User" Title="User Settings" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../UserControl/AlertControl.ascx" TagName="AlertControl" TagPrefix="uc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <style type="text/css">
                .myCheckbox label
                {
                    margin-left: 10px;
                }
            </style>
            <script type="text/javascript">
                function pageLoad() {
                    $('.chkMenu input:checkbox').on('ifClicked', function(event) {
                        var that = this;
                        $(this).parents('.chkMenuBox').find('.chkInnerMenu input:checkbox').each(function(index, item) {
                            if (!that.checked)
                                $(item).iCheck('check');
                            else
                                $(item).iCheck('uncheck');
                        });
                    });

                    $('.chkInnerMenu input:checkbox').on('ifClicked', function(event) {
                        var that = this;
                        setTimeout(function() {
                            var allUnChecked = true;
                            $(that).parents('.chkMenuBox').find('.chkInnerMenu input:checkbox').each(function(a, b) {
                                if (this.checked) {
                                    allUnChecked = false;
                                    return false;
                                }
                            });
                            if (allUnChecked) {
                                $(that).parents('.chkMenuBox').find('.chkMenu input:checkbox').iCheck('uncheck');
                            }
                            else {
                                $(that).parents('.chkMenuBox').find('.chkMenu input:checkbox').iCheck('check');
                            }
                        }, 100);
                    });

                    var tblUser = $('#tblUser').dataTable({
                        "bDestroy": true,
                        "bPaginate": true,
                        "bLengthChange": true,
                        "bStateSave": true,
                        "bFilter": false,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false
                    });
                };
                pageLoad();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function(s, e) {
                    pageLoad();
                });
            </script>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                User List</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>
                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:Repeater ID="rptUser" runat="server" OnItemDataBound="rptUser_ItemDataBound"
                                        OnItemCommand="rptUser_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-hover" id="tblUser">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            No.
                                                        </th>
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
                                                    <asp:Literal ID="litFullName" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litEmail" runat="server"></asp:Literal>
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
                                    <asp:LinkButton ID="btnAddNewUser" runat="server" OnClick="btnAddNewUser_Click"
                                        CssClass="btn btn-primary pull-right">
                                        <i class="fa fa-plus"></i> Add New User
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" BehaviorID="popUpConfirm"
        PopupControlID="pnlConfirm" Type="Warning" />
    <uc:PopUpControl ID="popUpEditUser" runat="server" HeaderText="Edit User" BehaviorID="popUpEditUser"
        PopupControlID="pnlEditUser" Type="Default" />
    <uc:PopUpControl ID="popUpAddUser" runat="server" HeaderText="Add New User" BehaviorID="popUpAddUser"
        PopupControlID="pnlAddUser" />
    <asp:Panel ID="pnlAddUser" runat="server" DefaultButton="btnAddUser">
        <div class="modal-body">
            <div class="form">
                <div class="form-group">
                    <label class="control-label">
                        Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="No space, at least 5 characters, e.g., johndoe"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ControlToValidate="txtUsername"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regUsername" runat="server" ControlToValidate="txtUsername"
                        ValidationExpression="^[\w\d]{5,}$" ErrorMessage="Username must be at least 5 characters, allowed characters: a-z, 0-9, or _"
                        Display="Dynamic" ValidationGroup="AddUser"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Full Name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="e.g., Jonathan Doe"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqFullName" runat="server" ControlToValidate="txtFullName"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
                        Placeholder="At least 6 characters"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regPassword" runat="server" ControlToValidate="txtPassword"
                        ValidationExpression="^.{6,}$" ErrorMessage="Password must be at least 6 characters" Display="Dynamic"
                        ValidationGroup="AddUser"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Confirm Password</label>
                    <asp:TextBox ID="txtConfPassword" runat="server" CssClass="form-control" TextMode="Password"
                        Placeholder="At least 6 characters"></asp:TextBox>
                    <asp:CompareValidator ID="cmpConfPassword" runat="server" ControlToCompare="txtPassword"
                        ControlToValidate="txtConfPassword" ErrorMessage="Confirm password did not match" Display="Dynamic"
                        Operator="Equal" ValidationGroup="AddUser"></asp:CompareValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        E-mail Address</label>
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="e.g., username@domain.com"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Invalid e-mail format (valid format: username@example.com)" Display="Dynamic" ValidationGroup="AddUser"
                        ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="chkIsAdmin" CssClass="checkbox myCheckbox" runat="server" Text="Is Administrator? (Give the user a full administrative role)">
                    </asp:CheckBox>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="chkShowSalary" CssClass="checkbox myCheckbox" runat="server" Text="Show Salary? (Give the user a privilege to see salary for each organizational structure position)">
                    </asp:CheckBox>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Menu Access</label>
                    <div>
                        <asp:Repeater ID="rptMenuList" runat="server" OnItemDataBound="rptMenuList_ItemDataBound">
                            <ItemTemplate>
                                <div class="chkMenuBox">
                                    <asp:CheckBox ID="chkMenu" CssClass="checkbox myCheckbox chkMenu" runat="server" AutoPostBack="true" />
                                    <asp:Repeater ID="rptInnerMenu" runat="server" OnItemDataBound="rptInnerMenuList_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInnerMenu" CssClass="checkbox myCheckbox chkInnerMenu" Style="margin-left: 20px;"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnAddUser" runat="server" CssClass="btn btn-primary" Text="Add"
                OnClick="btnAddUser_Click" ValidationGroup="AddUser" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlEditUser" runat="server">
        <div class="modal-body">
            <ul class="nav nav-tabs" role="tablist" id="tabEditUser">
                <li class="active"><a href="#profil" role="tab" data-toggle="tab">Profile</a></li>
                <li><a href="#changePassword" role="tab" data-toggle="tab">Change Password</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="profil">
                    <asp:Panel ID="pnlProfile" runat="server" CssClass="form" Style="margin-top: 10px;"
                        DefaultButton="btnSaveEdit">
                        <asp:HiddenField ID="hidKdUserEdit" runat="server" />
                        <div class="form-group">
                            <label class="control-label">
                                Username</label>
                            <asp:TextBox ID="txtUsernameEdit" runat="server" CssClass="form-control" Placeholder="No space, at least 5 characters, e.g., johndoe"
                                Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqUsernameEdit" runat="server" ControlToValidate="txtUsernameEdit"
                                ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regUsernameEdit" runat="server" ControlToValidate="txtUsernameEdit"
                                ValidationExpression="^[\w\d]{5,}$" ErrorMessage="Username must be at least 5 characters, allowed characters: a-z, 0-9, or _"
                                Display="Dynamic" ValidationGroup="EditUser"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                Full Name</label>
                            <asp:TextBox ID="txtFullNameEdit" runat="server" CssClass="form-control" Placeholder="e.g., Jonathan Doe"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqFullNameEdit" runat="server" ControlToValidate="txtFullNameEdit"
                                ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                E-mail address</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                <asp:TextBox ID="txtEmailEdit" runat="server" CssClass="form-control" Placeholder="e.g., username@domain.com"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="reqEmailEdit" runat="server" ControlToValidate="txtEmailEdit"
                                ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regEmailEdit" runat="server" ControlToValidate="txtEmailEdit"
                                ErrorMessage="Invalid e-mail format (valid format: username@example.com)" Display="Dynamic" ValidationGroup="EditUser"
                                ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox ID="chkIsAdminEdit" CssClass="checkbox myCheckbox" runat="server" Text="Is Administrator? (Give the user a full administrative role)">
                            </asp:CheckBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox ID="chkShowSalaryEdit" CssClass="checkbox myCheckbox" Text="Show Salary? (Give the user a privilege to see salary for each organizational structure position)"
                                runat="server"></asp:CheckBox>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                Menu Access</label>
                            <div>
                                <asp:Repeater ID="rptMenuListEdit" runat="server" OnItemDataBound="rptMenuList_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="chkMenuBox">
                                            <asp:CheckBox ID="chkMenu" CssClass="checkbox myCheckbox chkMenu" runat="server" AutoPostBack="true" />
                                            <asp:Repeater ID="rptInnerMenu" runat="server" OnItemDataBound="rptInnerMenuList_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInnerMenu" CssClass="checkbox myCheckbox chkInnerMenu" Style="margin-left: 20px;"
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Button ID="btnSaveEdit" runat="server" CssClass="btn btn-primary pull-right"
                                    Text="Save" OnClick="btnSaveEdit_Click" ValidationGroup="EditUser" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="tab-pane" id="changePassword">
                    <asp:Panel ID="pnlChangePassword" runat="server" CssClass="form" Style="margin-top: 10px;"
                        DefaultButton="btnChangePassword">
                        <div class="form-group">
                            <label class="control-label">
                                Password</label>
                            <asp:TextBox ID="txtPasswordEdit" runat="server" CssClass="form-control" TextMode="Password"
                                Placeholder="At least 6 characters"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqPasswordEdit" runat="server" ControlToValidate="txtPasswordEdit"
                                ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regPasswordEdit" runat="server" ControlToValidate="txtPasswordEdit"
                                ValidationExpression="^.{6,}$" ErrorMessage="Password must be at least 6 characters" Display="Dynamic"
                                ValidationGroup="ChangePassword"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                Confirm Password</label>
                            <asp:TextBox ID="txtConfPasswordEdit" runat="server" CssClass="form-control" TextMode="Password"
                                Placeholder="At least 6 characters"></asp:TextBox>
                            <asp:CompareValidator ID="compConfPasswordEdit" runat="server" ControlToCompare="txtPasswordEdit"
                                ControlToValidate="txtConfPasswordEdit" ErrorMessage="Confirm password did not match" Display="Dynamic"
                                Operator="Equal" ValidationGroup="ChangePassword"></asp:CompareValidator>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-primary pull-right"
                                    Text="Change Password" ValidationGroup="EditUser" OnClick="btnChangePassword_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="modal-footer">
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfirm" runat="server">
        <div class="modal-body">
            <asp:Label ID="lblConfirm" runat="server"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnYesConfirm" runat="server" CssClass="btn btn-warning" Text="Yes"
                OnClick="btnYesConfirm_Click" />
            <asp:Button ID="btnNoConfirm" runat="server" CssClass="btn btn-primary" Text="Cancel"
                OnClick="btnNoConfirm_Click" />
        </div>
    </asp:Panel>
</asp:Content>
