using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class RecruitmentData
    {
        public RecruitmentData()
        {
            _strukturOrganisasi = new StrukturOrganisasiData();
            _jabatan = new JabatanData();
            _creator = new UserData();
            _userUp = new UserData();
            _jobDescList = new List<JobDescData>();
            _qualificationList = new List<QualificationData>();
            _approvalDataList = new List<RecruitmentApprovalData>();
        }

        private int _kdDivisi;
        public int KdDivisi
        {
            get { return _kdDivisi; }
            set { _kdDivisi = value; }
        }

        private string _noRequest;
        public string NoRequest
        {
            get { return _noRequest; }
            set { _noRequest = value; }
        }

        private DateTime _tglButuh;
        public DateTime TglButuh
        {
            get { return _tglButuh; }
            set { _tglButuh = value; }
        }

        private DateTime _tglRequest;
        public DateTime TglRequest
        {
            get { return _tglRequest; }
            set { _tglRequest = value; }
        }

        private StrukturOrganisasiData _strukturOrganisasi;
        public StrukturOrganisasiData StrukturOrganisasi
        {
            get { return _strukturOrganisasi; }
            set { _strukturOrganisasi = value; }
        }

        private JabatanData _jabatan;
        public JabatanData Jabatan
        {
            get { return _jabatan; }
            set { _jabatan = value; }
        }

        private int _jmlOrang;
        public int JmlOrang
        {
            get { return _jmlOrang; }
            set { _jmlOrang = value; }
        }

        private int _kdAlasan;
        public int KdAlasan
        {
            get { return _kdAlasan; }
            set { _kdAlasan = value; }
        }

        private string _currLevelApproval;
        public string CurrLevelApproval
        {
            get { return _currLevelApproval; }
            set { _currLevelApproval = value; }
        }

        private UserData _creator;
        public UserData Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        private UserData _userUp;
        public UserData UserUp
        {
            get { return _userUp; }
            set { _userUp = value; }
        }

        private string _statusDokumen;
        public string StatusDokumen
        {
            get { return _statusDokumen; }
            set { _statusDokumen = value; }
        }

        private List<JobDescData> _jobDescList;
        public List<JobDescData> JobDescList
        {
            get { return _jobDescList; }
            set { _jobDescList = value; }
        }

        private List<QualificationData> _qualificationList;
        public List<QualificationData> QualificationList
        {
            get { return _qualificationList; }
            set { _qualificationList = value; }
        }

        private List<RecruitmentApprovalData> _approvalDataList;
        public List<RecruitmentApprovalData> ApprovalDataList
        {
            get { return _approvalDataList; }
            set { _approvalDataList = value; }
        }
    }
}
