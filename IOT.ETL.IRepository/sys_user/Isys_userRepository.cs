using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.sys_user
{
    public interface Isys_userRepository
    {
        Task<List<Model.sys_user>> Query();
        Task<int> Insert(Model.sys_user a);
        Task<int> UptState(string id);
        Task<int> Uptuser(Model.sys_user a);
        Task<int> DelUser(string id);
        Task<List<Model.sys_role>> Bang();
    }
}
