using IOT.ETL.IRepository.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        public LoginsController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="upwd"></param>
        /// <returns></returns>
        [Route("/api/Login")]
        [HttpPost]
        public int Login(string uname,string upwd)
        {
            object obj = _loginRepository.Login(uname,upwd);
            int i = Convert.ToInt32(obj);
            return i;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/Register_Add")]
        public int Register_Add([FromForm]Model.sys_user user)
        {
            int i = _loginRepository.Register(user);
            return i;
        }
        
    }
}
