using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class Stock
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        [Display(Name = "Identificador del Producto")]
        public virtual Items Items { get; set; }
        public int ItemId { get; set; }

        [Display(Name = "Ingreso")]
        public double In { get; set; }

        [Display(Name = "Salida")]
        public double Out { get; set; }

        [ForeignKey("IdNewEntry")]
        public virtual NewEntry NewEntry{ get; set; }
        public int? IdNewEntry { get; set; }

        [ForeignKey("IdOrderMaster")]
        [Display(Name = "Pedido Salida")]
        public virtual OrderMaster OrderMaster { get; set; }
        public int IdOrderMaster { get; set; }

        



    }
}