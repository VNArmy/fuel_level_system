using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.Business
{
    public class CalibrationRecord
    {

        public UInt64 PK { get; set; }
        public long TankId { get; set; }
        public Int32 Raw { get; set; }
        public Int32 Level { get; set; }
        
    }
}
