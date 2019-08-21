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
    /// SlimPhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SlimPhotoWindow : UserControl
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
        /// SelectedItem
        /// </summary>
        private Order SelectedItem => dgSlimOrder.SelectedItem as Order; 

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SlimPhotoWindow()
        {
            InitializeComponent();
            orderServices = new OrderServices();
        }

        /// <summary>
        /// 控件加载完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlimPhoto_Loaded(object sender, RoutedEventArgs e)
        {
            var result = orderServices.GetList(string.Format("SELECT * FROM [Order] WHERE ExecutionSystem = 'SLIM' AND Owner = '{0}'", App.CurrentUser));
            if (result.Success)
            {
                Orders = result.Datas;
            }

            dgSlimOrder.ItemsSource = Orders;
            dgSlimOrder.SelectedIndex = 0;
        } 

        #endregion

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
            SlimOrderModule slimOrder = new SlimOrderModule { Order = order };
            App.CurrentWindow.brMain.Child = slimOrder;
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
            if (SelectedItem == null) return;
            if (!CommonHelper.DeleteConfirm()) return;
            SelectedItem.Delete();
            Orders.Remove(SelectedItem);
            dgSlimOrder.SelectedIndex = 0;
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

        #endregion

        #region 照片预览

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

        #endregion
    }
}
