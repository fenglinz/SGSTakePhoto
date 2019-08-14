using System;

namespace AutoUpdate
{
    /// <summary>
    /// UpdateInfo
    /// </summary>
    [Serializable]
    public class UpdateInfo
    {
        #region 属性

        /// <summary>
        /// 软件名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 升级版本
        /// </summary>
        public Version AppVersion { get; set; }

        /// <summary>
        /// 最小升级版本
        /// </summary>
        public Version RequiredMinVersion { get; set; }

        /// <summary>
        /// 校验值
        /// </summary>
        public Guid Md5 { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = string.Join(Environment.NewLine, value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        #endregion
    }
}
