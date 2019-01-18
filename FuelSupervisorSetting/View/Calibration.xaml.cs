using Microsoft.Win32;
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

namespace FuelSupervisorSetting.View
{
    /// <summary>
    /// Interaction logic for Calibration.xaml
    /// </summary>
    public partial class Calibration : UserControl
    {
        public Calibration()
        {
            InitializeComponent();
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.DefaultExt = ".xlsx";
                openfile.Filter = "(.xlsx)|*.xlsx";

                var browsefile = openfile.ShowDialog();

                if (browsefile == true)
                {

                    ((ObjectDataProvider)this.FindResource("ImportIntoTab")).MethodParameters[1] = openfile.FileName;
                    //((ObjectDataProvider)this.FindResource("CalibrationTab")).Refresh();
                    ((ObjectDataProvider)this.FindResource("ImportIntoTab")).MethodParameters[1] = String.Empty;
                    ((ObjectDataProvider)this.FindResource("LoadCalibrationTab")).Refresh();
                }
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
                throw;
            }

        }



        private void cbSelTank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (sender) as ComboBox;
            if (cb.SelectedValue != null)
            {
                if (!this.btnImport.IsEnabled) this.btnImport.IsEnabled = true;

            }
            else
            {
                if (this.btnImport.IsEnabled) this.btnImport.IsEnabled = false;
            }

        }
    }
}
