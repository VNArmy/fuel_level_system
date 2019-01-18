using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelLevelSystem.Model
{
    class TankModel:GenericRecord
    {
        long _Id;
        string _Name;
        string _Desc;
        long _FuelId;
        
        string _StationName;
        Double _MaxCapacity;
        Double _Capacity;
        Double _Level;
        Double _WLevel;
        Double _Offset;
        string _CapacityText;
        Double _Thermo;
        string _ThermoText;
        Double _Water;
        string _WaterText;
        UInt64 _PumpTotal;
        
        public TankModel()
        {
            _Name = string.Empty;
            _Desc = string.Empty;
            
            _MaxCapacity = 1000;
            _Capacity = 0;
            _Thermo = 0;
            _PumpTotal = 0;
        }
        public long TankId
        {
            get { return _Id; }
            set { _Id = value; RaisePropertyChanged("TankId"); }
        }
        public string Name 
        {
            get { return _Name; }
            set { _Name = value; RaisePropertyChanged("TankName"); }
        }
        public string Description
        {
            get { return _Desc; }
            set { _Desc = value; RaisePropertyChanged("TankDescription"); }
        }
        public long FuelId
        {
            get { return _FuelId; }
            set { _FuelId = value; RaisePropertyChanged("TankFuelId"); }
        }
        public UInt64 StationId { get; set; }
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; RaisePropertyChanged("TankStation"); }
        }
        public Double MaxCapacity
        {
            get { return _MaxCapacity; }
            set { _MaxCapacity = value; RaisePropertyChanged("TankMaxCapacity"); }
        }
        public Double Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; RaisePropertyChanged("TankCapacity"); CapacityText = String.Format(CultureInfo.InvariantCulture, "{0:00.0} lit", value); }
        }
        public string CapacityText
        {
            get { return _CapacityText; }
            set { _CapacityText = value; RaisePropertyChanged("TankCapacityText"); }
        }

        public Double WaterCapacity
        {
            get { return _Water; }
            set { _Water = value; RaisePropertyChanged("TankWaterCapacity"); WaterCapacityText = String.Format(CultureInfo.InvariantCulture, "{0:00.0} lit", value); }
        }
        public string WaterCapacityText
        {
            get { return _WaterText; }
            set { _WaterText = value; RaisePropertyChanged("TankWaterCapacityText"); }
        }

        public Double Thermometer
        {
            get { return _Thermo; }
            set { _Thermo = value; RaisePropertyChanged("TankThermo"); ThermoText = String.Format(CultureInfo.InvariantCulture, "{0:00.00} °C", value); }
        }
        public string ThermoText
        {
            get {return _ThermoText;}
            set { _ThermoText = value; RaisePropertyChanged("TankThermoText"); }
        }
        public Double FuelLevel
        {
            get { return _Level; }
            set { _Level = value; RaisePropertyChanged("TankLevel"); LevelText = String.Format(CultureInfo.InvariantCulture, "{0:00.0} mm", value); }
        }

        public Double OffsetLevel
        {
            get { return _Offset; }
            set { _Offset = value; RaisePropertyChanged("OffsetLevel"); }
        }
        
        private string _LevelText;
        public string LevelText
        {
            get { return _LevelText; }
            set { _LevelText = value; RaisePropertyChanged("TankLevelText"); }
        }
        public Double WaterLevel {
            get { return _WLevel; }
            set { _WLevel = value; RaisePropertyChanged("TankWaterLevel"); WaterLevelText = String.Format(CultureInfo.InvariantCulture, "{0:00.0} mm", value); }
        
        }
        private string _WLevelText;
        public string WaterLevelText
        {
            get { return _WLevelText; }
            set { _WLevelText = value; RaisePropertyChanged("TankWaterLevelText"); }
        }

        public UInt64 TankPumpTotal
        {
            get { return _PumpTotal; }
            set { _PumpTotal = value; RaisePropertyChanged("TankPumpTotal"); }
        }

        public ICommand SavedDataCommand { get; set; }
        public ICommand FuelImportCommand { get; set; }
        public ICommand DetailCommand { get; set; }
        public ICommand FinishCommand { get; set; }
        private bool _IsImportFinish;
        private bool _HasData;

        public bool IsImportFinished
        {
            get
            {
                return _IsImportFinish;
            }
            set
            {
                if (value != _IsImportFinish)
                {
                    _IsImportFinish = value;
                    RaisePropertyChanged("IsImportFinished");
                }
            }
        }

        public bool HasData
        {
            get
            {
                return _HasData;
            }
            set
            {
                if (value != _HasData)
                {
                    _HasData = value;
                    RaisePropertyChanged("HasData");
                }
            }
        }
    }
}
