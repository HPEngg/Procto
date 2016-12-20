using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models
{
    public class PrescriptionCategory
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Medicine> Medicines { get; set; }
    }
}