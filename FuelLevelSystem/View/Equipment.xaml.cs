using System;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace FuelLevelSystem.View
{
	/// <summary>
	/// Interaction logic for Equipment.xaml
	/// </summary>
	public partial class Equipment : UserControl, INotifyPropertyChanged
	{
		public Equipment()
		{
			InitializeComponent();
		}

		#region Min

		public double Min
		{
			get { return (double)GetValue(MinProperty); }
			set { SetValue(MinProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Min.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MinProperty =
			DependencyProperty.Register(
				"Min", typeof(double), typeof(Equipment),
				new UIPropertyMetadata(
					(s, e) =>
					{
						var me = (Equipment)s;
						me.OnPropertyChanged("ScaledValue");
                        me.OnPropertyChanged("ScaledWaterValue");
					}));

		#endregion

		#region Max

		public double Max
		{
			get { return (double)GetValue(MaxProperty); }
			set { SetValue(MaxProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MaxProperty =
			DependencyProperty.Register(
				"Max", typeof(double), typeof(Equipment),
				new UIPropertyMetadata(
					(s, e) =>
					{
						var me = (Equipment)s;
						me.OnPropertyChanged("ScaledValue");
                        me.OnPropertyChanged("ScaledWaterValue");
					}));

		#endregion

		#region CurrentLevel

		public double CurrentLevel
		{
			get { return (double)GetValue(CurrentLevelProperty); }
			set { SetValue(CurrentLevelProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CurrentLevel.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CurrentLevelProperty =
			DependencyProperty.Register(
				"CurrentLevel", typeof(double), typeof(Equipment),
				new UIPropertyMetadata(
					(s, e) =>
					{
						var me = (Equipment)s;
						me.OnPropertyChanged("ScaledValue");
					}));

		#endregion

        #region CurrentWaterLevel

        public double CurrentWaterLevel
        {
            get { return (double)GetValue(CurrentWaterLevelProperty); }
            set { SetValue(CurrentWaterLevelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentLevel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentWaterLevelProperty =
            DependencyProperty.Register(
                "CurrentWaterLevel", typeof(double), typeof(Equipment),
                new UIPropertyMetadata(
                    (s, e) =>
                    {
                        var me = (Equipment)s;
                        me.OnPropertyChanged("ScaledWaterValue");
                    }));

        #endregion
		public double ScaledValue
		{
			get
			{
				var scaledValue = CurrentLevel / (Max - Min) - (Min / (Max - Min));
				return scaledValue;
			}
		}

        public double ScaledWaterValue
        {
            get
            {
                var scaledWaterValue = CurrentWaterLevel / (Max - Min) - (Min / (Max - Min));
                return scaledWaterValue;
            }
        }

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
