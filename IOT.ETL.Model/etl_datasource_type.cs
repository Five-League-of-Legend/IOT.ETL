using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    public class etl_datasource_type
    {
        public int Tid { get; set; } // ID
        public string Tname { get; set; } // 名称
        public string Tnote { get; set; } // 备注
        public string Tp_id { get; set; } // 父级ID
        public string Tcreate_by { get; set; } // 创建人
        public DateTime Tcreate_time { get; set; } // 创建时间
        public string Tupdate_by { get; set; } // 更新人
        public DateTime Tupdate_time { get; set; } // 更新时间
    }
}
