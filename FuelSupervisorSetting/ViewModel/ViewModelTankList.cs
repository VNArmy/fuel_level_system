using FLS.Business;
using FuelSupervisorSetting.Helpers;
using FuelSupervisorSetting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FuelSupervisorSetting.ViewModel
{
    class ViewModelTankList:ViewModelBase
    {
        
        
        private ObservableCollection<FuelList> _ListFuel;
        public ObservableCollection<FuelList> ListFuel
        {
            get { return _ListFuel; }
            set { SetProperty(ref _ListFuel, value); }
        }
       
        
        
        public ViewModelTankList()
        {
            try
            {
                BusinessHelper.InitConnection();

                ListFuel = getAllFuel();
            
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
                
            }
            
        }
        
        
        private ObservableCollection<FuelList> getAllFuel()
        {
            ObservableCollection<FuelList> _list = new ObservableCollection<FuelList>();
            try{
                DataTable dt = BusinessHelper.ListAllFuel();
                foreach (DataRow _row in dt.Rows)
                {
                    FuelList fl = new FuelList();
                    fl.FuelId = Convert.ToInt64(_row["fuelid"]);
                    fl.FuelDesc = _row["fueldesc"].ToString();
                    _list.Add(fl);
                }
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);

            }
            return _list;

        }
        
    }
}
