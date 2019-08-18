using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// 判断此条码是否存在
        /// </summary>
        /// <returns></returns>
        private bool Order_Exists()
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
            if (!string.IsNullOrEmpty(txtSampleId.Text))
            {
                lstFilter.Add(string.Format("SampleID = '{0}'", txtSampleId.Text));
            }

            var result = orderServices.SingleOrDefault(string.Format("SELECT * FROM [Order] {0}", string.Join(" AND ", lstFilter)));
            if (result.Success)
            {
                return result.Data != null;
            }

            return false;
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
            App.CurrentWindow.brMain.Child = otsOrder;
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
            dgOtsOrder.SelectedIndex = 0;
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

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyInputIsValid()) return;
            List<string> lstFilter = new List<string> { "WHERE (1=1)" };
            if (!string.IsNullOrEmpty(txtCaseNum.Text))
            {
                lstFilter.Add(string.Format("CaseNum = '{0}'", txtCaseNum.Text));
            }
            if (!string.IsNullOrEmpty(txtJobNum.Text))
            {
                lstFilter.Add(string.Format("JobNum = '{0}'", txtJobNum.Text));
            }
            if (!string.IsNullOrEmpty(txtSampleId.Text))
            {
                lstFilter.Add(string.Format("SampleID = '{0}'", txtSampleId.Text));
            }
            if (cmbStatus.SelectedIndex >= 0)
            {
                lstFilter.Add(string.Format("Status = '{0}'", cmbStatus.SelectedValue));
            }

            var result = orderServices.GetList(string.Format("SELECT * FROM [Order] {0}", string.Join(" AND ", lstFilter)));
            if (result.Success)
            {
                Orders = result.Datas;
            }
            else
            {
                //判断是否存在此Order
                if (!Order_Exists())
                {
                    Order model = new Order
                    {
                        ExecutionSystem = CommonHelper.CurrentSystem,
                        CaseNum = txtCaseNum.Text,
                        JobNum = txtJobNum.Text,
                        SampleID = txtSampleId.Text,
                        Status = cmbStatus.SelectedIndex >= 0 ? cmbStatus.SelectedValue.ToString() : "NoPhoto",
                        Owner = CommonHelper.CurrentUser
                    };

                    model.Create();
                    if (Orders == null) Orders = new ObservableCollection<Order>();
                    Orders.Add(model);
                }
            }

            dgOtsOrder.ItemsSource = Orders;
            dgOtsOrder.SelectedIndex = 0;
        }

        /// <summary>
        /// 校验输入是否完整
        /// </summary>
        /// <returns></returns>
        private bool VerifyInputIsValid()
        {
            if (string.IsNullOrEmpty(txtCaseNum.Text))
            {
                MessageBox.Show("Must input CaseNum", "Notice");
                return false;
            }
            if (string.IsNullOrEmpty(txtJobNum.Text))
            {
                MessageBox.Show("Must input JobNum", "Notice");
                return false;
            }
            if (string.IsNullOrEmpty(txtSampleId.Text))
            {
                MessageBox.Show("Must input SampleID", "Notice");
                return false;
            }

            return true;
        }
    }
}
