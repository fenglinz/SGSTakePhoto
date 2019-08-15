using SGSTakePhoto.Infrastructure;
using System.Collections.ObjectModel;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface BaseServices<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Response<int> Create(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Response<int> Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Response<int> Delete(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Response<T> SingleOrDefault(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        ResponseSet<T> GetList(string sql);
    }
}
