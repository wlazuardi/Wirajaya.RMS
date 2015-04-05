using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class RecruitmentApprovalData
    {
        private string _noRequest;
        public string NoRequest
        {
            get { return _noRequest; }
            set { _noRequest = value; }
        }

        private UserData _user;
        public UserData User
        {
            get { return _user; }
            set { _user = value; }
        }

        private LevelApprovalData _lvApproval;
        public LevelApprovalData LvApproval
        {
            get { return _lvApproval; }
            set { _lvApproval = value; }
        }

        private string _komentar;
        public string Komentar
        {
            get { return _komentar; }
            set { _komentar = value; }
        }

        private DateTime _tglProses;
        public DateTime TglProses
        {
            get { return _tglProses; }
            set { _tglProses = value; }
        }

        private int _isFinalize;
        public int IsFinalize
        {
            get { return _isFinalize; }
            set { _isFinalize = value; }
        }
    }
}
