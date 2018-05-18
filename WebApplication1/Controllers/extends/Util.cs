using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Quartz;
using Quartz.Impl;
using Quartz.net;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Net;
using Microsoft.Exchange.WebServices.Data;

namespace WebApplication1.Controllers.extends
{
    public class Util
    {
        /**
         * 计算日期差距
         * 参数：起始日期，结束日期，计算方式（1：年，2：月，3：日，4：时，5：分，6：秒）
         */
        public double DateDiff(DateTime startDate, DateTime endDate, int howtocompare = 3)
        {
            //调用
            //Util util = new Util();
            //DateTime end = DateTime.Now;
            //DateTime start = Convert.ToDateTime("2018-01-01 00:00");
            //double diff = util.DateDiff(start, end);
            //Response.Write(d.ToString("f0"));//四舍五入

            double diff = 0;
            TimeSpan TS = new TimeSpan(endDate.Ticks - startDate.Ticks);

            switch (howtocompare)
            {
                case 1:
                    diff = Convert.ToDouble(TS.TotalDays / 365);
                    break;
                case 2:
                    diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                    break;
                case 3:
                    diff = Convert.ToDouble(TS.TotalDays);
                    break;
                case 4:
                    diff = Convert.ToDouble(TS.TotalHours);
                    break;
                case 5:
                    diff = Convert.ToDouble(TS.TotalMinutes);
                    break;
                case 6:
                    diff = Convert.ToDouble(TS.TotalSeconds);
                    break;
                default:
                    diff = Convert.ToDouble(TS.TotalDays);
                    break;
            }

            return diff;
        }

        /**
         * 执行定时任务
         */
        public void ScheduleStart()
        {
            // 如果配置了Log4Net 可取消注释
            //Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

            // 从工厂里获取调度实例
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // 创建一个任务
            IJobDetail job = JobBuilder.Create<Job>()
                .WithIdentity("MyJob", "group1")
                .Build();

            // 创建一个触发器
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("MyJob", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(10) // 每10秒执行一次
            //        .RepeatForever()) // 无限次执行
            //    .Build();

            // 每天执行的触发器
            ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("MyJob", "group1")
               .ForJob(job)
               .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 0)) // 每天00:00执行一次
                                                                             //.ModifiedByCalendar("myHolidays") // but not on holidays 设置那一天不知道
               .Build();

            // 每天执行的将触发器换成天的就可以了
            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();
        }

        #region exchange发送邮件
        /**
         * exchange发送邮件
         * 参数：（username用户名，password密码，domain域名，url服务地址，subject标题，body内容，recipient收件人）
         */
        public void exchangeSendEmail(string username, string password, string domain, string url, string subject, string body, string recipient)
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);//实例化ExchangeService时一般要指定Exchange的版本，如不指定将会有异常抛出
            //service.Credentials = new WebCredentials(username, password, domain);// 指定用户名，密码，和域名
            service.Credentials = new WebCredentials(username, password);
            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;
            //如果我们知道Exchange Service的地址直接给实例指定URL，如果不知道可以用EWS Managed API的Autodiscover服务
            //service.Url = new Uri(url);// 指定Exchage服务的url地址
            service.AutodiscoverUrl(url, RedirectionUrlValidationCallback);
            //service.AutodiscoverUrl(url);// 指定邮箱账号
            EmailMessage message = new EmailMessage(service);
            message.Subject = subject;
            message.Body = body;
            message.ToRecipients.Add(recipient);//收件人
            message.SendAndSaveCopy();  // 发送并保存已发送邮件
        }

        private static bool CertificateValidationCallBack(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors
        )
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        #endregion

        #region smtp发送邮件
        /**
         * smtp发送邮件
         * 参数：（username用户名，password密码，domain域名，url服务地址，subject标题，body内容，recipient收件人）
         */
        public void smtpSendEmail(string username, string password, string url, string fromAddress, string fromName, string subject, string body, string recipient)
        {
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(username, password);
            client.Host = url;

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            /* 3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
            msg.From = new MailAddress(fromAddress, fromName, System.Text.Encoding.UTF8);
            msg.Subject = subject;//邮件标题
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码
            msg.Body = body;//邮件内容
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
            msg.IsBodyHtml = false;//是否是HTML邮件
            msg.Priority = MailPriority.High;//邮件优先级
            msg.To.Add(recipient);//添加收件人

            client.Send(msg);
        }
        #endregion

        #region aes
        /**
         * AES对称加密解密
         */
        private static byte[] initIv(int blockSize)
        {
            byte[] iv = new byte[blockSize];
            for (int i = 0; i < blockSize; i++)
            {
                iv[i] = (byte)0x0;
            }
            return iv;

        }
        static byte[] AES_IV = initIv(16);
        public static string AesEncrypt(string encryptKey, string content, string charset)
        {
            Byte[] keyArray = Convert.FromBase64String(encryptKey);
            Byte[] toEncryptArray = null;

            if (string.IsNullOrEmpty(charset))
            {
                toEncryptArray = Encoding.UTF8.GetBytes(content);
            }
            else
            {
                toEncryptArray = Encoding.GetEncoding(charset).GetBytes(content);
            }

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.IV = AES_IV;

            ICryptoTransform cTransform = rDel.CreateEncryptor(rDel.Key, rDel.IV);
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray);
        }
        public static string AesDencrypt(string encryptKey, string content, string charset)
        {
            Byte[] keyArray = Convert.FromBase64String(encryptKey);
            Byte[] toEncryptArray = Convert.FromBase64String(content);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.IV = AES_IV;

            ICryptoTransform cTransform = rDel.CreateDecryptor(rDel.Key, rDel.IV);
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);


            if (string.IsNullOrEmpty(charset))
            {
                return Encoding.UTF8.GetString(resultArray);
            }
            else
            {
                return Encoding.GetEncoding(charset).GetString(resultArray);
            }
        }

        public static Dictionary<string, string> sessionGet(string encrypt)
        {
            //string session = "{\"userName\":\"wxp\",\"email\":\"a@qq.com\",\"timespan\":\"2018/6/17 16:05:22\"}";
            //string str="0ISQ3GrRTN7/EfwdM/3iz36eXVgN89JyBVWRZvwtRc05VAnInRJwftLSgcpzfq+StmDlE/a7XtTIFE7vJxLD4qbS2UpteGGD8aSFeAqxHDg="
            string key = "1rec493fda794aef4f1556321fbb2257";
            //string encrypt = AesEncrypt(key, session, null);
            string dencrypt = AesDencrypt(key, encrypt, null);
            Dictionary<string, string> dic = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(dencrypt);
            return dic;
        }
        #endregion
    }
}