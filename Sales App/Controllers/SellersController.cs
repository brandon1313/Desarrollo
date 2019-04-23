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
    public class SellersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sellers1
        public ActionResult Index()
        {
            return View(db.Sellers.ToList());
        }

        // GET: Sellers1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sellers sellers = db.Sellers.Find(id);
            if (sellers == null)
            {
                return HttpNotFound();
            }
            return View(sellers);
        }

        // GET: Sellers1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sellers1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellersID,lastName,name")] Sellers sellers)
        {
            if (ModelState.IsValid)
            {
                db.Sellers.Add(sellers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sellers);
        }

        // GET: Sellers1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sellers sellers = db.Sellers.Find(id);
            if (sellers == null)
            {
                return HttpNotFound();
            }
            return View(sellers);
        }

        // POST: Sellers1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellersID,lastName,name")] Sellers sellers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sellers);
        }

        // GET: Sellers1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sellers sellers = db.Sellers.Find(id);
            if (sellers == null)
            {
                return HttpNotFound();
            }
            return View(sellers);
        }

        // POST: Sellers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sellers sellers = db.Sellers.Find(id);
            db.Sellers.Remove(sellers);
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
