using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class DivisiData
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

        public string KdNmDivisi
        {
            get { return _kdDivisi + ". " + _nmDivisi; }
        }
    }
}
