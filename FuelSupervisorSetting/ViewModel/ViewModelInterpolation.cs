using FLS.Business;
using FuelSupervisorSetting.Helpers;
using FuelSupervisorSetting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.ViewModel
{
    class ViewModelInterpolation:ViewModelBase
    {

        //public RelayActionCommand ImportExcelCommand { get; set; }
        private Double _Offset;
        public Double Offset
        {
            get { return _Offset; }
            set { SetProperty(ref _Offset, value); }
        }
        
        public ViewModelInterpolation()
        {
            
        }
        
    }
}
