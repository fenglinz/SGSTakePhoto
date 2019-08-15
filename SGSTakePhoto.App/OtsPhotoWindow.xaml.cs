using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
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
        private readonly OrderServices orderServices;

        /// <summary>
        /// 
        /// </summary>
        private Order SelectedItem => dgOtsOrder.SelectedItem as Order;

        /// <summary>
        /// 构造函数初始化数据
        /// </summary>
        public OtsPhotoWindow()
        {
            InitializeComponent();
            orderServices = new OrderServices();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtsPhoto_Loaded(object sender, RoutedEventArgs e)
        {
            var result = orderServices.GetList("SELECT * FROM [Order]");
            if (result.Success)
            {
                Orders = result.Datas;
            }

            dgOtsOrder.ItemsSource = Orders;
            dgOtsOrder.SelectedIndex = 0;
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
            if (SelectedItem == null) return;
            MessageBoxResult result = MessageBox.Show("Delete after confirmation", "Comfirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                SelectedItem.Delete();
                Orders.Remove(SelectedItem);
                dgOtsOrder.SelectedIndex = 0;
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
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<string> lstFilter = new List<string> { "WHERE (1=1)" };
            if (!string.IsNullOrEmpty(txtCaseNum.Text))
            {
                lstFilter.Add(string.Format("CaseNum = '{0}'", txtCaseNum.Text));
            }
            if (!string.IsNullOrEmpty(txtJobNum.Text))
            {
                lstFilter.Add(string.Format("JobNum = '{0}'", txtJobNum.Text));
            }
            if (cmbStatus.SelectedIndex >= 0)
            {
                lstFilter.Add(string.Format("Status = '{0}'", cmbStatus.SelectedValue));
            }

            var result = orderServices.GetList(string.Format("SELECT * FROM [Order] {0}", string.Join(" AND ", lstFilter)));
            if (result.Success)
            {
                Orders = result.Datas;
                dgOtsOrder.ItemsSource = Orders;
                dgOtsOrder.SelectedIndex = 0;
            }
            else
            {
                Orders.Clear();
            }
        }
    }
}
