using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class OrderView
    {
        public SellersOrder Sellers { get; set; }
        public ItemOrder Titles { get; set; }
        public Costumers Costumers { get; set; }
        public List<ItemOrder> Items { get; set; }

    }
}