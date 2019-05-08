using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales_App.Models;

namespace Sales_App.Controllers
{
    public class MeasureUnitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MeasureUnits
        public ActionResult Index()
        {
            return View(db.MeasureUnits.ToList());
        }

        // GET: MeasureUnits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = db.MeasureUnits.Find(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeasureUnits/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MeasureName")] MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                db.MeasureUnits.Add(measureUnit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = db.MeasureUnits.Find(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MeasureName")] MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measureUnit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = db.MeasureUnits.Find(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeasureUnit measureUnit = db.MeasureUnits.Find(id);
            db.MeasureUnits.Remove(measureUnit);
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
