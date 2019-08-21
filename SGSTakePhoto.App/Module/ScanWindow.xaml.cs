using Microsoft.Win32;
using SGSTakePhoto.Infrastructure;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// ScanWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScanWindow : Window
    {
        #region 属性

        /// <summary>
        /// 是否关闭当前控件
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// 扫描后识别的条形码或二维码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 判断当前使用的是哪个摄像头
        /// </summary>
        public int VideoInputQuantity { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ScanWindow()
        {
            InitializeComponent();
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                VideoCapture.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
            }
            else
            {
                MessageBox.Show("No camera found", "Notice", MessageBoxButton.OK, MessageBoxImage.Error);
                IsClosed = true;
                Close();
            }
        } 

        #endregion

        #region 窗口加载

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VideoCapture.Play();
        } 

        #endregion

        #region 拍照识别

        /// <summary>
        /// 拍照识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            //抓取控件做成图片
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)VideoCapture.NaturalVideoWidth, (int)VideoCapture.NaturalVideoHeight, 96, 96, PixelFormats.Default);
            VideoCapture.Stretch = Stretch.Fill;
            VideoCapture.Measure(VideoCapture.RenderSize);
            VideoCapture.Arrange(new Rect(VideoCapture.RenderSize));
            bmp.Render(VideoCapture);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            BarCodeScan scan = new BarCodeScan();
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                Response<string> result = scan.GetBarCode(ms);
                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    VideoCapture.Play();
                }
                else
                {
                    BarCode = result.Data;
                    if (string.IsNullOrEmpty(BarCode))
                    {
                        MessageBox.Show("No valid barcode was obtained,Please Retry", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        VideoCapture.Pause();
                        Close();
                    }
                }
            }
        } 

        #endregion

        #region 重新拍照

        /// <summary>
        /// 重新拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (VideoInputQuantity == MultimediaUtil.VideoInputNames.Length - 1)
            {
                VideoInputQuantity = 0;
            }
            else
            {
                VideoInputQuantity++;
            }

            VideoCapture.VideoCaptureSource = MultimediaUtil.VideoInputNames[VideoInputQuantity];
        } 

        #endregion

        #region 从本地文件夹选择

        /// <summary>
        /// 从本地文件夹选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocal_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "图片文件(*.png;*.jpg;*.bmp;*.jpeg)|*.png;*.jpg;*.bmp;*.jpeg"
            };

            if (openFile.ShowDialog() == true)
            {
                FileStream fileStream = File.Open(openFile.FileName, FileMode.Open);
                BarCodeScan scan = new BarCodeScan();
                Response<string> result = scan.GetBarCode(fileStream);
                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    BarCode = result.Data;
                    if (string.IsNullOrEmpty(BarCode))
                    {
                        MessageBox.Show("No valid barcode was obtained,Please Retry", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        VideoCapture.Pause();
                        Close();
                    }
                }
                fileStream.Close();
            }
        } 

        #endregion

        #region 关闭窗口

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            VideoCapture.Close();
            base.OnClosing(e);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoCapture_NewVideoSample(object sender, WPFMediaKit.DirectShow.MediaPlayers.VideoSampleArgs e)
        {

        }
    }
}
