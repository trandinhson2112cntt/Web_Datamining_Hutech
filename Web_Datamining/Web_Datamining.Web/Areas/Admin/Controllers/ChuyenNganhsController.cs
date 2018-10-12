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
    public class ChuyenNganhsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/ChuyenNganhs
        public ActionResult Index()
        {
            var chuyenNganhs = db.ChuyenNganhs.Include(c => c.Khoa);
            return View(chuyenNganhs.ToList());
        }

        // GET: Admin/ChuyenNganhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenNganh chuyenNganh = db.ChuyenNganhs.Find(id);
            if (chuyenNganh == null)
            {
                return HttpNotFound();
            }
            return View(chuyenNganh);
        }

        // GET: Admin/ChuyenNganhs/Create
        public ActionResult Create()
        {
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: Admin/ChuyenNganhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChuyenNganh,TenChuyenNganh,MaKhoa")] ChuyenNganh chuyenNganh)
        {
            if (ModelState.IsValid)
            {
                db.ChuyenNganhs.Add(chuyenNganh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", chuyenNganh.MaKhoa);
            return View(chuyenNganh);
        }

        // GET: Admin/ChuyenNganhs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenNganh chuyenNganh = db.ChuyenNganhs.Find(id);
            if (chuyenNganh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", chuyenNganh.MaKhoa);
            return View(chuyenNganh);
        }

        // POST: Admin/ChuyenNganhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChuyenNganh,TenChuyenNganh,MaKhoa")] ChuyenNganh chuyenNganh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuyenNganh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhoa = new SelectList(db.Khoas, "MaKhoa", "TenKhoa", chuyenNganh.MaKhoa);
            return View(chuyenNganh);
        }

        // GET: Admin/ChuyenNganhs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenNganh chuyenNganh = db.ChuyenNganhs.Find(id);
            if (chuyenNganh == null)
            {
                return HttpNotFound();
            }
            return View(chuyenNganh);
        }

        // POST: Admin/ChuyenNganhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChuyenNganh chuyenNganh = db.ChuyenNganhs.Find(id);
            db.ChuyenNganhs.Remove(chuyenNganh);
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
