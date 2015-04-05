using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.CrossCutting.OptManagement;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace WirajayaRMS.DataAccess.Components
{
    public class DivisiDB
    {
        public List<DivisiData> GetDivisiList()
        {
            try
            {
                string _spName = "spr_RMS_GetDivisiList";
                List<DivisiData> _list = new List<DivisiData>();
                DivisiData _data = new DivisiData();

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _data = new DivisiData();
                            _data.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _data.NmDivisi = _reader["NmDivisi"].ToString();
                            _list.Add(_data);
                        }
                    }
                }

                return _list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DivisiData> GetDivisiList(int kdUser)
        {
            try
            {
                string _spName = "spr_RMS_GetDivisiListByKdUser";
                
                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                List<DivisiData> _list = new List<DivisiData>();
                DivisiData _data = new DivisiData();

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _data = new DivisiData();
                            _data.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _data.NmDivisi = _reader["NmDivisi"].ToString();
                            _list.Add(_data);
                        }
                    }
                }

                return _list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DivisiData GetDivisiData(int kdDivisi)
        {
            try
            {
                string _spName = "spr_RMS_GetDivisiData";

                SqlParameter _sqlParameter = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdDivisi;

                DivisiData _data = new DivisiData();

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _data = new DivisiData();
                            _data.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _data.NmDivisi = _reader["NmDivisi"].ToString();
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
    }
}
