using IOT.ETL.Common;
using IOT.ETL.IRepository.sysy_user_role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.sys_user_role
{
    public class sys_user_roleRepository: Isys_user_roleRepository
    {
        RedisHelper<Model.sys_user_role> ur = new RedisHelper<Model.sys_user_role>();
        string redisKey;
        List<Model.sys_user_role> lst = new List<Model.sys_user_role>();
        public sys_user_roleRepository()
        {
            redisKey = "sys_ruser_list";
            lst = ur.GetList(redisKey);
        }
        public async Task<int> Add(Model.sys_user_role m)
        {
            string sql = $"insert into sys_user_role values(uuid(),'{m.role_id}','{m.user_id}')";
    
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                List<Model.sys_user_role> ls = await DapperHelper.GetList<Model.sys_user_role>("select * from sys_user_role order by id desc LIMIT 1");
                m = ls.FirstOrDefault();
                List<Model.sys_user_role> ss = new List<Model.sys_user_role>();
                ss.Add(m);
                lst = ss;
                ur.SetList(lst, redisKey);
            }
            return i;
        }

    }
}
