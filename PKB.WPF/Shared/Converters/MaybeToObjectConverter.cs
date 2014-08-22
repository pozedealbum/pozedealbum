using PKB.Utility;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PKB.WPF.Shared.Converters
{
    [ValueConversion(typeof(IMaybe), typeof(object))]
    public class MaybeToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IMaybe)value).NullableValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null
                ? Activator.CreateInstance(targetType)
                : Activator.CreateInstance(targetType, new[] { value });
        }
    }
}