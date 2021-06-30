using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_modules
{
   public interface Isys_modulesRepository
    {
        Task<List<Model.sys_modules>> GetSys_Modules();
        Task<List<Dictionary<string, object>>> BindTree();
    }
}
