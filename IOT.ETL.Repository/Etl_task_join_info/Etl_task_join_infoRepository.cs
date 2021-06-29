using IOT.ETL.Common;
using IOT.ETL.IRepository.Etl_task_join_info;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IOT.ETL.Repository.Etl_task_join_info
{
    public class Etl_task_join_infoRepository : Etl_task_join_infoIRepository
    {
        string cun1 = "joinA";
        string cun2 = "joinB";
        //缓存实例化  存放类
        RedisHelper<IOT.ETL.Model.etl_task_join_info> rh = new RedisHelper<etl_task_join_info>();
        List<IOT.ETL.Model.etl_task_join_info> joinlsA = new List<etl_task_join_info>();
        List<IOT.ETL.Model.etl_task_join_info> joinlsB = new List<etl_task_join_info>();


        //缓存实例化  存放SQL语句
        string sqlAll = "mainsql";
        RedisHelper<string> rds = new RedisHelper<string>();
        public Etl_task_join_infoRepository()
        {
            joinlsA = rh.GetList(cun1);
            joinlsB = rh.GetList(cun2);
        }
        //获取到第一个表的数据
        public async Task<int> AddInputA(etl_task_input_info ta)
        {
            int i = 0;
            List<IOT.ETL.Model.etl_task_join_info> a = JsonConvert.DeserializeObject<List<IOT.ETL.Model.etl_task_join_info>>(ta.ToString());
            rh.SetList(a, cun1);
            if (rh.GetList(cun1) != null)
            {
                i += 1;
            }
            return i;
        }
        //获取到第二个表的数据
        public async Task<int> AddInputB(etl_task_input_info ta)
        {
            int i = 0;
            List<IOT.ETL.Model.etl_task_join_info> a = JsonConvert.DeserializeObject<List<IOT.ETL.Model.etl_task_join_info>>(ta.ToString());
            rh.SetList(a, cun2);
            if (rh.GetList(cun2) != null)
            {
                i += 1;
            }
            return i;
        }
        //拼接sql语句
        public async Task<int> AddJoin(etl_task_join_info jo)
        {
            int i = 0;

            //获取到第一个连接对象
            string str1 = JsonConvert.SerializeObject(joinlsA);
            IOT.ETL.Model.etl_task_input_info jla = JsonConvert.DeserializeObject<IOT.ETL.Model.etl_task_input_info>(str1);

            //获取到第二个连接对象
            string str2 = JsonConvert.SerializeObject(joinlsB);
            IOT.ETL.Model.etl_task_input_info jlb = JsonConvert.DeserializeObject<IOT.ETL.Model.etl_task_input_info>(str2);
            string sql = $"insert into etl_task_join_info values(UUID(),'{jo.node_id}','{jo.node_name}','{jo.task_id}','{jo.left_node_id}','{jo.left_id}','{jo.join_type}','{jo.right_node_id}','{jo.right_id}','{jo.left_join_field}','{jo.right_join_field}',{jo.revision},'{jo.create_by}',now(),'{jo.update_by}',now())";
            if (jo.join_type.Equals(1))
            {
                 sql = $"insert into etl_task_join_info values(UUID(),'{jo.node_id}','{jo.node_name}','{jo.task_id}','{jo.left_node_id}','{jo.left_id}',' left join ','{jo.right_node_id}','{jo.right_id}','{jo.left_join_field}','{jo.right_join_field}',{jo.revision},'{jo.create_by}',now(),'{jo.update_by}',now())";
            }
            else if (jo.join_type.Equals(2))
            {
                 sql = $"insert into etl_task_join_info values(UUID(),'{jo.node_id}','{jo.node_name}','{jo.task_id}','{jo.left_node_id}','{jo.left_id}',' right join ','{jo.right_node_id}','{jo.right_id}','{jo.left_join_field}','{jo.right_join_field}',{jo.revision},'{jo.create_by}',now(),'{jo.update_by}',now())";
            }
            else
            {
                 sql = $"insert into etl_task_join_info values(UUID(),'{jo.node_id}','{jo.node_name}','{jo.task_id}','{jo.left_node_id}','{jo.left_id}',' inner join ','{jo.right_node_id}','{jo.right_id}','{jo.left_join_field}','{jo.right_join_field}',{jo.revision},'{jo.create_by}',now(),'{jo.update_by}',now())";
            }


            //将sql语句放入到缓存中
            rds.SetString(sql, sqlAll);

            if (!string.IsNullOrEmpty(rds.GetString(sqlAll)))
            {
                i += 1;
            }

            return i;
        }
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //根据数据库名称查出所有字段
        public async Task<List<IOT.ETL.Model.etl_task_VModel>> ShowField(string database_name)
        {
            string sql = $"select * from etl_task_VModel where 1=1 ";
            if (!string.IsNullOrEmpty(database_name))
            {
                sql += $" and database_name = '{database_name}'";
            }
            List<IOT.ETL.Model.etl_task_VModel> ls = await DapperHelper.GetList<IOT.ETL.Model.etl_task_VModel>(sql);
            return ls;
        }

        //根据任务ID查询出其任务设计的相关信息   显示出对应字段 
        public async Task<List<IOT.ETL.Model.etl_task_input_info>> FanFieldID(string id)
        {
            string sql = $"select table_name,table_as_name from etl_task_input_info where 1=1";
            if (!string.IsNullOrEmpty(id))
            {
                sql += $" and task_id = '{id}'";
            }
            return await DapperHelper.GetList<IOT.ETL.Model.etl_task_input_info>(sql);
        }
        //根据ID和所属的数据库进行筛选   显示其中选中字段
        public async Task<List<IOT.ETL.Model.etl_task_VModel>> SelectBind(string database_name, string id)
        {
            string sql = $"select * from etl_task_VModel where 1=1";
            if (!string.IsNullOrEmpty(database_name))
            {
                sql += $" and database_name = '{database_name}'";
            }
           
            if (!string.IsNullOrEmpty(id))
            {
                string[] arr = id.Split(',');
                int[] err = Array.ConvertAll<string, int>(arr, delegate (string s) { return int.Parse(s); });
                for (int i = 0; i < err.Length; i++)
                {
                    if (i==0)
                    {
                        sql += $" and id  ={err[i]}";
                    }
                    else
                    {
                        sql += $" or id  ={err[i]}";
                    }
                }
            }
            return await DapperHelper.GetList<IOT.ETL.Model.etl_task_VModel>(sql);
        }

        //所有选中的字段输出
        public async Task<List<IOT.ETL.Model.etl_task_VModel>> OutputAllField(string id)
        {
            string sql = $"select * from etl_task_VModel where 1=1";
            if (!string.IsNullOrEmpty(id))
            {
                sql += $" and id in ('{id}')";
            }
            return await DapperHelper.GetList<IOT.ETL.Model.etl_task_VModel>(sql);
        }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //点击执行
        public async Task<int> ExecuteAllSql(string id,string name)
        {
            int j = 0;
            //根据ID查询出指定任务   在找到指定任务下的执行次数
            string sqlTask = $"select * from etl_task_info where Id = '{id}'";
            List<Model.etl_task_info> tals = await DapperHelper.GetList<Model.etl_task_info>(sqlTask);
            Model.etl_task_info tata = tals.FirstOrDefault(m=>m.Id==id);


                //取出缓存   并拼接   根据条件插入
                string sql =$"{rds.GetString(sqlAll)}" ;
                for (int i = 0; i < tata.Execute_total; i++)
                {
                    j += await DapperHelper.Execute_plan(sql, name);
                }
                return j;
            
        }



    }
}
