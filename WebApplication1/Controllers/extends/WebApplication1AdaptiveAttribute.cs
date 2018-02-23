using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers.extends
{
    public class WebApplication1AdaptiveAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string rawUrl = System.Web.HttpContext.Current.Request.RawUrl;
                string userAgent = System.Web.HttpContext.Current.Request.UserAgent.ToLower();
                
                if (ifMobile(userAgent) && rawUrl.IndexOf("m")!=1)
                {
                    filterContext.Result = new RedirectResult(urlToMobile(rawUrl));
                }
                else if (!ifMobile(userAgent) && rawUrl.IndexOf("m") == 1)
                {
                    filterContext.Result = new RedirectResult(urlToPC(rawUrl));
                }
                return;
            }
            catch
            {
                //如果报错，返回首页
                filterContext.Result = new RedirectResult("/");
                return;
            }
        }

        private bool ifMobile(string userAgent)
        {
            if (userAgent.Contains("iphone") || userAgent.Contains("android") || userAgent.Contains("ipad") || userAgent.Contains("ucweb") || userAgent.Contains("blackberry") || userAgent.Contains("symbianos") || userAgent.Contains("windows phone") || userAgent.Contains("opera mini"))
            {
                return true;
            }
            return false;
        }

        private string urlToMobile(string rawUrl)
        {
            string url = "/m" + rawUrl;
            
            return url;
        }

        private string urlToPC(string rawUrl)
        {
            string url = rawUrl.Substring(2);
            
            return url;
        }

        
    }
}