using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class NotifyBaseEntity
    {
        public StringBuilder sb = new StringBuilder();
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChange<T>(Expression<Func<T>> expression)
        {
            if (PropertyChanged != null)
            {
                var propertyName = ((MemberExpression)expression.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        //protected PropertyInfo[] Properties
        //{
        //    get
        //    {
        //        return this.GetType().GetProperties();
        //    }
        //}

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public virtual Response<int> Create(string sql = "")
        {
            //if (string.IsNullOrEmpty(sql))
            //{
            //    sb.Clear();
            //    foreach (PropertyInfo item in Properties)
            //    {
            //        foreach(CustomAttributeData cusAttribute in item.CustomAttributes)
            //        {
            //            if(cusAttribute.AttributeType == typeof(Column))
            //            {

            //            }
            //        }
            //    }
            //}

            return SqLiteHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public virtual Response<int> Update(string sql = "")
        {
            return SqLiteHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public virtual Response<int> Delete(string sql = "")
        {
            return SqLiteHelper.ExecuteNonQuery(sql);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="executionSystem"></param>
        ///// <returns></returns>
        //public virtual T GetEntity<T>(string userName, string executionSystem)
        //{
        //    return T;
        //}
    }
}
