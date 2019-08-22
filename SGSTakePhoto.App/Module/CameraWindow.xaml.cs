﻿using SGSTakePhoto.Infrastructure;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;
using System;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// CameraModule.xaml 的交互逻辑
    /// </summary>
    public partial class CameraWindow : Window
    {
        #region 属性

        /// <summary>
        /// Order
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// 是否关闭当前控件
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// 判断当前使用的是哪个摄像头
        /// </summary>
        public int VideoInputQuantity { get; set; } 

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CameraWindow()
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

        #region 窗体加载

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Camera_Loaded(object sender, RoutedEventArgs e)
        {
            VideoCapture.Play();
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

        #region 点击拍照

        /// <summary>
        /// 点击拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTakePhoto_Click(object sender, RoutedEventArgs e)
        {
            //暂停
            VideoCapture.Pause();
            try
            {
                //抓取控件做成图片
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)VideoCapture.NaturalVideoWidth, (int)VideoCapture.NaturalVideoHeight, 96, 96, PixelFormats.Default);
                //VideoCapture.Stretch = Stretch.Fill;
                VideoCapture.Measure(VideoCapture.RenderSize);
                VideoCapture.Arrange(new Rect(VideoCapture.RenderSize));
                bmp.Render(VideoCapture);
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));

                UploadFile model = new UploadFile
                {
                    OrderId = Order.Id,
                    IsSync = false,
                    Status = "Waitting",
                    ExecutionSystem = App.CurrentSystem,
                    FileName = Order.GeneralFileName,
                    Location = Order.AbsolutePath,
                    PhotoType = Order.PhotoType
                };
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    byte[] captureData = ms.ToArray();

                    File.WriteAllBytes(model.FileFullName, captureData);
                }

                model.InsertOrReplace();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                //继续开始摄像
                VideoCapture.Play();
            }
        }

        #endregion

        #region 切换摄像头

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

        #region 选择图片

        /// <summary>
        /// 从文件夹选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "图片文件(*.png;*.jpg;*.bmp;*.jpeg)|*.png;*.jpg;*.bmp;*.jpeg"
            };

            try
            {
                if (openFile.ShowDialog() == true)
                {
                    foreach (string item in openFile.FileNames)
                    {
                        UploadFile model = new UploadFile
                        {
                            OrderId = Order.Id,
                            IsSync = false,
                            Status = "Waitting",
                            ExecutionSystem = App.CurrentSystem,
                            FileName = Order.GeneralFileName,
                            Location = Order.AbsolutePath,
                            PhotoType = Order.PhotoType
                        };

                        File.Copy(item, model.FileFullName, true);
                        model.InsertOrReplace();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
