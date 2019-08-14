using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 自动升级信息
    /// </summary>
    [Serializable]
    public class UpdateInfo
    {
        #region 属性

        public string Id { get; set; }

        /// <summary>
        /// 软件名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 升级版本
        /// </summary>
        public double AppVersion { get; set; }

        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool ForceUpgrade { get; set; }

        /// <summary>
        /// 校验值
        /// </summary>
        public Guid Md5 { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }

        #endregion
    }
}
