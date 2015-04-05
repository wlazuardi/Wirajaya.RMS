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
    public class PositionDB
    {
        //public int AddEditPosition(int kdKandidat, List<PositionData> listPosisi) 
        //{
        //    using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
        //    {
        //        conn.Open();

        //        using (SqlTransaction trans = conn.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Delete dulu semua position yang dilamar, baru add lagi
        //                int deleteResult = DeleteAllPosition(kdKandidat, conn, trans);
        //                if (deleteResult == 0) {
        //                    throw new Exception("Operation failed, no data inserted or updated");
        //                }

        //                foreach(PositionData positionData in listPosisi)
        //                {
        //                    int result = AddEditPosition(kdKandidat, positionData, conn, trans);
        //                    if (result == 0)
        //                    {
        //                        throw new Exception("Operation failed, no data inserted or updated");
        //                    }
        //                }

        //                trans.Commit();
        //                return 1;
        //            }
        //            catch(Exception ex)
        //            {
        //                trans.Rollback();
        //                return 0;
        //            }
        //        }
        //    }
        //}

        public int DeleteAllPosition(int kdKandidat, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_DeleteAllKandidatPosition";

                List<PositionData> _listPosisi = new List<PositionData>();

                SqlParameter _sqlParameter = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = DBNull.Value;
                _sqlParameter.Value = kdKandidat;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public int AddEditPosition(int kdKandidat, PositionData positionData, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateRecruitmentKandidat";

                SqlParameter[] _sqlParameter = new SqlParameter[8];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdKandidat;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = positionData.Divisi.KdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = positionData.StrukturOrganisasi.KdSO;

                _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = positionData.Jabatan.KdJabatan;

                _sqlParameter[4] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                if (positionData.NoRequest == null || positionData.NoRequest == "")
                    _sqlParameter[4].Value = DBNull.Value;
                else
                    _sqlParameter[4].Value = positionData.NoRequest;

                _sqlParameter[5] = new SqlParameter("@UserIn", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = positionData.UserIn.KdUser;

                _sqlParameter[6] = new SqlParameter("@IsPassed", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = positionData.IsPassed;

                _sqlParameter[7] = new SqlParameter("@Rate", SqlDbType.Int);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = positionData.Rate;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<PositionData> GetPosisiKandidat(int kdKandidat)
        {
            try
            {
                string _spName = "spr_RMS_GetPosisiKandidat";

                List<PositionData> _listPosisi = new List<PositionData>();
                PositionData _posisiData = new PositionData();

                SqlParameter _sqlParameter = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = DBNull.Value;
                _sqlParameter.Value = kdKandidat;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _posisiData = new PositionData();
                            _posisiData.Divisi = new DivisiData();
                            _posisiData.Divisi.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _posisiData.Divisi.NmDivisi = _reader["NmDivisi"].ToString();
                            _posisiData.StrukturOrganisasi = new StrukturOrganisasiData();
                            _posisiData.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                            _posisiData.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _posisiData.Jabatan = new JabatanData();
                            _posisiData.Jabatan.KdJabatan = _reader["KdJabatan"].ToString();
                            _posisiData.Jabatan.NmJabatan = _reader["NmJabatan"].ToString();
                            _posisiData.UserIn = new UserData();
                            _posisiData.UserIn.KdUser = Convert.ToInt32(_reader["UserIn"]);
                            _posisiData.IsPassed = Convert.ToInt32(_reader["IsPassed"]);
                            _posisiData.Rate = Convert.ToInt32(_reader["Rate"]);
                            _posisiData.TglProses = Convert.ToDateTime(_reader["TglProses"]);
                            _posisiData.NoRequest = _reader["NoRequest"].ToString();
                            _listPosisi.Add(_posisiData);
                        }
                    }
                }

                return _listPosisi;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeletePosisi(int kdKandidat, int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                string _spName = "spr_RMS_DeleteKandidatPosition";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdKandidat;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = kdSO;

                _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = kdJabatan;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
