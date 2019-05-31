using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC05Homework01.ActionFilters;
using MVC05Homework01.Extensions;
using MVC05Homework01.Models;
using Newtonsoft.Json;
using X.PagedList;
using X.PagedList.Mvc;
namespace MVC05Homework01.Controllers
{
    public class 客戶資料Controller : BaseController
    {
        private 客戶資料Repository rep;
        private 客戶聯絡人Repository rep聯絡人;

        public 客戶資料Controller()
        {
            //要用Helper啦

            rep = RepositoryHelper.Get客戶資料Repository();
            rep聯絡人 = RepositoryHelper.Get客戶聯絡人Repository(rep.UnitOfWork);
        }

        public ActionResult CreateAccPwd(int? id)
        {

            var data = rep.Find(id.Value);
            data.帳號 = "qweerty0322";
            var sha256 = new System.Security.Cryptography.SHA256CryptoServiceProvider();//建立一個SHA256
            byte[] source = System.Text.Encoding.Default.GetBytes("123456");//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串

            data.密碼 = result;
            rep.UnitOfWork.Commit();
            return RedirectToAction("Index", "Home");
        }

        [取得分類清單]
        // GET: 客戶資料
        public ActionResult Index(int? 分類篩選, string 客戶名稱, string currentSort, int Page = 1, string sortOrder = "客戶名稱")
        {
            var idss = (System.Web.Security.FormsIdentity)User.Identity;
            System.Web.Security.FormsAuthenticationTicket ticket = idss.Ticket;


            ViewBag.CurrentSort = sortOrder.Equals(currentSort) ? null : sortOrder;

            //var a =
            //    rep.AllOfQuery(分類篩選, 客戶名稱).Sort(sortOrder, currentSort).ToPagedList(Page, 10)
            //    .Select(x => new ViewModels.客戶資料ViewModel
            //    {
            //        Id = x.Id,
            //        客戶分類 = (EnumModel.客戶分類)x.客戶分類,
            //        客戶名稱 = x.客戶名稱,
            //        Email = x.Email,
            //        傳真 = x.傳真,
            //        地址 = x.地址,
            //        統一編號 = x.統一編號,
            //        電話 = x.電話,
            //        已刪除 = x.已刪除
            //    });

            var temp = rep.AllOfQuery(分類篩選, 客戶名稱).Sort(sortOrder, currentSort)
                       //.ToPagedList<ViewModels.客戶資料ViewModel>()
                       .Select(x => new ViewModels.客戶資料ViewModel()
                       {
                           Id = x.Id,
                           客戶分類 = (EnumModel.客戶分類)x.客戶分類,
                           客戶名稱 = x.客戶名稱,
                           Email = x.Email,
                           傳真 = x.傳真,
                           地址 = x.地址,
                           統一編號 = x.統一編號,
                           電話 = x.電話,
                           已刪除 = x.已刪除
                       });

            return View(temp.ToPagedList(Page, 10));
        }

