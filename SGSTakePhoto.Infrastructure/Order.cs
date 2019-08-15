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

        /// <summary>
        /// 
        /// </summary>
        public string RelativePath
        {
            get
            {
                return string.Format(@"{0}\{1}\{2}", string.Empty, DateTime.Now.ToString("yyyy-MM-dd"), CaseNum);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public static implicit operator Order(DataRow row)
        {
            return new Order
            {
                Id = row.IsNull("Id") ? string.Empty : row["Id"].ToString(),
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
        public override Response<int> Create(string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                sql = string.Format(@"INSERT INTO [Order]
                  (
                      Id,
                      CaseNum,
                      JobNum,
                      OrderNum,
                      SampleID,
                      TestItemID,
                      Status,
                      Owner,
                      CreateTime,
                      IsChecked
                  )
                  VALUES
                  ('{0}',
                   '{1}',
                   '{2}', 
                   '{3}',
                   '{4}',
                   '{5}',
                   '{6}',
                   '{7}',
                   DATETIME(),
                   0
                   )", Id, CaseNum, JobNum, OrderNum, SampleID, TestItemID, Status, Owner);
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
                sql = string.Format(@"UPDATE [Order] SET CaseNum = '{1}',JobNum = '{2}',OrderNum = '{3}',SampleID = '{4}',TestItemID = '{5}',Status = '{6}',Owner = '{7}' WHERE Id = '{0}'",
                    Id, CaseNum, JobNum, OrderNum, SampleID, TestItemID, Status, Owner);
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
                sql = string.Format(@"DELETE FROM [Order] WHERE ID = '{0}'", Id);
            }

            return base.Delete(sql);
        }
    }
}
