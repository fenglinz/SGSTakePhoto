using Microsoft.Win32;
using SGSTakePhoto.Infrastructure;
using System.ComponentModel;
using System.Drawing;
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
        public bool IsClosed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BarCode { get; set; }

        public int VideoInputQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScanWindow()
        {
            InitializeComponent();
            //cmbCam.ItemsSource = MultimediaUtil.VideoInputNames;
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                VideoCapture.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
            }
            else
            {
                MessageBox.Show("No camera found", "Notice", MessageBoxButton.OK, MessageBoxImage.Error);
                IsClosed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VideoCapture.Play();
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            //抓取控件做成图片
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)VideoCapture.ActualWidth, (int)VideoCapture.ActualHeight, 96, 96, PixelFormats.Default);
            bmp.Render(VideoCapture);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            string tmpFileName = Path.GetTempFileName();
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
                        this.Close();
                    }
                }
            }
        }

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

        /// <summary>
        /// 
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
                        this.Close();
                    }
                }
                fileStream.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            VideoCapture.Close();
            base.OnClosing(e);
        }
    }
}
