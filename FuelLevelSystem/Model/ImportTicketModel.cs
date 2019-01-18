using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelLevelSystem.Model
{
    class ImportTicketModel : GenericRecord
    {
        
        private long ticketId;
        private long tankId;
        private String stationName = String.Empty;
        private String driverName = String.Empty;
        private String carPlate;
        private String staffName = String.Empty;
        private String staffPos = String.Empty;
        private String templateBottleId;

        private DateTime importDate = DateTime.Now;

        private Double importCapacity;

       

        private long fuelId;
        private Double maxCapacity;
        private Double capacity;
        private Double level;
        private UInt64 pumpTotal;

        private String exStore;
        private Boolean sealStatus;
        private String sealId;

        public long FuelId
        {
            get { return fuelId; }
            set { fuelId = value; RaisePropertyChanged("FuelId"); }
        }

        public Double MaxCapacity
        {
            get { return maxCapacity; }
            set { maxCapacity = value; RaisePropertyChanged("MaxCapacity"); }
        }
        public UInt64 PumpTotal
        {
            get { return pumpTotal; }
            set { pumpTotal = value; RaisePropertyChanged("PumpTotal"); }
        }

        public Double TankCapacity
        {
            get { return capacity; }
            set { capacity = value; RaisePropertyChanged("TankCapacity"); }
        }

        public Double TankLevel
        {
            get { return level; }
            set { level = value; RaisePropertyChanged("TankLevel"); }
        }

        public Double ImportCapacity
        {
            get { return importCapacity; }
            set { importCapacity = value; RaisePropertyChanged("ImportCapacity"); }
        }

        public DateTime ImportDate
        {
            get { return importDate; }
            set { importDate = value; RaisePropertyChanged("ImportDate"); }
        }
        public long TicketId
        {
            get { return ticketId; }
            set { ticketId = value; RaisePropertyChanged("TicketId"); }
        }
        public long TankId
        {
            get { return tankId; }
            set { tankId = value; RaisePropertyChanged("TankId"); }
        }
        public String StationName
        {
            get { return stationName; }
            set { stationName = value; RaisePropertyChanged("StationName"); }
        }
        public String DriverName
        {
            get { return driverName; }
            set { driverName = value; RaisePropertyChanged("DriverName"); }
        }
        public String CarPlate
        {
            get { return carPlate; }
            set { carPlate = value; RaisePropertyChanged("CarPlate"); }
        }
        public String StaffName
        {
            get { return staffName; }
            set { staffName = value; RaisePropertyChanged("StaffName"); }
        }
        public String StaffPos
        {
            get { return staffPos; }
            set { staffPos = value; RaisePropertyChanged("StaffPos"); }
        }
        public String TemplateBottleId
        {
            get { return templateBottleId; }
            set { templateBottleId = value; RaisePropertyChanged("TemplateBottleId"); }
        }

        public String ExStore
        {
            get { return exStore; }
            set { exStore = value; RaisePropertyChanged("ExStore"); }
        }
        public Boolean SealStatus
        {
            get { return sealStatus; }
            set { sealStatus = value; RaisePropertyChanged("SealStatus"); }
        }
        public String SealId
        {
            get { return sealId; }
            set { sealId = value; RaisePropertyChanged("SealId"); }
        }
        
        //public ObservableCollection<ResultRecord> Results
        //{
        //    get { return _Result; }
        //    set { _Result = value; RaisePropertyChanged("ResultDetails"); }
        //}
    }
}
