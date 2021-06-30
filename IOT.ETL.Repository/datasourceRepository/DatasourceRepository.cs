using IOT.ETL.Common;
using IOT.ETL.IRepository.IdatasourceRepository;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace IOT.ETL.Repository.datasourceRepository
{
    public class DatasourceRepository : IdatasourceRepository
    {
        
        DapperHelper dh = new DapperHelper();


        /// <summary>
        /// redis && mysql 数据源添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public async Task<int> DatasourceAdd(etl_datasource_info m)
        {
            string sql = $"insert into etl_datasource_info(type,url,port,username,password,database_name,param,status,remark,describes)values" +
                $"('{m.type}','{m.url}','{m.port}','{m.username}','{m.password}','{m.database_name}','{m.param}','{m.status}','{m.remark}','{m.describes}')";
            return await DapperHelper.Execute(sql);
        }
        /// <summary>
        /// 数据源修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public async Task<int> DatasourceUpt(etl_datasource_info m)
        {
            string sql = $"update etl_datasource_info set type = {m.type},url = '{m.url}',port = '{m.port}',username = '{m.username}',password = '{m.password}',database_name = '{m.database_name}'," +
                $"param = '{m.param}',status = '{m.status}',remark = '{m.remark}',describes = '{m.describes}' where id = {m.id}";
            return await DapperHelper.Execute(sql);
        }
        /// <summary>
        /// 数据源删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int> DatasourceDel(string ids)
        {
            string sql = $"delete from etl_datasource_info where id in ({ids})";
            return await DapperHelper.Execute(sql);
        }
        /// <summary>
        /// 显示列表 + 查询
        /// </summary>
        /// <param name="miaoshu"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<etl_datasource_info>> DatasourceList()
        {
            string sql = "SELECT * from etl_datasource_info a LEFT JOIN etl_datasource_type b on a.type = b.Tid ORDER BY a.create_time DESC";
            return await DapperHelper.GetList<etl_datasource_info>(sql); 
        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<etl_datasource_type>> Getdatasource_type() 
        {
            string sql = "select * from etl_datasource_type";
            return await DapperHelper.GetList<etl_datasource_type>(sql);
        }
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public async Task<int> CommWhy(string url, string pwd, string mysqlname)
        {
            string str = $"server={url};user id=root;password={pwd};Charset=utf8;database={mysqlname}";
            //返回1代表连上了，返回0就是没连上，应该差不多吧，你试试看行不行  ok别关
            return await DapperHelper.Ceshi(str);
        }
    }
}
