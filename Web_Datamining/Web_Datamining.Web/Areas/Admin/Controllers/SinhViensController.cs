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
    public class SinhViensController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/SinhViens
        public ActionResult Index()
        {
            var sinhViens = db.SinhViens.Include(s => s.HoSoXetTuyen).Include(s => s.Lop);
            return View(sinhViens.ToList());
        }

        // GET: Admin/SinhViens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: Admin/SinhViens/Create
        public ActionResult Create()
        {
            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN");
            ViewBag.ID_Lop = new SelectList(db.Lops, "ID_Lop", "MaChuyenNganh");
            return View();
        }

        // POST: Admin/SinhViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MSSV,MaHoSo,CoVanHocTap,ID_Lop")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN", sinhVien.MaHoSo);
            ViewBag.ID_Lop = new SelectList(db.Lops, "ID_Lop", "MaChuyenNganh", sinhVien.ID_Lop);
            return View(sinhVien);
        }

        // GET: Admin/SinhViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN", sinhVien.MaHoSo);
            ViewBag.ID_Lop = new SelectList(db.Lops, "ID_Lop", "MaChuyenNganh", sinhVien.ID_Lop);
            return View(sinhVien);
        }

        // POST: Admin/SinhViens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MSSV,MaHoSo,CoVanHocTap,ID_Lop")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN", sinhVien.MaHoSo);
            ViewBag.ID_Lop = new SelectList(db.Lops, "ID_Lop", "MaChuyenNganh", sinhVien.ID_Lop);
            return View(sinhVien);
        }

        // GET: Admin/SinhViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: Admin/SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SinhVien sinhVien = db.SinhViens.Find(id);
            db.SinhViens.Remove(sinhVien);
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
