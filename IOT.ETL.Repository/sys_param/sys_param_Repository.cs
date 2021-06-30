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

        public async Task<List<Dictionary<string, object>>> BindParent()
        {
            string sql = "SELECT *FROM sys_param ORDER BY order_by;";
            List<Model.sys_param> list =await DapperHelper.GetList<Model.sys_param>(sql);
            List<Dictionary<string, object>> Alltree = Recursion(list, "0");
            return Alltree;
        }
        /// <summary>
        /// 递归方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> Recursion(List<Model.sys_param> lst, string pid)
        {
            //字典集合
            List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
            //获取所有的子节点集合
            List<Model.sys_param> lstson = lst.Where(x => x.Pid.Equals(pid)).ToList();
            //循环所有的子节点
            foreach (var item in lstson)
            {
                Dictionary<string, object> jsonsub = new Dictionary<string, object>();
                jsonsub.Add("value", item.Id);
                jsonsub.Add("label", item.Name);
                if (lst.Count(x => x.Pid == item.Id) > 0)
                {
                    jsonsub.Add("children", Recursion(lst, item.Id));
                }
                json.Add(jsonsub);
            }
            return json;
        }

        public async Task<int> Delete_sys_param(string ids)
        {
            try
            {
                string sql = $"delete from sys_param WHERE id in ('{ids}')";
                int i =await DapperHelper.Execute(sql);
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

        public async Task<List<Model.sys_param>> GetList_sys_param(string pid)
        {
            lst = null;
            try
            {
                List<Model.sys_param> ls = new List<Model.sys_param>();
                //判断缓存是否存在
                if (lst == null || lst.Count == 0)
                {
                    lst =await DapperHelper.GetList<IOT.ETL.Model.sys_param>("SELECT a.*,b.name fname FROM sys_param a LEFT  JOIN sys_param b on a.pid=b.id ORDER BY a.order_by");


                    foreach (var s in lst)
                    {
                        foreach (var ss in lst)
                        {
                            if (s.Id == ss.Pid)
                            {
                                s.HasChildren = true;
                            }
                        }
                    }

                    //不存在
                    //按order_by排序  左连接 子节点在前 父节点在后
                    if (pid != "0" || pid != null || !string.IsNullOrEmpty(pid))
                    {
                        ls = lst.Where(x => x.Pid == pid).ToList();
                    }
                }
                if (ls.Count != 0)
                {
                    return ls;
                }
                if (pid == "0" || pid == null)
                {
                    return lst.Where(m => m.Pid == "0").ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<int> Insert_sys_param(Model.sys_param sys_Param)
        {
            try
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                string sql = $"INSERT INTO sys_param VALUES(UUID(),'{sys_Param.Code}','{sys_Param.Name}','{sys_Param.Pid}',{sys_Param.Default_status},{sys_Param.Is_system},{sys_Param.Is_del},{sys_Param.Order_by},'{sys_Param.Text}','{sys_User.name}',NOW(),'{sys_User.name}',NOW())";
                //string sql = $"INSERT INTO sys_param VALUES(UUID(),'{sys_Param.Code}','{sys_Param.Name}','{sys_Param.Pid}',{sys_Param.Default_status},{sys_Param.Is_system},{sys_Param.Is_del},{sys_Param.Order_by},'{sys_Param.Text}','{sys_Param.Create_by}',NOW(),'{sys_Param.Update_by}',NOW())";
                int i =await DapperHelper.Execute(sql);
                if (i > 0)
                {
                    string sql1 = "select * FROM sys_param ORDER BY create_time DESC LIMIT 1";
                    List<Model.sys_param> list =await DapperHelper.GetList<Model.sys_param>(sql1);
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

        public async Task<int> Uptdate_sys_param(Model.sys_param model)
        {
            try
            {
                //从缓存中取登录用户信息
                Model.sys_user sys_User = lstl.FirstOrDefault();
                string sql = $"UPDATE sys_param SET code='{model.Code}',name='{model.Name}',is_system={model.Is_system},default_status={model.Default_status},order_by={model.Order_by},text='{model.Text}',update_by='{sys_User.name}',update_time=NOW()  WHERE id='{model.Id}'";
                //string sql = $"UPDATE sys_param SET Code='{model.Code}',Name='{model.Name}',Pid={model.Pid},Default_status={model.Default_status},Is_system={model.Is_system},Is_del={model.Is_del},Order_by={model.Order_by},Text='{model.Text}',Create_by='{model.Create_by}',Create_time=NOW(),Update_by='{model.Update_by}',Update_time=NOW() WHERE id ='{model.Id}'";
                int i =await DapperHelper.Execute(sql);
                if (i > 0)
                {
                    lst[lst.IndexOf(lst.First(x => x.Id == model.Id))] = model;
                    //从新存入
                    rh.SetList(lst, redisKey);
                    return i;
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
