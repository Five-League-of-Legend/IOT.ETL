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
        public List<Model.sys_user> Query()
        {
            lst = null;
            string sql = "select * from sys_user";
            if (lst == null || lst.Count == 0)
            {
                lst = DapperHelper.GetList<Model.sys_user>(sql);
                us.SetList(lst, redisKey);
            }
            return lst;
        }
        public int Insert(Model.sys_user a)
        {
            string sql2 = "select * from sys_user";
            List<Model.sys_user> listuser = DapperHelper.GetList<Model.sys_user>(sql2);
            if (listuser.Count!=0)
            {
                listuser = listuser.Where(x => x.name.Equals(a.name)).ToList();
                listuser = listuser.Where(x => x.username.Equals(a.username)).ToList();
                listuser = listuser.Where(x => x.email.Equals(a.email)).ToList();
                if (listuser.Count==0)
                {
                    string sql = $"insert into sys_user values(uuid(),'{a.name}','{a.email}','{a.phone}','{a.img_url}','{a.username}','{a.password}','{a.is_admin}','{a.status}','{a.revision}','{a.create_by}',now(),'{a.update_by}',now())";
                    int i = DapperHelper.Execute(sql);
                    if (i > 0)
                    {
                        a = DapperHelper.GetList<Model.sys_user>("select * from sys_user order by create_time desc LIMIT 1").FirstOrDefault();
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
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    a = DapperHelper.GetList<Model.sys_user>("select * from sys_user order by create_time desc LIMIT 1").FirstOrDefault();
                    lst.Add(a);
                    us.SetList(lst, redisKey);
                }
                return i;
            }
        }
        public int UptState(string id)
        {

            string sql = $"update sys_user set status=ABS(status-1) where id='{id}' ";
            return DapperHelper.Execute(sql);
        }

        public int Uptuser(Model.sys_user a)
        {
            string sql = $"Update sys_user set name='{a.name}',email='{a.email}',phone='{a.phone}',img_url='{a.img_url}',username='{a.username}',password='{a.password}',is_admin='{a.is_admin}',status='{a.status}',revision='{a.revision}',create_by='{a.create_by}',create_time='{a.create_time}',update_by='{a.update_by}',UPDATED_TIME='{a.UPDATED_TIME}' where id='{a.id}'";
            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                lst[lst.IndexOf(lst.FirstOrDefault(x => x.id == a.id))] = a;
                us.SetList(lst, redisKey);
            }
            return i;

        }
        public int DelUser(string id)
        {
            string sql = $"delete from sys_user where id='{id}'";
            return DapperHelper.Execute(sql);
        }
    }
}
