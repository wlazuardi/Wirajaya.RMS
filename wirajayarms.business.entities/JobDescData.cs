using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class JobDescData
    {
        public JobDescData() { }

        public JobDescData(string _jobDesc) 
        {
            this.JobDesc = _jobDesc;
        }

        private int _kdJobDesc;
        public int KdJobDesc
        {
            get { return _kdJobDesc; }
            set { _kdJobDesc = value; }
        }

        private int _kdDivisi;
        public int KdDivisi
        {
            get { return _kdDivisi; }
            set { _kdDivisi = value; }
        }

        private string _kdSO;
        public string KdSO
        {
            get { return _kdSO; }
            set { _kdSO = value; }
        }

        private string _kdJabatan;
        public string KdJabatan
        {
            get { return _kdJabatan; }
            set { _kdJabatan = value; }
        }

        private string _jobDesc;
        public string JobDesc
        {
            get { return _jobDesc; }
            set { _jobDesc = value; }
        }

        private string _noRequest;
        public string NoRequest
        {
            get { return _noRequest; }
            set { _noRequest = value; }
        }
    }
}
