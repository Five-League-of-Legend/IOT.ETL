using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_role;
using IOT.ETL.Common;

namespace IOT.ETL.Repository.sys_role
{
    public class sys_roleRepository : Isys_roleRepository
    {
        RedisHelper<Model.sys_role> rl = new RedisHelper<Model.sys_role>();
        string redisKey;
        List<Model.sys_role> rls = new List<Model.sys_role>();
        public sys_roleRepository()
        {
            redisKey = "role_list";
            rls = rl.GetList(redisKey);
        }
        public async Task<List<Model.sys_role>> ShowRoles()
        {
            string sql = "select * from sys_role";
            if (rls == null || rls.Count == 1)
            {
                rls =await DapperHelper.GetList<Model.sys_role>(sql);
                rl.SetList(rls, redisKey);
            }
            return rls;

        }
        public async Task<int> DelRoles(string id)
        {
            string sql = $"delete  from sys_role where id ='{id}'";
            return await DapperHelper.Execute(sql);
        }
        public async Task<int> insertRoles(Model.sys_role a)
        {
            a.id = Guid.NewGuid().ToString();
            string sql = $"insert into sys_role VALUES('{a.id}','{a.role_name}','{a.role_status}','{a.revision}','{a.create_by}','{a.create_time}','{a.update_by}','{a.update_time}')";
            int i =await DapperHelper.Execute(sql);
            if (i > 0)
            {
                List<Model.sys_role> ss = await DapperHelper.GetList<Model.sys_role>("select * from sys_role order by id desc LIMIT 1");
                a = ss.FirstOrDefault();
                rls.Add(a);
                rl.SetList(rls, redisKey);
            }

            return i;
        }
        public async Task<int> UpdateRoles(Model.sys_role a)
        {
            string sql = $"Update sys_role set role_name='{a.role_name}',role_status='{a.role_status}',revision='{a.revision}',create_by='{a.create_by}',create_time='{a.create_time}',update_by='{a.update_by}',update_time='{a.update_time}' where id='{a.id}'";
            int i =await DapperHelper.Execute(sql);
            if (i > 0)
            {
                rls[rls.IndexOf(rls.FirstOrDefault(x => x.id == a.id))] = a;
                rl.SetList(rls, redisKey);
            }
            return i;
        }
    }
}
