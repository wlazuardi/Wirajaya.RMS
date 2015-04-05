using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.DataAccess.Components;

namespace WirajayaRMS.Business.ApplicationFacade
{
    public class PositionSystem
    {
        public List<PositionData> GetPosisiKandidat(int kdKandidat)
        {
            try
            {
                return new PositionDB().GetPosisiKandidat(kdKandidat);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeletePosisi(int kdKandidat, int kdDivisi, string kdSO, string kdJabatan)
        {
            try
            {
                return new PositionDB().DeletePosisi(kdKandidat, kdDivisi, kdSO, kdJabatan);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
