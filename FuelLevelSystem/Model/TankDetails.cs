using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FuelLevelSystem.Model
{
    class TankDetails:GenericRecord
    {
        //private ObservableCollection<ResultRecord> _Result;
        private DateTime _FromDate;
        private DateTime _ToDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; RaisePropertyChanged("FromDateDetail"); }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; RaisePropertyChanged("ToDateDetail"); }
        }
        //public ObservableCollection<ResultRecord> Results
        //{
        //    get { return _Result; }
        //    set { _Result = value; RaisePropertyChanged("ResultDetails"); }
        //}
    }
}
