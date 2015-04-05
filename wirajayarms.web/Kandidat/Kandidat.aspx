<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kandidat.aspx.cs" Inherits="WirajayaRMS.Web.Kandidat.Kandidat" Title="Candidate List" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../UserControl/AlertControl.ascx" TagName="AlertControl" TagPrefix="uc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <style>
        .myRatingStar
        {
        	font-size:20px; 
        	color:#f0ad4e;
        	margin-top:5px;
        }
    </style>
        
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                Candidate List</h3>
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
                                    Position
                                </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlJabatan" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlJabatan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>               
                            <div class="col-sm-12" style="min-height:250px;">        
                                <asp:Repeater ID="rptKandidat" runat="server" OnItemDataBound="rptKandidat_ItemDataBound" OnItemCommand="rptKandidat_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-stripped">
                                            <tr>
                                                <th>#</th>
                                                <th>Name</th>
                                                <th>Gender</th>
                                                <th>No. Identity</th>
                                                <th>No. Handphone</th>
                                                <th>E-mail Address</th>
                                                <%--<th>Download CV</th>--%>
                                                <th>Division</th>
                                                <th style="width:150px;">Organizational Structure/ Position</th>
                                                <th style="width:90px;">Option</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><asp:Label ID="lblNo" runat="server"></asp:Label></td>
                                            <td><asp:Label ID="lblNama" runat="server"></asp:Label></td>
                                            <td><asp:Label ID="lblGender" runat="server"></asp:Label></td>
                                            <td><asp:Label ID="lblNoIdentitas" runat="server"></asp:Label></td>
                                            <td><asp:Label ID="lblNoHandphone" runat="server"></asp:Label></td>
                                            <td><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                                            <%--<td>
                                                <asp:Repeater ID="rptCVFiles" runat="server" OnItemDataBound="rptCVFiles_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <ul>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:HyperLink ID="linkDownloadCV" runat="server" ToolTip="Download"></asp:HyperLink>
                                                        </li>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </ul>
                                                    </FooterTemplate>
                                                </asp:Repeater>    
                                            </td>--%>
                                            <td><asp:Label ID="lblDivisi" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblSO" runat="server"></asp:Label>/
                                                <asp:Label ID="lblJabatan" runat="server"></asp:Label>
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
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:LinkButton ID="btnShowAddKandidat" OnClick="btnShowAddKandidat_Click" ToolTip="Add New Candidate" runat="server" CssClass="btn btn-primary pull-right">
                                        <span class="fa fa-plus"></span> Add New Candidate
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                function storeRating(ratingBehaviorID, hidRatingID, btnTriggerRatingID) {
                    var rate = $find(ratingBehaviorID).get_Rating();
                    $('#' + hidRatingID).val(rate);
                    $('#' + btnTriggerRatingID).click();
                }
                
                function pageLoad() {
                    
                }
                pageLoad();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function(s, e) {
                    pageLoad();
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Confirmation" 
        BehaviorID="popUpConfirm" PopupControlID="pnlConfirm" Type="Warning"/>
    
    <uc:PopUpControl ID="popUpEditKandidat" runat="server" HeaderText="Edit Candidate" BehaviorID="popUpEditKandidat"
        PopupControlID="pnlEditKandidat" Type="Default" EnableCloseBtn="true" Size="Large"/>
        
    <uc:PopUpControl ID="popUpAddKandidat" runat="server" HeaderText="Add New Candidate" BehaviorID="popUpAddKandidat"
        PopupControlID="pnlAddKandidat" Type="Default" EnableCloseBtn="true" Size="Large"/>
        
    <asp:Panel ID="pnlAddKandidat" runat="server">
        <div class="modal-body">
            <div class="tab-pane active">
                <asp:Panel ID="pnlAddKandidatInner" runat="server" CssClass="form" style="margin-top:10px;" DefaultButton="btnAddKandidat">                        
                    <div class="form-group">
                        <label class="control-label">
                            Candidate Name</label>
                        <asp:TextBox ID="txtNmKandidat" runat="server" CssClass="form-control" Placeholder="Candidate full name, e.g., John Doe"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNmKandidat" runat="server" ControlToValidate="txtNmKandidat"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            No. Identity</label>
                        <asp:TextBox ID="txtNoIdentitasKandidat" runat="server" CssClass="form-control" Placeholder="Identity number, e.g.,: national identity number, driving license, pasport"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNoIdentitasKandidat" runat="server" ControlToValidate="txtNoIdentitasKandidat"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            Gender</label>
                        <asp:RadioButtonList ID="rblGenderKandidat" runat="server" CssClass="myCheckbox">
                            <asp:ListItem Text="Male" Value="L"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="P"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CustomValidator runat="server" ID="cvGenderKandidat" ClientValidationFunction="ValidateGender"
                            ErrorMessage="Please choose gender" Display="Dynamic" ValidationGroup="AddKandidat"></asp:CustomValidator>
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
                        <asp:RequiredFieldValidator ID="reqNoHandphoneKandidat" runat="server" ControlToValidate="txtNoHandphoneKandidat"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="AddKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            E-mail</label>
                        <asp:TextBox ID="txtEmailKandidat" runat="server" CssClass="form-control" Placeholder="Candidate e-mail address"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="reqEmailKandidat" runat="server" ControlToValidate="txtEmailKandidat"
                            ErrorMessage="Invalid e-mail format" Display="Dynamic" ValidationGroup="AddKandidat" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">                   
                        <div class="row">
                            <label class="control-label col-sm-12">
                                CV File(s)</label>
                        </div>
                        <asp:Repeater ID="rptCVFiles" runat="server" OnItemDataBound="rptCVFiles_ItemDataBound" OnItemCommand="rptCVFiles_ItemCommand">
                            <ItemTemplate>
                                <div class="row" style="margin-bottom:5px;">
                                    <div class="col-sm-11">
                                        <asp:FileUpload ID="fuCVFile" runat="server" CssClass="form-control fuWithTrigger" Placeholder="Upload CV File"></asp:FileUpload>                                        
                                        <asp:RequiredFieldValidator ID="reqCVFile" runat="server" ControlToValidate="fuCVFile" ErrorMessage="File must be filled" Display="Dynamic" ValidationGroup="AddKandidat"></asp:RequiredFieldValidator>                                        
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
                        <div class="row" style="margin:20px 0px;">
                            <div class="col-sm-12" style="padding-left:0px;">
                                <asp:LinkButton ID="btnAddKandidatFile" ToolTip="Add More CV File" runat="server" CssClass="btn pull-left btn-success" OnClick="btnAddKandidatFile_Click">
                                    <span class="fa fa-plus"></span> Add More CV File
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label class="control-label col-sm-12">List of Applied Positions</label>
                            <asp:Repeater ID="rptPosisiKandidat" runat="server" 
                                          OnItemDataBound="rptPosisiKandidat_ItemDataBound" 
                                          OnItemCommand="rptPosisiKandidat_ItemCommand"
                                          OnItemCreated="rptPosisiKandidat_ItemCreated">
                                <HeaderTemplate>
                                    <table class="table table-stripped" style="margin-bottom:0px">
                                        <tr>
                                            <th>Division</th>
                                            <th width="380">Organizational Structure</th>
                                            <th>Position</th>
                                            <th>Recommendation</th>
                                            <th></th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlDivisiKandidat" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqDivisi" InitialValue="0" ControlToValidate="ddlDivisiKandidat"
                                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic" ValidationGroup="AddKandidat">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSOKandidat" runat="server" AutoPostBack="true" CssClass="form-control" Style="width:360px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqSO" InitialValue="0" ControlToValidate="ddlSOKandidat"
                                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic" ValidationGroup="AddKandidat">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlJabatanKandidat" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqJabatan" InitialValue="0" ControlToValidate="ddlJabatanKandidat"
                                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic" ValidationGroup="AddKandidat">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hidRating" runat="server" />
                                            <act:Rating ID="ratingKandidat" runat="server" MaxRating="5" 
                                                StarCssClass="fa myRatingStar" 
                                                WaitingStarCssClass="fa-spinner"
                                                FilledStarCssClass="fa-star"
                                                EmptyStarCssClass="fa-star-o"></act:Rating>
                                            <asp:Button ID="btnTriggerRating" runat="server" Visible="false" CommandName="TRIGGER"/>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnDelete" ToolTip="Hapus" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                                <span class="fa fa-trash-o"></span>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row" style="margin:0px 0px 20px 0px;">
                        <div class="col-sm-12" style="padding-left:0px;">
                            <asp:LinkButton ID="btnTambahPosisi" ToolTip="Add Position" runat="server" CssClass="btn pull-left btn-success" OnClick="btnTambahPosisi_Click">
                                <span class="fa fa-plus"></span> Add Position
                            </asp:LinkButton>
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
            <asp:LinkButton ID="btnAddKandidat" runat="server" CssClass="btn btn-primary pull-right" 
                OnClick="btnAddKandidat_Click" ValidationGroup="AddKandidat" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';">
                <i class="fa fa-save"></i>&nbsp; Save
            </asp:LinkButton> 
        </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlEditKandidat" runat="server">
        <div class="modal-body">
            <div class="tab-pane active">
                <asp:Panel ID="pnlEditKandidatDetail" runat="server" CssClass="form" style="margin-top:10px;" DefaultButton="btnEditKandidat">                        
                    <div class="form-group">
                        <label class="control-label">
                            Candidate Name</label>
                        <asp:TextBox ID="hidKdKandidatEdit" runat="server" Visible="false"/>
                        <asp:TextBox ID="txtNmKandidatEdit" runat="server" CssClass="form-control" Placeholder="Candidate full name, e.g., John Doe"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNmKandidatEdit" runat="server" ControlToValidate="txtNmKandidatEdit"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            No. Identity</label>
                        <asp:TextBox ID="txtNoIdentitasKandidatEdit" runat="server" CssClass="form-control" Placeholder="Identity number, e.g.,: national identity number, driving license, pasport"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNoIdentitasKandidatEdit" runat="server" ControlToValidate="txtNoIdentitasKandidatEdit"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            Gender</label>
                        <asp:RadioButtonList ID="rblGenderKandidatEdit" runat="server" CssClass="myCheckbox">
                            <asp:ListItem Text="Male" Value="L"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="P"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:CustomValidator runat="server" ID="cvGenderKandidatEdit" ClientValidationFunction="ValidateGenderEdit"
                            ErrorMessage="Please choose gender" Display="Dynamic" ValidationGroup="EditKandidat"></asp:CustomValidator>
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
                        <asp:TextBox ID="txtNoHandphoneKandidatEdit" runat="server" CssClass="form-control" Placeholder="Candidate phone number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqNoHandphoneEdit" runat="server" ControlToValidate="txtNoHandphoneKandidatEdit"
                            ErrorMessage="Field must be filled" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            E-mail</label>
                        <asp:TextBox ID="txtEmailKandidatEdit" runat="server" CssClass="form-control" Placeholder="Candidate e-mail address"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regEmailKandidatEdit" runat="server" ControlToValidate="txtEmailKandidatEdit"
                            ErrorMessage="Invalid e-mail format" Display="Dynamic" ValidationGroup="EditKandidat" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">                   
                        <div class="row">
                            <label class="control-label col-sm-12">
                                CV File(s)</label>
                        </div>           
                        <asp:Repeater ID="rptExistingCVFilesEdit" runat="server" OnItemDataBound="rptExistingCVFilesEdit_ItemDataBound" OnItemCommand="rptExistingCVFilesEdit_ItemCommand">
                            <ItemTemplate>
                                <div class="row" style="margin-bottom:5px;">
                                    <div class="col-sm-11">
                                        <asp:TextBox ID="hidKdFile" runat="server" Visible="false"/>
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
                        <asp:Repeater ID="rptCVFilesEdit" runat="server" OnItemDataBound="rptCVFiles_ItemDataBound" OnItemCommand="rptCVFilesEdit_ItemCommand">
                            <ItemTemplate>
                                <div class="row" style="margin-bottom:5px;">
                                    <div class="col-sm-11">
                                        <asp:FileUpload ID="fuCVFile" runat="server" CssClass="form-control fuWithTrigger" Placeholder="Upload CV File"></asp:FileUpload>                                        
                                        <asp:RequiredFieldValidator ID="reqCVFile" runat="server" ControlToValidate="fuCVFile" ErrorMessage="File must be filled" Display="Dynamic" ValidationGroup="EditKandidat"></asp:RequiredFieldValidator>                                        
                                    </div>
                                    <div class="col-sm-1">                                        
                                        <asp:LinkButton ID="btnDelete" ToolTip="Hapus" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                            <span class="fa fa-trash-o"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>                    
                    </div>
                    <div class="form-group">
                        <div class="row" style="margin:20px 0px;">
                            <div class="col-sm-12" style="padding-left:0px;">
                                <asp:LinkButton ID="btnAddKandidatFileEdit" ToolTip="Add More CV File" runat="server" CssClass="btn pull-left btn-success" OnClick="btnAddKandidatFileEdit_Click">
                                    <span class="fa fa-plus"></span> Add More CV File
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <label class="control-label col-sm-12">List of Applied Positions</label>
                            <asp:Repeater ID="rptPosisiKandidatEdit" runat="server" 
                                          OnItemDataBound="rptPosisiKandidatEdit_ItemDataBound" 
                                          OnItemCommand="rptPosisiKandidatEdit_ItemCommand"
                                          OnItemCreated="rptPosisiKandidatEdit_ItemCreated">
                                <HeaderTemplate>
                                    <table class="table table-stripped" style="margin-bottom:0px">
                                        <tr>
                                            <th>Division</th>
                                            <th width="380">Organizational Structure</th>
                                            <th>Position</th>
                                            <th>Recommendation</th>
                                            <th></th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlDivisiEditKandidat" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqDivisiEdit" InitialValue="0" ControlToValidate="ddlDivisiEditKandidat"
                                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic" ValidationGroup="EditKandidat">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSOEditKandidat" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqSOEdit" InitialValue="0" ControlToValidate="ddlSOEditKandidat"
                                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic" ValidationGroup="EditKandidat">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlJabatanEditKandidat" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqJabatanEdit" InitialValue="0" ControlToValidate="ddlJabatanEditKandidat"
                                                ErrorMessage="Field must be filled" runat="server" Display="Dynamic" ValidationGroup="EditKandidat">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNoRequest" runat="server" Visible="false"></asp:TextBox>
                                            <asp:HiddenField ID="hidRating" runat="server" />
                                            <act:Rating ID="ratingEditKandidat" runat="server" MaxRating="5" 
                                                StarCssClass="fa myRatingStar" 
                                                WaitingStarCssClass="fa-spinner"
                                                FilledStarCssClass="fa-star"
                                                EmptyStarCssClass="fa-star-o"></act:Rating>
                                            <asp:Button ID="btnTriggerRatingEdit" runat="server" Visible="false"/>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Width="35" CssClass="btn btn-sm btn-danger">
                                                <span class="fa fa-trash-o"></span>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row" style="margin:0px 0px 20px 0px;">
                        <div class="col-sm-12" style="padding-left:0px;">
                            <asp:LinkButton ID="btnTambahPosisiEdit" ToolTip="Add Position" runat="server" CssClass="btn pull-left btn-success" OnClick="btnTambahPosisiEdit_Click">
                                <span class="fa fa-plus"></span> Add Position
                            </asp:LinkButton>
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
                <i class="fa fa-save"></i>&nbsp; Save Changes
            </asp:LinkButton> 
        </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlConfirm" runat="server">
        <div class="modal-body">
            <asp:HiddenField ID="hidCommandArgument" runat="server"/>
            <asp:Literal ID="litConfirm" runat="server"></asp:Literal>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnYesConfirm" runat="server" CssClass="btn btn-warning" Text="Yes"
                OnClick="btnYesConfirm_Click" />
            <asp:Button ID="btnNoConfirm" runat="server" CssClass="btn btn-primary" Text="Cancel"
                OnClick="btnNoConfirm_Click" />
        </div>
    </asp:Panel>
</asp:Content>
