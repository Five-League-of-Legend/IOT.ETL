using IOT.ETL.Common;
using IOT.ETL.IRepository.TaskIRepository;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IOT.ETL.Repository.TaskRepository
{
    public class TaskRepository : TaskIRepository
    {
        //Redis缓存
        string str = "strTask";
        List<IOT.ETL.Model.etl_task_info> joinls = new List<Model.etl_task_info>();
        RedisHelper<IOT.ETL.Model.etl_task_info> rh = new RedisHelper<Model.etl_task_info>();
        public TaskRepository()
        {
            joinls = rh.GetList(str);
        }
        public async Task<int> AddTask(IOT.ETL.Model.etl_task_info ta)
        {
            string sql = $"insert into etl_task_info values(UUID(),'{ta.Name}',{ta.Weight},0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,null,0,null,now(),null,now())";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                List<Model.etl_task_info> ls = await DapperHelper.GetList<IOT.ETL.Model.etl_task_info>("select * from etl_task_info order by update_time DESC limit 1");
                ta = ls.FirstOrDefault();
                joinls.Add(ta);
                rh.SetList(joinls, str);
            }
            return i;
        }

        public async Task<int> DelTask(string id = "")
        {
            string sql = $"delete from etl_task_info where id = '{id}'";
            int i = await DapperHelper.Execute(sql);
            if (i>0)
            {
                IOT.ETL.Model.etl_task_info ls = joinls.First(x => x.Id.Equals(id));
                joinls.Remove(ls);
                rh.SetList(joinls, str);
            }
            return i;
        }

        public async Task<List<IOT.ETL.Model.etl_task_info>> ShowTask()
        {

            if (joinls == null || joinls.Count == 0)
            {
                string sql = $"select * from etl_task_info";
                joinls =await DapperHelper.GetList<Model.etl_task_info>(sql);
                rh.SetList(joinls, str);
            }

            return joinls;
        }


    }
}
