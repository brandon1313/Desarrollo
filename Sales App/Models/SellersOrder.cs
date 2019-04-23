using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class SellersOrder : Sellers
    {
        [Display(Name ="Fecha Pedido")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { set; get; }
    }
}