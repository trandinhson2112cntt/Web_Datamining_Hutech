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
    public class DiemXetTuyensController : Controller
    {
        private WebDbContext db = new WebDbContext();

        // GET: Admin/DiemXetTuyens
        public ActionResult Index()
        {
            return View(db.DiemXetTuyens.ToList());
        }

        // GET: Admin/DiemXetTuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemXetTuyen diemXetTuyen = db.DiemXetTuyens.Find(id);
            if (diemXetTuyen == null)
            {
                return HttpNotFound();
            }
            return View(diemXetTuyen);
        }

        // GET: Admin/DiemXetTuyens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DiemXetTuyens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DXT_ID,DiemToan,DiemVan,DiemLy,DiemHoa,DiemSinh,DiemDia,DiemGDCD,DiemNN,HinhThucXetTuyen")] DiemXetTuyen diemXetTuyen)
        {
            if (ModelState.IsValid)
            {
                db.DiemXetTuyens.Add(diemXetTuyen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diemXetTuyen);
        }

        // GET: Admin/DiemXetTuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemXetTuyen diemXetTuyen = db.DiemXetTuyens.Find(id);
            if (diemXetTuyen == null)
            {
                return HttpNotFound();
            }
            return View(diemXetTuyen);
        }

        // POST: Admin/DiemXetTuyens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DXT_ID,DiemToan,DiemVan,DiemLy,DiemHoa,DiemSinh,DiemDia,DiemGDCD,DiemNN,HinhThucXetTuyen")] DiemXetTuyen diemXetTuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemXetTuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diemXetTuyen);
        }

        // GET: Admin/DiemXetTuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemXetTuyen diemXetTuyen = db.DiemXetTuyens.Find(id);
            if (diemXetTuyen == null)
            {
                return HttpNotFound();
            }
            return View(diemXetTuyen);
        }

        // POST: Admin/DiemXetTuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemXetTuyen diemXetTuyen = db.DiemXetTuyens.Find(id);
            db.DiemXetTuyens.Remove(diemXetTuyen);
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
