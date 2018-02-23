using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [ActionName("index")]
        public ActionResult Index()
        {
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
    }
}