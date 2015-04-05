using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.DataAccess.Components;
using WirajayaRMS.Business.Entities;
using System.Net.Mail;
using WirajayaRMS.CrossCutting.OptManagement;
using System.Net;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class NotificationSystem
    {
        public int AddNotification(NotificationData notificationData)
        {
            try
            {
                return new NotificationDB().AddUpdateNotification(notificationData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<NotificationData> GetNotificationList(int kdUser)
        {
            try
            {
                return new NotificationDB().GetNotificationList(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int NotifyNextPIC(RecruitmentData recruitmentData, string template, int kdUserCreator)
        {
            try
            {
                List<UserData> _listPIC = new RecruitmentDB().GetNextPICList(recruitmentData.KdDivisi, recruitmentData.StrukturOrganisasi.KdSO, recruitmentData.CurrLevelApproval);

                DivisiData _divisiData = new DivisiDB().GetDivisiData(recruitmentData.KdDivisi);
                StrukturOrganisasiData _soData = new StrukturOrganisasiDB().GetStrukturOrganisasiData(recruitmentData.KdDivisi, recruitmentData.StrukturOrganisasi.KdSO);
                JabatanData _jabatanData = new JabatanDB().GetJabatanData(recruitmentData.KdDivisi, recruitmentData.Jabatan.KdJabatan);
                LevelApprovalData _lvApprovalData = new LevelApprovalSystem().GetLevelApprovalData(recruitmentData.KdDivisi, recruitmentData.CurrLevelApproval);
                UserData _creatorData = new UserSystem().GetUserData(recruitmentData.Creator.KdUser);

                template = template.Replace("#NoRequest#", recruitmentData.NoRequest)
                    .Replace("#Divisi#", _divisiData.NmDivisi)
                    .Replace("#SO#", _soData.ParentNmStrukturOrganisasi + " - " + _soData.NmStrukturOrganisasi)
                    .Replace("#Jabatan#", _jabatanData.NmJabatan)
                    .Replace("#JumlahKaryawan#", recruitmentData.JmlOrang.ToString())
                    .Replace("#TglDiperlukan#", recruitmentData.TglButuh.ToString("dd MMM yyyy"))
                    .Replace("#Alasan#", recruitmentData.KdAlasan == 1 ? "New Employee Allocation" : "Employee Replacement")
                    .Replace("#Status#", _lvApprovalData.StatusDokumen)
                    .Replace("#Creator#", _creatorData.FullName)
                    .Replace("#FollowUpURL#", SystemConfiguration.WebDomainName);

                // Send email & notification to each PIC
                foreach (UserData _picData in _listPIC)
                {
                    string body = template;
                    body = body.Replace("#NamaPIC#", _picData.FullName);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(SystemConfiguration.NotificationEmailUsername, "Wirajaya Recruitment Management System");
                    mail.To.Add(new MailAddress(_picData.Email));
                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(SystemConfiguration.NotificationEmailUsername, SystemConfiguration.NotificationEmailPassword);
                    client.Host = SystemConfiguration.SMTPHost;
                    mail.Subject = "[No-Reply] Recruitment Request Notification";
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.Body = body;
                    client.Send(mail);

                    NotificationData _notificationData = new NotificationData();
                    _notificationData.KdUser = _picData.KdUser;
                    _notificationData.Message = "<b>New Request</b><br />" + _jabatanData.NmJabatan + " - " + _soData.NmStrukturOrganisasi + " (" + _divisiData.NmDivisi + ")";
                    _notificationData.Argument = recruitmentData.NoRequest;
                    _notificationData.IsRead = false;
                    _notificationData.KdTipe = 1;
                    _notificationData.Creator.KdUser = kdUserCreator;

                    int result = new NotificationSystem().AddNotification(_notificationData);
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int NotifyStatusChanged(RecruitmentData recruitmentData, string template, int kdUserCreator)
        {
            try
            {
                List<RecruitmentApprovalData> _recAppHistoryList = new RecruitmentSystem().GetRequestApprovalHistory(recruitmentData.NoRequest);                

                DivisiData _divisiData = new DivisiDB().GetDivisiData(recruitmentData.KdDivisi);
                StrukturOrganisasiData _soData = new StrukturOrganisasiDB().GetStrukturOrganisasiData(recruitmentData.KdDivisi, recruitmentData.StrukturOrganisasi.KdSO);
                JabatanData _jabatanData = new JabatanDB().GetJabatanData(recruitmentData.KdDivisi, recruitmentData.Jabatan.KdJabatan);
                LevelApprovalData _lvApprovalData = new LevelApprovalSystem().GetLevelApprovalData(recruitmentData.KdDivisi, recruitmentData.CurrLevelApproval);
                UserData _creatorData = new UserSystem().GetUserData(recruitmentData.Creator.KdUser);

                template = template.Replace("#NoRequest#", recruitmentData.NoRequest)
                    .Replace("#Divisi#", _divisiData.NmDivisi)
                    .Replace("#SO#", _soData.ParentNmStrukturOrganisasi + " - " + _soData.NmStrukturOrganisasi)
                    .Replace("#Jabatan#", _jabatanData.NmJabatan)
                    .Replace("#JumlahKaryawan#", recruitmentData.JmlOrang.ToString())
                    .Replace("#TglDiperlukan#", recruitmentData.TglButuh.ToString("dd MMM yyyy"))
                    .Replace("#Alasan#", recruitmentData.KdAlasan == 1 ? "New Employee Allocation" : "Employee Replacement")
                    .Replace("#Status#", _lvApprovalData.StatusDokumen)
                    .Replace("#Creator#", _creatorData.FullName)
                    .Replace("#FollowUpURL#", SystemConfiguration.WebDomainName);

                // Send email & notification to each PIC
                string body = template;
                body = body.Replace("#NamaPIC#", _creatorData.FullName);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SystemConfiguration.NotificationEmailUsername, "Wirajaya Recruitment Management System");
                mail.To.Add(new MailAddress(_creatorData.Email));
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(SystemConfiguration.NotificationEmailUsername, SystemConfiguration.NotificationEmailPassword);
                client.Host = SystemConfiguration.SMTPHost;
                mail.Subject = "[No-Reply] Request Progress Notification";
                mail.SubjectEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Body = body;
                client.Send(mail);

                NotificationData _notificationData = new NotificationData();
                _notificationData.KdUser = _creatorData.KdUser;
                _notificationData.Message = "<b>Status: " + _lvApprovalData.StatusDokumen + "</b><br />" + _jabatanData.NmJabatan + " - " + _soData.NmStrukturOrganisasi + " (" + _divisiData.NmDivisi + ")";
                _notificationData.Argument = recruitmentData.NoRequest;
                _notificationData.IsRead = false;
                _notificationData.KdTipe = 4;
                _notificationData.Creator.KdUser = kdUserCreator;

                int result = new NotificationSystem().AddNotification(_notificationData);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int NotifyRequestClosed(RecruitmentData recruitmentData, string template, List<KandidatData> listKandidatTerpilih, int kdUserCreator)
        {
            try
            {
                List<UserData> _listPIC = new RecruitmentDB().GetAllPICList(recruitmentData.NoRequest);

                //Looping kandidat terpilih untuk ditampung ke string
                string _listKandidatString = "";
                foreach(KandidatData kandidatData in listKandidatTerpilih)
                {
                    _listKandidatString += "<li>";
                    _listKandidatString += kandidatData.NmKandidat;
                    _listKandidatString += "</li>";
                }

                DivisiData _divisiData = new DivisiDB().GetDivisiData(recruitmentData.KdDivisi);
                StrukturOrganisasiData _soData = new StrukturOrganisasiDB().GetStrukturOrganisasiData(recruitmentData.KdDivisi, recruitmentData.StrukturOrganisasi.KdSO);
                JabatanData _jabatanData = new JabatanDB().GetJabatanData(recruitmentData.KdDivisi, recruitmentData.Jabatan.KdJabatan);
                UserData _creatorData = new UserSystem().GetUserData(recruitmentData.Creator.KdUser);

                template = template.Replace("#NoRequest#", recruitmentData.NoRequest)
                    .Replace("#Divisi#", _divisiData.NmDivisi)
                    .Replace("#SO#", _soData.ParentNmStrukturOrganisasi + " - " + _soData.NmStrukturOrganisasi)
                    .Replace("#Jabatan#", _jabatanData.NmJabatan)
                    .Replace("#Alasan#", recruitmentData.KdAlasan == 1 ? "New Employee Allocation" : "Employee Replacement")
                    .Replace("#Creator#", _creatorData.FullName)
                    .Replace("#FollowUpURL#", SystemConfiguration.WebDomainName)
                    .Replace("#DaftarKandidat#", _listKandidatString);

                // Send email & notification to each PIC
                foreach (UserData _picData in _listPIC)
                {
                    string body = template;
                    body = body.Replace("#NamaPIC#", _picData.FullName);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(SystemConfiguration.NotificationEmailUsername, "Wirajaya Recruitment Management System");
                    mail.To.Add(new MailAddress(_picData.Email));                    
                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(SystemConfiguration.NotificationEmailUsername, SystemConfiguration.NotificationEmailPassword);
                    client.Host = SystemConfiguration.SMTPHost;
                    mail.Subject = "[No-Reply] Closed Request Notification";
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.Body = body;
                    client.Send(mail);

                    NotificationData _notificationData = new NotificationData();
                    _notificationData.KdUser = _picData.KdUser;
                    _notificationData.Message = "<b>Request Closed</b><br />" + _jabatanData.NmJabatan + " - " + _soData.NmStrukturOrganisasi + " (" + _divisiData.NmDivisi + ")";
                    _notificationData.Argument = recruitmentData.NoRequest;
                    _notificationData.IsRead = false;
                    _notificationData.KdTipe = 2;
                    _notificationData.Creator.KdUser = kdUserCreator;

                    int result = new NotificationSystem().AddNotification(_notificationData);
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int NotifyRequestDeclined(RecruitmentData recruitmentData, RecruitmentApprovalData recAppData, string template, int kdUserCreator)
        {
            try
            {
                List<UserData> _listPIC = new RecruitmentDB().GetAllPICList(recruitmentData.NoRequest);

                DivisiData _divisiData = new DivisiDB().GetDivisiData(recruitmentData.KdDivisi);
                StrukturOrganisasiData _soData = new StrukturOrganisasiDB().GetStrukturOrganisasiData(recruitmentData.KdDivisi, recruitmentData.StrukturOrganisasi.KdSO);
                JabatanData _jabatanData = new JabatanDB().GetJabatanData(recruitmentData.KdDivisi, recruitmentData.Jabatan.KdJabatan);
                UserData _creatorData = new UserSystem().GetUserData(recruitmentData.Creator.KdUser);
                UserData _declinerData = new UserSystem().GetUserData(recAppData.User.KdUser);
                LevelApprovalData _lvApprovalData = new LevelApprovalSystem().GetLevelApprovalData(recruitmentData.KdDivisi, recAppData.LvApproval.KdLevelApproval);


                template = template.Replace("#NoRequest#", recruitmentData.NoRequest)
                    .Replace("#Divisi#", _divisiData.NmDivisi)
                    .Replace("#SO#", _soData.ParentNmStrukturOrganisasi + " - " + _soData.NmStrukturOrganisasi)
                    .Replace("#Jabatan#", _jabatanData.NmJabatan)
                    .Replace("#JumlahKaryawan#", recruitmentData.JmlOrang.ToString())
                    .Replace("#TglDiperlukan#", recruitmentData.TglButuh.ToString("dd MMM yyyy"))
                    .Replace("#Alasan#", recruitmentData.KdAlasan == 1 ? "New Employee Allocation" : "Employee Replacement")
                    .Replace("#Status#", _lvApprovalData.StatusDokumen)
                    .Replace("#Creator#", _creatorData.FullName)
                    .Replace("#NamaPenolak#", _declinerData.FullName + " (" + _lvApprovalData.NmLevelApproval + ")")
                    .Replace("#Komentar#", recAppData.Komentar)
                    .Replace("#FollowUpURL#", SystemConfiguration.WebDomainName);

                // Send email & notification to each PIC
                foreach (UserData _picData in _listPIC)
                {
                    if (_picData.KdUser != recAppData.User.KdUser)
                    {
                        string body = template;
                        body = body.Replace("#NamaPIC#", _picData.FullName);

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(SystemConfiguration.NotificationEmailUsername, "Wirajaya Recruitment Management System");
                        mail.To.Add(new MailAddress(_picData.Email));
                        SmtpClient client = new SmtpClient();
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(SystemConfiguration.NotificationEmailUsername, SystemConfiguration.NotificationEmailPassword);
                        client.Host = SystemConfiguration.SMTPHost;
                        mail.Subject = "[No-Reply] Declined Request Notification";
                        mail.SubjectEncoding = Encoding.UTF8;
                        mail.IsBodyHtml = true;
                        mail.BodyEncoding = Encoding.UTF8;
                        mail.Body = body;
                        client.Send(mail);

                        NotificationData _notificationData = new NotificationData();
                        _notificationData.KdUser = _picData.KdUser;
                        _notificationData.Message = "<b>Request Declined</b><br />" + _jabatanData.NmJabatan + " - " + _soData.NmStrukturOrganisasi + " (" + _divisiData.NmDivisi + ")";
                        _notificationData.Argument = recruitmentData.NoRequest;
                        _notificationData.IsRead = false;
                        _notificationData.KdTipe = 3;
                        _notificationData.Creator.KdUser = kdUserCreator;

                        int result = new NotificationSystem().AddNotification(_notificationData);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int MarkNotifRead(int kdNotification)
        {
            try
            {
                return new NotificationDB().MarkNotifRead(kdNotification);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int NotifyResetPassword(int kdUser, string token, string email, string template)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(SystemConfiguration.NotificationEmailUsername, "Wirajaya Recruitment Management System");
                mail.To.Add(new MailAddress(email));
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(SystemConfiguration.NotificationEmailUsername, SystemConfiguration.NotificationEmailPassword);
                client.Host = SystemConfiguration.SMTPHost;
                mail.Subject = "[No-Reply] Reset Password Request";
                mail.SubjectEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Body = template;
                client.Send(mail);

                return new NotificationDB().AddResetPasswordRequest(kdUser, token);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public string CheckResetPasswordToken(int kdUser)
        {
            try
            {
                return new NotificationDB().CheckResetPasswordToken(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteChangePasswordToken(int kdUser)
        {
            try
            {
                return new NotificationDB().DeleteChangePasswordToken(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<NotificationData> GetAllNotificationList(int kdUser)
        {
            try
            {
                return new NotificationDB().GetAllNotificationList(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int GetUnreadNotificationCount(int kdUser)
        {
            try
            {
                return new NotificationDB().GetUnreadNotificationCount(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
