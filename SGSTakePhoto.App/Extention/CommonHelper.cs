using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 获取当前应用程序的路径
        /// </summary>
        public static string RootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photo_Files");

        /// <summary>
        /// 当前用户正在使用的系统
        /// </summary>
        public static string CurrentSystem { get; set; }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static string CurrentUser { get; set; }

        /// <summary>
        /// 当前正在使用的容器
        /// </summary>
        public static MainWindow MainWindow { get; set; }
        public static string SourcePath { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, UserControl> UserControls = new Dictionary<string, UserControl>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string CurrentUploadPath(string status)
        {
            switch (status)
            {
                case "Original":
                    break;
                case "Before":
                    break;
                case "During":
                    break;
                case "After":
                    break;
                case "Feature":
                    break;
                default:

                    break;
            }

            return string.Empty;
        }
    }
}
