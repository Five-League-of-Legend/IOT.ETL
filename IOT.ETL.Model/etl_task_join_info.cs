using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    class etl_task_join_info
    {
        //数据关联表
        public string id { get; set; }
        public string node_id { get; set; }          //节点ID
        public string node_name { get; set; }        //节点名称
        public string task_id { get; set; }          //任务ID
        public string left_node_id { get; set; }     //左节点ID
        public string left_id { get; set; }          //左表ID
        public string join_type { get; set; }        //关联方式
        public string right_node_id { get; set; }    //右表节点ID
        public string right_id { get; set; }         //右表ID
        public string left_join_field { get; set; }  //左表字段
        public string right_join_field { get; set; } //右表字段
        public int revision { get; set; }            //乐观锁
        public string create_by { get; set; }        //创建人
        public DateTime create_time { get; set; }    //创建时间
        public string update_by { get; set; }        //更新人
        public DateTime update_time { get; set; }    //更新时间
    }
}
