using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.Model
{
    class ImportReport
    {
        public String StationName { get; set; }
        public DateTime ReportDate { get; set; }
        public String DriverName { get; set; }
        public String CarPlate { get; set; }
        public String StaffName { get; set; }
        public String StaffPos { get; set; }
        public String Fuel { get; set; }
        public String TemplateBottle { get; set; }
        public UInt64 BeginTotalPump { get; set; }
        public Double BeginLevel { get; set; }
        public Double BeginCapacity { get; set; }
        public UInt64 EndTotalPump { get; set; }
        public Double EndLevel { get; set; }
        public Double EndCapacity { get; set; }
        public Double ImportCapacity { get; set; }
        public Double RealCapacity { get; set; }
        public String ExStore { get; set; }
        public Boolean SealStatus { get; set; }
        public String SealId { get; set; }
    }
}
