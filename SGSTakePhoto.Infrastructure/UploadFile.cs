using System;
using System.Data;
using System.IO;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// Upload
    /// </summary>
    public class UploadFile : NotifyBaseEntity
    {
        #region 字段

        private string orderId;
        private bool isSync;
        private string status;
        private DateTime createDate;
        private DateTime? uploadTime;
        private string executionSystem;
        private string fileName;
        private string location;
        private string photoType;

        #endregion

        #region 属性

        public string OrderId { get => orderId; set { orderId = value; NotifyPropertyChange(() => OrderId); } }
        public string ExecutionSystem { get => executionSystem; set { executionSystem = value; NotifyPropertyChange(() => ExecutionSystem); } }
        public bool IsSync { get => isSync; set { isSync = value; NotifyPropertyChange(() => IsSync); } }
        public string FileName { get => fileName; set { fileName = value; NotifyPropertyChange(() => FileName); } }
        public string Location { get => location; set { location = value; NotifyPropertyChange(() => Location); } }
        public string PhotoType { get => photoType; set { photoType = value; NotifyPropertyChange(() => PhotoType); } }
        public string Status { get => status; set { status = value; NotifyPropertyChange(() => Status); } }
        public DateTime? UploadTime { get => uploadTime; set { uploadTime = value; NotifyPropertyChange(() => UploadTime); } }
        public DateTime CreateTime { get => createDate; set { createDate = value; NotifyPropertyChange(() => CreateTime); } }

        /// <summary>
        /// 
        /// </summary>
        public string FileFullName
        {
            get
            {
                return Path.Combine(Location, FileName);
            }
        }

        #endregion

        #region 隐士转换

        /// <summary>
        /// 隐士转换
        /// </summary>
        /// <param name="row"></param>
        public static implicit operator UploadFile(DataRow row)
        {
            return new UploadFile
            {
                Id = row.IsNull("Id") ? string.Empty : row["Id"].ToString(),
                OrderId = row.IsNull("OrderId") ? string.Empty : row["OrderId"].ToString(),
                ExecutionSystem = row.IsNull("ExecutionSystem") ? string.Empty : row["ExecutionSystem"].ToString(),
                IsSync = row.IsNull("IsSync") ? false : Convert.ToBoolean(row["IsSync"]),
                FileName = row.IsNull("FileName") ? string.Empty : row["FileName"].ToString(),
                Location = row.IsNull("Location") ? string.Empty : row["Location"].ToString(),
                PhotoType = row.IsNull("PhotoType") ? string.Empty : row["PhotoType"].ToString(),
                Status = row.IsNull("Status") ? string.Empty : row["Status"].ToString(),
                UploadTime = row.IsNull("UploadTime") ? (DateTime?)null : Convert.ToDateTime(row["UploadTime"]),
                CreateTime = row.IsNull("CreateTime") ? DateTime.Now : Convert.ToDateTime(row["CreateTime"])
            };
        }

        #endregion

        #region 方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override Response<int> Create(string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                sql = string.Format(@"INSERT INTO [UploadFile]
                                                  (
                                                      Id,
                                                      OrderId,
                                                      ExecutionSystem,
                                                      IsSync,
                                                      FileName,
                                                      Location,
                                                      PhotoType,
                                                      Status,
                                                      UploadTime,
                                                      CreateTime
                                                  )
                                                  VALUES
                                                  (   '{0}',
                                                      '{1}', 
                                                      '{2}', 
                                                          0,    
                                                      '{3}',
                                                      '{4}',
                                                      '{5}',
                                                      '{6}',
                                                      NULL,
                                                      DATETIME()
                                                      )", Id, OrderId, ExecutionSystem, FileName, Location, PhotoType, Status);
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
                sql = string.Format(@"UPDATE [UploadFile] SET IsSync = '{0}',
                                                      FileName = '{1}',
                                                      Location = '{2}',
                                                      PhotoType = '{3}',
                                                      Status = '{4}',
                                                      UploadTime = {5}", IsSync, FileName, Location, PhotoType, Status, UploadTime.HasValue ? string.Format("'{0}'", UploadTime.Value.ToString("yyyy-MM-dd hh:mm:ss")) : "'NULL'", CreateTime);
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
                sql = string.Format("DELETE FROM [UploadFile] WHERE Id = '{0}'", Id);
            }

            File.Delete(FileFullName);

            return base.Delete(sql);
        }

        #endregion
    }
}
