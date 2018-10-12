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
    public class DiemCTHKiesController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/DiemCTHKies
        public ActionResult Index()
        {
            var diemCTHKys = db.DiemCTHKys.Include(d => d.DiemHocKy).Include(d => d.MonHoc);
            return View(diemCTHKys.ToList());
        }

        // GET: Admin/DiemCTHKies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemCTHKy diemCTHKy = db.DiemCTHKys.Find(id);
            if (diemCTHKy == null)
            {
                return HttpNotFound();
            }
            return View(diemCTHKy);
        }

        // GET: Admin/DiemCTHKies/Create
        public ActionResult Create()
        {
            ViewBag.MSSV = new SelectList(db.DiemHocKys, "MSSV", "MSSV");
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMon", "TenMon");
            return View();
        }

        // POST: Admin/DiemCTHKies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMon,MSSV,ID_HocKi,DiemTH,DiemQT,DiemThi1,DiemThi2,TiLeDiemTH,TiLeDiemQT,TiLeDiemThi1,TiLeDiemThi2,DiemTKHe10,DiemTKHe4,DiemTKChu")] DiemCTHKy diemCTHKy)
        {
            if (ModelState.IsValid)
            {
                db.DiemCTHKys.Add(diemCTHKy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MSSV = new SelectList(db.DiemHocKys, "MSSV", "MSSV", diemCTHKy.MSSV);
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMon", "TenMon", diemCTHKy.MaMon);
            return View(diemCTHKy);
        }

        // GET: Admin/DiemCTHKies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemCTHKy diemCTHKy = db.DiemCTHKys.Find(id);
            if (diemCTHKy == null)
            {
                return HttpNotFound();
            }
            ViewBag.MSSV = new SelectList(db.DiemHocKys, "MSSV", "MSSV", diemCTHKy.MSSV);
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMon", "TenMon", diemCTHKy.MaMon);
            return View(diemCTHKy);
        }

        // POST: Admin/DiemCTHKies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMon,MSSV,ID_HocKi,DiemTH,DiemQT,DiemThi1,DiemThi2,TiLeDiemTH,TiLeDiemQT,TiLeDiemThi1,TiLeDiemThi2,DiemTKHe10,DiemTKHe4,DiemTKChu")] DiemCTHKy diemCTHKy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemCTHKy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MSSV = new SelectList(db.DiemHocKys, "MSSV", "MSSV", diemCTHKy.MSSV);
            ViewBag.MaMon = new SelectList(db.MonHocs, "MaMon", "TenMon", diemCTHKy.MaMon);
            return View(diemCTHKy);
        }

        // GET: Admin/DiemCTHKies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemCTHKy diemCTHKy = db.DiemCTHKys.Find(id);
            if (diemCTHKy == null)
            {
                return HttpNotFound();
            }
            return View(diemCTHKy);
        }

        // POST: Admin/DiemCTHKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DiemCTHKy diemCTHKy = db.DiemCTHKys.Find(id);
            db.DiemCTHKys.Remove(diemCTHKy);
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
