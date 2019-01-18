using FuelSupervisorSetting.Model;
using FuelSupervisorSetting.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuelSupervisorSetting.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
            this.LoginLayer.DataContext = viewModel;
        }
        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            LoginLayer.Visibility = Visibility.Visible;
        }
        private void MenuItem_Click2a(object sender, RoutedEventArgs e)
        {
            Stage.Child = new StationControl();
        }

        private void MenuItem_Click2b(object sender, RoutedEventArgs e)
        {
            Stage.Child = new TankListControl();
            
        }

        //private void Login_Click(object sender, RoutedEventArgs e)
        //{
        //    LoginLayer.Visibility = Model.Authentication.Authenticate1(txtName.Text, txtPassword.Text) ? Visibility.Collapsed : Visibility.Visible;
        //}

        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void MenuItem_Click2c(object sender, RoutedEventArgs e)
        {
            Stage.Child = new Interpolation();

        }
        private void MenuItem_Click2e(object sender, RoutedEventArgs e)
        {
            Stage.Child = new Calibration();

        }
        protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
        {

            return null;
        }

        private void MenuItem_Click2d(object sender, RoutedEventArgs e)
        {
            Stage.Child = new MqttServer();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Stage.Child = new PumpListControl();
        }
              

    }
}
