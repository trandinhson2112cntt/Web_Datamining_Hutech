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
    public class DiemHocKiesController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/DiemHocKies
        public ActionResult Index()
        {
            var diemHocKys = db.DiemHocKys.Include(d => d.HocKy).Include(d => d.SinhVien);
            return View(diemHocKys.ToList());
        }

        // GET: Admin/DiemHocKies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocKy diemHocKy = db.DiemHocKys.Find(id);
            if (diemHocKy == null)
            {
                return HttpNotFound();
            }
            return View(diemHocKy);
        }

        // GET: Admin/DiemHocKies/Create
        public ActionResult Create()
        {
            ViewBag.ID_HocKi = new SelectList(db.HocKys, "ID_HocKi", "NamHoc");
            ViewBag.MSSV = new SelectList(db.SinhViens, "MSSV", "CoVanHocTap");
            return View();
        }

        // POST: Admin/DiemHocKies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MSSV,ID_HocKi,SoTCDK,SoTCD,SoTCTL,DiemTBTLHe4")] DiemHocKy diemHocKy)
        {
            if (ModelState.IsValid)
            {
                db.DiemHocKys.Add(diemHocKy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_HocKi = new SelectList(db.HocKys, "ID_HocKi", "NamHoc", diemHocKy.ID_HocKi);
            ViewBag.MSSV = new SelectList(db.SinhViens, "MSSV", "CoVanHocTap", diemHocKy.MSSV);
            return View(diemHocKy);
        }

        // GET: Admin/DiemHocKies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocKy diemHocKy = db.DiemHocKys.Find(id);
            if (diemHocKy == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_HocKi = new SelectList(db.HocKys, "ID_HocKi", "NamHoc", diemHocKy.ID_HocKi);
            ViewBag.MSSV = new SelectList(db.SinhViens, "MSSV", "CoVanHocTap", diemHocKy.MSSV);
            return View(diemHocKy);
        }

        // POST: Admin/DiemHocKies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MSSV,ID_HocKi,SoTCDK,SoTCD,SoTCTL,DiemTBTLHe4")] DiemHocKy diemHocKy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemHocKy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_HocKi = new SelectList(db.HocKys, "ID_HocKi", "NamHoc", diemHocKy.ID_HocKi);
            ViewBag.MSSV = new SelectList(db.SinhViens, "MSSV", "CoVanHocTap", diemHocKy.MSSV);
            return View(diemHocKy);
        }

        // GET: Admin/DiemHocKies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemHocKy diemHocKy = db.DiemHocKys.Find(id);
            if (diemHocKy == null)
            {
                return HttpNotFound();
            }
            return View(diemHocKy);
        }

        // POST: Admin/DiemHocKies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DiemHocKy diemHocKy = db.DiemHocKys.Find(id);
            db.DiemHocKys.Remove(diemHocKy);
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
