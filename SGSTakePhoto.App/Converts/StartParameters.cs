using System;
using System.Windows.Data;

namespace SGSTakePhoto.App
{
    public class StartParameters : IValueConverter//此接口有以下两个方法
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || (string)value == "")
                return "";

            return "[参数 " + (string)value + "]";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
