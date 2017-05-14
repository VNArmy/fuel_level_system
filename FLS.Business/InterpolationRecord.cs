using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.Business
{
     
    public class InterpolationRecord 
    {
  
        public UInt64 PK { get; set; }
        public long TankId { get; set; }
        public Double Level { get; set; }
        public Double Capacity { get; set; }

        //public static ObservableCollection<InterpolationRecord> LoadInterpolationTab(UInt64 pumpid)
        //{
            

        //    ObservableCollection<InterpolationRecord> collec = new ObservableCollection<InterpolationRecord>();
        //    DataTable dt = BusinessHelper.ListInterpolation((long)pumpid);
        //    foreach (DataRow _row in dt.Rows)
        //    {
        //        InterpolationRecord ir = new InterpolationRecord();
        //        ir.PK = Convert.ToUInt64(_row["pk_id"]);
        //        ir.TankId = Convert.ToUInt64(_row["pumpid"]);
        //        ir.Level = Convert.ToDouble(_row["in_level"]);
        //        ir.Capacity = Convert.ToDouble(_row["out_cap"]);

        //        collec.Add(ir);
        //    }

        //    return collec;

        //}

    }
    
}
