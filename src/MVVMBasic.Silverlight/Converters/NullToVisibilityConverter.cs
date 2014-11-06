using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MVVMBasic.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                if (parameter != null && parameter as string == "Not")
                    return value != null && !string.IsNullOrWhiteSpace((string)value) ? Visibility.Visible : Visibility.Collapsed;
                else
                    return value == null || string.IsNullOrWhiteSpace((string)value) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                if (parameter != null && parameter as string == "Not")
                    return value != null ? Visibility.Visible : Visibility.Collapsed;
                else
                    return value == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
