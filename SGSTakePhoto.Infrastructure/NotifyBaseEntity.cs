using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SGSTakePhoto.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class NotifyBaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        protected void NotifyPropertyChange<T>(Expression<Func<T>> expression)
        {
            if (PropertyChanged != null)
            {
                var propertyName = ((MemberExpression)expression.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string id;
        [Column(PrimaryKey = true, ColumnName = "Id")]
        public string Id { get => id; set { id = value; NotifyPropertyChange(() => Id); } }

        /// <summary>
        /// 
        /// </summary>
        public NotifyBaseEntity()
        {
            Id = Guid.NewGuid().ToString();
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
        /// 更新
        /// </summary>
        /// <returns></returns>
        public virtual Response<int> InsertOrReplace(string sql = "")
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        //public virtual Response<T> SingleOrDefault<T>(string sql = "")
        //{
        //    Response<DataTable> result = SqLiteHelper.ExecuteDataTable(sql);

        //    if (!result.Success)
        //    {
        //        return new Response<T> { Errors = result.Errors };
        //    }
        //    if (result.Data == null || result.Data.Rows.Count <= 0)
        //    {
        //        return new Response<T> { Errors = "No Data" };
        //    }


        //    return new Response<T> { Data = result.Data.Rows.Cast<T>().FirstOrDefault() };
        //}
    }
}
