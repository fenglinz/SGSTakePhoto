using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;

namespace AutoUpdate
{
    /// <summary>
    /// Program
    /// </summary>
    class Program
    {
        public static string RootPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;

            if (args.Length == 0)
            {
                MessageBoxResult result = MessageBox.Show("请打开主程序检查更新", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(Path.Combine(RootPath, "SGSTakePhoto.App.exe"));
                }
            }
            else
            {
                string filePath = Path.Combine(RootPath, "Source", args[0]);
                Process[] proces = Process.GetProcessesByName("SGSTakePhoto");
                foreach (Process proc in proces) proc.Kill();
                Response result = UnZipFile(filePath, RootPath);
                if (!result.Success) return;
                Process.Start(Path.Combine(RootPath, "SGSTakePhoto.App.exe"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
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
                if (stream == null)
                    return null;

                byte[] assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="zipedFolder"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [STAThread]
        public static Response UnZipFile(string fileName, string zipedFolder, string password = "")
        {
            FileStream reader = null; ZipInputStream zipStream = null;
            if (!File.Exists(fileName)) return new Response { Message = "File not exsist" };
            if (!Directory.Exists(zipedFolder)) Directory.CreateDirectory(zipedFolder);
            try
            {
                reader = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                zipStream = new ZipInputStream(reader);
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                ZipEntry ent;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (string.IsNullOrEmpty(ent.Name)) continue;
                    if (ent.Name.Contains("AutoUpdate") || ent.Name.Contains("ICSharpCode.SharpZipLib")) continue;
                    var singleFileName = Path.Combine(zipedFolder, ent.Name);
                    singleFileName = singleFileName.Replace('/', '\\');//change by Mr.HopeGi   
                    if (singleFileName.EndsWith("\\"))
                    {
                        Directory.CreateDirectory(singleFileName);
                        continue;
                    }
                    //无论文件是在哪个目录都将其加压到升级根目录
                    //singleFileName = Path.Combine(zipedFolder, Path.GetFileName(singleFileName));
                    if (File.Exists(singleFileName)) File.Delete(singleFileName);
                    using (FileStream streamWriter = File.Create(singleFileName))
                    {

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                return new Response { };
            }
            catch (Exception ex)
            {
                return new Response { Message = ex.Message };
            }
            finally
            {
                if (reader != null) reader.Close();
                if (zipStream != null) zipStream.Close();
                //删除源文件
                //if (File.Exists(fileName)) File.Delete(fileName);
            }
        }
    }
}
