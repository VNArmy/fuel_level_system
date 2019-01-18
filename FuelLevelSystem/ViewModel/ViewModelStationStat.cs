using FLS.Business;
using FuelLevelSystem.Helpers;
using FuelLevelSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace FuelLevelSystem.ViewModel
{
    //struct Fuel_t
    //{
    //    long _FuelId;
    //    String _FuelDesc;
    //}
    class ViewModelStationStat : ViewModelBase
    {
        private MqttClient statPublicClient;
        private UInt64 stationId;
        private StationStat stationStatData;
        public StationStat StationStatData
        {
            get { return stationStatData; }
            set { SetProperty(ref stationStatData, value); }
        }
        private ObservableCollection<FuelStat> _StatData;
        public ObservableCollection<FuelStat> StatData
        {
            get { return _StatData; }
            set { SetProperty(ref _StatData, value); }
        }

        public RelayCommand PrintCommand { get; set; }

        public ViewModelStationStat()
        {
            stationStatData = new StationStat();           
            
        }
        public ViewModelStationStat(IList<TankModel>  tanks, MqttClient _statPublicClient)
        {
            this.statPublicClient = _statPublicClient;

            StationStat _StationStatData = new StationStat();
            _StationStatData.StationName = tanks[0].StationName;
            stationId = tanks[0].StationId;
            _StationStatData.StatDate = DateTime.Now;
            _StationStatData.StatEndDate = _StationStatData.StatDate.AddMinutes(30);
            this.stationStatData = _StationStatData;

            FuelStat fuelStat = new FuelStat();

            BusinessHelper.InitConnection();
            DataTable dtFuel = BusinessHelper.ListAllFuel();
            Hashtable hashFuel = new Hashtable();
            foreach(DataRow _row in dtFuel.Rows)
            {
                            
                long FuelId = Convert.ToInt64(_row["fuelid"]);                                
                String FuelDesc = _row["fueldesc"].ToString();
                hashFuel.Add(FuelId,FuelDesc);

            }

            List<FuelStatRecord> fuelStatList = tanks
                .GroupBy(t => t.FuelId)
                .Select(ct => new FuelStatRecord()
                {
                    FuelName = (String)hashFuel[(Int64) ct.First().FuelId],
                    FuelPumpTotal = (UInt64)ct.Sum(p => (decimal)p.TankPumpTotal),
                    Capacity = (UInt64)ct.Sum(c=>c.Capacity),
                    
                }
                ).ToList();
            this._StatData = new ObservableCollection<FuelStat>();
            foreach (var fs in fuelStatList)
            {
                FuelStat _fuelStat = new FuelStat();
                _fuelStat.FuelName = fs.FuelName;
                _fuelStat.MeasuredCapacity = fs.Capacity;
                _fuelStat.MeasuredTotal = fs.FuelPumpTotal;
                
                _StatData.Add(_fuelStat);
            }
            this.PrintCommand = new RelayCommand(DoPrintStationStat);
        }

        private void DoPrintStationStat(object ignore)
        {
            ViewModelStationStatReport vmStationStatReport = new ViewModelStationStatReport();
            Model.StationStat _StationStat = new StationStat();
            _StationStat.StationName = stationStatData.StationName;
            _StationStat.StatDate = DateTime.Now;
            _StationStat.StatEndDate = _StationStat.StatDate.AddMinutes(30);
            _StationStat.StaffName = stationStatData.StaffName;
            _StationStat.StaffPos = stationStatData.StaffPos;
            _StationStat.StaffName2 = stationStatData.StaffName2;
            _StationStat.StaffPos2 = stationStatData.StaffPos2;
            _StationStat.StaffName3 = stationStatData.StaffName3;
            _StationStat.StaffPos3 = stationStatData.StaffPos3;
            _StationStat.StaffName4 = stationStatData.StaffName4;
            _StationStat.StaffPos4 = stationStatData.StaffPos4;
            _StationStat.StaffName5 = stationStatData.StaffName5;
            _StationStat.StaffPos5 = stationStatData.StaffPos5;

            String _statGuidId = String.Empty;
            if (StatData.Count>0)
            {
                _statGuidId = Guid.NewGuid().ToString();
            }
            else
            {

                System.Windows.MessageBox.Show("No Fuel in Stat Report!!!","Stat Report",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error);
                return;
            }
            
            foreach (var fs in StatData)
            {
                BusinessHelper.InsertStatTicket(_statGuidId, fs.FuelName, fs.MeasuredCapacity, fs.NotedCapacity, fs.MeasuredTotal, fs.NotedTotal, _StationStat.StatDate);
                if (fs.FuelName.Contains(@"A92"))
                {
                    _StationStat.BeginPumpA92 = fs.NotedTotal;
                    _StationStat.EndPumpA92 = fs.MeasuredTotal;
                    _StationStat.NotedA92 = fs.NotedCapacity;
                    _StationStat.MeasA92 = fs.MeasuredCapacity;

                }
                else if (fs.FuelName.Contains(@"A95"))
                {
                    _StationStat.BeginPumpA95 = fs.NotedTotal;
                    _StationStat.EndPumpA95 = fs.MeasuredTotal;
                    _StationStat.NotedA95 = fs.NotedCapacity;
                    _StationStat.MeasA95 = fs.MeasuredCapacity;
                }
                else if (fs.FuelName.Contains(@"05S"))
                {
                    _StationStat.BeginPumpDO005S = fs.NotedTotal;
                    _StationStat.EndPumpDO005S = fs.MeasuredTotal;
                    _StationStat.NotedDO005S = fs.NotedCapacity;
                    _StationStat.MeasDO005S = fs.MeasuredCapacity;
                }
                else if (fs.FuelName.Contains(@"02S"))
                {
                    _StationStat.BeginPumpDO025S = fs.NotedTotal;
                    _StationStat.EndPumpDO025S = fs.MeasuredTotal;
                    _StationStat.NotedDO025S = fs.NotedCapacity;
                    _StationStat.MeasDO025S = fs.MeasuredCapacity;
                }
                else if (fs.FuelName.Contains(@"E5"))
                {
                    _StationStat.BeginPumpE5 = fs.NotedTotal;
                    _StationStat.EndPumpE5 = fs.MeasuredTotal;
                    _StationStat.NotedE5 = fs.NotedCapacity;
                    _StationStat.MeasE5 = fs.MeasuredCapacity;
                }
                else if (fs.FuelName.Contains(@"KO"))
                {
                    _StationStat.BeginPumpKO = fs.NotedTotal;
                    _StationStat.EndPumpKO = fs.MeasuredTotal;
                    _StationStat.NotedKO = fs.NotedCapacity;
                    _StationStat.MeasKO = fs.MeasuredCapacity;
                }
                
            }
            vmStationStatReport.StationStatData = _StationStat;
            String _pubData = String.Format(@"{{""ticketId"": ""{0}"" , ""stationId"": {1} }}", _statGuidId, this.stationId );
            this.statPublicClient.Publish(@"statticket/", Encoding.UTF8.GetBytes(_pubData), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            View.StationStatReportWindow stationStatReportWnd = new View.StationStatReportWindow();
            stationStatReportWnd.DataContext = vmStationStatReport;
            stationStatReportWnd.Show();
        }
    }
}
