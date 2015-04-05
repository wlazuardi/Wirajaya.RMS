using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class JabatanData
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

        public string KdNmJabatan 
        {
            get { return _kdJabatan + ". " + _nmJabatan; }
        }

        private string _kdJabatan;
        public string KdJabatan
        {
            get { return _kdJabatan; }
            set { _kdJabatan = value; }
        }

        private string _nmJabatan;
        public string NmJabatan
        {
            get { return _nmJabatan; }
            set { _nmJabatan = value; }
        }

        private double _minSalary;
        public double MinSalary
        {
            get { return _minSalary; }
            set { _minSalary = value; }
        }

        private double _maxSalary;
        public double MaxSalary
        {
            get { return _maxSalary; }
            set { _maxSalary = value; }
        }

        private string _fasilitas;
        public string Fasilitas
        {
            get { return _fasilitas; }
            set { _fasilitas = value; }
        }

        private string _parentKdJabatan;
        public string ParentKdJabatan
        {
            get { return _parentKdJabatan; }
            set { _parentKdJabatan = value; }
        }

        private string _parentNmJabatan;
        public string ParentNmJabatan
        {
            get { return _parentNmJabatan; }
            set { _parentNmJabatan = value; }
        }

        private List<JabatanData> _childNode;
        public List<JabatanData> ChildNode
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
