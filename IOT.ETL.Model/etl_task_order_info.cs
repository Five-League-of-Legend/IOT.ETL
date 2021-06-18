using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_order_info
    {
        /// <summary>
        /// 任务处理排序表
        /// </summary>
        public string id { get; set; }
        public string node_id { get; set; }             //节点ID
        public string node_name { get; set; }           //节点名称
        public string task_id { get; set; }             //任务ID
        public string select_node_id { get; set; }      //选择节点ID
        public string select_table_name { get; set; }   //选择表名称
        public string select_table_field { get; set; }  //选择表字段
        public string order_sort { get; set; }          //排序类型
        public string create_by { get; set; }           //创建人
        public DateTime create_time { get; set; }       //创建时间
        public string update_by { get; set; }           //更新人
        public DateTime update_time { get; set; }      //更新时间
      
    }
}
