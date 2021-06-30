using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    public  class sys_modules
    {
        public string id { get; set; }
        public string name { get; set; }
        public string p_id { get; set; }
        public int order_num { get; set; }
        public string path { get; set; }
        public string component { get; set; }
        public int is_frame { get; set; }
        public char menu_type { get; set; }
        public char visible { get; set; }
        public char status { get; set; }
        public string perms { get; set; }
        public string icon { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
        public string remark { get; set; }
      
    }
}
