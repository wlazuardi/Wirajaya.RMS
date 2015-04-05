<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Jabatan.aspx.cs" 
    Inherits="WirajayaRMS.Web.Pendataan.Jabatan" Title="Position" %>
    
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="gcc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<%@ Register src="../UserControl/AlertControl.ascx" tagname="AlertControl" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/astreeview/astreeview_packed.js"></script>

    <link rel="Stylesheet" href="<%# Request.ApplicationPath.TrimEnd('/')%>/css/astreeview/astreeview.css" />

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/contextmenu/contextmenu_packed.js"></script>
    
    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/input-mask/jquery.inputmask.js"></script>
    
    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/input-mask/jquery.inputmask.numeric.extensions.js"></script>
    
    <link rel="Stylesheet" href="<%# Request.ApplicationPath.TrimEnd('/')%>/css/contextmenu/contextmenu.css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <uc:PopUpControl ID="popUpAddEditJabatan" runat="server" HeaderText="Add New Position"
        BehaviorID="popUpAddEditJabatan" PopupControlID="pnlAddEditJabatan" />
        
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" 
        BehaviorID="popUpConfirm" PopupControlID="pnlConfirm" Type="Warning"/>
        
    <asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header">
                        <h3 class="box-title">Position List</h3>       
                        <div class="box-tools pull-right">
                            <%--<asp:DropDownList ID="ddlDivisi" runat="server" CssClass="btn btn-sm btn-default pull-right" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisi_SelectedIndexChanged">                                
                            </asp:DropDownList>  --%>
                        </div>                 
                    </div>
                    <div class="box-body">   
                        <div class="alert alert-info">
                            <i class="fa fa-info"></i>You can add, edit, or delete the position by right-clicking on the position list
                        </div>
                        <div class="row">
                            <div class="col-sm-2 pull-right">
                            </div>
                            <div class="col-sm-10">
                                <gcc:ASTreeView ID="tvJabatan" runat="server" EnableCheckbox="false" EnableNodeIcon="false" EnableRoot="false"
                                    EnableContextMenuAdd="false" EnableContextMenuEdit="false" EnableContextMenuDelete="false" OnNodeSelectedScript="nodeSelectHandler(elem);"
                                    EnableDragDrop="false"
                                    BasePath="~/css/astreeview/" />
                            </div>
                        </div>
                        
                        <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true"/>
                        
                        <%--Hidden field untuk keperluan Add, Edit, Delete--%>
                        <asp:HiddenField ID="hidIsRoot" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidKdDivisi" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidParentKdJabatan" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidKdJabatan" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hidNamaJabatan" runat="server"></asp:HiddenField>
                    </div>
                    <div class="box-footer">
                        <div class="row">
                            <div class="pull-right" style="width:230px">
                                <asp:Button ID="btnAddJabatanBottom" runat="server" Text="Add" CssClass="btn btn-primary" disabled="disabled" OnClientClick="return AddJabatan();"/>
                                <asp:Button ID="btnEditJabatanBottom" runat="server" Text="Edit" CssClass="btn btn-primary" disabled="disabled" OnClientClick="return EditJabatan();"/>
                                <asp:Button ID="btnDeleteJabatanBottom" runat="server" Text="Delete" CssClass="btn btn-danger" disabled="disabled" OnClientClick="return DeleteJabatan();"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <%--Pop up untuk add/edit--%>
    <asp:Panel ID="pnlAddEditJabatan" runat="server">
        <div class="modal-body">
            <div class="form">
                <div class="form-group">
                    <label class="control-label">
                        Parent</label>
                    <asp:TextBox ID="txtParentNmJabatan" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Position Name</label>
                    <asp:TextBox ID="txtNamaJabatan" runat="server" CssClass="form-control" Placeholder="e.g., Manager"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqNamaJabatan" runat="server" ControlToValidate="txtNamaJabatan" 
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditJabatan"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Minimum Salary</label>
                    <div class="input-group">
                        <span class="input-group-addon">Rp</span>
                        <asp:TextBox ID="txtMinSalary" runat="server" CssClass="form-control decimal"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="reqMinSalary" runat="server" ControlToValidate="txtMinSalary"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditJabatan"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regMinSalary" runat="server" ControlToValidate="txtMinSalary"
                        ValidationExpression="^[0-9\,]+$" ErrorMessage="Please provide numeric value" Display="Dynamic" ValidationGroup="AddEditJabatan"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Maximum Salary</label>
                    <div class="input-group">
                        <span class="input-group-addon">Rp</span>
                        <asp:TextBox ID="txtMaxSalary" runat="server" CssClass="form-control decimal"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="reqMaxSalary" runat="server" ControlToValidate="txtMaxSalary"
                        ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddEditJabatan"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regMaxSalary" runat="server" ControlToValidate="txtMaxSalary"
                        ValidationExpression="^[0-9\,]+$" ErrorMessage="Please provide numeric value" Display="Dynamic" ValidationGroup="AddEditJabatan"></asp:RegularExpressionValidator>                    
                    <asp:CompareValidator runat="server" ErrorMessage="Maximum salary must be >= minimum salary" Type="Currency" 
                        ValidationGroup="AddEditJabatan" ControlToValidate="txtMaxSalary" ControlToCompare="txtMinSalary" 
                        Operator="GreaterThanEqual" Display="Dynamic">
                    </asp:CompareValidator>
                </div>
                <div class="form-group">
                    <label class="control-label">
                        Facility (optional)</label>
                    <asp:TextBox ID="txtFasilitas" runat="server" Placeholder="e.g., Health insurance, Allowance, etc." 
                        CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnAddJabatan" runat="server" CssClass="btn btn-primary" Text="Add"
                OnClick="btnAddJabatan_Click" ValidationGroup="AddEditJabatan" />
            <asp:Button ID="btnEditJabatan" runat="server" CssClass="btn btn-primary" Text="Save"
                OnClick="btnEditJabatan_Click" ValidationGroup="AddEditJabatan"/>
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
    
    <asp:PlaceHolder runat="server">
        <script type="text/javascript">
            function AddJabatan() {
                $('#<%=pnlAddEditJabatan.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnAddJabatan.ClientID%>")');
                
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdJabatanParent = $('#<%=hidKdJabatan.ClientID%>').val();
                var nmJabatanParent = $('#<%=hidNamaJabatan.ClientID%>').val();
                
                $('#<%=pnlAddEditJabatan.ClientID%>').parent().parent().find('.modal-title').html('Add New Position');
                $find('popUpAddEditJabatan').show();
                
                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    $('#<%=hidParentKdJabatan.ClientID%>').val(0);
                }
                else {
                    nmJabatanParent = nmJabatanParent.substr(nmJabatanParent.indexOf(kdJabatanParent) + kdJabatanParent.length + 2);
                    $('#<%=hidParentKdJabatan.ClientID%>').val(kdJabatanParent);
                }
                
                $('#<%=txtParentNmJabatan.ClientID%>').val(nmJabatanParent);
                $('#<%=txtNamaJabatan.ClientID%>').val('');
                $('#<%=txtMinSalary.ClientID%>').val('');
                $('#<%=txtMaxSalary.ClientID%>').val('');
                $('#<%=txtFasilitas.ClientID%>').val('');
                $('#<%=btnAddJabatan.ClientID%>').show();
                $('#<%=btnEditJabatan.ClientID%>').hide();

                return false;
            }

            function EditJabatan() {
                $('#<%=pnlAddEditJabatan.ClientID%>').attr('onkeypress', 'javascript:return WebForm_FireDefaultButton(event, "<%=btnEditJabatan.ClientID%>")');
                
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdJabatan = $('#<%=hidKdJabatan.ClientID%>').val();

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This node could not be edited');
                    return false;
                }
                
                $('#<%=pnlAddEditJabatan.ClientID%>').parent().parent().find('.modal-title').html('Edit Position');
                $.ajax({
                    url: 'Jabatan.aspx/GetJabatanData',
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        kdDivisi: kdDivisi,
                        kdJabatan: kdJabatan
                    }),
                    success: function(data) {
                        var result = data.d;
                        $('#<%=hidParentKdJabatan.ClientID%>').val(result.ParentKdJabatan);
                        $('#<%=hidKdJabatan.ClientID%>').val(result.KdJabatan);
                        $('#<%=txtNamaJabatan.ClientID%>').val(result.NmJabatan);
                        if (result.ParentNmJabatan) {
                            $('#<%=txtParentNmJabatan.ClientID%>').val(result.ParentNmJabatan);
                        }
                        else {
                            $('#<%=txtParentNmJabatan.ClientID%>').val(result.NmDivisi);
                        }
                        $find('popUpAddEditJabatan').show();
                        $('#<%=txtFasilitas.ClientID%>').val(result.Fasilitas);
                        $('#<%=txtMinSalary.ClientID%>').val(result.MinSalary);
                        $('#<%=txtMinSalary.ClientID%>').keyup();
                        $('#<%=txtMaxSalary.ClientID%>').val(result.MaxSalary);
                        $('#<%=txtMaxSalary.ClientID%>').keyup();
                        $('#<%=btnAddJabatan.ClientID%>').hide();
                        $('#<%=btnEditJabatan.ClientID%>').show();
                    }
                });

                return false;
            }

            function DeleteJabatan() {
                var kdDivisi = $('#<%=hidKdDivisi.ClientID%>').val();
                var kdJabatan = $('#<%=hidKdJabatan.ClientID%>').val();
                var nmJabatan = $('#<%=hidNamaJabatan.ClientID%>').val();

                if ($('#<%=hidIsRoot.ClientID%>').val() == "true") {
                    alert('This node could not be deleted');
                    return false;
                }

                nmJabatan = nmJabatan.substr(nmJabatan.indexOf(kdJabatan) + kdJabatan.length + 2);
                
                $find('popUpConfirm').show();
                $('#<%=hidKdJabatan.ClientID%>').val(kdJabatan);
                $('#<%=lblConfirm.ClientID%>').html('Are you sure want to delete this position: <b>"' + nmJabatan + '"</b>?<br/>Position will be deleted, including the sub-position(s)');
                return false;
            }

            function onAddContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdJabatanParent = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmJabatanParent = $(clientID.getSelectedItem()).text();

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaJabatan.ClientID%>').val(nmJabatanParent);
                $('#<%=hidKdJabatan.ClientID%>').val(kdJabatanParent);
                
                return AddJabatan();
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
                $('#<%=hidKdJabatan.ClientID%>').val(kdJabatan);
                
                return EditJabatan();
            }

            function onDeleteContextMenu(clientID) {
                var kdDivisi = $($(clientID.getSelectedItem()).parents('li:last')).attr('treeNodeValue');
                var kdJabatan = clientID.getSelectedItem().parentNode.getAttribute('treeNodeValue');
                var nmJabatan = $(clientID.getSelectedItem()).text();

                if ($(clientID.getSelectedItem()).parents('li')[0] == $($(clientID.getSelectedItem()).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }

                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaJabatan.ClientID%>').val(nmJabatan);
                $('#<%=hidKdJabatan.ClientID%>').val(kdJabatan);
                
                return DeleteJabatan();
            }

            function nodeSelectHandler(elem) {
                if ($(elem).parents('li')[0] == $($(elem).parents('li:last'))[0]) {
                    $('#<%=hidIsRoot.ClientID%>').val(true);
                }
                else {
                    $('#<%=hidIsRoot.ClientID%>').val(false);
                }
                
                var kdDivisi = $($(elem).parents('li:last')).attr('treeNodeValue');
                var kdJabatan = elem.parentNode.getAttribute("treeNodeValue");
                var nmJabatan = $(elem).text();
                
                $('#<%=hidKdDivisi.ClientID%>').val(kdDivisi);
                $('#<%=hidNamaJabatan.ClientID%>').val(nmJabatan);
                $('#<%=hidKdJabatan.ClientID%>').val(kdJabatan);
                $('#<%=btnAddJabatanBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnEditJabatanBottom.ClientID%>').removeAttr('disabled');
                $('#<%=btnDeleteJabatanBottom.ClientID%>').removeAttr('disabled');
            }

            function pageLoad() {
                $('input.decimal').inputmask('decimal', { radixPoint: '.', autoGroup: true, groupSeparator: ',', groupSize: 3, allowMinus: false });
            }
            pageLoad();
            Sys.Application.add_load(pageLoad);
        </script>

    </asp:PlaceHolder>
</asp:Content>
