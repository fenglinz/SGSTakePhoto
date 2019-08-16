using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// PhotoEditModule.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoEditModule : UserControl
    {
        [DllImport("User32.dll ", SetLastError = true, EntryPoint = "SetParent")]

        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);
        //对外部软件窗口发送一些消息(如 窗口最大化、最小化等)

        [DllImport("user32.dll ", EntryPoint = "ShowWindow")]

        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);


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
        public PhotoEditModule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Process pro = new Process();
                pro.StartInfo.FileName = @"C:\Workspaces\SGSTakePhoto\SGSTakePhoto.Edit\PhotoApp.exe";
                pro.StartInfo.Arguments = string.Format(" -e {0}", FileName.Replace("\\", "/"));
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
                //wfFrom.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => wfFrom.Child = pp));
                wfFrom.Child = pp;
                IntPtr hpanel1 = pp.Handle;
                SetParent(pro.MainWindowHandle, hpanel1); //panel1.Handle为要显示外部程序的容器
                ShowWindow(pro.MainWindowHandle, 3);

                //pro.StandardInput.WriteLine(strcms);
                //pro.StandardInput.WriteLine("exit");
                pro.StandardInput.AutoFlush = true;
                pro.WaitForExit();

                string reuslt = pro.StandardOutput.ReadToEnd();
                //MessageBox.Show(reuslt);
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pro_Exited(object sender, EventArgs e)
        {
            //e.ToString()
            MessageBox.Show(sender.ToString());
        }
    }
}
