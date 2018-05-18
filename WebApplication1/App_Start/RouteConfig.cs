using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            #region 首页
            routes.MapRoute(
                "Home1", // 首页
                "", // URL
                new { controller = "home", action = "index" },
                new string[] { "WebApplication1.Controllers" }
            );
            routes.MapRoute(
                "Home2", // 首页2
                "index.html", // index.html
                new { controller = "home", action = "index" },
                new string[] { "WebApplication1.Controllers" }
            );
            routes.MapRoute(
                "Home3", // 首页3
                "home/index", // index.html
                new { controller = "home", action = "index" },
                new string[] { "WebApplication1.Controllers" }
            );
            #endregion

            routes.MapRoute(
                "about", 
                "home/about", 
                new { controller = "home", action = "about" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "contact", 
                "home/contact", 
                new { controller = "home", action = "contact" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "ScheduleAndFileSystem",
                "home/ScheduleAndFileSystem",
                new { controller = "home", action = "ScheduleAndFileSystem" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "UploadPPT",
                "home/UploadPPT",
                new { controller = "home", action = "UploadPPT" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "UploadPPT2",
                "home/UploadPPT2",
                new { controller = "home", action = "UploadPPT2" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "aes",
                "home/aes",
                new { controller = "home", action = "aes" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "code",
                "home/code",
                new { controller = "home", action = "code" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "imgcode",
                "home/imgcode",
                new { controller = "home", action = "imgcode" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "ip",
                "home/ip",
                new { controller = "home", action = "ip" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "DeepClone",
                "home/DeepClone",
                new { controller = "home", action = "DeepClone" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "OpenerAndFileReader",
                "home/OpenerAndFileReader",
                new { controller = "home", action = "OpenerAndFileReader" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "smtp",
                "home/smtp",
                new { controller = "home", action = "smtp" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "exchange",
                "home/exchange",
                new { controller = "home", action = "exchange" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
