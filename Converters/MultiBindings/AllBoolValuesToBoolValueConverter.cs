using System;
using System.Globalization;
using System.Windows.Data;

namespace DatabaseExample.Converters.MultiBindings
{
    public class AllBoolValuesToBoolValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                return false;
            }

            bool resume = true;

            for (int i = 0; i < values.Length; i++)
            {
                if (!(values[i] is bool))
                {
                    resume = false;
                    break;
                }
            }

            if (!resume)
                return false;

            for (int i = 0; i < values.Length; i++)
                if (!(bool)values[i])
                    return false;

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
