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
namespace FuelLevelSystem.ViewModel
{
    class ViewModelTankDetails:ViewModelBase
    {
        private ResultRecord lastRecord = new ResultRecord();
        private ObservableCollection<ResultRecord> _Results;
        public ObservableCollection<ResultRecord> Results 
        {
            get { return _Results; }
            set { SetProperty(ref _Results, value); } 
        }


        public RelayCommand<object> SearchCommand { get; set; }
        private readonly Int64 _TankId;
        public String Description { get; private set; }
        public TankDetails CurrentTankDetails { get; set; }
        public ViewModelTankDetails(Int64 tankId,String description)
        {
            _TankId = tankId;
            Description = description;
            SearchCommand = new RelayCommand<object>( DoSearch);
            CurrentTankDetails = new TankDetails();
            CurrentTankDetails.FromDate = DateTime.Now;
            CurrentTankDetails.ToDate = DateTime.Now;
            
        }
        private void DoSearch(object ignore)
        {

            var _Results = new ObservableCollection<ResultRecord>();
            try
            {
                DataTable dtTank = FLS.Business.BusinessHelper.ListTankDetails(_TankId, CurrentTankDetails.FromDate, CurrentTankDetails.ToDate);
                foreach (DataRow _row in dtTank.Rows)
                {
                    ResultRecord _rr = new ResultRecord();
                    _rr.Capacity = Convert.ToDouble(_row["capacity"]);
                    _rr.Thermo = Convert.ToDouble(_row["thermo"]);
                    _rr.WaterCapacity = Convert.ToDouble(_row["water"]);
                    _rr.Savedtime = Convert.ToDateTime(_row["savedtime"]);
                    _rr.bStatus = Convert.ToByte(_row["status"]);

                    if (_rr.bStatus == 0)
                    {

                        if (lastRecord.bStatus == 0)
                        {
                            _rr.Status = @"Selling";
                            _rr.Details = String.Format(@"Thời điểm {0}, Nhiên liệu: {1:F1}, Nước: {2:F1}, Nhiệt độ: {3}",
                            _rr.Savedtime, _rr.Capacity, _rr.WaterCapacity, _rr.Thermo);
                        }
                        else
                        {
                            _rr.Status = @"Kết thúc nhập";
                            _rr.Details = String.Format(@"Thời điểm {0}, Nhiên liệu: {1:F1}, Nước: {2:F1}, Nhiệt độ: {3}{6}Tổng lượng nhập: Nhiên liệu: {4:F1}, Nước: {5:F1}",
                            _rr.Savedtime, _rr.Capacity, _rr.WaterCapacity, _rr.Thermo, _rr.Capacity - lastRecord.Capacity, _rr.WaterCapacity - lastRecord.WaterCapacity, Environment.NewLine);
                        }
                    }
                    else if (_rr.bStatus == 1)
                    {
                        _rr.Status = @"Bắt đầu nhập";
                        _rr.Details = String.Format(@"Thời điểm {0}, Nhiên liệu: {1:F1}, Nước: {2:F1}, Nhiệt độ: {3}",
                            _rr.Savedtime, _rr.Capacity, _rr.WaterCapacity, _rr.Thermo);
                    }
                    lastRecord = _rr;
                    _Results.Add(_rr);

                }
                Results = _Results;
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
        }
    }
}
