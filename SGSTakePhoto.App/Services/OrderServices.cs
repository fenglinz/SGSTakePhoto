using SGSTakePhoto.Infrastructure;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderServices : BaseServices<Order>
    {
        public Response<int> Create(Order entity)
        {
            return entity.Create();
        }

        public Response<int> Delete(Order entity)
        {
            return entity.Delete();
        }

        public ResponseSet<Order> GetList(string sql)
        {
            Response<DataTable> result = SqLiteHelper.ExecuteDataTable(sql);

            if (!result.Success)
            {
                return new ResponseSet<Order> { ErrorMessage = result.ErrorMessage };
            }
            if (result.Data == null || result.Data.Rows.Count <= 0)
            {
                return new ResponseSet<Order> { ErrorMessage = "No Data" };
            }

            ObservableCollection<Order> datas = new ObservableCollection<Order>();
            result.Data.Rows.Cast<Order>().ToList().ForEach(x => datas.Add(x));

            return new ResponseSet<Order> { Datas = datas };
        }

        public Response<Order> SingleOrDefault(string sql)
        {
            Response<DataTable> result = SqLiteHelper.ExecuteDataTable(sql);

            if (!result.Success)
            {
                return new Response<Order> { ErrorMessage = result.ErrorMessage };
            }
            if (result.Data == null || result.Data.Rows.Count <= 0)
            {
                return new Response<Order> { ErrorMessage = "No Data" };
            }


            return new Response<Order> { Data = result.Data.Rows.Cast<Order>().FirstOrDefault() };
        }

        public Response<int> Update(Order entity)
        {
            return entity.Update();
        }
    }
}
