using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.ETL.Model;
using IOT.ETL.IRepository.etl_data_engine;
using IOT.ETL.Common;

namespace IOT.ETL.Repository.etl_data_engine
{
    public class etl_data_engine_Repository : etl_data_engine_IRepository
    {
        //实例化缓存帮助类
        RedisHelper<Model.etl_data_engine> rh = new RedisHelper<Model.etl_data_engine>();//规则引擎的缓存
        RedisHelper<Model.sys_user> ru = new RedisHelper<Model.sys_user>();//登录用户的缓存
        //创建一个缓存关键字
        string redisKey;//规则引擎缓存的关键字
        string redisLogin;//登录用户的缓存关键字
        //规则引擎数据的集合
        List<Model.etl_data_engine> lst = new List<Model.etl_data_engine>();
        //登录缓存数据集合
        List<Model.sys_user> listuser = new List<Model.sys_user>();
        public etl_data_engine_Repository()
        {
            //规则引擎
            redisKey = "etl_data_engine_list";
            lst = rh.GetList(redisKey);
            //登录
            redisLogin = "loginlist";
            listuser = ru.GetList(redisLogin);
        }
        //绑定规则引擎类型
        public async Task<List<etl_data_engine_type>> Binds()
        {
            string sql = "select * from etl_data_engine_type";
            List<Model.etl_data_engine_type> list = await DapperHelper.GetList<Model.etl_data_engine_type>(sql);
            return list;
        }
        /// <summary>
        /// 删除规则引擎的数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int> Delete_etl_data_engine(string ids)
        {
            //异常处理
            try
            {
                string sql = $"delete from etl_data_engine WHERE id in ('{ids}')";
                int i = await DapperHelper.Execute(sql);
                if (i>0)
                {
                    string[] arr = ids.Split(',');
                    foreach (var item in arr)
                    {
                        Model.etl_data_engine me = lst.FirstOrDefault(x => x.id.Equals(item));
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
        /// <summary>
        /// 获取规则引擎的数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<Model.etl_data_engine>> GetList_etl_data_engine()
        {
            try
            {
                lst = null;
                string sql = "select a.*,b.engine_type FROM etl_data_engine a join etl_data_engine_type b on a.engine_type_id=b.id";
                List<Model.etl_data_engine> list = await DapperHelper.GetList<Model.etl_data_engine>(sql);
                //判断缓存数据是否存在
                if (lst==null||lst.Count==0)
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
        /// <summary>
        /// 添加规则引擎数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert_etl_data_engine(Model.etl_data_engine model)
        {
            try
            {
                Model.sys_user ms = listuser.FirstOrDefault();
                model.create_by = ms.name;
                model.update_by = ms.name;
                string sql = $"insert into etl_data_engine VALUES (UUID(),'{model.engine_name}',{model.engine_type_id},'{model.code_type}','{model.cl_name}',0,'{model.create_by}',NOW(),'{model.update_by}',NOW(),'{model.engine_code}');";
                int i = await DapperHelper.Execute(sql);
                if (i>0)
                {
                    string sql1 = "select * FROM etl_data_engine ORDER BY create_time DESC LIMIT 1";
                    List<Model.etl_data_engine> list = await DapperHelper.GetList<Model.etl_data_engine>(sql1);
                    Model.etl_data_engine me = list.FirstOrDefault();
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
        
        /// <summary>
        /// 编写代码数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Uptdate_code(Model.etl_data_engine model)
        {
            try
            {
                string sql = $"UPDATE etl_data_engine SET engine_code='{model.engine_code}' WHERE id ='{model.id}'";
                int i = await DapperHelper.Execute(sql);
                if (i>0)
                {
                    Model.etl_data_engine me = lst.FirstOrDefault(x => x.id.Equals(model.id));
                    me.engine_code = model.engine_code;
                    rh.SetList(lst, redisKey);
                }
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //修改规则引擎
        public async Task<int> Uptdate_etl_data_engine(Model.etl_data_engine model)
        {
            try
            {
                Model.sys_user ms = listuser.FirstOrDefault();
                model.update_by = ms.name;
                string sql = $"UPDATE etl_data_engine SET engine_name='{model.engine_name}',engine_type_id={model.engine_type_id},code_type='{model.code_type}',cl_name='{model.cl_name}',update_by='{model.update_by}',update_time=NOW() WHERE id ='{model.id}'";
                int i = await DapperHelper.Execute(sql);
                if (i>0)
                {
                    Model.etl_data_engine me = lst.FirstOrDefault(x => x.id.Equals(model.id));
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
