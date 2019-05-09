using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class ItemOrder:Items
    {
        public int detailId { get; set; }
        public int quantity { set; get; }
        public double partial { get { return Price * quantity; } }
        [Display(Name = "Cantidad Despachada")]
        public int deliveryQuantity { get; set; }
        [Display(Name = "Cantidad Aprobada")]
        public int approvedQuantity { get; set; }
    }
}