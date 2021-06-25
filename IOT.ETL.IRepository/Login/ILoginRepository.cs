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
        Task<object> Login(string loginName, string pwd);
        //注册
        Task<int> Register(Model.sys_user model);
        //修改密码
        Task<int> UptdatePwd(string email,string pwd);
    }
}
