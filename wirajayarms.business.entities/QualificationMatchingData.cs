using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class QualificationMatchingData
    {
        private QualificationData _qualification;
        public QualificationData Qualification
        {
            get { return _qualification; }
            set { _qualification = value; }
        }

        private int _kdKandidat;
        public int KdKandidat
        {
            get { return _kdKandidat; }
            set { _kdKandidat = value; }
        }

        private bool _isMatch;
        public bool IsMatch
        {
            get { return _isMatch; }
            set { _isMatch = value; }
        }

        private string _komentar;
        public string Komentar
        {
            get { return _komentar; }
            set { _komentar = value; }
        }
    }
}
