using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 用户操作服务
    /// </summary>
    public class UserServices : BaseServices<UserConfig>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Response<int> Create(UserConfig entity)
        {
            return entity.InsertOrReplace();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Response<int> Delete(UserConfig entity)
        {
            return entity.Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ResponseSet<UserConfig> GetList(string sql)
        {
            Response<DataTable> result = SqLiteHelper.ExecuteDataTable(sql);

            if (!result.Success)
            {
                return new ResponseSet<UserConfig> { ErrorMessage = result.ErrorMessage };
            }
            if (result.Data == null || result.Data.Rows.Count <= 0)
            {
                return new ResponseSet<UserConfig> { ErrorMessage = "No Data" };
            }

            ObservableCollection<UserConfig> datas = new ObservableCollection<UserConfig>();
            result.Data.Rows.Cast<DataRow>().ToList().ForEach(x => datas.Add(x));

            return new ResponseSet<UserConfig> { Datas = datas };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Response<UserConfig> SingleOrDefault(string sql)
        {
            Response<DataTable> result = SqLiteHelper.ExecuteDataTable(sql);

            if (!result.Success)
            {
                return new Response<UserConfig> { ErrorMessage = result.ErrorMessage };
            }
            if (result.Data == null || result.Data.Rows.Count <= 0)
            {
                return new Response<UserConfig> { ErrorMessage = "No Data" };
            }


            return new Response<UserConfig> { Data = result.Data.Rows.Cast<DataRow>().FirstOrDefault() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Response<int> Update(UserConfig entity)
        {
            return entity.InsertOrReplace();
        }
    }
}
