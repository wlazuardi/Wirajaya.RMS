using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using System.Data.SqlClient;
using System.Data;
using WirajayaRMS.CrossCutting.OptManagement;
using Microsoft.ApplicationBlocks.Data;

namespace WirajayaRMS.DataAccess.Components
{
    public class KandidatDB
    {
        public KandidatData GetKandidatData(int kdKandidat)
        {
            try
            {
                string _spName = "spr_RMS_GetKandidatData";

                KandidatData _kandidatData = new KandidatData();

                SqlParameter _sqlParameter = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdKandidat;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _kandidatData = new KandidatData();
                            _kandidatData.KdKandidat = Convert.ToInt32(_reader["KdKandidat"]);
                            _kandidatData.TglProses = Convert.ToDateTime(_reader["TglProses"]);
                            _kandidatData.UserIn = new UserData();
                            _kandidatData.UserIn.KdUser = Convert.ToInt32(_reader["UserIn"]);
                            _kandidatData.UserIn.FullName = _reader["NmUserIn"].ToString();
                            _kandidatData.NmKandidat = _reader["NmKandidat"].ToString();
                            _kandidatData.Gender = Convert.ToChar(_reader["Gender"]);
                            _kandidatData.NoIdentitas = _reader["NoIdentitas"].ToString();
                            _kandidatData.NoHandphone = _reader["NoHandphone"].ToString();
                            _kandidatData.Email = _reader["Email"].ToString();
                        }
                    }
                }

