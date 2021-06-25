using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IOT.ETL.Common
{
    public class SendCodeHelper
    {
        /// <summary>
        /// 生成验证码发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="email">邮箱账号</param>
        public static string SendCode(string email)
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
                if (i==10)
                {
                    i = 1;
                }
                //然后拿到这个数之后拼接到str字符串里面
                str += i;
            }
            //拼接发送的语句
            string content = "腾讯科技提醒您:您正在使用QQ邮箱安全验证服务,您本次操作的验证码是:" + str+"此验证码有效时期为5分钟,请注意时间";
            SendEmail1(email, "【腾讯科技】后台登录修改用户信息提示", content);

            return str;
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
            mailClient.Credentials = new NetworkCredential("2987644760@qq.com", "fqucwkhmsmpzddda");//邮箱
            MailMessage message = new MailMessage(new MailAddress("2987644760@qq.com"), new MailAddress(email));//发件人,收件人
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
