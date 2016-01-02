using System;
using System.Windows.Data;
using WPDevelopmentLibs.Helpers;

namespace WPDevelopmentLibs.Converters
{
    public class TimestampToDate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int val = (int)value;

            return DateHelpers.UnixTimeStampToDateTime(val).ToString("d");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateHelpers.DateTimeToUnitTimeStamp((DateTime)value);
        }
    }
}
