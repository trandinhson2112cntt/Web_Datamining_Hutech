using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Datamining.Data;
using Web_Datamining.Models;

namespace Web_Datamining.Web.Areas.Admin.Controllers
{
    public class HocKiesController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/HocKies
        public ActionResult Index()
        {
            return View(db.HocKys.ToList());
        }

        // GET: Admin/HocKies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKy hocKy = db.HocKys.Find(id);
            if (hocKy == null)
            {
                return HttpNotFound();
            }
            return View(hocKy);
        }

        // GET: Admin/HocKies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HocKies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_HocKi,NamHoc,KyHoc")] HocKy hocKy)
        {
            if (ModelState.IsValid)
            {
                db.HocKys.Add(hocKy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hocKy);
        }

        // GET: Admin/HocKies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKy hocKy = db.HocKys.Find(id);
            if (hocKy == null)
            {
                return HttpNotFound();
            }
            return View(hocKy);
        }

        // POST: Admin/HocKies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_HocKi,NamHoc,KyHoc")] HocKy hocKy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocKy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hocKy);
        }

        // GET: Admin/HocKies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocKy hocKy = db.HocKys.Find(id);
            if (hocKy == null)
            {
                return HttpNotFound();
            }
            return View(hocKy);
        }

        // POST: Admin/HocKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HocKy hocKy = db.HocKys.Find(id);
            db.HocKys.Remove(hocKy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
