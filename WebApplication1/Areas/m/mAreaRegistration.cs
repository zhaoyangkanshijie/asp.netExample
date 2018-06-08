using System.Web.Mvc;

namespace WebApplication1.Areas.m
{
    public class mAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "m";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "mhome", 
                "m",
                new { controller = "home", action = "index" },
                new string[] { "WebApplication1.Controllers.Areas.m.Controllers" }
            );

            context.MapRoute(
                "mhome2",
                "m/home/index",
                new { controller = "home", action = "index" },
                new string[] { "WebApplication1.Controllers.Areas.m.Controllers" }
            );

            context.MapRoute(
                "jsonp",
                "m/home/jsonp",
                new { controller = "home", action = "jsonp" },
                new string[] { "WebApplication1.Controllers.Areas.m.Controllers" }
            );

            context.MapRoute(
                "ProcessCallback",
                "m/home/ProcessCallback",
                new { controller = "home", action = "ProcessCallback" },
                new string[] { "WebApplication1.Controllers.Areas.m.Controllers" }
            );

            context.MapRoute(
                "mFormExample",
                "m/home/FormExample",
                new { controller = "home", action = "FormExample" },
                new string[] { "WebApplication1.Controllers" }
            );

            context.MapRoute(
                "mFormSubmit",
                "m/home/formSubmit",
                new { controller = "home", action = "formSubmit" },
                new string[] { "WebApplication1.Controllers" }
            );

            context.MapRoute(
                name: "mDefault",
                url: "m/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
