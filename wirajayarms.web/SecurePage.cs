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
using WirajayaRMS.CrossCutting.OptManagement;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.CrossCutting.Security;
using WirajayaRMS.Business.ApplicationFacade;

namespace WirajayaRMS.Web
{
    public class SecurePage : System.Web.UI.Page
    {
        public Int32 MenuId
        {
            set
            {
                ViewState["MenuId"] = value;
            }
            get
            {
                if (ViewState["MenuId"] == null)
                    return 0;
                return Convert.ToInt32(ViewState["MenuId"]);
            }
        }

        protected UserData UserDataSession
        {
            get 
            { 
                return (UserData)Session[SessionNameFactory.UserData]; 
            }
            set 
            {
                Session[SessionNameFactory.UserData] = value; 
            }
        }

        protected void Page_Init(Object sender, EventArgs e)
        {
            if (Session[SessionNameFactory.UserData] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Request.QueryString["mid"] != null && Request.QueryString["mid"] != String.Empty)
            {   
                MenuId = Convert.ToInt32(Rijndael.Decrypt(HttpUtility.UrlDecode(Request.QueryString["mid"])));
                bool isCanAccess = new MenuSystem().CheckMenuAccess(UserDataSession.KdUser, MenuId);

                if (!(isCanAccess == true || UserDataSession.IsAdmin == 1 || MenuId == 0))
                {
                    Response.Redirect("~/Home.aspx");
                }
            }
            else if (!(HttpContext.Current.Request.Url.AbsolutePath.Contains("Home.aspx"))) 
            {
                Response.Redirect("~/Home.aspx");
            }
        }
    }
}
