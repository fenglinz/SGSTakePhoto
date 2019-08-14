using SGSTakePhoto.Infrastructure;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// UploadModule.xaml 的交互逻辑
    /// </summary>
    public partial class UploadModule : UserControl
    {
        private Order order;

        /// <summary>
        /// 
        /// </summary>
        public UploadModule(Order order)
        {
            InitializeComponent();
            this.order = order;
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
