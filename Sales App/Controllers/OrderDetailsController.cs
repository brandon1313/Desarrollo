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
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var order_Detail = db.Order_Detail.Include(o => o.Items).Include(o => o.OrderMaster);
            return View(order_Detail.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.Order_Detail.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ItemsId = new SelectList(db.Items, "Id", "nameItem");
            ViewBag.OrderMasterID = new SelectList(db.OrderMaster, "OrderMasterID", "OrderMasterID");
            return View();
        }

        // POST: OrderDetails/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemsId,OrderMasterID,quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Order_Detail.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemsId = new SelectList(db.Items, "Id", "nameItem", orderDetail.ItemsId);
            ViewBag.OrderMasterID = new SelectList(db.OrderMaster, "OrderMasterID", "OrderMasterID", orderDetail.OrderMasterID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.Order_Detail.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemsId = new SelectList(db.Items, "Id", "nameItem", orderDetail.ItemsId);
            ViewBag.OrderMasterID = new SelectList(db.OrderMaster, "OrderMasterID", "OrderMasterID", orderDetail.OrderMasterID);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemsId,OrderMasterID,quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemsId = new SelectList(db.Items, "Id", "nameItem", orderDetail.ItemsId);
            ViewBag.OrderMasterID = new SelectList(db.OrderMaster, "OrderMasterID", "OrderMasterID", orderDetail.OrderMasterID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.Order_Detail.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.Order_Detail.Find(id);
            db.Order_Detail.Remove(orderDetail);
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
