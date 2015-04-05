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
using WirajayaRMS.Business.Entities;
using WirajayaRMS.Business.ApplicationFacade;
using System.Collections.Generic;
using WirajayaRMS.Web.UserControl;

namespace WirajayaRMS.Web.Settings
{
    public partial class User : SecurePage
    {
        //#region ViewState
        //public List<MenuData> AllMenuList
        //{
        //    set { ViewState["AllMenuList"] = value; }
        //    get 
        //    {
        //        if (ViewState["AllMenuList"] == null)
        //        {
        //            return new List<MenuData>();
        //        }
        //        else 
        //        {
        //            return (List<MenuData>)ViewState["AllMenuList"];
        //        }
        //    }
        //}
        //#endregion


        private void BuildUserRepeater()
        {
            List<UserData> _listUser = new UserSystem().GetAllUserList();
            rptUser.DataSource = _listUser;
            rptUser.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "User settings & data-logging page";

            if (!IsPostBack)
            {
                BuildUserRepeater();

                List<MenuData> _menuList = new MenuSystem().GetMenuList();
                rptMenuList.DataSource = _menuList;
                rptMenuList.DataBind();
            }
        }

        protected void rptUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                UserData _userData = (UserData)e.Item.DataItem;
                Literal litUsername = (Literal)e.Item.FindControl("litUsername");
                Literal litFullName = (Literal)e.Item.FindControl("litFullName");
                Literal litEmail = (Literal)e.Item.FindControl("litEmail");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

                litFullName.Text = _userData.FullName;
                litUsername.Text = _userData.Username;
                litEmail.Text = _userData.Email;
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnEdit.CommandArgument = _userData.KdUser.ToString();
                btnDelete.CommandArgument = _userData.KdUser.ToString();

