using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using WirajayaRMS.CrossCutting.OptManagement;

namespace WirajayaRMS.DataAccess.Components
{
    public class NotificationDB
    {
        public int AddUpdateNotification(NotificationData notificationData) 
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateNotification";

                SqlParameter[] _sqlParameter = new SqlParameter[7];
                _sqlParameter[0] = new SqlParameter("@KdNotification", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                if (notificationData.KdNotification == 0)
                {
                    _sqlParameter[0].Value = DBNull.Value;
                }
                else
                {
                    _sqlParameter[0].Value = notificationData.KdNotification;
                }

                _sqlParameter[1] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = notificationData.KdUser;

                _sqlParameter[2] = new SqlParameter("@Message", SqlDbType.VarChar, 300);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = notificationData.Message;

                _sqlParameter[3] = new SqlParameter("@Argument", SqlDbType.VarChar, 50);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = notificationData.Argument;

                _sqlParameter[4] = new SqlParameter("@IsRead", SqlDbType.Bit);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = notificationData.IsRead;

                _sqlParameter[5] = new SqlParameter("@KdTipe", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = notificationData.KdTipe;

                _sqlParameter[6] = new SqlParameter("@Creator", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = notificationData.Creator.KdUser;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
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
                string _spName = "spr_RMS_GetNotificationList";

                List<NotificationData> _listNotif = new List<NotificationData>();
                NotificationData _notifData = new NotificationData();

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = DBNull.Value;
                _sqlParameter.Value = kdUser;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _notifData = new NotificationData();
                            _notifData.KdNotification = Convert.ToInt32(_reader["KdNotification"]);
                            _notifData.Message = _reader["Message"].ToString();
                            _notifData.Argument = _reader["Argument"].ToString();
                            _notifData.IsRead = Convert.ToBoolean(_reader["IsRead"]);
                            _notifData.KdTipe = Convert.ToInt32(_reader["KdTipe"]);
                            _notifData.Creator.KdUser = Convert.ToInt32(_reader["Creator"]);
                            _notifData.CreatedDate = Convert.ToDateTime(_reader["CreateDate"]);
                            _listNotif.Add(_notifData);
                        }
                    }
                }

                return _listNotif;
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
                string _spName = "spr_RMS_UpdateNotificationRead";

                List<NotificationData> _listNotif = new List<NotificationData>();
                NotificationData _notifData = new NotificationData();

                SqlParameter _sqlParameter = new SqlParameter("@KdNotification", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdNotification;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int AddResetPasswordRequest(int kdUser, string token)
        {
            try
            {
                string _spName = "spr_RMS_InsertResetPasswordRequest";

                List<NotificationData> _listNotif = new List<NotificationData>();
                NotificationData _notifData = new NotificationData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdUser;

                _sqlParameter[1] = new SqlParameter("@Token", SqlDbType.VarChar, 100);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = token;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
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
                string _spName = "spr_RMS_CheckResetPasswordToken";

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                using (SqlDataReader _sqlDataReader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter)) 
                {
                    if (_sqlDataReader.HasRows) 
                    {
                        if (_sqlDataReader.Read()) 
                        {
                            return _sqlDataReader["Token"].ToString();
                        }
                    }
                }

                return "";
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
                string _spName = "spr_RMS_DeleteResetPasswordToken";

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);                
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
                string _spName = "spr_RMS_GetAllNotificationList";

                List<NotificationData> _listNotif = new List<NotificationData>();
                NotificationData _notifData = new NotificationData();

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = DBNull.Value;
                _sqlParameter.Value = kdUser;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _notifData = new NotificationData();
                            _notifData.KdNotification = Convert.ToInt32(_reader["KdNotification"]);
                            _notifData.Message = _reader["Message"].ToString();
                            _notifData.Argument = _reader["Argument"].ToString();
                            _notifData.IsRead = Convert.ToBoolean(_reader["IsRead"]);
                            _notifData.KdTipe = Convert.ToInt32(_reader["KdTipe"]);
                            _notifData.Creator.KdUser = Convert.ToInt32(_reader["Creator"]);
                            _notifData.CreatedDate = Convert.ToDateTime(_reader["CreateDate"]);
                            _listNotif.Add(_notifData);
                        }
                    }
                }

                return _listNotif;
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
                string _spName = "spr_RMS_GetUnreadNotificationCount";

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                return Convert.ToInt32(SqlHelper.ExecuteScalar(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
