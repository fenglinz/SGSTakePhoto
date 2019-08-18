using SGSTakePhoto.Infrastructure;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// OtsOrderModule.xaml 的交互逻辑
    /// </summary>
    public partial class OtsOrderModule : UserControl
    {
        /// <summary>
        /// Order
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// 选择的照片类型
        /// </summary>
        public string PhotoType { get; set; } = "Original";

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public OtsOrderModule()
        {
            InitializeComponent();
            if (Order != null) gdOtsOrder.DataContext = Order;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order"></param>
        public OtsOrderModule(Order order)
        {
            InitializeComponent();
            this.Order = order;
            if (Order != null) gdOtsOrder.DataContext = Order;
        }

        #endregion

        /// <summary>
        /// 返回到OTS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentWindow.brMain.Child = App.CurrentWindow.otsModule;
        }

        /// <summary>
        /// 点击扫描按钮
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
        /// 点击拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImageType_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender as Button;
            Order.PhotoType = btn.Content.ToString();
            PhotoType = Order.PhotoType;
            CameraWindow camera = new CameraWindow
            {
                Order = Order,
                Owner = App.CurrentWindow
            };
            if (camera.ShowDialog() == true) return;
            BrowserModule module = new BrowserModule { Order = Order, ParentControl = this };
            App.CurrentWindow.brMain.Child = module;
        }
    }
}
