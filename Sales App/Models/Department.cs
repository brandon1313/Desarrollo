using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sales_App.Models
{
    public class Department
    {
        [Display(Name = "Identificador")]
        public int Id { get; set; }
        [Display(Name = "Nombres")]
        public string DepartmentName { get; set; }
      
        public ICollection<Townships> Orders { get; set; }
    }
}