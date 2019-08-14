using SGSTakePhoto.Infrastructure;
using System.Windows;
using System.Windows.Controls;
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Camera_Loaded(object sender, RoutedEventArgs e)
        {
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                VideoCapture.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
            }
            else
            {
                MessageBox.Show("No camera found", "Notice", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            VideoCapture.Play();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            VideoCapture.Close();
        }
    }
}
