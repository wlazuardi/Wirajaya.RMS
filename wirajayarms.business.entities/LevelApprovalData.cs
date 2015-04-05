using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class LevelApprovalData
    {
        private string _kdLevelApproval;
        public string KdLevelApproval
        {
            get { return _kdLevelApproval; }
            set { _kdLevelApproval = value; }
        }

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

        public string KdNmLevelApproval
        {
            get { return _kdLevelApproval + ". " + _nmLevelApproval; }
        }

        private string _nmLevelApproval;
        public string NmLevelApproval
        {
            get { return _nmLevelApproval; }
            set { _nmLevelApproval = value; }
        }

        private string _statusDokumen;
        public string StatusDokumen
        {
            get { return _statusDokumen; }
            set { _statusDokumen = value; }
        }

        private string _parentKdLevelApproval;
        public string ParentKdLevelApproval
        {
            get { return _parentKdLevelApproval; }
            set { _parentKdLevelApproval = value; }
        }

        private string _parentNmLevelApproval;
        public string ParentNmLevelApproval
        {
            get { return _parentNmLevelApproval; }
            set { _parentNmLevelApproval = value; }
        }

        private List<LevelApprovalData> _childNode;
        public List<LevelApprovalData> ChildNode
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
