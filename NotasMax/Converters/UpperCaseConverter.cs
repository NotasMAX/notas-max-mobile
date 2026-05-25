using System.Globalization;

namespace NotasMax.Converters
{
    public class UpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                string dayOfWeek = dateTime.ToString("ddd", culture);
                return char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1).ToLower();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
