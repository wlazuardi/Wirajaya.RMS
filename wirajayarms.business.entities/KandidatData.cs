using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    public class KandidatData
    {
        public KandidatData() 
        {
            _divisi = new DivisiData();
            _strukturOrganisasi = new StrukturOrganisasiData();
            _jabatan = new JabatanData();
            _userIn = new UserData();
            _rate = 0;
            _isPassed = 0;
        }

        private int _kdKandidat;
        public int KdKandidat
        {
            get { return _kdKandidat; }
            set { _kdKandidat = value; }
        }

        private string _nmKandidat;
        public string NmKandidat
        {
            get { return _nmKandidat; }
            set { _nmKandidat = value; }
        }

        private char _gender;
        public char Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _noIdentitas;
        public string NoIdentitas
        {
            get { return _noIdentitas; }
            set { _noIdentitas = value; }
        }

        private string _noHandphone;
        public string NoHandphone
        {
            get { return _noHandphone; }
            set { _noHandphone = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private List<FileData> _cVFiles;
        public List<FileData> CVFiles
        {
            get { return _cVFiles; }
            set { _cVFiles = value; }
        }

        private DateTime _tglProses;
        public DateTime TglProses
        {
            get { return _tglProses; }
            set { _tglProses = value; }
        }

        private UserData _userIn;
        public UserData UserIn
        {
            get { return _userIn; }
            set { _userIn = value; }
        }

        private StrukturOrganisasiData _strukturOrganisasi;
        public StrukturOrganisasiData StrukturOrganisasi
        {
            get { return _strukturOrganisasi; }
            set { _strukturOrganisasi = value; }
        }

        private DivisiData _divisi;
        public DivisiData Divisi
        {
            get { return _divisi; }
            set { _divisi = value; }
        }

        private JabatanData _jabatan;
        public JabatanData Jabatan
        {
            get { return _jabatan; }
            set { _jabatan = value; }
        }

        private string _noRequest;
        public string NoRequest
        {
            get { return _noRequest; }
            set { _noRequest = value; }
        }

        private int _isPassed;
        public int IsPassed
        {
            get { return _isPassed; }
            set { _isPassed = value; }
        }

        private int _rate;
        public int Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
    }
}
