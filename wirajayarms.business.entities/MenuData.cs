using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WirajayaRMS.Business.Entities
{
    [Serializable]
    public class MenuData
    {
        private int _kdMenu;
        public int KdMenu
        {
            get { return _kdMenu; }
            set { _kdMenu = value; }
        }

        private string _nmMenu;
        public string NmMenu
        {
            get { return _nmMenu; }
            set { _nmMenu = value; }
        }

        private string _link;
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        private string _menuIcon;
        public string MenuIcon
        {
            get { return _menuIcon; }
            set { _menuIcon = value; }
        }

        private int _parentKdMenu;
        public int ParentKdMenu
        {
            get { return _parentKdMenu; }
            set { _parentKdMenu = value; }
        }

        private List<MenuData> _childNode;
        public List<MenuData> ChildNode
        {
            get { return _childNode; }
            set { _childNode = value; }
        }
    }
}
