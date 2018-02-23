using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}