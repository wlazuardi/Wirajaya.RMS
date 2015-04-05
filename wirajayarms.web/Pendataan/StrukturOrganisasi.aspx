﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StrukturOrganisasi.aspx.cs" Inherits="WirajayaRMS.Web.Pendataan.StrukturOrganisasi" Title="Organizational Structure" EnableEventValidation="false" %>
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
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <uc:PopUpControl ID="popUpAddEditSO" runat="server" HeaderText="Add New Organizational Structure"
        BehaviorID="popUpAddEditSO" PopupControlID="pnlAddEditSO" />
        
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" 
        BehaviorID="popUpConfirm" PopupControlID="pnlConfirm" Type="Warning"/>
        
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Organizational Structure List</h3>       
                        <div class="box-tools pull-right">
                        </div>                 
                    </div>
                    <div class="box-body">   
                        <div class="alert alert-info">
                            <i class="fa fa-info"></i>You can add, edit, or delete organizational structure by right-clicking on the organizational structure list
                        </div>
                        <div class="row">
                            <div class="col-sm-2 pull-right">
                            </div>
                            <div class="col-sm-10">
                                <gcc:ASTreeView ID="tvStrukturOrganisasi" runat="server" 
                                    EnableRoot="false"
                                    EnableCheckbox="false" EnableNodeIcon="false"
                                    EnableContextMenuAdd="false" EnableContextMenuEdit="false" 
                                    EnableContextMenuDelete="false" OnNodeSelectedScript="nodeSelectHandler(elem);"
                                    EnableDragDrop="false"
                                    BasePath="~/css/astreeview/" />
                            </div>
                        </div>
                        
                        <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true"/>
                        
                        <%--Hidden field untuk keperluan Add, Edit, Delete--%>
                        <asp:HiddenField ID="hidIsRoot" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidKdUnit" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidKdDivisi" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidParentKdSO" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidKdSO" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidNamaSO" runat="server"></asp:HiddenField>
                    </div>
                    <div class="box-footer">
                        <div class="row">
                            <div class="pull-right" style="width:230px">
                                <asp:Button ID="btnAddSOBottom" runat="server" Text="Tambah" CssClass="btn btn-primary" disabled="disabled" OnClientClick="return AddSO();"/>
                                <asp:Button ID="btnEditSOBottom" runat="server" Text="Ubah" CssClass="btn btn-primary" disabled="disabled" OnClientClick="return EditSO();"/>
                                <asp:Button ID="btnDeleteSOBottom" runat="server" Text="Hapus" CssClass="btn btn-danger" disabled="disabled" OnClientClick="return DeleteSO();"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <%--Pop up untuk add/edit--%>
    <asp:Panel ID="pnlAddEditSO" runat="server">
        <div class="modal-body">
            <div class="form">
                <div class="form-group">
                    <label class="control-label">
                        Parent</label>
                    <asp:TextBox ID="txtParentNmSO" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Organizational Structure Name</label>
                    <asp:TextBox ID="txtNamaSO" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNamaSO" runat="server" ControlToValidate="txtNamaSO"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditSO"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Unit</label>
                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" OnChange="OnUnitChanged();"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqUnit" runat="server" ControlToValidate="ddlUnit" InitialValue="0"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditSO"></asp:RequiredFieldValidator>                    
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Number of Employees</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtJmlKaryawan" runat="server" CssClass="form-control numeric"></asp:TextBox>
                        <span class="input-group-addon">person</span>
                    </div>
                    <asp:RequiredFieldValidator ID="reqJmlKaryawan" runat="server" ControlToValidate="txtJmlKaryawan"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditSO">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regJmlKaryawan" runat="server" ControlToValidate="txtJmlKaryawan"
                        ValidationExpression="^[0-9\,]+$" ErrorMessage="Please provide numeric value" Display="Dynamic" ValidationGroup="AddEditSO">
                    </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Maximum Number of Employees</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtMaxJmlKaryawan" runat="server" CssClass="form-control numeric"></asp:TextBox>
                        <span class="input-group-addon">person</span>
                    </div>
                    <asp:RequiredFieldValidator ID="reqMaxJmlKaryawan" runat="server" ControlToValidate="txtMaxJmlKaryawan"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditSO">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regMaxJmlKaryawan" runat="server" ControlToValidate="txtMaxJmlKaryawan"
                        ValidationExpression="^[0-9\,]+$" ErrorMessage="Please provide numeric value" Display="Dynamic" ValidationGroup="AddEditSO">
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="cmpJumlahKaryawan" runat="server" ErrorMessage="Maximum number of employees must be >= number of employees" 
                        Type="Integer" ValidationGroup="AddEditSO" ControlToValidate="txtMaxJmlKaryawan" 
                        ControlToCompare="txtJmlKaryawan" Operator="GreaterThanEqual">
                    </asp:CompareValidator>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnAddSO" runat="server" CssClass="btn btn-primary" Text="Add"
                OnClick="btnAddSO_Click" ValidationGroup="AddEditSO" />
            <asp:Button ID="btnEditSO" runat="server" CssClass="btn btn-primary" Text="Save"
                OnClick="btnEditSO_Click" ValidationGroup="AddEditSO"/>
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

            function OnUnitChanged() {
                var kdUnit = $('#<%=ddlUnit.ClientID%>').val();
                $('#<%=hidKdUnit.ClientID%>').val(kdUnit);
            }

            function BuiltUnitDropdownTree(kdDivisi) {
                $('#<%=ddlUnit.ClientID%>').empty();
                $('#<%=ddlUnit.ClientID%>')
                                .append($('<option></option>')
                                .attr('value', '0')
                                .text('-- Select Unit --'));
                $.ajax({
                    url: 'StrukturOrganisasi.aspx/GetUnitList',
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
                        $.each(result, function(index, unit) {
                            $('#<%=ddlUnit.ClientID%>')
                                .append($('<option></option>')
                                .attr('value', unit.KdUnit)
                                .html(unit.KdUnit + '. ' + unit.NmUnit));
                            if (unit.ChildNode) {
                                BuildUnitChildNode(unit.ChildNode, 1);
                            }
                        });
                    },
                    complete: function() {
                        //Hide the processing div
                        $('#ctl00_upLoading').hide();
                    }
                });
            }

            function BuildUnitChildNode(unit, depth) {
                var space = "";
                for (var i = 0; i < depth; i++) {
                    space += "&nbsp;&nbsp;&nbsp;";
                }

                $.each(unit, function(index, itemUnit) {
                    $('#<%=ddlUnit.ClientID%>')
                                    .append($('<option></option>')
                                    .attr('value', itemUnit.KdUnit)
                                    .html(space + itemUnit.KdUnit + '. ' + itemUnit.NmUnit));
                    if (itemUnit.ChildNode) {
                        BuildUnitChildNode(itemUnit.ChildNode, depth + 1);
                    }
                });
            }

            function AddSO() {
                $('#<%=pnlAddEditSO.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnAddSO.ClientID%>")');
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdSOParent = $('#<%=hidKdSO.ClientID%>').val();
                var nmSOParent = $('#<%=hidNamaSO.ClientID%>').val();

                BuiltUnitDropdownTree(kdDivisi);

                $('#<%=pnlAddEditSO.ClientID%>').parent().parent().find('.modal-title').html('Add New Organizational Structure');
                $find('popUpAddEditSO').show();
                
                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    $('#<%=hidParentKdSO.ClientID%>').val(0);
                }
                else {
                    nmSOParent = nmSOParent.substr(nmSOParent.indexOf(kdSOParent) + kdSOParent.length + 2);
                    $('#<%=hidParentKdSO.ClientID%>').val(kdSOParent);
                }
                
                $('#<%=hidKdUnit.ClientID%>').val(0);
                $('#<%=txtParentNmSO.ClientID%>').val(nmSOParent);
                $('#<%=txtNamaSO.ClientID%>').val('');
                $('#<%=ddlUnit.ClientID%>').val(0);
                $('#<%=txtJmlKaryawan.ClientID%>').val('');
                $('#<%=txtMaxJmlKaryawan.ClientID%>').val('');
                $('#<%=btnAddSO.ClientID%>').show();
                $('#<%=btnEditSO.ClientID%>').hide();

                return false;
            }

            function EditSO() {
                $('#<%=pnlAddEditSO.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnEditSO.ClientID%>")');
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdSO = $('#<%=hidKdSO.ClientID%>').val();

                BuiltUnitDropdownTree(kdDivisi);

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This not could not be edited');
                    return false;
                }

                $('#<%=pnlAddEditSO.ClientID%>').parent().parent().find('.modal-title').html('Edit Organizational Structure');
                $.ajax({
                    url: 'StrukturOrganisasi.aspx/GetStrukturOrganisasiData',
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        kdDivisi: kdDivisi,
                        kdSO: kdSO
                    }),
                    success: function(data) {
                        var result = data.d;
                        $('#<%=hidParentKdSO.ClientID%>').val(result.ParentKdSO);
                        $('#<%=hidKdSO.ClientID%>').val(result.KdSO);
                        $('#<%=txtNamaSO.ClientID%>').val(result.NmStrukturOrganisasi);
                        if (result.ParentNmStrukturOrganisasi) {
                            $('#<%=txtParentNmSO.ClientID%>').val(result.ParentNmStrukturOrganisasi);
                        }
                        else {
                            $('#<%=txtParentNmSO.ClientID%>').val(result.NmDivisi);
                        }
                        $('#<%=ddlUnit.ClientID%>').val(result.KdUnit);
                        $('#<%=hidKdUnit.ClientID%>').val(result.KdUnit);
                        $('#<%=txtJmlKaryawan.ClientID%>').val(result.JmlKaryawan);
                        $('#<%=txtMaxJmlKaryawan.ClientID%>').val(result.MaxJmlKaryawan);
                        $find('popUpAddEditSO').show();
                        $('#<%=btnAddSO.ClientID%>').hide();
                        $('#<%=btnEditSO.ClientID%>').show();
                    }
                });

                return false;
            }

            function DeleteSO() {
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdSO = $('#<%=hidKdSO.ClientID%>').val();
                var nmSO = $('#<%=hidNamaSO.ClientID%>').val();

                BuiltUnitDropdownTree(kdDivisi);

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This node could not be deleted');
                    return false;
                }
                
                nmSO = nmSO.substr(nmSO.indexOf(kdSO) + kdSO.length + 2);
                
                $find('popUpConfirm').show();
                $('#<%=hidKdSO.ClientID%>').val(kdSO);
                $('#<%=lblConfirm.ClientID%>').html('Are you sure want to delete this organizational structure: <b>"' + nmSO + '"</b>?<br/>Organizational structure will be deleted, including the sub-organizational structure(s)');
                return false;
            }

            function onAddContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdSOParent = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmSOParent = $(clientID.getSelectedItem()).text();

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaSO.ClientID%>').val(nmSOParent);
                $('#<%=hidKdSO.ClientID%>').val(kdSOParent);
                
                return AddSO();
            }

            function onEditContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdSO = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidKdSO.ClientID%>').val(kdSO);
                return EditSO();
            }

            function onDeleteContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdSO = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmSO = $(clientID.getSelectedItem()).text();
                
                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaSO.ClientID%>').val(nmSO);
                $('#<%=hidKdSO.ClientID%>').val(kdSO);
                
                return DeleteSO();
            }

            function nodeSelectHandler(elem) {
                if ($(elem).parents('li')[0] == $($(elem).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                var kdDivisi = $($(elem).parents('li:last')).attr('treeNodeValue');
                var kdSO = elem.parentNode.getAttribute("treeNodeValue");
                var nmSO = $(elem).text();

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaSO.ClientID%>').val(nmSO);
                $('#<%=hidKdSO.ClientID%>').val(kdSO);
                $('#<%=btnAddSOBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnEditSOBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnDeleteSOBottom.ClientID%>').removeAttr('disabled');
            }
            
            function pageLoad() {
                $('input.numeric').keypress(function(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;
                    return true;
                });
            }
            pageLoad();
            Sys.Application.add_load(pageLoad);
        </script>

    </asp:PlaceHolder>
</asp:Content>
