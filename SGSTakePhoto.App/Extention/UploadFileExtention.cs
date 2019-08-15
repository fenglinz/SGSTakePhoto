using SGSTakePhoto.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 文件上传服务
    /// </summary>
    public class UploadFileExtention : IDisposable
    {
        private const int BUFFER_SIZE = 4096;
        protected string ServerIP { get; }
        protected string UserID { get; }
        protected string Password { get; }

        public UploadFileExtention() { }

        /// <summary>
        /// 构造函数初始化参数
        /// </summary>
        /// <param name="ftpServerIP"></param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        public UploadFileExtention(string serverIP, string userID, string password)
        {
            ServerIP = serverIP;
            UserID = userID;
            Password = password;
        }


        #region HttpUploadFile

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public event UploadFileCompletedEventHandler UploadFileCompleted;

        /// <summary>
        /// 
        /// </summary>
        public event UploadProgressChangedEventHandler UploadProgressChanged;

        /// <summary>
        /// 
        /// </summary>
        public Response HttpUploadFile(string url, string fileName)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.UploadFileCompleted += UploadFileCompleted;
                    client.UploadProgressChanged += UploadProgressChanged;
                    client.UploadFileAsync(new Uri(url), "POST", fileName);
                }

                return new Response { };
            }
            catch (WebException ex)
            {
                return new Response { ErrorMessage = ex.Message };
            }
        }

        #endregion

        #region FTP方式上传文件

        protected FtpWebRequest FtpRequest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        internal void ConnectFtp(string path)
        {
            FtpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            //指定数据传输类型
            FtpRequest.UseBinary = true;
            //ftp用户名和密码
            FtpRequest.Credentials = new NetworkCredential(UserID, Password);
        }

        /// <summary>
        /// FTP方式上传文件
        /// </summary>
        public Response FtpUploadFile(string fileName, string path)
        {
            try
            {
                path = path.Replace("\\", "/");
                FileInfo fInfo = new FileInfo(fileName);
                string uri = string.Format("ftp://{0}/{1}", path, fInfo.Name);
                ConnectFtp(uri);
                // 默认为true，连接不会被关闭
                // 在一个命令之后被执行
                FtpRequest.KeepAlive = false;
                // 指定执行什么命令
                FtpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                // 上传文件时通知服务器文件的大小
                FtpRequest.ContentLength = fInfo.Length;
                // 缓冲大小设置为kb 
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                // 打开一个文件流(System.IO.FileStream) 去读上传的文件
                FileStream fs = fInfo.OpenRead();
                using (Stream stream = FtpRequest.GetRequestStream())
                {
                    // 把上传的文件写入流
                    // 每次读文件流的kb
                    contentLen = fs.Read(buff, 0, buffLength);
                    // 流内容没有结束
                    while (contentLen != 0)
                    {
                        // 把内容从file stream 写入upload stream 
                        stream.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }
                }
                fs.Close();

                return new Response { };
            }
            catch (Exception ex)
            {
                return new Response { ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Response FtpDownloadFile()
        {
            try
            {


                return new Response { };
            }
            catch (Exception ex)
            {
                return new Response { ErrorMessage = ex.Message };
            }
        }

        #endregion

        #region 共享文件夹方式上传文件

        /// <summary>
        /// 链接远程共享文件夹
        /// </summary>
        /// <param name="path">远程共享文件夹</param>
        /// <returns></returns>
        protected Response ConnectShareFolder(string path)
        {
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use " + path + " /User:" + UserID + " " + Password + " /PERSISTENT:YES";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1200);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    return new Response { ErrorMessage = errormsg };
                }

                return new Response { };
            }
            catch (Exception ex)
            {
                return new Response { ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// 共享文件夹方式上传文件
        /// </summary>
        public Response ShareFolderUploadFile(string localFileName, string remoteFileName)
        {
            //获取远程服务器的路径
            string remoteDir = Path.GetDirectoryName(remoteFileName);
            var result = ConnectShareFolder(remoteDir);
            if (!result.Success)
            {
                return result;
            }
            //将本地文件复制到远程目录
            //File.Copy(localFileName, remoteFileName, true);
            long totalSize = 0;
            byte[] buffer;
            using (FileStream fs = new FileStream(localFileName, FileMode.Open, FileAccess.Read))
            {
                totalSize = fs.Length;
                if (totalSize > BUFFER_SIZE)
                {
                    buffer = new byte[BUFFER_SIZE];
                    fs.BeginRead(buffer, 0, BUFFER_SIZE, AsyncCopyFile, fs);
                }
            }

            return new Response { };
        }

        public void AsyncCopyFile(IAsyncResult result)
        {

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
