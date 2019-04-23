using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class Items
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string NameItem { get; set; }
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        [Display(Name = "Precio")]
        public double Price { get; set; }
        [Display(Name = "Descuento %")]
        public double Discount { get; set; }
        
        public string ItemImage
        {
            get { return NameItem.Replace(" ", string.Empty) + ".jpg"; }
        }
        public ICollection<OrderDetail> OrderDetail { get; set; }
        

    }
}