using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class Townships
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }
        [Display(Name = "Nombre Municipio")]
        public string TownshipName { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        public int? DepartmentId { get; set; }

    }
}