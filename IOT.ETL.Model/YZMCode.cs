using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Model
{
    public class YZMCode
    {
        public DateTime time { get; set; }
        public string times { get { return time.ToString("yyyy-MM-dd HH:mm:ss"); } }
        public string code { get; set; }
        public string email { get; set; }
    }
}
