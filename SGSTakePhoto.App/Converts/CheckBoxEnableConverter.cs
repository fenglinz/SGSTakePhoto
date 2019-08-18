// 版权所有：
// 文 件  名：DateToString.cs
// 功能描述：
// 创建标识：Seven Song(m.sh.lin0328@163.com) 2014/1/19 13:28:35
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Data;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckBoxEnableConverter : IValueConverter//此接口有以下两个方法
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value <= 0 ? "False" : "True";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
