using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using WirajayaRMS.CrossCutting.OptManagement;
using System.Data;

namespace WirajayaRMS.DataAccess.Components
{
    public class MenuDB
    {
        public List<MenuData> GetMenuList(int kdUser)
        {
            try
            {
                string _spName = "spr_RMS_GetMenuAccessList";
                List<MenuData> _listMenu = new List<MenuData>();
                MenuData _menuData = new MenuData();

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter))
                {
                    if (_reader.HasRows)
                    {
                        while (_reader.Read())
                        {
                            _menuData = new MenuData();
                            _menuData.KdMenu = Convert.ToInt32(_reader["KdMenu"]);
                            _menuData.NmMenu = _reader["NmMenu"].ToString();
                            _menuData.Link = _reader["Link"].ToString();
                            _menuData.ParentKdMenu = Convert.ToInt32(_reader["ParentKdMenu"]);
                            _menuData.MenuIcon = _reader["MenuIcon"].ToString();
                            _listMenu.Add(_menuData);
                        }
                    }
                }

                return _listMenu;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public int AddMenuAccess(int kdUser, int kdMenu)
        {
            try
            {
                string _spName = "spr_RMS_InsertUpdateMenuAccess";

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdUser;

                _sqlParameter[1] = new SqlParameter("@KdMenu", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdMenu;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
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
                string _spName = "spr_RMS_DeleteMenuAccessByKdUser";

                SqlParameter _sqlParameter = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter.Direction = ParameterDirection.Input;
                _sqlParameter.Value = kdUser;

                return SqlHelper.ExecuteNonQuery(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter);
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
                string _spName = "spr_RMS_CheckMenuAccess";

                SqlParameter[] _sqlParameter = new SqlParameter[2];
                _sqlParameter[0] = new SqlParameter("@KdUser", SqlDbType.Int);
                _sqlParameter[0].Direction = ParameterDirection.Input;
                _sqlParameter[0].Value = kdUser;

                _sqlParameter[1] = new SqlParameter("@KdMenu", SqlDbType.Int);
                _sqlParameter[1].Direction = ParameterDirection.Input;
                _sqlParameter[1].Value = kdMenu;

                using (SqlDataReader _reader = SqlHelper.ExecuteReader(SystemConfiguration.RMSConnectionString, CommandType.StoredProcedure, _spName, _sqlParameter)) 
                { 
                    if(_reader.HasRows)
                    {
                        if (_reader.Read())
                            return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
