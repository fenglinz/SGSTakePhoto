using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Column : Attribute
    {
        public bool PrimaryKey { get; set; }
        public string ColumnName { get; set; }
    }
}
