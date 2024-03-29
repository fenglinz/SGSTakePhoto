﻿using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            foreach (UserControl item in userControl)
            {
                App.UserControls.Add(item.Name, item);
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
            App.CurrentSystem = "OTS";
        }

        /// <summary>
        /// 所有控件加载完成后检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //检查更新
            CommonHelper.AutoUpdate(this, (value) =>
            {
                UpdateWindow updateWindow = new UpdateWindow(value) { Owner = this };
                if (updateWindow.ShowDialog() == true && updateWindow.IsExsitUpgrade == false) return;
                this.Closed += new EventHandler((obj, arg) => Process.Start(Path.Combine(CommonHelper.RootPath, "AutoUpdate.exe"), string.Format("{0}.zip", value.AppName)));
                this.Close();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (UserControl control in App.UserControls.Values)
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
                case "btnShareFolder":
                    if (this.brMain.Child != this.shareFolderModule)
                        this.brMain.Child = this.shareFolderModule;
                    break;
                case "btnSetting":
                    if (this.brMain.Child != this.settingModule)
                        this.brMain.Child = this.settingModule;
                    break;
            }

            App.CurrentSystem = btn.Name.Replace("btn", string.Empty);
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
