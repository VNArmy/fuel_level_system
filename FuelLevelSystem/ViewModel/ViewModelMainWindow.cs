using FLS.Business;
using FuelLevelSystem.Helpers;
using FuelLevelSystem.Model;
using FuelLevelSystem.View;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace FuelLevelSystem.ViewModel
{
    public enum SaveDataStatus
    {
        SAVE = 0,
        IMPORT
    }
    class ViewModelMainWindow : ViewModelBase
    {
        private MqttClient clientSub;
        private string _HostMqtt;
        private UInt16 _HostPort;
        private string _MqttUser;
        private string _MqttPwd;

        private System.Timers.Timer demoTimer;

        public RelayCommand<UInt64> LoadStationTanks { get; set; }

        public RelayCommand<UInt64> SaveDataCommand { get; set; }
        public RelayCommand<UInt64> DetailCommand { get; set; }
        public RelayCommand<UInt64> FuelImportCommand { get; set; }
        public RelayCommand<UInt64> FinishCommand { get; set; }

        public RelayCommand StationStatCommand { get; set; }
                   

        //public AuthenticationViewModel AuthVM { get; set; }

        private ObservableCollection<FuelStation> _CollecStation;
        public ObservableCollection<FuelStation> CollecStation
        {
            get { return _CollecStation; }
            set { SetProperty(ref _CollecStation, value); }
        }
        
        private ObservableCollection<TankModel> _CollecTank;
        public ObservableCollection<TankModel> CollecTank
        {
            get { return _CollecTank; }
            set { SetProperty(ref _CollecTank, value); }
        }
        //private ObservableCollection<PumpModel> _CollecPump;
        //public ObservableCollection<PumpModel> CollecPump
        //{
        //    get { return _CollecPump; }
        //    set { SetProperty(ref _CollecPump, value,"CollecPump"); }
        //}

        public static MqttClient ClientPub{get;set;}
        public ViewModelMainWindow()
        {
            try
            {
                //AuthVM = new AuthenticationViewModel();

                //LoginCommand = new RelayCommand(DoLogin);
                BusinessHelper.InitConnection();
                MqttClientInit();

                LoadStationTanks = new RelayCommand<UInt64>(getCurrentTankList);
                SaveDataCommand = new RelayCommand<UInt64>(DoSaveData, CanSaveData);
                DetailCommand = new RelayCommand<UInt64>(DoTankDetails);
                FuelImportCommand = new RelayCommand<UInt64>(DoFuelImport, CanFuelImport);
                FinishCommand = new RelayCommand<UInt64>(DoImportFinish, CanFinishImport);
                StationStatCommand = new RelayCommand(DoStationStat);
                CollecStation = getAllStation();
                

                if (App.Demo == 1)
                {
                    demoTimer = new System.Timers.Timer(5000);
                    demoTimer.Elapsed += demoTimer_Elapsed;
                    demoTimer.Start();
                }
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
            
        }

        void demoTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TankModel t = new TankModel();            
                
            t.TankId = 1;
            t.FuelLevel = 100;
            t.Capacity = 10000;
            t.WaterLevel = 10;
            t.WaterCapacity = 100;
            t.Thermometer = 25;
            t.HasData = false;        
            

            foreach (var _tank in CollecTank)
            {
                if (_tank.TankId == t.TankId)
                {
                    _tank.FuelLevel = t.FuelLevel;
                    _tank.Capacity = t.Capacity;
                    _tank.WaterLevel = t.WaterLevel;
                    _tank.WaterCapacity = t.WaterCapacity;
                    _tank.Thermometer = t.Thermometer;
                    if (!_tank.HasData)
                    {
                        _tank.HasData = true;
                    }
                    
                    break;
                }
            }

            ObservableCollection<Model.TankModel> _FuelTanks = new ObservableCollection<Model.TankModel>(CollecTank);
            if (CollecTank != null)
            {

                CollecTank = null;

            }

            CollecTank = _FuelTanks;                                                
            
        }
        

        private ObservableCollection<FuelStation> getAllStation()
        {
            try
            {
                DataTable table = BusinessHelper.ListAllStation(-1);
                ObservableCollection<FuelStation> _fuelstations = new ObservableCollection<FuelStation>();
                if (table.Rows.Count != 0)
                {

                    foreach (DataRow _datarow in table.Rows)
                    {
                        UInt64 i = Convert.ToUInt64(_datarow["stationid"]);
                        string n = _datarow["stationname"].ToString();
                        string d = _datarow["stationdesc"].ToString();
                        string a = _datarow["stationaddress"].ToString();
                        string t = _datarow["stationtaxcode"].ToString();
                        FuelStation _fs = new FuelStation(i, n, d, a, t);
                        _fs.Command = this.LoadStationTanks;
                        _fuelstations.Add(_fs);

                    }
                }
                else
                {
                    MessageBox.Show("Declare Param First!!!");
                    return null;
                }
                return _fuelstations;
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
                return null;
            }
            
        }



        private void DoSaveData(UInt64 tankId)
        {
            var selTank = (from _tank in CollecTank
                           where _tank.TankId == (long)tankId
                           select _tank).FirstOrDefault() as TankModel;
            DateTime _now = DateTime.Now;

            try
            {
                BusinessHelper.SaveTankData(selTank.TankId, selTank.Capacity,selTank.Thermometer,selTank.WaterCapacity, (Byte)SaveDataStatus.SAVE, _now);
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
            }
        }
        private bool CanSaveData(UInt64 tankId)
        {
            if (CollecTank == null)
            {
                return false;
            }
            var selTank = (from _tank in CollecTank
                           where _tank.TankId == (long) tankId
                           select _tank).FirstOrDefault() as TankModel;
            if (selTank == null)
            {
                return false;
            }
            return (selTank.IsImportFinished & selTank.HasData);
        }
        private void DoFuelImport(UInt64 tankId)
        {
            var selTank = (from _tank in CollecTank
                           where _tank.TankId == (long)tankId
                           select _tank).FirstOrDefault() as TankModel;
            DateTime _now = DateTime.Now;

            try
            {

                
                if (App.Demo != 1)
                {
                    BusinessHelper.SaveTankData(selTank.TankId, selTank.Capacity, selTank.Thermometer, selTank.WaterCapacity, (Byte)SaveDataStatus.IMPORT, _now);

                }
                
                String _pubTopic = String.Format(@"importbegin");
                String _pubData = String.Format(@"{{""tankId"": {0} }}", tankId);
                if ((ClientPub != null) && (!ClientPub.IsConnected))
                {
                    string pubClientId = Guid.NewGuid().ToString();


                    ClientPub.Connect(pubClientId, _MqttUser, _MqttPwd);

                }
                ClientPub.Publish(_pubTopic, Encoding.UTF8.GetBytes(_pubData), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

                App.Log.LogDebugMessage("Topic: " + _pubTopic + "\t Data:" + _pubData);

                selTank.IsImportFinished = false;

                ImportTicketWindow importTicketWnd = new ImportTicketWindow();
                ViewModelImportTicket vmImportTicket = new ViewModelImportTicket(selTank);
                importTicketWnd.DataContext = vmImportTicket;
                if (vmImportTicket.CloseAction == null)
                    vmImportTicket.CloseAction = new Action(() => importTicketWnd.Close());

                //tdWnd.dgTankDetail.DataContext = vmTankDetail.CurrentTankDetails;
                importTicketWnd.WindowState = WindowState.Maximized;
                importTicketWnd.ShowDialog();   


            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
            }
        }
        private bool CanFuelImport(UInt64 tankId)
        {
            if (CollecTank == null)
            {
                return false;
            }
            var selTank = (from _tank in CollecTank
                           where _tank.TankId == (long)tankId
                           select _tank).FirstOrDefault() as TankModel;
            if (selTank == null)
            {
                return false;
            }
            if (!selTank.HasData) return false;
            
            return selTank.IsImportFinished ;
        }

        private void DoImportFinish(UInt64 tankId)
        {
            var selTank = (from _tank in CollecTank
                           where _tank.TankId ==(long) tankId
                           select _tank).FirstOrDefault() as TankModel;
            DateTime _now = DateTime.Now;

            try
            {
                if (App.Demo != 1)
                {
                    if (MessageBox.Show(String.Format(@"Kết thúc nhập bồn {0}", selTank.Description), @"Nhập nhiên liệu",MessageBoxButton.OKCancel,MessageBoxImage.Question) == MessageBoxResult.Cancel){
                        return;
                    }
                    BusinessHelper.SaveTankData(selTank.TankId, selTank.Capacity, selTank.Thermometer, selTank.WaterCapacity, (Byte)SaveDataStatus.SAVE, _now);

                    DataTable dt = BusinessHelper.GetTankList(selTank.TankId);
                    foreach (DataRow _dr in dt.Rows)
                    {
                        long tankid = Convert.ToInt64(_dr["tankid"]);
                        //BusinessHelper.UpdateTankStatus(tankid);
                        BusinessHelper.UpdatePumpStatus(tankid);
                    }
                }
                

                DataTable _dtReport = BusinessHelper.FindLastTicketByTankId(selTank.TankId);
                ImportReport _importReport = new ImportReport();
                long _ticketId = -1;
                long _tankId = -1;
                if (_dtReport != null)
                {
                    DataRow _dr = _dtReport.Rows[0];
                    _ticketId = Convert.ToInt64(_dr["ticket_id"]);
                    _tankId = Convert.ToInt64(_dr["tank_id"]);
                    _importReport.StationName = selTank.StationName;
                    _importReport.ReportDate = Convert.ToDateTime( _dr["ticketdate"]);
                    _importReport.DriverName = Convert.ToString(_dr["drivername"]);
                    _importReport.CarPlate = Convert.ToString(_dr["carplate"]);
                    _importReport.StaffName = Convert.ToString(_dr["staffname"]);
                    _importReport.StaffPos = Convert.ToString(_dr["staffpos"]);
                    _importReport.TemplateBottle = Convert.ToString(_dr["templatebottle"]);
                    _importReport.ImportCapacity = Convert.ToDouble(_dr["importcapacity"]);
                    long fuelId = Convert.ToInt64(_dr["fuel_id"]);
                    _importReport.Fuel = BusinessHelper.FindFuelById(fuelId);
                   
                    _importReport.BeginCapacity = Convert.ToDouble(_dr["capacity"]);
                    _importReport.BeginLevel = Convert.ToDouble(_dr["level"]);
                    _importReport.BeginTotalPump = Convert.ToUInt64(_dr["pumptotal"]);
                    _importReport.ExStore = Convert.ToString(_dr["exstore"]);
                    _importReport.SealId = Convert.ToString(_dr["sealid"]);
                    _importReport.SealStatus = Convert.ToBoolean(_dr["sealstatus"]);
                }
                else
                {
                    System.Windows.MessageBox.Show(@"Phiếu không tồn tại!!!", @"Kết thúc nhập bồn", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                    
                }
                _importReport.EndTotalPump = BusinessHelper.FindPumpTotalByTankId(selTank.TankId);
                _importReport.EndCapacity = selTank.Capacity;
                _importReport.EndLevel = selTank.FuelLevel;
                _importReport.RealCapacity = _importReport.EndCapacity - _importReport.BeginCapacity;

                if(!BusinessHelper.UpdateImportTicket(_ticketId, _importReport.EndTotalPump, _importReport.EndCapacity, _importReport.EndLevel))
                {
                    App.Log.LogInfoMessage(String.Format("Update ticket {0} failed",_ticketId));
                    System.Windows.MessageBox.Show(@"Lỗi trong quá trình cập nhật phiếu!!!", @"Kết thúc nhập bồn", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }
                else
	            {
                    String _pubTopic = String.Format(@"importticket/{0}", _tankId);
                    String _pubData = String.Format(@"{{""ticketId"": {0} }}", _ticketId);
                    if ((ClientPub != null) && (!ClientPub.IsConnected))
                    {
                        string pubClientId = Guid.NewGuid().ToString();

                       
                        ClientPub.Connect(pubClientId, _MqttUser, _MqttPwd);
                        
                    }
                    ClientPub.Publish(_pubTopic, Encoding.UTF8.GetBytes(_pubData), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,false);
                    
                    selTank.IsImportFinished = true;
                    
                    App.Log.LogDebugMessage("Topic: " + _pubTopic + "\t Data:" + _pubData);
	            }
                
                
               
                ImportReportWindow importReportWnd = new ImportReportWindow();
                ViewModelImportReport vmImportReport;
                if (App.Demo == 1)
                {
                    vmImportReport = new ViewModelImportReport();
                }
                else
                {
                    vmImportReport = new ViewModelImportReport(_importReport);
                }
                
                
                importReportWnd.DataContext = vmImportReport;
                importReportWnd.ShowDialog();
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
            }

        }

        private bool CanFinishImport(UInt64 tankId)
        {
            try
            {
                var selTank = (from _tank in CollecTank
                               where _tank.TankId ==(long) tankId
                               select _tank).FirstOrDefault() as TankModel;
                if (selTank == null)
                {
                    return false;
                }
                if (!selTank.HasData)
                {
                    return false;
                }
                return (!selTank.IsImportFinished);
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
                return false;
            }                        
        }
        private void DoTankDetails(UInt64 tankId)
        {
            var selTank = (from _tank in CollecTank
                               where _tank.TankId == (long)tankId
                               select _tank).FirstOrDefault() as TankModel;
            if (selTank == null)	
            {
                return;
            }
            TankDetail tdWnd = new TankDetail();
            ViewModelTankDetails vmTankDetail = new ViewModelTankDetails((long)tankId,selTank.Description);
            tdWnd.DataContext = vmTankDetail;
            //tdWnd.dgTankDetail.DataContext = vmTankDetail.CurrentTankDetails;
            tdWnd.ShowDialog();
        }

        public void getCurrentTankList(UInt64 stationid)
        {
            try
            {
                CollecTank = getTankList((long)stationid);
                if (CollecTank != null)
                {
                    foreach (TankModel tm in CollecTank)
                    {
                        SubscribeTank((long)stationid, tm.TankId);
                    }
                    //SubscribeStation((long)stationid);
                }


            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
            }
            
            //CollecPump = new ObservableCollection<PumpModel>()
            //{
            //    new PumpModel(10000)
                
            //};
            //_stationid = (Int64)stationid;
        }

        private void DoStationStat(object ignore)
        {
            try
            {

                List<TankModel> tankList = new List<TankModel>(this._CollecTank);
                foreach (var tank in tankList)
                {
                    tank.TankPumpTotal = BusinessHelper.FindPumpTotalByTankId(tank.TankId);
                    
                }
                ViewModelStationStat vmStationStat = new ViewModelStationStat(tankList,ClientPub);
                StationStatWindow stationStatWnd = new StationStatWindow();
                stationStatWnd.DataContext = vmStationStat;
                stationStatWnd.WindowState = WindowState.Maximized;
                stationStatWnd.Show();
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
            }

        }

        public ObservableCollection<Model.TankModel> getTankList(long stationid)
        {
            DataTable table = BusinessHelper.ListAllTank(stationid);
            ObservableCollection<Model.TankModel> _FuelTanks = new ObservableCollection<Model.TankModel>();
            try
            {
                if (table.Rows.Count != 0)
                {

                    foreach (DataRow _datarow in table.Rows)
                    {
                        TankModel _tm = new TankModel();
                        
                        _tm.TankId = Convert.ToInt64(_datarow["tankid"]);
                        _tm.Name = _datarow["tankname"].ToString();
                        _tm.Description = _datarow["tankdesc"].ToString();
                        _tm.StationId = (UInt64)stationid;
                        _tm.StationName = _datarow["stationname"].ToString();
                        _tm.MaxCapacity = Convert.ToDouble(_datarow["capacity"]);
                        _tm.OffsetLevel = Convert.ToDouble(_datarow["offset"]);
                        _tm.FuelId = Convert.ToInt64(_datarow["fuelid"]);
                        _tm.Capacity = 0;
                        _tm.FuelLevel = 0;
                        _tm.WaterLevel = 0;
                        _tm.Thermometer = 25.00;
                        _tm.WaterCapacity = 0;
                        _tm.IsImportFinished = true;
                        _tm.HasData = false;
                        _tm.SavedDataCommand = this.SaveDataCommand;
                        _tm.DetailCommand = this.DetailCommand;
                        _tm.FuelImportCommand = this.FuelImportCommand;
                        _tm.FinishCommand = this.FinishCommand;
                        _FuelTanks.Add(_tm);

                    }
                }
                else
                {
                    MessageBox.Show("Declare Param First!!!");
                    return null;
                }
                return _FuelTanks;
            }
         
            catch (Exception ex)
            {
                
                App.Log.LogException(ex);
                return null;
            }
            
        }

        private void MqttClientInit()
        {
            try
            {
                DataTable tblServer = BusinessHelper.GetServerInfo();
                foreach (DataRow _server in tblServer.Rows)
                {
                    _HostMqtt = _server["hostname"].ToString();
                    _HostPort = Convert.ToUInt16(_server["hostport"]);
                    _MqttUser = _server["username"].ToString();
                    _MqttPwd = FLS.Helper.CryptorEngine.Decrypt(_server["password"].ToString(), true);
                    
                }
                App.Log.LogInfoMessage(string.Format (@"Host: {0}, Port: {1}, User: {2}",_HostMqtt,_HostPort,_MqttUser));
                clientSub = new MqttClient(_HostMqtt, _HostPort, false, null, null, MqttSslProtocols.None);
                ClientPub = new MqttClient(_HostMqtt, _HostPort, false, null, null, MqttSslProtocols.None);
                
                clientSub.MqttMsgPublishReceived += clientSub_MqttMsgPublishReceived;

                string subClientId = Guid.NewGuid().ToString();
                string pubClientId = Guid.NewGuid().ToString();

                clientSub.Connect(subClientId, _MqttUser, _MqttPwd);
                ClientPub.Connect(pubClientId, _MqttUser, _MqttPwd);

                String s = @"{""status"":0}";
                ClientPub.Publish("status", Encoding.UTF8.GetBytes(s));

            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
        }

        public void MqttClientDisconnect()
        {
            try
            {
                if ((clientSub != null) && (clientSub.IsConnected))
                {
                    clientSub.Disconnect();
                }
                if ((ClientPub != null) && (ClientPub.IsConnected))
                {
                    ClientPub.Disconnect();
                }
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
        }
        public void SubscribeStation(long stationid)
        {
            try
            {
                if (clientSub.IsConnected)
                {
                    // subscribe to the topic "/home/temperature" with QoS 2
                    string topic = string.Format(@"/S{0}/#", stationid);
                    clientSub.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

                }
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
        }
        private void SubscribeTank(long stationid,long tankid)
        {
            try
            {
                if (clientSub.IsConnected)
                {
                    // subscribe to the topic "/home/temperature" with QoS 2
                    string topic = string.Format(@"/S{0}/T{1}/", stationid,tankid);
                    clientSub.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

                }
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }

        }
        private void clientSub_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Topic);
                string data = System.Text.UTF8Encoding.UTF8.GetString(e.Message);
                Console.WriteLine(data);
                string[] topic = e.Topic.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                Int64 recvTankId = Convert.ToInt64(topic[1].Substring(1));
                App.Log.LogDebugMessage (String.Format (@"Received Tank Id: {0}", recvTankId));
                JObject o = JObject.Parse(data);
                UInt64 _level = Convert.ToUInt64(o["fuel_level"])/100;
                UInt64 _wlevel = Convert.ToUInt64(o["water_level"])/100;
                UInt16 _thermo = Convert.ToUInt16(o["thermo"]);



                foreach (var _tank in CollecTank)
                {
                    if (_tank.TankId == recvTankId)
                    {
                        _tank.FuelLevel = (Double)(_level) - _tank.OffsetLevel;
                        _tank.Capacity = FuelInterpolation(_tank.FuelLevel, recvTankId);
                        _tank.WaterLevel = _wlevel;
                        _tank.WaterCapacity = FuelInterpolation((Double)_wlevel , recvTankId);
                        _tank.Thermometer = (Double)_thermo / 100;
                        if (!_tank.HasData)
                        {
                            _tank.HasData = true;
                            DataTable _dtTicket = BusinessHelper.FindLastTicketByTankId(_tank.TankId);
                            if (_dtTicket != null)
                            {
                                DataRow _dr = _dtTicket.Rows[0];
                                Double _endcapacity = Convert.ToDouble(_dr["endcapacity"]);
                                Double _endlevel = Convert.ToDouble(_dr["endlevel"]);
                                App.Log.LogDebugMessage(String.Format(@"Cap-Level: {0}-{1}", _endcapacity, _endlevel));
                                if ((_endcapacity == 0) && (_endlevel == 0))
                                {
                                    _tank.IsImportFinished = false;
                                    App.Log.LogDebugMessage(String.Format(@"Tank {0} doesn't finish importing!!!", _tank.TankId));
                                }
                                
                            }
                            
                        }
                        App.Log.LogDebugMessage (String.Format (_tank.CapacityText));
                        App.Log.LogDebugMessage(String.Format(_tank.WaterCapacityText));
                        App.Log.LogDebugMessage(String.Format(_tank.ThermoText));
                        break;
                    }
                }
                ObservableCollection<Model.TankModel> _FuelTanks = new ObservableCollection<Model.TankModel>(CollecTank);
                if (CollecTank != null)
                {
                    
                    CollecTank = null;
                    
                }

                CollecTank = _FuelTanks;                

                
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
        }
        static public double linear(double x, double x0, double x1, double y0, double y1)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }
        private double FuelInterpolation(double val,long tankId)
        {
            Double max_min = Double.MaxValue, min_max = 0;
            Double y0 = 0, y1 = min_max;
            Double ret = 0;
            try
            {
                DataTable dt = BusinessHelper.LoadInterpolation(tankId);
                var Rows = (from row in dt.AsEnumerable()
                            orderby row["in_level"] descending
                            select row);
                DataTable dataTableDesc = Rows.AsDataView().ToTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow _row = dt.Rows[i];
                    Double d = Convert.ToDouble(_row["in_level"]);
                    if (val < d)
                    {
                        min_max = Convert.ToDouble(_row["in_level"]);
                        y1 = Convert.ToDouble(_row["out_cap"]);
                        break;
                    }
                }
                for (int i = 0; i < dataTableDesc.Rows.Count; i++)
                {
                    DataRow _row = dataTableDesc.Rows[i];
                    Double d = Convert.ToDouble(_row["in_level"]);
                    if (val >= d)
                    {
                        max_min = Convert.ToDouble(_row["in_level"]);
                        y0 = Convert.ToDouble(_row["out_cap"]);
                        break;
                    }
                }
                //foreach (DataRow _row in dt.Rows)
                //{
                //    Double lv = Convert.ToDouble(_row["in_level"]);
                //    if ((lv < val) && (max_min < lv))
                //    {
                //        max_min = lv;
                //        y0 = Convert.ToDouble(_row["out_cap"]);
                //    }
                //    if ((lv > val) && (min_max > lv))
                //    {
                //        min_max = lv;
                //        y1 = Convert.ToDouble(_row["out_cap"]);
                //    }

                //}
                 ret = linear(val, max_min, min_max, y0, y1);
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
            return ret;
        }
    }
}
