using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("IdMeasureUnit")]
        [Display(Name = "Unidad de Medida")]
        public virtual MeasureUnit MeasureUnit { get; set; }
        public int IdMeasureUnit { get; set; }



        public string ItemImage
        {
            get { return NameItem.Replace(" ", string.Empty) + ".jpg"; }
        }
        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<NewEntry> NewEntries { get; set; }


    }
}