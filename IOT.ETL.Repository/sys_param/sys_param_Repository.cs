using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.IRepository.sys_param;
using IOT.ETL.Common;

namespace IOT.ETL.Repository.sys_param
{
    public class sys_param_Repository : sys_param_IRepository
    {
        //实例化缓存帮助类
        RedisHelper<Model.sys_param> rh = new RedisHelper<Model.sys_param>();
        RedisHelper<Model.sys_user> rl = new RedisHelper<Model.sys_user>();
        //创建一个缓存关键字
        string redisKey;
        string redisLogin;
        //全部数据的集合
        List<Model.sys_param> lst = new List<Model.sys_param>();
        //登录集合
        List<Model.sys_user> lstl = new List<Model.sys_user>();


        public sys_param_Repository()
        {
            redisKey = "sys_param_list";
            lst = rh.GetList(redisKey);
            redisLogin = "loginlist";
            lstl = rl.GetList(redisLogin);
        }


        public int Delete_sys_param(string ids)
        {
            try
            {
                string sql = $"delete from sys_param WHERE id in ('{ids}')";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    string[] arr = ids.Split(',');
                    foreach (var item in arr)
                    {
                        Model.sys_param me = lst.FirstOrDefault(x => x.Id.Equals(item));
                        lst.Remove(me);
                    }
                    //重新存入
                    rh.SetList(lst, redisKey);
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Model.sys_param> GetList_sys_param()
        {
            try
            {
                string sql = "select * from sys_param";
                List<Model.sys_param> list = DapperHelper.GetList<Model.sys_param>(sql);
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

        public int Insert_sys_param(Model.sys_param sys_Param)
        {
            try
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                string sql = $"INSERT INTO sys_param VALUES(UUID(),'{sys_Param.Code}','{sys_Param.Name}','{sys_Param.Pid}',{sys_Param.Default_status},{sys_Param.Is_system},{sys_Param.Is_del},{sys_Param.Order_by},'{sys_Param.Text}','{sys_User.name}',NOW(),'{sys_User.name}',NOW())";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    string sql1 = "select * FROM sys_param ORDER BY create_time DESC LIMIT 1";
                    List<Model.sys_param> list = DapperHelper.GetList<Model.sys_param>(sql1);
                    Model.sys_param me = list.FirstOrDefault();
                    //放入集合
                    lst.Add(me);
                    //重新放入缓存
                    rh.SetList(lst, redisKey);
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Uptdate_sys_param(Model.sys_param model)
        {
            try
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                string sql = $"UPDATE sys_param SET Code='{model.Code}',Name='{model.Name}',Pid={model.Pid},Default_status={model.Default_status},Is_system={model.Is_system},Is_del={model.Is_del},Order_by={model.Order_by},Text='{model.Text}',Create_by='{sys_User.name}',Create_time=NOW(),Update_by='{sys_User.name}',Update_time=NOW() WHERE id ='{model.Id}'";
                int i = DapperHelper.Execute(sql);
                if (i > 0)
                {
                    Model.sys_param me = lst.FirstOrDefault(x => x.Id.Equals(model.Id));
                    lst[lst.IndexOf(me)] = model;
                    //重新存入
                    rh.SetList(lst, redisKey);
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
