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
    /// Interaction logic for TankList.xaml
    /// </summary>
    public partial class TankListControl : UserControl
    {
        public TankListControl()
        {
            try
            {
                InitializeComponent();
                this.DataContext = new ViewModel.ViewModelTankList();
            }
            catch (Exception exception)
            {
                App.Log.LogException(exception);
                
            }
           
        }
        
    }
}
