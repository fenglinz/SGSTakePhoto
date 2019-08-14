using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
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
using WPFMediaKit.DirectShow.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// CameraModule.xaml 的交互逻辑
    /// </summary>
    public partial class CameraModule : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CameraModule()
        {
            InitializeComponent();
            VideoCapture.MediaClosed += VideoCapture_MediaClosed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoCapture_MediaClosed(object sender, RoutedEventArgs e)
        {
            //CommonHelper.MainWindow.brMain.Child.
        }

        public CameraModule(Order order)
        {
            InitializeComponent();
            this.Order = order;
            Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                VideoCapture.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
            }
            else
            {
                MessageBox.Show("No camera found", "Notice", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!VideoCapture.IsPlaying)
            {
                MessageBox.Show("照相机关闭");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            VideoCapture.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            VideoCapture.Play();
        }
    }
}
