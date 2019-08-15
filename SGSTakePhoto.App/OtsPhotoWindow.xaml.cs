using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// OtsPhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OtsPhotoWindow : UserControl
    {
        /// <summary>
        /// OtsOrder
        /// </summary>
        private ObservableCollection<Order> Orders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Order SelectedItem
        {
            get
            {
                return dgOtsOrder.SelectedItem as Order;
            }
        }

        /// <summary>
        /// 构造函数初始化数据
        /// </summary>
        public OtsPhotoWindow()
        {
            InitializeComponent();
            Orders = new ObservableCollection<Order>
            {
                new Order{Id=Guid.NewGuid().ToString(), CaseNum="001", JobNum="job111", OrderNum="o001", SampleID="s001", Status="待上传", Owner="test",CreateTime=DateTime.Now },
                new Order{Id=Guid.NewGuid().ToString(), CaseNum="002", JobNum="job222", OrderNum="o002", SampleID="s002", Status="上传中", Owner="test",CreateTime=DateTime.Now }
            };

            dgOtsOrder.ItemsSource = Orders;
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
                if (scan.ShowDialog() == true) return;
                TextBox txtBox = (sender as TextBox);
                txtBox.Text = scan.BarCode;
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            Order order = dgOtsOrder.SelectedItem as Order;
            OtsOrderModule otsOrder = new OtsOrderModule(order);
            CommonHelper.MainWindow.brMain.Child = otsOrder;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {

            }
            else
            {
                MessageBox.Show("No Data Selected", "Error");
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
                UploadModule uploadModule = new UploadModule(SelectedItem);
                CommonHelper.MainWindow.brMain.Child = uploadModule;
            }
            else
            {
                MessageBox.Show("No Data Selected", "Error");
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
                CommonHelper.MainWindow.brMain.Child = module;
            }
            else
            {
                MessageBox.Show("No Data Selected", "Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgOtsOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null)
            {
                gdOtsOrder.DataContext = SelectedItem;
            }
        }
    }
}
