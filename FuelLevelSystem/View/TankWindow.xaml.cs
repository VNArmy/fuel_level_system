using FuelLevelSystem.ViewModel;
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
using System.Windows.Shapes;

namespace FuelLevelSystem.View
{
    /// <summary>
    /// Interaction logic for TankWindow.xaml
    /// </summary>
    public partial class TankWindow : Window
    {
        public TankWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModelTankWindow();
        }
    }
}
