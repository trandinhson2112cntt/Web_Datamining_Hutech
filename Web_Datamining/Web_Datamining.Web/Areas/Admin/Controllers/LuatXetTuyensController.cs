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
    //public class LuatXetTuyensController : Controller
    //{
    //    private WebDbContext db = new WebDbContext();

    //    // GET: Admin/LuatXetTuyens
    //    public ActionResult Index()
    //    {
    //        return View(db.LuatXetTuyens.ToList());
    //    }

    //    // GET: Admin/LuatXetTuyens/Details/5
    //    public ActionResult Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        LoaiLuat luatXetTuyen = db.LuatXetTuyens.Find(id);
    //        if (luatXetTuyen == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(luatXetTuyen);
    //    }

    //    // GET: Admin/LuatXetTuyens/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Admin/LuatXetTuyens/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Id,X,Y,Support,Confidence")] LoaiLuat luatXetTuyen)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.LuatXetTuyens.Add(luatXetTuyen);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        return View(luatXetTuyen);
    //    }

    //    // GET: Admin/LuatXetTuyens/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        LuatXetTuyen luatXetTuyen = db.LuatXetTuyens.Find(id);
    //        if (luatXetTuyen == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(luatXetTuyen);
    //    }

    //    // POST: Admin/LuatXetTuyens/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,X,Y,Support,Confidence")] LuatXetTuyen luatXetTuyen)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(luatXetTuyen).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(luatXetTuyen);
    //    }

    //    // GET: Admin/LuatXetTuyens/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        LuatXetTuyen luatXetTuyen = db.LuatXetTuyens.Find(id);
    //        if (luatXetTuyen == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(luatXetTuyen);
    //    }

    //    // POST: Admin/LuatXetTuyens/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        LuatXetTuyen luatXetTuyen = db.LuatXetTuyens.Find(id);
    //        db.LuatXetTuyens.Remove(luatXetTuyen);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}
