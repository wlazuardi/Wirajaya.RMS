﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using WirajayaRMS.CrossCutting.OptManagement;
using System.Data.SqlClient;
using System.Data;
using WirajayaRMS.Business.Entities;

namespace WirajayaRMS.DataAccess.Components
{
    public class UserDB
    {
        public UserData CheckLogin(string username)
        {
            string _spName = "spr_RMS_CheckLogin";
            
            SqlParameter _sqlParameter = new SqlParameter("@Username", SqlDbType.VarChar, 100);
            _sqlParameter.Direction = ParameterDirection.Input;            
            _sqlParameter.Value = username;

            using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter)) 
            {
                if (_reader.HasRows)
                {
                    _reader.Read();
                    UserData _userData = new UserData();                    
                    _userData.KdUser = Convert.ToInt32(_reader["KdUser"]);
                    _userData.Username = _reader["Username"].ToString();                    
                    _userData.FullName = _reader["FullName"].ToString();                    
                    _userData.Email = _reader["Email"].ToString();
                    _userData.Password = _reader["Password"].ToString();
                    _userData.ShowSalary = Convert.ToInt32(_reader["ShowSalary"]);
                    _userData.IsAdmin = Convert.ToInt32(_reader["IsAdmin"]);
                    _userData.PhotoFile = _reader["PhotoFile"].ToString();
                    return _userData;
                }
            }

            return null;
        }

        public List<UserData> GetAllUserList()
        {
            try
            {
                string _spName = "spr_RMS_GetUserList";

                List<UserData> _listUserData = new List<UserData>();
                UserData _userData = new UserData();
                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _userData = new UserData();
                            _userData.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _userData.Username = _reader["Username"].ToString();
                            _userData.FullName = _reader["FullName"].ToString();
                            _userData.Email = _reader["Email"].ToString();
                            _userData.Password = _reader["Password"].ToString();
                            _userData.ShowSalary = Convert.ToInt32(_reader["ShowSalary"]);
                            _userData.IsAdmin = Convert.ToInt32(_reader["IsAdmin"]);
                            _userData.PhotoFile = _reader["PhotoFile"].ToString();        
                            _listUserData.Add(_userData);
                        }
                    }
                }
                return _listUserData;
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
                string _spName = "spr_RMS_InsertUpdateUser";

                SqlParameter[] _sqlParameter = new SqlParameter[6];
                _sqlParameter[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = userData.Username;

                _sqlParameter[1] = new SqlParameter("@FullName", SqlDbType.VarChar, 100);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = userData.FullName;

                _sqlParameter[2] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = userData.Password;

                _sqlParameter[3] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = userData.Email;

                _sqlParameter[4] = new SqlParameter("@ShowSalary", SqlDbType.Int);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = userData.ShowSalary;

                _sqlParameter[5] = new SqlParameter("@IsAdmin", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = userData.IsAdmin;

                return Convert.ToInt32(SqlHelper.ExecuteScalar(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter));
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
                string _spName = "spr_RMS_GetUserList";
                
                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                UserData _userData = new UserData();
                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _userData = new UserData();
                            _userData.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _userData.Username = _reader["Username"].ToString();
                            _userData.FullName = _reader["FullName"].ToString();
                            _userData.Email = _reader["Email"].ToString();
                            _userData.Password = _reader["Password"].ToString();
                            _userData.ShowSalary = Convert.ToInt32(_reader["ShowSalary"]);
                            _userData.IsAdmin = Convert.ToInt32(_reader["IsAdmin"]);
                            _userData.PhotoFile = _reader["PhotoFile"].ToString();        
                        }
                    }
                }

                return _userData;
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
                string _spName = "spr_RMS_InsertUpdateUser";

                SqlParameter[] _sqlParameter = new SqlParameter[7];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = userData.KdUser;

                _sqlParameter[1] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = userData.Username;

                _sqlParameter[2] = new SqlParameter("@FullName", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = userData.FullName;

                _sqlParameter[3] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = DBNull.Value;

                _sqlParameter[4] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = userData.Email;

                _sqlParameter[5] = new SqlParameter("@ShowSalary", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = userData.ShowSalary;

                _sqlParameter[6] = new SqlParameter("@IsAdmin", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = userData.IsAdmin;

                return Convert.ToInt32(SqlHelper.ExecuteScalar(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter));
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
                string _spName = "spr_RMS_ChangeUserPassword";

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdUser;

                _sqlParameter[1] = new SqlParameter("@NewPassword", SqlDbType.VarChar, 50);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = newPassword;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
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
                string _spName = "spr_RMS_DeleteUser";

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

        public List<UserData> SearchUser(string username, string fullName, string email)
        {
            try
            {
                string _spName = "spr_RMS_SearchUser";

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = username;

                _sqlParameter[1] = new SqlParameter("@FullName", SqlDbType.VarChar, 100);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = fullName;

                _sqlParameter[2] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = email;

                List<UserData> _listUserData = new List<UserData>();
                UserData _userData = new UserData();
                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _userData = new UserData();
                            _userData.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _userData.Username = _reader["Username"].ToString();
                            _userData.FullName = _reader["FullName"].ToString();
                            _userData.Email = _reader["Email"].ToString();
                            _userData.Password = _reader["Password"].ToString();
                            _userData.ShowSalary = Convert.ToInt32(_reader["ShowSalary"]);
                            _userData.IsAdmin = Convert.ToInt32(_reader["IsAdmin"]);
                            _listUserData.Add(_userData);
                        }
                    }
                }
                return _listUserData;
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
                string _spName = "spr_RMS_GetUserLevelApprovalList";

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                List<UserAccessData> _listUserApprovalAccess = new List<UserAccessData>();
                UserAccessData _userApprovalAccess = new UserAccessData();
                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _userApprovalAccess = new UserAccessData();
                            _userApprovalAccess.LevelApproval.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _userApprovalAccess.Divisi.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _listUserApprovalAccess.Add(_userApprovalAccess);
                        }
                    }
                }
                return _listUserApprovalAccess;
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
                string _spName = "spr_RMS_UpdateUserProfile";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = userData.KdUser;

                _sqlParameter[1] = new SqlParameter("@FullName", SqlDbType.VarChar, 100);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = userData.FullName;

                _sqlParameter[2] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = userData.Email;

                _sqlParameter[3] = new SqlParameter("@PhotoFile", SqlDbType.VarChar, 100);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                if (userData.PhotoFile == "" || userData.PhotoFile == String.Empty)
                    _sqlParameter[3].Value = DBNull.Value;
                else
                    _sqlParameter[3].Value = userData.PhotoFile;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
