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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuelSupervisorSetting.View
{
    /// <summary>
    /// Interaction logic for Interpolation.xaml
    /// </summary>
    public partial class Interpolation : UserControl
    {
        //private ViewModel.ViewModelInterpolation m_ViewModel;
        public Interpolation()
        {
            InitializeComponent();
            //this.DataContext = new ViewModel.ViewModelInterpolation();
            //m_ViewModel = new ViewModel.ViewModelInterpolation();
            //this.DataContext = m_ViewModel;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            //string f = @"D:\vnarmy\Documents\Book1.xlsx";
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();
 
            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                //this.txtFileName.Text = openfile.FileName;
                ((ObjectDataProvider)this.FindResource("ImportIntoTab")).MethodParameters[1] = openfile.FileName;
                //((ObjectDataProvider)this.FindResource("InterpolationTab")).Refresh();
                ((ObjectDataProvider)this.FindResource("ImportIntoTab")).MethodParameters[1] = String.Empty;
                ((ObjectDataProvider)this.FindResource("LoadInterpolationTab")).Refresh();
            }
        }

 

        private void cbSelTank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (sender) as ComboBox;
            if (cb.SelectedValue!=null)
            {
                if (!this.btnImport.IsEnabled) this.btnImport.IsEnabled = true;
                
            }
            else
            {
                if (this.btnImport.IsEnabled) this.btnImport.IsEnabled = false;
            }
            
        }

       

        //private void txtFileName_TargetUpdated(object sender, DataTransferEventArgs e)
        //{
        //    this.txtFileName.Text = String.Empty;
        //}

        //private void InterpolationGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    this.DataContext = m_ViewModel;
        //}

        //private void InterpolationGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (InterpolationGrid.SelectedItem == null)
        //    {
        //        IDColumn.IsReadOnly = true;
        //    }
        //    else
        //    {
        //        IDColumn.IsReadOnly = !InterpolationGrid.SelectedItem.Equals(CollectionView.NewItemPlaceholder);
        //    }
        //}
    }
}
