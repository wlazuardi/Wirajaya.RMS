using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class JobDescSystem
    {
        public List<JobDescData> GetJobDescList(int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                return new JobDescDB().GetJobDescList(kdDivisi, kdSO, kdJabatan);
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
                return new JobDescDB().AddJobDesc(jobDescData);
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
                return new JobDescDB().UpdateJobDesc(jobDescData);
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
                return new JobDescDB().DeleteJobDesc(kdJobDesc);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
