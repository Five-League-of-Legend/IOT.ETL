using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_user;
using IOT.ETL.Common;
using IOT.ETL.Model;

namespace IOT.ETL.Repository.sys_user
{

    public class sys_userRepository : Isys_userRepository
    {
        RedisHelper<Model.sys_user> us = new RedisHelper<Model.sys_user>();
        RedisHelper<Model.sys_user> loginre = new RedisHelper<Model.sys_user>();
        string redisKey;
        string redislogin;
        List<Model.sys_user> lst = new List<Model.sys_user>();
        List<Model.sys_user> loginls = new List<Model.sys_user>();

        public sys_userRepository()
        {
            redisKey = "sys_user_list";
            lst = us.GetList(redisKey);
            redislogin = "loginlist";
            loginls = loginre.GetList(redislogin);
        }
        public async Task<List<Model.sys_user>> Query()
        {
            lst = null;
         //   string sql = "select * from sys_user";
            string sql = "select a.id,a.name,a.username,c.id roleid,a.phone,a.status,a.email from sys_user a join sys_user_role b on a.id=b.user_id join sys_role c on b.role_id=c.id";
            if (lst == null || lst.Count == 0)
            {
                lst = await DapperHelper.GetList<Model.sys_user>(sql);
                us.SetList(lst, redisKey);
            }
            return lst;
        }
        public async Task<List<Model.sys_role>> Bang()
        {
            string sql = $"select id,role_name from sys_role";
            List<Model.sys_role> ls = await DapperHelper.GetList<Model.sys_role>(sql);
            return ls;
        }
        public async Task<int> Insert(Model.sys_user a)
        {

            Model.sys_user mm = loginls.FirstOrDefault();
            string id= Guid.NewGuid().ToString();

            string sql = $"insert into sys_user values('{id}','{a.name}','{a.email}','{a.phone}','{a.img_url}','{a.username}','{a.password}','{a.is_admin}','{a.status}','{a.revision}','{mm.name}',now(),'{mm.name}',now())";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                string sql2 = $"insert into sys_user_role values(uuid(),'{id}','{a.role_id}')";
                int m = await DapperHelper.Execute(sql2);
                if (m>0)
                {


                }

                List<Model.sys_user> lu = await DapperHelper.GetList<Model.sys_user>("select * from sys_user order by create_time desc LIMIT 1");
                a = lu.FirstOrDefault();
                lst.Add(a);
                us.SetList(lst, redisKey);
            }
            return i;
        }
        public async Task<int> UptState(string id)
        {


            List<Model.sys_user> list = await DapperHelper.GetList<IOT.ETL.Model.sys_user>($"select * from sys_user where id='{id}'");
            Model.sys_user ls = list.FirstOrDefault();
            if (ls.status == 0)
            {
                ls.status = 1;
            }
            else
            {
                ls.status = 0;
            }
            string sql = $"Update sys_user set status='{ls.status}' where id='{ls.id}'";
            int i= await DapperHelper.Execute(sql);
            if (i>0)
            {
                lst.FirstOrDefault(x => x.id.Equals(id)).status = ls.status;
                //lst.Where(x => x.id == id).FirstOrDefault().status = ls.status;

                //foreach (var item in lst)
                //{
                //    if (item.id==id)
                //    {
                //        if (ls.status == 0)
                //        {
                //            item.status = 1;
                //        }
                //        else
                //        {
                //            item.status = 0;
                //        }
                //    }
                //}

                us.SetList(lst, redisKey);

            }
            return i;
        }

        public async Task<int> Uptuser(Model.sys_user a)
        {
            Model.sys_user mm = loginls.FirstOrDefault();
            string sql = $"Update sys_user set name='{a.name}',email='{a.email}',phone='{a.phone}',img_url='{a.img_url}',username='{a.username}',password='{a.password}',is_admin='{a.is_admin}',status='{a.status}',revision='{a.revision}',create_by='{mm.name}',create_time=now(),update_by='{mm.name}',UPDATED_TIME=now() where id='{a.id}'";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                lst[lst.IndexOf(lst.FirstOrDefault(x => x.id == a.id))] = a;
                us.SetList(lst, redisKey);
            }
            return i;

        }
        public async Task<int> DelUser(string ids)
        {
            string sql = $"delete from sys_user where id='{ids}'";
             
            int i= await DapperHelper.Execute(sql);
            if (i>0)
            {
                string[] arr = ids.Split(',');
                foreach (var item in arr)
                {
                    Model.sys_user uu = lst.FirstOrDefault(x => x.id.Equals(item));
                    lst.Remove(uu);
                }
                us.SetList(lst, redisKey);
            }
            return i;

        }
     


       

    }
}
