using Sales_App.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sales_App.Models
{
    [VerifySession]
    public class CostumersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Costumers
        public ActionResult Index()
        {
            var costumers = db.Costumers.Include(c => c.Department).Include(c => c.Townships);
            return View(costumers.ToList());
        }

        // GET: Costumers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumers costumers = db.Costumers.Find(id);
            if (costumers == null)
            {
                return HttpNotFound();
            }
            return View(costumers);
        }

        // GET: Costumers/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");
            ViewBag.TownshipId = new SelectList(db.Townships, "Id", "TownshipName");
            return View();
        }

        // POST: Costumers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Nit,Address,DepartmentId,TownshipId")] Costumers costumers)
        {
            if (ModelState.IsValid)
            {
                db.Costumers.Add(costumers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", costumers.DepartmentId);
            ViewBag.TownshipId = new SelectList(db.Townships, "Id", "TownshipName", costumers.TownshipId);
            return View(costumers);
        }

        // GET: Costumers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumers costumers = db.Costumers.Find(id);
            if (costumers == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", costumers.DepartmentId);
            ViewBag.TownshipId = new SelectList(db.Townships, "Id", "TownshipName", costumers.TownshipId,"0");
            return View(costumers);
        }

        // POST: Costumers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Nit,Address,DepartmentId,TownshipId")] Costumers costumers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costumers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", costumers.DepartmentId);
            ViewBag.TownshipId = new SelectList(db.Townships, "Id", "TownshipName", costumers.TownshipId);
            return View(costumers);
        }

        // GET: Costumers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costumers costumers = db.Costumers.Find(id);
            if (costumers == null)
            {
                return HttpNotFound();
            }
            return View(costumers);
        }

        // POST: Costumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Costumers costumers = db.Costumers.Find(id);
            db.Costumers.Remove(costumers);
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
        [HttpPost]
        public ActionResult GetTownshipByDptId(int Dept)
        {
            IEnumerable<Townships> objcity = new List<Townships>();
            objcity = db.Townships.ToList().Where(a => a.DepartmentId == Dept);
            SelectList obgcity = new SelectList(objcity, "Id", "TownshipName", 0);
            return Json(obgcity);

     
        }
    }
}
