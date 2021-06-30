using IOT.ETL.Common;
using IOT.ETL.IRepository.BI_DataAnalysis;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.BI_DataAnalysis
{
    public class BI_DataAnalysisRepository : IBI_DataAnalysisRepositor
    {
        //实例化redis缓存帮助类
        RedisHelper<GetDataBases> dh = new RedisHelper<GetDataBases>();
        string dataredisKey;
        List<GetDataBases> listd = new List<GetDataBases>();

        //实例化redis缓存帮助类
        RedisHelper<GetTables> th = new RedisHelper<GetTables>();
        string tableredisKey;
        List<GetTables> listt = new List<GetTables>();

        public BI_DataAnalysisRepository()
        {
            dataredisKey = "DataBase_List";
           

            tableredisKey = "Table_List";

        }

        public async Task<List<GetDataBases>> GetDatabaseName(int flag)
        {            
            string sql = "";
            string name = "";
            if (flag==1)
            {
                sql = " SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA; ";

                dataredisKey = "dataredisKey_MySql";
            }
            else if(flag==2)
            {
                sql = " SELECT NAME SCHEMA_NAME FROM MASTER.DBO.SYSDATABASES ORDER BY NAME ";
                name = "master";
                dataredisKey = "dataredisKey_SqlServe";

            }


            listd = dh.GetList(dataredisKey);
            //判断缓存数据是否存在
            if (listd == null || listd.Count == 0)
            {
                //拿到所有数据
                listd =await DapperHelper.GetList_BI<GetDataBases>(sql,name,flag);
                //放入缓存
                dh.SetList(listd, dataredisKey);
            }
            return listd;
        }

        public async Task<List<GetTables>> GetTableName(string databasename, int flag)
        {
            string sql = "";
            if (flag == 1)
            {
                sql = $" select Table_Name from information_schema.tables where table_schema = '{databasename}'; ";
            }
            else if (flag == 2)
            {
                sql = $"use {databasename} SELECT NAME Table_Name FROM SYSOBJECTS WHERE XTYPE='U' ORDER BY NAME ";
            }

            //拼接key
            tableredisKey = "tableredisKey_" + flag+"-" + databasename;

            //根据数据库名获取redis缓存
            listt = th.GetList(tableredisKey);

            //判断缓存是否为空
            if (listt == null || listt.Count == 0)
            {
                //拿到所有数据
                listt = await DapperHelper.GetList_BI<GetTables>(sql,databasename,flag);
                //放入缓存
                th.SetList(listt, tableredisKey);
            }

            return listt;
        }

        /// <summary>
        /// 根据数据库名称获取其中表数据MySql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        public async Task<string> GetDataTable(string sql, string name)
        {
            string json =await DapperHelper.GetDataTable(sql, name);

            return json;
        }

        /// <summary>
        /// 根据数据库名称获取其中表数据SqlServer
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        public async Task<string> GetDataTableSql(string sql, string name)
        {
            string json = await DapperHelper.GetDataTableSql(sql, name);

            return json;
        }


        public async Task<object> GetSelTime()
        {
            string sql = "";
            return await DapperHelper.Exescalar(sql);
        }


    }
}
