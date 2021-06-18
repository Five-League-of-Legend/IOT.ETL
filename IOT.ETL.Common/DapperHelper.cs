using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using Newtonsoft.Json;

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

        /// <summary>
        /// 根据数据库名称获取其中表数据MySql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        public static string GetDataTable(string sql, string name)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConfigurationManager.ConnName + name))
                {
                    var reader = db.Query(sql);

                    string json = JsonConvert.SerializeObject(reader);

                    return json;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 根据数据库名称获取其中表数据SqlServer
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        public static string GetDataTableSql(string sql, string name)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnNameSql + name))
                {
                    var reader = db.Query(sql);

                    string json = JsonConvert.SerializeObject(reader);

                    return json;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取数据_BI数据分析
        ///</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> GetList_BI<T>(string sql, string name, int flag = 1)
        {
            try
            {
                if (flag != 1)
                {
                    string conn = ConfigurationManager.ConnNameSql + name;
                    using (IDbConnection db = new SqlConnection(conn))
                    {
                        return db.Query<T>(sql).ToList();
                    }
                }
                else
                {
                    using (IDbConnection db = new MySqlConnection(ConfigurationManager.Conn))
                    {
                        return db.Query<T>(sql).ToList();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
