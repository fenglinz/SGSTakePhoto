using SGSTakePhoto.Infrastructure;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 文件服务类
    /// </summary>
    public class UploadFileServices : BaseServices<UploadFile>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Response<int> Create(UploadFile entity)
        {
            return entity.InsertOrReplace();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Response<int> Delete(UploadFile entity)
        {
            return entity.Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ResponseSet<UploadFile> GetList(string sql)
        {
            Response<DataTable> result = SqLiteHelper.ExecuteDataTable(sql);

            if (!result.Success)
            {
                return new ResponseSet<UploadFile> { ErrorMessage = result.ErrorMessage };
            }
            if (result.Data == null || result.Data.Rows.Count <= 0)
            {
                return new ResponseSet<UploadFile> { ErrorMessage = "No Data" };
            }

            ObservableCollection<UploadFile> datas = new ObservableCollection<UploadFile>();
            result.Data.Rows.Cast<DataRow>().ToList().ForEach(x => datas.Add(x));

            return new ResponseSet<UploadFile> { Datas = datas };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Response<UploadFile> SingleOrDefault(string sql)
        {
            Response<DataTable> result = SqLiteHelper.ExecuteDataTable(sql);

            if (!result.Success)
            {
                return new Response<UploadFile> { ErrorMessage = result.ErrorMessage };
            }
            if (result.Data == null || result.Data.Rows.Count <= 0)
            {
                return new Response<UploadFile> { ErrorMessage = "No Data" };
            }


            return new Response<UploadFile> { Data = result.Data.Rows.Cast<DataRow>().FirstOrDefault() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Response<int> Update(UploadFile entity)
        {
            return entity.InsertOrReplace();
        }
    }
}
