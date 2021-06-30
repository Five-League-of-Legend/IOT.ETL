using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_role
{
    public interface Isys_roleRepository
    {
        Task<List<Model.sys_role>> ShowRoles();
        Task<int> DelRoles(string id);
        Task<int> insertRoles(Model.sys_role a);
        Task<int> UpdateRoles(Model.sys_role a);
    }
}
