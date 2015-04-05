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
    public class UnitDB
    {
        public List<UnitData> GetListUnit(int kdDivisi, string parentKdUnit)
        {
            try
            {
                string _spName = "spr_RMS_GetUnitList";
                List<UnitData> _listUnit = new List<UnitData>();
                UnitData _itemJabatan = new UnitData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@ParentKdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                if (parentKdUnit == null)
                    _sqlParameter[1].Value = DBNull.Value;
                else
                    _sqlParameter[1].Value = parentKdUnit;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemJabatan = new UnitData();
                            _itemJabatan.KdUnit = _reader["KdUnit"].ToString();
                            _itemJabatan.NmUnit = _reader["NmUnit"].ToString();
                            _itemJabatan.ParentKdUnit = _reader["ParentKdUnit"].ToString();
                            _itemJabatan.IsActive = Convert.ToInt32(_reader["IsActive"]);
                            _listUnit.Add(_itemJabatan);
                        }
                    }
                }

                return _listUnit;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertUnit(UnitData unitData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateUnit";

                SqlParameter[] _sqlParameter = new SqlParameter[6];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = unitData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = unitData.KdUnit;

                _sqlParameter[2] = new SqlParameter("@NamaUnit", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = unitData.NmUnit;

                _sqlParameter[3] = new SqlParameter("@ParentKdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = unitData.ParentKdUnit;

                _sqlParameter[4] = new SqlParameter("@MaxJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = unitData.MaxKdJabatan;

                _sqlParameter[5] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = unitData.IsActive;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UnitData GetUnitData(int kdDivisi, string kdUnit)
        {
            try
            {
                string _spName = "spr_RMS_GetUnitData";
                UnitData _data = new UnitData();

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
                        if (_reader.Read())
                        {
                            _data.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _data.NmDivisi = _reader["NmDivisi"].ToString();
                            _data.KdUnit = _reader["KdUnit"].ToString();
                            _data.NmUnit = _reader["NmUnit"].ToString();
                            _data.MaxKdJabatan = _reader["MaxJabatan"].ToString();
                            _data.ParentKdUnit = _reader["ParentKdUnit"].ToString();
                            _data.ParentNmUnit = _reader["ParentNmUnit"].ToString();
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

        public int UpdateUnit(UnitData unitData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateUnit";

                SqlParameter[] _sqlParameter = new SqlParameter[6];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = unitData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = unitData.KdUnit;

                _sqlParameter[2] = new SqlParameter("@NamaUnit", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = unitData.NmUnit;

                _sqlParameter[3] = new SqlParameter("@ParentKdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = unitData.ParentKdUnit;

                _sqlParameter[4] = new SqlParameter("@MaxJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = unitData.MaxKdJabatan;

                _sqlParameter[5] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = unitData.IsActive;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteUnit(int kdDivisi, string kdUnit)
        {
            try
            {
                string _spName = "spr_RMS_DeleteUnit";
                JabatanData _data = new JabatanData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdUnit", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdUnit;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
