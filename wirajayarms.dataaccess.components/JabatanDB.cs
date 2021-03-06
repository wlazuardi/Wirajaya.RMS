﻿using System;
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
    public class JabatanDB
    {
        public List<JabatanData> GetListJabatan(int kdDivisi, string parentKdJabatan)
        {
            try
            {
                string _spName = "spr_RMS_GetJabatanList";
                List<JabatanData> _listJabatan = new List<JabatanData>();
                JabatanData _itemJabatan = new JabatanData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@ParentKdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                if (parentKdJabatan == null)
                    _sqlParameter[1].Value = DBNull.Value;
                else
                    _sqlParameter[1].Value = parentKdJabatan;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemJabatan = new JabatanData();
                            _itemJabatan.KdJabatan = _reader["KdJabatan"].ToString();
                            _itemJabatan.NmJabatan = _reader["NmJabatan"].ToString();
                            _itemJabatan.ParentKdJabatan = _reader["ParentKdJabatan"].ToString();
                            _itemJabatan.IsActive = Convert.ToInt32(_reader["IsActive"]);
                            _listJabatan.Add(_itemJabatan);
                        }
                    }
                }

                return _listJabatan;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertJabatan(JabatanData jabatanData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateJabatan";

                SqlParameter[] _sqlParameter = new SqlParameter[8];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = jabatanData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = jabatanData.KdJabatan;

                _sqlParameter[2] = new SqlParameter("@NamaJabatan", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = jabatanData.NmJabatan;

                _sqlParameter[3] = new SqlParameter("@ParentKdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = jabatanData.ParentKdJabatan;

                _sqlParameter[4] = new SqlParameter("@MinSalary", SqlDbType.Decimal);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = jabatanData.MinSalary;

                _sqlParameter[5] = new SqlParameter("@MaxSalary", SqlDbType.Decimal);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = jabatanData.MaxSalary;

                _sqlParameter[6] = new SqlParameter("@Fasilitas", SqlDbType.VarChar, 200);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = jabatanData.Fasilitas;

                _sqlParameter[7] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = jabatanData.IsActive;


                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JabatanData GetJabatanData(int kdDivisi, string kdJabatan)
        {
            try
            {
                string _spName = "spr_RMS_GetJabatanData";
                JabatanData _data = new JabatanData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdJabatan;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _data.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _data.NmDivisi = _reader["NmDivisi"].ToString();
                            _data.KdJabatan = _reader["KdJabatan"].ToString();
                            _data.NmJabatan = _reader["NmJabatan"].ToString();
                            _data.MinSalary = Convert.ToDouble(_reader["MinSalary"]);
                            _data.MaxSalary = Convert.ToDouble(_reader["MaxSalary"]);
                            _data.ParentKdJabatan = _reader["ParentKdJabatan"].ToString();
                            _data.ParentNmJabatan = _reader["ParentNmJabatan"].ToString();
                            _data.Fasilitas = _reader["Fasilitas"].ToString();
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

        public int UpdateJabatan(JabatanData jabatanData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateJabatan";

                SqlParameter[] _sqlParameter = new SqlParameter[8];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = jabatanData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = jabatanData.KdJabatan;

                _sqlParameter[2] = new SqlParameter("@NamaJabatan", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = jabatanData.NmJabatan;

                _sqlParameter[3] = new SqlParameter("@ParentKdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = jabatanData.ParentKdJabatan;

                _sqlParameter[4] = new SqlParameter("@MinSalary", SqlDbType.Decimal);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = jabatanData.MinSalary;

                _sqlParameter[5] = new SqlParameter("@MaxSalary", SqlDbType.Decimal);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = jabatanData.MaxSalary;

                _sqlParameter[6] = new SqlParameter("@Fasilitas", SqlDbType.VarChar, 200);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = jabatanData.Fasilitas;

                _sqlParameter[7] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = jabatanData.IsActive;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteJabatan(int kdDivisi, string kdJabatan)
        {
            try
            {
                string _spName = "spr_RMS_DeleteJabatan";

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdJabatan;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<JabatanData> GetUnitMaxJabatanList(int kdDivisi, string kdUnit)
        {
            try
            {
                string _spName = "spr_RMS_GetUnitMaxJabatanList";
                List<JabatanData> _listJabatan = new List<JabatanData>();
                JabatanData _itemJabatan = new JabatanData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdUnit;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemJabatan = new JabatanData();
                            _itemJabatan.KdJabatan = _reader["KdJabatan"].ToString();
                            _itemJabatan.NmJabatan = _reader["NmJabatan"].ToString();
                            _listJabatan.Add(_itemJabatan);
                        }
                    }
                }

                return _listJabatan;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
