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

namespace MVC05Homework01.Controllers
{
    public class 客戶銀行資訊Controller : BaseController
    {
        private 客戶銀行資訊Repository rep;
        private 客戶資料Repository rep客戶;

        public 客戶銀行資訊Controller()
        {

            rep = RepositoryHelper.Get客戶銀行資訊Repository();
            rep客戶 = RepositoryHelper.Get客戶資料Repository(rep.UnitOfWork);
        }
        // GET: 客戶銀行資訊
        public ActionResult Index(string 帳戶名稱)
        {
            var 客戶銀行資訊 = rep.AllOfQuery(帳戶名稱).Include(p => p.客戶資料);
            return View(客戶銀行資訊.ToList());
        }
        public FileResult Download()
        {
            var 客戶銀行資訊 = rep.AllOfNonDel().Include(p => p.客戶資料).Select(x =>
            new ViewModels.客戶銀行資訊ExportViewModel
            {
                客戶名稱 = x.客戶資料.客戶名稱,
                分行代碼 = x.分行代碼.HasValue ? x.分行代碼.ToString() : "",
                銀行代碼 = x.銀行代碼,
                帳戶名稱 = x.帳戶名稱,
                帳戶號碼 = x.帳戶號碼,
                銀行名稱 = x.銀行名稱
            });
            return new DownloadController().ExportExcelV2(客戶銀行資訊.ListToDataTable(), "客戶銀行資訊.xlsx", "Data");
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶銀行資訊 = rep.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        [銀行資訊取得客戶ID清單]
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [銀行資訊取得客戶ID清單]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                rep.Add(客戶銀行資訊);
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶銀行資訊);
        }

        [銀行資訊取得客戶ID清單]
        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶銀行資訊 = rep.Find(id.Value);
            if (客戶銀行資訊 == null)
                return HttpNotFound();
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [銀行資訊取得客戶ID清單]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                rep.UnitOfWork.Context.Entry(客戶銀行資訊).State = EntityState.Modified;
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var 客戶銀行資訊 = rep.Find(id.Value);
            if (客戶銀行資訊 == null)
                return HttpNotFound();
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var 客戶銀行資訊 = rep.Find(id);
            //rep.Delete(客戶銀行資訊);
            客戶銀行資訊.已刪除 = true;
            rep.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

    }
}
