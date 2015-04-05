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
using WirajayaRMS.Business.ApplicationFacade;
using WirajayaRMS.Business.Entities;
using System.Collections.Generic;
using WirajayaRMS.CrossCutting.Security;

namespace WirajayaRMS.Web.User
{
    public partial class Notification : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageTitle = Page.Title;
                Master.PageSubTitle = "Your notification list";

                List<NotificationData> _notificationList = new NotificationSystem().GetAllNotificationList(UserDataSession.KdUser);
                Dictionary<string,List<NotificationData>> result = _notificationList.GroupBy(x => x.CreatedDate.ToString("MMM yyyy")).ToDictionary(g => g.Key, g => g.ToList());
                rptNotificationList.DataSource = result;
                rptNotificationList.DataBind();
            }
        }

        protected void rptNotificationList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlGenericControl spanMonth = (HtmlGenericControl)e.Item.FindControl("spanMonth");
                Repeater rptInnerNotificationList = (Repeater)e.Item.FindControl("rptInnerNotificationList");
                
                KeyValuePair<string, List<NotificationData>> _dictionary = (KeyValuePair<string, List<NotificationData>>)e.Item.DataItem;
                spanMonth.Attributes.Add("class", "bg-blue");
                spanMonth.InnerHtml = _dictionary.Key;
                rptInnerNotificationList.DataSource = _dictionary.Value;
                rptInnerNotificationList.DataBind();
            }
        }

        protected void rptInnerNotificationList_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                NotificationData _notificationData = (NotificationData)e.Item.DataItem;

                HtmlGenericControl divItem = (HtmlGenericControl)e.Item.FindControl("divItem");
                HtmlGenericControl spanIcon = (HtmlGenericControl)e.Item.FindControl("spanIcon");
                HyperLink linkMessage = (HyperLink)e.Item.FindControl("linkMessage");
                Literal litDate = (Literal)e.Item.FindControl("litDate");

                switch (_notificationData.KdTipe)
                {
                    case 1:
                        spanIcon.Attributes.Add("class", "fa fa-inbox bg-yellow");
                        break;
                    case 2:
                        spanIcon.Attributes.Add("class", "fa fa-check bg-green");
                        break;
                    case 3:
                        spanIcon.Attributes.Add("class", "fa fa-times bg-red");
                        break;
                    case 4:
                        spanIcon.Attributes.Add("class", "fa fa-level-up bg-aqua");
                        break;

                }

                linkMessage.Text = _notificationData.Message.Replace("<br />", "&nbsp;&nbsp;&nbsp;");
                linkMessage.NavigateUrl = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.HtmlEncode(Rijndael.Encrypt(_notificationData.Argument)) + "&notif=" + HttpUtility.HtmlEncode(Rijndael.Encrypt(_notificationData.KdNotification.ToString())) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString()));
                litDate.Text = _notificationData.CreatedDate.ToString("dd MMM yy, h:m tt");

                if (!_notificationData.IsRead) 
                {
                    divItem.Attributes.Add("style", "background:#E1E9FC;border: 1px solid #A8E2F7;");
                }
            }
        }
    }
}
