using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MVVMBasic.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
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

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
