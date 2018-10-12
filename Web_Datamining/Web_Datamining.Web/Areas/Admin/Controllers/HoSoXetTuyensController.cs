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
    public class HoSoXetTuyensController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/HoSoXetTuyens
        public ActionResult Index()
        {
            var hoSoXetTuyens = db.HoSoXetTuyens.Include(h => h.DiemXetTuyen).Include(h => h.TruongTHPT);
            return View(hoSoXetTuyens.ToList());
        }

        // GET: Admin/HoSoXetTuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoXetTuyen hoSoXetTuyen = db.HoSoXetTuyens.Find(id);
            if (hoSoXetTuyen == null)
            {
                return HttpNotFound();
            }
            return View(hoSoXetTuyen);
        }

        // GET: Admin/HoSoXetTuyens/Create
        public ActionResult Create()
        {
            ViewBag.DXT_ID = new SelectList(db.DiemXetTuyens, "DXT_ID", "DXT_ID");
            ViewBag.MaTruongTHPT = new SelectList(db.TruongTHPTs, "MaTruongTHPT", "TenTruong");
            return View();
        }

        // POST: Admin/HoSoXetTuyens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHoSo,MaTruongTHPT,CMDN,NgaySinh,HoTen,GioiTinh,DanToc,TinhTrangTrungTuyen,DXT_ID")] HoSoXetTuyen hoSoXetTuyen)
        {
            if (ModelState.IsValid)
            {
                db.HoSoXetTuyens.Add(hoSoXetTuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DXT_ID = new SelectList(db.DiemXetTuyens, "DXT_ID", "DXT_ID", hoSoXetTuyen.DXT_ID);
            ViewBag.MaTruongTHPT = new SelectList(db.TruongTHPTs, "MaTruongTHPT", "TenTruong", hoSoXetTuyen.MaTruongTHPT);
            return View(hoSoXetTuyen);
        }

        // GET: Admin/HoSoXetTuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoXetTuyen hoSoXetTuyen = db.HoSoXetTuyens.Find(id);
            if (hoSoXetTuyen == null)
            {
                return HttpNotFound();
            }
            ViewBag.DXT_ID = new SelectList(db.DiemXetTuyens, "DXT_ID", "DXT_ID", hoSoXetTuyen.DXT_ID);
            ViewBag.MaTruongTHPT = new SelectList(db.TruongTHPTs, "MaTruongTHPT", "TenTruong", hoSoXetTuyen.MaTruongTHPT);
            return View(hoSoXetTuyen);
        }

        // POST: Admin/HoSoXetTuyens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHoSo,MaTruongTHPT,CMDN,NgaySinh,HoTen,GioiTinh,DanToc,TinhTrangTrungTuyen,DXT_ID")] HoSoXetTuyen hoSoXetTuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoSoXetTuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DXT_ID = new SelectList(db.DiemXetTuyens, "DXT_ID", "DXT_ID", hoSoXetTuyen.DXT_ID);
            ViewBag.MaTruongTHPT = new SelectList(db.TruongTHPTs, "MaTruongTHPT", "TenTruong", hoSoXetTuyen.MaTruongTHPT);
            return View(hoSoXetTuyen);
        }

        // GET: Admin/HoSoXetTuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoSoXetTuyen hoSoXetTuyen = db.HoSoXetTuyens.Find(id);
            if (hoSoXetTuyen == null)
            {
                return HttpNotFound();
            }
            return View(hoSoXetTuyen);
        }

        // POST: Admin/HoSoXetTuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoSoXetTuyen hoSoXetTuyen = db.HoSoXetTuyens.Find(id);
            db.HoSoXetTuyens.Remove(hoSoXetTuyen);
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
