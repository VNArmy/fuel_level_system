using FuelSupervisorSetting.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MqttServer.xaml
    /// </summary>
    public partial class MqttServer : UserControl
    {
        public MqttServer()
        {
            InitializeComponent();
            this.DataContext = new MqttServerViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.passwordBox.Clear();
            this.confirmPwd.Clear();
        }
    }
}
