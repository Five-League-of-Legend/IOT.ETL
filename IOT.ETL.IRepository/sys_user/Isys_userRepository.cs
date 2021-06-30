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
        List<Model.sys_user> Query();
        int Insert(Model.sys_user a);
        int UptState(string id);
        int Uptuser(Model.sys_user a);
        int DelUser(string ids);
        List<Model. sys_role> Bang();
    }
}
