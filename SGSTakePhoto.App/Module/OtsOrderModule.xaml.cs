using SGSTakePhoto.Infrastructure;
using System.Collections.ObjectModel;
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.otsModule;
        }

        /// <summary>
        /// 点击扫描按钮
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
                        case "txtCaseNum":

                            break;
                        case "txtJobNum":

                            break;
                        case "txtSampleId":

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImageType_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender as Button;
            switch (btn.Content.ToString())
            {
                case "Original":
                    break;
                case "Before":
                    break;
                case "Testing":
                    break;
                case "During":
                    break;
                case "After":
                    break;
                case "Feature":
                    break;
                default:

                    break;
            }

            PhotoType = btn.Content.ToString();
            CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.cameraModule;
        }

        /// 关闭窗口事件
        /// </summary>
        ///<param name="sender">
        ///<param name="cancelEventArgs">
        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            MessageBox.Show("OnClosing 子窗口要被关闭了");
            // 析构
        }
    }
}
