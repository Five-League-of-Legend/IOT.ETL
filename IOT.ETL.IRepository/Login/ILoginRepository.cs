using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.IRepository.Login
{
    public interface ILoginRepository
    {
        //登录
        object Login(string loginName, string pwd);
        //注册
        int Register(Model.sys_user model);
    }
}
