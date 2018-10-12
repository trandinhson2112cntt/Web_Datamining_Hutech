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
    public class NganhTheoBoesController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/NganhTheoBoes
        public ActionResult Index()
        {
            return View(db.NganhTheoBos.ToList());
        }

        // GET: Admin/NganhTheoBoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganhTheoBo nganhTheoBo = db.NganhTheoBos.Find(id);
            if (nganhTheoBo == null)
            {
                return HttpNotFound();
            }
            return View(nganhTheoBo);
        }

        // GET: Admin/NganhTheoBoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NganhTheoBoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNganh,TeNganh")] NganhTheoBo nganhTheoBo)
        {
            if (ModelState.IsValid)
            {
                db.NganhTheoBos.Add(nganhTheoBo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nganhTheoBo);
        }

        // GET: Admin/NganhTheoBoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganhTheoBo nganhTheoBo = db.NganhTheoBos.Find(id);
            if (nganhTheoBo == null)
            {
                return HttpNotFound();
            }
            return View(nganhTheoBo);
        }

        // POST: Admin/NganhTheoBoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNganh,TeNganh")] NganhTheoBo nganhTheoBo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nganhTheoBo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nganhTheoBo);
        }

        // GET: Admin/NganhTheoBoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganhTheoBo nganhTheoBo = db.NganhTheoBos.Find(id);
            if (nganhTheoBo == null)
            {
                return HttpNotFound();
            }
            return View(nganhTheoBo);
        }

        // POST: Admin/NganhTheoBoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NganhTheoBo nganhTheoBo = db.NganhTheoBos.Find(id);
            db.NganhTheoBos.Remove(nganhTheoBo);
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
