﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LevelApproval.aspx.cs"
    Inherits="WirajayaRMS.Web.Settings.LevelApproval" Title="Level Approval" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="gcc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<%@ Register Src="../UserControl/AlertControl.ascx" TagName="AlertControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/astreeview/astreeview_packed.js"></script>

    <link rel="Stylesheet" href="<%# Request.ApplicationPath.TrimEnd('/')%>/css/astreeview/astreeview.css" />

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/contextmenu/contextmenu_packed.js"></script>

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/input-mask/jquery.inputmask.js"></script>

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/input-mask/jquery.inputmask.numeric.extensions.js"></script>

    <link rel="Stylesheet" href="<%# Request.ApplicationPath.TrimEnd('/')%>/css/contextmenu/contextmenu.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:PopUpControl ID="popUpAddEditLvApproval" runat="server" HeaderText="Add New Level Approval"
        BehaviorID="popUpAddEditLvApproval" PopupControlID="pnlAddEditLvApproval" />
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" BehaviorID="popUpConfirm"
        PopupControlID="pnlConfirm" Type="Warning" />
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                Level Approval List</h3>
                            <div class="box-tools pull-right">
                                <%--<asp:DropDownList ID="ddlDivisi" runat="server" CssClass="btn btn-sm btn-default pull-right" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisi_SelectedIndexChanged">                                
                            </asp:DropDownList>  --%>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="alert alert-info">
                                <i class="fa fa-info"></i>You can add, edit, or delete level approval by right-clicking on the level approval list
                            </div>
                            <div class="row">
                                <div class="col-sm-2 pull-right">
                                </div>
                                <div class="col-sm-10">
                                    <gcc:ASTreeView ID="tvLvApproval" runat="server" EnableCheckbox="false" EnableNodeIcon="false"
                                        EnableRoot="false" EnableContextMenuAdd="false" EnableContextMenuEdit="false"
                                        EnableContextMenuDelete="false" OnNodeSelectedScript="nodeSelectHandler(elem);"
                                        EnableDragDrop="false" BasePath="~/css/astreeview/" />
                                </div>
                            </div>
                            <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true" />
                            <%--Hidden field untuk keperluan Add, Edit, Delete--%>
                            <asp:HiddenField ID="hidIsRoot" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidKdDivisi" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidParentKdLvApproval" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidKdLvApproval" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidNamaLvApproval" runat="server"></asp:HiddenField>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="pull-right" style="width: 230px">
                                    <asp:Button ID="btnAddLvApprovalBottom" runat="server" Text="Add" CssClass="btn btn-primary"
                                        disabled="disabled" OnClientClick="return AddLvApproval();" />
                                    <asp:Button ID="btnEditLvApprovalBottom" runat="server" Text="Edit" CssClass="btn btn-primary"
                                        disabled="disabled" OnClientClick="return EditLvApproval();" />
                                    <asp:Button ID="btnDeleteLvApprovalBottom" runat="server" Text="Delete" CssClass="btn btn-danger"
                                        disabled="disabled" OnClientClick="return DeleteLvApproval();" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%--Pop up untuk add/edit--%>
    <asp:Panel ID="pnlAddEditLvApproval" runat="server">
        <div class="modal-body">
            <div class="form">
                <div class="form-group">
                    <label class="control-label">
                        Parent</label>
                    <asp:TextBox ID="txtParentNmLevelApproval" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Level Approval Name</label>
                    <asp:TextBox ID="txtNmLevelApproval" runat="server" CssClass="form-control" Placeholder="e.g., Manager"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNamaLevelApproval" runat="server" ControlToValidate="txtNmLevelApproval" 
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditLevelApproval"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Document Status</label>
                    <asp:TextBox ID="txtStatusDokumen" runat="server" CssClass="form-control" Placeholder="e.g., Waiting for Manager"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqStatusDokumen" runat="server" ControlToValidate="txtStatusDokumen" 
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditLevelApproval"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnAddLevelApproval" runat="server" CssClass="btn btn-primary" Text="Tambah"
                OnClick="btnAddLevelApproval_Click" ValidationGroup="AddEditLevelApproval" />
            <asp:Button ID="btnEditLevelApproval" runat="server" CssClass="btn btn-primary" Text="Simpan"
                OnClick="btnEditLevelApproval_Click" ValidationGroup="AddEditLevelApproval"/>
        </div>
    </asp:Panel>
    
    <%--Popup untuk konfirmasi delete--%>
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
    
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script type="text/javascript">
            function AddLvApproval() {
                $('#<%=pnlAddEditLvApproval.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnAddLevelApproval.ClientID%>")');

                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdLvApprovalParent = $('#<%=hidKdLvApproval.ClientID%>').val();
                var nmLvApprovalParent = $('#<%=hidNamaLvApproval.ClientID%>').val();

                $('#<%=pnlAddEditLvApproval.ClientID%>').parent().parent().find('.modal-title').html('Add New Level Approval');
                $find('popUpAddEditLvApproval').show();

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    $('#<%=hidParentKdLvApproval.ClientID%>').val(0);
                }
                else {
                    nmLvApprovalParent = nmLvApprovalParent.substr(nmLvApprovalParent.indexOf(kdLvApprovalParent) + kdLvApprovalParent.length + 2);
                    $('#<%=hidParentKdLvApproval.ClientID%>').val(kdLvApprovalParent);
                }

                $('#<%=txtParentNmLevelApproval.ClientID%>').val(nmLvApprovalParent);
                $('#<%=txtNmLevelApproval.ClientID%>').val('');
                $('#<%=txtStatusDokumen.ClientID%>').val('');
                $('#<%=btnAddLevelApproval.ClientID%>').show();
                $('#<%=btnEditLevelApproval.ClientID%>').hide();

                return false;
            }

            function EditLvApproval() {
                $('#<%=pnlAddEditLvApproval.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnEditLevelApproval.ClientID%>")');

                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdLvApproval = $('#<%=hidKdLvApproval.ClientID%>').val();

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This node could not be edited');
                    return false;
                }

                $('#<%=pnlAddEditLvApproval.ClientID%>').parent().parent().find('.modal-title').html('Edit Level Approval');
                $.ajax({
                    url: 'LevelApproval.aspx/GetLevelApprovalData',
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        kdDivisi: kdDivisi,
                        kdLevelApproval: kdLvApproval
                    }),
                    success: function(data) {
                        var result = data.d;
                        $('#<%=hidParentKdLvApproval.ClientID%>').val(result.ParentKdLevelApproval);
                        $('#<%=hidKdLvApproval.ClientID%>').val(result.KdLevelApproval);
                        $('#<%=txtNmLevelApproval.ClientID%>').val(result.NmLevelApproval);
                        $('#<%=txtStatusDokumen.ClientID%>').val(result.StatusDokumen);
                        if (result.ParentNmLvApproval) {
                            $('#<%=txtParentNmLevelApproval.ClientID%>').val(result.ParentNmLevelApproval);
                        }
                        else {
                            $('#<%=txtParentNmLevelApproval.ClientID%>').val(result.NmDivisi);
                        }
                        $find('popUpAddEditLvApproval').show();
                        $('#<%=btnAddLevelApproval.ClientID%>').hide();
                        $('#<%=btnEditLevelApproval.ClientID%>').show();
                    }
                });

                return false;
            }

            function DeleteLvApproval() {
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdLvApproval = $('#<%=hidKdLvApproval.ClientID%>').val();
                var nmLvApproval = $('#<%=hidNamaLvApproval.ClientID%>').val();

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This node could not be deleted');
                    return false;
                }

                nmLvApproval = nmLvApproval.substr(nmLvApproval.indexOf(kdLvApproval) + kdLvApproval.length + 2);

                $find('popUpConfirm').show();
                $('#<%=hidKdLvApproval.ClientID%>').val(kdLvApproval);
                $('#<%=lblConfirm.ClientID%>').html('Are you sure want to delete this level approval: <b>"' + nmLvApproval + '"</b>?<br/>Level approval will be deleted, including the sub-level approval(s)');
                return false;
            }

            function onAddContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdLvApprovalParent = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmLvApprovalParent = $(clientID.getSelectedItem()).text();

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaLvApproval.ClientID%>').val(nmLvApprovalParent);
                $('#<%=hidKdLvApproval.ClientID%>').val(kdLvApprovalParent);

                return AddLvApproval();
            }

            function onEditContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdLvApproval = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidKdLvApproval.ClientID%>').val(kdLvApproval);

                return EditLvApproval();
            }

            function onDeleteContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdLvApproval = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmLvApproval = $(clientID.getSelectedItem()).text();

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaLvApproval.ClientID%>').val(nmLvApproval);
                $('#<%=hidKdLvApproval.ClientID%>').val(kdLvApproval);

                return DeleteLvApproval();
            }

            function nodeSelectHandler(elem) {
                if ($(elem).parents('li')[0] == $($(elem).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                var kdDivisi = $($(elem).parents('li:last')).attr('treeNodeValue');
                var kdLvApproval = elem.parentNode.getAttribute("treeNodeValue");
                var nmLvApproval = $(elem).text();

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaLvApproval.ClientID%>').val(nmLvApproval);
                $('#<%=hidKdLvApproval.ClientID%>').val(kdLvApproval);
                $('#<%=btnAddLvApprovalBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnEditLvApprovalBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnDeleteLvApprovalBottom.ClientID%>').removeAttr('disabled');
            }
        </script>

    </asp:PlaceHolder>
</asp:Content>
