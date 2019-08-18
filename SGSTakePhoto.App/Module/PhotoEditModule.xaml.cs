using Newtonsoft.Json;
using SGSTakePhoto.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// PhotoEditModule.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoEditModule : UserControl
    {
        #region 引用Windows接口

        [DllImport("User32.dll ", SetLastError = true, EntryPoint = "SetParent")]

        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]

        //对外部软件窗口发送一些消息(如 窗口最大化、最小化等)
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll ", EntryPoint = "ShowWindow")]

        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        #endregion

        #region 属性

        /// <summary>
        /// FileName
        /// </summary>
        public UploadFile UploadFile { get; set; }

        /// <summary>
        /// ParentControl
        /// </summary>
        public UserControl ParentControl { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PhotoEditModule()
        {
            InitializeComponent();
        }

        #endregion

        #region 调用外部的图片编辑程序

        /// <summary>
        /// 调用外部的图片编辑程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string tmpFile = Path.GetTempFileName().Replace(".tmp", Path.GetExtension(UploadFile.FileFullName));
            File.Copy(UploadFile.FileFullName, tmpFile);

            Process pro = new Process();
            pro.StartInfo.FileName = Path.Combine(CommonHelper.RootPath, "PhotoApp.exe");
            pro.StartInfo.Arguments = string.Format(" -e {0}", tmpFile.Replace("\\", "/"));
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.RedirectStandardInput = true;
            pro.Start();

            System.Threading.Thread.Sleep(1000);//加上，100如果效果没有就继续加大

            PictureBox pp = new PictureBox
            {
                Width = 1000,
                Height = 800,
                Bounds = new System.Drawing.Rectangle(0, 0, 1000, 800),
                Name = "PictureBox"
            };
            wfFrom.Child = pp;
            IntPtr hpanel1 = pp.Handle;
            SetParent(pro.MainWindowHandle, hpanel1); //panel1.Handle为要显示外部程序的容器
            ShowWindow(pro.MainWindowHandle, 3);
            pro.StandardInput.AutoFlush = true;
            Task<string> result = pro.StandardOutput.ReadToEndAsync();
            result.ContinueWith(PhotoEdit_Complated);
        }

        /// <summary>
        /// 图片编辑完成后
        /// </summary>
        /// <param name="result"></param>
        private void PhotoEdit_Complated(Task<string> value)
        {
            EditResult editResult = JsonConvert.DeserializeObject<EditResult>(value.Result);
            if (editResult == null)
            {
                System.Windows.MessageBox.Show("Edit Failed");
            }
            else
            {
                File.Copy(editResult.Paths.Replace(@"/", "\\"), UploadFile.FileFullName, true);
            }
            this.Dispatcher.Invoke(() =>
            {
                App.CurrentWindow.brMain.Child = ParentControl;
            });
        }

        #endregion
    }
}
