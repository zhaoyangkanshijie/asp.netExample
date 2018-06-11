﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication1.Controllers.extends;
using WebApplication1.Dal;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        static HomeController()
        {
            //Util util = new Util();
            //util.ScheduleStart();
        }

        [ActionName("index")]
        [WebApplication1Adaptive]
        public ActionResult Index()
        {
            UnitOfWork WebUnity = new UnitOfWork();
            List<r_user> r_userlist = WebUnity.UserRepository.Get().ToList<r_user>();

            ViewData["list"] = r_userlist;
            return View();
        }

        [ActionName("about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            ViewData["Message"] = "message";

            return View();
        }

        [ActionName("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ActionName("scheduleAndFileSystem")]
        public ActionResult ScheduleAndFileSystem()
        {
            Util util = new Util();
            util.ScheduleStart();

            return View();
        }

        [ActionName("uploadPPT")]
        public ActionResult UploadPPT()
        {
            return View();
        }

        [ActionName("aes")]
        public ActionResult AES()
        {
            string key = "1bec493fda794aef4f1556321fbb2257";
            string content = "123456";
            Util util = new Util();
            string encrypt = Util.AesEncrypt(key, content, null);
            string dencrypt = Util.AesDencrypt(key, encrypt, null);
            ViewData["key"] = key;
            ViewData["content"] = content;
            ViewData["encrypt"] = encrypt;
            ViewData["dencrypt"] = dencrypt;
            return View();
        }

        [ActionName("ip")]
        public ActionResult IP()
        {
            string ip = staticUtil.GetRealIp();
            ViewData["ip"] = ip;
            return View();
        }

        [ActionName("deepclone")]
        public ActionResult DeepClone()
        {
            IList<string> list = new List<string>();
            list.Add("a");
            list.Add("b");
            list.Add("c");
            IList<string> list2 = staticUtil.DeepClone<string>(list);
            ViewData["list"] = list;
            ViewData["list2"] = list2;
            return View();
        }

        [ActionName("openerAndFileReader")]
        public ActionResult OpenerAndFileReader()
        {
            return View();
        }

        [ActionName("smtp")]
        public ActionResult SMTP()
        {
            string username = "用户名";
            string password = "密码";
            string domain = "服务器域名或ip";
            string url = "服务器域名或ip";
            string fromAddress = "发送地址";
            string fromName = "发件人";
            string subject = "标题";
            string body = "内容";
            string recipient = "收件人";
            Util util = new Util();
            //util.exchangeSendEmail(username, password, domain, url, subject, body, recipient);
            util.smtpSendEmail(username, password, url, fromAddress, fromName, subject, body, recipient);

            return Content("ok");
        }

        [ActionName("exchange")]
        public ActionResult Exchange()
        {
            string username = "用户名";
            string password = "密码";
            string domain = "服务器域名或ip";
            string url = "服务器域名或ip";
            string fromAddress = "发送地址";
            string fromName = "发件人";
            string subject = "标题";
            string body = "内容";
            string recipient = "收件人";
            Util util = new Util();
            util.exchangeSendEmail(username, password, domain, url, subject, body, recipient);
            //util.smtpSendEmail(username, password, url, fromAddress, fromName, subject, body, recipient);

            return Content("ok");
        }

        [ActionName("deserialize")]
        public ActionResult Deserialize()
        {
            string str = "{\"userName\":\"sysadmin\",\"email\":\"xxx@xxx.com\",\"other\":\"remenber to use EncodeURIComponent()\",\"timespan\":\"" + DateTime.Now.ToString() + "\"}";
            Dictionary<string, string> dic = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(str);
            ViewData["dic"] = dic;
            return View();
        }

        [ActionName("formExample")]
        public ActionResult FormExample()
        {
            return View();
        }

        [ActionName("formSubmit")]
        public ActionResult FormSubmit()
        {
            string formMsg = string.Empty;
            using (Stream s = Request.InputStream)
            {
                StreamReader reader = new StreamReader(s);
                formMsg = reader.ReadToEnd();
            }
            string actionMsg = formMsg.Substring(formMsg.IndexOf('{'), formMsg.LastIndexOf('}') - formMsg.IndexOf('{') + 1);

            //获取配置文件的办法
            //webConfig:configuration标签下
            //<appSettings>
            //    <add key="SolutionUrl" value="http://..." />
            //</appSettings>
            //controller:
            //string solutionUrl = WebConfigurationManager.AppSettings["SolutionUrl"];
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(solutionUrl);
            string url = "https://www.baidu.com/";//填入请求的url,url那边需要写response，此处为了方便，手动设置response的内容为ok
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = "application/json";
            request.Method = "post";
            request.Timeout = 10000;
            Stream responseStream = null;
            StreamReader sReader = null;
            String value = null;
            request.UserAgent = Request.UserAgent;
            //如果需要POST数据

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(actionMsg);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);


            WebResponse res = request.GetResponse();//由于url没response，此处会报错

            try
            {
                // 获取响应流
                responseStream = res.GetResponseStream();
                // 对接响应流(以"utf-8"字符集)
                sReader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                // 开始读取数据
                value = sReader.ReadToEnd();
            }
            catch (Exception)
            {
                //日志异常
            }
            finally
            {
                //强制关闭
                writer.Close();//关闭请求流
                if (sReader != null)
                {
                    sReader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (res != null)
                {
                    res.Close();
                }
            }

            return Content("ok");
        }

        #region 图形验证码
        [ActionName("code")]
        public ActionResult Code()
        {
            ValidateCode vCode = new ValidateCode();
            byte[] bytes = vCode.CreateValidateGraphic();
            return File(bytes, @"image/jpeg");
        }

        [ActionName("imgCode")]
        public ActionResult ImgCode()
        {
            return View();
        }
        #endregion

        #region 上传附件ppt
        [ActionName("uploadPPT2")]
        [HttpPost]
        public ActionResult UploadPPT2()
        {
            //错误列表
            List<string> uploadErrors = new List<string>();

            // 1、文件名（包含除去ppt的路径）；2、文件大小；3、文件显示名
            List<string> fileInfo = new List<string>();

            string result = "success";
            try
            {
                // 上传到根目录下的~/content/ppt     * 按月份新建文件夹       * 命名按照原来的文件名  
                HttpPostedFileBase file = Request.Files["file1"];

                //判断文件是否合法
                if (isAllowPPT(uploadErrors, file))
                {
                    //文件合法，开始上传
                    string month = DateTime.Now.Month.ToString();
                    month = Convert.ToInt32(month) < 10 ? "0" + month : month;
                    string path = Server.MapPath("/Content/ppt/" + DateTime.Now.Year.ToString() + month);
                    string fName;
                    if (file.FileName.IndexOf('\\') > -1)
                    {
                        fName = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    }
                    else
                    {
                        fName = file.FileName;
                    }
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(path + "/" + fName);
                    fileInfo.Add(("/content/ppt/" + DateTime.Now.Year.ToString() + month + "/" + fName));
                    fileInfo.Add((((float)(file.ContentLength * 1.00 / (1024 * 1024))).ToString("f2") + "MB"));
                    fileInfo.Add(fName);
                }
                else
                {
                    result = "fail";
                }
            }
            catch
            {
                uploadErrors.Add("上传失败");
                result = "fail";
            }
            ViewData["uploadErrors"] = uploadErrors;
            ViewData["fileInfo"] = fileInfo;
            ViewData["state"] = result;
            return View("uploadPPT2");
        }

        // 判断文件大小，不超过30MB  判断文件后缀，只允许ppt,pptx
        private bool isAllowPPT(List<string> uploadErrors, HttpPostedFileBase file)
        {
            if (file.ContentLength == 0)
            {
                uploadErrors.Add("未找到文件");
                return false;
            }
            if ((file.ContentLength / (1024 * 1024)) > 30)
            {
                uploadErrors.Add("文件大小超过30MB");
                return false;
            }
            if (getFilePostfix(file.FileName).ToLower() != "ppt" &&
                getFilePostfix(file.FileName).ToLower() != "pptx")
            {
                uploadErrors.Add("文件类型只能为“ppt或pptx”");
                return false;
            }
            return true;
        }

        //获取文件后缀名
        private string getFilePostfix(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf('.') + 1);
        }
        #endregion
    }
}