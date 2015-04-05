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
using WirajayaRMS.CrossCutting.Security;
using WirajayaRMS.Business.ApplicationFacade;
using WirajayaRMS.Business.Entities;

namespace WirajayaRMS.Web
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["t"] == null || Request.QueryString["t"].Trim() == String.Empty ||
               Request.QueryString["uid"] == null || Request.QueryString["uid"].Trim() == String.Empty)
            {
                Response.Redirect("~/Login.aspx");
            }

            string token = Rijndael.Decrypt(HttpUtility.HtmlDecode(Request.QueryString["t"].Trim()));
            int kdUser = Convert.ToInt32(Rijndael.Decrypt(HttpUtility.HtmlDecode(Request.QueryString["uid"].Trim())));

            UserData _userData = new UserSystem().GetUserData(kdUser);
            litUser.Text = _userData.FullName;

            if (token != new NotificationSystem().CheckResetPasswordToken(kdUser))
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnChangePass_Click(object sender, EventArgs e) 
        {
            int kdUser = Convert.ToInt32(Rijndael.Decrypt(HttpUtility.HtmlDecode(Request.QueryString["uid"].Trim())));
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "sha1");
            int result = new UserSystem().ChangePassword(kdUser, password);
            if (result > 0)
            {
                int rDeleteToken = new NotificationSystem().DeleteChangePasswordToken(kdUser);
                Response.Redirect("~/Login.aspx");
            }
            else 
            {
                lblError.Text = "Failed to change password";
            }
        }
    }
}
