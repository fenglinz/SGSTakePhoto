using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public const string ApplicationName = "SGSTakePhoto";

        /// <summary>
        /// 主窗口
        /// </summary>
        public static MainWindow CurrentWindow { get; set; }

        /// <summary>
        /// 当前用户正在使用的系统
        /// </summary>
        public static string CurrentSystem { get; set; }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static string CurrentUser { get; set; }

        /// <summary>
        /// UserControl集合
        /// </summary>
        public static Dictionary<string, UserControl> UserControls = new Dictionary<string, UserControl>();

        //单实例模式
        private Mutex mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            bool startupFlag = false;
            try
            {
                mutex = new Mutex(true, ApplicationName, out startupFlag);
                if (!startupFlag)
                {
                    MessageBox.Show("程序已经启动!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(0);
                }
                else
                {
                    if (e.Args.Length > 0)//启动参数
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "启动时发生错误");
            }
            finally
            {
                base.OnStartup(e);
                LoadLanguage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadLanguage()
        {
            CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
            ResourceDictionary langRd = null;
            try
            {
                langRd = Application.LoadComponent(new Uri(@"Language\" + currentCultureInfo.Name + ".xaml", UriKind.Relative)) as ResourceDictionary;
            }
            catch
            {

            }
            if (langRd != null)
            {
                //if (this.Resources.MergedDictionaries.Count > 0)
                //{
                //    this.Resources.MergedDictionaries.Clear();
                //}
                this.Resources.MergedDictionaries[0] = langRd;
            }
        }

        /// <summary>
        /// 将软件所需的资源一次都加载到内存中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            var executingAssemblyName = executingAssembly.GetName();
            var resName = executingAssemblyName.Name + ".resources";
            AssemblyName assemblyName = new AssemblyName(args.Name); string path = "";
            if (resName == assemblyName.Name)
            {
                path = executingAssemblyName.Name + ".g.resources"; ;
            }
            else
            {
                path = assemblyName.Name + ".dll";
                if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false)
                {
                    path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);
                }
            }
            using (Stream stream = executingAssembly.GetManifestResourceStream(path))
            {
                if (stream == null) return null;
                byte[] assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }

        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
            e.Handled = true;
        }
    }
}
