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
    public class JobDescDB
    {
        public List<JobDescData> GetJobDescList(int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                string _spName = "spr_RMS_GetJobDescList";
                List<JobDescData> _listJobDesc = new List<JobDescData>();
                JobDescData _itemJobDesc = new JobDescData();

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
                            _itemJobDesc = new JobDescData();
                            _itemJobDesc.KdJobDesc = Convert.ToInt32(_reader["KdJobDesc"].ToString());
                            _itemJobDesc.JobDesc = _reader["JobDesc"].ToString();
                            _listJobDesc.Add(_itemJobDesc);
                        }
                    }
                }

                return _listJobDesc;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddJobDesc(JobDescData jobDescData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateJobDesc";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = jobDescData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = jobDescData.KdSO;

                _sqlParameter[2] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = jobDescData.KdJabatan;

                _sqlParameter[3] = new SqlParameter("@JobDesc", SqlDbType.VarChar, 250);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = jobDescData.JobDesc;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int UpdateJobDesc(JobDescData jobDescData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateJobDesc";

                SqlParameter[] _sqlParameter = new SqlParameter[5];
                _sqlParameter[0] = new SqlParameter("@KdJobDesc", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = jobDescData.KdJobDesc;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = jobDescData.KdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = jobDescData.KdSO;

                _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = jobDescData.KdJabatan;

                _sqlParameter[4] = new SqlParameter("@JobDesc", SqlDbType.VarChar, 250);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = jobDescData.JobDesc;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteJobDesc(int kdJobDesc)
        {
            try
            {
                string _spName = "spr_RMS_DeleteJobDesc";

                SqlParameter _sqlParameter = new SqlParameter("@KdJobDesc", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdJobDesc;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
