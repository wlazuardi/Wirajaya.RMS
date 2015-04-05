using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class MenuSystem
    {
        public List<MenuData> GetMenuList(int kdUser)
        {
            try
            {
                List<MenuData> _menuList = new MenuDB().GetMenuList(kdUser);
                List<MenuData> _parentList = _menuList.Where(m => m.ParentKdMenu == 0).ToList();
                List<MenuData> _finalList = new List<MenuData>();

                foreach(MenuData _menuItem  in _parentList)
                {
                    _menuItem.ChildNode = GetChildMenu(_menuItem, _menuList);
                    _finalList.Add(_menuItem);
                }

                return _finalList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MenuData> GetMenuList()
        {
            try
            {
                List<MenuData> _menuList = new MenuDB().GetMenuList(0);
                List<MenuData> _parentList = _menuList.Where(m => m.ParentKdMenu == 0).ToList();
                List<MenuData> _finalList = new List<MenuData>();

                foreach (MenuData _menuItem in _parentList)
                {
                    _menuItem.ChildNode = GetChildMenu(_menuItem, _menuList);
                    _finalList.Add(_menuItem);
                }

                return _finalList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<MenuData> GetChildMenu(MenuData node, List<MenuData> list)
        {
            List<MenuData> _list = list.Where(item => item.ParentKdMenu == node.KdMenu).ToList();
            foreach (MenuData _item in _list)
            {
                _item.ChildNode = GetChildMenu(_item, list);
            }
            return _list;
        }

        public int AddMenuAccess(int kdUser, int kdMenu)
        {
            try
            {
                return new MenuDB().AddMenuAccess(kdUser, kdMenu);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteMenuAccess(int kdUser)
        {
            try
            {
                return new MenuDB().DeleteMenuAccess(kdUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckMenuAccess(int kdUser, int kdMenu)
        {
            try
            {
                return new MenuDB().CheckMenuAccess(kdUser, kdMenu);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
