using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly OtsPhotoWindow otsModule = new OtsPhotoWindow { };
        public readonly SlimPhotoWindow SlimModule = new SlimPhotoWindow { };
        public readonly ShareFolderWindow shareFolderModule = new ShareFolderWindow { };
        public readonly SettingWindow settingModule = new SettingWindow { };
        public readonly CameraModule cameraModule = new CameraModule { };
        private readonly List<UserControl> userControl;

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            userControl = new List<UserControl>
            {
                otsModule,SlimModule,shareFolderModule,settingModule
            };


            CommonHelper.MainWindow = this;
            foreach (UserControl item in userControl)
            {
                CommonHelper.UserControls.Add(item.Name, item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.brMain.Child = this.otsModule;
            CommonHelper.CurrentSystem = "OTS";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (UserControl control in CommonHelper.UserControls.Values)
            {
                control.Height = e.NewSize.Height;
                control.Width = e.NewSize.Width - 200;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btnOTS":
                    if (this.brMain.Child != this.otsModule)
                        this.brMain.Child = this.otsModule;
                    break;
                case "btnSLIM":
                    if (this.brMain.Child != this.SlimModule)
                        this.brMain.Child = this.SlimModule;
                    break;
                case "btnShare":
                    if (this.brMain.Child != this.shareFolderModule)
                        this.brMain.Child = this.shareFolderModule;
                    break;
                case "btnSetting":
                    if (this.brMain.Child != this.settingModule)
                        this.brMain.Child = this.settingModule;
                    break;
            }

            //停止照相机
            cameraModule.Stop();
            CommonHelper.CurrentSystem = btn.Name.Replace("btn", string.Empty);
            this.brMain.Child.RenderSize = new Size { Height = this.Height, Width = this.Width };
        }

        #region 结束应用程序

        /// <summary>
        /// 结束应用程序
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        } 

        #endregion
    }
}
