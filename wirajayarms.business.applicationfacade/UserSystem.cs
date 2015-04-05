using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.DataAccess.Components;
using WirajayaRMS.Business.Entities;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class UserSystem
    {
        public UserData CheckLogin(string username)
        {
            try
            {
                return new UserDB().CheckLogin(username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserData> GetAllUserList()
        {
            try
            {
                return new UserDB().GetAllUserList();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int AddUser(UserData userData)
        {
            try
            {
                return new UserDB().AddUser(userData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public UserData GetUserData(int kdUser)
        {
            try
            {
                return new UserDB().GetUserData(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int EditUser(UserData userData)
        {
            try
            {
                return new UserDB().EditUser(userData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int ChangePassword(int kdUser, string newPassword)
        {
            try
            {
                return new UserDB().ChangePassword(kdUser, newPassword);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteUser(int kdUser)
        {
            try
            {
                return new UserDB().DeleteUser(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<UserData> SearchUser(string username, string fullName, string email)
        {
            try
            {
                return new UserDB().SearchUser(username, fullName, email);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<UserAccessData> GetUserLevelApprovalList(int kdUser) 
        {
            try
            {
                return new UserDB().GetUserLevelApprovalList(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int EditUserProfile(UserData userData)
        {
            try
            {
                return new UserDB().EditUserProfile(userData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
