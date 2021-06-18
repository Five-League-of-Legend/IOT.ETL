using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_role
{
    public interface Isys_roleRepository
    {
        List<Model.sys_role> ShowRoles();
        int DelRoles(string id);
        int insertRoles(Model.sys_role a);
        int UpdateRoles(Model.sys_role a);
    }
}
