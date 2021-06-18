using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_logic_info
    {
        /// <summary>
        /// 任务处理过程表
        /// </summary>
        public string id { get; set; }
        public string node_id { get; set; }            //节点ID
        public string node_name { get; set; }          //节点名称
        public string task_id { get; set; }            //任务ID
        public string select_node_id { get; set; }     //选择节点Id
        public string select_node_table { get; set; }  //选择表名称
        public string select_node_field { get; set; }  //选择字段名称
        public string annal_engine { get; set; }       //记录级别处理方案
        public string field_engine { get; set; }       //字段级别处理方案
        public string create_by { get; set; }          //创建人
        public DateTime create_time { get; set; }      //创建时间
        public string update_by { get; set; }          //更新人
        public DateTime update_time { get; set; }      //更新时间
    }
}
