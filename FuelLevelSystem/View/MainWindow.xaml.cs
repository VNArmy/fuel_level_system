
using FuelLevelSystem.View;
using FuelLevelSystem.ViewModel;
using MahApps.Metro.Controls;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuelLevelSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow 
    {
        
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                this.DataContext = new ViewModelMainWindow();
            }
            catch (Exception err)
            {
                App.Log.LogException(err);
                
            }
            
        }

       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var vm = this.DataContext as ViewModelMainWindow;
            vm.MqttClientDisconnect();
            if (vm.CollecStation != null)
            {
                vm.CollecStation.Clear();
            }
            if (vm.CollecTank != null)
            {
                vm.CollecTank.Clear();
            }
            
        }
              
    }
}
