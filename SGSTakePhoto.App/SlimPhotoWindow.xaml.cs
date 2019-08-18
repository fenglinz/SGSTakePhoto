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
    /// SlimPhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SlimPhotoWindow : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public SlimPhotoWindow()
        {
            InitializeComponent();
        }

        #region 扫描

        /// <summary>
        /// 扫描
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

        #endregion

        #region 拍照

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgSlimOrder.SelectedItem as Order;
            if (order == null)
            {

            }

            if (!App.UserControls.ContainsKey("SlimOrder"))
            {
                SlimOrderModule slimOrderOrder = new SlimOrderModule(order);
                App.CurrentWindow.brMain.Child = slimOrderOrder;
                App.UserControls.Add("SlimOrder", slimOrderOrder);
            }
            else
            {
                App.CurrentWindow.brMain.Child = App.UserControls["SlimOrder"];
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region 照片上传查询

        /// <summary>
        /// 照片上传查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgSlimOrder.SelectedItem as Order;
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

        #endregion

        #region 照片预览

        /// <summary>
        /// 照片预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowser_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgSlimOrder.SelectedItem as Order;
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

        #endregion
    }
}
