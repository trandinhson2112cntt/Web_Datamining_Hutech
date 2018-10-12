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
    public class LopsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/Lops
        public ActionResult Index()
        {
            var lops = db.Lops.Include(l => l.ChuyenNganh).Include(l => l.HeDaoTao).Include(l => l.KhoaHoc);
            return View(lops.ToList());
        }

        // GET: Admin/Lops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // GET: Admin/Lops/Create
        public ActionResult Create()
        {
            ViewBag.MaChuyenNganh = new SelectList(db.ChuyenNganhs, "MaChuyenNganh", "TenChuyenNganh");
            ViewBag.MaHeDaoTao = new SelectList(db.HeDaoTaos, "MaHeDaoTao", "TenHeDaoTao");
            ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "NamHoc");
            return View();
        }

        // POST: Admin/Lops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Lop,MaChuyenNganh,MaHeDaoTao,MaKhoaHoc,TenLop")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Lops.Add(lop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChuyenNganh = new SelectList(db.ChuyenNganhs, "MaChuyenNganh", "TenChuyenNganh", lop.MaChuyenNganh);
            ViewBag.MaHeDaoTao = new SelectList(db.HeDaoTaos, "MaHeDaoTao", "TenHeDaoTao", lop.MaHeDaoTao);
            ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "NamHoc", lop.MaKhoaHoc);
            return View(lop);
        }

        // GET: Admin/Lops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuyenNganh = new SelectList(db.ChuyenNganhs, "MaChuyenNganh", "TenChuyenNganh", lop.MaChuyenNganh);
            ViewBag.MaHeDaoTao = new SelectList(db.HeDaoTaos, "MaHeDaoTao", "TenHeDaoTao", lop.MaHeDaoTao);
            ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "NamHoc", lop.MaKhoaHoc);
            return View(lop);
        }

        // POST: Admin/Lops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Lop,MaChuyenNganh,MaHeDaoTao,MaKhoaHoc,TenLop")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChuyenNganh = new SelectList(db.ChuyenNganhs, "MaChuyenNganh", "TenChuyenNganh", lop.MaChuyenNganh);
            ViewBag.MaHeDaoTao = new SelectList(db.HeDaoTaos, "MaHeDaoTao", "TenHeDaoTao", lop.MaHeDaoTao);
            ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "NamHoc", lop.MaKhoaHoc);
            return View(lop);
        }

        // GET: Admin/Lops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Admin/Lops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lop lop = db.Lops.Find(id);
            db.Lops.Remove(lop);
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
