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
    public class NewEntriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewEntries
        public ActionResult Index()
        {
            var newEntries = db.NewEntries.Include(n => n.Items);
            return View(newEntries.ToList());
        }

        // GET: NewEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewEntry newEntry = db.NewEntries.Find(id);
            if (newEntry == null)
            {
                return HttpNotFound();
            }
            return View(newEntry);
        }

        // GET: NewEntries/Create
        public ActionResult Create()
        {
            ViewBag.IdItem = new SelectList(db.Items, "Id", "NameItem");
            return View();
        }

        // POST: NewEntries/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CorrelativeBill,IdItem,InQuantity,Distributor")] NewEntry newEntry)
        {
            if (ModelState.IsValid)
            {
                db.NewEntries.Add(newEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdItem = new SelectList(db.Items, "Id", "NameItem", newEntry.IdItem);
            return View(newEntry);
        }

        // GET: NewEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewEntry newEntry = db.NewEntries.Find(id);
            if (newEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdItem = new SelectList(db.Items, "Id", "NameItem", newEntry.IdItem);
            return View(newEntry);
        }

        // POST: NewEntries/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CorrelativeBill,IdItem,InQuantity,Distributor")] NewEntry newEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdItem = new SelectList(db.Items, "Id", "NameItem", newEntry.IdItem);
            return View(newEntry);
        }

        // GET: NewEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewEntry newEntry = db.NewEntries.Find(id);
            if (newEntry == null)
            {
                return HttpNotFound();
            }
            return View(newEntry);
        }

        // POST: NewEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewEntry newEntry = db.NewEntries.Find(id);
            db.NewEntries.Remove(newEntry);
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
