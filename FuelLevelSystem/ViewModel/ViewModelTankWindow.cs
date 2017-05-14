using DALC4NET;
using FuelLevelSystem.Helpers;
using FuelLevelSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace FuelLevelSystem.ViewModel
{
    class ViewModelTankWindow : ViewModelBase
    {
        public TankModel CurrentTankModel = new TankModel();
        public FuelTank CurrentTank { get; private set; }
        public RelayCommand SaveData { get; set; }
        public RelayCommand ImportFuel { get; set; }
        public ViewModelTankWindow()
        {
            SaveData = new RelayCommand(DoSaveData);
            ImportFuel = new RelayCommand(DoImportFuel);
        }
        //public ViewModelTankWindow(FuelTank _tank) 
        //{
        //    CurrentTankModel.CurrentTank = _tank;
        //}
        private void DoSaveData()
        {
        }
        private void DoImportFuel()
        {
        }
    }
}
