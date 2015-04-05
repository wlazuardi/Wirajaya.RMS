using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.CrossCutting.OptManagement;

namespace WirajayaRMS.DataAccess.Components
{
    public class RecruitmentDB
    {
        public List<RecruitmentData> GetRecruitmentList(int kdDivisi)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentList";
                List<RecruitmentData> _listRecruitment = new List<RecruitmentData>();
                RecruitmentData _itemRecruitment = new RecruitmentData();

                SqlParameter _sqlParameter = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdDivisi;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemRecruitment = new RecruitmentData();
                            _itemRecruitment.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _itemRecruitment.NoRequest = _reader["NoRequest"].ToString();
                            _itemRecruitment.TglButuh = Convert.ToDateTime(_reader["TglButuh"]);
                            _itemRecruitment.TglRequest = Convert.ToDateTime(_reader["TglRequest"]);
                            _itemRecruitment.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                            _itemRecruitment.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _itemRecruitment.Jabatan.KdJabatan = _reader["KdJabatan"].ToString();
                            _itemRecruitment.Jabatan.NmJabatan = _reader["NmJabatan"].ToString();
                            _itemRecruitment.JmlOrang = Convert.ToInt32(_reader["JmlOrang"]);
                            _itemRecruitment.KdAlasan = Convert.ToInt32(_reader["KdAlasan"]);
                            _itemRecruitment.CurrLevelApproval = _reader["CurrLevelApproval"].ToString();
                            _itemRecruitment.StatusDokumen = _reader["StatusDokumen"].ToString();
                            _itemRecruitment.Creator.KdUser = Convert.ToInt32(_reader["Creator"]);
                            _listRecruitment.Add(_itemRecruitment);
                        }
                    }
                }

                return _listRecruitment;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RecruitmentData> GetRecruitmentListOnHold(int kdUser, int kdDivisi, int kdSO, int kdJabatan)
        {
            string _spName = "spr_RMS_GetRecruitmentListOnHold";
            List<RecruitmentData> _listRecruitment = new List<RecruitmentData>();
            RecruitmentData _itemRecruitment = new RecruitmentData();

            SqlParameter[] _sqlParameter = new SqlParameter[4];
            _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
            _sqlParameter[0].Direction = ParameterDirection.Input;
            _sqlParameter[0].Value = kdUser;

            _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
            _sqlParameter[1].Direction = ParameterDirection.Input;
            if (kdDivisi == 0)
                _sqlParameter[1].Value = DBNull.Value;
            else
                _sqlParameter[1].Value = kdDivisi;

            _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
            _sqlParameter[2].Direction = ParameterDirection.Input;
            if (kdSO == 0)
                _sqlParameter[2].Value = DBNull.Value;
            else
                _sqlParameter[2].Value = kdSO;

            _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
            _sqlParameter[3].Direction = ParameterDirection.Input;
            if (kdJabatan == 0)
                _sqlParameter[3].Value = DBNull.Value;
            else
                _sqlParameter[3].Value = kdJabatan;

            using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
            {
                if (_reader.HasRows)
                {
                    while (_reader.Read())
                    {
                        _itemRecruitment = new RecruitmentData();
                        _itemRecruitment.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                        _itemRecruitment.NoRequest = _reader["NoRequest"].ToString();
                        _itemRecruitment.TglButuh = Convert.ToDateTime(_reader["TglButuh"]);
                        _itemRecruitment.TglRequest = Convert.ToDateTime(_reader["TglRequest"]);
                        _itemRecruitment.StrukturOrganisasi = new StrukturOrganisasiData();
                        _itemRecruitment.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                        _itemRecruitment.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                        _itemRecruitment.Jabatan = new JabatanData();
                        _itemRecruitment.Jabatan.KdJabatan = _reader["KdJabatan"].ToString();
                        _itemRecruitment.Jabatan.NmJabatan = _reader["NmJabatan"].ToString();
                        _itemRecruitment.JmlOrang = Convert.ToInt32(_reader["JmlOrang"]);
                        _itemRecruitment.KdAlasan = Convert.ToInt32(_reader["KdAlasan"]);
                        _itemRecruitment.CurrLevelApproval = _reader["CurrLevelApproval"].ToString();
                        _itemRecruitment.StatusDokumen = _reader["StatusDokumen"].ToString();
                        _itemRecruitment.Creator.KdUser = Convert.ToInt32(_reader["Creator"]);
                        _listRecruitment.Add(_itemRecruitment);
                    }
                }
            }

            return _listRecruitment;
        }

        public List<RecruitmentData> GetRecruitmentHistoryList(int kdUser, int kdDivisi, int kdSO, int kdJabatan)
        {
            string _spName = "spr_RMS_GetRecruitmentHistoryList";
            List<RecruitmentData> _listRecruitment = new List<RecruitmentData>();
            RecruitmentData _itemRecruitment = new RecruitmentData();

            SqlParameter[] _sqlParameter = new SqlParameter[4];
            _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
            _sqlParameter[0].Direction = ParameterDirection.Input;
            _sqlParameter[0].Value = kdUser;

            _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
            _sqlParameter[1].Direction = ParameterDirection.Input;
            if (kdDivisi == 0)
                _sqlParameter[1].Value = DBNull.Value;
            else
                _sqlParameter[1].Value = kdDivisi;

            _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
            _sqlParameter[2].Direction = ParameterDirection.Input;
            if (kdSO == 0)
                _sqlParameter[2].Value = DBNull.Value;
            else
                _sqlParameter[2].Value = kdSO;

            _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
            _sqlParameter[3].Direction = ParameterDirection.Input;
            if (kdJabatan == 0)
                _sqlParameter[3].Value = DBNull.Value;
            else
                _sqlParameter[3].Value = kdJabatan;

            using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
            {
                if (_reader.HasRows)
                {
                    while (_reader.Read())
                    {
                        _itemRecruitment = new RecruitmentData();
                        _itemRecruitment.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                        _itemRecruitment.NoRequest = _reader["NoRequest"].ToString();
                        _itemRecruitment.TglButuh = Convert.ToDateTime(_reader["TglButuh"]);
                        _itemRecruitment.TglRequest = Convert.ToDateTime(_reader["TglRequest"]);
                        _itemRecruitment.StrukturOrganisasi = new StrukturOrganisasiData();
                        _itemRecruitment.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                        _itemRecruitment.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                        _itemRecruitment.Jabatan = new JabatanData();
                        _itemRecruitment.Jabatan.KdJabatan = _reader["KdJabatan"].ToString();
                        _itemRecruitment.Jabatan.NmJabatan = _reader["NmJabatan"].ToString();
                        _itemRecruitment.JmlOrang = Convert.ToInt32(_reader["JmlOrang"]);
                        _itemRecruitment.KdAlasan = Convert.ToInt32(_reader["KdAlasan"]);
                        _itemRecruitment.CurrLevelApproval = _reader["CurrLevelApproval"].ToString();
                        _itemRecruitment.StatusDokumen = _reader["StatusDokumen"].ToString();
                        _itemRecruitment.Creator.KdUser = Convert.ToInt32(_reader["Creator"]);
                        _listRecruitment.Add(_itemRecruitment);
                    }
                }
            }

            return _listRecruitment;
        }

        public List<JobDescData> GetRecruitmentJobDescList(string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentJobDescList";
                List<JobDescData> _listJobDesc = new List<JobDescData>();
                JobDescData _itemJobDesc = new JobDescData();

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemJobDesc = new JobDescData();
                            _itemJobDesc.KdJobDesc = Convert.ToInt32(_reader["KdJobDesc"]);
                            _itemJobDesc.NoRequest = _reader["NoRequest"].ToString();
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

        public List<QualificationData> GetRecruitmentQualificationList(string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentQualificationList";
                List<QualificationData> _listQualification = new List<QualificationData>();
                QualificationData _itemQualification = new QualificationData();

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _itemQualification = new QualificationData();
                            _itemQualification.KdQualification = Convert.ToInt32(_reader["KdQualification"]);
                            _itemQualification.NoRequest = _reader["NoRequest"].ToString();
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

        public string AddNewRequest(RecruitmentData recruitmentData)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string _noRequest = new RecruitmentDB().AddNewRequestHeader(recruitmentData, conn, trans);

                        // Insert Approval Data (Komentar & History Progress)
                        if (recruitmentData.ApprovalDataList.Count > 0)
                        {
                            recruitmentData.ApprovalDataList[0].NoRequest = _noRequest;
                            int recApprovalAdd = new RecruitmentDB().AddEditApprovalRequest(recruitmentData.ApprovalDataList[0]);
                        }

                        //List<JobDescData> _existingJobDescList = GetRecruitmentJobDescList(_noRequest);
                        //foreach (JobDescData _existingJobDesc in _existingJobDescList)
                        //{
                        //    // If not exist then delete
                        //    if (!recruitmentData.JobDescList.Exists(j => j.KdJobDesc == _existingJobDesc.KdJobDesc))
                        //    {
                        //        int deleteResult = new RecruitmentDB().DeleteRecruitmentJobDesc(_existingJobDesc.KdJobDesc, conn, trans);
                        //    }
                        //}

                        //List<QualificationData> _existingQualificationList = GetRecruitmentQualificationList(_noRequest);
                        //foreach (QualificationData _existingQualification in _existingQualificationList)
                        //{
                        //    // If not exist then delete
                        //    if (!recruitmentData.QualificationList.Exists(q => q.KdQualification == _existingQualification.KdQualification))
                        //    {
                        //        int deleteResult = new RecruitmentDB().DeleteRecruitmentQualification(_existingQualification.KdQualification, conn, trans);
                        //    }
                        //}

                        foreach (JobDescData jobDesc in recruitmentData.JobDescList)
                        {
                            int result = new RecruitmentDB().AddRecruitmentJobDesc(jobDesc.KdJobDesc, _noRequest, jobDesc.JobDesc, conn, trans);
                        }

                        foreach (QualificationData qualification in recruitmentData.QualificationList)
                        {
                            int result = new RecruitmentDB().AddRecruitmentQualification(qualification.KdQualification, _noRequest, qualification.Qualification, conn, trans);
                        }

                        trans.Commit();
                        return _noRequest;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return "";
                    }
                }
            }
        }

        private int DeleteRecruitmentQualification(int kdQualification, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_DeleteRecruitmentQualification";

                SqlParameter _sqlParameter = new SqlParameter("@KdQualification", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdQualification;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private int DeleteRecruitmentJobDesc(int kdJobDesc, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_DeleteRecruitmentJobDesc";

                SqlParameter _sqlParameter = new SqlParameter("@KdJobDesc", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdJobDesc;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string AddNewRequestHeader(RecruitmentData recruitmentData, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateRecruitment";
                List<UserAccessData> _listUserAccess = new List<UserAccessData>();
                UserAccessData _itemUserAccess = new UserAccessData();

                SqlParameter[] _sqlParameter = new SqlParameter[9];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = recruitmentData.KdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = recruitmentData.StrukturOrganisasi.KdSO;

                _sqlParameter[2] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = recruitmentData.Jabatan.KdJabatan;

                _sqlParameter[3] = new SqlParameter("@TglButuh", SqlDbType.DateTime);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = recruitmentData.TglButuh;

                _sqlParameter[4] = new SqlParameter("@JmlOrang", SqlDbType.Int);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = recruitmentData.JmlOrang;

                _sqlParameter[5] = new SqlParameter("@KdAlasan", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = recruitmentData.KdAlasan;

                _sqlParameter[6] = new SqlParameter("@CurrLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = recruitmentData.CurrLevelApproval;

                _sqlParameter[7] = new SqlParameter("@Creator", SqlDbType.Int);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = recruitmentData.Creator.KdUser;

                _sqlParameter[8] = new SqlParameter("@UserUp", SqlDbType.Int);
                _sqlParameter[8].Direction = ParameterDirection.Input;
                _sqlParameter[8].Value = recruitmentData.UserUp.KdUser;

                return SqlHelper.ExecuteScalar(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter).ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddRecruitmentJobDesc(int kdJobDesc, string noRequest, string jobDesc, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertRecruitmentJobDesc";

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@KdJobDesc", SqlDbType.VarChar, 15);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdJobDesc;

                _sqlParameter[1] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = noRequest;

                _sqlParameter[2] = new SqlParameter("@JobDesc", SqlDbType.VarChar, 250);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = jobDesc;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddRecruitmentQualification(int kdQualification, string noRequest, string qualification, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertRecruitmentQualification";

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@KdQualification", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdQualification;

                _sqlParameter[1] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = noRequest;

                _sqlParameter[2] = new SqlParameter("@Qualification", SqlDbType.VarChar, 250);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = qualification;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RecruitmentData GetRecruitmentData(string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentData";
                RecruitmentData _itemRecruitment = new RecruitmentData();

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _itemRecruitment = new RecruitmentData();
                            _itemRecruitment.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _itemRecruitment.NoRequest = _reader["NoRequest"].ToString();
                            _itemRecruitment.TglButuh = Convert.ToDateTime(_reader["TglButuh"]);
                            _itemRecruitment.TglRequest = Convert.ToDateTime(_reader["TglRequest"]);
                            _itemRecruitment.StrukturOrganisasi = new StrukturOrganisasiData();
                            _itemRecruitment.StrukturOrganisasi.KdSO = _reader["KdSO"].ToString();
                            _itemRecruitment.StrukturOrganisasi.NmStrukturOrganisasi = _reader["NmSO"].ToString();
                            _itemRecruitment.Jabatan = new JabatanData();
                            _itemRecruitment.Jabatan.KdJabatan = _reader["KdJabatan"].ToString();
                            _itemRecruitment.Jabatan.NmJabatan = _reader["NmJabatan"].ToString();
                            _itemRecruitment.JmlOrang = Convert.ToInt32(_reader["JmlOrang"]);
                            _itemRecruitment.KdAlasan = Convert.ToInt32(_reader["KdAlasan"]);
                            _itemRecruitment.CurrLevelApproval = _reader["CurrLevelApproval"].ToString();
                            _itemRecruitment.StatusDokumen = _reader["StatusDokumen"].ToString();
                            _itemRecruitment.Creator.KdUser = Convert.ToInt32(_reader["Creator"]);
                        }
                    }
                }

                return _itemRecruitment;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateRequest(RecruitmentData recruitmentData)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string noRequest = new RecruitmentDB().UpdateRequestHeader(recruitmentData, conn, trans);

                        // Insert Approval Data (Komentar & History Progress)
                        if (recruitmentData.ApprovalDataList.Count > 0)
                        {
                            recruitmentData.ApprovalDataList[0].NoRequest = noRequest;
                            int recApprovalAdd = new RecruitmentDB().AddEditApprovalRequest(recruitmentData.ApprovalDataList[0]);
                        }

                        // Delete old job desc & qualification
                        //int jobDescDelete = new RecruitmentDB().DeleteAllJobDesc(recruitmentData.NoRequest, conn, trans);
                        //int qualificationDelete = new RecruitmentDB().DeleteAllQualification(recruitmentData.NoRequest, conn, trans);
                        List<JobDescData> _existingJobDescList = GetRecruitmentJobDescList(noRequest);
                        foreach (JobDescData _existingJobDesc in _existingJobDescList)
                        {
                            // If not exist then delete
                            if (!recruitmentData.JobDescList.Exists(j => j.KdJobDesc == _existingJobDesc.KdJobDesc))
                            {
                                int deleteResult = new RecruitmentDB().DeleteRecruitmentJobDesc(_existingJobDesc.KdJobDesc, conn, trans);
                            }
                        }

                        List<QualificationData> _existingQualificationList = GetRecruitmentQualificationList(noRequest);
                        foreach (QualificationData _existingQualification in _existingQualificationList)
                        {
                            // If not exist then delete
                            if (!recruitmentData.QualificationList.Exists(q => q.KdQualification == _existingQualification.KdQualification))
                            {
                                int deleteResult = new RecruitmentDB().DeleteRecruitmentQualification(_existingQualification.KdQualification, conn, trans);
                            }
                        }

                        // Insert new job desc & qualification
                        foreach (JobDescData jobDesc in recruitmentData.JobDescList)
                        {
                            int result = new RecruitmentDB().AddRecruitmentJobDesc(jobDesc.KdJobDesc, recruitmentData.NoRequest, jobDesc.JobDesc, conn, trans);
                        }

                        foreach (QualificationData qualification in recruitmentData.QualificationList)
                        {
                            int result = new RecruitmentDB().AddRecruitmentQualification(qualification.KdQualification, recruitmentData.NoRequest, qualification.Qualification, conn, trans);
                        }

                        trans.Commit();
                        return noRequest;

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return "";
                    }
                }
            }

        }

        public string UpdateRequestHeader(RecruitmentData recruitmentData, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateRecruitment";

                SqlParameter[] _sqlParameter = new SqlParameter[10];
                _sqlParameter[0] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = recruitmentData.NoRequest;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = recruitmentData.KdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = recruitmentData.StrukturOrganisasi.KdSO;

                _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = recruitmentData.Jabatan.KdJabatan;

                _sqlParameter[4] = new SqlParameter("@TglButuh", SqlDbType.DateTime);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = recruitmentData.TglButuh;

                _sqlParameter[5] = new SqlParameter("@JmlOrang", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = recruitmentData.JmlOrang;

                _sqlParameter[6] = new SqlParameter("@KdAlasan", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = recruitmentData.KdAlasan;

                _sqlParameter[7] = new SqlParameter("@CurrLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = recruitmentData.CurrLevelApproval;

                _sqlParameter[8] = new SqlParameter("@Creator", SqlDbType.Int);
                _sqlParameter[8].Direction = ParameterDirection.Input;
                _sqlParameter[8].Value = recruitmentData.Creator.KdUser;

                _sqlParameter[9] = new SqlParameter("@UserUp", SqlDbType.Int);
                _sqlParameter[9].Direction = ParameterDirection.Input;
                _sqlParameter[9].Value = recruitmentData.UserUp.KdUser;


                return SqlHelper.ExecuteScalar(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter).ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteAllJobDesc(string noRequest, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_DeleteAllRecruitmentJobDesc";

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteAllQualification(string noRequest, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_DeleteAllRecruitmentQualification";

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public int DeleteRecruitment(string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_DeleteRecruitment";

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public LevelApprovalData GetNextLevelApproval(int kdDivisi, string currLevelApproval)
        {
            try
            {
                string _spName = "spr_RMS_GetNextLevelApproval";

                LevelApprovalData _lvApprovalData = new LevelApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@CurrLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = currLevelApproval;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _lvApprovalData = new LevelApprovalData();
                            _lvApprovalData.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _lvApprovalData.NmLevelApproval = _reader["NmLevelApproval"].ToString();
                            _lvApprovalData.KdDivisi = Convert.ToInt32(_reader["KdDivisi"]);
                            _lvApprovalData.StatusDokumen = _reader["StatusDokumen"].ToString();
                        }
                    }
                }

                return _lvApprovalData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddEditApprovalRequest(RecruitmentApprovalData recApprovalData)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateRecruitmentApproval";

                SqlParameter[] _sqlParameter = new SqlParameter[5];
                _sqlParameter[0] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = recApprovalData.NoRequest;

                _sqlParameter[1] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = recApprovalData.User.KdUser;

                _sqlParameter[2] = new SqlParameter("@KdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = recApprovalData.LvApproval.KdLevelApproval;

                _sqlParameter[3] = new SqlParameter("@Komentar", SqlDbType.VarChar, 300);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = recApprovalData.Komentar;

                _sqlParameter[4] = new SqlParameter("@IsFinalize", SqlDbType.Int);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = recApprovalData.IsFinalize;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RecruitmentApprovalData GetRecruitmentApprovalComment(string noRequest, int kdUser, string kdLevelApproval)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentApprovalComment";

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = noRequest;

                _sqlParameter[1] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdUser;

                _sqlParameter[2] = new SqlParameter("@KdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = kdLevelApproval;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        if (_reader.Read())
                        {
                            _recAppData = new RecruitmentApprovalData();
                            _recAppData.NoRequest = _reader["NoRequest"].ToString();
                            _recAppData.LvApproval = new LevelApprovalData();
                            _recAppData.LvApproval.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _recAppData.User = new UserData();
                            _recAppData.User.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _recAppData.Komentar = _reader["Komentar"].ToString();
                            _recAppData.TglProses = Convert.ToDateTime(_reader["TglProses"]);
                            _recAppData.IsFinalize = Convert.ToInt32(_reader["IsFinalize"]);
                        }
                    }
                }

                return _recAppData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RecruitmentApprovalData> GetRequestApprovalHistory(string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentApprovalCommentList";

                List<RecruitmentApprovalData> _listRecAppData = new List<RecruitmentApprovalData>();
                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _recAppData = new RecruitmentApprovalData();
                            _recAppData.NoRequest = _reader["NoRequest"].ToString();
                            _recAppData.LvApproval = new LevelApprovalData();
                            _recAppData.LvApproval.KdLevelApproval = _reader["KdLevelApproval"].ToString();
                            _recAppData.LvApproval.NmLevelApproval = _reader["NmLevelApproval"].ToString();
                            _recAppData.User = new UserData();
                            _recAppData.User.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _recAppData.User.FullName = _reader["FullName"].ToString();
                            _recAppData.User.Email = _reader["Email"].ToString();
                            _recAppData.Komentar = _reader["Komentar"].ToString();
                            _recAppData.TglProses = Convert.ToDateTime(_reader["TglProses"]);
                            _recAppData.IsFinalize = Convert.ToInt32(_reader["IsFinalize"]);
                            _listRecAppData.Add(_recAppData);
                        }
                    }
                }

                return _listRecAppData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddRecruitmentKandidat(KandidatData dataKandidat)
        {
            using (SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        int kdKandidat = new KandidatDB().AddUpdateKandidat(dataKandidat, conn, trans);

                        dataKandidat.KdKandidat = kdKandidat;

                        int success = new RecruitmentDB().AddRecruitmentKandidatDetail(dataKandidat, conn, trans);

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
                        
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public int AddRecruitmentKandidatDetail(KandidatData dataKandidat, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateRecruitmentKandidat";

                SqlParameter[] _sqlParameter = new SqlParameter[8];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = dataKandidat.KdKandidat;

                _sqlParameter[1] = new SqlParameter("@KdDivisi", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = dataKandidat.Divisi.KdDivisi;

                _sqlParameter[2] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = dataKandidat.StrukturOrganisasi.KdSO;

                _sqlParameter[3] = new SqlParameter("@KdJabatan", SqlDbType.VarChar, 10);
                _sqlParameter[3].Direction = ParameterDirection.Input;
                _sqlParameter[3].Value = dataKandidat.Jabatan.KdJabatan;

                _sqlParameter[4] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[4].Direction = ParameterDirection.Input;
                _sqlParameter[4].Value = dataKandidat.NoRequest;

                _sqlParameter[5] = new SqlParameter("@UserIn", SqlDbType.Int);
                _sqlParameter[5].Direction = ParameterDirection.Input;
                _sqlParameter[5].Value = dataKandidat.UserIn.KdUser;

                _sqlParameter[6] = new SqlParameter("@IsPassed", SqlDbType.Int);
                _sqlParameter[6].Direction = ParameterDirection.Input;
                _sqlParameter[6].Value = dataKandidat.IsPassed;

                _sqlParameter[7] = new SqlParameter("@Rate", SqlDbType.Int);
                _sqlParameter[7].Direction = ParameterDirection.Input;
                _sqlParameter[7].Value = dataKandidat.Rate;

                return SqlHelper.ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<KandidatData> GetRecruitmentKandidatList(string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetRecruitmentKandidat";

                List<KandidatData> _listKandidat = new List<KandidatData>();
                KandidatData _kandidatData = new KandidatData();

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

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
                            _kandidatData.IsPassed = Convert.ToInt32(_reader["IsPassed"]);
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

        public int UpdateKandidatTerpilih(int kdKandidat, string noRequest, bool isSelected)
        {
            try
            {
                string _spName = "spr_RMS_UpdateKandidatPassed";

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdKandidat;

                _sqlParameter[1] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = noRequest;

                _sqlParameter[2] = new SqlParameter("@IsPassed", SqlDbType.Int);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = isSelected;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteRecruitmentKandidat(int kdKandidat, string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_DeleteRecruitmentKandidat";

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdKandidat;

                _sqlParameter[1] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = noRequest;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int AddRecruitmentKandidatFromExistingList(List<KandidatData> listSelectedKandidat)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(SystemConfiguration.RMSConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction()) 
                    {
                        try 
                        { 
                            foreach(KandidatData kandidatData in listSelectedKandidat)
                            {
                                int result = AddRecruitmentKandidatDetail(kandidatData, conn, trans);
                                if (result == 0) 
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                            }

                            trans.Commit();
                            return 1;
                        }
                        catch(Exception ex)
                        {
                            trans.Rollback();
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteKandidatFromRequest(int kdKandidat, string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_DeleteKandidatFromRequest";

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdKandidat", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdKandidat;

                _sqlParameter[1] = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = noRequest;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserData> GetNextPICList(int kdDivisi, string kdSO, string kdLvApproval)
        {
            try
            {
                string _spName = "spr_RMS_GetNextApprovalPIC";

                List<UserData> _listPIC = new List<UserData>();
                UserData _picData = new UserData();

                SqlParameter[] _sqlParameter = new SqlParameter[3];
                _sqlParameter[0] = new SqlParameter("@KdDivisi", SqlDbType.Int );
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdDivisi;

                _sqlParameter[1] = new SqlParameter("@KdSO", SqlDbType.VarChar, 10);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdSO;

                _sqlParameter[2] = new SqlParameter("@KdLevelApproval", SqlDbType.VarChar, 10);
                _sqlParameter[2].Direction = ParameterDirection.Input;
                _sqlParameter[2].Value = kdLvApproval;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _picData = new UserData();
                            _picData.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _picData.FullName = _reader["FullName"].ToString();
                            _picData.Email = _reader["Email"].ToString();
                            _listPIC.Add(_picData);
                        }
                    }
                }

                return _listPIC;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserData> GetAllPICList(string noRequest)
        {
            try
            {
                string _spName = "spr_RMS_GetAllRecruitmentPIC";

                List<UserData> _listPIC = new List<UserData>();
                UserData _picData = new UserData();

                SqlParameter _sqlParameter = new SqlParameter("@NoRequest", SqlDbType.VarChar, 15);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = noRequest;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _picData = new UserData();
                            _picData.KdUser = Convert.ToInt32(_reader["KdUser"]);
                            _picData.FullName = _reader["FullName"].ToString();
                            _picData.Email = _reader["Email"].ToString();
                            _listPIC.Add(_picData);
                        }
                    }
                }

                return _listPIC;
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }
    }
}
