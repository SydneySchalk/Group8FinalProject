using System;
using System.Globalization;

namespace Final.Converters
{
    public class BoolToObtainedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool obtained)
            {
                return obtained ? "Obtained" : "Not Obtained";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
