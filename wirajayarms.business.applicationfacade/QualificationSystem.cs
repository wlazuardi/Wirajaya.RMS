using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class QualificationSystem
    {
        public List<QualificationData> GetQualificationList(int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                return new QualificationDB().GetQualificationList(kdDivisi, kdSO, kdJabatan);
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
                return new QualificationDB().AddQualification(QualificationData);
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
                return new QualificationDB().UpdateQualification(QualificationData);
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
                return new QualificationDB().DeleteQualification(kdQualification);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
