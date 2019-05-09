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
            ViewBag.Labels = "Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre";

            var cantidadMes = "";

            DateTime fechaActual = DateTime.Today;
            int mes = fechaActual.Month;
            int año = fechaActual.Year;
            var data = db.OrderMaster
                  .GroupBy(p => p.DateTime.Month)
                  .Select(g => new { month = g.Key, count = g.Count() });
            int counter = 1;
            foreach (var item in data)
            {
                if(item.month != counter)
                {
                    if(counter < item.month)
                    {
                        while(counter < item.month)
                        {
                            cantidadMes = cantidadMes + 0 + ",";
                            counter++;
                        }
                    }
                    else
                    {
                        cantidadMes = cantidadMes + item.count + ",";
                    }
                }

                if (counter >= item.month)
                {
                    cantidadMes = cantidadMes + item.count + ",";
                    counter++;
                }

            }
    
            ViewBag.DataValues = cantidadMes;


            int Total = db.OrderMaster.Count();
            int Entregado = db.OrderMaster.Count(e => e.Delivered == true);
            int Resultado = 0;

            if (Total > 0)
            {
                Resultado = (Entregado * 100) / Total;
            }
            ViewBag.PedidosDespachados = Resultado;
            ViewBag.PedidosTotal = Total;


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