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
    public class ToHopMonsController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/ToHopMons
        public ActionResult Index()
        {
            return View(db.ToHopMons.ToList());
        }

        // GET: Admin/ToHopMons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToHopMon toHopMon = db.ToHopMons.Find(id);
            if (toHopMon == null)
            {
                return HttpNotFound();
            }
            return View(toHopMon);
        }

        // GET: Admin/ToHopMons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ToHopMons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaToHop,Mon1,Mon2,Mon3")] ToHopMon toHopMon)
        {
            if (ModelState.IsValid)
            {
                db.ToHopMons.Add(toHopMon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toHopMon);
        }

        // GET: Admin/ToHopMons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToHopMon toHopMon = db.ToHopMons.Find(id);
            if (toHopMon == null)
            {
                return HttpNotFound();
            }
            return View(toHopMon);
        }

        // POST: Admin/ToHopMons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaToHop,Mon1,Mon2,Mon3")] ToHopMon toHopMon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toHopMon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toHopMon);
        }

        // GET: Admin/ToHopMons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToHopMon toHopMon = db.ToHopMons.Find(id);
            if (toHopMon == null)
            {
                return HttpNotFound();
            }
            return View(toHopMon);
        }

        // POST: Admin/ToHopMons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ToHopMon toHopMon = db.ToHopMons.Find(id);
            db.ToHopMons.Remove(toHopMon);
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
