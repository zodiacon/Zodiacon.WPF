using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Zodiacon.WPF {
    public sealed class BooleanToVisibilityConverter : IValueConverter {
        public bool InvisibleAsHidden { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var visible = (value is bool && (bool)value) || (value is bool? && (bool?)value == true);
            return visible ? Visibility.Visible : (InvisibleAsHidden ? Visibility.Hidden : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (Visibility)value == Visibility.Visible;
        }
    }
}
