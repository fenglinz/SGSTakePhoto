using SGSTakePhoto.Infrastructure;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// Update.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {
        #region 字段

        /// <summary>
        /// 升级信息
        /// </summary>
        private UpdateInfo updateInfo { get; set; }

        /// <summary>
        /// 是否开始升级
        /// </summary>
        public bool IsExsitUpgrade { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// Update
        /// </summary>
        public UpdateWindow(UpdateInfo updateInfo)
        {
            this.updateInfo = updateInfo;
            InitializeComponent();
            UpgradeGrid.DataContext = updateInfo;
        }

        #endregion

        #region OnClosing

        /// <summary>
        /// OnClosing
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            ///强制更新
            if (updateInfo.ForceUpgrade)
            {
                e.Cancel = true;
                MessageBox.Show(string.Format("必须更新到最新版本才能继续使用", updateInfo.AppVersion), "提示");
            }
            else
            {
                base.OnClosing(e);
            }
        }

        #endregion

        #region AutoUpgrade_Click

        /// <summary>
        /// AutoUpgrade_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoUpgrade_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btnAutoUpgrade":
                    btn.IsEnabled = false;
                    DownloadUpdate(updateInfo);
                    break;
                case "btnHandUpgrade":
                    //调用系统默认的浏览器   
                    System.Diagnostics.Process.Start(updateInfo.DownloadUrl);
                    updateInfo.ForceUpgrade = false;
                    IsExsitUpgrade = false;
                    break;
            }
        }

        #endregion

        #region DownloadUpdate

        /// <summary>
        /// DownloadUpdate
        /// </summary>
        /// <param name="updateInfo"></param>
        private void DownloadUpdate(UpdateInfo updateInfo)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string savePath = Path.Combine(CommonHelper.SourcePath, string.Format("{0}.zip", updateInfo.AppName));
                    client.DownloadFileAsync(new Uri(updateInfo.DownloadUrl), savePath);
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DownloadProgressChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pbDownloadProgress.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// DownloadFileCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                btnAutoUpgrade.IsEnabled = true;
                updateInfo.ForceUpgrade = false;
                IsExsitUpgrade = true;

                this.Close();
            }
            else
            {
                MessageBox.Show(e.Error.Message, "下载过程中发生错误");
            }
        }

        #endregion
    }
}
