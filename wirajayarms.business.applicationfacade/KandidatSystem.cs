using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class KandidatSystem
    {
        public KandidatData GetKandidatData(int kdKandidat)
        {
            try
            {
                KandidatData _kandidatData = new KandidatDB().GetKandidatData(kdKandidat);
                _kandidatData.CVFiles = new FileDB().GetKandidatCVList(kdKandidat);
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
                KandidatData _kandidatData = new KandidatDB().GetKandidatData(kdKandidat, noRequest);
                _kandidatData.CVFiles = new FileDB().GetKandidatCVList(kdKandidat);
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
                return new KandidatDB().GetInterviewResultList(kdKandidat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddInterviewData(InterviewData interviewData)
        {
            try
            {
                return new KandidatDB().InsertUpdateInterviewData(interviewData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int UpdateInterviewData(InterviewData interviewData)
        {
            try
            {
                return new KandidatDB().InsertUpdateInterviewData(interviewData);
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
                return new KandidatDB().DeleteInterviewData(kdInterview);
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
                return new KandidatDB().GetQualificationMatchingList(kdKandidat, noRequest);
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
                return new KandidatDB().GetAdditionalQualificationList(kdKandidat);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int AddQualificationMatchingList(List<QualificationMatchingData> qualificationMatchingList)
        {
            try
            {
                return new KandidatDB().AddQualificationMatchingList(qualificationMatchingList);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public int AddAdditionalQualificationList(List<QualificationMatchingData> listAdditionalQualification)
        {
            try
            {
                return new KandidatDB().AddAdditionalQualificationList(listAdditionalQualification);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int AddKandidat(KandidatData dataKandidat, List<PositionData> listPosisi)
        {
            try
            {
                return new KandidatDB().AddKandidat(dataKandidat, listPosisi);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int EditKandidat(KandidatData dataKandidat)
        {
            try
            {
                return new KandidatDB().EditKandidat(dataKandidat);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int EditKandidat(KandidatData dataKandidat, List<PositionData> listPosisi)
        {
            try
            {
                return new KandidatDB().EditKandidat(dataKandidat, listPosisi);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<KandidatData> GetKandidatList(int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                List<KandidatData> _listKandidat = new KandidatDB().GetKandidatList(kdDivisi, kdSO, kdJabatan);
                foreach (KandidatData _kandidatData in _listKandidat)
                {
                    _kandidatData.CVFiles = new FileDB().GetKandidatCVList(_kandidatData.KdKandidat);
                }
                return _listKandidat;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<KandidatData> GetKandidatNotInRequest(int kdDivisi, string kdSO, string kdJabatan, string noRequest)
        {
            try
            {
                return new KandidatDB().GetKandidatNotInRequest(kdDivisi, kdSO, kdJabatan, noRequest);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
