using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class FileData
    {
        private int _kdFile;
        public int KdFile
        {
            get { return _kdFile; }
            set { _kdFile = value; }
        }

        private int _kdKandidat;
        public int KdKandidat
        {
            get { return _kdKandidat; }
            set { _kdKandidat = value; }
        }

        private string _nmFile;
        public string NmFile
        {
            get { return _nmFile; }
            set { _nmFile = value; }
        }

        private string _nmFileAsli;
        public string NmFileAsli
        {
            get { return _nmFileAsli; }
            set { _nmFileAsli = value; }
        }

        private long _fileSize;
        public long FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }
    }
}
