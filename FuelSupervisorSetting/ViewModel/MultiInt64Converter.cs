using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FuelSupervisorSetting.ViewModel
{
    //[ValueConversion(typeof(Int32), typeof(UInt64))]
    class MultiInt64Converter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            //return new object[] { value, value };
            if (value == null)
            {
                return new object[] {};

            }
            return new object[]
            {
                value,value
            };
        }
    }
}
