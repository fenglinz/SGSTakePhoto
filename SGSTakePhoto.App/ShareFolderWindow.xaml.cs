using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region 属性

        /// <summary>
        /// OtsOrder
        /// </summary>
        private ObservableCollection<Order> Orders { get; set; }

        /// <summary>
        /// OrderServices
        /// </summary>
        private readonly OrderServices orderServices;

        /// <summary>
        /// Order
        /// </summary>
        private Order SelectedItem => dgShareFolder.SelectedItem as Order;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ShareFolderWindow()
        {
            InitializeComponent();
            orderServices = new OrderServices();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShareFolder_Loaded(object sender, RoutedEventArgs e)
        {
            var result = orderServices.GetList(string.Format("SELECT * FROM [Order] WHERE ExecutionSystem = 'ShareFolder' AND Owner = '{0}'", App.CurrentUser));
            if (result.Success)
            {
                Orders = result.Datas;
            }

            dgShareFolder.ItemsSource = Orders;
            dgShareFolder.SelectedIndex = 0;
        }

        #endregion

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgShareFolder.SelectedItem as Order;
            ShareFolderModule shareOrder = new ShareFolderModule { Order = order };
            App.CurrentWindow.brMain.Child = shareOrder;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null) return;
            if (!CommonHelper.DeleteConfirm()) return;
            SelectedItem.Delete();
            Orders.Remove(SelectedItem);
            dgShareFolder.SelectedIndex = 0;
        }

        /// <summary>
        /// 扫描条形码或二维码
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
            if (SelectedItem != null)
            {
                UploadModule uploadModule = new UploadModule { Order = SelectedItem, ParentControl = this };
                App.CurrentWindow.brMain.Child = uploadModule;
            }
            else
            {
                CommonHelper.NoDataSelected();
            }
        }

        /// <summary>
        /// 照片预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowser_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                BrowserModule module = new BrowserModule { Order = SelectedItem, ParentControl = this };
                App.CurrentWindow.brMain.Child = module;
            }
            else
            {
                CommonHelper.NoDataSelected();
            }
        }
    }
}
