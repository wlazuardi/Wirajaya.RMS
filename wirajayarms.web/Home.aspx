<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="WirajayaRMS.Web.Home" Title="Home Page" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register Src="UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<%@ Register src="UserControl/AlertControl.ascx" tagname="AlertControl" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                Dashboard
                            </h3>                    
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    Welcome to Wirajaya Recruitment Management System
                                </div>
                            </div>                    
                        </div>
                    </div>
                    <!-- Custom Tabs -->
                    <div id="requestTab" class="nav-tabs-custom">                        
                        <ul class="nav nav-tabs">                            
                            <li class="active"><a href="#tabCurrent" data-toggle="tab" onclick="changeRequestTab('#tabCurrent');">Request To Approve</a></li>
                            <li><a href="#tabHistory" data-toggle="tab" onclick="changeRequestTab('#tabHistory');">Request History</a></li>
                        </ul>
                        <div class="tab-content">
                            <asp:HiddenField ID="hidRequestActiveTab" runat="server" Value="tabCurrent" />
                            <div class="tab-pane active" id="tabCurrent">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">
                                            Division
                                        </label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlDivisi" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDivisi_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="min-height:250px;">
                                    <div class="col-sm-12">
                                        <asp:Repeater ID="rptRecruitment" runat="server" OnItemDataBound="rptRecruitment_ItemDataBound">
                                            <HeaderTemplate>
                                                <table class="table table-striped" id="tblRequest">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                No
                                                            </th>
                                                            <th>
                                                                No. Request
                                                            </th>
                                                            <th>
                                                                Organizational Structure
                                                            </th>
                                                            <th>
                                                                Position
                                                            </th>
                                                            <th>
                                                                #Persons Needed
                                                            </th>
                                                            <th>
                                                                Reason
                                                            </th>
                                                            <th>
                                                                Status
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
                                                        <asp:Literal ID="litNo" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="linkNoRequest" runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litStrukturOrganisasi" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litJabatan" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litJmlOrang" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litAlasan" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" Width="35" CssClass="btn btn-sm btn-primary">
                                                            <span class="fa fa-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:HyperLink ID="btnPrint"  ToolTip="Print" runat="server" Width="35" CssClass="btn btn-sm btn-info">
                                                            <span class="fa fa-print"></span>
                                                        </asp:HyperLink>
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
                                <div class="row" style="margin-top:20px;">
                                    <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true"/>
                                </div>
                                <div class="row" style="margin-top:20px;">
                                    <div class="col-sm-12">
                                        <asp:LinkButton ID="lbAddNewRecruitment" runat="server" CssClass="btn btn-primary pull-right">
                                            <i class="fa fa-plus"></i>&nbsp;
                                            Create New Request
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div><!-- /.tab-pane -->
                            <div class="tab-pane" id="tabHistory">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">
                                            Division
                                        </label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlDivisiHistory" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDivisiHistory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="min-height:320px;">
                                    <div class="col-sm-12">
                                        <asp:Repeater ID="rptRecruitmentHistory" runat="server" OnItemDataBound="rptRecruitmentHistory_ItemDataBound">
                                            <HeaderTemplate>
                                                <table class="table table-striped" id="tblRequestHistory">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                No
                                                            </th>
                                                            <th>
                                                                No. Request
                                                            </th>
                                                            <th>
                                                                Organizational Structure
                                                            </th>
                                                            <th>
                                                                Position
                                                            </th>
                                                            <th>
                                                                # Persons Needed
                                                            </th>
                                                            <th>
                                                                Reason
                                                            </th>
                                                            <th>
                                                                Status
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
                                                        <asp:Literal ID="litNo" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="linkNoRequest" runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litStrukturOrganisasi" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litJabatan" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litJmlOrang" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litAlasan" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" Width="35" CssClass="btn btn-sm btn-primary">
                                                            <span class="fa fa-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:HyperLink ID="btnPrint"  ToolTip="Print" runat="server" Width="35" CssClass="btn btn-sm btn-info">
                                                            <span class="fa fa-print"></span>
                                                        </asp:HyperLink>
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
                            </div><!-- /.tab-pane -->
                        </div><!-- /.tab-content -->
                    </div><!-- nav-tabs-custom -->
                </div>
            </div>
            <script type="text/javascript">
                function changeRequestTab(tabID) {
                    $('#<%=hidRequestActiveTab.ClientID%>').val(tabID);
                }

                function pageLoad() {
                    var tab = $('#<%=hidRequestActiveTab.ClientID%>').val();
                    $('#requestTab a[href="' + tab + '"]').tab('show');

                    var tblRequest = $('#tblRequest').dataTable({
                        "bDestroy": true,
                        "bPaginate": true,
                        "bLengthChange": true,
                        "bStateSave": true,
                        "bFilter": false,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false
                    });

                    var tblRequestHistory = $('#tblRequestHistory').dataTable({
                        "bDestroy": true,
                        "bPaginate": true,
                        "bLengthChange": true,
                        "bStateSave": true,
                        "bFilter": false,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false
                    });
                }
                
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function(s, e) {
                    pageLoad();
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
