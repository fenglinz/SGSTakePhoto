using System;
using System.Data;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 用户设置
    /// </summary>
    public class UserConfig : NotifyBaseEntity
    {
        private string id;
        private string userName;
        private string organization;
        private string executionSystem;
        private double defaultWidth;
        private double defaultHeight;
        private double defaultDPI;

        [Column(PrimaryKey = true, ColumnName = "Id")]
        public string Id
        {
            get => id; set
            {
                id = value;
                NotifyPropertyChange(() => Id);
            }
        }
        [Column(ColumnName = "User")]
        public string UserName
        {
            get => userName; set
            {
                userName = value;
                NotifyPropertyChange(() => UserName);
            }
        }
        [Column(ColumnName = "Organization")]
        public string Organization
        {
            get => organization; set
            {
                organization = value; NotifyPropertyChange(() => Organization);
            }
        }
        [Column(ColumnName = "ExecutionSystem")]
        public string ExecutionSystem
        {
            get => executionSystem; set
            {
                executionSystem = value; NotifyPropertyChange(() => ExecutionSystem);
            }
        }
        [Column(ColumnName = "DefaultWidth")]
        public double DefaultWidth
        {
            get => defaultWidth; set
            {
                defaultWidth = value; NotifyPropertyChange(() => DefaultWidth);
            }
        }
        [Column(ColumnName = "DefaultHeight")]
        public double DefaultHeight
        {
            get => defaultHeight; set
            {
                defaultHeight = value; NotifyPropertyChange(() => DefaultHeight);
            }
        }
        [Column(ColumnName = "DefaultDPI")]
        public double DefaultDPI
        {
            get => defaultDPI; set
            {
                defaultDPI = value; NotifyPropertyChange(() => DefaultDPI);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public static implicit operator UserConfig(DataRow row)
        {
            return new UserConfig
            {
                Id = row.IsNull("Id") ? string.Empty : row["Id"].ToString(),
                UserName = row.IsNull("UserName") ? string.Empty : row["UserName"].ToString(),
                Organization = row.IsNull("Organization") ? string.Empty : row["Organization"].ToString(),
                ExecutionSystem = row.IsNull("ExecutionSystem") ? string.Empty : row["ExecutionSystem"].ToString(),
                DefaultWidth = row.IsNull("DefaultWidth") ? 0 : Convert.ToDouble(row["DefaultWidth"]),
                DefaultHeight = row.IsNull("DefaultHeight") ? 0 : Convert.ToDouble(row["DefaultHeight"]),
                DefaultDPI = row.IsNull("DefaultDPI") ? 0 : Convert.ToDouble(row["DefaultDPI"]),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override Response<int> Create(string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                sql = string.Format(@"INSERT INTO UserConfig(Id,UserName,Organization,ExecutionSystem,DefaultWidth,DefaultHeight,DefaultDPI) VALUES('{0}','{1}','{2}','{3}',{4},{5},{6})",
                         Guid.NewGuid().ToString(),
                         UserName,
                         Organization,
                         ExecutionSystem,
                         DefaultWidth,
                         DefaultHeight,
                         DefaultHeight);
            }

            return base.Create(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override Response<int> Update(string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                sql = string.Format(@"UPDATE UserConfig SET UserName = '{0}',Organization = '{1}',ExecutionSystem = '{2}',DefaultWidth = '{3}',DefaultHeight = '{4}',DefaultDPI = '{5}' WHERE Id = '{6}'",
                         UserName,
                         Organization,
                         ExecutionSystem,
                         DefaultWidth,
                         DefaultHeight,
                         DefaultHeight,
                         Id);
            }

            return base.Update(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override Response<int> Delete(string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                sql = string.Format(@"DELETE FROM UserConfig WHERE Id = '{0}'", Id);
            }

            return base.Delete(sql);
        }
    }
}