                if (_userData.IsAdmin == 1)
                {
                    litUsername.Text += "&nbsp;<small class=\"badge pull-right bg-yellow\"><i class=\"fa fa-star\"></i> admin</small>";
                }
            }
        }

        protected void rptUser_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "EDIT")
                {
                    List<MenuData> _menuList = new MenuSystem().GetMenuList();
                    rptMenuListEdit.DataSource = _menuList;
                    rptMenuListEdit.DataBind();

                    int kdUser = Convert.ToInt32(e.CommandArgument);
                    hidKdUserEdit.Value = e.CommandArgument.ToString();

                    UserData _userData = new UserSystem().GetUserData(kdUser);
                    txtUsernameEdit.Text = _userData.Username;
                    txtFullNameEdit.Text = _userData.FullName;
                    txtEmailEdit.Text = _userData.Email;
                    chkIsAdminEdit.Checked = _userData.IsAdmin == 1 ? true : false;
                    chkShowSalaryEdit.Checked = _userData.ShowSalary == 1 ? true : false;

                    List<MenuData> _userMenuList = new MenuSystem().GetMenuList(kdUser);
                    foreach (MenuData _menuData in _userMenuList)
                    {
                        foreach (RepeaterItem _item in rptMenuListEdit.Items)
                        {
                            CheckBox chkMenu = (CheckBox)_item.FindControl("chkMenu");
                            int kdMenu = Convert.ToInt32(chkMenu.Attributes["Value"]);

                            if (_menuData.KdMenu == kdMenu)
                            {
                                chkMenu.Checked = true;
                            }

                            // If there is inner menu
                            if (_menuData.ChildNode.Count > 0)
                            {
                                foreach (MenuData _innerMenuData in _menuData.ChildNode)
                                {
                                    Repeater rptInnerMenu = (Repeater)_item.FindControl("rptInnerMenu");
                                    if (rptInnerMenu.Items.Count > 0)
                                    {
                                        foreach (RepeaterItem _innerItem in rptInnerMenu.Items)
                                        {
                                            CheckBox chkInnerMenu = (CheckBox)_innerItem.FindControl("chkInnerMenu");
                                            int kdInnerMenu = Convert.ToInt32(chkInnerMenu.Attributes["Value"]);

                                            if (_innerMenuData.KdMenu == kdInnerMenu)
                                            {
                                                chkInnerMenu.Checked = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    popUpEditUser.Show();
                }
                else if (e.CommandName == "DELETE")
                {
                    int kdUser = Convert.ToInt32(e.CommandArgument);
                    hidKdUserEdit.Value = e.CommandArgument.ToString();
                    UserData _userData = new UserSystem().GetUserData(kdUser);
                    lblConfirm.Text = "Are you sure want to delete thsi user: <strong>" + _userData.FullName + "</strong>? <br/> Your action cannot be undone.";
                    popUpConfirm.Show();
                }
            }
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            popUpAddUser.Show();
            txtConfPassword.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtFullName.Text = "";
            txtUsername.Text = "";
            chkIsAdmin.Checked = false;
            chkShowSalary.Checked = false;
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {

                UserData _userData = new UserData();
                _userData.Username = txtUsername.Text;
                _userData.FullName = txtFullName.Text;
                _userData.Email = txtEmail.Text;
                _userData.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
                _userData.IsAdmin = chkIsAdmin.Checked == true ? 1 : 0;
                _userData.ShowSalary = chkShowSalary.Checked == true ? 1 : 0;

                int kdUser = new UserSystem().AddUser(_userData);
                if (kdUser > 0)
                {
                    int menuAccessDelete = new MenuSystem().DeleteMenuAccess(kdUser);

                    foreach (RepeaterItem item in rptMenuList.Items)
                    {
                        CheckBox chkMenu = (CheckBox)item.FindControl("chkMenu");
                        if (chkMenu.Checked)
                        {
                            int kdMenu = Convert.ToInt32(chkMenu.Attributes["Value"]);
                            int mrnuAccessResult = new MenuSystem().AddMenuAccess(kdUser, kdMenu);
                        }

                        Repeater rptInnerMenu = (Repeater)item.FindControl("rptInnerMenu");
                        if (rptInnerMenu.Items.Count > 0)
                        {
                            foreach (RepeaterItem innerItem in rptInnerMenu.Items)
                            {
                                CheckBox chkInnerMenu = (CheckBox)innerItem.FindControl("chkInnerMenu");
                                if (chkInnerMenu.Checked)
                                {
                                    int kdMenuInner = Convert.ToInt32(chkInnerMenu.Attributes["Value"]);
                                    int mrnuAccessResult = new MenuSystem().AddMenuAccess(kdUser, kdMenuInner);
                                }
                            }
                        }
                    }

                    alertNotification.Show("User data saved successfully", AlertType.Success);
                    BuildUserRepeater();
                    popUpAddUser.Hide();
                }
                else
                {
                    alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Success);
                }

            }
            catch (Exception)
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Success);
            }
        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidKdUserEdit.Value != null && hidKdUserEdit.Value != "")
                {
                    UserData _userData = new UserData();
                    _userData.KdUser = Convert.ToInt32(hidKdUserEdit.Value);
                    _userData.Username = txtUsernameEdit.Text;
                    _userData.FullName = txtFullNameEdit.Text;
                    _userData.Email = txtEmailEdit.Text;
                    _userData.IsAdmin = chkIsAdminEdit.Checked == true ? 1 : 0;
                    _userData.ShowSalary = chkShowSalaryEdit.Checked == true ? 1 : 0;

                    int kdUser = new UserSystem().EditUser(_userData);
                    if (kdUser > 0)
                    {
                        int menuAccessDelete = new MenuSystem().DeleteMenuAccess(kdUser);

                        foreach (RepeaterItem item in rptMenuListEdit.Items)
                        {
                            CheckBox chkMenu = (CheckBox)item.FindControl("chkMenu");
                            if (chkMenu.Checked)
                            {
                                int kdMenu = Convert.ToInt32(chkMenu.Attributes["Value"]);
                                int mrnuAccessResult = new MenuSystem().AddMenuAccess(kdUser, kdMenu);
                            }

                            Repeater rptInnerMenu = (Repeater)item.FindControl("rptInnerMenu");
                            if (rptInnerMenu.Items.Count > 0)
                            {
                                foreach (RepeaterItem innerItem in rptInnerMenu.Items)
                                {
                                    CheckBox chkInnerMenu = (CheckBox)innerItem.FindControl("chkInnerMenu");
                                    if (chkInnerMenu.Checked)
                                    {
                                        int kdMenuInner = Convert.ToInt32(chkInnerMenu.Attributes["Value"]);
                                        int mrnuAccessResult = new MenuSystem().AddMenuAccess(kdUser, kdMenuInner);
                                    }
                                }
                            }
                        }

                        alertNotification.Show("User data updated successfully", AlertType.Success);
                        popUpEditUser.Hide();
                        BuildUserRepeater();
                    }
                    else
                    {
                        alertNotification.Show("Failed to update the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
                    }
                }
            }
            catch (Exception)
            {
                alertNotification.Show("Failed to update the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (hidKdUserEdit.Value != null && hidKdUserEdit.Value != "")
            {
                int kdUser = Convert.ToInt32(hidKdUserEdit.Value);
                string newPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPasswordEdit.Text, "sha1");

                int result = new UserSystem().ChangePassword(kdUser, newPassword);
                if (result > 0)
                {
                    alertNotification.Show("User password changed successfully", AlertType.Success);
                    popUpEditUser.Hide();
                }
                else
                {
                    alertNotification.Show("Failed to change user password. Please re-check the data you input or try again in a moment", AlertType.Danger);
                }
            }
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e)
        {
            if (hidKdUserEdit.Value != null && hidKdUserEdit.Value != "")
            {
                int kdUser = Convert.ToInt32(hidKdUserEdit.Value);

                int result = new UserSystem().DeleteUser(kdUser);

                if (result > 0)
                {
                    alertNotification.Show("User deleted successfully", AlertType.Success);
                    popUpConfirm.Hide();
                    BuildUserRepeater();
                }
                else
                {
                    alertNotification.Show("Failed to delete user", AlertType.Danger);
                }
            }
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            popUpConfirm.Hide();
        }

        protected void rptMenuList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkMenu = (CheckBox)e.Item.FindControl("chkMenu");
                MenuData item = (MenuData)e.Item.DataItem;

                chkMenu.Text = item.NmMenu;
                chkMenu.Attributes.Add("Value", item.KdMenu.ToString());

                if (item.ChildNode.Count > 0)
                {
                    Repeater rptInnerMenu = (Repeater)e.Item.FindControl("rptInnerMenu");
                    rptInnerMenu.DataSource = item.ChildNode;
                    rptInnerMenu.DataBind();
                }
            }
        }

        protected void rptInnerMenuList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkInnerMenu = (CheckBox)e.Item.FindControl("chkInnerMenu");
                MenuData item = (MenuData)e.Item.DataItem;

                chkInnerMenu.Text = item.NmMenu;
                chkInnerMenu.Attributes.Add("Value", item.KdMenu.ToString());
            }
        }
    }
}
