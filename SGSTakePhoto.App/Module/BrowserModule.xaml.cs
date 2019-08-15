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
        public UserControl ParentControl { get; set; }

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order"></param>
        public BrowserModule()
        {
            InitializeComponent();
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
                row[2] = files[i];

                dtTemp.Rows.Add(row);
            }

            lbImageView.ItemsSource = dtTemp.DefaultView;
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
            CommonHelper.MainWindow.brMain.Child = ParentControl;
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

        }

        #endregion

        #region 上传

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region 图片操作

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

        #endregion
    }
}
