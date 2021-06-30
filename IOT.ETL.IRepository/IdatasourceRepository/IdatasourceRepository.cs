using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.IdatasourceRepository
{
    public interface IdatasourceRepository
    {
        //添加
        Task<int> DatasourceAdd(Model.etl_datasource_info m);
        //修改
        Task<int> DatasourceUpt(Model.etl_datasource_info m);
        //删除
        Task<int> DatasourceDel(string ids); 
        //显示列表+查询
        Task<List<Model.etl_datasource_info>> DatasourceList();
        //测试连接
        Task<int> CommWhy(string url, string pwd, string mysqlname);
        //下拉
        Task<List<Model.etl_datasource_type>> Getdatasource_type();
    }
}
