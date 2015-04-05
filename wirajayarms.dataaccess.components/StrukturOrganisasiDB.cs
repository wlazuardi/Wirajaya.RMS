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
    public class StrukturOrganisasiDB
    {
        public List<StrukturOrganisasiData> GetListStrukturOrganisasi(int kdDivisi, string parentKdSO)
        {
            try
            {
                string _spName = "spr_RMS_GetStrukturOrganisasiList";
                List<StrukturOrganisasiData> _listSO = new List<StrukturOrganisasiData>();
                StrukturOrganisasiData _itemSO = new StrukturOrganisasiData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@ParentKdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                if (parentKdSO == null)
                    _sqlParameter[1].Value = DBNull.Value;
                else
                    _sqlParameter[1].Value = parentKdSO;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemSO = new StrukturOrganisasiData();
                            _itemSO.KdSO = _reader["KdSO"].ToString();
                            _itemSO.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _itemSO.ParentKdSO = _reader["ParentKdSO"].ToString();
                            _itemSO.IsActive = Convert.ToInt32(_reader["IsActive"]);
                            _listSO.Add(_itemSO);
                        }
                    }
                }

                return _listSO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<StrukturOrganisasiData> GetListStrukturOrganisasi(int kdDivisi, int kdUser, string parentKdSO)
        {
            try
            {
                string _spName = "spr_RMS_GetStrukturOrganisasiListByKdUser";
                List<StrukturOrganisasiData> _listSO = new List<StrukturOrganisasiData>();
                StrukturOrganisasiData _itemSO = new StrukturOrganisasiData();

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdUser;

                _sqlParameter[2] = new SqlParameter("@ParentKdSO", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                if (parentKdSO == null)
                    _sqlParameter[2].Value = DBNull.Value;
                else
                    _sqlParameter[2].Value = parentKdSO;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemSO = new StrukturOrganisasiData();
                            _itemSO.KdSO = _reader["KdSO"].ToString();
                            _itemSO.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _itemSO.ParentKdSO = _reader["ParentKdSO"].ToString();
                            _itemSO.IsActive = Convert.ToInt32(_reader["IsActive"]);
                            _listSO.Add(_itemSO);
                        }
                    }
                }

                return _listSO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertStrukturOrganisasi(StrukturOrganisasiData soData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateStrukturOrganisasi";

                SqlParameter[] _sqlParameter = new SqlParameter[8];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = soData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = soData.KdSO;

                _sqlParameter[2] = new SqlParameter("@KdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = soData.KdUnit;

                _sqlParameter[3] = new SqlParameter("@NamaSO", SqlDbType.VarChar, 100);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = soData.NmStrukturOrganisasi;

                _sqlParameter[4] = new SqlParameter("@ParentKdSO", SqlDbType.VarChar, 10);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = soData.ParentKdSO;

                _sqlParameter[5] = new SqlParameter("@JmlKaryawan", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = soData.JmlKaryawan;

                _sqlParameter[6] = new SqlParameter("@MaxJmlKaryawan", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = soData.MaxJmlKaryawan;

                _sqlParameter[7] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = soData.IsActive;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public StrukturOrganisasiData GetStrukturOrganisasiData(int kdDivisi, string kdSO)
        {
            try
            {
                string _spName = "spr_RMS_GetStrukturOrganisasiData";
                StrukturOrganisasiData _data = new StrukturOrganisasiData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdSO;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _data.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _data.NmDivisi = _reader["NmDivisi"].ToString();
                            _data.KdUnit = _reader["KdUnit"].ToString();
                            _data.KdSO = _reader["KdSO"].ToString();
                            _data.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _data.JmlKaryawan = Convert.ToInt32(_reader["JmlKaryawan"]);
                            _data.MaxJmlKaryawan = Convert.ToInt32(_reader["MaxJmlKaryawan"]);
                            _data.ParentKdSO = _reader["ParentKdSO"].ToString();
                            _data.ParentNmStrukturOrganisasi = _reader["ParentNmSO"].ToString();
                            _data.IsActive = Convert.ToInt32(_reader["IsActive"]);
                        }
                    }
                }

                return _data;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int UpdateStrukturOrganisasi(StrukturOrganisasiData soData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateStrukturOrganisasi";

                SqlParameter[] _sqlParameter = new SqlParameter[8];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = soData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = soData.KdSO;

                _sqlParameter[2] = new SqlParameter("@KdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = soData.KdUnit;

                _sqlParameter[3] = new SqlParameter("@NamaSO", SqlDbType.VarChar, 100);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = soData.NmStrukturOrganisasi;

                _sqlParameter[4] = new SqlParameter("@ParentKdSO", SqlDbType.VarChar, 10);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = soData.ParentKdSO;

                _sqlParameter[5] = new SqlParameter("@JmlKaryawan", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = soData.JmlKaryawan;

                _sqlParameter[6] = new SqlParameter("@MaxJmlKaryawan", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = soData.MaxJmlKaryawan;

                _sqlParameter[7] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = soData.IsActive;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteStrukturOrganisasi(int kdDivisi, string kdSO)
        {
            try
            {
                string _spName = "spr_RMS_DeleteStrukturOrganisasi";                

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdSO;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
