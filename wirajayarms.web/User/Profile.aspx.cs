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
using WirajayaRMS.Web.UserControl;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using WirajayaRMS.CrossCutting.OptManagement;
using System.Collections.Generic;
using WirajayaRMS.CrossCutting.Security;

namespace WirajayaRMS.Web.User
{
    public partial class Profile : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "User profile & change password page";

            if (Request.QueryString["success"] != null && Request.QueryString["success"] == "1") {
                alertNotification.Show("Data saved successfully", AlertType.Success);
            }

            if (!IsPostBack)
            {
                UserData _userData = new UserSystem().GetUserData(UserDataSession.KdUser);
                txtUsername.Text = _userData.Username;
                txtFullName.Text = _userData.FullName;
                txtEmail.Text = _userData.Email;
                if (_userData.PhotoFile == String.Empty)
                {
                    imgPhoto.ImageUrl = "~/img/avatar-default.png";
                    btnRemoveImage.Visible = false;
                }
                else 
                {
                    imgPhoto.ImageUrl = "~/Photo/" + _userData.PhotoFile;
                    btnRemoveImage.Visible = true;
                }

                if (UserDataSession.IsAdmin == 1)
                {
                    litMessage.Text = "Administrator";
                }
                else
                {
                    List<UserAccessData> _userAccessList = new UserAccessSystem().GetUserAccessListByKdUser(UserDataSession.KdUser);
                    rptUserAccess.DataSource = _userAccessList;
                    rptUserAccess.DataBind();
                }
            }
        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            UserData _userData = new UserData();
            _userData.KdUser = UserDataSession.KdUser;
            _userData.FullName = txtFullName.Text;
            _userData.Email = txtEmail.Text;
            _userData.PhotoFile = hidPhoto.Value;

            int result = new UserSystem().EditUserProfile(_userData);

            _userData = new UserSystem().GetUserData(UserDataSession.KdUser);
            Session[SessionNameFactory.UserData] = _userData;

