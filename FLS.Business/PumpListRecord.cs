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
    public class PumpListRecord
    {
        //UInt64 _TankId;
        //string _TankName;
        //string _TankDesc;
        //Double _Max;
        //UInt64 _StationId;
        //string _StationName;
        //UInt64 _FuelId;
        //string _FuelDesc;
        public long PumpId { get; set; }
        public long StationId { get; set; }
        public long TankId { get; set; }
        public string PumpName { get; set; }
        public string PumpDesc { get; set; }
        public string PumpIp { get; set; }
        public int PumpPort { get; set; }
        public int PumpDelayTime { get; set; }       
        public int PumpStatus { get; set; }

        public long PumpPickUp { get; set; }

        public PumpListRecord()
        {
            PumpId = -1;
            TankId = 0;
        }        
    }
}
