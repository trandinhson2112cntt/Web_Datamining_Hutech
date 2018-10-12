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
    public class DSNguyenVongsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/DSNguyenVongs
        public ActionResult Index()
        {
            var dSNguyenVongs = db.DSNguyenVongs.Include(d => d.HoSoXetTuyen).Include(d => d.NganhTheoBo).Include(d => d.ToHopMon);
            return View(dSNguyenVongs.ToList());
        }

        // GET: Admin/DSNguyenVongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSNguyenVong dSNguyenVong = db.DSNguyenVongs.Find(id);
            if (dSNguyenVong == null)
            {
                return HttpNotFound();
            }
            return View(dSNguyenVong);
        }

        // GET: Admin/DSNguyenVongs/Create
        public ActionResult Create()
        {
            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN");
            ViewBag.MaNganh = new SelectList(db.NganhTheoBos, "MaNganh", "TeNganh");
            ViewBag.MaToHop = new SelectList(db.ToHopMons, "MaToHop", "Mon1");
            return View();
        }

        // POST: Admin/DSNguyenVongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHoSo,ThuTuNV,MaToHop,MaNganh,MaTDH,TrangThaiNV")] DSNguyenVong dSNguyenVong)
        {
            if (ModelState.IsValid)
            {
                db.DSNguyenVongs.Add(dSNguyenVong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN", dSNguyenVong.MaHoSo);
            ViewBag.MaNganh = new SelectList(db.NganhTheoBos, "MaNganh", "TeNganh", dSNguyenVong.MaNganh);
            ViewBag.MaToHop = new SelectList(db.ToHopMons, "MaToHop", "Mon1", dSNguyenVong.MaToHop);
            return View(dSNguyenVong);
        }

        // GET: Admin/DSNguyenVongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSNguyenVong dSNguyenVong = db.DSNguyenVongs.Find(id);
            if (dSNguyenVong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN", dSNguyenVong.MaHoSo);
            ViewBag.MaNganh = new SelectList(db.NganhTheoBos, "MaNganh", "TeNganh", dSNguyenVong.MaNganh);
            ViewBag.MaToHop = new SelectList(db.ToHopMons, "MaToHop", "Mon1", dSNguyenVong.MaToHop);
            return View(dSNguyenVong);
        }

        // POST: Admin/DSNguyenVongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHoSo,ThuTuNV,MaToHop,MaNganh,MaTDH,TrangThaiNV")] DSNguyenVong dSNguyenVong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dSNguyenVong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHoSo = new SelectList(db.HoSoXetTuyens, "MaHoSo", "CMDN", dSNguyenVong.MaHoSo);
            ViewBag.MaNganh = new SelectList(db.NganhTheoBos, "MaNganh", "TeNganh", dSNguyenVong.MaNganh);
            ViewBag.MaToHop = new SelectList(db.ToHopMons, "MaToHop", "Mon1", dSNguyenVong.MaToHop);
            return View(dSNguyenVong);
        }

        // GET: Admin/DSNguyenVongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSNguyenVong dSNguyenVong = db.DSNguyenVongs.Find(id);
            if (dSNguyenVong == null)
            {
                return HttpNotFound();
            }
            return View(dSNguyenVong);
        }

        // POST: Admin/DSNguyenVongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DSNguyenVong dSNguyenVong = db.DSNguyenVongs.Find(id);
            db.DSNguyenVongs.Remove(dSNguyenVong);
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
