using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.Business.ApplicationFacade;
using WirajayaRMS.Web.UserControl;

namespace WirajayaRMS.Web.Settings
{
    public partial class UserAccess : SecurePage
    {
        #region Private Method
        private void BuildSODropdown(DropDownList dropdown, int kdDivisi)
        {
            List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiSystem().GetTopManagementListStrukturOrganisasi(kdDivisi);

            dropdown.Items.Clear();

            foreach (StrukturOrganisasiData _itemSO in _listSO)
            {
                string _value = _itemSO.KdSO;
                string _text = _itemSO.NmStrukturOrganisasi;

                dropdown.Items.Add(new ListItem(_value + ". " + _text, _value));

                if (_itemSO.ChildNode.Count > 0)
                {
                    BuildSOChildNode(_itemSO, 1, dropdown);
                }
            }

            dropdown.Items.Insert(0, new ListItem("-- Select Organizational Structure --", "0"));
            dropdown.SelectedIndex = 0;
        }

        private void BuildSOChildNode(StrukturOrganisasiData _node, int _depth, DropDownList dropdown)
        {
            foreach (StrukturOrganisasiData _item in _node.ChildNode)
            {
                string _value = _item.KdSO;
                string _text = _item.NmStrukturOrganisasi;

                string _space = "";
                for (int i = 0; i < _depth; i++)
                {
                    _space += "&nbsp;&nbsp;&nbsp;";
                }

                dropdown.Items.Add(new ListItem(HttpUtility.HtmlDecode(_space) + _value + ". " + _text, _value));

                if (_item.ChildNode.Count > 0)
                {
                    BuildSOChildNode(_item, _depth + 1, dropdown);
                }
            }
        }

        private void BuildLevelApprovalDropdown(DropDownList dropdown, int kdDivisi)
        {
            List<LevelApprovalData> _listLvApproval = new LevelApprovalSystem().GetLevelApprovalList(kdDivisi);
            dropdown.DataSource = _listLvApproval;
            dropdown.DataTextField = "KdNmLevelApproval";
            dropdown.DataValueField = "KdLevelApproval";
            dropdown.DataBind();

            dropdown.Items.Insert(0, new ListItem("-- Select Level Approval --", "0"));
            dropdown.SelectedIndex = 0;
        }

        private void BuildUserAccessRepeater()
        {
            int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            int kdLevelApproval = Convert.ToInt32(ddlLevelApproval.SelectedValue);
            string kdSO = ddlStrukturOrganisasi.SelectedValue == "0" ? null : ddlStrukturOrganisasi.SelectedValue;
            List<UserAccessData> _listUserAccess = new UserAccessSystem().GetUserAccessList(kdDivisi, kdSO, kdLevelApproval);
            rptUserAccess.DataSource = _listUserAccess;
            rptUserAccess.DataBind();
        }

        private void ResetAddEditForm() 
        {
            txtUsername.Text = "";
            ddlDivisiAddEdit.SelectedIndex = 0;
            ddlLevelApprovalAddEdit.SelectedIndex = 0;
            ddlStrukturOrganisasiAddEdit.SelectedIndex = 0;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "User access settings page";

            if (!IsPostBack)
            {
                List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList();
                ddlDivisi.DataSource = _listDivisi;
                ddlDivisi.DataTextField = "KdNmDivisi";
                ddlDivisi.DataValueField = "KdDivisi";
                ddlDivisi.DataBind();
                ddlDivisi.Items.Insert(0, new ListItem("-- Select Division --", "0"));
                ddlDivisi.SelectedIndex = 0;


                ddlDivisiAddEdit.DataSource = _listDivisi;
                ddlDivisiAddEdit.DataTextField = "KdNmDivisi";
                ddlDivisiAddEdit.DataValueField = "KdDivisi";
                ddlDivisiAddEdit.DataBind();
                ddlDivisiAddEdit.Items.Insert(0, new ListItem("-- Select Division --", "0"));
                ddlDivisiAddEdit.SelectedIndex = 0;

                int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                BuildSODropdown(ddlStrukturOrganisasi, kdDivisi);
                BuildLevelApprovalDropdown(ddlLevelApproval, kdDivisi);
                BuildSODropdown(ddlStrukturOrganisasiAddEdit, kdDivisi);
                BuildLevelApprovalDropdown(ddlLevelApprovalAddEdit, kdDivisi);
                BuildUserAccessRepeater();
            }
        }

        protected void ddlDivisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            BuildSODropdown(ddlStrukturOrganisasi, kdDivisi);
            BuildLevelApprovalDropdown(ddlLevelApproval, kdDivisi);
            BuildUserAccessRepeater();
        }

