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

        private string id;
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

        public string Id { get => id; set { id = value; NotifyPropertyChange(() => Id); } }
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

            };
        }
    }
}
