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
using System.Collections.Generic;
using System.IO;
using WirajayaRMS.CrossCutting.OptManagement;
using WirajayaRMS.CrossCutting.Security;

namespace WirajayaRMS.Web
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e) 
        {
            List<UserData> _data = new UserSystem().SearchUser("", "", txtEmail.Text.Trim());
            pnlSearchEmail.Visible = false;
            pnlSendInstruction.Visible = true;
            if (_data.Count > 0)
            {
                litSearchResult.Text = "User registered with e-mail address <b>" + txtEmail.Text + "</b>";
                rblUser.Visible = true;
                ccVerification.Visible = true;
                txtCapthca.Visible = true;
                btnSendInstruction.Visible = true;
            }
            else 
            {
                litSearchResult.Text = "There's no registered user found with e-mail address <b>" + txtEmail.Text + "</b>";
                rblUser.Visible = false;
                ccVerification.Visible = false;
                txtCapthca.Visible = false;
                btnSendInstruction.Visible = false;
            }

            rblUser.DataSource = _data;
            rblUser.DataTextField = "FullName";
            rblUser.DataValueField = "KdUser";
            rblUser.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e) 
        {
            pnlSearchEmail.Visible = true;
            pnlSendInstruction.Visible = false;
        }

        protected void btnSendInstruction_Click(object sender, EventArgs e) 
        {
            ccVerification.ValidateCaptcha(txtCapthca.Text);
            if (ccVerification.UserValidated)
            {
                lblError.Text = "";

                // Ambil template email untuk kiriim notifikasi
                StreamReader sr = new StreamReader(Server.MapPath("~/MailTemplate/ForgotPasswordMailTemplate.htm"));
                string template = sr.ReadToEnd();
                sr.Close();

                int kdUser = Convert.ToInt32(rblUser.SelectedValue);

                string token = "";
                string registeredToken = new NotificationSystem().CheckResetPasswordToken(kdUser);
                if (registeredToken != null && registeredToken != String.Empty) 
                { 
                    token = registeredToken;
                }
                else
                {
                    token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                }


                template = template.Replace("##Link##", "<a href='" + SystemConfiguration.WebDomainName + "/ResetPassword.aspx?t=" + HttpUtility.UrlEncode(Rijndael.Encrypt(token)) + "&uid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(kdUser.ToString())) + "'>Please click here to change your password</a>");

                int result = new NotificationSystem().NotifyResetPassword(kdUser, token, txtEmail.Text.Trim(), template);

                pnlSearchEmail.Visible = false;
                pnlSendInstruction.Visible = false;
                pnlSuccess.Visible = true;
            }
            else 
            {
                lblError.Text = "Invalid code";
            }
        }
    }
}
