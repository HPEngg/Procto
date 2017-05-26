using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class ExpanseCategory
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<ExpanseCategory> ExpanseCategories { get; set; }
    }
}