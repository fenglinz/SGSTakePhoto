using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// UploadModule.xaml 的交互逻辑
    /// </summary>
    public partial class UploadModule : UserControl
    {
        #region 属性

        /// <summary>
        /// Order
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// 父级控件，用于返回
        /// </summary>
        public UserControl ParentControl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<UploadFile> UploadFiles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private UploadFileServices uploadFileServices;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public UploadModule()
        {
            InitializeComponent();
            uploadFileServices = new UploadFileServices();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Upload_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Order == null) return;
            Binding_Data();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void Binding_Data()
        {
            var result = uploadFileServices.GetList(string.Format("SELECT *FROM [UploadFile] WHERE OrderId = '{0}'", Order.Id));
            if (result.Success)
            {
                UploadFiles = result.Datas;
                dgUpload.ItemsSource = UploadFiles;
            }
        }

        /// <summary>
        /// 返回到上次目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.CurrentWindow.brMain.Child = ParentControl;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Has joined the upload queue!");
        }
    }
}
