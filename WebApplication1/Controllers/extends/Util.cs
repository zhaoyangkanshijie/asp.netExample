﻿using System;
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

namespace WebApplication1.Controllers.extends
{
    public class Util
    {
        /**
         * 计算日期差距
         * 参数：起始日期，结束日期，计算方式（1：年，2：月，3：日，4：时，5：分，6：秒）
         */
        public double DateDiff(DateTime startDate, DateTime endDate,int howtocompare = 3)
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
                .WithIdentity("MyJob", "group2")
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
               .WithIdentity("MyJob", "group2")
               .ForJob(job)
               .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(0, 0)) // 每天00:00执行一次
                //.ModifiedByCalendar("myHolidays") // but not on holidays 设置那一天不知道
               .Build();

            // 每天执行的将触发器换成天的就可以了
            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();

        }

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
            //string session = "{\"userName\":\"wxp\",\"email\":\"a@qq.com\",\"timespan\":\"123456548979789\"}";
            string key = "1bec493fda794aef4f1556321fbb2257";
            //string encrypt = AesEncrypt(key, session, null);
            string dencrypt = AesDencrypt(key, encrypt, null);
            Dictionary<string, string> dic = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(dencrypt);
            return dic;
        }

    }
}