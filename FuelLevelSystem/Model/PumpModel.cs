using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.Model
{
    public class PumpModel
    {
        public PumpModel(UInt32 total)
        {
            Total = total;
        }
        public long PumpId{get;set;}
        public string Name { get; set; }
        public string Desc { get; set; }
        public string KindofFuel { get; set; }
        public string StationName { get; set; }

        public UInt32 Total { get; set; }
                       
    }
}
