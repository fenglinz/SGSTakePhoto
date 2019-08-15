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

            if (!CommonHelper.UserControls.ContainsKey("ShareFolderOrder"))
            {
                ShareFolderModule shareFolderOrder = new ShareFolderModule(order);
                CommonHelper.MainWindow.brMain.Child = shareFolderOrder;
                CommonHelper.UserControls.Add("ShareFolderOrder", shareFolderOrder);
            }
            else
            {
                CommonHelper.MainWindow.brMain.Child = CommonHelper.UserControls["ShareFolderOrder"];
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
            ScanWindow scan = new ScanWindow { };
            //如果是激活状态则返回
            if (scan.IsClosed)
            {
                scan.Close();
            }
            else
            {
                if (scan.ShowDialog() == false)
                {
                    switch ((sender as TextBox).Name)
                    {
                        case "txtOrderNum":

                            break;
                        case "txtSampleId":

                            break;
                    }
                }
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

            if (!CommonHelper.UserControls.ContainsKey("Upload"))
            {
                UploadModule uploadModule = new UploadModule(order);
                CommonHelper.MainWindow.brMain.Child = uploadModule;
                CommonHelper.UserControls.Add("Upload", uploadModule);
            }
            else
            {
                CommonHelper.MainWindow.brMain.Child = CommonHelper.UserControls["Upload"];
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

            if (!CommonHelper.UserControls.ContainsKey("Browser"))
            {
                BrowserModule browserModule = new BrowserModule { Order = order, ParentControl = this };
                CommonHelper.MainWindow.brMain.Child = browserModule;
                CommonHelper.UserControls.Add("Browser", browserModule);
            }
            else
            {
                CommonHelper.MainWindow.brMain.Child = CommonHelper.UserControls["Browser"];
            }
        }
    }
}
