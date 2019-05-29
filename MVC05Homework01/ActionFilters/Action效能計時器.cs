using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC05Homework01.ActionFilters
{
    public class Action效能計時器 : ActionFilterAttribute
    {
        public Stopwatch stopWatchAction;
        public Stopwatch stopWatchView;

        public Action效能計時器()
        {
            stopWatchAction = new Stopwatch();
            stopWatchView = new Stopwatch();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatchAction.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var ControllerName = filterContext.RouteData.Values["Controller"].ToString();
            var ActionName = filterContext.RouteData.Values["Action"].ToString();

            stopWatchAction.Stop();
            var ts = stopWatchAction.Elapsed.TotalSeconds;
            stopWatchAction.Reset();
            Debug.WriteLine($"ActionTimer:{ts.ToString()}/Sec, From {ControllerName}/{ActionName}");
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            stopWatchView.Start();
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var ControllerName = filterContext.RouteData.Values["Controller"].ToString();
            var ActionName = filterContext.RouteData.Values["Action"].ToString();

            stopWatchView.Stop();
            var ts = stopWatchView.Elapsed.TotalSeconds;
            stopWatchView.Reset();
            Debug.WriteLine($"ViewTimer:{ts.ToString()}/Sec, From {ControllerName}/{ActionName}");
            base.OnResultExecuted(filterContext);
        }

    }
}