using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class UserAccessSystem
    {
        public List<UserAccessData> GetUserAccessList(int kdDivisi, string kdSO, int kdLevelApproval)
        {
            try
            {
                return new UserAccessDB().GetUserAccessList(0, kdDivisi, kdSO, kdLevelApproval);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int AddEditUserAccess(UserAccessData userAccessData)
        {
            try
            {
                UserData _userData = new UserDB().CheckLogin(userAccessData.User.Username);

                userAccessData.User.KdUser = _userData.KdUser;

                return new UserAccessDB().AddEditUserAccess(userAccessData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserAccessData GetUserAccessData(int kdUser, int kdDivisi, string kdSO, int kdLevelApproval)
        {
            try
            {
                return new UserAccessDB().GetUserAccessList(kdUser, kdDivisi, kdSO, kdLevelApproval)[0];
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteUserAccess(int kdUser, int kdDivisi, string kdSO, int kdLevelApproval)
        {
            try
            {
                return new UserAccessDB().DeleteUserAccess(kdUser, kdDivisi, kdSO, kdLevelApproval);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<UserAccessData> GetUserAccessListByKdUser(int kdUser)
        {
            try
            {
                return new UserAccessDB().GetUserAccessListByKdUser(kdUser);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
