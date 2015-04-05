using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class JabatanSystem
    {
        public List<JabatanData> GetAllListJabatan(int kdDivisi)
        {
            try
            {
                List<JabatanData> _listJabatan = new JabatanDB().GetListJabatan(kdDivisi, null);
                List<JabatanData> _finalList = new List<JabatanData>();

                List<JabatanData> _parentList = _listJabatan.Where(item => item.ParentKdJabatan == "0").ToList();
                foreach (JabatanData _item in _parentList)
                {
                    _item.ChildNode = GetChildJabatan(_item, _listJabatan);
                    _finalList.Add(_item);
                }

                return _finalList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<JabatanData> GetChildJabatan(JabatanData node, List<JabatanData> list)
        {
            List<JabatanData> _list = list.Where(item => item.ParentKdJabatan == node.KdJabatan).ToList();
            foreach (JabatanData _item in _list) 
            {
                _item.ChildNode = GetChildJabatan(_item, list);
            }
            return _list;
        }

        public List<JabatanData> GetListJabatan(int kdDivisi, string parentKdJabatan)
        {
            try
            {
                return new JabatanDB().GetListJabatan(kdDivisi, parentKdJabatan);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertJabatan(JabatanData jabatanData)
        {
            try
            {
                List<JabatanData> _childNodes = new JabatanSystem().GetListJabatan(jabatanData.KdDivisi, jabatanData.ParentKdJabatan);                                

                // Generate Kode Jabatan
                if (_childNodes.Count == 0)
                {
                    if (jabatanData.ParentKdJabatan == "0")
                    {
                        jabatanData.KdJabatan = "1";
                    }
                    else
                    {
                        jabatanData.KdJabatan = jabatanData.ParentKdJabatan + ".1";
                    }
                }
                else 
                {
                    string _maxKdJabatan = _childNodes.Max(item => item.KdJabatan);

                    int _lastDotPosition = _maxKdJabatan.LastIndexOf('.');
                    if (_lastDotPosition > 0)
                    {
                        int _newNode = Convert.ToInt32(_maxKdJabatan.Substring(_lastDotPosition + 1)) + 1;
                        string _prefix = _maxKdJabatan.Substring(0, _lastDotPosition);
                        jabatanData.KdJabatan = _prefix + "." + _newNode.ToString();
                    }
                    else 
                    {
                        jabatanData.KdJabatan = (Convert.ToInt32(_maxKdJabatan) + 1).ToString();
                    }
                }

                return new JabatanDB().InsertJabatan(jabatanData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public JabatanData GetJabatanData(int kdDivisi, string kdJabatan)
        {
            try
            {
                return new JabatanDB().GetJabatanData(kdDivisi, kdJabatan);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int UpdateJabatan(JabatanData jabatanData)
        {
            try
            {
                return new JabatanDB().UpdateJabatan(jabatanData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteJabatan(int kdDivisi, string kdJabatan)
        {
            try
            {
                return new JabatanDB().DeleteJabatan(kdDivisi, kdJabatan);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<JabatanData> GetUnitMaxJabatanList(int kdDivisi, string kdUnit)
        {
            try
            {
                return new JabatanDB().GetUnitMaxJabatanList(kdDivisi, kdUnit);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
