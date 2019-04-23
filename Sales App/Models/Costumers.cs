using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class Costumers
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }
        [Display(Name = "Nombres")]
        public string Name { get; set; }
        [Display(Name = "Nit")]
        public string Nit { get; set; }
        [Display(Name = "Direccion")]
        public string Address { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        public int? DepartmentId { get; set; }
        [ForeignKey("TownshipId")]
        public virtual Townships Townships { get; set; }
        public int? TownshipId { get; set; }
        
        public ICollection<OrderMaster> Orders { get; set; }
    }
}