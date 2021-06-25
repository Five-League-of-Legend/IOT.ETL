using IOT.ETL.IRepository.Login;
using IOT.ETL.IRepository.sys_user;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using IOT.ETL.Common;

namespace IOT.ETL.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        RedisHelper<Model.YZMCode> logintime = new RedisHelper<Model.YZMCode>();
        string redistime;
        List<Model.YZMCode> ls = new List<Model.YZMCode>();
        Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ILoginRepository _loginRepository;
        private readonly Isys_userRepository _sys_UserRepository;
        public LoginsController(ILoginRepository loginRepository, Isys_userRepository sys_UserRepository)
        {
            _loginRepository = loginRepository;
            _sys_UserRepository = sys_UserRepository;
            redistime = "login_time";
            ls = logintime.GetList(redistime);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="upwd"></param>
        /// <returns></returns>
        [Route("/api/Login")]
        [HttpPost]
        public int Login(string uname, string upwd)
        {
            object obj = _loginRepository.Login(uname, upwd);
            int i = Convert.ToInt32(obj);
            if (i > 0)
            {
                logger.Debug($"用户名为:{uname}的用户在{DateTime.Now}登录成功");
            }
            return i;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/Register_Add")]
        public int Register_Add([FromForm] Model.sys_user user)
        {
            int i = _loginRepository.Register(user);
            if (i > 0)
            {
                logger.Debug($"用户名为:{user.name}的用户在{DateTime.Now}注册成功");
            }
            return i;

        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/TestEamil")]
        public int TestEamil(string email)
        {
            try
            {
                //获取所有数据
                var list = _sys_UserRepository.Query();
                var ss = list.FirstOrDefault(x => x.email.Equals(email));
                //判断有没有这个邮箱的用户
                if (ss == null)
                {
                    return -1;//说明这个邮箱没有被注册过
                }
                else
                {
                    //调用发送验证码方法,并返回当前验证码
                    string code = SendCodeHelper.SendCode(ss.email);
                    logger.Debug($"邮箱号为:{ss.email},在" + DateTime.Now + "申请验证码");
                    //发送当前时间
                    var time = DateTime.Now;
                    Model.YZMCode cd = new Model.YZMCode();
                    //存储发送验证码的时间
                    cd.time = time;
                    //存储验证码
                    cd.code = code;
                    //存储验证时的邮箱号
                    cd.email = ss.email;
                    List<Model.YZMCode> yzm = new List<Model.YZMCode>();
                    yzm.Add(cd);
                    ls = yzm;
                    logintime.SetList(ls, redistime);
                    return 1;//发送成功
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/Testcode")]
        public int Testcode(string email,string fcode)
        {
            try
            {
                Model.YZMCode ms = ls.FirstOrDefault();
                if (ms.email!=email)
                {
                    //此时邮箱发生了变化
                    return -1;
                }
                if (ms.code!=fcode)
                {
                    //此时验证码输入错误
                    return -2;
                }
                //判断验证码是否过期
                //获取当前时间
                var now = DateTime.Now;
                var mins = (now - Convert.ToDateTime(ms.time)).TotalMinutes;
                if (mins>5)
                {
                    return -3;//时间长超过五分钟 验证码失效
                }
                return 1;//验证码正确
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
                throw;
            }
        }
        
        [HttpPost]
        [Route("/api/UptPwd")]
        public int UptPwd(string email,string pwd)
        {
            try
            {
                Model.YZMCode ms = ls.FirstOrDefault();
                if (ms.email!=email)
                {
                    return -1;//此邮箱不是发送验证码的邮箱
                }
                var i = _loginRepository.UptdatePwd(email, pwd);
                if (i>0)
                {
                    logger.Debug($"邮箱号为:{email}在" + DateTime.Now + "修改密码成功");
                }
                return i;
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
                throw;
            }
        }
    }
}
