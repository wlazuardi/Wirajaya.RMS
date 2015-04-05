using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WirajayaRMS.CrossCutting.OptManagement;
using WirajayaRMS.Business.Entities;
using System.Collections.Generic;
using WirajayaRMS.Business.ApplicationFacade;
using WirajayaRMS.CrossCutting.Security;

namespace WirajayaRMS.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private string _pageTitle;
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value;
                litPageTitle.Text = _pageTitle + " :: Wirajaya Recruitment Management System";
                litPageHeader.Text = _pageTitle;
                litPageName.Text = _pageTitle;
            }
        }

        private string _pageSubTitle;
        public string PageSubTitle
        {
            get
            {
                return _pageSubTitle;
            }
            set
            {
                _pageSubTitle = value;
                litPageSubHeader.Text = _pageSubTitle;
            }
        }

        public HtmlGenericControl BodyTag
        {
            get
            {
                return MasterPageBodyTag;
            }
            set
            {
                MasterPageBodyTag = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = _pageTitle + " :: Wirajaya Recruitment Management System";

            if (Session[SessionNameFactory.UserData] != null)
            {
                UserData _userData = (UserData)Session[SessionNameFactory.UserData];
                litUserNameLeft.Text = litUsernameTop.Text = _userData.FullName.Split(' ')[0];
                litUsernamePop.Text = _userData.FullName;

                if (_userData.PhotoFile == String.Empty)
                {
                    imgPhotoLeft.ImageUrl = imgPhotoRight.ImageUrl = "~/img/avatar-default.png";
                }
                else 
                {
                    imgPhotoLeft.ImageUrl = imgPhotoRight.ImageUrl = "~/Photo/" +_userData.PhotoFile;
                }

                if (!IsPostBack)
                {
                    linkAllNotif.NavigateUrl = "~/User/Notification.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0"));
                    linkPhotoRight.NavigateUrl = "~/User/Profile.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0"));
                    linkUsernameRight.NavigateUrl = "~/User/Profile.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0"));
                    linkProfile.NavigateUrl = "~/User/Profile.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0"));
                    linkPhotoLeft.NavigateUrl = "~/User/Profile.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0"));
                    linkUsernameLeft.NavigateUrl = "~/User/Profile.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0"));

                    List<MenuData> _menuList = new MenuSystem().GetMenuList(_userData.KdUser);

                    litMenu.Text = "<ul class='sidebar-menu'>";
                    foreach (MenuData _menuItem in _menuList)
                    {
                        if (_menuItem.ChildNode.Count > 0)
                        {
                            string menu =
                                @"<li class='treeview active'>
                                <a href='#'>
                                    <i class='" + _menuItem.MenuIcon + @"'></i>
                                    <span>" + _menuItem.NmMenu + @"</span>
                                    <i class='fa fa-angle-left pull-right'></i>
                                </a>
                                <ul class='treeview-menu'>";

                            foreach (MenuData _childItem in _menuItem.ChildNode)
                            {
                                menu += "<li><a href='" + Request.ApplicationPath.TrimEnd('/') + "/" + _childItem.Link + "?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_childItem.KdMenu.ToString())) + "'><i class='fa fa-angle-double-right'></i> " + _childItem.NmMenu + "</a></li>";
                            }

                            menu +=
                                    @"</ul>
                             </li>";

                            litMenu.Text += menu;
                        }
                        else
                        {
                            litMenu.Text +=
                                @"<li>
                            <a href='" + Request.ApplicationPath.TrimEnd('/') + "/" + _menuItem.Link + "?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_menuItem.KdMenu.ToString())) + @"'>
                                <i class='" + _menuItem.MenuIcon + @"'></i>
                                <span>" + _menuItem.NmMenu + @"</span>
                            </a>
                         </li>";
                        }
                    }
                    litMenu.Text += "</ul>";

                    List<NotificationData> _listNotif = new NotificationSystem().GetNotificationList(_userData.KdUser);
                    int _unreadNotif = new NotificationSystem().GetUnreadNotificationCount(_userData.KdUser);
                    if (_unreadNotif > 0)
                    {
                        lblNotifCount.Text = _unreadNotif.ToString();
                    }
                    rptNotif.DataSource = _listNotif.OrderByDescending(item => item.CreatedDate).ToList();
                    rptNotif.DataBind();

                    lblNotifCount2.Text = _unreadNotif.ToString();

                    List<UserAccessData> _listUserAccess = new UserAccessSystem().GetUserAccessListByKdUser(_userData.KdUser);
                    List<UserAccessData> _distinctList = _listUserAccess.GroupBy(item => item.LevelApproval.NmLevelApproval)
                                                            .Select(item => item.First())
                                                            .OrderBy(x => x.LevelApproval.KdLevelApproval)
                                                            .ToList();
                    litUserAccess.Text = "Access privilege: ";

                    if (_userData.IsAdmin == 1)
                    {
                        litUserAccess.Text += "Administrator";
                    }
                    else
                    {
                        foreach (UserAccessData _userAccess in _distinctList)
                        {
                            litUserAccess.Text += _userAccess.LevelApproval.NmLevelApproval + ", ";
                        }
                        litUserAccess.Text = litUserAccess.Text.Substring(0, litUserAccess.Text.Length - 2);
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }

        protected void rptNotif_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        { 
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor linkNotif = (HtmlAnchor)e.Item.FindControl("linkNotif");
                NotificationData notifData = (NotificationData)e.Item.DataItem;

                if (!notifData.IsRead) 
                    linkNotif.Attributes.Add("class", "unread");

                switch(notifData.KdTipe)
                {
                    case 1:
                        linkNotif.InnerHtml = "<i class=\"fa fa-inbox warning\"></i><div>" + notifData.Message + "<br /><span><i class=\"fa fa-clock-o\"></i> " + notifData.CreatedDate.ToString("dd MMM yy, h:m tt") + "</span></div>";
                        linkNotif.HRef = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.Argument)) + "&notif=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.KdNotification.ToString())) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                        break;
                    case 2:
                        linkNotif.InnerHtml = "<i class=\"fa fa-check success\"></i><div>" + notifData.Message + "<br /><span><i class=\"fa fa-clock-o\"></i> " + notifData.CreatedDate.ToString("dd MMM yy, h:m tt") + "</span></div>";
                        linkNotif.HRef = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.Argument)) + "&notif=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.KdNotification.ToString())) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                        break;
                    case 3:
                        linkNotif.InnerHtml = "<i class=\"fa fa-times danger\"></i><div>" + notifData.Message + "<br /><span><i class=\"fa fa-clock-o\"></i> " + notifData.CreatedDate.ToString("dd MMM yy, h:m tt") + "</span></div>";
                        linkNotif.HRef = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.Argument)) + "&notif=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.KdNotification.ToString())) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                        break;
                    case 4:
                        linkNotif.InnerHtml = "<i class=\"fa fa-level-up info\"></i><div>" + notifData.Message + "<br /><span><i class=\"fa fa-clock-o\"></i> " + notifData.CreatedDate.ToString("dd MMM yy, h:m tt") + "</span></div>";
                        linkNotif.HRef = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.Argument)) + "&notif=" + HttpUtility.UrlEncode(Rijndael.Encrypt(notifData.KdNotification.ToString())) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                        break;
                }
                
            }
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session.Remove(SessionNameFactory.UserData);
            Response.Redirect("~/Login.aspx");
        }
    }
}
