using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class DivisiSystem
    {
        public List<DivisiData> GetDivisiList()
        {
            try
            {
                return new DivisiDB().GetDivisiList();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public List<DivisiData> GetDivisiList(int kdUser)
        {
            try
            {
                return new DivisiDB().GetDivisiList(kdUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DivisiData GetDivisiData(int kdDivisi)
        {
            try
            {
                return new DivisiDB().GetDivisiData(kdDivisi);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
