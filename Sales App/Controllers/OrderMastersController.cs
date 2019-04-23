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
    public class OrderMastersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderMasters
        public ActionResult Index()
        {
            var orderMaster = db.OrderMaster.Include(o => o.Sellers).Where(o => o.Delivered != true);
            return View(orderMaster.ToList());
        }

        public ActionResult ToApproved()
        {
            var orderMaster = db.OrderMaster.Include(o => o.Sellers).Where(o => o.Delivered == true && o.Approved != true);
            return View(orderMaster.ToList());
        }
        [HttpGet]
        public ActionResult Delivery(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMaster orderMaster = db.OrderMaster.Find(id);
            if (orderMaster == null)
            {
                return HttpNotFound();
            }
            DeliveryView deliveryView = new DeliveryView();
            deliveryView.orderMaster = orderMaster;
            deliveryView.orderDetails = new List<ItemOrder>();
            List<OrderDetail> details = db.Order_Detail.Where(e => e.OrderMasterID == id).ToList();
            foreach(OrderDetail orderDetail in details)
            {
                var itemOrder = new ItemOrder
                {
                    detailId = orderDetail.Id,
                    NameItem = orderDetail.Items.NameItem,
                    Price = orderDetail.Items.Price,
                    quantity = Convert.ToInt32(orderDetail.quantity),
                    deliveryQuantity = orderDetail.deliveryQuantity


                };
                deliveryView.orderDetails.Add(itemOrder);
            }
            



            
            var list = db.Order_Detail.Where(e => e.OrderMasterID == id).Join(db.Items, e => e.ItemsId, d => d.Id, (detail, item) => new
            {
                Id = detail.Id,
                name = item.NameItem
            }).ToList();
            ViewBag.ItemOrder = new SelectList(list, "detailId", "name");


            Session["DeliverySession"] = deliveryView;
            return View(deliveryView);
            
        }

        [HttpPost]
        public ActionResult Delivery(DeliveryView orderView)
        {
            Session["DeliverySession"] = orderView;
            if (ModelState.IsValid)
            {
                foreach (ItemOrder item in orderView.orderDetails)
                {
                    var detail = db.Order_Detail.FirstOrDefault(o => o.Id == item.detailId);


                    detail.deliveryQuantity = item.deliveryQuantity;




                    db.SaveChanges();
                }
                var orderDelivered = db.OrderMaster.FirstOrDefault(o => o.OrderMasterID == orderView.orderMaster.OrderMasterID);
                orderDelivered.Delivered = true;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            var orderMaster = db.OrderMaster.Include(o => o.Sellers);
            return View(orderMaster.ToList());
        }

            public ActionResult GetQuantity(int detailId)
        {
            var quantity = db.Order_Detail.Where(d => d.Id == detailId).Select(d => d.quantity).SingleOrDefault();
            ViewBag.quantity = quantity + detailId;
            DeliveryView deliveryView = Session["DeliverySession"] as DeliveryView;

            var listd = db.Order_Detail.Where(e => e.OrderMasterID == deliveryView.orderMaster.OrderMasterID).Join(db.Items, e => e.ItemsId, d => d.Id, (detail, item) => new
            {
                Id = detail.Id,
                name = item.NameItem
            }).ToList();
            ViewBag.ItemOrder = new SelectList(listd, "Id", "name");
            
            
            
            return View("Delivery",deliveryView);

        }

        // GET: OrderMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMaster orderMaster = db.OrderMaster.Find(id);
            if (orderMaster == null)
            {
                return HttpNotFound();
            }
            DeliveryView deliveryView = new DeliveryView();
            deliveryView.orderMaster = orderMaster;
            deliveryView.orderDetails = new List<ItemOrder>();
            List<OrderDetail> details = db.Order_Detail.Where(e => e.OrderMasterID == id).ToList();
            foreach (OrderDetail orderDetail in details)
            {
                var itemOrder = new ItemOrder
                {
                    detailId = orderDetail.Id,
                    NameItem = orderDetail.Items.NameItem,
                    Price = orderDetail.Items.Price,
                    quantity = Convert.ToInt32(orderDetail.quantity),
                    deliveryQuantity = orderDetail.deliveryQuantity


                };
                deliveryView.orderDetails.Add(itemOrder);
            }
            return View(deliveryView);
        }

        // GET: OrderMasters/Create
        public ActionResult Create()
        {
            ViewBag.SellersID = new SelectList(db.Sellers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderMasterID,SellersID,dateTime")] OrderMaster orderMaster)
        {
            if (ModelState.IsValid)
            {
                db.OrderMaster.Add(orderMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SellersID = new SelectList(db.Sellers, "Id", "lastName", orderMaster.SellersID);
            return View(orderMaster);
        }

        // GET: OrderMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMaster orderMaster = db.OrderMaster.Find(id);
            if (orderMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.SellersID = new SelectList(db.Sellers, "Id", "lastName", orderMaster.SellersID);
            return View(orderMaster);
        }

        // POST: OrderMasters/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderMasterID,SellersID,dateTime")] OrderMaster orderMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SellersID = new SelectList(db.Sellers, "SellersID", "lastName", orderMaster.SellersID);
            return View(orderMaster);
        }

        // GET: OrderMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMaster orderMaster = db.OrderMaster.Find(id);
            if (orderMaster == null)
            {
                return HttpNotFound();
            }
            return View(orderMaster);
        }

        // POST: OrderMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderMaster orderMaster = db.OrderMaster.Find(id);
            db.OrderMaster.Remove(orderMaster);
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
