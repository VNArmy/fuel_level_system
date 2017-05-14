
using FLS.Business;
using FuelSupervisorSetting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FuelSupervisorSetting.ViewModel
{
    class MqttServerViewModel : ViewModelBase
    {
        private readonly RelayCommand _saveCommand;
        string _serveraddress;
        int _serverport;
        string _userid;
        string _status;
        string _confirmpwd;

        public MqttServerViewModel()
        {
            BusinessHelper.InitConnection();
            _serverport = 1883;
            _saveCommand = new RelayCommand(DoSaveCommand, CanSave);
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand;}           
        }
        public String ServerAddress
        {
            get { return _serveraddress; }
            set { SetProperty(ref _serveraddress, value); SaveCommand.RaiseCanExecuteChanged(); }
        }
        public int ServerPort
        {
            get { return _serverport; }
            set { SetProperty(ref _serverport, value); SaveCommand.RaiseCanExecuteChanged(); }
        }
        public String UserId
        {
            get { return _userid; }
            set { SetProperty(ref _userid, value); SaveCommand.RaiseCanExecuteChanged(); }
        }
        public String Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        //public String ConfirmPassword
        //{
        //    get { return _confirmpwd; }
        //    set { SetProperty(ref _confirmpwd, value); }
        //}
        private void DoSaveCommand(object parameter)
        {
            var values = (object[])parameter;
            PasswordBox pwdBox = values[0] as PasswordBox;
            string clearPassword = pwdBox.Password;
            string clearConfirmPwd = (values[1] as PasswordBox).Password;
            if (!clearPassword.Equals(clearConfirmPwd))
            {
                Status = string.Format("Password not equal!!");
                return;
            }
            BusinessHelper.AddMqttServer(ServerAddress, ServerPort, UserId, FLS.Helper.CryptorEngine.Encrypt(clearPassword, true));
            Status = string.Format("Add server success!!");
            ServerAddress = string.Empty;
            ServerPort = 1883;
            UserId = string.Empty;
            values = null;
        }
        private bool CanSave(object parameter)
        {
            if (string.IsNullOrEmpty(ServerAddress) || (_serverport<0) || (_serverport>65535)
                || string.IsNullOrEmpty(_userid)
                //|| string.IsNullOrEmpty(_password)||string.IsNullOrEmpty(_confirmpwd)
                )            
                return false;
            return true;
        }
    }
}
