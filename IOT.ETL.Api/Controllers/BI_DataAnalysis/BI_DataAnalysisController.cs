using IOT.ETL.IRepository.BI_DataAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers.BI_DataAnalysis
{
    public enum enum_DataBase
    {
        MySQL = 1,
        SqlServer = 2
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BI_DataAnalysisController : ControllerBase
    {
        //日志
        Logger logger = NLog.LogManager.GetCurrentClassLogger();//实例化
        //BI数据分析接口
        private readonly IBI_DataAnalysisRepositor _iBI_DataAnalysisRepositor;

        //构造函数
        public BI_DataAnalysisController(IBI_DataAnalysisRepositor iBI_DataAnalysisRepositor)
        {
            _iBI_DataAnalysisRepositor = iBI_DataAnalysisRepositor;
        }


        //数据库树         public async Task<string>  DatabaseTree()
        [Route("/api/DatabaseTree")]
        [HttpGet]
        public async Task<string> DatabaseTree()
        {
            try
            {
                //获取MySql全部数据
                List<Model.GetDataBases> getDataBases = _iBI_DataAnalysisRepositor.GetDatabaseName(1);
                List<Model.GetDataBases> getDataBasesSql = _iBI_DataAnalysisRepositor.GetDatabaseName(2);

                //用于拼接的字符串
                StringBuilder builder = new StringBuilder();

                builder.Append("{");
                builder.Append("AAAid:1");
                builder.Append(",label:'MYSQL数据库'");
                builder.Append(",children:[");

                //MySql第一层循环 拼接数据库名
                for (int i = 0; i < getDataBases.Count; i++)
                {
                    builder.Append("{");
                    builder.Append("BBBid:" + (i + 1));
                    builder.Append(",label:'" + getDataBases[i].SCHEMA_NAME + "'");


                    List<Model.GetTables> getTables = _iBI_DataAnalysisRepositor.GetTableName(getDataBases[i].SCHEMA_NAME, 1);
                    builder.Append(",children:[");

                    //MySql第二层循环 拼接数据库下的表名
                    for (int j = 0; j < getTables.Count; j++)
                    {

                        builder.Append("{id:" + (j + 1));
                        builder.Append(",label:'" + getTables[j].Table_Name + "'},");
                    }

                    builder.Append("]},");

                }
                builder.Append("]},");

                builder.Append("{");
                builder.Append("AAAid:2");
                builder.Append(",label:'SqlServer数据库'");
                builder.Append(",children:[");

                //SqlServer第一层循环 拼接数据库名
                for (int i = 0; i < getDataBasesSql.Count; i++)
                {
                    builder.Append("{");
                    builder.Append("BBBid:" + (i + 1));
                    builder.Append(",label:'" + getDataBasesSql[i].SCHEMA_NAME + "'");


                    List<Model.GetTables> getTablesSql = _iBI_DataAnalysisRepositor.GetTableName(getDataBasesSql[i].SCHEMA_NAME, 2);
                    builder.Append(",children:[");

                    //SqlServer第二层循环 拼接数据库下的表名
                    for (int j = 0; j < getTablesSql.Count; j++)
                    {

                        builder.Append("{id:" + (j + 1));
                        builder.Append(",label:'" + getTablesSql[j].Table_Name + "'},");
                    }

                    builder.Append("]},");

                }
                builder.Append("]},");




                //添加日志
                logger.Debug($"显示数据库名及其表名，拼接树");

                //返回字符串并去掉末尾逗号
                return builder.ToString().TrimEnd(',');
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// 根据数据库名称获取其中表数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="name">数据库名称</param>
        /// <returns></returns>
        [Route("/api/SqlGetJson")]
        [HttpGet]
        public async Task<string> SqlGetJson(string sql, string name, int flag)
        {
            try
            {
                enum_DataBase ed = (enum_DataBase)flag;
                string json = "";
                switch (ed)
                {
                    case enum_DataBase.MySQL:
                        //获取全部数据
                        json = _iBI_DataAnalysisRepositor.GetDataTable(sql, name);
                        break;
                    case enum_DataBase.SqlServer:
                        //获取全部数据
                        json = _iBI_DataAnalysisRepositor.GetDataTableSql(sql, name);
                        break;
                }



                logger.Debug($"根据数据库名称，sql查询其数据库并返回，执行SQL为:{sql}");

                return json;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
