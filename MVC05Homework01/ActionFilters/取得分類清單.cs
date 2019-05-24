using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC05Homework01.ActionFilters
{
    public class 取得分類清單: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.分類 = new SelectList(Enum.GetValues(typeof(EnumModel.客戶分類)).Cast<EnumModel.客戶分類>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");
          
            base.OnActionExecuting(filterContext);
        }
    }
}