            if (result > 0)
            {
                //alertNotification.Show("Data saved successfully", AlertType.Success);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Refresh",
                //                       "setTimeout(function(){location.reload();}, 2000);",
                //                       true);
                Response.Redirect("~/User/Profile.aspx?success=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
            }
            else
            {
                alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            UserData _userData = new UserSystem().GetUserData(UserDataSession.KdUser);
            string _oldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtOldPassword.Text, "sha1");
            if (_oldPassword.Equals(_userData.Password))
            {
                if (txtNewPassword.Text.Equals(txtConfNewPassword.Text))
                {
                    string _newPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "sha1");
                    int resultChangePass = new UserSystem().ChangePassword(UserDataSession.KdUser, _newPassword);
                    if (resultChangePass > 0)
                    {
                        alertChangePassword.Show("Password changed successfully", AlertType.Success);
                    }
                }
                else
                {
                    alertChangePassword.Show("Confirm password did not match", AlertType.Danger);
                }
            }
            else
            {
                alertChangePassword.Show("Wrong password verification. Please check your old password field", AlertType.Danger);
            }
        }

        protected void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            if (fuPhoto.HasFile)
            {
                try
                {
                    string ext = Path.GetExtension(fuPhoto.FileName);
                    string formatedFileName = Path.GetFileNameWithoutExtension(fuPhoto.FileName).Replace(' ', '_') + DateTime.Now.ToString("ddMMyyyyhhmmss") + ext;
                    string path = Server.MapPath("~/Temp/" + formatedFileName);
                    fuPhoto.SaveAs(path + "_temp" + ext);

                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(path + "_temp" + ext))
                    {
                        System.Drawing.Image scalledImg = ScaleImage(img, 500);
                        scalledImg.Save(path + ext);
                    }

                    File.Delete(path + "_temp" + ext);

                    imgPhotoCrop.ImageUrl = "~/Temp/" + formatedFileName + ext;
                    popUpChangeUserPhoto.Show();

                    // The last param tells .Net to surround your
                    // code with script tags (true) or not (false)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Jcrop",
                                       "$('#" + imgPhotoCrop.ClientID + "').Jcrop({aspectRatio: 1, setSelect: [0,0,100,100], onChange: showCoords, onSelect: showCoords});",
                                       true);
                }
                catch(Exception ex)
                {
                    alertNotification.Show("Failed to upload image. " + ex.Message, AlertType.Danger); 
                }
            }
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth)
        {
            var ratio = (double)maxWidth / image.Width;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        } 

        protected void btnDoneCrop_Click(object sender, EventArgs e)
        {
            try
            {
                int X1 = Convert.ToInt32(hidX1.Value);
                int Y1 = Convert.ToInt32(hidY1.Value);
                int X2 = Convert.ToInt32(hidX2.Value);
                int Y2 = Convert.ToInt32(hidY2.Value);
                int X = System.Math.Min(X1, X2);
                int Y = System.Math.Min(Y1, Y2);
                int w = Convert.ToInt32(hidW.Value);
                int h = Convert.ToInt32(hidH.Value);

                // That can be any image (jpg,jpeg,png,gif) from anywhere in the server
                string fileName = Path.GetFileName(imgPhotoCrop.ImageUrl);
                string originalFile = Server.MapPath("~/Temp/" + fileName);

                using (System.Drawing.Image img = System.Drawing.Image.FromFile(originalFile))
                {
                    using (System.Drawing.Bitmap _bitmap = new System.Drawing.Bitmap(w, h))
                    {
                        _bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                        using (Graphics _graphic = Graphics.FromImage(_bitmap))
                        {
                            _graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            _graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            _graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            _graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            _graphic.DrawImage(img, 0, 0, w, h);
                            _graphic.DrawImage(img, new Rectangle(0, 0, w, h), X, Y, w, h, GraphicsUnit.Pixel);

                            string extension = Path.GetExtension(originalFile);
                            string croppedFileName = Guid.NewGuid().ToString();
                            string path = Server.MapPath("~/Photo/");


                            // If the image is a gif file, change it into png
                            if (extension.EndsWith("gif", StringComparison.OrdinalIgnoreCase))
                            {
                                extension = ".png";
                            }

                            string newFullPathName = string.Concat(path, croppedFileName, extension);

                            using (EncoderParameters encoderParameters = new EncoderParameters(1))
                            {
                                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                                _bitmap.Save(newFullPathName, GetImageCodec(extension), encoderParameters);

                                imgPhoto.ImageUrl = "~/Photo/" + croppedFileName + extension;
                                hidPhoto.Value = croppedFileName + extension;
                                alertNotification.Show("All changes are not saved until you click \"Save Changes\" button", AlertType.Warning); 
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                alertNotification.Show("Failed to process the image. " + ex.Message, AlertType.Danger); 
            }

            popUpChangeUserPhoto.Hide();
        }

        /// <summary>
        /// Find the right codec
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetImageCodec(string extension)
        {
            extension = extension.ToUpperInvariant();
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FilenameExtension.Contains(extension))
                {
                    return codec;
                }
            }
            return codecs[1];
        }

        protected void btnRemoveImage_Click(object sender, EventArgs e) 
        {
            hidPhoto.Value = "";
            imgPhoto.ImageUrl = "~/img/avatar-default.png";
            btnRemoveImage.Visible = false;
        }

        protected void rptUserAccess_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
            { 
                Literal litLvApproval = (Literal)e.Item.FindControl("litLvApproval");
                Literal litSO = (Literal)e.Item.FindControl("litSO");
                Literal litDivisi = (Literal)e.Item.FindControl("litDivisi");
                UserAccessData _userAccess = (UserAccessData)e.Item.DataItem;

                litLvApproval.Text = _userAccess.LevelApproval.NmLevelApproval;
                litSO.Text = _userAccess.StrukturOrganisasi.NmStrukturOrganisasi;
                litDivisi.Text = _userAccess.Divisi.NmDivisi;
            }
        }
    }
}
