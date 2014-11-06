using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MVVMBasic.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && (string)parameter == "Not")
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            else
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && (string)parameter == "Not")
                return (Visibility)value != Visibility.Visible;
            else
                return (Visibility)value == Visibility.Visible;
        }
    }
}
