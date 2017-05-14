using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.Model
{
    class ResultRecord:GenericRecord
    {
        Double _capacity;
        DateTime _savedtime;
        Double _thermo;
        Double _water;
        byte _bStatus;
        String _status;
        String _Details;
        public DateTime Savedtime
        {
            get { return _savedtime; }
            set
            {
                _savedtime = value; RaisePropertyChanged("SavedtimeDetail");
            }
        }

        public Double Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value; RaisePropertyChanged("CapacityDetail");
            }
        }
        public Double Thermo
        {
            get { return _thermo; }
            set
            {
                _thermo = value; RaisePropertyChanged("ThermoDetail");
            }
        }

        public Double WaterCapacity
        {
            get { return _water; }
            set
            {
                _water = value; RaisePropertyChanged("WaterDetail");
            }
        }

        public byte bStatus
        {
            get { return _bStatus; }
            set
            {
                _bStatus = value; RaisePropertyChanged("bStatusDetail");
            }
        }

        public String Status
        {
            get { return _status; }
            set
            {
                _status = value; RaisePropertyChanged("StatusDetail");
            }
        }
        public String Details
        {
            get { return _Details; }
            set
            {
                _Details = value; RaisePropertyChanged("Details");
            }
        }
    }
}
