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
    public enum PopUpType
    {
        Default=0,
        Warning=1
    }

    public enum PopUpSize
    {
        Normal=0,
        Large=1
    }

    public partial class PopUpControl : System.Web.UI.UserControl
    {
        #region ViewState
        public string PopupControlID
        {
            set
            {
                ViewState["PopupControlID"] = value;
            }
            get
            {
                if (ViewState["PopupControlID"] == null)
                    return "";
                else
                    return ViewState["PopupControlID"].ToString().Trim();
            }
        }
        public string HeaderText
        {
            set
            {
                ViewState["HeaderText"] = value;
            }
            get
            {
                if (ViewState["HeaderText"] == null)
                    return "";
                else
                    return ViewState["HeaderText"].ToString().Trim();
            }
        }
        public bool EnableCloseBtn
        {
            set
            {
                ViewState["EnableCloseBtn"] = value;
            }
            get
            {
                if (ViewState["EnableCloseBtn"] == null)
                    return true;
                else
                    return (bool)ViewState["EnableCloseBtn"];
            }
        }

        public string BehaviorID
        {
            set
            {
                ViewState["BehaviorID"] = value;
            }
            get
            {
                if (ViewState["BehaviorID"] == null)
                    return "";
                else
                    return ViewState["BehaviorID"].ToString().Trim();
            }
        }

        public PopUpType Type 
        {
            get 
            {
                switch (pnlHeader.Attributes["class"])
                {
                    case "modal-header warning":
                        return PopUpType.Warning;
                    default:
                        return PopUpType.Default;
                }
            }
            set 
            { 
                switch(value)
                {
                    case PopUpType.Warning:
                        pnlHeader.Attributes["class"] = "modal-header warning";
                        iIndicator.Attributes["class"] = "fa fa-warning";
                        break;

                    default:
                        pnlHeader.Attributes["class"] = "modal-header";
                        iIndicator.Attributes["class"] = "";
                        break;
                }
            }
        }

        public PopUpSize Size
        {
            get
            {
                switch (divModalDialog.Attributes["class"])
                {
                    case "modal-dialog modal-lg":
                        return PopUpSize.Large;
                    default:
                        return PopUpSize.Normal;
                }
            }
            set
            {
                switch (value)
                {
                    case PopUpSize.Large:
                        divModalDialog.Attributes["class"] = "modal-dialog modal-lg";
                        break;

                    default:
                        divModalDialog.Attributes["class"] = "modal-dialog";
                        break;
                }
            }
        }

        #endregion

        /// <summary>
        /// Overrides the aspx page load event to move the pop up control
        /// as defined in the property PopupControlID
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Page.Load += new EventHandler(MovePopupControl);
        }

        protected void MovePopupControl(object sender, EventArgs e)
        {
            if (PopupControlID != "")
            {
                Control popupcontrol = this.NamingContainer.FindControl(PopupControlID);
                if (popupcontrol != null)
                {
                    phPopUpContent.Controls.Add(popupcontrol);
                }
            }

            if (BehaviorID != "")
            {
                mpePopUpControl.BehaviorID = BehaviorID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {    
                    
            //if (!EnableCloseBtn){
            //    imgCloseControl.Enabled = false;
            //    imgCloseControl.ImageUrl = "~/Common/images/disclose.gif";
            //}else{
            //    imgCloseControl.Enabled = true;
            //    imgCloseControl.ImageUrl = "~/Common/images/close.gif";
            //}

            litTitle.Text = HeaderText;
        }

        /// <summary>
        /// Show the pop up
        /// </summary>
        public void Show()
        {
            mpePopUpControl.Show();
            ScriptManager.RegisterStartupScript(this.Page, typeof(System.Web.UI.Page), "DisableBodyScroll", "$('html').css('overflow', 'hidden');", true);
        }

        /// <summary>
        /// Hide the pop up
        /// </summary>
        public void Hide()
        {
            mpePopUpControl.Hide();
            ScriptManager.RegisterStartupScript(this.Page, typeof(System.Web.UI.Page), "EnableBodyScroll", "$('html').css('overflow', 'auto');", true);
        }
        
        public void RegisterPostBackControl(LinkButton control)
        {
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = control.ID;
            uPnlPopUpContent.Triggers.Add(trigger);
        }
    }
}