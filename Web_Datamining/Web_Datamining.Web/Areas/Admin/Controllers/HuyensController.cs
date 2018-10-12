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
    public class HuyensController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/Huyens
        public ActionResult Index()
        {
            var huyens = db.Huyens.Include(h => h.Tinh);
            return View(huyens.ToList());
        }

        // GET: Admin/Huyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Huyen huyen = db.Huyens.Find(id);
            if (huyen == null)
            {
                return HttpNotFound();
            }
            return View(huyen);
        }

        // GET: Admin/Huyens/Create
        public ActionResult Create()
        {
            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh");
            return View();
        }

        // POST: Admin/Huyens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHuyen,MaTinh,khuvuc,TenHuyen")] Huyen huyen)
        {
            if (ModelState.IsValid)
            {
                db.Huyens.Add(huyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh", huyen.MaTinh);
            return View(huyen);
        }

        // GET: Admin/Huyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Huyen huyen = db.Huyens.Find(id);
            if (huyen == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh", huyen.MaTinh);
            return View(huyen);
        }

        // POST: Admin/Huyens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHuyen,MaTinh,khuvuc,TenHuyen")] Huyen huyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(huyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTinh = new SelectList(db.Tinhs, "MaTinh", "TenTinh", huyen.MaTinh);
            return View(huyen);
        }

        // GET: Admin/Huyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Huyen huyen = db.Huyens.Find(id);
            if (huyen == null)
            {
                return HttpNotFound();
            }
            return View(huyen);
        }

        // POST: Admin/Huyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Huyen huyen = db.Huyens.Find(id);
            db.Huyens.Remove(huyen);
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
