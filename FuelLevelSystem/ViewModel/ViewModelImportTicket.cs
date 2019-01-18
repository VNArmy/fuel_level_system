using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuelLevelSystem.Helpers;
using FuelLevelSystem.Model;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using FuelLevelSystem.View;
using FLS.Business;
using uPLibrary.Networking.M2Mqtt.Messages;
namespace FuelLevelSystem.ViewModel
{
    class ViewModelImportTicket:ViewModelBase
    {
        private bool yesIsCheck;
        public bool YesIsCheck
        {
            get { return this.yesIsCheck; }
            set
            {
                this.yesIsCheck = value;
                importTicketData.SealStatus = true;
                this.OnPropertyChanged("YesIsCheck");
                
                this.OnPropertyChanged("SealStatus");
            }
        }

        private bool noIsCheck;
        public bool NoIsCheck
        {
            get { return this.noIsCheck; }
            set
            {
                this.noIsCheck = value;
                importTicketData.SealStatus = false;
                this.OnPropertyChanged("YesIsCheck");
                
                this.OnPropertyChanged("SealStatus");
            }
        }

        private ImportTicketModel importTicketData;
        public ImportTicketModel ImportTicketData {
            get { return importTicketData; } 
            set { SetProperty(ref importTicketData, value);} 
        }
        private String fuel;
        public String Fuel
        {
            get { return fuel; }
            set { SetProperty(ref fuel, value); } 
            
        }

        public Boolean SaveSuccess { get; set; }

        public RelayCommand<object> SaveCommand { get; set; }
        public RelayCommand<object> ClearCommand { get; set; }

        public Action CloseAction { get; set; } 

        public ViewModelImportTicket(TankModel tankData)
        {
            ImportTicketModel importModel = new ImportTicketModel();
            importModel.StationName = tankData.StationName;
            importModel.TankId = tankData.TankId;
            importModel.FuelId = tankData.FuelId;
            importModel.MaxCapacity = tankData.MaxCapacity;
            importModel.TankLevel = tankData.FuelLevel;
            importModel.TankCapacity = tankData.Capacity;
            importModel.ImportDate = DateTime.Now;
            importModel.ImportCapacity = 0;

            importTicketData = importModel;
            fuel = BusinessHelper.FindFuelById(tankData.FuelId);
            SaveCommand = new RelayCommand<object>(DoSave,CanSave);
            ClearCommand = new RelayCommand<object>(DoClear);
            SaveSuccess = false;
            BusinessHelper.InitConnection();
        }


        private void DoSave(object ignore)
        {

            
            try
            {
                long ticketId = -1;
                ImportTicketData.PumpTotal = BusinessHelper.FindPumpTotalByTankId(importTicketData.TankId);
                
                BusinessHelper.InserUpdateImportTicket(
                    ref ticketId,importTicketData.TankId, ImportTicketData.ImportDate, ImportTicketData.DriverName, ImportTicketData.CarPlate,
                    ImportTicketData.StaffName, ImportTicketData.StaffPos, ImportTicketData.TemplateBottleId, ImportTicketData.ImportCapacity,
                    ImportTicketData.FuelId, ImportTicketData.MaxCapacity, ImportTicketData.TankCapacity, ImportTicketData.TankLevel, ImportTicketData.PumpTotal,
                    ImportTicketData.ExStore,ImportTicketData.SealId,ImportTicketData.SealStatus);

                App.Log.LogDebugMessage(String.Format(@"ticketId: {0}", ticketId));
                if (ticketId != -1)
                {
                    ImportTicketData.TicketId = ticketId;

                    System.Windows.MessageBox.Show(@"Lưu thông tin thành công!!!", @"Lưu thông tin nhập bồn", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    
                    CloseAction(); // Invoke the Action previously defined by the View
                }
                else
                {

                    System.Windows.MessageBox.Show(@"Có lỗi trong quá trình lưu thông tin!!!", @"Lưu thông tin nhập bồn", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    
                }
                
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
        }
        private Boolean CanSave(object ignore)
        {
            try
            {
                if (SaveSuccess)
                {
                    return false;
                }

                if ((ImportTicketData.ImportCapacity == 0) || (ImportTicketData.ImportCapacity + ImportTicketData.TankCapacity > ImportTicketData.MaxCapacity))
                {
                    return false;
                }

                if (
                    (String.IsNullOrEmpty(ImportTicketData.CarPlate) || String.IsNullOrWhiteSpace(ImportTicketData.CarPlate)) ||
                    (String.IsNullOrEmpty(ImportTicketData.TemplateBottleId) || String.IsNullOrWhiteSpace(ImportTicketData.TemplateBottleId)) ||
                    (String.IsNullOrEmpty(ImportTicketData.SealId) || String.IsNullOrWhiteSpace(ImportTicketData.ExStore))

                    )
                {
                    return false;
                }                

                return true;
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
                return false;
            }
        }

        private void DoClear(object ignore)
        {
            importTicketData.DriverName = "";
            importTicketData.CarPlate = "";
            importTicketData.StaffName = "";
            importTicketData.StaffPos = "";
            importTicketData.TemplateBottleId = "";
            importTicketData.ImportCapacity = 0;

        }
    }
}
