using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC05Homework01.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public JsonResult CustomNameRepeatCheck(string 客戶名稱)
        {
            return Json(!RepositoryHelper.Get客戶資料Repository().All().Any(x => x.客戶名稱.Equals(客戶名稱)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmailRepeatCheckOnCustomer(int 客戶Id, string Email)
        {
            return Json(!RepositoryHelper.Get客戶聯絡人Repository().All().Any(x => x.客戶Id == 客戶Id && x.Email.Equals(x.Email)), JsonRequestBehavior.AllowGet);
        }

    }
}