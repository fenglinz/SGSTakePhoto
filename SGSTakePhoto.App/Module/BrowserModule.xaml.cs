using SGSTakePhoto.Infrastructure;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// BrowserModule.xaml 的交互逻辑
    /// </summary>
    public partial class BrowserModule : UserControl
    {
        #region 属性

        /// <summary>
        /// Order
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// UploadFiles
        /// </summary>
        private ObservableCollection<UploadFile> UploadFiles { get; set; }

        /// <summary>
        /// 父级容器
        /// </summary>
        public UserControl ParentControl { get; set; }

        /// <summary>
        /// UploadFileServices
        /// </summary>
        private UploadFileServices uploadFileServices;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order"></param>
        public BrowserModule()
        {
            InitializeComponent();
            uploadFileServices = new UploadFileServices();
        }

        #endregion

        #region 控件加载完成后

        /// <summary>
        /// 控件加载完成后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browser_Loaded(object sender, RoutedEventArgs e)
        {
            Binding_Data();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void Binding_Data(string photoType = "Original")
        {
            var result = uploadFileServices.GetList(string.Format("SELECT *FROM [UploadFile] WHERE OrderId = '{0}' AND PhotoType = '{1}'", Order.Id, photoType));
            if (result.Success)
            {
                UploadFiles = result.Datas;
                lbImageView.ItemsSource = UploadFiles;
            }
            else
            {
                if (UploadFiles != null) UploadFiles.Clear();
            }
        }

        #endregion

        #region 返回到层窗口

        /// <summary>
        /// 返回到上次目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentWindow.brMain.Child = ParentControl;
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

        }

        #endregion

        #region 全选

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            lbImageView.SelectAll();
        }

        #endregion

        #region 取消全选

        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            lbImageView.UnselectAll();
        }

        #endregion

        #region 删除选中的图片

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            IList<UploadFile> lstUploadFiles = lbImageView.SelectedItems.Cast<UploadFile>().ToList();
            if (lstUploadFiles == null || lstUploadFiles.Count <= 0)
            {
                CommonHelper.NoDataSelected();
            }
            else
            {
                if (!CommonHelper.DeleteConfirm()) return;
                foreach (UploadFile item in lstUploadFiles)
                {
                    UploadFiles.Remove(item);
                    item.Delete();
                }
            }
        }

        #endregion

        #region 上传图片

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Has joined the upload queue!");
            UploadModule uploadModule = new UploadModule { Order = Order, ParentControl = this };
            App.CurrentWindow.brMain.Child = uploadModule;
        }

        #endregion

        #region 图片类型筛选

        /// <summary>
        /// 图片类型筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPhotoTypeFilter_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string filter = btn.Content.ToString();
            Binding_Data(filter);
        }

        /// <summary>
        /// 图片操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOperate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            UploadFile model = UploadFiles.Where(x => x.Id == btn.Tag.ToString()).FirstOrDefault();
            switch (btn.Content)
            {
                case "Del":
                    UploadFiles.Remove(model);
                    model.Delete();
                    break;
                case "Edit":
                    PhotoEditModule editModule = new PhotoEditModule
                    {
                        ParentControl = this,
                        UploadFile = model
                    };

                    App.CurrentWindow.brMain.Child = editModule;
                    break;
                case "Browser":
                    PhotoViewModule viewModule = new PhotoViewModule(model.FileFullName) { ParentControl = this };
                    App.CurrentWindow.brMain.Child = viewModule;
                    break;
                case "Upload":
                    MessageBox.Show("Has joined the upload queue!");
                    break;
            }
        }

        #endregion
    }
}
