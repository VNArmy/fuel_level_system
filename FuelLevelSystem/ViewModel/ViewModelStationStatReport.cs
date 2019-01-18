using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.ViewModel
{
    class ViewModelStationStatReport : ViewModelBase
    {
        private Model.StationStat stationStatData;
        public Model.StationStat StationStatData
        {
            get { return stationStatData; }
            set { SetProperty(ref stationStatData, value); }
        }

        public ViewModelStationStatReport()
        {
        }
    }
}
