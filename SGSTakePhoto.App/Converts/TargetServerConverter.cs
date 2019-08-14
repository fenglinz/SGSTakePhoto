using System;
using System.Globalization;
using System.Windows.Data;

namespace SGSTakePhoto.App
{
    [ValueConversion(typeof(string), typeof(string))]
    public class TargetServerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a String");

            string name = value as string;
            if (string.IsNullOrEmpty(name))
            {
                return "New Teacher";
            }
            else
            {
                return name;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sourceName = (string)value;
            if (!string.IsNullOrEmpty(sourceName))
            {
                return sourceName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
