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
        RedisHelper<Model.sys_user> lo = new RedisHelper<Model.sys_user>();
        //缓存关键字
        string redisKey;
        string redisLogin;
        //获取全部数据
        List<Model.sys_role> rls = new List<Model.sys_role>();
        List<Model.sys_user> loginls = new List<Model.sys_user>();
        public sys_roleRepository()
        {
            redisKey = "role_list";
            rls = rl.GetList(redisKey);
            redisLogin = "loginlist";
            loginls = lo.GetList(redisLogin);
        }
        public async Task<List<Model.sys_role>> ShowRoles()
        {
            rls = null;
            try 
            {

               // string sql = "select a.id,a.role_name,c.name,c.id modules_id from sys_role a join sys_role_modules b on a.id=b.role_id  join sys_modules c on c.id=b.module_id";
                //string sql = "select a.id,a.role_name,c.name from sys_role a join sys_role_modules b on a.id=b.role_id  join sys_modules c on c.id=b.module_id";

               string sql = "select * from sys_role";
               
                if (rls == null || rls.Count == 1)
                {
                    rls = await DapperHelper.GetList<Model.sys_role>(sql);
                    rl.SetList(rls, redisKey);
                }
                return rls;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<int> DelRoles(string id)
        {
           

            string sql = $"delete from sys_role where id='{id}'";

            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                string[] arr = id.Split(',');
                foreach (var item in arr)
                {
                    Model.sys_role uu = rls.FirstOrDefault(x => x.id.Equals(item));
                    rls.Remove(uu);
                }
                rl.SetList(rls, redisKey);
            }
            return i;
        }
        //添加
        public async Task<int> insertRoles(Model.sys_role a)
        {
            Model.sys_user mm = loginls.FirstOrDefault();
            string id = Guid.NewGuid().ToString();
            string sql = $"insert into sys_role VALUES('{id}','{a.role_name}','{a.role_status}','{a.revision}','{mm.name}',now(),'{mm.name}',now())";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                //  string sql2 = $"insert into sys_role_modules values(uuid(),'{id}','{a.module_id}')";
                // int m = DapperHelper.Execute(sql2);
                // if (m>0)
                // {

                // }
                List<Model.sys_role> ls = await DapperHelper.GetList<Model.sys_role>("select * from sys_role order by id desc LIMIT 1");
                Model.sys_role mc = ls.FirstOrDefault();
                a = mc;
                rls.Add(a);
                rl.SetList(rls, redisKey);
            }
  
            return i;
        }
        public async Task<int> UpdateRoles(Model.sys_role a)
        {
            Model.sys_user mm = loginls.FirstOrDefault();
            string sql = $"Update sys_role set role_name='{a.role_name}',role_status='{a.role_status}',revision='{a.revision}',create_by='{mm.name}',create_time=now(),update_by='{mm.name}',update_time=now() where id='{a.id}'";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                rls[rls.IndexOf(rls.FirstOrDefault(x => x.id == a.id))] = a;
                rl.SetList(rls, redisKey);
            }
            return i;
        }

    }
}
