using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class UnitData
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

        private string _nmUnit;
        public string NmUnit
        {
            get { return _nmUnit; }
            set { _nmUnit = value; }
        }

        private string _maxKdJabatan;
        public string MaxKdJabatan
        {
            get { return _maxKdJabatan; }
            set { _maxKdJabatan = value; }
        }

        private string _parentKdUnit;
        public string ParentKdUnit
        {
            get { return _parentKdUnit; }
            set { _parentKdUnit = value; }
        }

        private string _parentNmUnit;
        public string ParentNmUnit
        {
            get { return _parentNmUnit; }
            set { _parentNmUnit = value; }
        }

        private List<UnitData> _childNode;
        public List<UnitData> ChildNode
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
