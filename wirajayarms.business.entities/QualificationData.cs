using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class QualificationData
    {
        public QualificationData() { }

        public QualificationData(string _qualification) 
        {
            this.Qualification = _qualification;
        }

        private int _kdQualification;
        public int KdQualification
        {
            get { return _kdQualification; }
            set { _kdQualification = value; }
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

        private string _Qualification;
        public string Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; }
        }

        private string _noRequest;
        public string NoRequest
        {
            get { return _noRequest; }
            set { _noRequest = value; }
        }
    }
}
