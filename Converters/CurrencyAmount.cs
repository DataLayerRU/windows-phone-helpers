using System;
using System.Windows.Data;

namespace WPDevelopmentLibs.Converters
{
    public class CurrencyAmount : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string val = value.ToString();
            float amount = 0;
            if (float.TryParse(val.Replace(".", ","), out amount))
            {
                val += parameter.ToString();
            }

            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
