using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers.Areas.m.Controllers
{
    public class HomeController : Controller
    {
        [ActionName("index")]
        public ActionResult Index()
        {
            String ua = Request.UserAgent;
            ua = ua.ToLower();
            var isAndroid = ua.IndexOf("android") > -1 || ua.IndexOf("linux") > -1;
            var isIOS = Regex.IsMatch(ua, @"\(i[^;]+;( u;)? cpu.+mac os x");
            String result = "";
            if (isAndroid)
            {
                Regex reg = new Regex(@"android (.*?);");
                var mat = reg.Match(ua);
                String deviceVersion = mat.Groups[1].ToString();
                String[] versionNum = deviceVersion.Split('.');
                result = "android " + versionNum[0];
                if (Convert.ToInt32(versionNum[0]) >= 7)
                {
                    ViewData["DeviceType"] = "android7";
                }
                else
                {
                    ViewData["DeviceType"] = "else";
                }
            }
            else if (isIOS)
            {
                Regex reg = new Regex(@"os (.*?)like");
                var mat = reg.Match(ua);
                String deviceVersion = mat.Groups[1].ToString();
                String[] versionNum = deviceVersion.Split('_');
                result = "IOS " + versionNum[0];
                if (Convert.ToInt32(versionNum[0]) >= 11)
                {
                    ViewData["DeviceType"] = "ios11";
                }
                else
                {
                    ViewData["DeviceType"] = "else";
                }
            }
            else
            {
                ViewData["DeviceType"] = "else";
            }
            return View();
        }

        [ActionName("jsonp")]
        public ActionResult jsonp()
        {
            return View();
        }

        public string ProcessCallback(string name, string email)
        {
            if (Request.QueryString != null)
            {
                string jsonpCallback = Request.QueryString["jsonpcallback"];
                var user = new User
                {
                    Name = name,
                    Email = email
                };

                return jsonpCallback + "(" + new JavaScriptSerializer().Serialize(user) + ")";
            }

            return "error";
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

    }
}