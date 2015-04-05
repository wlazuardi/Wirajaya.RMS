using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.ComponentModel;

namespace WirajayaRMS.Web
{
    public class ExRating : Rating
    {
        [Category("Behavior")]
        public string CommandName
        {
            get { return (string)ViewState["CommandName"] ?? string.Empty; }
            set { ViewState["CommandName"] = value; }
        }

        [Category("Behavior")]
        public string CommandArgument
        {
            get { return (string)ViewState["CommandArgument"] ?? string.Empty; }
            set { ViewState["CommandArgument"] = value; }
        }

        protected override void OnChanged(RatingEventArgs e)
        {
            base.OnChanged(e);
            RaiseBubbleEvent(this, new CommandEventArgs(CommandName, CommandArgument));
        }

    }
}
