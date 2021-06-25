using IOT.ETL.Common;
using IOT.ETL.IRepository.Login;
using IOT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Session;
using NLog;

namespace IOT.ETL.Repository.Login
{
    public class LoginRepository : ILoginRepository
    {
        Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //实例化缓存帮助类
        RedisHelper<Model.sys_user> rh = new RedisHelper<Model.sys_user>();
        //创建一个缓存关键字
        string redisLogin;
        List<Model.sys_user> lst = new List<Model.sys_user>();
        public LoginRepository()
        {
            redisLogin = "loginlist";
            lst = rh.GetList(redisLogin);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public async Task<object> Login(string loginName, string pwd)
        {
            try
            {
                object result = DapperHelper.Exescalar($"select count(1) from sys_user where username='{loginName}' and password='{DESEncrypt.GetMd5Str(pwd)}'");
                if (Convert.ToInt32(result) > 0)
                {
                    string sql = $"select * from sys_user where username='{loginName}' AND password='{DESEncrypt.GetMd5Str(pwd)}'";
                    lst = await DapperHelper.GetList<Model.sys_user>(sql);
                    rh.SetList(lst, redisLogin);
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
                throw;
            }

        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns></returns>
        public async Task<int> Register(Model.sys_user model)
        {
            try
            {
                string sql1 = "select * from sys_user";
                List<Model.sys_user> ls = await DapperHelper.GetList<Model.sys_user>(sql1);
                ls = ls.Where(x => x.name.Equals(model.name) || x.username.Equals(model.username)||x.email.Equals(model.email)).ToList();
                //判断用户名是否已经存在
                if (ls == null || ls.Count == 0)
                {
                    string sql = $"insert into sys_user VALUES (UUID(),'{model.name}','{model.email}','{model.phone}','{model.img_url}','{model.username}','{DESEncrypt.GetMd5Str(model.password)}',{model.is_admin},{model.status},0,'{model.name}',NOW(),'{model.name}',NOW())";
                    int i = await DapperHelper.Execute(sql);
                    return i;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<int> UptdatePwd(string email, string pwd)
        {
            try
            {
                string sql = $"UPDATE sys_user SET password='{DESEncrypt.GetMd5Str(pwd)}' WHERE email='{email}'";
                int i = await DapperHelper.Execute(sql);
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
