using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_output_info
    {
        public string id { get; set; }
        public string node_id { get; set; }             //节点ID
        public string node_name { get; set; }           //节点名称
        public string task_id { get; set; }             //任务ID
        public string database_id { get; set; }
        public string input_field { get; set; }         //输入字段
        public string output_table { get; set; }        //输出表
        public string output_table_field { get; set; }  //输入表字段
        public string create_by { get; set; }           //创建人
        public DateTime create_time { get; set; }       //创建时间
        public string update_by { get; set; }           //更新人
        public DateTime update_time { get; set; }       //更新时间
    }
}
