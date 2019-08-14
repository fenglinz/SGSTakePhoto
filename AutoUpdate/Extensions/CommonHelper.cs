using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoUpdate.Extensions
{
    /// <summary>
    /// CommonHelper
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UpdateInfo GetUpdateInfo()
        {
            return new UpdateInfo { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UpdateInfo GetCurrentAppInfo()
        {
            UpdateInfo info = new UpdateInfo();


            return info;
        }
    }
}
