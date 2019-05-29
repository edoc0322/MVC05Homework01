using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC05Homework01.Controllers
{
    public class 客戶資料統計Controller : BaseController
    {
        private 客戶資料統計Repository rep;
        public 客戶資料統計Controller()
        {
            rep = RepositoryHelper.Get客戶資料統計Repository();
        }
        // GET: 客戶資料統計
        public ActionResult Index()
        {
            return View(rep.All().ToList());
        }
    }
}