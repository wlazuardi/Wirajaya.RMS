using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WirajayaRMS.CrossCutting.OptManagement
{
    public static class SystemConfiguration
    {
        private static string _RMSConnectionString = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        public static string RMSConnectionString
        {
            get { return SystemConfiguration._RMSConnectionString; }
            set { SystemConfiguration._RMSConnectionString = value; }
        }

        private static string _encryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
        public static string EncryptionKey
        {
            get { return SystemConfiguration._encryptionKey; }
            set { SystemConfiguration._encryptionKey = value; }
        }

        private static string _uploadDirectory = ConfigurationManager.AppSettings["UploadDirectory"];
        public static string UploadDirectory
        {
            get { return SystemConfiguration._uploadDirectory; }
            set { SystemConfiguration._uploadDirectory = value; }
        }

        private static string _notificationEmailUsername = ConfigurationManager.AppSettings["NotificationEmailUsername"];
        public static string NotificationEmailUsername
        {
            get { return SystemConfiguration._notificationEmailUsername; }
            set { SystemConfiguration._notificationEmailUsername = value; }
        }

        private static string _notificationEmailPassword = ConfigurationManager.AppSettings["NotificationEmailPassword"];
        public static string NotificationEmailPassword
        {
            get { return SystemConfiguration._notificationEmailPassword; }
            set { SystemConfiguration._notificationEmailPassword = value; }
        }

        private static string _SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
        public static string SMTPHost
        {
            get { return SystemConfiguration._SMTPHost; }
            set { SystemConfiguration._SMTPHost = value; }
        }

        private static string _webDomainName = ConfigurationManager.AppSettings["WebDomainName"];
        public static string WebDomainName
        {
            get { return SystemConfiguration._webDomainName; }
            set { SystemConfiguration._webDomainName = value; }
        }
    }
}
