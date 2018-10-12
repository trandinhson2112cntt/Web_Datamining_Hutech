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
    public class LuatsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/Luats
        public ActionResult Index()
        {
            return View(db.Luats.ToList());
        }

        // GET: Admin/Luats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luat luat = db.Luats.Find(id);
            if (luat == null)
            {
                return HttpNotFound();
            }
            return View(luat);
        }

        // GET: Admin/Luats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Luats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,X,Y,Support,Confidence")] Luat luat)
        {
            if (ModelState.IsValid)
            {
                db.Luats.Add(luat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(luat);
        }

        // GET: Admin/Luats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luat luat = db.Luats.Find(id);
            if (luat == null)
            {
                return HttpNotFound();
            }
            return View(luat);
        }

        // POST: Admin/Luats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,X,Y,Support,Confidence")] Luat luat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(luat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(luat);
        }

        // GET: Admin/Luats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luat luat = db.Luats.Find(id);
            if (luat == null)
            {
                return HttpNotFound();
            }
            return View(luat);
        }

        // POST: Admin/Luats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Luat luat = db.Luats.Find(id);
            db.Luats.Remove(luat);
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