        public FileResult Download(int? 分類篩選, string 客戶名稱, string currentSort, string sortOrder = "客戶名稱")
        {
            var 客戶資料 = rep.AllOfQuery(分類篩選, 客戶名稱).Sort(sortOrder, currentSort).Select(x =>
             new ViewModels.客戶資料ExportViewModel
             {
                 客戶名稱 = x.客戶名稱,
                 統一編號 = x.統一編號,
                 Email = x.Email,
                 傳真 = x.傳真,
                 電話 = x.電話,
                 地址 = x.地址
             });
            return new DownloadController().ExportExcelV2(客戶資料.ListToDataTable(), "客戶資料.xlsx", "Data");
            //return new DownloadController.ExportExcelResult
            //{
            //    SheetName = "Data",
            //    FileName = "客戶資料.xlsx",
            //    ExportData = db
            //};
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var 客戶資料 = rep.Find(id.Value);
            客戶資料.客戶聯絡人 = rep聯絡人.All().Where(x => x.客戶Id == id.Value).ToList();

            if (客戶資料 == null)
                return HttpNotFound();

            var 客戶資料ViewModel = new ViewModels.客戶資料ViewModel
            {
                Id = 客戶資料.Id,
                客戶分類 = (EnumModel.客戶分類)客戶資料.客戶分類,
                客戶名稱 = 客戶資料.客戶名稱,
                電話 = 客戶資料.電話,
                統一編號 = 客戶資料.統一編號,
                已刪除 = 客戶資料.已刪除,
                地址 = 客戶資料.地址,
                Email = 客戶資料.Email,
                傳真 = 客戶資料.傳真,
                客戶聯絡人 = 客戶資料.客戶聯絡人
            };

            return View(客戶資料ViewModel);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶分類,客戶名稱,統一編號,電話,傳真,地址,Email")] ViewModels.客戶資料ViewModel 客戶資料ViewModel)
        {
            if (ModelState.IsValid)
            {
                var 客戶資料 = new 客戶資料
                {
                    Id = 客戶資料ViewModel.Id,
                    客戶分類 = (int)客戶資料ViewModel.客戶分類,
                    客戶名稱 = 客戶資料ViewModel.客戶名稱,
                    Email = 客戶資料ViewModel.Email,
                    傳真 = 客戶資料ViewModel.傳真,
                    地址 = 客戶資料ViewModel.地址,
                    統一編號 = 客戶資料ViewModel.統一編號,
                    電話 = 客戶資料ViewModel.電話,
                    已刪除 = false
                };

                rep.Add(客戶資料);
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料ViewModel);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶資料 = rep.Find(id.Value);
            if (客戶資料 == null)
                return HttpNotFound();

            var 客戶資料ViewModel = new ViewModels.客戶資料ViewModel
            {
                Id = 客戶資料.Id,
                客戶分類 = (EnumModel.客戶分類)客戶資料.客戶分類,
                客戶名稱 = 客戶資料.客戶名稱,
                電話 = 客戶資料.電話,
                統一編號 = 客戶資料.統一編號,
                已刪除 = 客戶資料.已刪除,
                地址 = 客戶資料.地址,
                Email = 客戶資料.Email,
                傳真 = 客戶資料.傳真,
                帳號 = 客戶資料.帳號
            };

            return View(客戶資料ViewModel);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        //public ActionResult Edit([Bind(Include = "Id,客戶分類,客戶名稱,統一編號,電話,傳真,地址,Email,已刪除")] ViewModels.客戶資料ViewModel 客戶資料ViewModel)
        {
            var 客戶資料 = rep.Find(id);

            if (TryUpdateModel<客戶資料>(客戶資料) && TryValidateModel(客戶資料))
            {
                if (!string.IsNullOrEmpty(客戶資料.密碼))
                {
                    客戶資料.密碼 = new Services.LoginService().EncryPasswd(客戶資料.密碼);
                }
                else
                {/*如果沒輸入密碼 就在把原本密碼存回去?*/
                    var 客戶資料pwdCheck = RepositoryHelper.Get客戶資料Repository().Find(id);
                    if (!string.IsNullOrEmpty(客戶資料pwdCheck.密碼))
                        客戶資料.密碼 = 客戶資料pwdCheck.密碼;
                }
                //
                //密碼 = new Services.LoginService().EncryPasswd(客戶資料.密碼)
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            var 客戶資料ViewModel = new ViewModels.客戶資料ViewModel
            {
                客戶名稱 = 客戶資料.客戶名稱,
                Email = 客戶資料.Email,
                Id = 客戶資料.Id,
                傳真 = 客戶資料.傳真,
                地址 = 客戶資料.地址,
                客戶分類 = (EnumModel.客戶分類)客戶資料.客戶分類,
                已刪除 = 客戶資料.已刪除,
                統一編號 = 客戶資料.統一編號,
                電話 = 客戶資料.電話,
                帳號 = 客戶資料.帳號
            };
            if (!string.IsNullOrEmpty(客戶資料.密碼))
            {

            }

            return View(客戶資料ViewModel);

            //如果Input沒有 FormCollection 那TryUpdateModel就會進行模型驗證，如果有就不會，就要搭配TryValidateModel 做模型驗證。
            //if (ModelState.IsValid)
            //{
            //    var 客戶資料 = new 客戶資料
            //    {
            //        Id = 客戶資料ViewModel.Id,
            //        客戶分類 = (int)客戶資料ViewModel.客戶分類,
            //        客戶名稱 = 客戶資料ViewModel.客戶名稱,
            //        Email = 客戶資料ViewModel.Email,
            //        傳真 = 客戶資料ViewModel.傳真,
            //        地址 = 客戶資料ViewModel.地址,
            //        統一編號 = 客戶資料ViewModel.統一編號,
            //        電話 = 客戶資料ViewModel.電話,
            //        已刪除 = false
            //    };
            //
            //    rep.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
            //    rep.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶資料 = rep.Find(id.Value);
            if (客戶資料 == null)
                return HttpNotFound();
            var 客戶資料ViewModel = new ViewModels.客戶資料ViewModel
            {
                Id = 客戶資料.Id,
                客戶分類 = (EnumModel.客戶分類)客戶資料.客戶分類,
                客戶名稱 = 客戶資料.客戶名稱,
                Email = 客戶資料.Email,
                傳真 = 客戶資料.傳真,
                地址 = 客戶資料.地址,
                統一編號 = 客戶資料.統一編號,
                電話 = 客戶資料.電話,
                已刪除 = 客戶資料.已刪除
            };

            return View(客戶資料ViewModel);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //model.id = viewmodel.id 不用mapper了...
            var 客戶資料 = rep.Find(id);
            //rep.Delete(客戶資料);
            客戶資料.已刪除 = true;
            rep.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}
