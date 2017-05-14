using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.Business
{
    public interface ITankListDAL
    {
        /// <summary>
        /// Return all the persistent tanks
        /// </summary>
        List<TankListRecord> GetAllTank(long stationid);

        /// <summary>
        /// Updates or adds the given tank
        /// </summary>
        void UpdateTank(TankListRecord record);

        /// <summary>
        /// Delete the given tank
        /// </summary>
        void DeleteTank(TankListRecord record);
    }
    public class TankListDAL : ITankListDAL
    {
        public TankListDAL()
        {
            BusinessHelper.InitConnection();
        }
        public List<TankListRecord> GetAllTank(long stationid)
        {
            List<TankListRecord> _list = new List<TankListRecord>();
            DataTable dtTank = BusinessHelper.ListAllTank((long)stationid);
            foreach (DataRow _row in dtTank.Rows)
            {
                TankListRecord tlrec = new TankListRecord();
                tlrec.StationId = stationid;
                tlrec.FuelId = Convert.ToInt64(_row["fuelid"]);                
                tlrec.TankDesc = _row["tankdesc"].ToString();
                tlrec.TankId = Convert.ToInt64(_row["tankid"]);
                tlrec.TankName = _row["tankname"].ToString();
                tlrec.Max = Convert.ToDouble(_row["capacity"]);
                tlrec.Offset = Convert.ToDouble(_row["offset"]);
                _list.Add(tlrec);
            }

            return _list;
        }

        public void UpdateTank(TankListRecord record)
        {
            long retId = record.TankId;

            BusinessHelper.InserUpdateTank(ref retId,record.StationId,record.FuelId, record.TankName, record.TankDesc, record.Max,record.Offset);
            if (retId != -1)
            {
                record.TankId = retId;
            }
        }

        public void DeleteTank(TankListRecord record)
        {
            BusinessHelper.DeleteTank(record.TankId);
        }
    }
}
