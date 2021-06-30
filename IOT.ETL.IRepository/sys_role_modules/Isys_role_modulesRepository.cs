using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_role_modules
{
   public interface Isys_role_modulesRepository
    {
        Task<int> Adds(Model.sys_role_modules m);
        Task<int> Uptuser(Model.sys_role_modules a);
        Task<List<Model.sys_role_modules>> Uptft(string id);
        Task<object> cha(string id);
    }
}
