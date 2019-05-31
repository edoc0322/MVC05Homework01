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
using MVC05Homework01.ViewModels;

using X.PagedList;
using X.PagedList.Mvc;

namespace MVC05Homework01.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {
        private 客戶聯絡人Repository rep;
        private 客戶資料Repository rep客戶;

        public 客戶聯絡人Controller()
        {
            rep = RepositoryHelper.Get客戶聯絡人Repository();
            rep客戶 = RepositoryHelper.Get客戶資料Repository(rep.UnitOfWork);
        }
        // GET: 客戶聯絡人
        public ActionResult Index(string 職稱篩選, string currentSort, int Page = 1, string sortOrder = "姓名")
        {
            ViewBag.CurrentSort = sortOrder.Equals(currentSort) ? null : sortOrder;
            ViewBag.職稱篩選 = new SelectList(items: rep.Get職稱List().ToList());
            var 客戶聯絡人 = rep.JobTitleQuery(職稱篩選).Include(p => p.客戶資料).Sort(sortOrder, currentSort);
            //return View(客戶聯絡人.ToList());

            var 客戶聯絡人批次更新ViewModel = 客戶聯絡人.Select(x => new 客戶聯絡人批次更新ViewModel
            {
                姓名 = x.姓名,
                Email = x.Email,
                Id = x.Id,
                客戶Id = x.客戶Id,
                手機 = x.手機,
                職稱 = x.職稱,
                電話 = x.電話,
                客戶資料 = x.客戶資料
            });
            
            return View(客戶聯絡人批次更新ViewModel.ToPagedList(Page, 10));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(客戶聯絡人批次更新ViewModel[] data)
        {
            ViewBag.CurrentSort = null;
            ViewBag.職稱篩選 = new SelectList(items: rep.Get職稱List().ToList());

            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var 客戶聯絡人 = rep.Find(item.Id);
                    客戶聯絡人.職稱 = item.職稱;
                    客戶聯絡人.手機 = item.手機;
                    客戶聯絡人.電話 = item.電話;
                }
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index", "Home");
            }

            var 客戶聯絡人批次更新ViewModel = rep.All().Select(x => new 客戶聯絡人批次更新ViewModel
            {
                姓名 = x.姓名,
                Email = x.Email,
                Id = x.Id,
                客戶Id = x.客戶Id,
                手機 = x.手機,
                職稱 = x.職稱,
                電話 = x.電話,
                客戶資料 = x.客戶資料
            });
            return View(客戶聯絡人批次更新ViewModel);
        }


        [ChildActionOnly]
        [Route("List/{客戶ID}")]
        public ActionResult List(int 客戶ID)
        {
            var model = rep.All().Where(x => x.客戶Id == 客戶ID);
            return PartialView(model);
        }

        public FileResult Download(string 職稱篩選, string currentSort, string sortOrder = "姓名")
        {
            var 客戶聯絡人 = rep.JobTitleQuery(職稱篩選).Include(p => p.客戶資料).Sort(sortOrder, currentSort).Select(x =>
            new ViewModels.客戶聯絡人ExportViewModel
            {
                客戶名稱 = x.客戶資料.客戶名稱,
                姓名 = x.姓名,
                Email = x.Email,
                手機 = x.手機,
                職稱 = x.職稱,
                電話 = x.電話
            });
            return new DownloadController().ExportExcelV2(客戶聯絡人.ListToDataTable(), "客戶聯絡人.xlsx", "Data");
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var 客戶聯絡人 = rep.Find(id.Value);
            if (客戶聯絡人 == null)
                return HttpNotFound();
            return View(客戶聯絡人);
        }

        [聯絡人資訊取得客戶ID清單]
        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [聯絡人資訊取得客戶ID清單]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                rep.Add(客戶聯絡人);
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶聯絡人);
        }
        [聯絡人資訊取得客戶ID清單]
        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var 客戶聯絡人 = rep.Find(id.Value);
            if (客戶聯絡人 == null)
                return HttpNotFound();
            ModelState.Remove("Email");
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [聯絡人資訊取得客戶ID清單]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            ModelState.Remove("Email");
            if (ModelState.IsValid)
            {
                rep.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                rep.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var 客戶聯絡人 = rep.Find(id.Value);
            if (客戶聯絡人 == null)
                return HttpNotFound();
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var 客戶聯絡人 = rep.Find(id);
            //rep.Delete(客戶聯絡人);
            客戶聯絡人.已刪除 = true;
            rep.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}
