using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    public class InterviewData
    {
        private int _kdInterview;
        public int KdInterview
        {
            get { return _kdInterview; }
            set { _kdInterview = value; }
        }

        private int _kdKandidat;
        public int KdKandidat
        {
            get { return _kdKandidat; }
            set { _kdKandidat = value; }
        }

        private string _interviewer;
        public string Interviewer
        {
            get { return _interviewer; }
            set { _interviewer = value; }
        }

        private DateTime _tglInterview;
        public DateTime TglInterview
        {
            get { return _tglInterview; }
            set { _tglInterview = value; }
        }

        private string _hasil;
        public string Hasil
        {
            get { return _hasil; }
            set { _hasil = value; }
        }
    }
}
