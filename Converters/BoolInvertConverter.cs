using System;
using System.Globalization;
using System.Windows.Data;

namespace DatabaseExample.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;

            return Binding.DoNothing;
        }
    }
}
