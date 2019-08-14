using System;
using System.Globalization;
using System.Windows.Data;
using SGSTakePhoto.Infrastructure;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 传输类型转换
    /// </summary>
    public class TransferTypeConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransferType s = (TransferType)value;
            return s == (TransferType)int.Parse(parameter.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if (!isChecked)
            {
                return null;
            }
            return (TransferType)int.Parse(parameter.ToString());
        }
    }
}
