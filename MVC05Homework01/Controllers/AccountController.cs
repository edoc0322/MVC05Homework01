using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC05Homework01.Models;
using MVC05Homework01.ViewModels;

namespace MVC05Homework01.Controllers
{
    public class AccountController : Controller
    {

        [AllowAnonymous]
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            if (!new Services.LoginService().LoginValidation(model))
            {
                ModelState.AddModelError("", "無效的帳號或密碼兒");
                return View();
            }

            //FormsAuthentication.RedirectFromLoginPage(model.帳號,false);
            //FormsAuthentication.SetAuthCookie(model.帳號, false);
            //return Redirect(FormsAuthentication.GetRedirectUrl(model.帳號, false));



            var ticket = new System.Web.Security.FormsAuthenticationTicket(
                version: 1,
                name: model.帳號, //可以放使用者Id
                issueDate: DateTime.UtcNow,//現在UTC時間
                expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: true,// 是否要記住我 true or false
                userData: "超級賽亞人", //可以放使用者角色名稱
                cookiePath: System.Web.Security.FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
            //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
            return RedirectToAction("Index", "Home");
            //return Redirect(FormsAuthentication.GetRedirectUrl(ticket.Name, false));
        }

        public ActionResult Logout()
        {
            //FormsAuthentication.RedirectFromLoginPage(model.帳號,false);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}