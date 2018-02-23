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
                "m/index",
                new { controller = "home", action = "index" },
                new string[] { "WebApplication1.Controllers.Areas.m.Controllers" }
            );

            context.MapRoute(
                name: "mDefault",
                url: "m/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
