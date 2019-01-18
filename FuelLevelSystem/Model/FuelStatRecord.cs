using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.Model
{
    class FuelStatRecord
    {
        public String FuelName { get; set; }
        public UInt64 Capacity { get; set; }
        public UInt64 FuelPumpTotal { get; set; }
    }
}
