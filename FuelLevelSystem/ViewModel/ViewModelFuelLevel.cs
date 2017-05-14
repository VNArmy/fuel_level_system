using FuelLevelSystem.Helpers;
using FuelLevelSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.ViewModel
{
    class ViewModelFuelLevel : ViewModelBase
    {
        
        public RelayCommand ShowCommand { get; set; }

        private ObservableCollection<FuelTank> _FuelTanks;

        public ObservableCollection<FuelTank> FuelTanks
        {
            get { return _FuelTanks; }
            set { SetProperty(ref _FuelTanks, value); }
        }

        public ViewModelFuelLevel()
        {
            ShowCommand = new RelayCommand (OnShowCommand);
        }
        private void OnShowCommand()
        {
        }
    }
}
