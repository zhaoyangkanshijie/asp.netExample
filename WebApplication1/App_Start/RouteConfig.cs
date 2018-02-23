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
            #endregion

            routes.MapRoute(
                "about", // 首页
                "about", // URL
                new { controller = "home", action = "about" },
                new string[] { "WebApplication1.Controllers" }
            );

            routes.MapRoute(
                "contact", // 首页
                "contact", // URL
                new { controller = "home", action = "contact" },
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
