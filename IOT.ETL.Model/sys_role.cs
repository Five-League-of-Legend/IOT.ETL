using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class sys_role
    {
        public string id { get; set; }
        public string role_name { get; set; }
        public int role_status { get; set; }
        public int revision { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}
