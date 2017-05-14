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
        public RelayCommand<UInt64> LoadStationTanks { get; set; }

        public RelayCommand<UInt64> SaveDataCommand { get; set; }
        public RelayCommand<UInt64> DetailCommand { get; set; }
        public RelayCommand<UInt64> FuelImportCommand { get; set; }
        public RelayCommand<UInt64> FinishCommand { get; set; }

                   

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
        public ViewModelMainWindow()
        {
            try
            {
                //AuthVM = new AuthenticationViewModel();

                //LoginCommand = new RelayCommand(DoLogin);
                BusinessHelper.InitConnection();

                LoadStationTanks = new RelayCommand<UInt64>(getCurrentTankList);
                SaveDataCommand = new RelayCommand<UInt64>(DoSaveData, CanSaveData);
                DetailCommand = new RelayCommand<UInt64>(DoTankDetails);
                FuelImportCommand = new RelayCommand<UInt64>(DoFuelImport, CanFuelImport);
                FinishCommand = new RelayCommand<UInt64>(DoImportFinish, CanFinishImport);
                CollecStation = getAllStation();
                MqttClientInit();
            }
            catch (Exception ex)
            {

                App.Log.LogException(ex);
            }
            
            
        }
        //private void DoLogin()
        //{
        //    AuthVM.Authenticate();
        //}

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
            var selTank = (from _tank in CollecTank
                           where _tank.TankId == (long) tankId
                           select _tank).FirstOrDefault() as TankModel;
            if (selTank == null)
            {
                return false;
            }
            return (selTank.IsImportFinish && selTank.HasData);
        }
        private void DoFuelImport(UInt64 tankId)
        {
            var selTank = (from _tank in CollecTank
                           where _tank.TankId == (long)tankId
                           select _tank).FirstOrDefault() as TankModel;
            DateTime _now = DateTime.Now;

            try
            {
                BusinessHelper.SaveTankData(selTank.TankId, selTank.Capacity,selTank.Thermometer,selTank.WaterCapacity, (Byte)SaveDataStatus.IMPORT, _now);
                selTank.IsImportFinish = false;

            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
            }
        }
        private bool CanFuelImport(UInt64 tankId)
        {
            var selTank = (from _tank in CollecTank
                           where _tank.TankId == (long)tankId
                           select _tank).FirstOrDefault() as TankModel;
            if (selTank == null)
            {
                return true;
            }
            return selTank.IsImportFinish;
        }

        private void DoImportFinish(UInt64 tankId)
        {
            var selTank = (from _tank in CollecTank
                           where _tank.TankId ==(long) tankId
                           select _tank).FirstOrDefault() as TankModel;
            DateTime _now = DateTime.Now;

            try
            {
                BusinessHelper.SaveTankData(selTank.TankId, selTank.Capacity,selTank.Thermometer,selTank.WaterCapacity, (Byte)SaveDataStatus.SAVE, _now);
                selTank.IsImportFinish = true;
                DataTable dt = BusinessHelper.GetTankList(selTank.TankId);
                foreach (DataRow _dr in dt.Rows)
                {
                    long tankid = Convert.ToInt64(_dr["tankid"]);
                    BusinessHelper.UpdateTankStatus(tankid);
                }
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
                return (!selTank.IsImportFinish);
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
                        _tm.StationName = _datarow["stationname"].ToString();
                        _tm.MaxCapacity = Convert.ToDouble(_datarow["capacity"]);
                        _tm.OffsetLevel = Convert.ToDouble(_datarow["offset"]);
                        _tm.Capacity = 0;
                        _tm.FuelLevel = 0;
                        _tm.WaterLevel = 0;
                        _tm.Thermometer = 25.00;
                        _tm.WaterCapacity = 0;
                        _tm.IsImportFinish = true;
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
                clientSub.MqttMsgPublishReceived += clientSub_MqttMsgPublishReceived;

                string clientId = Guid.NewGuid().ToString();

                clientSub.Connect(clientId, _MqttUser, _MqttPwd);
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
        public void SubscribeTank(long stationid,long tankid)
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
        private void clientSub_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
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

                //var recvTank = (from _tank in CollecTank
                //                where _tank.TankId == recvTankId
                //                select _tank).FirstOrDefault() as FuelTank;
                //Console.WriteLine(@"Fuel: {0}; Water: {1}, Thermo: {2}", _level, _wlevel, _thermo);

                //recvTank.Capacity = FuelInterpolation((Double)_level);

                //recvTank.Thermometer = (Double)_thermo / 100;

                
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
