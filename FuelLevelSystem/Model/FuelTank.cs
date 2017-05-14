using NationalInstruments.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelLevelSystem.Model
{
    class FuelTank:GenericRecord
    {
        UInt64 _Id;
        string _Name;
        string _Desc;
        string _KindofFuel;
        string _StationName;
        Double _MaxCapacity;
        Range<Double> _Range;
        Double _Capacity;
        string _CapacityText;
        Double _Thermo;
        string _ThermoText;
        public FuelTank()
        {
            _Name = string.Empty;
            _Desc = string.Empty;
            _KindofFuel = string.Empty;
            _MaxCapacity = 1000;
            _Capacity = 0;
            _Thermo = 0;
        }
        public UInt64 TankId
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
        public string KindOfFuel
        {
            get { return _KindofFuel; }
            set { _KindofFuel = value; RaisePropertyChanged("TankKindofFuel"); }
        }
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; RaisePropertyChanged("TankStation"); }
        }
        public Double MaxCapacity
        {
            get { return _MaxCapacity; }
            set { _MaxCapacity = value; _Range = new Range<double>(0,_MaxCapacity); RaisePropertyChanged("TankCapacity"); }
        }
        public Double Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; RaisePropertyChanged("TankCapacity"); CapacityText = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", value); }
        }
        public string CapacityText
        {
            get { return _CapacityText; }
            set { _CapacityText = value; RaisePropertyChanged("TankCapacityText"); }
        }
        public Double Thermometer
        {
            get { return _Thermo; }
            set { _Thermo = value; RaisePropertyChanged("TankThermo"); ThermoText = String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", value); }
        }
        public string ThermoText
        {
            get {return _ThermoText;}
            set { _ThermoText = value; RaisePropertyChanged("TankThermoText"); }
        }
        public Double FuelLevel { get; set; }
        public Double WaterLevel { get; set; }
        public Range<Double> CapacityRange
        {
            get { return _Range; }
        }
        
        //public ICommand TankCommand { get; set; }

        //private ObservableCollection<FuelTank> _FuelTank;
        //public ObservableCollection<FuelTank> FuelTanks
        //{
        //    get { return _FuelTank; }
        //    set { _FuelTank = value; RaisePropertyChanged("FuelTank"); }
        //}
    }
}