                return _kandidatData;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public KandidatData GetKandidatData(int kdKandidat, string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentKandidatData";

                KandidatData _kandidatData = new KandidatData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdKandidat;

                _sqlParameter[1] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _kandidatData = new KandidatData();
                            _kandidatData.KdKandidat = Convert.ToInt32(_reader["KdKandidat"]);
                            _kandidatData.TglProses = Convert.ToDateTime(_reader["TglProses"]);
                            _kandidatData.UserIn = new UserData();
                            _kandidatData.UserIn.KdUser = Convert.ToInt32(_reader["UserIn"]);
                            _kandidatData.UserIn.FullName = _reader["NmUserIn"].ToString();
                            _kandidatData.NmKandidat = _reader["NmKandidat"].ToString();
                            _kandidatData.Gender = Convert.ToChar(_reader["Gender"]);
                            _kandidatData.NoIdentitas = _reader["NoIdentitas"].ToString();
                            _kandidatData.NoHandphone = _reader["NoHandphone"].ToString();
                            _kandidatData.Email = _reader["Email"].ToString();
                            _kandidatData.Rate = Convert.ToInt32(_reader["Rate"]);
                            _kandidatData.IsPassed = Convert.ToInt32(_reader["IsPassed"]);
                        }
                    }
                }

                return _kandidatData;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<InterviewData> GetInterviewResultList(int kdKandidat)
        {
            try
            {
                string _spName = "spr_RMS_GetInterviewKandidatList";

                InterviewData _interviewData = new InterviewData();
                List<InterviewData> _listInterview = new List<InterviewData>();

                SqlParameter _sqlParameter = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdKandidat;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _interviewData = new InterviewData();
                            _interviewData.KdInterview = Convert.ToInt32(_reader["KdInterviewKandidat"]);
                            _interviewData.KdKandidat = Convert.ToInt32(_reader["KdKandidat"]);
                            _interviewData.Interviewer = _reader["Interviewer"].ToString();
                            _interviewData.TglInterview = Convert.ToDateTime(_reader["TglInterview"]);
                            _interviewData.Hasil = _reader["Hasil"].ToString();
                            _listInterview.Add(_interviewData);
                        }
                    }
                }

                return _listInterview;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int InsertUpdateInterviewData(InterviewData interviewData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateInterviewKandidat";

                SqlParameter[] _sqlParameter = new SqlParameter[5];
                _sqlParameter[0] = new SqlParameter("@KdInterviewKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                if (interviewData.KdInterview == 0)
                    _sqlParameter[0].Value = DBNull.Value;
                else
                    _sqlParameter[0].Value = interviewData.KdInterview;

                _sqlParameter[1] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = interviewData.KdKandidat;

                _sqlParameter[2] = new SqlParameter("@Interviewer", SqlDbType.VarChar, 100);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = interviewData.Interviewer;

                _sqlParameter[3] = new SqlParameter("@TglInterview", SqlDbType.DateTime);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = interviewData.TglInterview;

                _sqlParameter[4] = new SqlParameter("@Hasil", SqlDbType.VarChar, 250);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = interviewData.Hasil;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteInterviewData(int kdInterview)
        {
            try
            {
                string _spName = "spr_RMS_DeleteInterviewKandidat";

                SqlParameter _sqlParameter = new SqlParameter("@KdInterviewKandidat", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdInterview;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<QualificationMatchingData> GetQualificationMatchingList(int kdKandidat, string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetKandidatQualificationList";

                QualificationMatchingData _qualifcationMatching = new QualificationMatchingData();
                List<QualificationMatchingData> _listQualificationMatching = new List<QualificationMatchingData>();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;                
                _sqlParameter[0].Value = kdKandidat;

                _sqlParameter[1] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _qualifcationMatching = new QualificationMatchingData();
                            _qualifcationMatching.Qualification = new QualificationData();
                            _qualifcationMatching.Qualification.KdQualification = Convert.ToInt32(_reader["KdQualification"]);
                            _qualifcationMatching.Qualification.NoRequest = _reader["NoRequest"].ToString();
                            _qualifcationMatching.KdKandidat = Convert.ToInt32(_reader["KdKandidat"]);
                            _qualifcationMatching.Qualification.Qualification = _reader["Qualification"].ToString();
                            _qualifcationMatching.IsMatch = Convert.ToBoolean(_reader["IsMatch"]);
                            _qualifcationMatching.Komentar = _reader["Komentar"].ToString();                            
                            _listQualificationMatching.Add(_qualifcationMatching);
                        }
                    }
                }

                return _listQualificationMatching;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<QualificationMatchingData> GetAdditionalQualificationList(int kdKandidat)
        {
            try
            {
                string _spName = "spr_RMS_GetKandidatAdditionalQualificationList";

                QualificationMatchingData _additionalQualification = new QualificationMatchingData();
                List<QualificationMatchingData> _listAdditionalQualification = new List<QualificationMatchingData>();

                SqlParameter _sqlParameter = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdKandidat;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _additionalQualification = new QualificationMatchingData();
                            _additionalQualification.Qualification = new QualificationData();
                            _additionalQualification.Qualification.KdQualification = Convert.ToInt32(_reader["KdAdditionalQualification"]);                            
                            _additionalQualification.KdKandidat = Convert.ToInt32(_reader["KdKandidat"]);
                            _additionalQualification.Qualification.Qualification = _reader["Qualification"].ToString();
                            _additionalQualification.Komentar = _reader["Komentar"].ToString();
                            _listAdditionalQualification.Add(_additionalQualification);
                        }
                    }
                }

                return _listAdditionalQualification;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddQualificationMatchingList(List<QualificationMatchingData> qualificationMatchingList)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach(QualificationMatchingData qualificationMatchingData in qualificationMatchingList)
                        {
                            int result = AddQualificationMatchingData(qualificationMatchingData, conn, trans);
                        }
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public int AddQualificationMatchingData(QualificationMatchingData qualificationMatchingData, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateKandidatQualification";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = qualificationMatchingData.KdKandidat;

                _sqlParameter[1] = new SqlParameter("@KdQualification", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = qualificationMatchingData.Qualification.KdQualification;

                _sqlParameter[2] = new SqlParameter("@IsMatch", SqlDbType.Int);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = qualificationMatchingData.IsMatch;

                _sqlParameter[3] = new SqlParameter("@Komentar", SqlDbType.VarChar, 200);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = qualificationMatchingData.Komentar;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int AddAdditionalQualificationList(List<QualificationMatchingData> listAdditionalQualification)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        List<QualificationMatchingData> existingQualifactionList = GetAdditionalQualificationList(listAdditionalQualification[0].KdKandidat);                        

                        foreach (QualificationMatchingData existingQualifactionData in existingQualifactionList)
                        { 
                            bool exists = listAdditionalQualification.Exists(i => i.Qualification.KdQualification == existingQualifactionData.Qualification.KdQualification);
                            if (!exists) 
                            {
                                int deleteResult = DeleteAdditionalQualification(existingQualifactionData.Qualification.KdQualification, conn, trans);
                            }
                        }

                        foreach (QualificationMatchingData additionalQualificationData in listAdditionalQualification)
                        {
                            int result = AddAdditionalQualificationData(additionalQualificationData, conn, trans);
                        }
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public int AddAdditionalQualificationData(QualificationMatchingData additionalQualificationData, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateKandidatAdditionalQualification";

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdAdditionalQualification", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = additionalQualificationData.Qualification.KdQualification;

                _sqlParameter[1] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = additionalQualificationData.KdKandidat;

                _sqlParameter[2] = new SqlParameter("@Qualification", SqlDbType.VarChar, 250);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = additionalQualificationData.Qualification.Qualification;

                _sqlParameter[3] = new SqlParameter("@Komentar", SqlDbType.VarChar, 200);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = additionalQualificationData.Komentar;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteAdditionalQualification(int kdAdditionalQualification, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_DeleteKandidatAdditionalQualification";

                SqlParameter _sqlParameter = new SqlParameter("@KdAdditionalQualification", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdAdditionalQualification;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddUpdateKandidat(KandidatData dataKandidat, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateKandidat";

                SqlParameter[] _sqlParameter = new SqlParameter[7];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                if (dataKandidat.KdKandidat == 0)
                    _sqlParameter[0].Value = DBNull.Value;
                else
                    _sqlParameter[0].Value = dataKandidat.KdKandidat;

                _sqlParameter[1] = new SqlParameter("@NoIdentitas", SqlDbType.VarChar, 20);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = dataKandidat.NoIdentitas;

                _sqlParameter[2] = new SqlParameter("@NmKandidat", SqlDbType.VarChar, 200);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = dataKandidat.NmKandidat;

                _sqlParameter[3] = new SqlParameter("@Gender", SqlDbType.Char, 1);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = dataKandidat.Gender;

                _sqlParameter[4] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = dataKandidat.Email;

                _sqlParameter[5] = new SqlParameter("@NoHandphone", SqlDbType.VarChar, 20);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = dataKandidat.NoHandphone;

                _sqlParameter[6] = new SqlParameter("@UserIn", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = dataKandidat.UserIn.KdUser;

                return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int EditKandidat(KandidatData dataKandidat)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int kdKandidat = new KandidatDB().AddUpdateKandidat(dataKandidat, conn, trans);

                        int success = new RecruitmentDB().AddRecruitmentKandidatDetail(dataKandidat, conn, trans);

                        List<FileData> _listExistingFile = new FileDB().GetKandidatCVList(kdKandidat);
                        List<FileData> _listExistingFileUpdated = dataKandidat.CVFiles.Where(item => item.KdFile != 0).ToList();
                        foreach(FileData _fileData in _listExistingFile)
                        {
                            // Cari file yang sudah tidak ada dan hapus file tersebut
                            if (!_listExistingFileUpdated.Exists(item => item.KdFile == _fileData.KdFile)) 
                            {
                                int result = new FileDB().DeleteKandidatCV(_fileData.KdFile, conn, trans);
                                if (result == 0) 
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                            }
                        }                        

                        List<FileData> _listNewFile = dataKandidat.CVFiles.Where(item => item.KdFile == 0).ToList();
                        foreach (FileData fileData in _listNewFile)
                        {
                            fileData.KdKandidat = kdKandidat;
                            int addFileResult = new FileDB().AddKandidatCV(fileData, conn, trans);
                            if (addFileResult == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                        }

                        trans.Commit();
                        return kdKandidat;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public int EditKandidat(KandidatData dataKandidat, List<PositionData> listPosisi)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int kdKandidat = new KandidatDB().AddUpdateKandidat(dataKandidat, conn, trans);

                        List<FileData> _listExistingFile = new FileDB().GetKandidatCVList(kdKandidat);
                        List<FileData> _listExistingFileUpdated = dataKandidat.CVFiles.Where(item => item.KdFile != 0).ToList();
                        foreach (FileData _fileData in _listExistingFile)
                        {
                            // Cari file yang sudah tidak ada dan hapus file tersebut
                            if (!_listExistingFileUpdated.Exists(item => item.KdFile == _fileData.KdFile))
                            {
                                int result = new FileDB().DeleteKandidatCV(_fileData.KdFile, conn, trans);
                                if (result == 0)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                            }
                        }

                        int deleteAllPositionResult = new PositionDB().DeleteAllPosition(kdKandidat, conn, trans);
                        if (deleteAllPositionResult == 0) 
                        {
                            trans.Rollback();
                            return 0;
                        }

                        List<FileData> _listNewFile = dataKandidat.CVFiles.Where(item => item.KdFile == 0).ToList();
                        foreach (FileData fileData in _listNewFile)
                        {
                            fileData.KdKandidat = kdKandidat;
                            int addFileResult = new FileDB().AddKandidatCV(fileData, conn, trans);
                            if (addFileResult == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                        }

                        foreach (PositionData positionData in listPosisi)
                        {
                            int result = new PositionDB().AddEditPosition(kdKandidat, positionData, conn, trans);
                            if (result == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                        }

                        trans.Commit();
                        return kdKandidat;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public List<KandidatData> GetKandidatList(int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                string _spName = "spr_RMS_GetKandidatList";

                List<KandidatData> _listKandidat = new List<KandidatData>();
                KandidatData _kandidatData = new KandidatData();

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                if (kdDivisi == 0)
                    _sqlParameter[0].Value = DBNull.Value;
                else
                    _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                if(kdSO == "0")
                    _sqlParameter[1].Value = DBNull.Value;
                else
                    _sqlParameter[1].Value = kdSO;

                _sqlParameter[2] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                if(kdJabatan == "0")
                    _sqlParameter[2].Value = DBNull.Value;
                else
                    _sqlParameter[2].Value = kdJabatan;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _kandidatData = new KandidatData();
                            _kandidatData.KdKandidat = Convert.ToInt32(_reader["KdKandidat"]);
                            _kandidatData.TglProses = Convert.ToDateTime(_reader["TglProses"]);
                            _kandidatData.UserIn = new UserData();
                            _kandidatData.UserIn.KdUser = Convert.ToInt32(_reader["UserIn"]);
                            _kandidatData.UserIn.FullName = _reader["NmUserIn"].ToString();
                            _kandidatData.NmKandidat = _reader["NmKandidat"].ToString();
                            _kandidatData.Gender = Convert.ToChar(_reader["Gender"]);
                            _kandidatData.NoIdentitas = _reader["NoIdentitas"].ToString();
                            _kandidatData.NoHandphone = _reader["NoHandphone"].ToString();
                            _kandidatData.Email = _reader["Email"].ToString();
                            _kandidatData.Divisi = new DivisiData();
                            _kandidatData.Divisi.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _kandidatData.Divisi.NmDivisi = _reader["NmDivisi"].ToString();
                            _kandidatData.StrukturOrganisasi = new StrukturOrganisasiData();
                            _kandidatData.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                            _kandidatData.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();                            
                            _kandidatData.Jabatan = new JabatanData();
                            _kandidatData.Jabatan.KdJabatan = _reader["KdJabatan"].ToString();
                            _kandidatData.Jabatan.NmJabatan = _reader["NmJabatan"].ToString();
                            _listKandidat.Add(_kandidatData);
                        }
                    }
                }

                return _listKandidat;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int AddKandidat(KandidatData dataKandidat, List<PositionData> listPosisi)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int kdKandidat = new KandidatDB().AddUpdateKandidat(dataKandidat, conn, trans);

                        foreach (FileData fileData in dataKandidat.CVFiles)
                        {
                            fileData.KdKandidat = kdKandidat;
                            int addFileResult = new FileDB().AddKandidatCV(fileData, conn, trans);
                            if (addFileResult == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                        }

                        foreach (PositionData positionData in listPosisi)
                        {
                            int result = new PositionDB().AddEditPosition(kdKandidat, positionData, conn, trans);
                            if (result == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                        }

                        trans.Commit();
                        return kdKandidat;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public List<KandidatData> GetKandidatNotInRequest(int kdDivisi, string kdSO, string kdJabatan, string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetKandidatListNotInRequest";

                List<KandidatData> _listKandidat = new List<KandidatData>();
                KandidatData _kandidatData = new KandidatData();

                SqlParameter[] _sqlParameter = new SqlParameter[4];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdSO;

                _sqlParameter[2] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = kdJabatan;

                _sqlParameter[3] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _kandidatData = new KandidatData();
                            _kandidatData.KdKandidat = Convert.ToInt32(_reader["KdKandidat"]);
                            _kandidatData.NmKandidat = _reader["NmKandidat"].ToString();
                            _kandidatData.Gender = Convert.ToChar(_reader["Gender"]);
                            _kandidatData.NoIdentitas = _reader["NoIdentitas"].ToString();
                            _kandidatData.NoHandphone = _reader["NoHandphone"].ToString();
                            _kandidatData.Email = _reader["Email"].ToString();
                            _kandidatData.Rate = Convert.ToInt32(_reader["Rate"]);
                            _listKandidat.Add(_kandidatData);
                        }
                    }
                }

                return _listKandidat;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
