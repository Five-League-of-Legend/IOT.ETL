using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_user;
using IOT.ETL.Common;

namespace IOT.ETL.Repository.sys_user
{
    public class sys_userRepository : Isys_userRepository
    {
        RedisHelper<Model.sys_user> us = new RedisHelper<Model.sys_user>();
        string redisKey;
        List<Model.sys_user> lst = new List<Model.sys_user>();
        public sys_userRepository()
        {
            redisKey = "sys_user_list";
            lst = us.GetList(redisKey);
        }
        public async Task<List<Model.sys_user>> Query()
        {
            lst = null;
            string sql = "select * from sys_user";
            if (lst == null || lst.Count == 0)
            {
                lst = await DapperHelper.GetList<Model.sys_user>(sql);
                us.SetList(lst, redisKey);
            }
            return lst;
        }
        public async Task<int> Insert(Model.sys_user a)
        {
            string sql2 = "select * from sys_user";
            List<Model.sys_user> listuser =await DapperHelper.GetList<Model.sys_user>(sql2);
            if (listuser.Count!=0)
            {
                listuser = listuser.Where(x => x.name.Equals(a.name)).ToList();
                listuser = listuser.Where(x => x.username.Equals(a.username)).ToList();
                listuser = listuser.Where(x => x.email.Equals(a.email)).ToList();
                if (listuser.Count==0)
                {
                    string sql = $"insert into sys_user values(uuid(),'{a.name}','{a.email}','{a.phone}','{a.img_url}','{a.username}','{a.password}','{a.is_admin}','{a.status}','{a.revision}','{a.create_by}',now(),'{a.update_by}',now())";
                    int i =await DapperHelper.Execute(sql);
                    if (i > 0)
                    {
                        List<Model.sys_user> ccc = await DapperHelper.GetList<Model.sys_user>("select * from sys_user order by create_time desc LIMIT 1");
                        a = ccc.FirstOrDefault();
                        lst.Add(a);
                        us.SetList(lst, redisKey);
                    }
                    return i;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                string sql = $"insert into sys_user values(uuid(),'{a.name}','{a.email}','{a.phone}','{a.img_url}','{a.username}','{a.password}','{a.is_admin}','{a.status}','{a.revision}','{a.create_by}',now(),'{a.update_by}',now())";
                int i =await DapperHelper.Execute(sql);
                if (i > 0)
                {
                    List<Model.sys_user> ccc = await DapperHelper.GetList<Model.sys_user>("select * from sys_user order by create_time desc LIMIT 1");
                    a = ccc.FirstOrDefault();
                    lst.Add(a);
                    us.SetList(lst, redisKey);
                }
                return i;
            }
        }
        public async Task<int> UptState(string id)
        {

            string sql = $"update sys_user set status=ABS(status-1) where id='{id}' ";
            return await DapperHelper.Execute(sql);
        }

        public async Task<int> Uptuser(Model.sys_user a)
        {
            string sql = $"Update sys_user set name='{a.name}',email='{a.email}',phone='{a.phone}',img_url='{a.img_url}',username='{a.username}',password='{a.password}',is_admin='{a.is_admin}',status='{a.status}',revision='{a.revision}',create_by='{a.create_by}',create_time='{a.create_time}',update_by='{a.update_by}',UPDATED_TIME='{a.UPDATED_TIME}' where id='{a.id}'";
            int i = await DapperHelper.Execute(sql);
            if (i > 0)
            {
                lst[lst.IndexOf(lst.FirstOrDefault(x => x.id == a.id))] = a;
                us.SetList(lst, redisKey);
            }
            return i;

        }
        public async Task<int> DelUser(string id)
        {
            string sql = $"delete from sys_user where id='{id}'";
            return await DapperHelper.Execute(sql);
        }
    }
}
