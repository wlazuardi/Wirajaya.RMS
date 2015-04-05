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

namespace WirajayaRMS.Web.UserControl
{
    public enum AlertType
    {
        Info = 0, 
        Danger = 1,
        Warning = 2,
        Success = 4
    }

    public partial class AlertControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// Set alert control to be visible or not
        /// </summary>
        public bool Visible
        {
            set { pnlAlert.Visible = value; }
            get { return pnlAlert.Visible; }
        }

        /// <summary>
        /// Alert type can be various: Info (default), Danger, Error, or Warning
        /// </summary>
        public AlertType Type
        {
            get 
            {
                switch (pnlAlert.CssClass) 
                { 
                    case "alert alert-info":
                        return AlertType.Info;

                    case "alert alert-danger":
                        return AlertType.Danger;

                    case "alert alert-warning":
                        return AlertType.Warning;

                    case "alert alert-success":
                        return AlertType.Success;

                    default:
                        return AlertType.Info;
                }
            }
            set 
            { 
                switch (value)
                {
                    case AlertType.Info:
                        pnlAlert.CssClass = "alert alert-info";
                        iIndicator.Attributes["class"] = "fa fa-info";
                        break;
                    case AlertType.Danger:
                        pnlAlert.CssClass = "alert alert-danger";
                        iIndicator.Attributes["class"] = "fa fa-ban";
                        break;
                    case AlertType.Warning:
                        pnlAlert.CssClass = "alert alert-warning";
                        iIndicator.Attributes["class"] = "fa fa-warning";
                        break;
                    case AlertType.Success:
                        pnlAlert.CssClass = "alert alert-success";
                        iIndicator.Attributes["class"] = "fa fa-check";
                        break;
                    default:
                        pnlAlert.CssClass = "alert alert-info";
                        iIndicator.Attributes["class"] = "fa fa-info";
                        break;
                }
            }
        }

        /// <summary>
        /// Text to be displayed on alert
        /// </summary>
        public string Text
        {
            set { lblError.Text = value; }
            get { return lblError.Text; }
        }

        /// <summary>
        /// Set true if alert can have close button
        /// </summary>
        public bool Dismissable
        {
            get 
            {
                if (btnDismissAlert.Attributes.CssStyle["display"] == "none")
                {
                    return false;
                }
                else 
                {
                    return true;
                }
            }
            set 
            {
                if (value == false)
                {
                    btnDismissAlert.Attributes.CssStyle["display"] = "none";
                }
                else 
                {
                    btnDismissAlert.Attributes.CssStyle["display"] = "block";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Type == null) 
                Type = AlertType.Info;

            if (Dismissable == null)
                Dismissable = false;
        }

        protected void btnDismissAlert_Click(object sender, EventArgs e)
        {
            pnlAlert.Visible = false;
        }

        /// <summary>
        /// Show the alert box
        /// </summary>
        /// <param name="message">Message text to be displayed</param>
        /// <param name="alertType">Alert type (Info, Danger, Warning, or Success)</param>
        public void Show(string message, AlertType alertType) 
        {
            this.Text = message;
            this.Type = alertType;
            this.Visible = true;
        }

        /// <summary>
        /// Hide the alert box
        /// </summary>
        public void Hide() 
        {
            this.Visible = false;
        }
    }
}