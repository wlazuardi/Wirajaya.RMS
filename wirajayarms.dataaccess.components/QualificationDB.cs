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
    public class QualificationDB
    {
        public List<QualificationData> GetQualificationList(int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                string _spName = "spr_RMS_GetQualificationList";
                List<QualificationData> _listQualification = new List<QualificationData>();
                QualificationData _itemQualification = new QualificationData();

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdSO;

                _sqlParameter[2] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = kdJabatan;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemQualification = new QualificationData();
                            _itemQualification.KdQualification = Convert.ToInt32(_reader["KdQualification"].ToString());
                            _itemQualification.Qualification = _reader["Qualification"].ToString();
                            _listQualification.Add(_itemQualification);
                        }
                    }
                }

                return _listQualification;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddQualification(QualificationData QualificationData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateQualification";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = QualificationData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = QualificationData.KdSO;

                _sqlParameter[2] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = QualificationData.KdJabatan;

                _sqlParameter[3] = new SqlParameter("@Qualification", SqlDbType.VarChar, 250);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = QualificationData.Qualification;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int UpdateQualification(QualificationData QualificationData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateQualification";

                SqlParameter[] _sqlParameter = new SqlParameter[5];
                _sqlParameter[0] = new SqlParameter("@KdQualification", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = QualificationData.KdQualification;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = QualificationData.KdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = QualificationData.KdSO;

                _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = QualificationData.KdJabatan;

                _sqlParameter[4] = new SqlParameter("@Qualification", SqlDbType.VarChar, 250);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = QualificationData.Qualification;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteQualification(int kdQualification)
        {
            try
            {
                string _spName = "spr_RMS_DeleteQualification";

                SqlParameter _sqlParameter = new SqlParameter("@KdQualification", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdQualification;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
