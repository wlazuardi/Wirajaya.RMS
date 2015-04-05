using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class PositionData
    {
        public PositionData() 
        {
            _divisi = new DivisiData();
            _divisi.KdDivisi = 0;
            _strukturOrganisasi = new StrukturOrganisasiData();
            _strukturOrganisasi.KdSO = "0";
            _jabatan = new JabatanData();
            _jabatan.KdJabatan = "0";
            _userIn = new UserData();
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

        private int _rate;
        public int Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        private int _isPassed;
        public int IsPassed
        {
            get { return _isPassed; }
            set { _isPassed = value; }
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
    }
}
