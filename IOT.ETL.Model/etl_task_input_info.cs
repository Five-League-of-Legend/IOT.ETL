using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_input_info
    {
        // 任务数据输入表
        public string id { get; set; }
        public string node_id { get; set; }             //节点ID
        public string node_name { get; set; }           //节点名称
        public string task_id { get; set; }             //任务ID
        public string database_id { get; set; }         //数据库ID
        public string database_name { get; set; }       //数据库名称
        public string table_name { get; set; }          //表名称
        public string table_as_name { get; set; }       //表别名
        public string table_field { get; set; }         //表字段
        public string table_as_field { get; set; }      //表字段别名
        public string field_as_egine_id { get; set; }   //字段规则
        public int revision { get; set; }               //乐观锁
        public string create_by { get; set; }           //创建人
        public DateTime create_time { get; set; }       //创建时间
        public string update_by { get; set; }           //更新人
        public DateTime update_time { get; set; }       //更新时间
    }
}
