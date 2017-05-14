using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelLevelSystem.Model
{
    class FuelStation : GenericRecord
    {
        UInt64 _id;
        string _name;
        string _address;
        string _taxCode;

        public ICommand Command { get; set; }

        public FuelStation(UInt64 id, string name, string desc, string addr, string tax)
        {
            _id = id;
            _name = name;
            _address = addr;
            _taxCode = tax;
            Desc = desc;
        }
        public UInt64 Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
        }
        public string Address
        {
            get { return _address; }
        }
        public string TaxCode
        {
            get { return _taxCode; }
        }
        public string Desc
        {
            get;
            private set;
        }

        //private ObservableCollection<FuelStation> _FuelStations;
        //public ObservableCollection<FuelStation> FuelStations
        //{
        //    get { return _FuelStations; }
        //    set { _FuelStations = value; RaisePropertyChanged("FuelStations"); }
        //}
        //private ObservableCollection<FuelTank> _FuelTanks;
        //public ObservableCollection<FuelTank> FuelTanks
        //{
        //    get { return _FuelTanks; }
        //    set { _FuelTanks = value; RaisePropertyChanged("FuelTanks"); }
        //}

    }
}
