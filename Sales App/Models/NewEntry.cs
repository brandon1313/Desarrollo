using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_App.Models
{
    public class NewEntry
    {
        [Display(Name="Id")]
        public int Id { get; set; }
        [Display(Name = "Numero de Factura")]
        public string CorrelativeBill { set; get; }
        [ForeignKey("IdItem")]
        [Display(Name = "Producto")]
        public virtual Items Items { get; set; }
        public int IdItem { get; set; }
        [Display(Name = "Cantidad de Ingreso")]
        public double InQuantity { get; set; }
        [Display(Name = "Distribuidor")]
        public string Distributor { get; set; }



        public ICollection<Stock> Stocks { get; set; }




    }
}