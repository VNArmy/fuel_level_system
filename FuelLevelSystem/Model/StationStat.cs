using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.Model
{
    class StationStat
    {
        public StationStat()
        {
            NotedA92 = 0;
            NotedA95 = 0;
            NotedE5 = 0;
            NotedDO005S = 0;
            NotedDO025S = 0;
            NotedKO = 0;

            MeasA92 =0;
            MeasA95 = 0;
            MeasE5 = 0;
            MeasDO005S = 0;
            MeasDO025S = 0;
            MeasKO = 0;

            BeginPumpA92 = 0;
            BeginPumpA95 = 0;
            BeginPumpE5 = 0;
            BeginPumpDO005S = 0;
            BeginPumpDO025S = 0;
            BeginPumpKO = 0;

            EndPumpA92 = 0;
            EndPumpA95 = 0;
            EndPumpE5 = 0;
            EndPumpDO005S = 0;
            EndPumpDO025S = 0;
            EndPumpKO = 0;
        }

        public DateTime StatDate { get; set; }
        public DateTime StatEndDate { get; set; }
        public String StationName { get; set; }
        public String StaffName { get; set; }
        public String StaffPos { get; set; }
        public String StaffName2 { get; set; }
        public String StaffPos2 { get; set; }
        public String StaffName3 { get; set; }
        public String StaffPos3 { get; set; }
        public String StaffName4 { get; set; }
        public String StaffPos4 { get; set; }
        public String StaffName5 { get; set; }
        public String StaffPos5 { get; set; }

        public UInt64 NotedA92 { get; set; }
        public UInt64 NotedA95 { get; set; }
        public UInt64 NotedE5 { get; set; }
        public UInt64 NotedDO005S { get; set; }
        public UInt64 NotedDO025S { get; set; }
        public UInt64 NotedKO { get; set; }
        
        public UInt64 MeasA92 { get; set; }
        public UInt64 MeasA95 { get; set; }
        public UInt64 MeasE5 { get; set; }
        public UInt64 MeasDO005S { get; set; }
        public UInt64 MeasDO025S { get; set; }
        public UInt64 MeasKO { get; set; }

        public UInt64 BeginPumpA92 { get; set; }
        public UInt64 BeginPumpA95 { get; set; }
        public UInt64 BeginPumpE5 { get; set; }
        public UInt64 BeginPumpDO005S { get; set; }
        public UInt64 BeginPumpDO025S { get; set; }
        public UInt64 BeginPumpKO { get; set; }

        public UInt64 EndPumpA92 { get; set; }
        public UInt64 EndPumpA95 { get; set; }
        public UInt64 EndPumpE5 { get; set; }
        public UInt64 EndPumpDO005S { get; set; }
        public UInt64 EndPumpDO025S { get; set; }
        public UInt64 EndPumpKO { get; set; } 
        //public FuelStatRecord[] FuelStat { get; set; }
    }
}
