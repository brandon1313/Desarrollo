using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class DeliveryView
    {
        
        public OrderMaster orderMaster { get; set; }
        public ItemOrder Titles { get; set; }
        public List<ItemOrder> orderDetails { get; set; }
        
        
        


    }
}