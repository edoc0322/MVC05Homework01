using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC05Homework01.ActionFilters;
namespace MVC05Homework01.Controllers
{
    [Action效能計時器]
    [HandleError]
    [Authorize]
    public class BaseController : Controller
    {

    }
}