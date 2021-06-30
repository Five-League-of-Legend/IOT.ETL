using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    public class etl_datasource_info
    {
        public int id { get; set; } // ID
        public int type { get; set; } // 数据源类型
        public int Tid { get; set; } // ID
        public string name { get; set; } // 数据源名称
        public string url { get; set; } // 数据源连接地址
        public string port { get; set; } // 连接端口
        public string database_name { get; set; } // 连接数据库名称
        public string username { get; set; } // 数据源用户名
        public string password { get; set; } // 数据源密码
        public string param { get; set; } // 额外配置
        public int status { get; set; } // 状态 1启用 0停用
        public string remark { get; set; } // 备注 / 说明
        public int revision { get; set; } // 乐观锁
        public string create_by { get; set; } // 创建人
        public DateTime create_time { get; set; } // 创建时间
        public string update_by { get; set; } // 更新人
        public DateTime update_time { get; set; } // 更新时间
        public string describes { get; set; } //数据源描述
        public string Tname { get; set; }
    }
}
