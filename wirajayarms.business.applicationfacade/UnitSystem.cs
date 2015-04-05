using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class UnitSystem
    {
        public List<UnitData> GetAllListUnit(int kdDivisi)
        {
            try
            {
                List<UnitData> _listUnit = new UnitDB().GetListUnit(kdDivisi, null);
                List<UnitData> _finalList = new List<UnitData>();

                List<UnitData> _parentList = _listUnit.Where(item => item.ParentKdUnit == "0").ToList();
                foreach (UnitData _item in _parentList)
                {
                    _item.ChildNode = GetChildUnit(_item, _listUnit);
                    _finalList.Add(_item);
                }

                return _finalList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UnitData> GetChildUnit(UnitData node, List<UnitData> list)
        {
            List<UnitData> _list = list.Where(item => item.ParentKdUnit == node.KdUnit).ToList();
            foreach (UnitData _item in _list)
            {
                _item.ChildNode = GetChildUnit(_item, list);
            }
            return _list;
        }

        public List<UnitData> GetListUnit(int kdDivisi, string parentKdUnit) 
        {
            try
            {
                return new UnitDB().GetListUnit(kdDivisi, parentKdUnit);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int InsertUnit(UnitData unitData)
        {
            try
            {
                List<UnitData> _childNodes = new UnitSystem().GetListUnit(unitData.KdDivisi, unitData.ParentKdUnit);

                // Generate Kode Unit
                if (_childNodes.Count == 0)
                {
                    if (unitData.ParentKdUnit == "0")
                    {
                        unitData.KdUnit = "1";
                    }
                    else
                    {
                        unitData.KdUnit = unitData.ParentKdUnit + ".1";
                    }
                }
                else
                {
                    string _maxKdUnit = _childNodes.Max(item => item.KdUnit);

                    int _lastDotPosition = _maxKdUnit.LastIndexOf('.');
                    if (_lastDotPosition > 0)
                    {
                        int _newNode = Convert.ToInt32(_maxKdUnit.Substring(_lastDotPosition + 1)) + 1;
                        string _prefix = _maxKdUnit.Substring(0, _lastDotPosition);
                        unitData.KdUnit = _prefix + "." + _newNode.ToString();
                    }
                    else
                    {
                        unitData.KdUnit = (Convert.ToInt32(_maxKdUnit) + 1).ToString();
                    }
                }

                return new UnitDB().InsertUnit(unitData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UnitData GetUnitData(int kdDivisi, string kdUnit)
        {
            try
            {
                return new UnitDB().GetUnitData(kdDivisi, kdUnit);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int UpdateUnit(UnitData unitData)
        {
            try
            {
                return new UnitDB().UpdateUnit(unitData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int DeleteUnit(int kdDivisi, string kdUnit)
        {
            try
            {
                return new UnitDB().DeleteUnit(kdDivisi, kdUnit);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
