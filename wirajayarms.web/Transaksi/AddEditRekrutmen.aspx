﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditRekrutmen.aspx.cs"
    Inherits="WirajayaRMS.Web.Transaksi.AddEditRekrutmen" Title="Add/Edit Request" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../UserControl/AlertControl.ascx" TagName="AlertControl" TagPrefix="uc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/input-mask/jquery.inputmask.js"></script>

    <script type="text/javascript" src="<%# Request.ApplicationPath.TrimEnd('/')%>/js/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>

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
                                Add/Edit Request</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>
                        <div class="box-body form-horizontal">
                            <div id="divNoRequest" runat="server" class="form-group">
                                <label class="control-label col-sm-2">
                                    No. Request
                                </label>
                                <div class="col-sm-9" style="margin-top: 5px;">
                                    <asp:Label ID="lblNoRequest" runat="server" Style="font-family: Courier New"></asp:Label>
                                    <%--<act:Rating ID="ratingTest" runat="server" MaxRating="5" 
                                        StarCssClass="fa" 
                                        WaitingStarCssClass="fa-spinner"
                                        FilledStarCssClass="fa-star"
                                        EmptyStarCssClass="fa-star-o">
                                    </act:Rating>--%>
                                </div>
                            </div>
                            <div id="divStatusRequest" runat="server" class="form-group">
                                <label class="control-label col-sm-2">
                                    Request Status
                                </label>
                                <div class="col-sm-9" style="margin-top: 5px;">
                                    <asp:Label ID="lblStatuRequest" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div id="divRequestCreator" runat="server" class="form-group">
                                <label class="control-label col-sm-2">
                                    Requested By
                                </label>
                                <div class="col-sm-9" style="margin-top: 5px;">
                                    <asp:Label ID="lblRequestCreator" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Division
                                </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlDivisi" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlDivisi_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqDivisi" InitialValue="0" ControlToValidate="ddlDivisi"
                                        ErrorMessage="Please select division" runat="server" ValidationGroup="AddEditRecruitment"
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="reqStrukturOrganisasi" InitialValue="0" ControlToValidate="ddlStrukturOrganisasi"
                                        ErrorMessage="Please select organizational structure" runat="server" ValidationGroup="AddEditRecruitment"
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div id="divJmlKaryawan" class="form-group" runat="server">
                                <div class="col-sm-2">
                                    &nbsp;
                                </div>
                                <label class="col-sm-2">
                                    Number of Current Employees
                                </label>
                                <div class="col-sm-1">
                                    <asp:Label ID="lblJmlKaryawan" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div id="divMaxJmlKaryawan" class="form-group" runat="server">
                                <div class="col-sm-2">
                                    &nbsp;
                                </div>
                                <label class="col-sm-2">
                                    Max Number of Employees
                                </label>
                                <div class="col-sm-1">
                                    <asp:Label ID="lblMaxJmlKaryawan" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Position
                                </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlJabatan" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlJabatan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqJabatan" InitialValue="0" ControlToValidate="ddlJabatan"
                                        ErrorMessage="Please select position" runat="server" ValidationGroup="AddEditRecruitment"
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div id="divFasilitas" class="form-group" runat="server">
                                <div class="col-sm-2">
                                    &nbsp;
                                </div>
                                <label class="col-sm-2">
                                    Facility
                                </label>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblFasilitas" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div id="divGaji" class="form-group" runat="server">
                                <div class="col-sm-2">
                                    &nbsp;
                                </div>
                                <label class="col-sm-2">
                                    Range of Salary
                                </label>
                                <div class="col-sm-8">
                                    <asp:Label ID="lblMinSalary" runat="server"></asp:Label>
                                    -
                                    <asp:Label ID="lblMaxSalary" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Numbers of Person Requested
                                </label>
                                <div class="col-sm-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtJmlKebutuhanKaryawan" runat="server" CssClass="form-control numeric"></asp:TextBox>
                                        <span class="input-group-addon">person(s)</span>
                                    </div>
                                </div>
                                <div class="col-sm-7">
                                    <asp:RequiredFieldValidator ControlToValidate="txtJmlKebutuhanKaryawan" ErrorMessage="Please fill the numbers of persons needed"
                                        runat="server" ValidationGroup="AddEditRecruitment">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Date Required
                                </label>
                                <div class="col-sm-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtTglButuh" runat="server" Enabled="true" CssClass="form-control calendar"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnShowCalendar" runat="server" CssClass="btn btn-info btn-flat">
                                                <i class="fa fa-calendar"></i>
                                            </asp:LinkButton>
                                        </span>
                                        <act:CalendarExtender ID="calTglButuh" runat="server" TargetControlID="txtTglButuh"
                                            PopupButtonID="btnShowCalendar" Format="dd/MM/yyyy">
                                        </act:CalendarExtender>
                                        <act:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTglButuh"
                                            PopupButtonID="txtTglButuh" Format="dd/MM/yyyy">
                                        </act:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-sm-7">
                                    <asp:RequiredFieldValidator ControlToValidate="txtTglButuh" ErrorMessage="Please fill the date required"
                                        runat="server" ValidationGroup="AddEditRecruitment" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="customCalendar" runat="server" Type="Date" Operator="DataTypeCheck"
                                        ControlToValidate="txtTglButuh" ErrorMessage="Invalid date format. Valid format: dd/MM/yyyy" ValidationGroup="AddEditRecruitment"
                                        Display="Dynamic" ClientValidationFunction="isDate">
                                    </asp:CustomValidator>

                                    <script type="text/javascript">
                                        function isDate(source, args) {
                                            var currVal = document.getElementById(source.controltovalidate).value;
                                            if (currVal == '') {
                                                args.IsValid = false;
                                                return false;
                                            }

                                            //Declare Regex  
                                            var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                                            var dtArray = currVal.match(rxDatePattern); // is format OK?

                                            if (dtArray == null) {
                                                args.IsValid = false;
                                                return false;
                                            }

                                            //Checks for dd/mm/yyyy format.
                                            dtDay = dtArray[1];
                                            dtMonth = dtArray[3];
                                            dtYear = dtArray[5];

                                            if (dtMonth < 1 || dtMonth > 12) {
                                                args.IsValid = false;
                                                return false;
                                            }
                                            else if (dtDay < 1 || dtDay > 31) {
                                                args.IsValid = false;
                                                return false;
                                            }
                                            else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31) {
                                                args.IsValid = false;
                                                return false;
                                            }
                                            else if (dtMonth == 2) {
                                                var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
                                                if (dtDay > 29 || (dtDay == 29 && !isleap)) {
                                                    args.IsValid = false;
                                                    return false;
                                                }
                                            }
                                            args.IsValid = true;
                                            return true;
                                        }
                                    </script>

                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Reason of Request
                                </label>
                                <div class="col-sm-3">
                                    <asp:RadioButtonList ID="rblAlasan" runat="server" CssClass="myCheckbox">
                                        <asp:ListItem Text="New Employee Allocation" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Employee Replacement" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-3">
                                    <asp:CustomValidator runat="server" ID="cvListMenuAccess" ClientValidationFunction="ValidateAlasan"
                                        ErrorMessage="Please select one of request reason" Display="Dynamic" ValidationGroup="AddEditRecruitment"></asp:CustomValidator>
                                </div>

                                <script type="text/javascript">
                                    function ValidateAlasan(source, args) {
                                        var rblAlasan = document.getElementById('<%=rblAlasan.ClientID %>');
                                        var alasan = rblAlasan.getElementsByTagName("input");
                                        for (var i = 0; i < alasan.length; i++) {
                                            if (alasan[i].checked) {
                                                args.IsValid = true;
                                                return;
                                            }
                                        }
                                        args.IsValid = false;
                                    }
                                </script>

                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Job Description List
                                </label>
                                <div class="col-sm-9">
                                    <asp:Repeater ID="rptJobDesc" runat="server" OnItemDataBound="rptJobDesc_ItemDataBound"
                                        OnItemCommand="rptJobDesc_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="tblJobDesc" class="table table-striped tblCollapse">
                                                <thead>
                                                    <tr>
                                                        <th width="20">
                                                            #
                                                        </th>
                                                        <th>
                                                            Job Description Point
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
                                                        <asp:Label ID="lblJobDesc" CssClass="jobDescItem" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtEditJobDesc" runat="server" TextMode="MultiLine" Visible="false"
                                                            Width="100%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqEditJobDesc" runat="server" ControlToValidate="txtEditJobDesc"
                                                            Display="Dynamic" ErrorMessage="Field must be filled">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" Width="35" CssClass="btn btn-sm btn-primary">
                                                            <span class="fa fa-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                                            <span class="fa fa-trash-o"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnSave" ToolTip="Save" runat="server" Width="35" CssClass="btn btn-sm btn-success"
                                                            Visible="false">
                                                            <span class="fa fa-save"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnCancel" ToolTip="Cancel" runat="server" Width="35" CssClass="btn btn-sm btn-warning"
                                                            Visible="false">
                                                            <span class="fa fa-times"></span>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <asp:CustomValidator runat="server" ID="cvJobDesc" ClientValidationFunction="ValidateJobDesc"
                                        ErrorMessage="Please provide job description" Display="Dynamic" ValidationGroup="AddEditRecruitment"></asp:CustomValidator>

                                    <script type="text/javascript">
                                        function ValidateJobDesc(source, args) {
                                            var isValid = false;
                                            $("#tblJobDesc .jobDescItem").each(function(index, val) {
                                                if ($(val).html()) {
                                                    isValid = true;
                                                }
                                            });
                                            args.IsValid = isValid;
                                        }
                                    </script>

                                </div>
                                <div class="col-sm-1">
                                    <asp:HiddenField ID="hidCollapseJobDesc" runat="server" Value="false"/>
                                    <a onclick="doCollapse('<%=hidCollapseJobDesc.ClientID%>');" class="btn btn-default btn-sm" data-widget="table-collapse" data-toggle="tooltip" title="" data-original-title="Show/Hide">
                                        <i class="fa fa-minus"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    &nbsp;
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtJobDesc" runat="server" TextMode="MultiLine" Placeholder="Add job description"
                                        Rows="2" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqJobDesc" runat="server" ControlToValidate="txtJobDesc"
                                        ErrorMessage="Field must be filled" ValidationGroup="AddJobDesc"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="btnAddJobDesc" runat="server" CssClass="btn btn-primary" ValidationGroup="AddJobDesc"
                                        OnClick="btnAddJobDesc_Click">
                                        <i class="fa fa-plus"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-11">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Qualification List
                                </label>
                                <div class="col-sm-9">
                                    <asp:Repeater ID="rptQualification" runat="server" OnItemDataBound="rptQualification_ItemDataBound"
                                        OnItemCommand="rptQualification_ItemCommand">
                                        <HeaderTemplate>
                                            <table id="tblQualification" class="table table-striped tblCollapse">
                                                <thead>
                                                    <tr>
                                                        <th width="20">
                                                            #
                                                        </th>
                                                        <th>
                                                            Qualification Point
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
                                                    <asp:Label ID="lblQualification" CssClass="qualificationItem" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtEditQualification" runat="server" TextMode="MultiLine" Visible="false"
                                                        Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqEditQualification" runat="server" ControlToValidate="txtEditQualification"
                                                        Display="Dynamic" ErrorMessage="Field must be filled">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" Width="35" CssClass="btn btn-sm btn-primary">
                                                        <span class="fa fa-pencil"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                                        <span class="fa fa-trash-o"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnSave" ToolTip="Save" runat="server" Width="35" CssClass="btn btn-sm btn-success"
                                                        Visible="false">
                                                        <span class="fa fa-save"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" ToolTip="Cancel" runat="server" Width="35" CssClass="btn btn-sm btn-warning"
                                                        Visible="false">
                                                        <span class="fa fa-times"></span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <asp:CustomValidator runat="server" ID="cvQualification" ClientValidationFunction="ValidateQualification"
                                        ErrorMessage="Please provide job qualification" Display="Dynamic" ValidationGroup="AddEditRecruitment"></asp:CustomValidator>

                                    <script type="text/javascript">
                                        function ValidateQualification(source, args) {
                                            var isValid = false;
                                            $("#tblQualification .qualificationItem").each(function(index, val) {
                                                if ($(val).html()) {
                                                    isValid = true;
                                                }
                                            });
                                            args.IsValid = isValid;
                                        }
                                    </script>

                                </div>
                                <div class="col-sm-1">
                                    <asp:HiddenField ID="hidCollapseQualification" runat="server" Value="false"/>
                                    <a onclick="doCollapse('<%=hidCollapseQualification.ClientID%>');" class="btn btn-default btn-sm" data-widget="table-collapse" data-toggle="tooltip" title="" data-original-title="Show/Hide">
                                        <i class="fa fa-minus"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    &nbsp;
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtQualification" runat="server" TextMode="MultiLine" Placeholder="Add qualification"
                                        Rows="2" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqQualification" runat="server" ControlToValidate="txtQualification"
                                        ErrorMessage="Field must be filled" ValidationGroup="AddQualification"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="btnAddQualification" runat="server" CssClass="btn btn-primary"
                                        ValidationGroup="AddQualification" OnClick="btnAddQualification_Click">
                                        <i class="fa fa-plus"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <asp:Panel ID="pnlDetailStatusRecruitment" Visible="false" CssClass="form-group"
                                runat="server">
                                <label class="control-label col-sm-2">
                                    Recruitment Status Detail
                                </label>
                                <div class="col-sm-9">
                                    <asp:RadioButtonList ID="rblDetailStatusRecruitment" runat="server" CssClass="myCheckbox">
                                        <asp:ListItem Text="5.1. Publishing job vacancy" Value="5.1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="5.2. Candidate selection" Value="5.2"></asp:ListItem>
                                        <asp:ListItem Text="5.3. Candidate interview" Value="5.3"></asp:ListItem>
                                        <asp:ListItem Text="5.4. Finalize" Value="5.4"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </asp:Panel>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Comments
                                </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="form-control"
                                        Placeholder="Add comments"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqComment" runat="server" ControlToValidate="txtComment"
                                        ErrorMessage="Please provide comments if you decline the request" ValidationGroup="DeclineRequest"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--<div class="form-group">
                                <label class="control-label col-sm-2">
                                    Kandidat
                                </label>
                                <div class="col-sm-9">
                                    
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:ValidationSummary ID="vsAddEditRecruitment" runat="server" ValidationGroup="AddEditRecruitment"
                                    DisplayMode="List" CssClass="col-sm-12" Style="text-align: right;" HeaderText="There's some invalid inputs, please re-check the data you input:" />
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:LinkButton ID="lbBack" runat="server"
                                        CssClass="btn btn-warning pull-left" Text="Kembali"></asp:LinkButton>
                                    <asp:LinkButton ID="btnDeclineRecruitment" ValidationGroup="DeclineRequest" runat="server"
                                        CssClass="btn btn-danger pull-right" ToolTip="Decline Request" OnClick="btnDeclineRecruitment_Click"
                                        Visible="false">
                                        <i class="glyphicon glyphicon-remove"></i>&nbsp;Decline Request
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnProcess" ValidationGroup="AddEditRecruitment" runat="server"
                                        CssClass="btn btn-success pull-right" ToolTip="Process Request" Style="margin-right: 10px;"
                                        OnClick="btnProcessRecruitment_Click">
                                        <i class="glyphicon glyphicon-play"></i>&nbsp;Proccess Request
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnSaveRecruitment" ValidationGroup="AddEditRecruitment" runat="server"
                                        CssClass="btn btn-primary pull-right" ToolTip="Save Changes" Style="margin-right: 10px;"
                                        OnClick="btnSaveRecruitment_Click">
                                        <i class="fa fa-save"></i>&nbsp;Save Changes
                                    </asp:LinkButton>
                                    <asp:HyperLink ID="btnPrint" runat="server"
                                        CssClass="btn btn-info pull-right" ToolTip="Print Report" Style="margin-right: 10px;" Visible="false">
                                        <i class="fa fa-print"></i>&nbsp;Print Report
                                    </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                        </div>
                    </div>
                    <asp:Panel ID="pnlKandidat" Visible="false" runat="server" CssClass="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                Candidate List
                            </h3>
                            <div class="box-tools pull-right">
                                <asp:HiddenField ID="hidCollapseDaftarKandidat" runat="server" Value="false" />
                                <a onclick="doCollapse('<%=hidCollapseDaftarKandidat.ClientID%>');" class="btn btn-default btn-sm"
                                    data-widget="collapse" data-toggle="tooltip" title="" data-original-title="Collapse">
                                    <i class="fa fa-minus"></i></a>
                            </div>
                        </div>
                        <div class="box-body form-horizontal">
                            <div id="divInfoKandidatTerpilih" runat="server" class="callout callout-info">
                                <h4>Information</h4>
                                <p>Candidate(s) marked by <b>green badge</b> is/are the selected candidate(s).</p>
                            </div>
                            <asp:Repeater ID="rptKandidat" runat="server" OnItemDataBound="rptKandidat_ItemDataBound"
                                OnItemCommand="rptKandidat_ItemCommand">
                                <HeaderTemplate>
                                    <div style="overflow-x:scroll;">
                                    <table class="table table-stripped" style="width:1500px;">
                                        <tr>
                                            <th>
                                                #
                                            </th>
                                            <th>
                                                Name
                                            </th>
                                            <th>
                                                Gender
                                            </th>
                                            <th>
                                                No. Identity
                                            </th>
                                            <th>
                                                No. Handphone
                                            </th>
                                            <th>
                                                E-mail
                                            </th>
                                            <th>
                                                Recommendation
                                            </th>
                                            <th>
                                                Download CV
                                            </th>
                                            <th style="width: 100px;">
                                                Qualification<br />
                                                Matching
                                            </th>
                                            <th style="width: 100px;">
                                                Interview<br />
                                                Records
                                            </th>
                                            <th width="90">
                                                Option
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="rowTblKandidat" runat="server">
                                        <td>
                                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNama" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNoIdentitas" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNoHandphone" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="hidRatingKandidat" runat="server" CssClass="ratingValue" Style="display:none;"/>
                                            <act:Rating ID="ratingKandidat" runat="server" MaxRating="5" StarCssClass="fa myRatingStar"
                                                WaitingStarCssClass="fa-spinner" FilledStarCssClass="fa-star" EmptyStarCssClass="fa-star-o"
                                                ReadOnly="true">
                                            </act:Rating>
                                        </td>
                                        <td>
                                            <asp:Repeater ID="rptKandidatCVFiles" runat="server" OnItemDataBound="rptKandidatCVFiles_ItemDataBound">
                                                <HeaderTemplate>
                                                    <ul style="padding: 0px">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:HyperLink ID="linkDownloadCV" runat="server" ToolTip="Download" Target="_blank"></asp:HyperLink>
                                                    </li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbMatching" runat="server" CssClass="btn btn-default" ToolTip="Seleksi Kualifikasi"
                                                Target="_blank">
                                                    <i class="fa fa-check-square-o"></i>&nbsp; Qualification Matching 
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbInterview" runat="server" CssClass="btn btn-default" ToolTip="Proses Interview">
                                                    <i class="fa fa-users"></i>&nbsp; Interview Records
                                            </asp:LinkButton>
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
                                        </table>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            <div class="row" style="margin-top: 20px;">
                                <div class="col-sm-12">
                                    <uc:AlertControl ID="alertNotificationAddKandidat" runat="server" Visible="false"
                                        Dismissable="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:LinkButton ID="btnShowAddKandidat" runat="server" CssClass="btn btn-primary pull-right"
                                        OnClick="btnShowAddKandidat_Click">
                                        <i class="fa fa-plus"></i> Add New Candidate
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlHistoryApproval" Visible="false" runat="server" CssClass="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                Request Approval History</h3>
                            <div class="box-tools pull-right">
                                <asp:HiddenField ID="hidCollapseApprovalHistory" runat="server" Value="false" />
                                <a onclick="doCollapse('<%=hidCollapseApprovalHistory.ClientID%>');" class="btn btn-default btn-sm"
                                    data-widget="collapse" data-toggle="tooltip" title="" data-original-title="Collapse">
                                    <i class="fa fa-minus"></i></a>
                            </div>
                        </div>
                        <div class="box-body form-horizontal">
                            <asp:Repeater ID="rptApprovalHistory" runat="server" OnItemDataBound="rptApprovalHistory_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="table table-stripped">
                                        <tr>
                                            <th>
                                                User
                                            </th>
                                            <th>
                                                Level Approval
                                            </th>
                                            <th width="420">
                                                Comments
                                            </th>
                                            <th width="140">
                                                Date Processed
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblUser" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLevelApproval" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblComment" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTglProses" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            </div>

            <script type="text/javascript">
                function changeKandidatTab(tabID) {
                    $('#<%=hidKandidatActiveTab.ClientID%>').val(tabID);
                }

                function disableRatingControl(behaviorID) {
                    $find(behaviorID).set_ReadOnly(true);
                }

                function doCollapse(hiddenValueID) {
                    var checked = $('#' + hiddenValueID).val();
                    if (checked == 'true') {
                        $('#' + hiddenValueID).val('false');
                    }
                    else {
                        $('#' + hiddenValueID).val('true');
                    }
                }

                function pageLoad() {
                    setTimeout(function() { 
                        $('.ratingControl').each(function(index, value) {
                            var behaviorID = $(value).attr('data-behaviorid');
                            var rate = $(value).siblings('.ratingValue').val();
                            $find(behaviorID).set_Rating(rate);
                        });
                    }, 500);
                
                    $('input.numeric').keypress(function(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if (charCode > 31 && (charCode < 48 || charCode > 57))
                            return false;
                        return true;
                    });

                    $("[data-widget='collapse']").off('click').on('click', function() {
                        //Find the box parent        
                        var box = $(this).parents(".box").first();
                        //Find the body and the footer
                        var bf = box.find(".box-body, .box-footer");
                        if (!box.hasClass("collapsed-box")) {
                            box.addClass("collapsed-box");
                            //Convert minus into plus
                            $(this).children(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
                            bf.slideUp();
                        } else {
                            box.removeClass("collapsed-box");
                            //Convert plus into minus
                            $(this).children(".fa-plus").removeClass("fa-plus").addClass("fa-minus");
                            bf.slideDown();
                        }
                    });

                    $("[data-widget='table-collapse']").off('click').on('click', function() {
                        //Find the box parent        
                        var box = $(this).parents(".form-group").first();
                        //Find the body and the footer
                        var bf = box.find(".tblCollapse tbody");
                        if (!box.hasClass("collapsed-table")) {
                            box.addClass("collapsed-table");
                            //Convert minus into plus
                            $(this).children(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
                            bf.slideUp();
                        } else {
                            box.removeClass("collapsed-table");
                            //Convert plus into minus
                            $(this).children(".fa-plus").removeClass("fa-plus").addClass("fa-minus");
                            bf.slideDown();
                        }
                    });

                    var tab = $('#<%=hidKandidatActiveTab.ClientID%>').val();
                    $('#tabKandidat a[href="' + tab + '"]').tab('show');

                    var tblExistingKandidat = $('#tblExistingKandidat').dataTable({
                        "bDestroy": true,
                        "bPaginate": true,
                        "bLengthChange": true,
                        "bStateSave": true,
                        "bFilter": true,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false
                    });
                }

                function checkCollapse(hidValID) {
                    if ($('#' + hidValID).val() == 'true') {
                        var box = $('#' + hidValID).parents(".box").first();
                        $(box).find(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
                        //Find the body and the footer
                        var bf = box.find(".box-body, .box-footer");
                        if (!box.hasClass("collapsed-box")) {
                            box.addClass("collapsed-box");
                            //Convert minus into plus
                            box.children(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
                            bf.hide();
                        }
                    }
                }

                function checkTableCollapse(hidValID) {
                    if ($('#' + hidValID).val() == 'true') {
                        var box = $('#' + hidValID).parents(".form-group").first();
                        $(box).find(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
                        //Find the body and the footer
                        var bf = box.find(".tblCollapse tbody");
                        if (!box.hasClass("collapsed-table")) {
                            box.addClass("collapsed-table");
                            //Convert minus into plus
                            box.children(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
                            bf.hide();
                        }
                    }
                }

                function storeRating(ratingBehaviorID, hidRatingID) {
                    var rate = $find(ratingBehaviorID).get_Rating();
                    $('#' + hidRatingID).val(rate);
                }

                $('input.calendar').inputmask("d/m/y", { "placeholder": "dd/mm/yyyy" });
                pageLoad();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function(s, e) {
                    pageLoad();
                    checkCollapse('<%=hidCollapseDaftarKandidat.ClientID%>');
                    checkCollapse('<%=hidCollapseApprovalHistory.ClientID%>');
                    checkTableCollapse('<%=hidCollapseQualification.ClientID%>');
                    checkTableCollapse('<%=hidCollapseJobDesc.ClientID%>');
                });
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" BehaviorID="popUpConfirm"
        PopupControlID="pnlConfirm" Type="Warning" />
    <uc:PopUpControl ID="popUpKandidatFinal" runat="server" HeaderText="Choose Selected Candidate"
        BehaviorID="popUpKandidatFinal" PopupControlID="pnlKandidatFinal" Type="Default"
        EnableCloseBtn="true" Size="Large" />
    <uc:PopUpControl ID="popUpInterviewKandidat" runat="server" HeaderText="Candidate Interview Records"
        BehaviorID="popUpInterviewKandidat" PopupControlID="pnlUpInterviewKandidat" Type="Default"
        EnableCloseBtn="true" Size="Large" />
    <uc:PopUpControl ID="popUpMatchingKandidat" runat="server" HeaderText="Candidate Qualification Matching"
        BehaviorID="popUpMatchingKandidat" PopupControlID="pnlMatchingKandidat" Type="Default"
        EnableCloseBtn="true" Size="Large" />
    <uc:PopUpControl ID="popUpEditKandidat" runat="server" HeaderText="Edit Kandidat"
        BehaviorID="popUpEditKandidat" PopupControlID="pnlEditKandidat" Type="Default"
        EnableCloseBtn="true" Size="Large" />
    <uc:PopUpControl ID="popUpAddKandidat" runat="server" HeaderText="Add Kandidat"
        BehaviorID="popUpAddKandidat" PopupControlID="pnlAddKandidat" Type="Default"
        EnableCloseBtn="true" Size="Large" />
    <asp:Panel ID="pnlAddKandidat" runat="server">
        <div id="tabKandidat" class="modal-body">
            <asp:HiddenField ID="hidKandidatActiveTab" runat="server" Value="addNew" />
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a href="#addNew" role="tab" data-toggle="tab" onclick="changeKandidatTab('#addNew');">
                    Add New Candidate</a></li>
                <li><a href="#existing" role="tab" data-toggle="tab" onclick="changeKandidatTab('#existing');">
                    Add From Existing Candidate</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="addNew">
                    <asp:Panel ID="pnlAddNew" runat="server" CssClass="form" Style="margin-top: 10px;"
                        DefaultButton="btnAddKandidat">
                        <div class="form-group">
                            <label class="control-label">
                                Candidate Name</label>
                            <asp:TextBox ID="txtNamaKandidat" runat="server" CssClass="form-control" Placeholder="Candidate full name, e.g.,: John Doe"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqNamaKandidat" runat="server" ControlToValidate="txtNamaKandidat"
                                ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddNewKandidat"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                No. Identity</label>
                            <asp:TextBox ID="txtNoIdentitasKandidat" runat="server" CssClass="form-control" Placeholder="Identity number, e.g.,: national identity number, driving license, pasport"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqNoIdentitas" runat="server" ControlToValidate="txtNoIdentitasKandidat"
                                ErrorMessage="Field tidak boleh kosong" Display="Dynamic" ValidationGroup="AddNewKandidat"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                Gender</label>
                            <asp:RadioButtonList ID="rblGenderKandidat" runat="server" CssClass="myCheckbox">
                                <asp:ListItem Text="Male" Value="L"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="P"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:CustomValidator runat="server" ID="cvGenderKandidat" ClientValidationFunction="ValidateGender"
                                ErrorMessage="Choose gender" Display="Dynamic" ValidationGroup="AddNewKandidat"></asp:CustomValidator>

                            <script type="text/javascript">
                                // javascript to add to your aspx page
                                function ValidateGender(source, args) {
                                    var chkListGender = document.getElementById('<%=rblGenderKandidat.ClientID %>');
                                    var chkListInputs = chkListGender.getElementsByTagName("input");
                                    for (var i = 0; i < chkListInputs.length; i++) {
                                        if (chkListInputs[i].checked) {
                                            args.IsValid = true;
                                            return;
                                        }
                                    }
                                    args.IsValid = false;
                                }
                            </script>

                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                No. Handphone</label>
                            <asp:TextBox ID="txtNoHandphoneKandidat" runat="server" CssClass="form-control" Placeholder="Candidate phone number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqNoHandphone" runat="server" ControlToValidate="txtNoHandphoneKandidat"
                                ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddNewKandidat"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                E-mail</label>
                            <asp:TextBox ID="txtEmailKandidat" runat="server" CssClass="form-control" Placeholder="Candidate e-mail address"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regEmailKandidat" runat="server" ControlToValidate="txtEmailKandidat"
                                ErrorMessage="Invalid e-mail format" Display="Dynamic" ValidationGroup="AddNewKandidat"
                                ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label class="control-label">
                                Recommendation</label>                            
                            <div style="margin-bottom:5px;margin-bottom: 5px;display: block;height: 30px;">
                                <asp:TextBox ID="hidRatingAddKandidat" runat="server" CssClass="ratingValue" Style="display:none;" Text="0"></asp:TextBox>
                                <act:Rating ID="ratingAddKandidat" runat="server" MaxRating="5" StarCssClass="fa myRatingStar"
                                    WaitingStarCssClass="fa-spinner" FilledStarCssClass="fa-star" EmptyStarCssClass="fa-star-o"
                                    BehaviorID="ratingAddKandidat">
                                </act:Rating>
                            </div>                            
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="control-label col-sm-12">
                                    CV Files</label>
                            </div>
                            <asp:Repeater ID="rptCVFiles" runat="server" OnItemDataBound="rptCVFiles_ItemDataBound"
                                OnItemCommand="rptCVFiles_ItemCommand">
                                <ItemTemplate>
                                    <div class="row" style="margin-bottom: 5px;">
                                        <div class="col-sm-11">
                                            <asp:FileUpload ID="fuCVFile" runat="server" CssClass="form-control fuWithTrigger"
                                                Placeholder="Upload CV file"></asp:FileUpload>
                                            <asp:RequiredFieldValidator ID="reqCVFile" runat="server" ControlToValidate="fuCVFile"
                                                ErrorMessage="File must be filled" Display="Dynamic" ValidationGroup="AddNewKandidat"></asp:RequiredFieldValidator>
                                            <%--<asp:Button ID="btnTriggerFileUpload" CssClass="btnTriggerFu" runat="server" style="display:none" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';"/>--%>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                                <span class="fa fa-trash-o"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="form-group">
                            <div class="row" style="margin: 10px 0px;">
                                <div class="col-sm-12" style="padding-left: 0px;">
                                    <asp:LinkButton ID="btnAddKandidatFile" ToolTip="Tambah Berkas" runat="server" CssClass="btn pull-left btn-success"
                                        OnClick="btnAddKandidatFile_Click">
                                        <span class="fa fa-plus"></span> Add CV File(s)
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:LinkButton ID="btnAddKandidat" runat="server" CssClass="btn btn-primary pull-right"
                                    OnClick="btnAddKandidat_Click" ValidationGroup="AddNewKandidat" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';">
                                    <i class="fa fa-save"></i>&nbsp; Save
                                </asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="tab-pane" id="existing">
                    <asp:Panel ID="pnlAddFromMasterKandidat" runat="server" CssClass="form" Style="margin-top: 10px;"
                        DefaultButton="btnAddKandidat">
                        <div class="row">
                            <div class="col-sm-12">
                            <style>
                                .myRatingStar
                                {
                                    font-size: 20px;
                                    color: #f0ad4e;
                                    margin-top: 5px;
                                }
                                .headcol
                                {
                                	position: absolute;
                                	left: 10px;
                                	width: 50px;
                                }
                                .headcol2
                                {
                                	position: absolute;
                                	left: 50px;
                                	width: 250px;
                                }
                                .headcol3
                                {
                                	position: absolute;
                                	right: 10px;
                                	width: 90px;
                                }
                            </style>
                            <table id="tblExistingKandidat" class="table table-stripped">
                                <thead>
                                    <tr>
                                        <th>
                                            Nama
                                        </th>
                                        <th>
                                            Gender
                                        </th>
                                        <th>
                                            No. Identity
                                        </th>
                                        <th>
                                            No. Handphone
                                        </th>
                                        <th>
                                            Recommendation
                                        </th>
                                        <th>
                                            Choose
                                        </th>
                                    </tr>
                                    <tr id="rowNoMasterKandidat" runat="server" style="display: none;">
                                        <td colspan="6">
                                            There's not yet registered candidates in current requested position
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                <asp:Repeater ID="rptMasterKandidat" runat="server" OnItemDataBound="rptMasterKandidat_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="hidKdKandidat" Visible="false" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblNama" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblGender" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNoIdentitas" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNoHP" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="hidRatingKandidat" runat="server" CssClass="ratingValue" Style="display:none;"/>
                                                <act:Rating ID="ratingKandidat" runat="server" MaxRating="5" StarCssClass="fa myRatingStar"
                                                    WaitingStarCssClass="fa-spinner" FilledStarCssClass="fa-star" EmptyStarCssClass="fa-star-o"
                                                    ReadOnly="true">
                                                </act:Rating>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkPilih" runat="server" AutoPostBack="true" CssClass="chkPilih" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:CustomValidator ID="cvAddExistingKandidat" runat="server" ValidationGroup="AddExistingKandidat"
                                    ClientValidationFunction="ValidatePilihanKandidat" ErrorMessage="Please choose at least one candidate"
                                    Display="Dynamic">
                                </asp:CustomValidator>

                                <script type="text/javascript">
                                    // javascript to add to your aspx page
                                    function ValidatePilihanKandidat(source, args) {
                                        args.IsValid = false;
                                        $('#tblExistingKandidat input[type=checkbox]').each(function(i, v) {
                                            if ($(v).prop('checked')) {
                                                args.IsValid = true;
                                                return false;
                                            }
                                        });
                                        return;
                                    }
                                </script>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <uc:AlertControl ID="alertAddExistingKandidat" runat="server" Visible="false" Dismissable="true" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 20px;">
                            <div class="col-sm-12">
                                <asp:LinkButton ID="btnAddKandidatFromMaster" ToolTip="Add Candidate(s) to Request"
                                    runat="server" CssClass="btn pull-right btn-success" ValidationGroup="AddExistingKandidat" OnClick="btnAddKandidatFromMaster_Click">
                                    <span class="fa fa-check"></span> Add Candidate(s) to Request
                                </asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="modal-footer">
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlEditKandidat" runat="server">
        <div class="modal-body">
            <div class="tab-pane active">
                <asp:Panel ID="pnlEditKandidatDetail" runat="server" CssClass="form" Style="margin-top: 10px;"
                    DefaultButton="btnEditKandidat">
                    <div class="form-group">
                        <label class="control-label">
                            Candidate Name</label>
                        <asp:TextBox ID="hidKdKandidatEdit" runat="server" Visible="false" />
                        <asp:TextBox ID="txtNmKandidatEdit" runat="server" CssClass="form-control" Placeholder="Candidate full name, e.g.,: John Doe"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNmKandidatEdit" runat="server" ControlToValidate="txtNmKandidatEdit"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            No. Identity</label>
                        <asp:TextBox ID="txtNoIdentitasKandidatEdit" runat="server" CssClass="form-control"
                            Placeholder="Identity number, e.g.,: national identity number, driving license, pasport"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNoIdentitasKandidatEdit" runat="server" ControlToValidate="txtNoIdentitasKandidatEdit"
                            ErrorMessage="Field tidak boleh kosong" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            Gender</label>
                        <asp:RadioButtonList ID="rblGenderKandidatEdit" runat="server" CssClass="myCheckbox">
                            <asp:ListItem Text="Male" Value="L"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="P"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CustomValidator runat="server" ID="cvGenderKandidatEdit" ClientValidationFunction="ValidateGenderEdit"
                            ErrorMessage="Choose gender" Display="Dynamic" ValidationGroup="EditKandidat"></asp:CustomValidator>

                        <script type="text/javascript">
                            // javascript to add to your aspx page
                            function ValidateGenderEdit(source, args) {
                                var chkListGender = document.getElementById('<%=rblGenderKandidatEdit.ClientID %>');
                                var chkListInputs = chkListGender.getElementsByTagName("input");
                                for (var i = 0; i < chkListInputs.length; i++) {
                                    if (chkListInputs[i].checked) {
                                        args.IsValid = true;
                                        return;
                                    }
                                }
                                args.IsValid = false;
                            }
                        </script>

                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            No. Handphone</label>
                        <asp:TextBox ID="txtNoHandphoneKandidatEdit" runat="server" CssClass="form-control"
                            Placeholder="Candidate phone number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNoHandphoneEdit" runat="server" ControlToValidate="txtNoHandphoneKandidatEdit"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            E-mail</label>
                        <asp:TextBox ID="txtEmailKandidatEdit" runat="server" CssClass="form-control" Placeholder="Candidate e-mail address"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regEmailKandidatEdit" runat="server" ControlToValidate="txtEmailKandidatEdit"
                            ErrorMessage="Invalid e-mail format" Display="Dynamic" ValidationGroup="EditKandidat"
                            ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            Recommendation</label>                            
                        <div style="margin-bottom:5px;margin-bottom: 5px;display: block;height: 30px;">
                            <asp:TextBox ID="hidRatingEditKandidat" runat="server" CssClass="ratingValue" Style="display:none;" Text="0"></asp:TextBox>
                            <act:Rating ID="ratingEditKandidat" runat="server" MaxRating="5" StarCssClass="fa myRatingStar"
                                WaitingStarCssClass="fa-spinner" FilledStarCssClass="fa-star" EmptyStarCssClass="fa-star-o"
                                BehaviorID="ratingEditKandidat">                             
                            </act:Rating>
                        </div>                            
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label class="control-label col-sm-12">
                                CV Files</label>
                        </div>
                        <asp:Repeater ID="rptExistingCVFilesEdit" runat="server" OnItemDataBound="rptExistingCVFilesEdit_ItemDataBound"
                            OnItemCommand="rptExistingCVFilesEdit_ItemCommand">
                            <ItemTemplate>
                                <div class="row" style="margin-bottom: 5px;">
                                    <div class="col-sm-11">
                                        <asp:TextBox ID="hidKdFile" runat="server" Visible="false" />
                                        <asp:HyperLink ID="linkDownloadCV" runat="server" ToolTip="Download"></asp:HyperLink>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                            <span class="fa fa-trash-o"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rptCVFilesEdit" runat="server" OnItemDataBound="rptCVFiles_ItemDataBound"
                            OnItemCommand="rptCVFilesEdit_ItemCommand">
                            <ItemTemplate>
                                <div class="row" style="margin-bottom: 5px;">
                                    <div class="col-sm-11">
                                        <asp:FileUpload ID="fuCVFile" runat="server" CssClass="form-control fuWithTrigger"
                                            Placeholder="Upload CV file"></asp:FileUpload>
                                        <asp:RequiredFieldValidator ID="reqCVFile" runat="server" ControlToValidate="fuCVFile"
                                            ErrorMessage="File must be filled" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                            <span class="fa fa-trash-o"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="form-group">
                        <div class="row" style="margin: 10px 0px;">
                            <div class="col-sm-12" style="padding-left: 0px;">
                                <asp:LinkButton ID="btnAddKandidatFileEdit" ToolTip="Add CV File(s)" runat="server"
                                    CssClass="btn pull-left btn-success" OnClick="btnAddKandidatFileEdit_Click">
                                    <span class="fa fa-plus"></span> Add CV File(s)
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="modal-footer">
            <asp:LinkButton ID="btnEditKandidat" runat="server" CssClass="btn btn-primary pull-right"
                OnClick="btnEditKandidat_Click" ValidationGroup="EditKandidat" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';">
                <i class="fa fa-save"></i>&nbsp; Save
            </asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMatchingKandidat" runat="server">
        <div class="modal-body">
            <div>
                <div class="row">
                    <label class="text-right col-sm-2">
                        Candidate Name
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblNmKandidatMatching" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidKdKandidatMatching" runat="server" />
                    </div>
                    <label class="text-right col-sm-2">
                        No. Identity
                    </label>
                    <div class="col-sm-4" style="margin-top: 5px;">
                        <asp:Label ID="lblNoIdentitasMatching" runat="server" Style="font-family: Courier New"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="text-right col-sm-2">
                        Gender
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblGenderMatching" runat="server"></asp:Label>
                    </div>
                    <label class="text-right col-sm-2">
                        No. Handphone
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblNoHandphoneMatching" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="text-right col-sm-2">
                        Candidate CV File(s)
                    </label>
                    <div class="col-sm-4">
                        <asp:Repeater ID="rptKandidatMatchingCVFiles" runat="server" OnItemDataBound="rptKandidatCVFiles_ItemDataBound">
                            <HeaderTemplate>
                                <ul style="padding: 10px">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:HyperLink ID="linkDownloadCV" runat="server" ToolTip="Download" Target="_blank"></asp:HyperLink>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <label class="text-right col-sm-2">
                        E-mail
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblEmailMatching" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <asp:Repeater ID="rptQualificationMatching" runat="server" OnItemDataBound="rptQualificationMatching_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-stripped">
                        <tr>
                            <th style="width: 5%;">
                                #
                            </th>
                            <th style="width: 50%;">
                                Qualification
                            </th>
                            <th style="width: 7%;">
                                Is Match?
                            </th>
                            <th>
                                Comments
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="hidKdQualification" Visible="false" runat="server"></asp:TextBox>
                            <asp:Label ID="lblQualification" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkMatch" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtComment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div>
                Additional Qualification
            </div>
            <table class="table table-stripped">
                <tr>
                    <th style="width: 5%;">
                        #
                    </th>
                    <th style="width: 45%;">
                        Qualification
                    </th>
                    <th>
                        Comments
                    </th>
                    <th style="width: 11%;">
                        Option
                    </th>
                </tr>
                <asp:Repeater ID="rptAdditionalQualification" runat="server" OnItemDataBound="rptAdditionalQualification_ItemDataBound"
                    OnItemCommand="rptAdditionalQualification_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="hidKdQualification" Visible="false" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtAdditionalQualificationRepeater" CssClass="form-control" Visible="false"
                                    runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="lblQualification" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator ID="reqAdditionalQualificationRepeater" runat="server"
                                    ControlToValidate="txtAdditionalQualificationRepeater" Display="Dynamic" ErrorMessage="Field must be filled"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdditionalQualificationCommentRepeater" CssClass="form-control"
                                    Visible="false" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="lblComment" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" Width="35" CssClass="btn btn-sm btn-primary">
                                <span class="fa fa-pencil"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                <span class="fa fa-trash-o"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnSave" ToolTip="Save" runat="server" Width="35" CssClass="btn btn-sm btn-success"
                                    Visible="false">
                                <span class="fa fa-save"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" ToolTip="Cancel" runat="server" Width="35" CssClass="btn btn-sm btn-warning"
                                    Visible="false">
                                <span class="fa fa-times"></span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>
                    </td>
                    <td style="width: 50%;">
                        <asp:TextBox ID="txtAdditionalQualification" CssClass="form-control" runat="server"
                            Placeholder="Additional qualification" TextMode="MultiLine">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqAdditionalQualification" ErrorMessage="Field must be filled"
                            ControlToValidate="txtAdditionalQualification" Display="Dynamic" runat="server"
                            ValidationGroup="TambahAdditionalQualification">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtKomentarAdditionalQualification" CssClass="form-control" runat="server"
                            Placeholder="Additional qualification comments" TextMode="MultiLine">
                        </asp:TextBox>
                    </td>
                    <td style="width: 7%;">
                        <asp:LinkButton ID="btnTambahAdditionalQualification" ValidationGroup="TambahAdditionalQualification"
                            OnClick="btnTambahAdditionalQualification_Click" ToolTip="Add New" runat="server"
                            Width="35" CssClass="btn btn-sm btn-info">
                            <span class="fa fa-plus"></span>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <asp:LinkButton ID="btnSaveQualificationMatching" runat="server" CssClass="btn btn-primary pull-right"
                OnClick="btnSaveQualificationMatching_Click">
                <i class="fa fa-save"></i>&nbsp; Save Qualification Matching
            </asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlUpInterviewKandidat" runat="server">
        <div class="modal-body">
            <div>
                <div class="row">
                    <label class="text-right col-sm-2">
                        Candidate Name
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblNmKandidatInterview" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidKdKandidatInterview" runat="server" />
                    </div>
                    <label class="text-right col-sm-2">
                        No. Identity
                    </label>
                    <div class="col-sm-4" style="margin-top: 5px;">
                        <asp:Label ID="lblNoIdentitasInterview" runat="server" Style="font-family: Courier New"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="text-right col-sm-2">
                        Gender
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblGenderKandidatInterview" runat="server"></asp:Label>
                    </div>
                    <label class="text-right col-sm-2">
                        No. Handphone
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblNoHandphoneInterview" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="text-right col-sm-2">
                        Candidate CV File(s)
                    </label>
                    <div class="col-sm-4">
                        <asp:Repeater ID="rptKandidatInterviewCVFiles" runat="server" OnItemDataBound="rptKandidatCVFiles_ItemDataBound">
                            <HeaderTemplate>
                                <ul style="padding: 10px">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:HyperLink ID="linkDownloadCV" runat="server" ToolTip="Download" Target="_blank"></asp:HyperLink><br />
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <label class="text-right col-sm-2">
                        E-mail
                    </label>
                    <div class="col-sm-4">
                        <asp:Label ID="lblEmailInterview" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <asp:Repeater ID="rptKandidatInterview" runat="server" OnItemDataBound="rptKandidatInterview_ItemDataBound"
                OnItemCommand="rptKandidatInterview_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-stripped">
                        <tr>
                            <th style="width: 15%;">
                                Interview Date
                            </th>
                            <th style="width: 25%;">
                                Interviewer Name
                            </th>
                            <th>
                                Interview Results
                            </th>
                            <th style="width: 11%;">
                                Option
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox ID="hidKdInterview" Visible="false" runat="server"></asp:TextBox>
                            <asp:Label ID="lblTglInterview" runat="server"></asp:Label>
                            <asp:TextBox ID="txtTglInterviewRepeater" Visible="false" CssClass="form-control"
                                runat="server"></asp:TextBox>
                            <act:CalendarExtender ID="calTglInterview" runat="server" TargetControlID="txtTglInterviewRepeater"
                                PopupButtonID="txtTglInterviewRepeater" Format="dd/MM/yyyy">
                            </act:CalendarExtender>
                            <asp:RequiredFieldValidator ID="reqTglInterviewRepeater" ControlToValidate="txtTglInterviewRepeater"
                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvTglInterviewRepeater" runat="server" Type="Date" Operator="DataTypeCheck"
                                ControlToValidate="txtTglInterviewRepeater" ErrorMessage="Invalid date format. Valid format: dd/MM/yyyy"
                                Display="Dynamic" ClientValidationFunction="isDate">
                            </asp:CustomValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblInterviewer" runat="server"></asp:Label>
                            <asp:TextBox ID="txtInterviewerRepeater" Visible="false" CssClass="form-control" Placeholder="Interviewer name"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqInterviewRepeater" ControlToValidate="txtInterviewerRepeater"
                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHasilInterview" runat="server"></asp:Label>
                            <asp:TextBox ID="txtHasilInterviewRepeater" Visible="false" CssClass="form-control"
                                TextMode="MultiLine" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqHasilInterviewRepeater" ControlToValidate="txtHasilInterviewRepeater"
                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" Width="35" CssClass="btn btn-sm btn-primary">
                                <span class="fa fa-pencil"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                <span class="fa fa-trash-o"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" ToolTip="Save" runat="server" Width="35" CssClass="btn btn-sm btn-success"
                                Visible="false">
                                <span class="fa fa-save"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" ToolTip="Cancel" runat="server" Width="35" CssClass="btn btn-sm btn-warning"
                                Visible="false">
                                <span class="fa fa-times"></span>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-sm-2">
                        Interview date
                    </label>
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtTglInterview" runat="server" Enabled="true" CssClass="form-control calendar"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnShowTglInterview" runat="server" CssClass="btn btn-info btn-flat">
                                    <i class="fa fa-calendar"></i>
                                </asp:LinkButton>
                            </span>
                            <act:CalendarExtender ID="calTglInterview" runat="server" TargetControlID="txtTglInterview"
                                PopupButtonID="btnShowTglInterview" Format="dd/MM/yyyy">
                            </act:CalendarExtender>
                            <act:CalendarExtender ID="calTglInterview2" runat="server" TargetControlID="txtTglInterview"
                                PopupButtonID="txtTglInterview" Format="dd/MM/yyyy">
                            </act:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-sm-7">
                        <asp:RequiredFieldValidator ID="reqTglInterview" ControlToValidate="txtTglInterview"
                            ErrorMessage="Field must be filled" runat="server" ValidationGroup="AddInterview"
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvTglInterview" runat="server" Type="Date" Operator="DataTypeCheck"
                            ControlToValidate="txtTglInterview" ErrorMessage="Invalid date format. Valid format: dd/MM/yyyy"
                            ValidationGroup="AddInterview" Display="Dynamic" ClientValidationFunction="isDate">
                        </asp:CustomValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2">
                        Interviewer Name
                    </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtInterviewer" CssClass="form-control" runat="server" Placeholder="Interviewer name"></asp:TextBox>
                    </div>
                    <div class="col-sm-7">
                        <asp:RequiredFieldValidator ID="reqInterviewer" ControlToValidate="txtInterviewer"
                            ErrorMessage="Field must be filled" runat="server" ValidationGroup="AddInterview"
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2">
                        Interview Results
                    </label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtHasilInterview" CssClass="form-control" TextMode="MultiLine"
                            Placeholder="Interview result notes" runat="server">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqHasilInterview" ControlToValidate="txtHasilInterview"
                            ErrorMessage="Field must be filled" runat="server" ValidationGroup="AddInterview"
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:LinkButton ID="lbAddInterview" runat="server" ValidationGroup="AddInterview"
                CssClass="btn btn-primary pull-right" OnClick="lbAddInterview_Click">
                <i class="fa fa-plus"></i>&nbsp; Add Interview Records
            </asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlKandidatFinal" runat="server">
        <div class="modal-body">
            <div class="callout callout-info">
                <h4>Choosen Candidate</h4>
                <p>Please choose the selected candidate(s) before the request closed. <br />The selected candidate(s) will be informed to the manager, general manager, and director of respective department.</p>
            </div>
            <asp:Repeater ID="rptKandidatFinal" runat="server" OnItemDataBound="rptKandidatFinal_ItemDataBound">
                <HeaderTemplate>
                    <table id="tblFinalisasi" class="table table-stripped">
                        <tr>
                            <th>
                                #
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Gender
                            </th>
                            <th>
                                No. Identity
                            </th>
                            <th>
                                No. Handphone
                            </th>
                            <th>
                                E-mail
                            </th>
                            <th style="width: 100px;">
                                Download CV
                            </th>
                            <th>
                                Choose
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNama" runat="server"></asp:Label>
                            <asp:TextBox ID="hidKdKandidat" Visible="false" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNoIdentitas" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNoHandphone" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Repeater ID="rptKandidatFinalCVFiles" runat="server" OnItemDataBound="rptKandidatCVFiles_ItemDataBound">
                                <HeaderTemplate>
                                    <ul style="padding:0px">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <asp:HyperLink ID="linkDownloadCV" runat="server" ToolTip="Download" Target="_blank"></asp:HyperLink>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkPilih" runat="server" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="row">
                <div class="col-sm-12">
                    <asp:CustomValidator runat="server" ID="cvFinalisasi" ClientValidationFunction="ValidateFinalisasi"
                        ErrorMessage="Choose at least one candidate" Display="Dynamic" ValidationGroup="Finalisasi"></asp:CustomValidator>
                </div>

                <script type="text/javascript">
                    function ValidateFinalisasi(source, args) {
                        args.IsValid = false;
                        $('#tblFinalisasi input[type=checkbox]').each(function(i, v) {
                            if ($(v).prop('checked')) {
                                args.IsValid = true;
                                return false;
                            }
                        });
                        return;
                    }
                </script>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <uc:AlertControl ID="alertFinalisasi" runat="server" Visible="false" Dismissable="true" />
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:LinkButton ID="btnFinalisasi" runat="server" CssClass="btn btn-primary pull-right"
                OnClick="btnFinalisasi_Click" ValidationGroup="Finalisasi">
                <i class="fa fa-check"></i>&nbsp; Finalize
            </asp:LinkButton>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlConfirm" runat="server">
        <div class="modal-body">
            <asp:HiddenField ID="hidKdKandidatConfirm" runat="server" />
            <asp:Literal ID="litConfirm" runat="server"></asp:Literal><br />
            Notes:
            <ul style="padding-left: 15px">
                <li><strong>Delete Permanently</strong>: Candidate will be deleted from master candidate</li>
                <li><strong>Delete from Request</strong>: Candidate will only be deleted from the request (the respective candidate's data still remains in master candidate)</li>
            </ul>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnYesPermanentConfirm" runat="server" CssClass="btn btn-danger"
                Text="Yes, Delete Permanently" OnClick="btnYesPermanentConfirm_Click" />
            <asp:Button ID="btnYesConfirm" runat="server" CssClass="btn btn-warning" Text="Yes, Delete From Request"
                OnClick="btnYesConfirm_Click" />
            <asp:Button ID="btnNoConfirm" runat="server" CssClass="btn btn-primary" Text="No"
                OnClick="btnNoConfirm_Click" />
        </div>
    </asp:Panel>
</asp:Content>
