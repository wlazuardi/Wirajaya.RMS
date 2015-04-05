using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class UserAccessData
    {
        private DivisiData _divisi;
        public DivisiData Divisi
        {
            get { return _divisi; }
            set { _divisi = value; }
        }

        private UserData _user;
        public UserData User
        {
            get { return _user; }
            set { _user = value; }
        }

        private LevelApprovalData _levelApproval;
        public LevelApprovalData LevelApproval
        {
            get { return _levelApproval; }
            set { _levelApproval = value; }
        }

        private StrukturOrganisasiData _StrukturOrganisasi;
        public StrukturOrganisasiData StrukturOrganisasi
        {
            get { return _StrukturOrganisasi; }
            set { _StrukturOrganisasi = value; }
        }
    }
}
