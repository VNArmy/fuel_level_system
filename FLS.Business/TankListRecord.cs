using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.Business
{
    public class TankListRecord
    {
        //UInt64 _TankId;
        //string _TankName;
        //string _TankDesc;
        //Double _Max;
        //UInt64 _StationId;
        //string _StationName;
        //UInt64 _FuelId;
        //string _FuelDesc;
        public long TankId { get; set; }
        public string TankName { get; set; }
        public string TankDesc { get; set; }
        public Double Max { get; set; }
        public long StationId { get; set; }
        //public string StationName { get; set; }
        public long FuelId { get; set; }
        public Double Offset { get; set; }

        public TankListRecord()
        {
            TankId = -1;
            StationId = 0;
            Offset = 0;
        }        
    }
}
