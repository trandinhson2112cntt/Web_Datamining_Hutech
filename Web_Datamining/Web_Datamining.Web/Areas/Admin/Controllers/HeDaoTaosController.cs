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
    public class HeDaoTaosController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/HeDaoTaos
        public ActionResult Index()
        {
            return View(db.HeDaoTaos.ToList());
        }

        // GET: Admin/HeDaoTaos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeDaoTao heDaoTao = db.HeDaoTaos.Find(id);
            if (heDaoTao == null)
            {
                return HttpNotFound();
            }
            return View(heDaoTao);
        }

        // GET: Admin/HeDaoTaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HeDaoTaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHeDaoTao,TenHeDaoTao")] HeDaoTao heDaoTao)
        {
            if (ModelState.IsValid)
            {
                db.HeDaoTaos.Add(heDaoTao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(heDaoTao);
        }

        // GET: Admin/HeDaoTaos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeDaoTao heDaoTao = db.HeDaoTaos.Find(id);
            if (heDaoTao == null)
            {
                return HttpNotFound();
            }
            return View(heDaoTao);
        }

        // POST: Admin/HeDaoTaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHeDaoTao,TenHeDaoTao")] HeDaoTao heDaoTao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(heDaoTao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(heDaoTao);
        }

        // GET: Admin/HeDaoTaos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeDaoTao heDaoTao = db.HeDaoTaos.Find(id);
            if (heDaoTao == null)
            {
                return HttpNotFound();
            }
            return View(heDaoTao);
        }

        // POST: Admin/HeDaoTaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HeDaoTao heDaoTao = db.HeDaoTaos.Find(id);
            db.HeDaoTaos.Remove(heDaoTao);
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
