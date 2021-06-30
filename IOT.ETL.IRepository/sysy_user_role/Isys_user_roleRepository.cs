using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sysy_user_role
{
     public interface Isys_user_roleRepository
     {
        Task<int> Add(Model.sys_user_role m);
     }
}
