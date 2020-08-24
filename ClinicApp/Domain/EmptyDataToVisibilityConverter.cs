using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ClinicApp.Domain
{
    class EmptyDataToVisibilityConverter : IValueConverter
    {
        // Прямое конвертирование
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility ReturnValue = Visibility.Visible;
            if (value == null) ReturnValue = Visibility.Collapsed;
            return ReturnValue;
        }

        // Обратное
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
