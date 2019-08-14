using System;
using System.Data;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// Upload
    /// </summary>
    public class Upload : NotifyBaseEntity
    {
        #region 字段

        private string id;
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

        public string Id { get => id; set { id = value; NotifyPropertyChange(() => Id); } }
        public string OrderId { get => orderId; set { orderId = value; NotifyPropertyChange(() => OrderId); } }
        public string ExecutionSystem { get => executionSystem; set { executionSystem = value; NotifyPropertyChange(() => ExecutionSystem); } }
        public bool IsSync { get => isSync; set { isSync = value; NotifyPropertyChange(() => IsSync); } }
        public string FileName { get => fileName; set { fileName = value; NotifyPropertyChange(() => FileName); } }
        public string Location { get => location; set { location = value; NotifyPropertyChange(() => Location); } }
        public string PhotoType { get => photoType; set { photoType = value; NotifyPropertyChange(() => PhotoType); } }
        public string Status { get => status; set { status = value; NotifyPropertyChange(() => Status); } }
        public DateTime? UploadTime { get => uploadTime; set { uploadTime = value; NotifyPropertyChange(() => UploadTime); } }
        public DateTime CreateTime { get => createDate; set { createDate = value; NotifyPropertyChange(() => CreateTime); } }

        #endregion

        #region 隐士转换

        /// <summary>
        /// 隐士转换
        /// </summary>
        /// <param name="row"></param>
        public static implicit operator Upload(DataRow row)
        {
            return new Upload
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
                sql = string.Format(@"INSERT INTO UploadFile
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
                                                  (   NEWID(),      -- Id - uniqueidentifier
                                                      '{0}',      -- OrderId - uniqueidentifier
                                                      '{1}',       -- ExecutionSystem - nvarchar(256)
                                                          0,         -- IsSync - int
                                                      '{2}',       -- FileName - nvarchar(512)
                                                      '{3}',       -- Location - nvarchar(512)
                                                      '{4}',        -- PhotoType - varchar(64)
                                                      '{5}',       -- Status - nvarchar(64)
                                                      NULL, -- UploadTime - datetime
                                                      GETDATE()  -- CreateTime - datetime
                                                      )", OrderId, ExecutionSystem, FileName, Location, PhotoType, Status);
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
            return base.Update(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override Response<int> Delete(string sql = "")
        {
            return base.Delete(sql);
        }

        #endregion
    }
}
