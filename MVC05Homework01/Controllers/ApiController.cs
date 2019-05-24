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
        public JsonResult 客戶的聯絡人Email重覆確認(int 客戶Id, string Email)
        {
            return Json(!RepositoryHelper.Get客戶聯絡人Repository().All()
                .Any(x => x.客戶Id == 客戶Id && x.Email.Equals(x.Email))
                , JsonRequestBehavior.AllowGet);
        }

        // GET: Api
        public JsonResult CustomNameRepeatCheck(string 客戶名稱)
        {
            return Json(!RepositoryHelper.Get客戶資料Repository().All()
                .Any(x => x.客戶名稱.Equals(客戶名稱))
                , JsonRequestBehavior.AllowGet);
        }
    }
}