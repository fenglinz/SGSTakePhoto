using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// UploadModule.xaml 的交互逻辑
    /// </summary>
    public partial class UploadModule : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<UploadFile> UploadFiles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UploadModule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public UploadModule(Order order)
        {
            InitializeComponent();
            this.Order = order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Upload_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Order == null) return;
            if (UploadFiles != null) return;

            UploadFiles = new ObservableCollection<UploadFile>
            {
                new UploadFile
                {
                    Id = Guid.NewGuid().ToString(),
                     FileName = "哈哈哈",
                     Status = "Uploading",
                     UploadTime = DateTime.Now
                }
            };

            dgUpload.ItemsSource = UploadFiles;
        }

        /// <summary>
        /// 返回到上次目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (CommonHelper.CurrentSystem)
            {
                case "OTS":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.otsModule;
                    break;
                case "SLIM":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.SlimModule;
                    break;
                case "Share":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.shareFolderModule;
                    break;
                case "Setting":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.settingModule;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
