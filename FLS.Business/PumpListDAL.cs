using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.Business
{
    public interface IPumpListDAL
    {
        /// <summary>
        /// Return all the persistent tanks
        /// </summary>
        List<PumpListRecord> GetAllPump(long stationid);

        /// <summary>
        /// Updates or adds the given tank
        /// </summary>
        void UpdatePump(PumpListRecord record);

        /// <summary>
        /// Delete the given tank
        /// </summary>
        void DeletePump(PumpListRecord record);
    }
    public class PumpListDAL : IPumpListDAL
    {
        public PumpListDAL()
        {
            BusinessHelper.InitConnection();
        }
        public List<PumpListRecord> GetAllPump(long stationid)
        {
            List<PumpListRecord> _list = new List<PumpListRecord>();
            DataTable dtPump = BusinessHelper.ListAllPump((long)stationid);
            foreach (DataRow _row in dtPump.Rows)
            {
                PumpListRecord plrec = new PumpListRecord();
                plrec.StationId = stationid;
                plrec.PumpId = Convert.ToInt64(_row["pumpid"]);
                plrec.TankId = Convert.ToInt64(_row["tankid"]);
                plrec.PumpIp = _row["ipaddress"].ToString();                
                plrec.PumpName = _row["name"].ToString();
                plrec.PumpPort = Convert.ToInt32(_row["port"]);
                plrec.PumpDelayTime = Convert.ToInt32(_row["delaytime"]);
                plrec.PumpDesc = _row["description"].ToString();
                plrec.PumpStatus = Convert.ToInt16(_row["status"]);
                if (!_row.IsNull("pickuptimes"))
                {
                    plrec.PumpPickUp = Convert.ToInt64(_row["pickuptimes"]);

                }
                _list.Add(plrec);
            }

            return _list;
        }

        public void UpdatePump(PumpListRecord record)
        {
            long retId = record.PumpId;

            BusinessHelper.InserUpdatePump(ref retId,record.TankId,record.PumpName, record.PumpIp, record.PumpPort, record.PumpDelayTime,record.PumpDesc,record.PumpStatus);
            if (retId != -1)
            {
                record.PumpId = retId;
            }
        }

        public void DeletePump(PumpListRecord record)
        {
            BusinessHelper.DeletePump(record.PumpId);
        }
    }
}
