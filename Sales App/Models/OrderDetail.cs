using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sales_App.Models
{
    public class OrderDetail
    {

        [Display(Name = "Id")]
        public int Id { set; get; }

        /* FK for the Items */
        [Display(Name = "Codigo del Producto")]
        
        public int ItemsId { set; get; }
        public virtual Items Items { get; set; }


        /* FK for the Order Head */
        [Display(Name = "Codio de Orden")]
        public int OrderMasterID { set; get; }
        public virtual OrderMaster OrderMaster { get; set; }

        [Display(Name = "Cantidad")]
        public double quantity { get; set; }

        [Display(Name = "Cantidad despachada")]
        public int deliveryQuantity { set; get; }

        [Display(Name = "Cantidad Aprobada")]
        public int approvedQuantity { set; get; }
    }
}