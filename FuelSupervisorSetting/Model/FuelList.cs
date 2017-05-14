using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.Model
{
    class FuelList:GenericRecord
    {
        long _FuelId;
        String _FuelDesc;
        public long FuelId
        {
            get { return _FuelId; }
            set { _FuelId = value; RaisePropertyChanged("FuelId"); }
    
        }
        public String FuelDesc
        {
            get { return _FuelDesc; }
            set { _FuelDesc = value; RaisePropertyChanged("FuelDescription"); }

        }
    }
}
