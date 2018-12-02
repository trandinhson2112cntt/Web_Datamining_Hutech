using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Datamining.Data;
using Web_Datamining.Model.Models;

namespace Web_Datamining.Web.Areas.Admin.Controllers
{
    public class KhaoSatsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/KhaoSats
        public ActionResult Index()
        {
            return View(db.KhaoSat.ToList());
        }

        // GET: Admin/KhaoSats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhaoSat khaoSat = db.KhaoSat.Find(id);
            if (khaoSat == null)
            {
                return HttpNotFound();
            }
            return View(khaoSat);
        }

        // GET: Admin/KhaoSats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhaoSats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CMND,Khoi,DiemMon1,DiemMon2,DiemMon3")] KhaoSat khaoSat)
        {
            if (ModelState.IsValid)
            {
                db.KhaoSat.Add(khaoSat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khaoSat);
        }

        // GET: Admin/KhaoSats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhaoSat khaoSat = db.KhaoSat.Find(id);
            if (khaoSat == null)
            {
                return HttpNotFound();
            }
            return View(khaoSat);
        }

        // POST: Admin/KhaoSats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CMND,Khoi,DiemMon1,DiemMon2,DiemMon3")] KhaoSat khaoSat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khaoSat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khaoSat);
        }

        // GET: Admin/KhaoSats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhaoSat khaoSat = db.KhaoSat.Find(id);
            if (khaoSat == null)
            {
                return HttpNotFound();
            }
            return View(khaoSat);
        }

        // POST: Admin/KhaoSats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhaoSat khaoSat = db.KhaoSat.Find(id);
            db.KhaoSat.Remove(khaoSat);
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
