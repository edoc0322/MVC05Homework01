using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC05Homework01.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var a = "";
            if (HttpContext.User != null && HttpContext.User.Identity is FormsIdentity)
            {
                var b = "";
            }


            var id = (System.Web.Security.FormsIdentity)User.Identity;
            System.Web.Security.FormsAuthenticationTicket ticket = id.Ticket;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}