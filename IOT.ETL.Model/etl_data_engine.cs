using System;

namespace IOT.ETL.Model
{
    //数据规则引擎表
    public class etl_data_engine
    {
        public string id { get; set; }//id
        public string engine_name { get; set; }//规则名称
        public string engine_type_id { get; set; }//规则类型id
        public string code_type { get; set; }//代码类型
        public string cl_name { get; set; }//类/函数名称
        public int revision { get; set; }//乐观锁
        public string create_by { get; set; }//创建人
        public DateTime create_time { get; set; }//创建时间
        public string update_by { get; set; }//更新人
        public DateTime update_time { get; set; }//更新时间
        public string engine_code { get; set; }//规则引擎代码
        public string engine_type { get; set; }//规则引擎类型
    }
}
