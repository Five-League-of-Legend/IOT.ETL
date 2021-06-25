using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    public class etl_task_VModel
    {
        //查询表里面的字段信息
        public int Id { get; set; }
        public string column_name { get; set; }//字段
        public string data_type { get; set; }//类型长度
        public string Fkong { get; set; }//非空
        public string Zshi { get; set; }//注释
        public string Rule { get; set; }//处理规则
        public string AsName { get; set; }//表别名
        public string database_name { get; set; }  //属于哪个数据库
    }
}
