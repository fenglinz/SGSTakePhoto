using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// BrowserModule.xaml 的交互逻辑
    /// </summary>
    public partial class BrowserModule : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private DataTable dtTemp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public BrowserModule(Order order)
        {
            InitializeComponent();
            Order = order;
        }

        private void Browser_Loaded(object sender, RoutedEventArgs e)
        {
            dtTemp = new DataTable();
            var files = Directory.GetFiles(@"C:\Users\meizu\Pictures\Saved Pictures");
            dtTemp.Columns.Add("Id", typeof(int));
            dtTemp.Columns.Add("IsUploaded", typeof(int));
            dtTemp.Columns.Add("PicturePath", typeof(string));

            for (int i = 0; i < files.Length; i++)
            {
                if (!(files[i].EndsWith(".png") || files[i].EndsWith(".jpg"))) continue;
                DataRow row = dtTemp.NewRow();
                row[0] = i;
                row[1] = 1;
                row[2] = "https://ss1.bdstatic.com/70cFuXSh_Q1YnxGkpoWK1HF6hhy/it/u=3155068632,1890443235&fm=26&gp=0.jpg"; //files[i];

                dtTemp.Rows.Add(row);
            }

            lbImageView.ItemsSource = dtTemp.DefaultView;
        }

        /// <summary>
        /// 返回到上次目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (CommonHelper.CurrentSystem)
            {
                case "OTS":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.otsModule;
                    break;
                case "SLIM":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.SlimModule;
                    break;
                case "Share":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.shareFolderModule;
                    break;
                case "Setting":
                    CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.settingModule;
                    break;
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakePhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUnSelectAll_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 图片操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOperate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRow row = dtTemp.Select(string.Format("Id = {0}", btn.Tag))[0];
            switch (btn.Content)
            {
                case "Del":
                    break;
                case "Edit":
                    break;
                case "Browser":
                    PhotoViewModule viewModule = new PhotoViewModule(row["PicturePath"].ToString())
                    {
                        ParentControl = this
                    };
                    CommonHelper.MainWindow.brMain.Child = viewModule;
                    break;
                case "Upload":
                    break;
            }
        }
    }
}
