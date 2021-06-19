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

namespace IOT.ETL.Repository.Login
{
    public class LoginRepository : ILoginRepository
    {
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
        public object Login(string loginName, string pwd)
        {
            try
            {
                object result = DapperHelper.Exescalar($"select count(1) from sys_user where username='{loginName}' and password='{pwd}'");
                if (Convert.ToInt32(result) > 0)
                {
                    string sql = $"select * from sys_user where username='{loginName}' AND password='{pwd}'";
                    lst = DapperHelper.GetList<Model.sys_user>(sql);
                    rh.SetList(lst, redisLogin);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns></returns>
        public int Register(Model.sys_user model)
        {
            try
            {
                string sql1 = $"select * from sys_user where username='{model.username}'";
                List<Model.sys_user> ls = DapperHelper.GetList<Model.sys_user>(sql1);
                //判断用户名是否已经存在
                if (ls == null || ls.Count == 0)
                {
                    string sql = $"insert into sys_user VALUES (UUID(),'{model.name}','{model.email}','{model.phone}','{model.img_url}','{model.username}','{model.password}',{model.is_admin},{model.status},0,'{model.name}',NOW(),'{model.name}',NOW())";
                    int i = DapperHelper.Execute(sql);
                    return i;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 生成验证码发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="email">邮箱账号</param>
        public void SendVerificationCode(string email)
        {
            //随机生成验证码
            Random rm = new Random();
            //随机生成的一个数字存放到i里面
            int i;
            //定义一个字符串为了接受生成的随机数  每次都清空
            string str = string.Empty;
            //循环 随机生成一个六位的数字
            for (int p = 0; p < 6; p++)
            {
                //NextDouble生成一个0.0到1.0之间的随机数
                i = Convert.ToInt32(rm.NextDouble() * 10);
                //然后拿到这个数之后拼接到str字符串里面
                str += i;
            }
            //拼接发送的语句
            string content = "腾讯科技提醒您:您正在使用QQ邮箱安全验证服务,您本次操作的验证码是:" + str;
            SendEmail1(email, "【腾讯科技】后台登录修改用户信息提示", content);
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="email">收件人的邮箱</param>
        /// <param name="mailSubject">主题</param>
        /// <param name="mailContent">内容</param>
        public static void SendEmail1(string email, string mailSubject, string mailContent)
        {
            SmtpClient mailClient = new SmtpClient("smtp.qq.com");
            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;
            //Credentials登陆SMTP服务器的身份验证.
            mailClient.Credentials = new NetworkCredential("2200176291@qq.com", "QWC12345..");//邮箱
            MailMessage message = new MailMessage(new MailAddress("2200176291@qq.com"), new MailAddress(email));//发件人,收件人
            message.IsBodyHtml = true;
            // message.Bcc.Add(new MailAddress("tst@qq.com")); //可以添加多个收件人
            message.Body = mailContent;//邮件内容
            message.Subject = mailSubject;//邮件主题
                                          //Attachment 附件
                                          //Attachment att = new Attachment(@"C:/hello.txt");
                                          //message.Attachments.Add(att);//添加附件
                                          //Console.WriteLine("Start Send Mail....");
                                          //发送....
            mailClient.Send(message); // 发送邮件
        }
    }
}
