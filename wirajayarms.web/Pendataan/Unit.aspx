﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Unit.aspx.cs" Inherits="WirajayaRMS.Web.Pendataan.Unit" Title="Unit" EnableEventValidation="false"%>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="gcc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<%@ Register src="../UserControl/AlertControl.ascx" tagname="AlertControl" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/astreeview/astreeview_packed.js"></script>

    <link rel="Stylesheet" href="<%# Request.ApplicationPath.TrimEnd('/')%>/css/astreeview/astreeview.css" />

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/contextmenu/contextmenu_packed.js"></script>

    <link rel="Stylesheet" href="<%# Request.ApplicationPath.TrimEnd('/')%>/css/contextmenu/contextmenu.css" />
    
    <link rel="Stylesheet" href="<%# Request.ApplicationPath.TrimEnd('/')%>/css/asdropdowntreeview/dropdowntreeview.css" />
    
    <style>
        .defaultDropdownBox
        {
        	border: none;
        	width: 100%;
        }
        .defaultDropdownBoxContainer
        {
        	width: 100%;
        }
        .defaultDropdownTextContainer
        {
        	padding: 6px 12px;
        }
        .defaultDropdownIconTD
        {
        	background: #ddd;
        }
        .defaultDropdownTree
        {
        	width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:PopUpControl ID="popUpAddEditUnit" runat="server" HeaderText="Add New Unit"
        BehaviorID="popUpAddEditUnit" PopupControlID="pnlAddEditUnit" />
        
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" 
        BehaviorID="popUpConfirm" PopupControlID="pnlConfirm" Type="Warning"/>
        
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Unit List</h3>       
                        <div class="box-tools pull-right">
                        </div>                 
                    </div>
                    <div class="box-body">   
                        <div class="alert alert-info">
                            <i class="fa fa-info"></i>You can add, edit, or delete unit data by right-clicking on the unit list
                        </div>
                        <div class="row">
                            <div class="col-sm-2 pull-right">
                            </div>
                            <div class="col-sm-10">
                                <gcc:ASTreeView ID="tvUnit" runat="server" EnableRoot="false" EnableCheckbox="false" EnableNodeIcon="false"
                                    EnableContextMenuAdd="false" EnableContextMenuEdit="false" EnableContextMenuDelete="false" OnNodeSelectedScript="nodeSelectHandler(elem);"
                                    EnableDragDrop="false"
                                    BasePath="~/css/astreeview/" />
                            </div>
                        </div>
                        
                        <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true"/>
                        
                        <%--Hidden field untuk keperluan Add, Edit, Delete--%>
                        <asp:HiddenField ID="hidIsRoot" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidKdDivisi" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidMaxJabatan" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidParentKdUnit" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidKdUnit" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidNamaUnit" runat="server"></asp:HiddenField>
                    </div>
                    <div class="box-footer">
                        <div class="row">
                            <div class="pull-right" style="width:230px">
                                <asp:Button ID="btnAddUnitBottom" runat="server" Text="Add" CssClass="btn btn-primary" disabled="disabled" OnClientClick="return AddUnit();"/>
                                <asp:Button ID="btnEditUnitBottom" runat="server" Text="Edit" CssClass="btn btn-primary" disabled="disabled" OnClientClick="return EditUnit();"/>
                                <asp:Button ID="btnDeleteUnitBottom" runat="server" Text="Delete" CssClass="btn btn-danger" disabled="disabled" OnClientClick="return DeleteUnit();"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <%--Pop up untuk add/edit--%>
    <asp:Panel ID="pnlAddEditUnit" runat="server">
        <div class="modal-body">
            <div class="form">
                <div class="form-group">
                    <label class="control-label">
                        Parent</label>
                    <asp:TextBox ID="txtParentNmUnit" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Unit Name</label>
                    <asp:TextBox ID="txtNamaUnit" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNamaUnit" runat="server" ControlToValidate="txtNamaUnit"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditUnit"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Maximum Position</label>
                    <asp:DropDownList ID="ddlMaxJabatan" runat="server" CssClass="form-control" OnChange="OnMaxJabatanChanged();"></asp:DropDownList>    
                    <asp:RequiredFieldValidator ID="reqMaxJabatan" runat="server" 
                        InitialValue="0" 
                        ControlToValidate="ddlMaxJabatan" 
                        ErrorMessage="Field must be filled"
                        ValidationGroup="AddEditUnit">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnAddUnit" runat="server" CssClass="btn btn-primary" Text="Add"
                OnClick="btnAddUnit_Click" ValidationGroup="AddEditUnit" />
            <asp:Button ID="btnEditUnit" runat="server" CssClass="btn btn-primary" Text="Save"
                OnClick="btnEditUnit_Click" ValidationGroup="AddEditUnit" />
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
            function OnMaxJabatanChanged() {
                var kdMaxJabatan = $('#<%=ddlMaxJabatan.ClientID%>').val();
                $('#<%=hidMaxJabatan.ClientID%>').val(kdMaxJabatan);
            }
            
            function BuildJabatanDropdownTree(kdDivisi) {
                $('#<%=ddlMaxJabatan.ClientID%>').empty();
                $('#<%=ddlMaxJabatan.ClientID%>')
                                .append($('<option></option>')
                                .attr('value', '0')
                                .text('-- Select Position --'));
                $.ajax({
                    url: 'Unit.aspx/GetJabatanList',
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    async: false,
                    data: JSON.stringify({
                        kdDivisi: kdDivisi
                    }),
                    beforeSend: function() {
                        //Show the processing div
                        $('#ctl00_upLoading').show();
                    },
                    success: function(data) {
                        var result = data.d;
                        $.each(result, function(index, jabatan) {
                            $('#<%=ddlMaxJabatan.ClientID%>')
                                .append($('<option></option>')
                                .attr('value', jabatan.KdJabatan)
                                .html(jabatan.KdJabatan + '. ' + jabatan.NmJabatan));
                            if (jabatan.ChildNode) {
                                BuildJabatanChildNode(jabatan.ChildNode, 1);
                            }
                        });
                    },
                    complete: function() {
                        //Hide the processing div
                        $('#ctl00_upLoading').hide();
                    }
                });
            }

            function BuildJabatanChildNode(jabatan, depth) {
                var space = "";
                for (var i = 0; i < depth; i++) 
                {
                    space += "&nbsp;&nbsp;&nbsp;";
                }

                $.each(jabatan, function(index, itemJabatan) {
                    $('#<%=ddlMaxJabatan.ClientID%>')
                                    .append($('<option></option>')
                                    .attr('value', itemJabatan.KdJabatan)
                                    .html(space + itemJabatan.KdJabatan + '. ' + itemJabatan.NmJabatan));
                    if (itemJabatan.ChildNode) {
                        BuildJabatanChildNode(itemJabatan.ChildNode, depth + 1);
                    }
                });
            }

            function AddUnit() {
                $('#<%=pnlAddEditUnit.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnAddUnit.ClientID%>")');
                
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdUnitParent = $('#<%=hidKdUnit.ClientID%>').val();
                var nmUnitParent = $('#<%=hidNamaUnit.ClientID%>').val();

                BuildJabatanDropdownTree(kdDivisi);
                
                $('#<%=hidMaxJabatan.ClientID%>').val(0);
                
                $('#<%=pnlAddEditUnit.ClientID%>').parent().parent().find('.modal-title').html('Add New Unit');
                $find('popUpAddEditUnit').show();
                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    $('#<%=hidParentKdUnit.ClientID%>').val(0);
                }
                else {
                    nmUnitParent = nmUnitParent.substr(nmUnitParent.indexOf(kdUnitParent) + kdUnitParent.length + 2);
                    $('#<%=hidParentKdUnit.ClientID%>').val(kdUnitParent);
                }
                $('#<%=txtParentNmUnit.ClientID%>').val(nmUnitParent);
                $('#<%=txtNamaUnit.ClientID%>').val('');
                $('#<%=btnAddUnit.ClientID%>').show();
                $('#<%=btnEditUnit.ClientID%>').hide();
                $('#<%=ddlMaxJabatan.ClientID%>').val('0');

                return false;
            }

            function EditUnit() {
                $('#<%=pnlAddEditUnit.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnEditUnit.ClientID%>")');
                
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdUnit = $('#<%=hidKdUnit.ClientID%>').val();

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This node could not be edited');
                    return false;
                }

                BuildJabatanDropdownTree(kdDivisi);
                
                $('#<%=pnlAddEditUnit.ClientID%>').parent().parent().find('.modal-title').html('Edit Unit');
                $.ajax({
                    url: 'Unit.aspx/GetUnitData',
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        kdDivisi: 1,
                        kdUnit: kdUnit
                    }),
                    success: function(data) {
                        var result = data.d;
                        $('#<%=hidParentKdUnit.ClientID%>').val(result.ParentKdUnit);
                        $('#<%=hidKdUnit.ClientID%>').val(result.KdUnit);
                        $('#<%=txtNamaUnit.ClientID%>').val(result.NmUnit);
                        $('#<%=ddlMaxJabatan.ClientID%>').val(result.MaxKdJabatan);
                        $('#<%=hidMaxJabatan.ClientID%>').val(result.MaxKdJabatan);
                        if (result.ParentNmUnit) {
                            $('#<%=txtParentNmUnit.ClientID%>').val(result.ParentNmUnit);
                        }
                        else {
                            $('#<%=txtParentNmUnit.ClientID%>').val(result.NmDivisi);
                        }
                        $find('popUpAddEditUnit').show();
                        $('#<%=btnAddUnit.ClientID%>').hide();
                        $('#<%=btnEditUnit.ClientID%>').show();
                    }
                });

                return false;
            }

            function DeleteUnit() {
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdUnit = $('#<%=hidKdUnit.ClientID%>').val();
                var nmUnit = $('#<%=hidNamaUnit.ClientID%>').val();

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This node could not be deleted');
                    return false;
                }

                BuildJabatanDropdownTree(kdDivisi);

                nmUnit = nmUnit.substr(nmUnit.indexOf(kdUnit) + kdUnit.length + 2);
                
                $find('popUpConfirm').show();
                $('#<%=hidKdUnit.ClientID%>').val(kdUnit);
                $('#<%=lblConfirm.ClientID%>').html('Are you sure want to delete this unit: <b>"' + nmUnit + '"</b>?<br/>Unit will be deleted, including the sub-unit(s)');
                return false;
            }

            function onAddContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdUnitParent = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmUnitParent = $(clientID.getSelectedItem()).text();

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaUnit.ClientID%>').val(nmUnitParent);
                $('#<%=hidKdUnit.ClientID%>').val(kdUnitParent);
                
                return AddUnit();
            }

            function onEditContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdJabatan = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                
                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }
                
                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidKdUnit.ClientID%>').val(kdJabatan);
                
                return EditUnit();
            }

            function onDeleteContextMenu(clientID) {                
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdUnit = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmUnit = $(clientID.getSelectedItem()).text();

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaUnit.ClientID%>').val(nmUnit);
                $('#<%=hidKdUnit.ClientID%>').val(kdUnit);
                    
                return DeleteUnit();
            }

            function nodeSelectHandler(elem) {
                var kdDivisi = $($(elem).parents('li:last')).attr('treeNodeValue');
                var kdUnit = elem.parentNode.getAttribute("treeNodeValue");
                var nmUnit = $(elem).text();

                if ($(elem).parents('li')[0] == $($(elem).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaUnit.ClientID%>').val(nmUnit);
                $('#<%=hidKdUnit.ClientID%>').val(kdUnit);
                $('#<%=btnAddUnitBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnEditUnitBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnDeleteUnitBottom.ClientID%>').removeAttr('disabled');
            }
            
            function pageLoad() {
                $('.defaultDropdownIcon').each(function(index, item) {
                    $(item).attr('src', '../css/asdropdowntreeview/images/windropdown.gif');
                });

                $('.defaultDropdownTextContainer').each(function(index, item) {
                    $(item).addClass('form-control');
                    $(item).css('width', '100%');
                });
            }
            
            pageLoad();
            Sys.Application.add_load(pageLoad);
        </script>

    </asp:PlaceHolder>
</asp:Content>
