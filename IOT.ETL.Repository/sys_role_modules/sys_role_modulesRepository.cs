using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Common;
using IOT.ETL.IRepository.sys_role_modules;

namespace IOT.ETL.Repository.sys_role_modules
{
    public class sys_role_modulesRepository: Isys_role_modulesRepository
    {
        RedisHelper<Model.sys_role_modules> rm = new RedisHelper<Model.sys_role_modules>();
        string redisKey;
        List<Model.sys_role_modules> rms = new List<Model.sys_role_modules>();

        public sys_role_modulesRepository()
        {
            redisKey = "role_modules";
            rms = rm.GetList(redisKey);
        }
        public List<Model.sys_role_modules> Uptft(string id)
        {
            string sql = $"select * from  sys_role_modules where id={id}";
            return DapperHelper.GetList<Model.sys_role_modules>(sql);
        }
        public int Uptuser(Model.sys_role_modules a)
        {
            //先把当前角色id的都删了
            //然后再循环添加
           
            string sql = $"Update sys_role_modules set module_id='{a.module_id}' where role_id='{a.role_id}'";
            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                //sql = "select * from sys_role_modules";
                //rms=DapperHelper.GetList<Model.sys_role_modules>(sql);
                //int aa = rms.IndexOf(rms.FirstOrDefault(x => x.id == a.id));
                //rms[aa] = a;
                //rms.FirstOrDefault(x => x.role_id.Equals(a.role_id)).module_id = a.module_id;
                Model.sys_role_modules mm = rms.FirstOrDefault(x => x.role_id.Equals(a.role_id));
                rm.SetList(rms, redisKey);
            }
            return i;

        }

        public int Adds(Model.sys_role_modules m)
        {
            string sql = $"insert into sys_role_modules values(uuid(),'{m.role_id}','{m.module_id}')";

            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                m = DapperHelper.GetList<Model.sys_role_modules>("select * from sys_role_modules order by id desc LIMIT 1").FirstOrDefault();
                List<Model.sys_role_modules> ss = new List<Model.sys_role_modules>();
                ss.Add(m);
                rms = ss;
                rm.SetList(rms, redisKey);
            }
            return i;
        }
        public object cha(string id)
        {
            string ss = "select * from sys_role_modules";
            rms = DapperHelper.GetList<Model.sys_role_modules>(ss);
            rm.SetList(rms, redisKey);
            string sql = $"select module_id from sys_role_modules where role_id='{id}'";
            object aa= DapperHelper.Exescalar(sql);
            return aa;
        }


    }
}
