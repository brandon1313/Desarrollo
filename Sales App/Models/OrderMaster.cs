
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sales_App.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_App.Models
{
    public class OrderMaster
    {
       
        public int OrderMasterID { get; set; }


        [ForeignKey("SellersID")]
        public virtual Sellers Sellers { get; set; }
        public int? SellersID { get; set; }

        [ForeignKey("CostumerID")]
        public virtual Costumers Costumers { get; set; }
        public int? CostumerID { get; set; }

        [Display(Name = "Fecha Pedido")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Aprobado")]
        public Boolean Approved { get; set; }

        [Display(Name = "Despachado")]
        public Boolean Delivered { get; set; }



        public ICollection<OrderDetail> OrderDetail { get; set; }
        

    }
}