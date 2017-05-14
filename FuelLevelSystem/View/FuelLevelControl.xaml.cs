
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

namespace FuelLevelSystem.View.FuelLevelUserControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FuelLevelControl : UserControl
    {
        /// <summary>
        /// Gets or sets the Description which is displayed next to the field
        /// </summary>
        public String Description
        {
            get { return (String)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        /// <summary>
        /// Identified the Description dependency property
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string),
              typeof(FuelLevelControl), new PropertyMetadata(""));

        /// <summary>
        /// Gets or sets the LevelText which is displayed next to the field
        /// </summary>
        public String LevelText
        {
            get { return (String)GetValue(LevelTextProperty); }
            set { SetValue(LevelTextProperty, value); }
        }

        /// <summary>
        /// Identified the LevelText dependency property
        /// </summary>
        public static readonly DependencyProperty LevelTextProperty =
            DependencyProperty.Register("LevelText", typeof(string),
              typeof(FuelLevelControl), new PropertyMetadata("0.00"));

        /// <summary>
        /// Gets or sets the CapacityText which is displayed next to the field
        /// </summary>
        public String CapacityText
        {
            get { return (String)GetValue(CapacityTextProperty); }
            set { SetValue(CapacityTextProperty, value); }
        }

        /// <summary>
        /// Identified the CapacityText dependency property
        /// </summary>
        public static readonly DependencyProperty CapacityTextProperty =
            DependencyProperty.Register("CapacityText", typeof(string),
              typeof(FuelLevelControl), new PropertyMetadata("0.00"));

        /// <summary>
        /// Gets or sets the WaterCapacityText which is displayed next to the field
        /// </summary>
        public String WaterCapacityText
        {
            get { return (String)GetValue(WaterCapacityTextProperty); }
            set { SetValue(WaterCapacityTextProperty, value); }
        }

        /// <summary>
        /// Identified the WaterCapacityText dependency property
        /// </summary>
        public static readonly DependencyProperty WaterCapacityTextProperty =
            DependencyProperty.Register("WaterCapacityText", typeof(string),
              typeof(FuelLevelControl), new PropertyMetadata("0.00"));

        /// <summary>
        /// Gets or sets the WaterLevelText which is displayed next to the field
        /// </summary>
        public String WaterLevelText
        {
            get { return (String)GetValue(WaterLevelTextProperty); }
            set { SetValue(WaterLevelTextProperty, value); }
        }

        /// <summary>
        /// Identified the WaterLevelText dependency property
        /// </summary>
        public static readonly DependencyProperty WaterLevelTextProperty =
            DependencyProperty.Register("WaterLevelText", typeof(string),
              typeof(FuelLevelControl), new PropertyMetadata("0.00"));


        /// <summary>
        /// Gets or sets the ThermoText which is displayed next to the field
        /// </summary>
        public String ThermoText
        {
            get { return (String)GetValue(ThermoTextProperty); }
            set { SetValue(ThermoTextProperty, value); }
        }

        /// <summary>
        /// Identified the ThermoText dependency property
        /// </summary>
        public static readonly DependencyProperty ThermoTextProperty =
            DependencyProperty.Register("ThermoText", typeof(string),
              typeof(FuelLevelControl), new PropertyMetadata("0.00"));

        /// <summary>
        /// Gets or sets the ThermoText which is displayed next to the field
        /// </summary>
        public Double Capacity
        {
            get { return (Double)GetValue(CapacityProperty); }
            set { SetValue(CapacityProperty, value); }
        }

        /// <summary>
        /// Identified the ThermoText dependency property
        /// </summary>
        public static readonly DependencyProperty CapacityProperty =
            DependencyProperty.Register("Capacity", typeof(Double),
              typeof(FuelLevelControl), new PropertyMetadata( 0.00));

        /// <summary>
        /// Gets or sets the ThermoText which is displayed next to the field
        /// </summary>
        public Double WaterCapacity
        {
            get { return (Double)GetValue(WaterCapacityProperty); }
            set { SetValue(WaterCapacityProperty, value); }
        }

        /// <summary>
        /// Identified the ThermoText dependency property
        /// </summary>
        public static readonly DependencyProperty WaterCapacityProperty =
            DependencyProperty.Register("WaterCapacity", typeof(Double),
              typeof(FuelLevelControl), new PropertyMetadata(0.00));

        /// <summary>
        /// Gets or sets the MaxCapacity which is displayed next to the field
        /// </summary>
        public Double MaxCapacity
        {
            get { return (Double)GetValue(MaxCapacityProperty); }
            set { SetValue(MaxCapacityProperty, value); }
        }

        /// <summary>
        /// Identified the MaxCapacity dependency property
        /// </summary>
        public static readonly DependencyProperty MaxCapacityProperty =
            DependencyProperty.Register("MaxCapacity", typeof(Double),
              typeof(FuelLevelControl), new PropertyMetadata( 10000.00));



        /// <summary>
        /// Gets or sets the SaveCmd which is displayed next to the field
        /// </summary>
        public ICommand ImportCmd
        {
            get { return (ICommand)GetValue(ImportCmdProperty); }
            set { SetValue(ImportCmdProperty, value); }
        }

        /// <summary>
        /// Identified the CapacityRange dependency property
        /// </summary>
        public static readonly DependencyProperty ImportCmdProperty =
            DependencyProperty.Register("ImportCmd", typeof(ICommand),
              typeof(FuelLevelControl), new PropertyMetadata(null));
        /// <summary>
        /// Gets or sets the SaveCmd which is displayed next to the field
        /// </summary>
        public ICommand DetailCmd
        {
            get { return (ICommand)GetValue(DetailCmdProperty); }
            set { SetValue(DetailCmdProperty, value); }
        }

        /// <summary>
        /// Identified the CapacityRange dependency property
        /// </summary>
        public static readonly DependencyProperty DetailCmdProperty =
            DependencyProperty.Register("DetailCmd", typeof(ICommand),
              typeof(FuelLevelControl), new PropertyMetadata(null));
        /// <summary>
        /// Gets or sets the SaveCmd which is displayed next to the field
        /// </summary>
        public ICommand SaveCmd
        {
            get { return (ICommand)GetValue(SaveCmdProperty); }
            set { SetValue(SaveCmdProperty, value); }
        }

        /// <summary>
        /// Identified the CapacityRange dependency property
        /// </summary>
        public static readonly DependencyProperty SaveCmdProperty =
            DependencyProperty.Register("SaveCmd", typeof(ICommand),
              typeof(FuelLevelControl), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the SaveCmd which is displayed next to the field
        /// </summary>
        public ICommand FinishCmd
        {
            get { return (ICommand)GetValue(FinishCmdProperty); }
            set { SetValue(FinishCmdProperty, value); }
        }

        /// <summary>
        /// Identified the CapacityRange dependency property
        /// </summary>
        public static readonly DependencyProperty FinishCmdProperty =
            DependencyProperty.Register("FinishCmd", typeof(ICommand),
              typeof(FuelLevelControl), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the TankId which is displayed next to the field
        /// </summary>
        public UInt64 CmdParam
        {
            get { return (UInt64)GetValue(CmdParamProperty); }
            set { SetValue(CmdParamProperty, value); }
        }

        /// <summary>
        /// Identified the TankId dependency property
        /// </summary>
        public static readonly DependencyProperty CmdParamProperty =
            DependencyProperty.Register("CmdParam", typeof(UInt64),
              typeof(FuelLevelControl), new PropertyMetadata((UInt64)0));
        
        public FuelLevelControl()
        {
            InitializeComponent();
        }

        
    }
}
