using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace SGSTakePhoto.App
{
    public class AutoNumberValueConverter : IMultiValueConverter
    {
        #region IMultiValueConverter 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = values[0];
            var items = values[1] as ItemCollection;

            var index = items.IndexOf(item);
            return (index).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
