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
    public class LevelApprovalDB
    {
        public List<LevelApprovalData> GetLevelApprovalList(int kdDivisi, string parentKdLevelApproval)
        {
            try
            {
                string _spName = "spr_RMS_GetLevelApprovalList";
                List<LevelApprovalData> _listLvApproval = new List<LevelApprovalData>();
                LevelApprovalData _itemLvApproval = new LevelApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@ParentKdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                if (parentKdLevelApproval == null)
                    _sqlParameter[1].Value = DBNull.Value;
                else
                    _sqlParameter[1].Value = parentKdLevelApproval;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemLvApproval = new LevelApprovalData();
                            _itemLvApproval.KdDivisi = Convert.ToInt32(_reader["KdDivisi"].ToString());
                            _itemLvApproval.NmLevelApproval = _reader["NmLevelApproval"].ToString();
                            _itemLvApproval.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _itemLvApproval.ParentKdLevelApproval = _reader["ParentKdLevelApproval"].ToString();
                            _itemLvApproval.StatusDokumen = _reader["StatusDokumen"].ToString();
                            _itemLvApproval.IsActive = Convert.ToInt32(_reader["IsActive"]);
                            _listLvApproval.Add(_itemLvApproval);
                        }
                    }
                }

                return _listLvApproval;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertLevelApproval(LevelApprovalData lvApprovalData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateLevelApproval";

                SqlParameter[] _sqlParameter = new SqlParameter[6];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = lvApprovalData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = lvApprovalData.KdLevelApproval;

                _sqlParameter[2] = new SqlParameter("@NmLevelApproval", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = lvApprovalData.NmLevelApproval;

                _sqlParameter[3] = new SqlParameter("@ParentKdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = lvApprovalData.ParentKdLevelApproval;

                _sqlParameter[4] = new SqlParameter("@StatusDokumen", SqlDbType.VarChar, 200);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = lvApprovalData.StatusDokumen;

                _sqlParameter[5] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = lvApprovalData.IsActive;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateLevelApproval(LevelApprovalData lvApprovalData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateLevelApproval";

                SqlParameter[] _sqlParameter = new SqlParameter[6];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = lvApprovalData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = lvApprovalData.KdLevelApproval;

                _sqlParameter[2] = new SqlParameter("@NmLevelApproval", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = lvApprovalData.NmLevelApproval;

                _sqlParameter[3] = new SqlParameter("@ParentKdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = lvApprovalData.ParentKdLevelApproval;

                _sqlParameter[4] = new SqlParameter("@StatusDokumen", SqlDbType.VarChar, 200);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = lvApprovalData.StatusDokumen;

                _sqlParameter[5] = new SqlParameter("@IsActive", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = lvApprovalData.IsActive;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LevelApprovalData GetLevelApprovalData(int kdDivisi, string kdLevelApproval)
        {
            try
            {
                string _spName = "spr_RMS_GetLevelApprovalData";
                LevelApprovalData _itemLevelApproval = new LevelApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdLevelApproval;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _itemLevelApproval = new LevelApprovalData();
                            _itemLevelApproval.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _itemLevelApproval.NmDivisi = _reader["NmDivisi"].ToString();
                            _itemLevelApproval.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _itemLevelApproval.NmLevelApproval = _reader["NmLevelApproval"].ToString();
                            _itemLevelApproval.ParentKdLevelApproval = _reader["ParentKdLevelApproval"].ToString();
                            _itemLevelApproval.ParentNmLevelApproval = _reader["ParentNmLevelApproval"].ToString();
                            _itemLevelApproval.StatusDokumen = _reader["StatusDokumen"].ToString();
                            _itemLevelApproval.IsActive = Convert.ToInt32(_reader["IsActive"]);
                        }
                    }
                }

                return _itemLevelApproval;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteLevelApproval(int kdDivisi, string kdLevelApproval)
        {
            try
            {
                string _spName = "spr_RMS_DeleteLevelApproval";
                LevelApprovalData _itemLevelApproval = new LevelApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdLevelApproval;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
