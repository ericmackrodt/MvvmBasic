using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MVVMBasic.Converters
{
    public class EmptyListToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && (string)parameter == "Not")
                return value != null && ((IEnumerable<object>)value).Any() ? Visibility.Collapsed : Visibility.Visible;
            else
                return value != null && ((IEnumerable<object>)value).Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
