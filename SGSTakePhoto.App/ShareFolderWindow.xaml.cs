using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// ShareFolderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShareFolderWindow : UserControl
    {
        public ShareFolderWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgShareFolder.SelectedItem as Order;
            if (order == null)
            {

            }

            if (!App.UserControls.ContainsKey("ShareFolderOrder"))
            {
                ShareFolderModule shareFolderOrder = new ShareFolderModule(order);
                App.CurrentWindow.brMain.Child = shareFolderOrder;
                App.UserControls.Add("ShareFolderOrder", shareFolderOrder);
            }
            else
            {
                App.CurrentWindow.brMain.Child = App.UserControls["ShareFolderOrder"];
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 条形码或二维码扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScan_Click(object sender, RoutedEventArgs e)
        {
            ScanWindow scan = new ScanWindow { Owner = App.CurrentWindow };
            //如果是激活状态则返回
            if (scan.IsClosed) return;
            if (scan.ShowDialog() == false)
            {
                TextBox txtBox = (sender as TextBox);
                txtBox.Text = scan.BarCode;
            }
        }

        /// <summary>
        /// 照片上传查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgShareFolder.SelectedItem as Order;
            if (order == null)
            {

            }

            if (!App.UserControls.ContainsKey("Upload"))
            {
                UploadModule uploadModule = new UploadModule { Order = order, ParentControl = this };
                App.CurrentWindow.brMain.Child = uploadModule;
                App.UserControls.Add("Upload", uploadModule);
            }
            else
            {
                App.CurrentWindow.brMain.Child = App.UserControls["Upload"];
            }
        }

        /// <summary>
        /// 照片预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowser_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgShareFolder.SelectedItem as Order;
            if (order == null)
            {

            }

            if (!App.UserControls.ContainsKey("Browser"))
            {
                BrowserModule browserModule = new BrowserModule { Order = order, ParentControl = this };
                App.CurrentWindow.brMain.Child = browserModule;
                App.UserControls.Add("Browser", browserModule);
            }
            else
            {
                App.CurrentWindow.brMain.Child = App.UserControls["Browser"];
            }
        }
    }
}
