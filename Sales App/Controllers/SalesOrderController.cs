using Sales_App.Filters;
using Sales_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sales_App.Controllers
{
    [VerifySession]
    public class SalesOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SalesOrder
        public ActionResult NewOrder()
        {
            OrderView orderView = new OrderView();
            orderView.Sellers = new SellersOrder();
            orderView.Items = new List<ItemOrder>();
            Session["OrderView"] = orderView;
            var list = db.Costumers.ToList();
            ViewBag.SellersID = new SelectList(list, "Id", "Name");
            return View(orderView);
        }



        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["OrderView"] as OrderView;
            int CostumerId = int.Parse(Request["SellersID"]);
            DateTime dateOrder = Convert.ToDateTime(Request["Sellers.OrderDate"]);
            OrderMaster orderNew = new OrderMaster
            {
                CostumerID = CostumerId ,
                DateTime = dateOrder

            };
            db.OrderMaster.Add(orderNew);
            db.SaveChanges();
            int lastorderId = db.OrderMaster.ToList().Select(o => o.OrderMasterID).Max();
            foreach (ItemOrder item in orderView.Items)
            {
                var detail = new OrderDetail()
                {
                    OrderMasterID = lastorderId,
                    ItemsId = item.Id,
                    quantity = item.quantity,
                    deliveryQuantity = item.quantity


                };
                db.Order_Detail.Add(detail);
            }

            db.SaveChanges();
            orderView = new OrderView();
            orderView = Session["OrderView"] as OrderView;
            orderView.Items.Clear();
            var list = db.Costumers.ToList();
            ViewBag.SellersID = new SelectList(list, "Id", "Name");
            return View(orderView);

        }
        [HttpGet]
        public ActionResult addProduct()
        {


            var listp = db.Items.ToList();
            ViewBag.ItemsId = new SelectList(listp, "Id", "nameItem");
            ViewBag.Lista = listp;
            return View();
        }

        [HttpPost]
        public ActionResult addProduct(ItemOrder item)
        {
            var orderView = Session["OrderView"] as OrderView;
            var itemId = int.Parse(Request["ItemsId"]);
            var product = db.Items.Find(itemId);
            item = new ItemOrder
            {
                Id = product.Id,
                NameItem = product.NameItem,
                Price = product.Price,
                quantity = int.Parse(Request["quantity"])

            };
            orderView.Items.Add(item);
            var list = db.Costumers.ToList();
            ViewBag.SellersID = new SelectList(list, "Id", "Name");
            var listp = db.Items.ToList();
            ViewBag.ItemsId = new SelectList(listp, "Id", "nameItem");
            return View("NewOrder", orderView);
        }
        [HttpPost]
        public JsonResult Carrito(int id, int cantidad)
        {
            var orderView = Session["OrderView"] as OrderView;
            if (orderView == null)
            {
                Session["OrderView"] = new OrderView();
                orderView = Session["OrderView"] as OrderView;
            }

            int indexExistente = getIndex(id);
            var product = db.Items.Find(id);
            if (indexExistente == -1)
            {


                var item = new ItemOrder
                {
                    Id = product.Id,
                    NameItem = product.NameItem,
                    Price = product.Price,
                    quantity = cantidad
                };
                orderView.Items.Add(item);

            }
            else
            {


                var item = orderView.Items.ElementAt(indexExistente);
             
                orderView.Items[indexExistente].quantity = item.quantity + cantidad;
            }

       
            
            var list = db.Sellers.ToList();
            ViewBag.SellersID = new SelectList(list, "Id", "fullname");
            return Json(new { response = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Carrito()
        {
            var orderView = Session["OrderView"] as OrderView;
            var list = db.Costumers.ToList();
            ViewBag.SellersID = new SelectList(list, "Id", "Name");
            var listp = db.Items.ToList();
            return View("NewOrder", orderView);
        }
        public int getIndex(int id)
        {
            int index = -1;
            var orderView = Session["OrderView"] as OrderView;
            if(orderView.Items.Count != 0) { 
            for (int i = 0; i < orderView.Items.Count; i++)
            {
                if (orderView.Items[i].Id == id)
                {
                    index = orderView.Items.IndexOf(orderView.Items[i]);
                }
         
            }
            }
            return index;
        }

        public ActionResult RemoveItem(int Id)
        {
            var orderView = Session["OrderView"] as OrderView;
            if (orderView.Items.Count != 0)
            {
                for (int i = 0; i < orderView.Items.Count; i++)
                {
                    if (orderView.Items[i].Id == Id)
                    {
                        orderView.Items.RemoveAt(orderView.Items.IndexOf(orderView.Items[i]));
                    }

                }
            }
            
            var list = db.Costumers.ToList();
            ViewBag.SellersID = new SelectList(list, "Id", "Name");
            var listp = db.Items.ToList();
            return View("NewOrder", orderView);

        }

    }
    
}