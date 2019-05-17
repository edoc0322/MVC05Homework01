using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC05Homework01.Extensions;
using MVC05Homework01.Models;
using Newtonsoft.Json;
using PagedList;
namespace MVC05Homework01.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Repository rep;
        private 客戶聯絡人Repository rep聯絡人;

        public 客戶資料Controller()
        {
            //要用Helper啦

            rep = RepositoryHelper.Get客戶資料Repository();
            rep聯絡人 = RepositoryHelper.Get客戶聯絡人Repository(rep.UnitOfWork);
        }

        // GET: 客戶資料
        public ActionResult Index(string 客戶名稱, string currentSort, string sortOrder = "客戶名稱")
        {
            ViewBag.CurrentSort = sortOrder.Equals(currentSort) ? null : sortOrder;

            return View(rep.AllOfQuery(客戶名稱).Sort(sortOrder, currentSort));
        }

        public FileResult Download(string 客戶名稱, string currentSort, string sortOrder = "客戶名稱")
        {
            var 客戶資料 = rep.AllOfQuery(客戶名稱).Sort(sortOrder, currentSort).Select(x =>
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
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶資料 = rep.Find(id.Value);
            客戶資料.客戶聯絡人 = rep聯絡人.All().Where(x => x.客戶Id == id.Value).ToList();

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
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
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                rep.Add(客戶資料);
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
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
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,已刪除")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                rep.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
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
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var 客戶資料 = rep.Find(id);
            //rep.Delete(客戶資料);
            客戶資料.已刪除 = true;
            rep.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}
