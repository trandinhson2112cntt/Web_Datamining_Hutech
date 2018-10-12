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
    public class TruongTHPTsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/TruongTHPTs
        public ActionResult Index()
        {
            var truongTHPTs = db.TruongTHPTs.Include(t => t.Huyen);
            return View(truongTHPTs.ToList());
        }

        // GET: Admin/TruongTHPTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongTHPT truongTHPT = db.TruongTHPTs.Find(id);
            if (truongTHPT == null)
            {
                return HttpNotFound();
            }
            return View(truongTHPT);
        }

        // GET: Admin/TruongTHPTs/Create
        public ActionResult Create()
        {
            ViewBag.MaHuyen = new SelectList(db.Huyens, "MaHuyen", "khuvuc");
            return View();
        }

        // POST: Admin/TruongTHPTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTruongTHPT,MaHuyen,MaTinh,TenTruong")] TruongTHPT truongTHPT)
        {
            if (ModelState.IsValid)
            {
                db.TruongTHPTs.Add(truongTHPT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHuyen = new SelectList(db.Huyens, "MaHuyen", "khuvuc", truongTHPT.MaHuyen);
            return View(truongTHPT);
        }

        // GET: Admin/TruongTHPTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongTHPT truongTHPT = db.TruongTHPTs.Find(id);
            if (truongTHPT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHuyen = new SelectList(db.Huyens, "MaHuyen", "khuvuc", truongTHPT.MaHuyen);
            return View(truongTHPT);
        }

        // POST: Admin/TruongTHPTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTruongTHPT,MaHuyen,MaTinh,TenTruong")] TruongTHPT truongTHPT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truongTHPT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHuyen = new SelectList(db.Huyens, "MaHuyen", "khuvuc", truongTHPT.MaHuyen);
            return View(truongTHPT);
        }

        // GET: Admin/TruongTHPTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruongTHPT truongTHPT = db.TruongTHPTs.Find(id);
            if (truongTHPT == null)
            {
                return HttpNotFound();
            }
            return View(truongTHPT);
        }

        // POST: Admin/TruongTHPTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TruongTHPT truongTHPT = db.TruongTHPTs.Find(id);
            db.TruongTHPTs.Remove(truongTHPT);
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
