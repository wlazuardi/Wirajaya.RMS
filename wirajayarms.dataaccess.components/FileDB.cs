using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.CrossCutting.OptManagement;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace WirajayaRMS.DataAccess.Components
{
    public class FileDB
    {
        public int AddKandidatCV(FileData fileData, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateKandidatCVFile";

                SqlParameter[] _sqlParameter = new SqlParameter[5];
                _sqlParameter[0] = new SqlParameter("@KdFile", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = fileData.KdFile;

                _sqlParameter[1] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = fileData.KdKandidat;

                _sqlParameter[2] = new SqlParameter("@NmFile", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = fileData.NmFile;

                _sqlParameter[3] = new SqlParameter("@NmFileAsli", SqlDbType.VarChar, 100);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = fileData.NmFileAsli;

                _sqlParameter[4] = new SqlParameter("@FileSize", SqlDbType.BigInt);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = fileData.FileSize;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<FileData> GetKandidatCVList(int kdKandidat)
        {
            try
            {
                string _spName = "spr_RMS_GetKandidatCVFileByKdKandidat";

                FileData _fileData = new FileData();
                List<FileData> _listFile = new List<FileData>();

                SqlParameter _sqlParameter = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdKandidat;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _fileData = new FileData();
                            _fileData.KdFile = Convert.ToInt32(_reader["KdFile"]);
                            _fileData.NmFile = _reader["NmFile"].ToString();
                            _fileData.NmFileAsli = _reader["NmFileAsli"].ToString();
                            _fileData.FileSize = Convert.ToInt64(_reader["FileSize"]);
                            _listFile.Add(_fileData);
                        }
                    }
                }

                return _listFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteKandidatCV(int kdFile, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_DeleteKandidatCVFile";

                FileData _fileData = new FileData();
                List<FileData> _listFile = new List<FileData>();

                SqlParameter _sqlParameter = new SqlParameter("@KdFile", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdFile;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }       
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
