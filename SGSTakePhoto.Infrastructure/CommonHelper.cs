using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 当前软件版本
        /// </summary>
        public const double AppVersion = 1.1;

        /// <summary>
        /// 获取当前应用程序的路径
        /// </summary>
        public static string RootPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 程序升级目录
        /// </summary>
        public static string SourcePath
        {
            get
            {
                return Path.Combine(RootPath, "Upgrade");
            }
        }

        /// <summary>
        /// 图片保存目录
        /// </summary>
        public static string UploadPath
        {
            get
            {
                return Path.Combine(RootPath, "Photo_Files");
            }
        }

        /// <summary>
        /// 照片上传的路径
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string CurrentUploadPath(Order entity)
        {
            string type = entity.PhotoType[0].ToString();
            switch (entity.ExecutionSystem)
            {
                case "OTS":
                    switch (entity.PhotoType)
                    {
                        case "Original":
                            return Path.Combine(UploadPath, entity.Owner, DateTime.Now.ToString("yyyy-MM-dd"), entity.CaseNum, type);
                        default:
                            return Path.Combine(UploadPath, entity.Owner, DateTime.Now.ToString("yyyy-MM-dd"), entity.JobNum, type);
                    }
                case "SLIM":
                    return Path.Combine(UploadPath, entity.Owner, DateTime.Now.ToString("yyyy-MM-dd"), entity.OrderNum, type);
                case "Share":
                default:
                    return Path.Combine(UploadPath, entity.Owner, DateTime.Now.ToString("yyyy-MM-dd"), entity.JobNum, type);
            }
        }

        /// <summary>
        /// 生成照片名称
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static string GeneralPhotoName(Order entity)
        {
            string type = entity.PhotoType[0].ToString();
            string dirPath = CurrentUploadPath(entity);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileName, orderId = GeneralOrderNum(dirPath);
            switch (entity.ExecutionSystem)
            {
                case "OTS":
                    switch (entity.PhotoType)
                    {
                        case "Original":
                            fileName = string.Format("{0}_O_{1}.jpg", IsNullOrEmpty(entity.CaseNum), orderId);
                            break;
                        default:
                            fileName = string.Format("{0}_{1}_{2}_{3}_{4}.jpg", IsNullOrEmpty(entity.JobNum), IsNullOrEmpty(entity.CaseNum), IsNullOrEmpty(entity.SampleID), type, orderId);
                            break;
                    }
                    break;
                case "SLIM":
                    fileName = string.Format("{0}.jpg", entity.SampleID);
                    break;
                case "Share":
                default:
                    fileName = string.Format("{0}_{1}_{2}_{3}_{4}.jpg", IsNullOrEmpty(entity.JobNum), IsNullOrEmpty(entity.CaseNum), IsNullOrEmpty(entity.SampleID), type, orderId);
                    break;
            }

            return fileName;
        }

        /// <summary>
        /// 如果某个Num的值为空则用N代替
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public static string IsNullOrEmpty(string orderNum)
        {
            if (string.IsNullOrEmpty(orderNum)) return "N";
            return orderNum;
        }

        /// <summary>
        /// 生成文件序号
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        private static string GeneralOrderNum(string dirPath)
        {
            int count = Directory.GetFiles(dirPath).Length;
            if (count >= 0 && count < 10)
            {
                return string.Format("00{0}", count + 1);
            }
            else if (count >= 10 && count < 100)
            {
                return string.Format("0{0}", count + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 删除确认
        /// </summary>
        /// <returns></returns>
        public static bool DeleteConfirm()
        {
            MessageBoxResult result = MessageBox.Show("Delete after confirmation", "Comfirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
                return true;

            return false;
        }

        /// <summary>
        /// 没有数据被选择
        /// </summary>
        public static void NoDataSelected()
        {
            MessageBox.Show("No Data Selected", "Error");
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="win"></param>
        /// <param name="action"></param>
        public static void AutoUpdate(Window win, Action<UpdateInfo> action)
        {
            try
            {
                HttpClient.PostAsync("http://47.106.120.125//Api/Upgrade/DataTransfer", DateTime.Now.Ticks.ToString(), "").ContinueWith((postResult) =>
                {
                    if (postResult.Exception != null) return;
                    HttpResult result = postResult.Result;
                    if (string.IsNullOrWhiteSpace(result.Html)) return;
                    UpdateInfo updateInfo = JsonConvert.DeserializeObject<UpdateInfo>(result.Html);
                    //如果版本号不相同或者要求强制升级
                    if (updateInfo.AppVersion != AppVersion || updateInfo.ForceUpgrade)
                    {
                        if (updateInfo.AppVersion == AppVersion) return;
                        win.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new Action(() => action.Invoke(updateInfo)));
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
