using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales_App.Filters;
using Sales_App.Models;

namespace Sales_App.Controllers
{
    [VerifySession]
    public class TownshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Townships
        public ActionResult Index()
        {
            var townships = db.Townships.Include(t => t.Department);
            return View(townships.ToList());
        }

        // GET: Townships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Townships townships = db.Townships.Find(id);
            if (townships == null)
            {
                return HttpNotFound();
            }
            return View(townships);
        }

        // GET: Townships/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");
            return View();
        }

        // POST: Townships/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TownshipName,DepartmentId")] Townships townships)
        {
            if (ModelState.IsValid)
            {
                db.Townships.Add(townships);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", townships.DepartmentId);
            return View(townships);
        }

        // GET: Townships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Townships townships = db.Townships.Find(id);
            if (townships == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", townships.DepartmentId);
            return View(townships);
        }

        // POST: Townships/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TownshipName,DepartmentId")] Townships townships)
        {
            if (ModelState.IsValid)
            {
                db.Entry(townships).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", townships.DepartmentId);
            return View(townships);
        }

        // GET: Townships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Townships townships = db.Townships.Find(id);
            if (townships == null)
            {
                return HttpNotFound();
            }
            return View(townships);
        }

        // POST: Townships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Townships townships = db.Townships.Find(id);
            db.Townships.Remove(townships);
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
