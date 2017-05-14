using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.Business
{
    public interface IStationListDAL
    {
        /// <summary>
        /// Return all the persistent customers
        /// </summary>
        List<StationListRecord> GetAllStation();

        /// <summary>
        /// Updates or adds the given customer
        /// </summary>
        void UpdateStation(StationListRecord record);

        /// <summary>
        /// Delete the given customer
        /// </summary>
        void DeleteStation(StationListRecord record);
    }

    public class StationListDAL : IStationListDAL
    {
        public StationListDAL()
        {
            BusinessHelper.InitConnection();
        }
        public List<StationListRecord> GetAllStation()
        {
            List<StationListRecord> collec = new List<StationListRecord>();
            DataTable dt = BusinessHelper.ListAllStation(-1);
            foreach (DataRow _row in dt.Rows)
            {
                StationListRecord sl = new StationListRecord();
                sl.StationId = Convert.ToInt64(_row["stationid"]);
                sl.StationName = _row["stationname"].ToString();
                sl.StationDesc = _row["stationdesc"].ToString();
                sl.StationAddress = _row["stationaddress"].ToString();
                sl.TaxCode = _row["stationtaxcode"].ToString();

                collec.Add(sl);
            }

            return collec;
        }
        public void UpdateStation(StationListRecord record) 
        {
            long retId=record.StationId;
            BusinessHelper.InserUpdateStation(ref retId, record.StationName, record.StationDesc, record.StationAddress, record.TaxCode);
            if (retId != -1)
            {
                record.StationId = retId;
            }
        }
        public void DeleteStation(StationListRecord record)
        {
            BusinessHelper.DeleteStation(record.StationId);
        }
    }
    

}
