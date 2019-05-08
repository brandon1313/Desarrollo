using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales_App.Models
{
    public class MeasureUnit
    {
        [Display(Name ="Id")]
        public int Id { get; set; }
        [Display(Name = "Nombre de la Medida")]
        public string MeasureName { get; set; }

        public ICollection<Items> Items { get; set; }



    }
}