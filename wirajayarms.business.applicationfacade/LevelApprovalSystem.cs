using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class LevelApprovalSystem
    {
        public List<LevelApprovalData> GetLevelApprovalList(int kdDivisi)
        {
            try
            {
                List<LevelApprovalData> _listLevelApproval = new LevelApprovalDB().GetLevelApprovalList(kdDivisi, null);
                List<LevelApprovalData> _finalList = new List<LevelApprovalData>();

                List<LevelApprovalData> _parentList = _listLevelApproval.Where(item => item.ParentKdLevelApproval == "0").ToList();
                foreach(LevelApprovalData _item in _parentList)
                {
                    _item.ChildNode = GetChildLevelApproval(_item, _listLevelApproval);
                    _finalList.Add(_item);

                }

                return _finalList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<LevelApprovalData> GetListLevelApproval(int kdDivisi, string parentKdLevelApproval)
        {
            try
            {
                return new LevelApprovalDB().GetLevelApprovalList(kdDivisi, parentKdLevelApproval);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private List<LevelApprovalData> GetChildLevelApproval(LevelApprovalData node, List<LevelApprovalData> list)
        {
            List<LevelApprovalData> _list = list.Where(item => item.ParentKdLevelApproval == node.KdLevelApproval).ToList();
            foreach (LevelApprovalData _item in _list)
            {
                _item.ChildNode = GetChildLevelApproval(_item, list);
            }

            return _list;
        }

        public int InsertLevelApproval(LevelApprovalData lvApprovalData)
        {
            try
            {
                List<LevelApprovalData> _childNodes = new LevelApprovalSystem().GetListLevelApproval(lvApprovalData.KdDivisi, lvApprovalData.ParentKdLevelApproval);

                // Generate Kode Unit
                if (_childNodes.Count == 0)
                {
                    if (lvApprovalData.ParentKdLevelApproval == "0")
                    {
                        lvApprovalData.KdLevelApproval = "1";
                    }
                    else
                    {
                        lvApprovalData.KdLevelApproval = lvApprovalData.ParentKdLevelApproval + ".1";
                    }
                }
                else
                {
                    string _maxKdSO = _childNodes.Max(item => item.KdLevelApproval);

                    int _lastDotPosition = _maxKdSO.LastIndexOf('.');
                    if (_lastDotPosition > 0)
                    {
                        int _newNode = Convert.ToInt32(_maxKdSO.Substring(_lastDotPosition + 1)) + 1;
                        string _prefix = _maxKdSO.Substring(0, _lastDotPosition);
                        lvApprovalData.KdLevelApproval = _prefix + "." + _newNode.ToString();
                    }
                    else
                    {
                        lvApprovalData.KdLevelApproval = (Convert.ToInt32(_maxKdSO) + 1).ToString();
                    }
                }

                return new LevelApprovalDB().InsertLevelApproval(lvApprovalData);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int UpdateLevelApproval(LevelApprovalData lvApprovalData)
        {
            try
            {
                return new LevelApprovalDB().UpdateLevelApproval(lvApprovalData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LevelApprovalData GetLevelApprovalData(int kdDivisi, string kdLevelApproval)
        {
            try
            {
                return new LevelApprovalDB().GetLevelApprovalData(kdDivisi, kdLevelApproval);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteLevelApproval(int kdDivisi, string kdLevelApproval)
        {
            try
            {
                return new LevelApprovalDB().DeleteLevelApproval(kdDivisi, kdLevelApproval);
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }
    }
}
