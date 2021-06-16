using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace IOT.ETL.Common
{
    public class DapperHelper
    {
        /// <summary>
        /// 获取数据
        /// MySql.Data.MySqlClient.MySqlException:“Unknown column 'a.WarehouseId' in 'field list'”</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return db.Query<T>(sql).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Execute(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return db.Execute(sql);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object Exescalar(string sql)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                {
                    return db.ExecuteScalar(sql);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
