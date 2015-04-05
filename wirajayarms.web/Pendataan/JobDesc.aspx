<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobDesc.aspx.cs"
    Inherits="WirajayaRMS.Web.Pendataan.JobDesc" Title="Job Description" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../UserControl/AlertControl.ascx" TagName="AlertControl" TagPrefix="uc" %>
<%@ Register Src="../UserControl/PopUpControl.ascx" TagName="PopUpControl" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <%--<uc:PopUpControl ID="popUpConfirm" runat="server" HeaderText="Konfirmasi" 
        BehaviorID="popUpConfirm" PopupControlID="pnlConfirm" Type="Warning"/>--%>
        
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">
                                Job Description List</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>
                        <div class="box-body form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-sm-2">Division</label>
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
                                    <asp:RequiredFieldValidator ID="reqStrukturOrganisasi" runat="server" ControlToValidate="ddlStrukturOrganisasi"
                                        ErrorMessage="Field must be filled" Display="Dynamic" InitialValue="0" ValidationGroup="AddJobDesc">
                                    </asp:RequiredFieldValidator>
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
                                    <asp:RequiredFieldValidator ID="reqJabatan" runat="server" ControlToValidate="ddlJabatan"
                                        ErrorMessage="Field must be filled" Display="Dynamic" InitialValue="0" ValidationGroup="AddJobDesc">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    Job Description List
                                </label>
                                <div class="col-sm-9">
                                    <asp:Repeater ID="rptJobDesc" runat="server" 
                                        OnItemDataBound="rptJobDesc_ItemDataBound" 
                                        onitemcommand="rptJobDesc_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="table table-striped">
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
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.ItemIndex + 1 %>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="litJobDesc" runat="server"></asp:Literal>
                                                    <asp:TextBox ID="txtEditJobDesc" runat="server" TextMode="MultiLine" Visible="false" Width="100%"></asp:TextBox>
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
                                                    <asp:LinkButton ID="btnSave" ToolTip="Save" runat="server" Width="35" CssClass="btn btn-sm btn-success" Visible="false">
                                                        <span class="fa fa-save"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" ToolTip="Cancel" runat="server" Width="35" CssClass="btn btn-sm btn-warning" Visible="false">
                                                        <span class="fa fa-times"></span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <p>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="text-yellow"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    &nbsp;
                                </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtJobDesc" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqJobDesc" runat="server" ControlToValidate="txtJobDesc"
                                        ErrorMessage="Field must be filled" ValidationGroup="AddJobDesc"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-2">
                                    &nbsp;
                                </label>
                                <div class="col-sm-9">
                                    <uc:AlertControl ID="alertNotification" runat="server" Visible="false" Dismissable="true" />
                                    <asp:Button ID="btnAddJobDesc" runat="server" Text="Add Job Description" OnClick="btnAddJobDesc_Click"
                                        ValidationGroup="AddJobDesc" CssClass="btn btn-primary pull-right" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%--Popup untuk konfirmasi delete--%>
    <%--<asp:Panel ID="pnlConfirm" runat="server">
        <div class="modal-body">
            <asp:Label ID="lblConfirm" runat="server"></asp:Label>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnYesConfirm" runat="server" CssClass="btn btn-warning" Text="Ya"
                OnClick="btnYesConfirm_Click" />
            <asp:Button ID="btnNoConfirm" runat="server" CssClass="btn btn-primary" Text="Tidak"
                OnClick="btnNoConfirm_Click" />
        </div>
    </asp:Panel>--%>
</asp:Content>
