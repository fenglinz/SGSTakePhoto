using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 数据传输方式
    /// </summary>
    public enum TransferType
    {
        /// <summary>
        /// Merge
        /// </summary>
        Merge,

        /// <summary>
        /// 批量插入
        /// </summary>
        BulkCopy,

        /// <summary>
        /// 数据库对拷
        /// </summary>
        BatchDataBase
    }
}
