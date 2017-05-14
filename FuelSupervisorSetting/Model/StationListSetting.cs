using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelSupervisorSetting.Model
{
    class StationListSetting
    {
        public StationListRecord CurrentStationRecord { get; set; }
        public ICommand StationSaveCommand { get; set; }

    }
}
