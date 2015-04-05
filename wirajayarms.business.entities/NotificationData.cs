using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    public class NotificationData
    {
        public NotificationData() 
        {
            _creator = new UserData();
        }

        private int _kdNotification;
        public int KdNotification
        {
            get { return _kdNotification; }
            set { _kdNotification = value; }
        }

        private int _kdUser;
        public int KdUser
        {
            get { return _kdUser; }
            set { _kdUser = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _argument;
        public string Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }

        private bool _isRead;
        public bool IsRead
        {
            get { return _isRead; }
            set { _isRead = value; }
        }

        private int _kdTipe;
        public int KdTipe
        {
            get { return _kdTipe; }
            set { _kdTipe = value; }
        }

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        private UserData _creator;
        public UserData Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

    }
}
