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
    public class UserAccessDB
    {
        public List<UserAccessData> GetUserAccessList(int kdUser, int kdDivisi, string kdSO, int kdLevelApproval)
        {
            try
            {
                string _spName = "spr_RMS_GetUserAccessList";
                List<UserAccessData> _listUserAccess = new List<UserAccessData>();
                UserAccessData _itemUserAccess = new UserAccessData();

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                if (kdUser == 0)
                    _sqlParameter[0].Value = DBNull.Value;
                else
                    _sqlParameter[0].Value = kdUser;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                if (kdDivisi == 0)
                    _sqlParameter[1].Value = DBNull.Value;
                else
                    _sqlParameter[1].Value = kdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdLevelApproval", SqlDbType.Int);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                if (kdLevelApproval == 0)
                    _sqlParameter[2].Value = DBNull.Value;
                else
                    _sqlParameter[2].Value = kdLevelApproval;

                _sqlParameter[3] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                if (kdSO == null)
                    _sqlParameter[3].Value = DBNull.Value;
                else
                    _sqlParameter[3].Value = kdSO;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemUserAccess = new UserAccessData();
                            _itemUserAccess.Divisi = new DivisiData();
                            _itemUserAccess.Divisi.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _itemUserAccess.Divisi.NmDivisi = _reader["NmDivisi"].ToString();
                            _itemUserAccess.User = new UserData();
                            _itemUserAccess.User.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _itemUserAccess.User.Username = _reader["Username"].ToString();
                            _itemUserAccess.LevelApproval = new LevelApprovalData();
                            _itemUserAccess.LevelApproval.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _itemUserAccess.LevelApproval.NmLevelApproval = _reader["NmLevelApproval"].ToString();
                            _itemUserAccess.StrukturOrganisasi = new StrukturOrganisasiData();
                            _itemUserAccess.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                            _itemUserAccess.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _listUserAccess.Add(_itemUserAccess);
                        }
                    }
                }

                return _listUserAccess;
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
                string _spName = "spr_RMS_InsertUpdateUserAccess";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = userAccessData.Divisi.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = userAccessData.User.KdUser;

                _sqlParameter[2] = new SqlParameter("@KdLevelApproval", SqlDbType.Int);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = userAccessData.LevelApproval.KdLevelApproval;

                _sqlParameter[3] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = userAccessData.StrukturOrganisasi.KdSO;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
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
                string _spName = "spr_RMS_DeleteUserAccess";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdUser;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdLevelApproval", SqlDbType.Int);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = kdLevelApproval;

                _sqlParameter[3] = new SqlParameter("@KdSO", SqlDbType.Int);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = kdSO;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
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
                string _spName = "spr_RMS_GetUserAccessByUserId";
                List<UserAccessData> _listUserAccess = new List<UserAccessData>();
                UserAccessData _itemUserAccess = new UserAccessData();

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemUserAccess = new UserAccessData();
                            _itemUserAccess.Divisi = new DivisiData();
                            _itemUserAccess.Divisi.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _itemUserAccess.Divisi.NmDivisi = _reader["NmDivisi"].ToString();
                            _itemUserAccess.LevelApproval = new LevelApprovalData();
                            _itemUserAccess.LevelApproval.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _itemUserAccess.LevelApproval.NmLevelApproval = _reader["NmLevelApproval"].ToString();
                            _itemUserAccess.StrukturOrganisasi = new StrukturOrganisasiData();
                            _itemUserAccess.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                            _itemUserAccess.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _listUserAccess.Add(_itemUserAccess);
                        }
                    }
                }

                return _listUserAccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
