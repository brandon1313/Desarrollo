using Sales_App.Filters;
using Sales_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales_App.Controllers
{
    [VerifySession]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            ViewBag.ForDeliver = db.OrderMaster.Count(e => e.Delivered != true);
            ViewBag.ForApproved = db.OrderMaster.Count(e => e.Approved != true);
            ViewBag.Labels = "Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic";

            var a = "";

            DateTime fechaActual = DateTime.Today;
            int mes = fechaActual.Month;
            var data = db.OrderMaster.GroupBy(o => o.DateTime.Month).Select(c => c.Count()).ToList();
            for (int i = 0; i < data.Count; i++ )
            {
                if(i == 0)
                {
                 
                    a = "" + data[i];
                }
                else { 
                Console.WriteLine(data[i]);
                a = a + "," + data[i];
                }

            }
            ViewBag.DataValues = a;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}