<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WirajayaRMS.Web.User.Profile" Title="My Profile" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../UserControl/AlertControl.ascx" TagName="AlertControl" TagPrefix="uc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .jcrop-holder
        {
        	display:inline-block !important;
        }
    </style>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <asp:Panel ID="pnlChangeProfile" runat="server" CssClass="box box-info" DefaultButton="btnSaveEdit">
                        <div class="box-header">
                            <h3 class="box-title">
                                Change Profile</h3>
                            <div class="box-tools pull-right">                                
                            </div>
                        </div>
                        <div class="box-body form-horizontal">                            
                            <div class="col-sm-9">
                                <div class="form-group">
                                    <label class="control-label col-sm-3">
                                        Username
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqUsername" runat="server" ControlToValidate="txtUsername"
                                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditProfile">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regUsername" runat="server" ControlToValidate="txtUsername"
                                            ValidationExpression="^[\w\d]{5,}$" ErrorMessage="Username must be at least 5 characters, allowed characters: a-z, 0-9, atau _"
                                            Display="Dynamic" ValidationGroup="EditProfile"></asp:RegularExpressionValidator>
                                    </div>                                
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">
                                        Full Name
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqFullName" runat="server" ControlToValidate="txtFullName"
                                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditProfile">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">
                                        E-mail Address
                                    </label>
                                    <div class="col-sm-9">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="e.g., username@domain.com"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditProfile"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="Invalid e-mail format. Valid format: username@domain.com" Display="Dynamic" ValidationGroup="EditProfile"
                                                ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-9">
                                        <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <span style="border:1px solid #ddd;display:inline-block;border-radius:4px;width:142px;position:relative;">
                                    <asp:LinkButton ID="btnRemoveImage" runat="server" CssClass="btn btn-primary" OnClick="btnRemoveImage_Click" ToolTip="Remove Image"
                                        style="position: absolute;right: -5px;top: -5px;border-radius: 15px;width: 30px;height: 30px;display: inline-block;padding: 2px;border: 3px solid #fff;box-sizing: border-box;">
                                        <i class="fa fa-times"></i>
                                    </asp:LinkButton>
                                    <a id="linkUploadPhoto" href="#">
                                        <asp:Image ID="imgPhoto" runat="server" Width="140" style="background:#ddd; border:5px solid #fff;border-radius:4px;"/>                                        
                                        <button class="btn btn-sm btn-primary" style="position: absolute;bottom: 10px;right: 10px;opacity: 0.7;"><span class="glyphicon glyphicon-camera"></span></button>
                                    </a>
                                    <asp:FileUpload ID="fuPhoto" runat="server" style="display:none;" accept="image/*"/>                                        
                                    <asp:Button ID="btnUploadPhoto" runat="server" style="display:none;" OnClick="btnUploadPhoto_Click"/>
                                    <asp:HiddenField ID="hidPhoto" runat="server"/>
                                    
                                    <%--Hidden field untuk crop image--%>
                                    <asp:HiddenField ID="hidX1" runat="server"/>
                                    <asp:HiddenField ID="hidX2" runat="server"/>
                                    <asp:HiddenField ID="hidY1" runat="server"/>
                                    <asp:HiddenField ID="hidY2" runat="server"/>
                                    <asp:HiddenField ID="hidW" runat="server"/>
                                    <asp:HiddenField ID="hidH" runat="server"/>
                                </span>
                            </div>                          
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    &nbsp;
                                </label>
                                <div class="col-sm-9">                                    
                                    <asp:LinkButton ID="btnSaveEdit" OnClick="btnSaveEdit_Click" ToolTip="Simpan Perubahan" runat="server" CssClass="btn btn-primary pull-right">
                                        <span class="fa fa-save"></span> &nbsp;&nbsp;Save Changes
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlChangePassword" runat="server" CssClass="box box-info" DefaultButton="btnChangePassword">
                        <div class="box-header">
                            <h3 class="box-title">
                                Change Password</h3>
                            <div class="box-tools pull-right">                                
                            </div>
                        </div>
                        <div class="box-body form-horizontal">
                            <div class="col-sm-9">
                                <div class="form-group">
                                    <label class="control-label col-sm-3">
                                        Old password
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password"
                                            Placeholder="At least 6 characters"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                            ValidationExpression="^.{6,}$" ErrorMessage="Password must be at least 6 characters" Display="Dynamic"
                                            ValidationGroup="ChangePassword"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">
                                        New Password
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"
                                            Placeholder="At least 6 characters"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="ChangePassword"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                            ValidationExpression="^.{6,}$" ErrorMessage="Password must be at least 6 characters" Display="Dynamic"
                                            ValidationGroup="ChangePassword"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">
                                        Confirm New Password
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtConfNewPassword" runat="server" CssClass="form-control" TextMode="Password"
                                            Placeholder="At least 6 characters"></asp:TextBox>
                                        <asp:CompareValidator ID="compConfNewPassword" runat="server" ControlToCompare="txtNewPassword"
                                            ControlToValidate="txtConfNewPassword" ErrorMessage="Confirm password did not match" Display="Dynamic"
                                            Operator="Equal" ValidationGroup="ChangePassword"></asp:CompareValidator>
                                    </div>                                
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-9">
                                        <uc:AlertControl ID="alertChangePassword" runat="server" Visible="false" Dismissable="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                            </div>                            
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    &nbsp;
                                </label>
                                <div class="col-sm-9">                                    
                                    <asp:LinkButton ID="btnChangePassword" OnClick="btnChangePassword_Click" ToolTip="Ubah Password" runat="server" CssClass="btn btn-primary pull-right">
                                        <span class="fa fa-check"></span> Change Password
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlUserAccessList" runat="server" CssClass="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                Your Role</h3>
                            <div class="box-tools pull-right">                                
                            </div>
                        </div>
                        <div class="box-body form-horizontal" style="min-height:100px;">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                                    <asp:Repeater ID="rptUserAccess" runat="server" OnItemDataBound="rptUserAccess_ItemDataBound">
                                        <HeaderTemplate>
                                            <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <asp:Literal ID="litLvApproval" runat="server"></asp:Literal> - 
                                                <asp:Literal ID="litSO" runat="server"></asp:Literal> -
                                                <asp:Literal ID="litDivisi" runat="server"></asp:Literal>
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    &nbsp;
                                </label>
                                <div class="col-sm-9">    
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <script type="text/javascript">
                function pageLoad() {
                    $('#<%=fuPhoto.ClientID%>').off('change').on('change', function() {
                        setTimeout(function() {
                            document.forms[0].encoding = 'multipart/form-data';
                            $('#<%=btnUploadPhoto.ClientID%>').click();
                        }, 1000);
                    });

                    $('#linkUploadPhoto').off('click').on('click', function(ev) {
                        ev.preventDefault();
                        $('#<%=fuPhoto.ClientID%>').click();
                    });

                    
                }
                
                function showCoords(c) {
                    $('#<%=hidX1.ClientID%>').val(c.x);
                    $('#<%=hidY1.ClientID%>').val(c.y);
                    $('#<%=hidX2.ClientID%>').val(c.x2);
                    $('#<%=hidY2.ClientID%>').val(c.y2);
                    $('#<%=hidW.ClientID%>').val(c.w);
                    $('#<%=hidH.ClientID%>').val(c.h);
                };

                pageLoad();
                
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function(s, e) {
                    pageLoad();
                });
            </script>

            <link href="../css/jCrop/jquery.Jcrop.min.css" rel="stylesheet" type="text/css" />
            <script src="../js/plugins/jCrop/jquery.Jcrop.min.js" type="text/javascript"></script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadPhoto"/>
        </Triggers>
    </asp:UpdatePanel>
    
    <uc:PopUpControl ID="popUpChangeUserPhoto" runat="server" HeaderText="Change Photo" BehaviorID="popUpChangeUserPhoto"
        PopupControlID="pnlChangeUserPhoto" Type="Default" EnableCloseBtn="true"/>
        
    <asp:Panel ID="pnlChangeUserPhoto" runat="server">
        <div class="modal-body">
            <div class="tab-pane active" style="text-align:center;">
                <asp:Image ID="imgPhotoCrop" runat="server"/>
            </div>            
        </div>
        <div class="modal-footer">               
            <asp:LinkButton ID="btnDoneCrop" runat="server" CssClass="btn btn-primary pull-right" 
                OnClick="btnDoneCrop_Click" ValidationGroup="CropImage">
                <i class="fa fa-save"></i>&nbsp; Crop & Save
            </asp:LinkButton>
        </div>
    </asp:Panel>
</asp:Content>
