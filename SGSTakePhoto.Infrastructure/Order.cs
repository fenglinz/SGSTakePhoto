using System;
using System.Data;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// Order
    /// </summary>
    public class Order : NotifyBaseEntity
    {
        #region 字段

        private string executionSystem;
        private string caseNum;
        private string jobNum;
        private string orderNum;
        private string sampleID;
        private string testItemID;
        private string status;
        private string owner;
        private DateTime createTime;
        private bool isChecked;

        #endregion

        #region 属性

        public string ExecutionSystem { get => executionSystem; set { executionSystem = value; NotifyPropertyChange(() => ExecutionSystem); } }
        public string CaseNum { get => caseNum; set { caseNum = value; NotifyPropertyChange(() => CaseNum); } }
        public string JobNum { get => jobNum; set { jobNum = value; NotifyPropertyChange(() => JobNum); } }
        public string OrderNum { get => orderNum; set { orderNum = value; NotifyPropertyChange(() => OrderNum); } }
        public string SampleID { get => sampleID; set { sampleID = value; NotifyPropertyChange(() => SampleID); } }
        public string TestItemID { get => testItemID; set { testItemID = value; NotifyPropertyChange(() => TestItemID); } }
        public string Status { get => status; set { status = value; NotifyPropertyChange(() => Status); } }
        public string Owner { get => owner; set { owner = value; NotifyPropertyChange(() => Owner); } }
        public DateTime CreateTime { get => createTime; set { createTime = value; NotifyPropertyChange(() => CreateTime); } }
        public bool IsChecked { get => isChecked; set { isChecked = value; NotifyPropertyChange(() => IsChecked); } }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 用于保存选择的图片类型
        /// </summary>
        public string PhotoType { get; set; }

        /// <summary>
        /// 生成文件名
        /// </summary>
        public string GeneralFileName
        {
            get
            {
                return CommonHelper.GeneralPhotoName(this);
            }
        }

        /// <summary>
        /// 文件的绝对路径
        /// </summary>
        public string AbsolutePath
        {
            get
            {
                return CommonHelper.CurrentUploadPath(this);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public static implicit operator Order(DataRow row)
        {
            return new Order
            {
                Id = row.IsNull("Id") ? string.Empty : row["Id"].ToString(),
                ExecutionSystem = row.IsNull("ExecutionSystem") ? string.Empty : row["ExecutionSystem"].ToString(),
                CaseNum = row.IsNull("CaseNum") ? string.Empty : row["CaseNum"].ToString(),
                JobNum = row.IsNull("JobNum") ? string.Empty : row["JobNum"].ToString(),
                OrderNum = row.IsNull("OrderNum") ? string.Empty : row["OrderNum"].ToString(),
                SampleID = row.IsNull("SampleID") ? string.Empty : row["SampleID"].ToString(),
                TestItemID = row.IsNull("TestItemID") ? string.Empty : row["TestItemID"].ToString(),
                Status = row.IsNull("Status") ? string.Empty : row["Status"].ToString(),
                Owner = row.IsNull("Owner") ? string.Empty : row["Owner"].ToString(),
                CreateTime = row.IsNull("CreateTime") ? DateTime.Now : Convert.ToDateTime(row["CreateTime"]),
                IsChecked = row.IsNull("IsChecked") ? false : Convert.ToBoolean(row["IsChecked"])
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override Response<int> InsertOrReplace(string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                sql = string.Format(@"REPLACE INTO [Order] (Id, ExecutionSystem, CaseNum, JobNum, OrderNum, SampleID, TestItemID, Status, Owner, CreateTime, IsChecked)
                                                   VALUES ('{0}', '{1}',  '{2}',  '{3}', '{4}', '{5}', '{6}',  '{7}', '{8}', '{9}', '{10}')",
                                                   Id, ExecutionSystem, CaseNum, JobNum, OrderNum, SampleID, TestItemID, Status, Owner, CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), IsChecked ? 1 : 0);
            }

            return base.InsertOrReplace(sql);
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
                sql = string.Format(@"DELETE FROM [Order] WHERE ID = '{0}'", Id);
            }

            return base.Delete(sql);
        }
    }
}
