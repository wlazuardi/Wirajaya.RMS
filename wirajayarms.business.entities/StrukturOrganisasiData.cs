using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class StrukturOrganisasiData
    {
        private int _kdDivisi;
        public int KdDivisi
        {
            get { return _kdDivisi; }
            set { _kdDivisi = value; }
        }

        private string _nmDivisi;
        public string NmDivisi
        {
            get { return _nmDivisi; }
            set { _nmDivisi = value; }
        }

        private string _kdUnit;
        public string KdUnit
        {
            get { return _kdUnit; }
            set { _kdUnit = value; }
        }

        private string _kdSO;
        public string KdSO
        {
            get { return _kdSO; }
            set { _kdSO = value; }
        }

        private string _parentKdSO;
        public string ParentKdSO
        {
            get { return _parentKdSO; }
            set { _parentKdSO = value; }
        }

        private string _parentNmStrukturOrganisasi;
        public string ParentNmStrukturOrganisasi
        {
            get { return _parentNmStrukturOrganisasi; }
            set { _parentNmStrukturOrganisasi = value; }
        }

        private string _nmStrukturOrganisasi;
        public string NmStrukturOrganisasi
        {
            get { return _nmStrukturOrganisasi; }
            set { _nmStrukturOrganisasi = value; }
        }

        private int _jmlKaryawan;
        public int JmlKaryawan
        {
            get { return _jmlKaryawan; }
            set { _jmlKaryawan = value; }
        }

        private int _maxJmlKaryawan;
        public int MaxJmlKaryawan
        {
            get { return _maxJmlKaryawan; }
            set { _maxJmlKaryawan = value; }
        }

        private List<StrukturOrganisasiData> _childNode;
        public List<StrukturOrganisasiData> ChildNode
        {
            get { return _childNode; }
            set { _childNode = value; }
        }

        private int _isActive;
        public int IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        
    }
}
