using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC05Homework01.ActionFilters
{
    public class 銀行資訊取得客戶ID清單 : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values.ContainsKey("id") && int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int id))
            {
                var rep = RepositoryHelper.Get客戶銀行資訊Repository();
                var rep客戶 = RepositoryHelper.Get客戶資料Repository(rep.UnitOfWork);
                filterContext.Controller.ViewBag.客戶Id = new SelectList(rep客戶.All(), "Id", "客戶名稱", rep.Find(id).客戶Id);
            }
            else
            {
                var rep客戶 = RepositoryHelper.Get客戶資料Repository();
                filterContext.Controller.ViewBag.客戶Id = new SelectList(rep客戶.All(), "Id", "客戶名稱");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}