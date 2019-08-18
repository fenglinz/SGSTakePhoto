using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Cache;
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
    /// PhotoViewModule.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoViewModule : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserControl ParentControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public PhotoViewModule(string fileName)
        {
            InitializeComponent();
            gdPhotoView.DataContext = new { PicturePath = fileName };
        }


        /// <summary>
        /// 返回上层目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentWindow.brMain.Child = ParentControl;
        }
    }
}
