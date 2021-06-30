using IOT.ETL.Common;
using IOT.ETL.IRepository.sys_modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Repository.sys_modules
{
    public class sys_modulesRepository: Isys_modulesRepository
    {
        RedisHelper<Model.sys_modules> md = new RedisHelper<Model.sys_modules>();
        string redisKey;
        List<Model.sys_modules> lst = new List<Model.sys_modules>();
        public sys_modulesRepository()
        {
            redisKey = "sys_modules_list";
            lst = md.GetList(redisKey);
            
        }

        public List<Dictionary<string, object>> BindTree()
        {
            string sql = "select * from sys_modules";
            List<Model.sys_modules> ls = DapperHelper.GetList<Model.sys_modules>(sql);
            List<Dictionary<string, object>> result = Recursion(ls, "0");
            return result;
        }
        /// <summary>
        /// 递归方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> Recursion(List<Model.sys_modules> lst, string pid)
        {
            //字典集合
            List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
            //获取所有的子节点集合
            List<Model.sys_modules> lstson = lst.Where(x => x.p_id.Equals(pid)).ToList();
            //循环所有的子节点
            foreach (var item in lstson)
            {
                Dictionary<string, object> jsonsub = new Dictionary<string, object>();
                jsonsub.Add("value", item.id);
                jsonsub.Add("label", item.name);
                if (lst.Count(x => x.p_id == item.id) > 0)
                {
                    jsonsub.Add("children", Recursion(lst, item.id));
                }
                json.Add(jsonsub);
            }
            return json;
        }
        public List<Model.sys_modules> GetSys_Modules()
        {
            string sql = "select * from sys_modules";
            if (lst==null || lst.Count==0)
            {
                lst = DapperHelper.GetList<Model.sys_modules>(sql);
                md.SetList(lst, redisKey);
            }
            return lst;
        }

    }
}
