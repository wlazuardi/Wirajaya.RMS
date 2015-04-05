using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class UserData
    {
        private int _kdUser;
        public int KdUser
        {
            get { return _kdUser; }
            set { _kdUser = value; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private int _showSalary;
        public int ShowSalary
        {
            get { return _showSalary; }
            set { _showSalary = value; }
        }

        private int _isAdmin;
        public int IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        private string _photoFile;
        public string PhotoFile
        {
            get { return _photoFile; }
            set { _photoFile = value; }
        }
    }
}
