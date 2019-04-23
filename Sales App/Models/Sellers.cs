using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Sales_App.Models
{
    public class Sellers
    {

       [Display(Name = "Identificador")]
        public int Id { get; set; }
       [Display(Name = "Apellidos")]
       public string LastName { get; set; }
        [Display(Name = "Nombres")]
        public string Name { get; set; }
        
        public string Fullname { get { return Name + " " + LastName; } }
        public  ICollection<OrderMaster> Orders { get; set; }
        

    }
}