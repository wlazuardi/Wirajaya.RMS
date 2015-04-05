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
using WirajayaRMS.Business.Entities;
using WirajayaRMS.Business.ApplicationFacade;
using WirajayaRMS.CrossCutting.OptManagement;

namespace WirajayaRMS.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionNameFactory.UserData] != null)
                Response.Redirect("~/Home.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e) 
        {
            UserData _userData = new UserSystem().CheckLogin(txtUsername.Text);
            if (_userData != null)
            {
                string _cmpPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
                if (_cmpPassword.Equals(_userData.Password.ToUpper()))
                {
                    Session[SessionNameFactory.UserData] = _userData;
                    Response.Redirect("Home.aspx");
                }
                else 
                {
                    lblErrPassword.Visible = true;
                    lblErrPassword.Text = "Password not match";
                }
            }
            else 
            {
                lblErrUsername.Visible = true;
                lblErrUsername.Text = "Username not registered";
            }
        }
    }
}