        protected void ddlLevelApproval_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildUserAccessRepeater();
        }

        protected void ddlStrukturOrganisasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildUserAccessRepeater();
        }

        protected void rptUserAccess_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                UserAccessData _userAccess = (UserAccessData)e.Item.DataItem;
                Literal litDivisi = (Literal)e.Item.FindControl("litDivisi");
                Literal litUsername = (Literal)e.Item.FindControl("litUsername");
                Literal litLevelApproval = (Literal)e.Item.FindControl("litLevelApproval");
                Literal litStrukturOrganisasi = (Literal)e.Item.FindControl("litStrukturOrganisasi");
                CheckBox chkPendataan = (CheckBox)e.Item.FindControl("chkPendataan");
                CheckBox chkSetting = (CheckBox)e.Item.FindControl("chkSetting");
                CheckBox chkRequest = (CheckBox)e.Item.FindControl("chkRequest");
                CheckBox chkKandidat = (CheckBox)e.Item.FindControl("chkKandidat");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

                litDivisi.Text = _userAccess.Divisi.NmDivisi;
                litUsername.Text = _userAccess.User.Username;
                litLevelApproval.Text = _userAccess.LevelApproval.NmLevelApproval;
                litStrukturOrganisasi.Text = _userAccess.StrukturOrganisasi.NmStrukturOrganisasi;
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnEdit.CommandArgument = _userAccess.User.KdUser + "-" + _userAccess.Divisi.KdDivisi + "-" + _userAccess.StrukturOrganisasi.KdSO + "-" + _userAccess.LevelApproval.KdLevelApproval;
                btnDelete.CommandArgument = _userAccess.User.KdUser + "-" + _userAccess.Divisi.KdDivisi + "-" + _userAccess.StrukturOrganisasi.KdSO + "-" + _userAccess.LevelApproval.KdLevelApproval;

            }
        }

        protected void rptUserAccess_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "EDIT")
                {
                    ResetAddEditForm();
                    
                    // Hidden value untuk edit
                    hidArgument.Value = e.CommandArgument.ToString();

                    string[] _param = e.CommandArgument.ToString().Split('-');
                    int _kdUser = Convert.ToInt32(_param[0]);
                    int _kdDivisi = Convert.ToInt32(_param[1]);
                    string _kdSO = _param[2];
                    int _kdLevelApproval = Convert.ToInt32(_param[3]);

                    UserAccessData _userAccessData = new UserAccessSystem().GetUserAccessData(_kdUser, _kdDivisi, _kdSO, _kdLevelApproval);
                    txtUsername.Text = _userAccessData.User.Username;
                    ddlDivisiAddEdit.SelectedValue = _userAccessData.Divisi.KdDivisi.ToString();
                    BuildSODropdown(ddlStrukturOrganisasiAddEdit, _kdDivisi);
                    ddlStrukturOrganisasiAddEdit.SelectedValue = _userAccessData.StrukturOrganisasi.KdSO;
                    BuildLevelApprovalDropdown(ddlLevelApprovalAddEdit, _kdDivisi);
                    ddlLevelApprovalAddEdit.SelectedValue = _userAccessData.LevelApproval.KdLevelApproval.ToString();

                    popUpAddEditUserAccess.Show();
                    btnSaveAddUserAcces.Text = "Save";
                    popUpAddEditUserAccess.HeaderText = "Edit User Access";
                }
                else if (e.CommandName == "DELETE")
                {
                    string[] _param = e.CommandArgument.ToString().Split('-');
                    int _kdUser = Convert.ToInt32(_param[0]);
                    int _kdDivisi = Convert.ToInt32(_param[1]);
                    string _kdSO = _param[2];
                    int _kdLevelApproval = Convert.ToInt32(_param[3]);
                    
                    UserAccessData _userAccessData = new UserAccessSystem().GetUserAccessData(_kdUser, _kdDivisi, _kdSO, _kdLevelApproval);

                    lblConfirm.Text = "Are you sure want to delete the access for user: <strong>" + _userAccessData.User.Username + "</strong>?";
                    hfConfirmArgument.Value = e.CommandArgument.ToString();
                    popUpConfirm.Show();
                }
            }
        }

        protected void btnAddNewAccess_Click(object sender, EventArgs e)
        {
            hidArgument.Value = "";
            ResetAddEditForm();
            popUpAddEditUserAccess.Show();
            btnSaveAddUserAcces.Text = "Tambah";
            popUpAddEditUserAccess.HeaderText = "Add New User Access";
        }

        protected void btnSaveAddUserAcces_Click(object sender, EventArgs e)
        {
            UserAccessData _userAccessData = new UserAccessData();
            _userAccessData.User = new UserData();
            _userAccessData.User.Username = txtUsername.Text;
            _userAccessData.Divisi = new DivisiData();
            _userAccessData.Divisi.KdDivisi = Convert.ToInt32(ddlDivisiAddEdit.SelectedValue);
            _userAccessData.StrukturOrganisasi = new StrukturOrganisasiData();
            _userAccessData.StrukturOrganisasi.KdSO = ddlStrukturOrganisasiAddEdit.SelectedValue;
            _userAccessData.LevelApproval = new LevelApprovalData();
            _userAccessData.LevelApproval.KdLevelApproval = ddlLevelApprovalAddEdit.SelectedValue;

            if (hidArgument.Value != "")
            {
                string[] _param = hidArgument.Value.Split('-');
                int _kdUser = Convert.ToInt32(_param[0]);
                int _kdDivisi = Convert.ToInt32(_param[1]);
                string _kdSO = _param[2];
                int _kdLevelApproval = Convert.ToInt32(_param[3]);

                int deleteResult = new UserAccessSystem().DeleteUserAccess(_kdUser, _kdDivisi, _kdSO, _kdLevelApproval);
            }

            int result = new UserAccessSystem().AddEditUserAccess(_userAccessData);
            if (result > 0)
            {
                alertNotification.Show("User access data saved successfully", AlertType.Success);
                BuildUserAccessRepeater();
            }
            else
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }
            popUpAddEditUserAccess.Hide();
        }

        protected void ddlDivisiAddEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kdDivisi = Convert.ToInt32(ddlDivisiAddEdit.SelectedValue);
            BuildSODropdown(ddlStrukturOrganisasiAddEdit, kdDivisi);
            BuildLevelApprovalDropdown(ddlLevelApprovalAddEdit, kdDivisi);
        }

        protected void btnShowSearchUser_Click(object sender, EventArgs e) 
        {
            popUpSearchUser.Show();

            txtUsernameSearch.Text = "";
            txtFullNameSearch.Text = "";
            txtEmailSearch.Text = "";

            rptUserSearch.DataSource = null;
            rptUserSearch.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string _username = txtUsernameSearch.Text;
            string _fullName = txtFullNameSearch.Text;
            string _email = txtEmailSearch.Text;

            List<UserData> _listUserSearch = new UserSystem().SearchUser(_username, _fullName, _email);
            rptUserSearch.DataSource = _listUserSearch;
            rptUserSearch.DataBind();
        }

        protected void rptUserSearch_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
            {
                UserData _searchItem = (UserData)e.Item.DataItem;

                Literal litUsername = (Literal)e.Item.FindControl("litUsername");
                Literal litFullName = (Literal)e.Item.FindControl("litFullName");
                Literal litEmail = (Literal)e.Item.FindControl("litEmail");
                LinkButton btnChoose = (LinkButton)e.Item.FindControl("btnChoose");

                litUsername.Text = _searchItem.Username;
                litFullName.Text = _searchItem.FullName;
                litEmail.Text = _searchItem.Email;
                btnChoose.CommandName = "CHOOSE";
                btnChoose.CommandArgument = _searchItem.Username;
            }
        }

        protected void rptUserSearch_OnItemCommand(object sender, RepeaterCommandEventArgs e) 
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
            {
                if (e.CommandName == "CHOOSE") 
                {
                    txtUsername.Text = e.CommandArgument.ToString();
                    popUpSearchUser.Hide();
                    popUpAddEditUserAccess.Show();
                }
            }
        }

        protected void btnCancelSearch_Click(object sender, EventArgs e) 
        {
            popUpAddEditUserAccess.Show();
            popUpSearchUser.Hide();
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e)
        {
            if (hfConfirmArgument.Value != null && hfConfirmArgument.Value != "")
            {
                string[] _param = hfConfirmArgument.Value.Split('-');
                int _kdUser = Convert.ToInt32(_param[0]);
                int _kdDivisi = Convert.ToInt32(_param[1]);
                string _kdSO = _param[2];
                int _kdLevelApproval = Convert.ToInt32(_param[3]);

                int result = new UserAccessSystem().DeleteUserAccess(_kdUser, _kdDivisi, _kdSO, _kdLevelApproval);

                if (result > 0)
                {
                    alertNotification.Show("User access data deleted successfully", AlertType.Success);
                    popUpConfirm.Hide();
                    BuildUserAccessRepeater();
                }
                else
                {
                    alertNotification.Show("Failed to delete the data", AlertType.Danger);
                }
            }
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            popUpConfirm.Hide();
        }
    }
}
