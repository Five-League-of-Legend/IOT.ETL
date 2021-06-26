using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.IRepository.etl_task_info;
using IOT.ETL.Common;

namespace IOT.ETL.Repository.etl_task_info
{
    public class etl_task_info_Repository : etl_task_info_IRepository
    {
        //实例化缓存帮助类
        RedisHelper<Model.etl_task_info> rh = new RedisHelper<Model.etl_task_info>();
        //创建一个缓存关键字
        string redisKey;
        //全部数据的集合
        List<Model.etl_task_info> lst = new List<Model.etl_task_info>();
        public etl_task_info_Repository()
        {
            redisKey = "etl_task_info_list";
            lst = rh.GetList(redisKey);
        }


        public async Task<List<Model.etl_task_info>> GetList_etl_task_info()
        {
            try
            {
                string sql = "select * from etl_task_info ";
                List<Model.etl_task_info> list =await DapperHelper.GetList<Model.etl_task_info>(sql);
                //判断缓存数据是否存在
                if (lst == null || lst.Count == 0)
                {
                    lst = list;
                    rh.SetList(lst, redisKey);
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
