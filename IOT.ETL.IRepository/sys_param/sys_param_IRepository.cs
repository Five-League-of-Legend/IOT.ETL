using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_param
{
    public interface sys_param_IRepository
    {
        Task<List<Model.sys_param>> GetList_sys_param(string pid);
        Task<int> Insert_sys_param(Model.sys_param model);
        Task<int> Delete_sys_param(string ids);
        Task<int> Uptdate_sys_param(Model.sys_param model);
        Task<List<Dictionary<string, object>>> BindParent();
    }
}
