using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.ViewModel
{
    class MenuViewModel : ViewModelBase
    {
        private bool _ismenuvisibility;
        public bool IsMenuVisibility
        {
            get { return _ismenuvisibility; }
            set { SetProperty(ref _ismenuvisibility, value); }
        }
    }
}
