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
    public class TinhsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/Tinhs
        public ActionResult Index()
        {
            return View(db.Tinhs.ToList());
        }

        // GET: Admin/Tinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tinh tinh = db.Tinhs.Find(id);
            if (tinh == null)
            {
                return HttpNotFound();
            }
            return View(tinh);
        }

        // GET: Admin/Tinhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tinhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinh,TenTinh,KhuVuc")] Tinh tinh)
        {
            if (ModelState.IsValid)
            {
                db.Tinhs.Add(tinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tinh);
        }

        // GET: Admin/Tinhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tinh tinh = db.Tinhs.Find(id);
            if (tinh == null)
            {
                return HttpNotFound();
            }
            return View(tinh);
        }

        // POST: Admin/Tinhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinh,TenTinh,KhuVuc")] Tinh tinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tinh);
        }

        // GET: Admin/Tinhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tinh tinh = db.Tinhs.Find(id);
            if (tinh == null)
            {
                return HttpNotFound();
            }
            return View(tinh);
        }

        // POST: Admin/Tinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tinh tinh = db.Tinhs.Find(id);
            db.Tinhs.Remove(tinh);
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
