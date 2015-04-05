using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class StrukturOrganisasiSystem
    {
        public List<StrukturOrganisasiData> GetAllListStrukturOrganisasi(int kdDivisi)
        {
            try
            {
                List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiDB().GetListStrukturOrganisasi(kdDivisi, null);
                List<StrukturOrganisasiData> _finalList = new List<StrukturOrganisasiData>();

                List<StrukturOrganisasiData> _parentList = _listSO.Where(item => item.ParentKdSO == "0").ToList();
                foreach (StrukturOrganisasiData _item in _parentList)
                {
                    _item.ChildNode = GetChildStrukturOrganisasi(_item, _listSO);
                    _finalList.Add(_item);
                }

                return _finalList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<StrukturOrganisasiData> GetListStrukturOrganisasiByKdUser(int kdDivisi, int kdUser)
        {
            try
            {
                List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiDB().GetListStrukturOrganisasi(kdDivisi, kdUser, null);
                List<StrukturOrganisasiData> _finalList = new List<StrukturOrganisasiData>();

                if (_listSO.Count > 0)
                {
                    // Cari yang parentnya 0
                    List<StrukturOrganisasiData> _parentList = _listSO.Where(item => item.ParentKdSO == "0").ToList();

                    // Kalau yang parentnya 0 tidak ada, cari ParentKdSO yang panjangnya paling pendek
                    // Misal : Ada KdSO 1, 1.1, 1.2, 1.3, 2, 2.1, 2.2, maka yang diambil hanya 1 dan 2
                    if (_parentList.Count == 0)
                    {
                        int minLength = _listSO.Min(item => item.ParentKdSO.Length);
                        _parentList = _listSO.Where(item => item.ParentKdSO.Length == minLength).ToList();
                    }

                    // Cari anaknya
                    foreach (StrukturOrganisasiData _item in _parentList)
                    {
                        _item.ChildNode = GetChildStrukturOrganisasiWithParent(_item, _listSO);
                        _finalList.Add(_item);
                    }
                }

                return _finalList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<StrukturOrganisasiData> GetChildStrukturOrganisasi(StrukturOrganisasiData node, List<StrukturOrganisasiData> list)
        {
            List<StrukturOrganisasiData> _list = list.Where(item => item.ParentKdSO == node.KdSO).ToList();
            foreach (StrukturOrganisasiData _item in _list)
            {
                _item.ChildNode = GetChildStrukturOrganisasi(_item, list);
            }
            return _list;
        }

        private List<StrukturOrganisasiData> GetChildStrukturOrganisasiWithParent(StrukturOrganisasiData node, List<StrukturOrganisasiData> list)
        {
            List<StrukturOrganisasiData> _list = list.Where(item => item.ParentKdSO == node.KdSO).ToList();
            foreach (StrukturOrganisasiData _item in _list)
            {
                //_item.ParentNmStrukturOrganisasi += node.NmStrukturOrganisasi + " - ";
                //_item.ParentNmStrukturOrganisasi = GetFullParentName(_item, list);                
                if (_item.KdSO.Count(i => i == '.') > 1)
                {
                    _item.ParentNmStrukturOrganisasi = GetDepartmentName(_item, list);
                }
                _item.ChildNode = GetChildStrukturOrganisasiWithParent(_item, list);
            }
            return _list;
        }

        private string GetDepartmentName(StrukturOrganisasiData node, List<StrukturOrganisasiData> list)
        {
            string parentName = "";
            int position = node.KdSO.IndexOf('.', node.KdSO.IndexOf('.') + 1);
            string kdDept = node.KdSO.Substring(0, position);
            if (list.Exists(item => item.KdSO == kdDept))
            {
                parentName = list.Where(item => item.KdSO == kdDept).ToList()[0].NmStrukturOrganisasi + " - ";
            }
            return parentName;
        }

        private string GetFullParentName(StrukturOrganisasiData node, List<StrukturOrganisasiData> list)
        {
            string parentName = "";            
            if (list.Exists(item => item.KdSO == node.ParentKdSO)) 
            {
                StrukturOrganisasiData parent = list.Where(item => item.KdSO == node.ParentKdSO).ToList()[0];
                parentName += parent.NmStrukturOrganisasi + " - ";
                parentName = GetFullParentName(parent, list) + parentName;
            }
            return parentName;
        }

        public List<StrukturOrganisasiData> GetListStrukturOrganisasi(int kdDivisi, string parentKdSO)
        {
            try
            {
                return new StrukturOrganisasiDB().GetListStrukturOrganisasi(kdDivisi, parentKdSO);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertStrukturOrganisasi(StrukturOrganisasiData soData)
        {
            try
            {
                List<StrukturOrganisasiData> _childNodes = new StrukturOrganisasiSystem().GetListStrukturOrganisasi(soData.KdDivisi, soData.ParentKdSO);

                // Generate Kode Unit
                if (_childNodes.Count == 0)
                {
                    if (soData.ParentKdSO == "0")
                    {
                        soData.KdSO = "1";
                    }
                    else
                    {
                        soData.KdSO = soData.ParentKdSO + ".1";
                    }
                }
                else
                {
                    string _maxKdSO = _childNodes.Max(item => item.KdSO);

                    int _lastDotPosition = _maxKdSO.LastIndexOf('.');
                    if (_lastDotPosition > 0)
                    {
                        int _newNode = Convert.ToInt32(_maxKdSO.Substring(_lastDotPosition + 1)) + 1;
                        string _prefix = _maxKdSO.Substring(0, _lastDotPosition);
                        soData.KdSO = _prefix + "." + _newNode.ToString();
                    }
                    else
                    {
                        soData.KdSO = (Convert.ToInt32(_maxKdSO) + 1).ToString();
                    }
                }

                return new StrukturOrganisasiDB().InsertStrukturOrganisasi(soData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public StrukturOrganisasiData GetStrukturOrganisasiData(int kdDivisi, string kdSO)
        {
            try
            {
                return new StrukturOrganisasiDB().GetStrukturOrganisasiData(kdDivisi, kdSO);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int UpdateStrukturOrganisasi(StrukturOrganisasiData soData)
        {
            try
            {
                return new StrukturOrganisasiDB().UpdateStrukturOrganisasi(soData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int DeleteStrukturOrganisasi(int kdDivisi, string kdSO)
        {
            try
            {
                return new StrukturOrganisasiDB().DeleteStrukturOrganisasi(kdDivisi, kdSO);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<StrukturOrganisasiData> GetTopLevelListStrukturOrganisasi(int kdDivisi)
        {
            try
            {
                List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiDB().GetListStrukturOrganisasi(kdDivisi, null);
                return _listSO.Where(item => item.ParentKdSO == "0").ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<StrukturOrganisasiData> GetTopManagementListStrukturOrganisasi(int kdDivisi)
        {
            try
            {
                List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiDB().GetListStrukturOrganisasi(kdDivisi, null);

                // Ambil sampai level manager saja (berarti parentnya jangan mengandung titik)
                _listSO = _listSO.Where(item => !item.ParentKdSO.Contains(".")).ToList();

                List<StrukturOrganisasiData> _finalList = new List<StrukturOrganisasiData>();

                List<StrukturOrganisasiData> _parentList = _listSO.Where(item => item.ParentKdSO == "0").ToList();
                foreach (StrukturOrganisasiData _item in _parentList)
                {
                    _item.ChildNode = GetChildStrukturOrganisasi(_item, _listSO);
                    _finalList.Add(_item);
                }

                return _finalList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
