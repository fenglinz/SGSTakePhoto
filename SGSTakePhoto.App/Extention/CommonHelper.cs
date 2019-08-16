using SGSTakePhoto.Infrastructure;
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

        /// <summary>
        /// 
        /// </summary>
        public static string SourcePath { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, UserControl> UserControls = new Dictionary<string, UserControl>();

        /// <summary>
        /// 照片上传的路径
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string CurrentUploadPath(Order entity, string status)
        {
            string type = status[0].ToString();
            switch (CurrentSystem)
            {
                case "OTS":
                    switch (status)
                    {
                        case "Original":
                            return Path.Combine(RootPath, CurrentUser, DateTime.Now.ToString("yyyy-MM-dd"), entity.CaseNum, type);
                        default:
                            return Path.Combine(RootPath, CurrentUser, DateTime.Now.ToString("yyyy-MM-dd"), entity.JobNum, type);
                    }
                case "SLIM":
                    return Path.Combine(RootPath, CurrentUser, DateTime.Now.ToString("yyyy-MM-dd"), entity.OrderNum, type);
                case "Share":
                default:
                    return Path.Combine(RootPath, CurrentUser, DateTime.Now.ToString("yyyy-MM-dd"), entity.JobNum, type);
            }
        }

        /// <summary>
        /// 生成照片名称
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GeneralPhotoName(Order order, string status)
        {
            string type = status[0].ToString();
            string dirPath = CurrentUploadPath(order, status);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileName;
            switch (CurrentSystem)
            {
                case "OTS":
                    switch (status)
                    {
                        case "Original":
                            fileName = string.Format("{0}_O_{1}", order.CaseNum, order.Id);
                            break;
                        default:
                            fileName = string.Format("{0}_{1}_{2}_{3}_{4}", order.JobNum, order.CaseNum, order.SampleID, type, order.Id);
                            break;
                    }
                    break;
                case "SLIM":
                    fileName = order.SampleID;
                    break;
                case "Share":
                default:
                    fileName = string.Format("{0}_{1}_{2}_{3}_{4}", order.JobNum, order.CaseNum, order.SampleID, type, order.Id);
                    break;
            }

            return Path.Combine(dirPath, fileName);
        }
    }
}
