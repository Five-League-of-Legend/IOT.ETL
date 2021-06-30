using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_role_modules
{
   public interface Isys_role_modulesRepository
    {
        int Adds(Model.sys_role_modules m);
        int Uptuser(Model.sys_role_modules a);
        List<Model.sys_role_modules> Uptft(string id);
        object cha(string id);
    }
}
