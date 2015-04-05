using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;
using System.Net.Mail;
using System.Net;
using WirajayaRMS.CrossCutting.OptManagement;
using System.IO;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class RecruitmentSystem
    {
        public List<RecruitmentData> GetRecruitmentList(int kdDivisi)
        {
            try
            {
                return new RecruitmentDB().GetRecruitmentList(kdDivisi);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string AddNewRequest(RecruitmentData recruitmentData)
        {
            try
            {
                return new RecruitmentDB().AddNewRequest(recruitmentData);
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
                return new RecruitmentDB().GetRecruitmentData(noRequest);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<JobDescData> GetRecruitmentJobDescList(string noRequest)
        {
            try
            {
                return new RecruitmentDB().GetRecruitmentJobDescList(noRequest);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<QualificationData> GetRecruitmentQualification(string noRequest)
        {
            try
            {
                return new RecruitmentDB().GetRecruitmentQualificationList(noRequest);
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
                return new RecruitmentDB().DeleteRecruitment(noRequest);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateRequest(RecruitmentData recruitmentData)
        {
            try
            {
                return new RecruitmentDB().UpdateRequest(recruitmentData);
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
                return new RecruitmentDB().GetNextLevelApproval(kdDivisi, currLevelApproval);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public int ProcessRequest(RecruitmentData recruitmentData)
        //{
        //    try
        //    {
        //        int result = 0;

        //        if (recruitmentData.NoRequest != "" && recruitmentData.NoRequest != null)
        //        {
        //            result = UpdateRequest(recruitmentData);
        //        }
        //        else
        //        {
        //            result = AddNewRequest(recruitmentData);
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public RecruitmentApprovalData GetRecruitmentApprovalComment(string noRequest, int kdUser, string kdLevelApproval)
        {
            try
            {
                return new RecruitmentDB().GetRecruitmentApprovalComment(noRequest, kdUser, kdLevelApproval);
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
                return new RecruitmentDB().GetRequestApprovalHistory(noRequest);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddRecruitmentKandidat(KandidatData dataKandidat)
        {
            try
            {
                return new RecruitmentDB().AddRecruitmentKandidat(dataKandidat);
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
                List<KandidatData> _listKandidat = new RecruitmentDB().GetRecruitmentKandidatList(noRequest);
                foreach (KandidatData _kandidatData in _listKandidat)
                {
                    _kandidatData.CVFiles = new FileDB().GetKandidatCVList(_kandidatData.KdKandidat);
                }
                return _listKandidat;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RecruitmentData> GetRecruitmentListOnHold(int kdUser, int kdDivisi, int kdSO, int kdJabatan)
        {
            try
            {
                return new RecruitmentDB().GetRecruitmentListOnHold(kdUser, kdDivisi, kdSO, kdJabatan);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RecruitmentData> GetRecruitmentHistoryList(int kdUser, int kdDivisi, int kdSO, int kdJabatan)
        {
            try
            {
                return new RecruitmentDB().GetRecruitmentHistoryList(kdUser, kdDivisi, kdSO, kdJabatan);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteRecruitmentKandidat(int kdKandidat, string noRequest)
        {
            try
            {
                return new RecruitmentDB().DeleteRecruitmentKandidat(kdKandidat, noRequest);
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
                return new RecruitmentDB().AddRecruitmentKandidatFromExistingList(listSelectedKandidat);
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
                return new RecruitmentDB().DeleteKandidatFromRequest(kdKandidat, noRequest);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int UpdateKandidatTerpilih(List<KandidatData> listKandidatTerpilih)
        {
            try
            {
                int count = 0;
                foreach (KandidatData kandidatTerpilih in listKandidatTerpilih)
                {
                    count += new RecruitmentDB().UpdateKandidatTerpilih(kandidatTerpilih.KdKandidat, kandidatTerpilih.NoRequest, true);
                }
                return count;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
