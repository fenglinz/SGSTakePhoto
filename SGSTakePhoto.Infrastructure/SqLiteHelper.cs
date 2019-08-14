using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace SGSTakePhoto.Infrastructure
{
    public class SqLiteHelper
    {
        #region 链接字符串

        /// <summary>
        /// 链接字符串
        /// </summary>
        public static string SqLiteConnectionString = @"Data Source=|DataDirectory|\Data\SgsLocalData.db;Pooling=true;FailIfMissing=false";

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Response<int> ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            int result = -1;
            try
            {
                using (SQLiteConnection sqlCon = new SQLiteConnection(SqLiteConnectionString))
                {
                    sqlCon.Open();
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        cmd.CommandText = sql;//sql查询语句
                        foreach (SQLiteParameter parameter in parameters)
                        {
                            if (!cmd.Parameters.Contains(parameter))
                                cmd.Parameters.Add(parameter);
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                }

                return new Response<int> { Data = result };
            }
            catch (SQLiteException ex)
            {
                return new Response<int> { ErrorMessage = ex.Message };
            }
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Response<object> ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            object result = null;
            try
            {
                using (SQLiteConnection sqlCon = new SQLiteConnection(SqLiteConnectionString))
                {
                    sqlCon.Open();
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        if (parameters.Any())
                        {
                            foreach (SQLiteParameter parameter in parameters)
                            {
                                if (!cmd.Parameters.Contains(parameter))
                                    cmd.Parameters.Add(parameter);
                            }
                        }
                        result = cmd.ExecuteScalar();
                    }
                }

                return new Response<object> { Data = result };
            }
            catch (SQLiteException ex)
            {
                return new Response<object> { ErrorMessage = ex.Message };
            }
        }

        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Response<DataTable> ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection sqlCon = new SQLiteConnection(SqLiteConnectionString))
                {
                    sqlCon.Open();
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        foreach (SQLiteParameter parameter in parameters)
                        {
                            if (!cmd.Parameters.Contains(parameter))
                                cmd.Parameters.Add(parameter);
                        }
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                return new Response<DataTable> { Data = dt };
            }
            catch (SQLiteException ex)
            {
                return new Response<DataTable> { ErrorMessage = ex.Message };
            }
        }

        #endregion

        #region ExecuteBulkCopy

        /// <summary>
        /// ExecuteBulkCopy
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="table"></param>
        public static Response<int> ExecuteBulkCopy(string sql, DataTable table)
        {
            try
            {
                using (SQLiteConnection sqlCon = new SQLiteConnection(SqLiteConnectionString))
                {
                    sqlCon.Open();
                    SQLiteTransaction transcation = sqlCon.BeginTransaction();
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        bool isFirst = true;
                        foreach (DataRow row in table.Rows)
                        {
                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                SQLiteParameter parameter = new SQLiteParameter
                                {
                                    ParameterName = string.Format("@{0}", table.Columns[i].ColumnName),
                                    DbType = DataTypeToDbType(table.Columns[i].DataType),
                                    Value = row[i]
                                };
                                if (isFirst)
                                    cmd.Parameters.Add(parameter);
                                else
                                    cmd.Parameters[i].Value = row[i];
                            }
                            isFirst = false;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transcation.Commit();
                }

                return new Response<int> { };
            }
            catch (SQLiteException ex)
            {
                return new Response<int> { ErrorMessage = ex.Message };
            }
        }

        #endregion

        #region GetFilterTables

        /// <summary>
        /// 获取基础表
        /// </summary>
        /// <returns></returns>
        //public static ResponseSet<string> GetFilterTables(bool isEnable = false, bool isBasic = true)
        //{
        //    var response = ExecuteDataTable(string.Format("SELECT Name FROM BaseDataTable WHERE IsEnable = {0} {1}", isEnable ? 1 : 0, isBasic ? "AND IsBasic = 1" : string.Empty));
        //    if (!response.Success) return new ResponseSet<string> { Message = response.Message };
        //    ObservableCollection<string> lstTbNames = new ObservableCollection<string>();
        //    foreach (DataRow row in response.Data.Rows)
        //    {
        //        lstTbNames.Add(row[0].ToString());
        //    }

        //    return new ResponseSet<string> { Datas = lstTbNames };
        //}

        #endregion

        #region DataTypeToDbType

        /// <summary>
        /// DataTypeToDbType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static DbType DataTypeToDbType(Type type)
        {
            switch (type.Name)
            {
                case "Guid":
                case "String":
                    return DbType.String;
                case "DateTime":
                    return DbType.DateTime;
                case "Boolean":
                    return DbType.Boolean;
                default:
                    return DbType.String;
            }
        }

        #endregion
    }
}
